using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuestion : MonoBehaviour {

    // Basic sample question that can both be used as a guideline and as an example

    public static bool correct; // Whether the answer is what we want
    private static GameObject rhand; // Object for the player's right hand
    private static GameObject correctness;
    private static Material red;
    private static Material green;
    public static int answer;
    public static int touchframe;
    public static int counter;

    // Use this for initialization
    void Start () {
        rhand = GameObject.Find("hand_right");
        correctness = GameObject.Find("Correctness");
        red = Resources.Load("red", typeof(Material)) as Material;
        green = Resources.Load("green", typeof(Material)) as Material;
        answer = QuestionGenerator.getCorrectAnswer();
        touchframe = 0;
        counter = 0;
    }

    // FixedUpdate is called 30 times a second
    void FixedUpdate () {
        answer = QuestionGenerator.getCorrectAnswer();
        if (Touched())
        {
            if (counter > touchframe + 30)
                touchframe = counter;
            if (answer == 0)
            {
                if (Bubble.bubbled && !Bubble2.bubbled && !Bubble3.bubbled && !Bubble4.bubbled && !Bubble5.bubbled)
                    correctness.GetComponent<Renderer>().material = green;
                else
                    correctness.GetComponent<Renderer>().material = red;
            }
            else if (answer == 1)
            {
                if (!Bubble.bubbled && Bubble2.bubbled && !Bubble3.bubbled && !Bubble4.bubbled && !Bubble5.bubbled)
                    correctness.GetComponent<Renderer>().material = green;
                else
                    correctness.GetComponent<Renderer>().material = red;
            }
            else if (answer == 2)
            {
                if (!Bubble.bubbled && !Bubble2.bubbled && Bubble3.bubbled && !Bubble4.bubbled && !Bubble5.bubbled)
                    correctness.GetComponent<Renderer>().material = green;
                else
                    correctness.GetComponent<Renderer>().material = red;
            }
            else if (answer == 3)
            {
                if (!Bubble.bubbled && !Bubble2.bubbled && !Bubble3.bubbled && Bubble4.bubbled && !Bubble5.bubbled)
                    correctness.GetComponent<Renderer>().material = green;
                else
                    correctness.GetComponent<Renderer>().material = red;
            }
            else if (answer == 4)
            {
                if (!Bubble.bubbled && !Bubble2.bubbled && !Bubble3.bubbled && !Bubble4.bubbled && Bubble5.bubbled)
                    correctness.GetComponent<Renderer>().material = green;
                else
                    correctness.GetComponent<Renderer>().material = red;
            }
            else
                Debug.Log("Unknown type of answer int!");
        }
        if (counter < touchframe + 30)
        {
            if (counter < touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, -0.01f);
            if (counter >= touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, 0.01f);
        }
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
}
