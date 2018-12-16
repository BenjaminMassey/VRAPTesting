using UnityEngine;
using System.Collections;
using System;

public class TouchControl : MonoBehaviour {

    // Allows the player to pick up the pencil. Apply to a hand/controller.

    public static bool grabbing; // Whether the pencil is being controlled by the player

    private OVRInput.Controller controller;
    public static bool touching; // Whether the hand is colliding with the pencil.
    private static GameObject pencil; // The pencil
    private static int n = 0; // Counting variable for debugging

	// Use this for initialization
	void Start () {
        // Shouldn't start out grabbing or touching the pencil
        grabbing = false;
        touching = false;
        pencil = GameObject.Find("Pencil");
    }
	
	// Update is called once per frame
	void Update () {
        n++;
        //DebugMessages();
        CheckTouching(); // Sets touching to true or false accordingly
        if (touching && OVRInput.Get(OVRInput.Button.Any)) // Touching and pressing a button = grabbing it
        {
            grabbing = true;
            pencil.transform.position = transform.TransformPoint(0.03f, 0.035f, -0.1f);
            pencil.transform.rotation = transform.rotation;
            pencil.transform.LookAt(transform.TransformPoint(0.05f, -0.5f, 0.9f));
            pencil.GetComponent<Rigidbody>().isKinematic = true; // Disbale physics while holding
        }
        else
        {
            grabbing = false; // Default false if not under conditions below
            pencil.GetComponent<Rigidbody>().isKinematic = false; // Allow pencil to have physics when not picked up
        }
    }
    void CheckTouching()
        // Check conditions and then set touching to true or false accordingly
    {
        float radius = 0.25f;

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float px = pencil.transform.position.x;
        float py = pencil.transform.position.y;
        float pz = pencil.transform.position.z;

        // If the pencil's coords are within +/- radius of the hands coords, then they are touching
        if (px < x + radius && px > x - radius && py < y + radius && py > y - radius && pz < z + radius && pz > z - radius)
            touching = true;
        else
            touching = false;
    }
    void DebugMessages()
    {
        if (touching)
            Debug.Log("Touching at time " + n);
        if (OVRInput.Get(OVRInput.Button.Any))
            Debug.Log("Pressing some button at time " + n);
    }
}
