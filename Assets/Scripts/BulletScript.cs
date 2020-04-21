using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float m_bulletDamage;

    TextMesh textMesh;

    // Bullet's lifetime in seconds.
    [SerializeField]
    float bulletLifetime;

    void Start()
    {
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
            if (collision.tag != "Player" && collision.tag != "Item" && collision.tag != "Bullet")
            {
                collision.gameObject.GetComponent<HealthManager>().health -= m_bulletDamage;
                PlayerStatsManager.playerDamageDealt += (int)m_bulletDamage;
                PlayerStatsManager.playerComboDamage += (int)m_bulletDamage;
                DestroySelf();
            }
        }
    }
}
