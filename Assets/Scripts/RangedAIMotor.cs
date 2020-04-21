using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIMotor : MonoBehaviour
{
    HealthManager healthManager;

    [SerializeField]
    ScriptableEnemies m_scriptableEnemies;
    [SerializeField]
    GameObject m_weapon;
    [SerializeField]
    GameObject m_target;
    [SerializeField]
    GameObject m_projectilePrefab;
    [SerializeField]
    float m_attackDamage;
    [SerializeField]
    float m_projectileSpeed;
    [SerializeField]
    float m_attackCooldown;
    [SerializeField]
    float m_attackTimer;

    [SerializeField]
    int shotsToFire;
    [SerializeField]
    int maxShotsAvailable;
    [SerializeField]
    float m_timeToReload;
    [SerializeField]
    float m_maxReloadTime;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GetComponent<HealthManager>();

        m_timeToReload = 0;

        m_attackDamage = m_scriptableEnemies.attackDamage;
        m_projectileSpeed = m_scriptableEnemies.attackSpeed;
        m_projectilePrefab = m_scriptableEnemies.projectile;
        m_attackCooldown = m_scriptableEnemies.attackFrequency;

        m_target = GameObject.FindGameObjectWithTag("Player");

        healthManager.health = m_scriptableEnemies.health;
    }


    void FixedUpdate()
    {

    }

    void Update()
    {
        m_attackTimer -= (1 * 60) * Time.deltaTime;
        m_timeToReload -= (1 * 60) * Time.deltaTime;

        if (m_target != null && m_attackTimer < 0 && shotsToFire > 0)
        {
            ShootTarget();
        }
        else if (m_timeToReload <= 0 && shotsToFire <= 0)
        {
            StartReload();
        }
        LookAtTarget();
    }

    void ShootTarget()
    {
       
        GameObject bullet = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        {

            Vector2 bulletPath = (m_target.transform.position - bullet.transform.position);

            bullet.GetComponent<Rigidbody2D>().velocity = bulletPath.normalized * m_projectileSpeed;

            Vector3 difference = m_target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            bullet.GetComponent<BulletScript>().m_bulletDamage = m_attackDamage;
        }
        shotsToFire -= 1;

        m_timeToReload = m_maxReloadTime;
        m_attackTimer = m_attackCooldown;
    }

    void StartReload()
    {
        print("cannont reloaded");
        shotsToFire = maxShotsAvailable;
    }


    void LookAtTarget()
    {
        Vector3 difference = m_target.transform.position - m_weapon.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        m_weapon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
