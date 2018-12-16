using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalcTest : MonoBehaviour
{

    private static GameObject rhand; // Object for the player's right hand
    private static GameObject lhand; // Object for the player's left hand
    public static int touchframe;
    public static int counter;
    public static bool beenTouched = false;

    // Use this for initialization
    void Start()
    {
        rhand = GameObject.Find("hand_right");
        lhand = GameObject.Find("hand_left");
        touchframe = 0;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touched())
        {
            beenTouched = true;
            if (counter > touchframe + 30)
                touchframe = counter;
        }
        if (counter < touchframe + 30 && beenTouched)
        {
            if (counter < touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, -0.01f);
            if (counter >= touchframe + 15)
                transform.position = transform.TransformPoint(0f, 0f, 0.01f);
        }
        if (counter == touchframe + 30 && beenTouched)
        {
            TextParsing.filename = "Calc";
            resetLiterallyEverything();
            SceneManager.LoadScene("Library");
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
        {
            hx = lhand.transform.position.x;
            hy = lhand.transform.position.y;
            hz = lhand.transform.position.z;
            if (hx < x + 0.05f && hx > x - 0.05f && hy < y + 0.05f && hy > y - 0.05f && hz < z + 0.05f && hz > z - 0.05f)
                return true;
            else
                return false;
        }
    }
    void resetLiterallyEverything()
    {
        PsychTest.touchframe = 0;
        PsychTest.counter = 0;
        PsychTest.beenTouched = false;
        CalcTest.touchframe = 0;
        CalcTest.counter = 0;
        CalcTest.beenTouched = false;
        BioTest.touchframe = 0;
        BioTest.counter = 0;
        BioTest.beenTouched = false;
        CurrentQuestionHandler.currentQuestion = 1;
        CurrentQuestionHandler.counter = 0;
        CurrentQuestionHandler.lastTouch = 0;
        CurrentQuestionHandler.touchframe = 0;
        SampleQuestion.counter = 0;
        SampleQuestion.touchframe = 0;
        SampleQuestion.correct = false;
        SampleQuestion.answer = 0;
        ExitToSelect.touchframe = 0;
        ExitToSelect.counter = 0;
        ExitToSelect.beenTouched = false;
    }
}
