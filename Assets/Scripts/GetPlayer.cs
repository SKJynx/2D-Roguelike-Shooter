using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayer : MonoBehaviour
{

    public Vector2 getPlayerPos;

    void Awake()
    {

    }

    void Update()
    {
        GetPlayerLocation();
    }

    void GetPlayerLocation()
    {
        getPlayerPos = GameManager.PlayerTransform.position - transform.position;
    }


}
