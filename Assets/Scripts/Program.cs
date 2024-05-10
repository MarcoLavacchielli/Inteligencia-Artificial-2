using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Program : MonoBehaviour
{
    private Category selectedCategory;
    private Difficulty selectedDifficulty;
    private List<Question> questions;
    private Difficult difficultySelector;

    public TMP_Text textDisplay;
    public TextMeshProUGUI RightAnswerText;
    public UIManager UIManagerScript;
    public List<int> RightOrNotAnswers;
    public List<string> TheAnswersYouSelect;
    public int questionsAnsweredCounter;
    public TMP_Text textStatusDisplay;
    //TimerVariables
    public List<float> TimeTakenForEveryQuestion;
    public float Timer;
    public UIManager UIManager;
    public GameObject SecondPanel;
    public TextMeshProUGUI TimerText;
    public TMP_Text textTimeDisplay;

    void Start()
    {
        difficultySelector = FindObjectOfType<Difficult>();

        questions = new List<Question>
        {
            new Question("�Cu�l es la capital de Francia?", "Par�s", Category.Geograf�a, Difficulty.Easy),
            new Question("�En qu� a�o lleg� el hombre a la luna por primera vez?", "1969", Category.Historia, Difficulty.Easy),
            new Question("�Cu�l es el s�mbolo qu�mico del agua?", "H2O", Category.Ciencia, Difficulty.Easy),
            new Question("�Qui�n escribi� 'Don Quijote de la Mancha'?", "Miguel de Cervantes", Category.Literatura, Difficulty.Easy),
            new Question("�Qui�n escribi� 'Billy Summers'?", "Stephen King", Category.Literatura, Difficulty.Normal)
        };
    }
    private void Update()
    {
        if(SecondPanel.gameObject.activeInHierarchy)
        Timer += Time.deltaTime;
        TimerText.text = $"timer: {Timer}";

    }

    public string PrintPlayerStatus()
    {
        IEnumerable<string> correctAnswers = TheAnswersYouSelect
            .Where((answer, index) => RightOrNotAnswers[index] == 1)
            .TakeWhile((answer, index) => index < questionsAnsweredCounter)
            .OrderBy(x => x);

        IEnumerable<string> incorrectAnswers = TheAnswersYouSelect
            .Where((answer, index) => RightOrNotAnswers[index] == 2)
            .TakeWhile((answer, index) => index < questionsAnsweredCounter)
            .OrderBy(x => x);
        float tIMEtakenForeEveryQuestonEnum= TimeTakenForEveryQuestion.OrderByDescending(x=>x).Last();

        IEnumerable<string> combinedMessages = correctAnswers.Concat(incorrectAnswers);

        string statusMessage = string.Join("\n", combinedMessages);

        return statusMessage;
    }
    public string PrintBestTimeAndYouWonOrNotInStatus()
    {

        float suma = RightOrNotAnswers.Aggregate(0, (acum, current) => acum + current);
        double sumadetiempos = TimeTakenForEveryQuestion.Select(x => (double)x).Aggregate((total, next) => total + next);

        float tIMEtakenForeEveryQuestonEnum = TimeTakenForEveryQuestion.OrderByDescending(x => x).Last();
        string textThatWillBePrint = $" tu mejor tiempo:{tIMEtakenForeEveryQuestonEnum}. en total te ha llevado {sumadetiempos}";
        //bool YouPassOrNot = RightOrNotAnswers.Any(x => x > 1);
        if (suma != 3)
        {
            textThatWillBePrint += "\n no has pasado el nivel";
        }
        else
        {
            textThatWillBePrint += "\n has completado el nivel";
        }
        return textThatWillBePrint;
    }

    public void RightAnswerButtonFunction()
    {
        TimeTakenForEveryQuestion.Add(Timer);
        Timer = 0;
        RightOrNotAnswers.Add(1);
        TheAnswersYouSelect.Add(RightAnswerText.text);
        questionsAnsweredCounter++;
        if (questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            textStatusDisplay.text = PrintPlayerStatus();
            textTimeDisplay.text = PrintBestTimeAndYouWonOrNotInStatus();
            DisplayPlayerStatusOnUI();

        }
        else
        {
            UIManagerScript.GoBack();
        }
    }

    public void WrongAnswerButtonFunction()
    {
        TimeTakenForEveryQuestion.Add(Timer);
        Timer = 0;
        RightOrNotAnswers.Add(2);
        questionsAnsweredCounter++;
        TheAnswersYouSelect.Add("Respuesta Incorrecta");
        if (questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            textStatusDisplay.text = PrintPlayerStatus();
            textTimeDisplay.text = PrintBestTimeAndYouWonOrNotInStatus();
            DisplayPlayerStatusOnUI();
        }
        else
        {
            UIManagerScript.GoBack();
        }
    }

    string PrintAnswerInButtonText()
    {
        IEnumerable<object> selectedQuestionsByDifficulty;

        selectedQuestionsByDifficulty = questions.Where(q => q.Category == selectedCategory && q.Difficulty == selectedDifficulty)
                                                 .Select(q => new { QuestionText = q.Text, Answer = q.Answer })
                                                 .Take(selectedDifficulty == Difficulty.Easy ? 1 : selectedDifficulty == Difficulty.Normal ? 2 : questions.Count(q => q.Category == selectedCategory));

        string displayText = "";
        foreach (var q in selectedQuestionsByDifficulty)
        {
            var answer = q.GetType().GetProperty("Answer").GetValue(q, null);
            displayText += $"Respuesta: {answer}";
        }

        return displayText;
    }

    public void SelectCategoryMenuButtonFunction(string categoryName)
    {
        Category category;
        switch (categoryName.ToLower())
        {
            case "geograf�a":
                category = Category.Geograf�a;
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
                Debug.LogError("Categor�a no v�lida.");
                return;
        }

        selectedCategory = category;
        selectedDifficulty = difficultySelector.selectedDifficulty;

        textDisplay.text = PrintQuestionInUI();
        RightAnswerText.text = PrintAnswerInButtonText();
    }

    string PrintQuestionInUI()
    {
        IEnumerable<object> selectedQuestionsByDifficulty;

        selectedQuestionsByDifficulty = questions.Where(q => q.Category == selectedCategory && q.Difficulty == selectedDifficulty)
                                                 .Select(q => new { QuestionText = q.Text, Answer = q.Answer })
                                                 .Take(selectedDifficulty == Difficulty.Easy ? 1 : selectedDifficulty == Difficulty.Normal ? 2 : questions.Count(q => q.Category == selectedCategory));

        string displayText = $"Preguntas de la categor�a {selectedCategory} para la dificultad {selectedDifficulty}:\n";
        foreach (var q in selectedQuestionsByDifficulty)
        {
            var questionText = q.GetType().GetProperty("QuestionText").GetValue(q, null);
            displayText += $"Pregunta: {questionText}";
        }

        return displayText;
    }

    void DisplayPlayerStatusOnUI()
    {
        IEnumerable<string> correctAnswers = TheAnswersYouSelect
            .Where((answer, index) => RightOrNotAnswers[index] == 1)
            .TakeWhile((answer, index) => index < questionsAnsweredCounter)
            .OrderBy(x => x);

        IEnumerable<string> incorrectAnswers = TheAnswersYouSelect
            .Where((answer, index) => RightOrNotAnswers[index] == 2)
            .TakeWhile((answer, index) => index < questionsAnsweredCounter)
            .OrderBy(x => x);

        string correctAnswersText = string.Join("\n", correctAnswers);
        string incorrectAnswersText = string.Join("\n", incorrectAnswers);
    }
}

public enum Category
{
    Geograf�a,
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