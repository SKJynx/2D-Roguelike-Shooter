using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMotor : MonoBehaviour
{
    Rigidbody2D rb2d;


    [SerializeField]
    float maxSpeed;

    GetPlayer getPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        getPlayerScript = GetComponent<GetPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = getPlayerScript.getPlayerPos.normalized * maxSpeed;
    }
}
