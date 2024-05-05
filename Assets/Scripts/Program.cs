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
            new Question("¿Cuál es la capital de Francia?", "París", Category.Geografía),
            new Question("¿En qué año llegó el hombre a la luna por primera vez?", "1969", Category.Historia),
            new Question("¿Cuál es el símbolo químico del agua?", "H2O", Category.Ciencia),
            new Question("¿Quién escribió 'Don Quijote de la Mancha'?", "Miguel de Cervantes", Category.Literatura)
        };

        ShowResults(selectedCategory); // Pasar la categoría seleccionada como argumento
    }

    void ShowResults(Category selectedCategory)
    {
        // Utilizando LINQ para cumplir las consignas

        // 2 Select (Mínimo 1 concatenación)
        var selectedQuestions = questions.Where(q => q.Category == selectedCategory)
                                         .Select(q => new { QuestionText = q.Text, Answer = q.Answer });
        var questionsWithoutAnswers = questions.Select(q => q.Text);

        // 2 Where (Mínimo 2 concatenaciones)
        var scienceQuestions = questions.Where(q => q.Category == Category.Ciencia);
        var difficultQuestions = questions.Where(q => q.Text.Length > 30);

        // 1 Take o Skip (Mínimo 1 concatenación)
        var firstTwoQuestions = questions.Take(2);

        // 1 TakeWhile o SkipWhile (Sin mínimo)
        var skipWhileShortQuestions = questions.SkipWhile(q => q.Text.Length < 20);

        // 1 First/Last/LastOrDefault/FirstOrDefault (Mínimo 2 concatenaciones)
        var firstHistoryQuestion = questions.First(q => q.Category == Category.Historia);
        var lastGeographyQuestion = questions.LastOrDefault(q => q.Category == Category.Geografía);

        // 1 Concat (Se puede utilizar + FList) (Sin mínimo)
        var additionalQuestions = new List<Question>
        {
            new Question("¿Cuál es el río más largo del mundo?", "Nilo", Category.Geografía),
            new Question("¿Cuál es el planeta más grande del sistema solar?", "Júpiter", Category.Ciencia)
        };
        var combinedQuestions = questions.Concat(additionalQuestions);

        // 1 SelectMany o Zip (Sin mínimo)
        var combinedAnswers = questions.Zip(additionalQuestions, (q1, q2) => $"{q1.Answer} - {q2.Answer}");

        // 2 OrderBy o OrderByDescending (Mínimo 1 que no sea thenBy)
        var orderedQuestionsByCategory = questions.OrderBy(q => q.Category);
        var orderedQuestionsByLength = questions.OrderByDescending(q => q.Text.Length);

        // 1 ThenBy o ThenByDescending (Minimo 1 que no sea orderBy)
        var orderedQuestionsByCategoryAndLength = questions.OrderBy(q => q.Category).ThenByDescending(q => q.Text.Length);

        // Al menos una de ToList/ToArray/ToDictionary (Sin mínimo)
        var questionsList = questions.ToList();
        var questionsArray = questions.ToArray();
        var questionsDictionary = questions.ToDictionary(q => q.Text, q => q.Answer);

        // 1 Any u All (sin mínimo)
        var anyHistoryQuestions = questions.Any(q => q.Category == Category.Historia);
        var allQuestionsLongerThan20Chars = questions.All(q => q.Text.Length > 20);

        // 1 OfType (Mínimo 1 concatenación)
        var onlyStringQuestions = questions.OfType<string>();

        // 2 Aggregate (sin mínimo)
        var totalQuestionsLength = questions.Aggregate(0, (total, q) => total + q.Text.Length);
        var concatenatedAnswers = questions.Aggregate("", (result, q) => $"{result}, {q.Answer}");

        // Al menos una utilización de tipos compuestos en algún lugar del código, para procesar o crear a partir de ciertos datos. (Tuplas/Tipos anónimos)
        var categorizedQuestions = questions.GroupBy(q => q.Category);

        // Mostrar resultados en la consola de Unity
        Debug.Log($"Preguntas de la categoría {selectedCategory}:");
        foreach (var q in selectedQuestions)
        {
            Debug.Log($"Pregunta: {q.QuestionText}, Respuesta: {q.Answer}");
        }

        // Mostrar otros resultados de LINQ...
    }
}

public enum Category
{
    Geografía,
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