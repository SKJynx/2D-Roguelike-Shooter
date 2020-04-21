using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb2d;


    public float health;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        health = 100;
    }

    void Update()
    {

    }


    //TODO - Implement knockback system
    //void OnTriggerEnter2D(Collider2D otherCollider)
    //{
    //    if (otherCollider.CompareTag("Bullet"))
    //    {
    //        anim.Play("AITarget_Hit");
    //        print("enemy hit");
    //        Vector2 getTargetDir = (otherCollider.transform.position - transform.position).normalized;

    //        rb2d.velocity = new Vector2(getTargetDir.x * 4, getTargetDir.y * 4);
    //        print(getTargetDir);
    //    }
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;

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
