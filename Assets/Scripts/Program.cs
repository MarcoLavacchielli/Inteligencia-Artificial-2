using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Program : MonoBehaviour
{
    public Category selectedCategory;
    private List<Question> questions;

    void Start()
    {
        // Ejemplo de lista de preguntas y respuestas
        questions = new List<Question>
        {
            new Question("�Cu�l es la capital de Francia?", "Par�s", Category.Geograf�a),
            new Question("�En qu� a�o lleg� el hombre a la luna por primera vez?", "1969", Category.Historia),
            new Question("�Cu�l es el s�mbolo qu�mico del agua?", "H2O", Category.Ciencia),
            new Question("�Qui�n escribi� 'Don Quijote de la Mancha'?", "Miguel de Cervantes", Category.Literatura)
        };

        ShowResults(selectedCategory); // Pasar la categor�a seleccionada como argumento
    }

    void ShowResults(Category selectedCategory)
    {
        // Utilizando LINQ para cumplir las consignas

        // 2 Select (M�nimo 1 concatenaci�n)
        var selectedQuestions = questions.Where(q => q.Category == selectedCategory)
                                         .Select(q => new { QuestionText = q.Text, Answer = q.Answer });
        var questionsWithoutAnswers = questions.Select(q => q.Text);

        // 2 Where (M�nimo 2 concatenaciones)
        var scienceQuestions = questions.Where(q => q.Category == Category.Ciencia);
        var difficultQuestions = questions.Where(q => q.Text.Length > 30);

        // 1 Take o Skip (M�nimo 1 concatenaci�n)
        var firstTwoQuestions = questions.Take(2);

        // 1 TakeWhile o SkipWhile (Sin m�nimo)
        var skipWhileShortQuestions = questions.SkipWhile(q => q.Text.Length < 20);

        // 1 First/Last/LastOrDefault/FirstOrDefault (M�nimo 2 concatenaciones)
        var firstHistoryQuestion = questions.First(q => q.Category == Category.Historia);
        var lastGeographyQuestion = questions.LastOrDefault(q => q.Category == Category.Geograf�a);

        // 1 Concat (Se puede utilizar + FList) (Sin m�nimo)
        var additionalQuestions = new List<Question>
        {
            new Question("�Cu�l es el r�o m�s largo del mundo?", "Nilo", Category.Geograf�a),
            new Question("�Cu�l es el planeta m�s grande del sistema solar?", "J�piter", Category.Ciencia)
        };
        var combinedQuestions = questions.Concat(additionalQuestions);

        // 1 SelectMany o Zip (Sin m�nimo)
        var combinedAnswers = questions.Zip(additionalQuestions, (q1, q2) => $"{q1.Answer} - {q2.Answer}");

        // 2 OrderBy o OrderByDescending (M�nimo 1 que no sea thenBy)
        var orderedQuestionsByCategory = questions.OrderBy(q => q.Category);
        var orderedQuestionsByLength = questions.OrderByDescending(q => q.Text.Length);

        // 1 ThenBy o ThenByDescending (Minimo 1 que no sea orderBy)
        var orderedQuestionsByCategoryAndLength = questions.OrderBy(q => q.Category).ThenByDescending(q => q.Text.Length);

        // Al menos una de ToList/ToArray/ToDictionary (Sin m�nimo)
        var questionsList = questions.ToList();
        var questionsArray = questions.ToArray();
        var questionsDictionary = questions.ToDictionary(q => q.Text, q => q.Answer);

        // 1 Any u All (sin m�nimo)
        var anyHistoryQuestions = questions.Any(q => q.Category == Category.Historia);
        var allQuestionsLongerThan20Chars = questions.All(q => q.Text.Length > 20);

        // 1 OfType (M�nimo 1 concatenaci�n)
        var onlyStringQuestions = questions.OfType<string>();

        // 2 Aggregate (sin m�nimo)
        var totalQuestionsLength = questions.Aggregate(0, (total, q) => total + q.Text.Length);
        var concatenatedAnswers = questions.Aggregate("", (result, q) => $"{result}, {q.Answer}");

        // Al menos una utilizaci�n de tipos compuestos en alg�n lugar del c�digo, para procesar o crear a partir de ciertos datos. (Tuplas/Tipos an�nimos)
        var categorizedQuestions = questions.GroupBy(q => q.Category);

        // Mostrar resultados en la consola de Unity
        Debug.Log($"Preguntas de la categor�a {selectedCategory}:");
        foreach (var q in selectedQuestions)
        {
            Debug.Log($"Pregunta: {q.QuestionText}, Respuesta: {q.Answer}");
        }

        // Mostrar otros resultados de LINQ...
    }
}

public enum Category
{
    Geograf�a,
    Historia,
    Ciencia,
    Literatura
}

public class Question
{
    public string Text { get; }
    public string Answer { get; }
    public Category Category { get; }

    public Question(string text, string answer, Category category)
    {
        Text = text;
        Answer = answer;
        Category = category;
    }
}