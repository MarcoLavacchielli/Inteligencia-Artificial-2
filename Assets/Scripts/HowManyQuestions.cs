using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyQuestions : MonoBehaviour
{
    public int questionsAmount;

    public void oneQuestion()
    {
        questionsAmount = 1;
    }

    public void twoQuestion()
    {
        questionsAmount = 2;
    }

    public void threeQuestion()
    {
        questionsAmount = 3;
    }
}
