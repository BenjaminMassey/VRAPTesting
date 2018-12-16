using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour {

    private static GameObject gameQuestion;
    private static GameObject gameA;
    private static GameObject gameB;
    private static GameObject gameC;
    private static GameObject gameD;
    private static GameObject gameE;
    private static int correctAnswer; // 0 = A, 1 = B, etc.
    private static string[] questions;
    private static string[][] allAnswers;
    private static int currentQuestion;
    private static int n; // Counter of frames (30 frames = 1 second)

    // Use this for initialization
    void Start () {
        n = 0;
    }
    // FixedUpdate is called 30 times a second
    void FixedUpdate()
    {
        if (n == 15) // Should be in start, but need to delay this and I'm a bad programmer
        {
            gameQuestion = GameObject.Find("Question");
            gameA = GameObject.Find("Answer A");
            gameB = GameObject.Find("Answer B");
            gameC = GameObject.Find("Answer C");
            gameD = GameObject.Find("Answer D");
            gameE = GameObject.Find("Answer E");
            questions = TextParsing.getQuestions();
            allAnswers = TextParsing.getAnswers();
            resetText();
        }
        if (n > 15)
        {
            if (currentQuestion != CurrentQuestionHandler.getCurrentQuestion())
                resetText();
            currentQuestion = CurrentQuestionHandler.getCurrentQuestion();
        }
        n++;
    }
    
    private static void resetText()
    {
        gameQuestion.GetComponent<TextMesh>().text = autoWrap(questions[currentQuestion], 47);
        string[] answers = allAnswers[currentQuestion];
        for (int i = 0; i < answers.Length; i++)
        {
            char[] charArrayAnswer = answers[i].ToCharArray();
            if (charArrayAnswer[0] == '*')
            {
                correctAnswer = i;
                string goodAnswer = "";
                for (int j = 1; j < answers[i].Length; j++)
                    goodAnswer += charArrayAnswer[j];
                answers[i] = goodAnswer;
            }
        }
        gameA.GetComponent<TextMesh>().fontSize = 121;
        gameB.GetComponent<TextMesh>().fontSize = 121;
        gameC.GetComponent<TextMesh>().fontSize = 121;
        gameD.GetComponent<TextMesh>().fontSize = 121;
        gameE.GetComponent<TextMesh>().fontSize = 121;

        gameA.GetComponent<TextMesh>().text = autoWrap(answers[0], 40);
        if (getLineCount(gameA.GetComponent<TextMesh>().text) >= 4)
        {
            gameA.GetComponent<TextMesh>().fontSize = 100;
            gameA.GetComponent<TextMesh>().text = autoWrap(answers[0], 50);
        }
        gameB.GetComponent<TextMesh>().text = autoWrap(answers[1], 40);
        if (getLineCount(gameB.GetComponent<TextMesh>().text) >= 4)
        {
            gameB.GetComponent<TextMesh>().fontSize = 100;
            gameB.GetComponent<TextMesh>().text = autoWrap(answers[1], 50);
        }
        gameC.GetComponent<TextMesh>().text = autoWrap(answers[2], 40);
        if (getLineCount(gameC.GetComponent<TextMesh>().text) >= 4)
        {
            gameC.GetComponent<TextMesh>().fontSize = 100;
            gameC.GetComponent<TextMesh>().text = autoWrap(answers[2], 50);
        }
        gameD.GetComponent<TextMesh>().text = autoWrap(answers[3], 40);
        if (getLineCount(gameD.GetComponent<TextMesh>().text) >= 4)
        {
            gameD.GetComponent<TextMesh>().fontSize = 100;
            gameD.GetComponent<TextMesh>().text = autoWrap(answers[3], 50);
        }
        if (answers.Length > 4)
        {
            gameE.GetComponent<TextMesh>().text = autoWrap(answers[4], 40);
            if (getLineCount(gameE.GetComponent<TextMesh>().text) >= 4)
            {
                gameE.GetComponent<TextMesh>().fontSize = 100;
                gameE.GetComponent<TextMesh>().text = autoWrap(answers[4], 50);
            }
        }
        else {
            gameE.transform.position = new Vector3(0f, 90f, 0f);
            GameObject.Find("E Bubble").transform.position = new Vector3(0f, 90f, 0f);
        }
    }

    private static string simpleAutoWrap(string original, int charCount)
    {
        string newString = "";
        char[] charOriginal = original.ToCharArray();
        for (int i = 0; i < original.Length; i++)
        {
            newString += charOriginal[i];
            if (i % charCount == 0 && i != 0)
                newString += "\n";
        }
        return newString;
    }
    private static string autoWrap(string original, int charCount)
    {
        string newString = "";
        string[] words = original.ToString().Split(' ');
        string line = "";
        for (int i = 0; i < words.Length; i++) {
            int lengthWithNewWord = (line + words[i]).Length;
            if (lengthWithNewWord < charCount) // Fits
                line += words[i] + " ";
            else
            {// Doesn't fit
                newString += line + "\n";
                line = words[i] + " ";
            }
        }
        newString += line;
        return newString;
    }
    private static int getLineCount(string text)
    {
        string[] lines = text.ToString().Split('\n');
        return lines.Length;
    }

    public static int getCorrectAnswer() { return correctAnswer; }
}
