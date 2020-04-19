using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string m_fireSFX;

    [FMODUnity.EventRef]
    public string m_reloadSFX;

    SpriteRenderer sr;

    public bool isEquipped;

    public GameObject bulletPrefab;
    [SerializeField]
    GameObject firingAnimation;

    public string m_weaponName;

    public float m_weaponDamage;
    public float m_weaponCost;
    public float m_weaponAccuracy;
    public float m_critChance;
    public float m_reloadTime;
    public float m_currentAmmo;
    public float m_maxAmmo;
    public float m_fireRate;
    public float m_bulletSpeed;
    public float m_critMultiplier;
    public bool m_autofire;

    public float m_remainingReloadTime;

    float m_fireTimer;

    public int itemID;

    Animator anim;
    Animator firingAnimator;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        firingAnimator = firingAnimation.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        m_fireTimer -= 1;
        m_remainingReloadTime -= 1;
    }

    void Update()
    {
        // Autofire
        if (m_fireTimer < 0 && m_currentAmmo > 0 && m_remainingReloadTime < 0 && m_autofire == true && gameObject.GetComponentInParent<PlayerController>().isHolding == 1)
        {
            m_currentAmmo -= 1;
            m_fireTimer = m_fireRate;
            FireBullet();
        }

        if (sr.sprite != null)
        {
            isEquipped = true;
        }
        else
        {
            isEquipped = false;
        }
    }

    public void shootWeapon()
    {


        // SemiFire
        if (m_fireTimer < 0 && m_currentAmmo > 0 && m_remainingReloadTime < 0 && m_autofire == false && gameObject.GetComponentInParent<PlayerController>().isHolding == 1)
        {

            m_currentAmmo -= 1;
            m_fireTimer = m_fireRate;
            FireBullet();

        }

        if (m_currentAmmo == 0)
        {
            OnReload();
        }

    }

    void OnReload()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_reloadSFX);
        m_currentAmmo = m_maxAmmo;
        m_remainingReloadTime = m_reloadTime;
    }

    void FireBullet()
    {
        anim.Play("Player_Weapon_Fire", -1, 0);
        firingAnimator.Play("Weapon_Rifle_Fire", -1, 0);
        

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        {

            // TODO - Add proper bullet rotation, otherwise bullets won't be able to inherit accuracy
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 bulletPath = (target - bullet.transform.position);

            bullet.GetComponent<Rigidbody2D>().velocity = bulletPath.normalized * m_bulletSpeed;

            Vector3 difference = target - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            float critChance = Random.Range(0, 100);

            if (critChance <= m_critChance)
            {
                bullet.GetComponent<BulletScript>().m_bulletDamage = m_weaponDamage * m_critMultiplier;
            }
            else
            {
                bullet.GetComponent<BulletScript>().m_bulletDamage = m_weaponDamage;
            }



            //Debug.DrawLine(bullet.transform.position, target, Color.red, 1.0f);
        }
    }

}
