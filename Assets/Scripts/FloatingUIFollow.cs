using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUIFollow : MonoBehaviour
{

    GameObject player;

    Vector2 playerPos;

    // The tracking speed that will be multiplied and modified by Time.deltaTime to equal the interpolation rate used to create easing for the crosshair position.
    public float trackSpeed = 10.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    void FixedUpdate()
    {
        playerPos = player.transform.position;

        float interpolation = trackSpeed * Time.deltaTime;

        // To create an eased crosshair, I used the lerp function to interpolate between it's current position in the world and the mouse's position.
        // While not as "precise" I found the added easing on the interpolated movement made it feel much better.
        //transform.position = Vector2.Lerp(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), interpolation);
        transform.position = Vector2.Lerp(transform.position, new Vector2(playerPos.x, playerPos.y), interpolation);
    }
}
