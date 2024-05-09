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

    void Start()
    {
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

        IEnumerable<string> combinedMessages = correctAnswers.Concat(incorrectAnswers);

        string statusMessage = string.Join("\n", combinedMessages);

        return statusMessage;
    }

    public void RightAnswerButtonFunction()
    {
        RightOrNotAnswers.Add(1);
        TheAnswersYouSelect.Add(RightAnswerText.text);
        questionsAnsweredCounter++;
        if (questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            textStatusDisplay.text = PrintPlayerStatus();
            DisplayPlayerStatusOnUI();
        }
        else
        {
            UIManagerScript.GoBack();
        }
    }

    public void WrongAnswerButtonFunction()
    {
        RightOrNotAnswers.Add(2);
        questionsAnsweredCounter++;
        TheAnswersYouSelect.Add("Respuesta Incorrecta");
        if (questionsAnsweredCounter >= 3)
        {
            UIManagerScript.ShowStatus();
            textStatusDisplay.text = PrintPlayerStatus();
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

        string displayText = $"Preguntas de la categoría {selectedCategory} para la dificultad {selectedDifficulty}:\n";
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