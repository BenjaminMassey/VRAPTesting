using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble4 : MonoBehaviour {

    // Script for the answer bubbles to allow them to be bubbled in and exist in a binary state.

    public static bool bubbled; // Whether this bubble is filled in or not

    private static float leeway = 0.01f; // Distance away the pencil bit needs to be to trigger a bubble

    private static GameObject tip; // The tip of the pencil
    private static GameObject eraser; // The eraser at the end of the pencil
    private static Material blue;
    private static Material red;

    // Use this for initialization
    void Start()
    {
        tip = GameObject.Find("Tip");
        eraser = GameObject.Find("Eraser");
        bubbled = false; // Start out unfilled
        blue = Resources.Load("DarkBlue", typeof(Material)) as Material;
        red = Resources.Load("red", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float tx = tip.transform.position.x;
        float ty = tip.transform.position.y;
        float tz = tip.transform.position.z;

        float ex = eraser.transform.position.x;
        float ey = eraser.transform.position.y;
        float ez = eraser.transform.position.z;

        // If the pencil tip is within 0.015 of the bubble, then we bubble it in
        if (tx < x + leeway && tx > x - leeway && ty < y + leeway && ty > y - leeway && tz < z + leeway && tz > z - leeway)
            bubbled = true;
        // If the eraser is within 0.015 of the bubble, then we unbubble it
        if (ex < x + leeway && ex > x - 0.015f && ey < y + leeway && ey > y - leeway && ez < z + leeway && ez > z - leeway)
            bubbled = false;
        // Change color to indicate filled in state
        if (bubbled)
            GetComponent<Renderer>().material = red;
        else
            GetComponent<Renderer>().material = blue;
    }
}
