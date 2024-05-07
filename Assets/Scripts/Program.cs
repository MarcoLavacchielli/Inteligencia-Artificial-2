using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Program : MonoBehaviour
{
    private Category selectedCategory;
    private Difficulty selectedDifficulty; // Nueva variable para la dificultad
    private List<Question> questions;
    private Difficult difficultySelector; // Referencia al script de selecci�n de dificultad

    public TMP_Text textDisplay; // Referencia al objeto de texto en Unity usando TextMeshPro

    void Start()
    {
        // Obtener la referencia al script de selecci�n de dificultad
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

        selectedCategory = category; // Guardar la categor�a seleccionada

        // Obtener la dificultad seleccionada del script de selecci�n de dificultad
        selectedDifficulty = difficultySelector.selectedDifficulty;

        // Luego, llamar a la funci�n que selecciona las preguntas seg�n la dificultad
        SelectDifficultyQuestions();
    }

    void SelectDifficultyQuestions()
    {
        IEnumerable<object> selectedQuestionsByDifficulty;

        selectedQuestionsByDifficulty = questions.Where(q => q.Category == selectedCategory && q.Difficulty == selectedDifficulty)
                                                 .Select(q => new { QuestionText = q.Text, Answer = q.Answer })
                                                 .Take(selectedDifficulty == Difficulty.Easy ? 1 : selectedDifficulty == Difficulty.Normal ? 2 : questions.Count(q => q.Category == selectedCategory));

        string displayText = $"Preguntas de la categor�a {selectedCategory} para la dificultad {selectedDifficulty}:\n";
        foreach (var q in selectedQuestionsByDifficulty)
        {
            var questionText = q.GetType().GetProperty("QuestionText").GetValue(q, null);
            var answer = q.GetType().GetProperty("Answer").GetValue(q, null);
            displayText += $"Pregunta: {questionText}, Respuesta: {answer}\n";
        }

        // Actualiza el texto en el objeto de texto en Unity usando TextMeshPro
        textDisplay.text = displayText;
    }

    // Otras funciones del script...

    // Enumeraciones y clase de preguntas...
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