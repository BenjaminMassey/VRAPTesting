using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Globalization;

public class TextParsing : MonoBehaviour {
    private static string[] questions;
    private static string[][] answers;
    public static string filename = "Calc";

	// Use this for initialization
	void Start () {
        string pathToUnity = Application.dataPath;
        //Debug.Log("pathToUnity: " + pathToUnity);
        string pathInUnity = "/Resources/" + filename + ".txt";
        //Debug.Log("pathInUnity: " + pathInUnity);
        string path = pathToUnity + pathInUnity;
        //Debug.Log("dataPath + restOfIt: " + path);
        StreamReader file = File.OpenText(@path);
        
        int optionsNum = int.Parse(file.ReadLine());
        int questionsNum = (getFileLength(filename) - 1) / (optionsNum + 1);
        
        questions = new string[questionsNum];
        answers = new string[questionsNum][];

        //Debug.Log("Number of Options: " + optionsNum);
        Debug.Log("Number of Questions: " + questionsNum);

        
        for (int i = 0; i < questionsNum; i++)
        {
            //Debug.Log("i: " + i);
            questions[i] = file.ReadLine();
            //Debug.Log("questions[" + i + "]: " + questions[i]);
            string[] theseAnswers = new string[optionsNum];
            for (int j = 0; j < optionsNum; j++)
            {
                //Debug.Log("j: " + j);
                theseAnswers[j] = file.ReadLine();
                //Debug.Log("theseAnswers[" + j + "]: " + theseAnswers[j]);
            }
            answers[i] = theseAnswers;
            //Debug.Log("answers[" + i + "]: " + answers[i]);
        }
        /* Simple print out of the results
        for (int i = 0; i < questionsNum; i++)
        {
            Debug.Log("Question " + (i + 1) + ": " + questions[i]);
            for (int j = 0; j < optionsNum; j++)
            {
                string option = answers[i][j];
                if (option.ToCharArray()[0] == '*')
                    Debug.Log("Correct answer: " + option);
                else
                    Debug.Log("Incorrect answer: " + option);
            }
        }
        */
    }
    public static string[] getQuestions() { return questions; }
    public static string[][] getAnswers() { return answers; }
    // Update is called once per frame
    void Update () {}
    private static int getFileLength(string path)
    {
        StreamReader file = File.OpenText(path + ".txt");
        int numLines = 0;
        string line;
        while ((line = file.ReadLine()) != null)
            numLines++;
        return numLines;
    }
}
