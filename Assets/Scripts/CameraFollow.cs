using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public AnimationCurve animCurve;
    // Create the GameObject variable for Player 1. Made public to be accessed by the inspector and can specify the "player1" game object from the hierarchy.
    GameObject player1;

    // The tracking speed that will be multiplied and modified by Time.deltaTime to equal the interpolation rate used to create easing for the camera's position.
    public float trackSpeed = 2.0f;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        // The interpolation rate that will calculate the distance between the A & B values of the Lerp fucntion.
        // Time.deltaTime is the value of time passed since the previous frame.
        float interpolate = trackSpeed * Time.deltaTime;

        // The Vector3 position resolved as the transform of the tracked object to get position.y & position.x.
        // Using a high curve on the interpolated animation curve, I was able to get a smoothed camera that still tracked quickly.
        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, player1.transform.position.y, animCurve.Evaluate(interpolate));
        position.x = Mathf.Lerp(transform.position.x, player1.transform.position.x, animCurve.Evaluate(interpolate));

        transform.position = position;
    }
}

