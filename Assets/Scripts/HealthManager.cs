using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float health;

    void Start()
    {
        health = 100;
    }

    void Update()
    {
        if (health <= 0)
        {
            KillObject();
        }
    }

    void KillObject()
    {
        Destroy(gameObject);
    }
}
