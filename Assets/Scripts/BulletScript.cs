using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float m_bulletDamage;

    TextMesh textMesh;

    // Bullet's lifetime in seconds.
    [SerializeField]
    float bulletLifetime;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", bulletLifetime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Enemy") && gameObject.CompareTag("Bullet"))
            {
                print("Hit player");
                collision.gameObject.GetComponent<HealthManager>().TakeDamage((int)m_bulletDamage);
                PlayerStatsManager.playerDamageDealt += (int)m_bulletDamage;
                PlayerStatsManager.playerComboDamage += (int)m_bulletDamage;
                DestroySelf();
            }
        }

        if (collision != null)
        {
            if (collision.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
            {
                PlayerStatsManager.playerHealth -= (int)m_bulletDamage;

                collision.GetComponent<PlayerController>().CheckHealth();

                DestroySelf();
            }
        }
    }
}
