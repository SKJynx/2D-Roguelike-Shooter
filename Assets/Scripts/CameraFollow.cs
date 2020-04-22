using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player1, player2;
    public float minSizeY = 5f;
    public float minSizeX;
    public float minWidthX;

    Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        SetCameraPos();
        SetCameraSize();
    }

    void SetCameraPos()
    {
        Vector3 middle = (player1.position + player2.position) * 0.5f;

        cam.transform.position = new Vector3(
            middle.x,
            middle.y,
            cam.transform.position.z
        );
    }

    void SetCameraSize()
    {
        //horizontal size is based on actual screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;

        //multiplying by 0.5, because the ortographicSize is actually half the height
        float width = Mathf.Abs(player1.position.x - player2.position.x) * 0.5f;
        float height = Mathf.Abs(player1.position.y - player2.position.y) * 0.5f;

        //computing the size
        float camSizeX = Mathf.Max(width - minWidthX, minSizeX);
        cam.orthographicSize = Mathf.Max(height,
            camSizeX * Screen.height / Screen.width + 1, minSizeY);
    }

}