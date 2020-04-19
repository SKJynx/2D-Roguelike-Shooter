using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    // The tracking speed that will be multiplied and modified by Time.deltaTime to equal the interpolation rate used to create easing for the crosshair position.
    public float trackSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set Cursor to not be visible  and use the graphic cursor instead.
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float interpolation = trackSpeed * Time.deltaTime;

        // While not as "precise" I found the added easing on the interpolated movement made it feel much better.
        transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), interpolation);
    }
}
