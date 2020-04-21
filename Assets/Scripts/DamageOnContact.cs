using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField]
    float damage;


    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player") && otherCollider.GetComponent<PlayerController>().canBeHurt == true)
        {
            otherCollider.GetComponent<HealthManager>().TakeDamage((int)damage);
        }
    }
}
