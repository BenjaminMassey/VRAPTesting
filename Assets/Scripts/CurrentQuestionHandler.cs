using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentQuestionHandler : MonoBehaviour {

    private static GameObject rhand; // Object for the player's right hand
    public static int currentQuestion;
    public static int counter;
    public static int lastTouch;
    public static int touchframe;

    // Use this for initialization
    void Start () {
        rhand = GameObject.Find("hand_right");
        currentQuestion = 1;
        counter = 0;
        lastTouch = -30;
        touchframe = 0;
    }

    // FixedUpdate is called 30 times a second
    void FixedUpdate () {
        if (Touched())
        {
            if (counter > touchframe + 30)
                touchframe = counter;
        }
        if (counter < touchframe + 30)
        {
            if (counter < touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, -0.01f);
            if (counter >= touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, 0.01f);
        }
        if (Touched() && counter > lastTouch + 30) // Next question signal
        {
            if(!(currentQuestion + 1 > TextParsing.getQuestions().Length))
                currentQuestion += 1;
            lastTouch = counter;
            Bubble.bubbled = false;
            Bubble2.bubbled = false;
            Bubble3.bubbled = false;
            Bubble4.bubbled = false;
            Bubble5.bubbled = false;
            resetCorrectness();
        }
        GameObject.Find("Question Num").GetComponent<TextMesh>().text = currentQuestion.ToString();
        counter++;
    }

    bool Touched()
    // Did the player touch the ball with his right hand?
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float hx = rhand.transform.position.x;
        float hy = rhand.transform.position.y;
        float hz = rhand.transform.position.z;

        // Touched if within +/- 0.05 of each other
        if (hx < x + 0.05f && hx > x - 0.05f && hy < y + 0.05f && hy > y - 0.05f && hz < z + 0.05f && hz > z - 0.05f)
            return true;
        else
            return false;
    }
    public static void resetCorrectness()
    {
        GameObject correctness = GameObject.Find("Correctness");
        Material blank = Resources.Load("Default", typeof(Material)) as Material;
        correctness.GetComponent<Renderer>().material = blank;
    }
    public static int getCurrentQuestion() { return currentQuestion; }
}
