using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Program : MonoBehaviour
{
    private Category selectedCategory;
    private Difficulty selectedDifficulty; // Nueva variable para la dificultad
    private List<Question> questions;
    private Difficult difficultySelector; // Referencia al script de selección de dificultad

    public TMP_Text textDisplay; // Referencia al objeto de texto en Unity usando TextMeshPro
    public TextMeshProUGUI RightAnswerText;
    //cosas para la respuesta correcta
    public UIManager UIManagerScript;
    public List<int> RightOrNotAnswers;//si Int es=1 es correcta si es =2 en incorrecta
    public List<string> TheAnswersYouSelect;// Paras que al terminar el juego(a las 5 preg poner un listado con un orderBy)
    public int questionsAnsweredCounter;
    public TMP_Text textStatusDisplay;



    void Start()
    {
        // Obtener la referencia al script de selección de dificultad
        difficultySelector = FindObjectOfType<Difficult>();

        questions = new List<Question>
        {
            new Question("¿Cuál es la capital de Francia?", "París", Category.Geografía, Difficulty.Easy),
            new Question("¿En qué año llegó el hombre a la luna por primera vez?", "1969", Category.Historia, Difficulty.Easy),
            new Question("¿Cuál es el símbolo químico del agua?", "H2O", Category.Ciencia, Difficulty.Easy),
            new Question("¿Quién escribió 'Don Quijote de la Mancha'?", "Miguel de Cervantes", Category.Literatura, Difficulty.Easy),
            new Question("¿Quién escribió 'Billy Summers'?", "Stephen King", Category.Literatura, Difficulty.Normal)
        };
    }
    public string PrintPlayerStatus()//sin side effect
    {
        IEnumerable<int> selectedRightAnswers;
        selectedRightAnswers= RightOrNotAnswers.TakeWhile(x => x == 1);
        IEnumerable<string> selectedArchiveAnswers;
        selectedArchiveAnswers = TheAnswersYouSelect.OrderBy(x => x);

        int f=selectedRightAnswers.Count();
        string m= $" total de respuestas correctas son{f}";
        if (selectedRightAnswers.Count() !=questionsAnsweredCounter)// solo se lee el counter asi que no cuenta como sideeffect(no?
        {
            m = "hubo respuestas incorrectas";
        }
        else
            m = $"El total de respuestas fueron respondidas correctamente({f} y fueron las siguiente:)";
        foreach(string q in selectedArchiveAnswers)
        {
            m += $"\nrta: {q}";
        }
        return m;
        
    }
    public void RightAnswerButtonFunction()// con side effect
    {
        RightOrNotAnswers.Add(1);
        TheAnswersYouSelect.Add(RightAnswerText.text);
        questionsAnsweredCounter++;
        if (questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            textStatusDisplay.text = PrintPlayerStatus();

        }
        else
            UIManagerScript.GoBack();

    }

    public void WrongAnswerButtonFunction()// con side effect
    {
        RightOrNotAnswers.Add(2);
        TheAnswersYouSelect.Add("Otro");
        questionsAnsweredCounter++;
        if(questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            //textStatusDisplay.text = $" la cantidad de respuestas correctas fueron {PrintPlayerStatus()}";
            textStatusDisplay.text = PrintPlayerStatus();
        }
        else
        UIManagerScript.GoBack();

    }
    string PrintAnswerInButtonText()// YA NO TIENE SIDE EFFECT   
    {
        IEnumerable<object> selectedQuestionsByDifficulty;

        selectedQuestionsByDifficulty = questions.Where(q => q.Category == selectedCategory && q.Difficulty == selectedDifficulty)
                                                 .Select(q => new { QuestionText = q.Text, Answer = q.Answer })
                                                 .Take(selectedDifficulty == Difficulty.Easy ? 1 : selectedDifficulty == Difficulty.Normal ? 2 : questions.Count(q => q.Category == selectedCategory));

        string displayText = $"";
        foreach (var q in selectedQuestionsByDifficulty)
        {
           
            var answer = q.GetType().GetProperty("Answer").GetValue(q, null);
            displayText += $"Respuesta: {answer}";
        }

        // Actualiza el texto en el objeto de texto en Unity usando TextMeshPro
        //textDisplay.text = displayText;
        return displayText;
    }

    public void SelectCategoryMenuButtonFunction(string categoryName)// con side effect
    {
        Category category;
        switch (categoryName.ToLower())
        {
            case "geografía":
                category = Category.Geografía;
                break;
            case "historia":
                category = Category.Historia;
                break;
            case "ciencia":
                category = Category.Ciencia;
                break;
            case "literatura":
                category = Category.Literatura;
                break;
            default:
                Debug.LogError("Categoría no válida.");
                return;
        }

        selectedCategory = category; // Guardar la categoría seleccionada

        // Obtener la dificultad seleccionada del script de selección de dificultad
        selectedDifficulty = difficultySelector.selectedDifficulty;

        // Luego, llamar a la función que selecciona las preguntas según la dificultad
        textDisplay.text = PrintQuestionInUI();
        RightAnswerText.text = PrintAnswerInButtonText();
        //PrintQuestionInUI();
    }


    string PrintQuestionInUI()// YA NO TIENE SIDE EFFECT   
    {
        IEnumerable<object> selectedQuestionsByDifficulty;

        selectedQuestionsByDifficulty = questions.Where(q => q.Category == selectedCategory && q.Difficulty == selectedDifficulty)
                                                 .Select(q => new { QuestionText = q.Text, Answer = q.Answer })
                                                 .Take(selectedDifficulty == Difficulty.Easy ? 1 : selectedDifficulty == Difficulty.Normal ? 2 : questions.Count(q => q.Category == selectedCategory));

        string displayText = $"Preguntas de la categoría {selectedCategory} para la dificultad {selectedDifficulty}:\n";
        foreach (var q in selectedQuestionsByDifficulty)
        {
            var questionText = q.GetType().GetProperty("QuestionText").GetValue(q, null);
            
            displayText += $"Pregunta: {questionText}";
        }

        // Actualiza el texto en el objeto de texto en Unity usando TextMeshPro
        //textDisplay.text = displayText;
        return displayText;
    }

    // Otras funciones del script...

    // Enumeraciones y clase de preguntas...
}



public enum Category
{
    Geografía,
    Historia,
    Ciencia,
    Literatura
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public class Question
{
    public string Text { get; }
    public string Answer { get; }
    public Category Category { get; }
    public Difficulty Difficulty { get; }

    public Question(string text, string answer, Category category, Difficulty difficulty)
    {
        Text = text;
        Answer = answer;
        Category = category;
        Difficulty = difficulty;
    }
}