using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public ScriptableWeapons m_scriptableWeapon;

    [FMODUnity.EventRef]
    public string m_fireSFX;

    [FMODUnity.EventRef]
    public string m_reloadSFX;

    [FMODUnity.EventRef]
    public string m_endReloadSFX;

    [FMODUnity.EventRef]
    public string m_pickupSFX;

    SpriteRenderer sr;

    public bool isEquipped;
    [SerializeField]
    bool canReload;

    public GameObject bulletPrefab;
    [SerializeField]
    GameObject firingAnimation;

    PlayerInventory playerInventory;

    public string m_weaponName;

    public float m_weaponDamage;
    public float m_weaponCost;
    public float m_weaponAccuracy;
    public float m_critChance;
    public float m_reloadTime;
    public int m_currentAmmo;
    public int m_maxAmmo;
    public float m_fireRate;
    public float m_bulletSpeed;
    public float m_critMultiplier;
    public bool m_autofire;

    public float m_remainingReloadTime;

    float m_fireTimer;

    public int m_equippedAmmo;
    int m_ammoToLoad;

    public int itemID;

    public int m_currentAmmoPool;

    Animator anim;
    Animator firingAnimator;

    // Start is called before the first frame update
    void Start()
    {

        canReload = true;

        playerInventory = GetComponentInParent<PlayerInventory>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        firingAnimator = firingAnimation.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        m_fireTimer -= 1;
        m_remainingReloadTime -= 1;

        if (m_remainingReloadTime < 0 && canReload == false)
        {
            FinishReload();
        }
    }

    public void GetScriptableValues()
    {
        sr.sprite = this.m_scriptableWeapon.weaponSprite;

        m_weaponName = this.m_scriptableWeapon.weaponName;

        m_weaponDamage = this.m_scriptableWeapon.weaponDamage;
        m_weaponCost = this.m_scriptableWeapon.weaponCost;
        m_weaponAccuracy = this.m_scriptableWeapon.weaponAccuracy;
        m_critChance = this.m_scriptableWeapon.critChance;
        m_reloadTime = this.m_scriptableWeapon.reloadTime;
        m_maxAmmo = this.m_scriptableWeapon.maxAmmo;
        m_currentAmmo = this.m_scriptableWeapon.maxAmmo;
        m_fireRate = this.m_scriptableWeapon.fireRate;
        m_bulletSpeed = this.m_scriptableWeapon.bulletVelocity;
        m_autofire = this.m_scriptableWeapon.automatic;
        m_critMultiplier = this.m_scriptableWeapon.criticalMultiplier;
        m_remainingReloadTime = 0;

        // Sound effects get passed through [FMODUnity.EventRef]
        m_fireSFX = this.m_scriptableWeapon.weaponFireSound;
        m_reloadSFX = this.m_scriptableWeapon.reloadSound;
        m_endReloadSFX = this.m_scriptableWeapon.endReloadSound;
        m_pickupSFX = this.m_scriptableWeapon.weaponPickupSound;

        // Type of bullet the weapon shoots
        bulletPrefab = this.m_scriptableWeapon.bulletType;

        itemID = this.m_scriptableWeapon.itemID;
    }


    //When called, sets the ammo type for the currently equipped weapon to get and match the player's inventory amount.
    public void GetAmmo()
    {
        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Light)
        {
            m_equippedAmmo = playerInventory.ammoLightCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Light)
        {
            m_equippedAmmo = playerInventory.ammoLightCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Magnum)
        {
            m_equippedAmmo = playerInventory.ammoMagnumCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Assault)
        {
            m_equippedAmmo = playerInventory.ammoAssaultCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Heavy)
        {
            m_equippedAmmo = playerInventory.ammoHeavyCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Shell)
        {
            m_equippedAmmo = playerInventory.ammoShellCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Explosive)
        {
            m_equippedAmmo = playerInventory.ammoExplosiveCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Energy)
        {
            m_equippedAmmo = playerInventory.ammoEnergyCount;
        }
    }


    //When called, if the current assigned weapon name is not the name of the scriptable weapon's name, refresh the values to match.
    public void CheckCurrentWeapon()
    {
        if (m_scriptableWeapon != null)
        {
            isEquipped = true;
        }
        else
        {
            isEquipped = false;
        }

        if (m_scriptableWeapon != null)
        {
            if (m_weaponName != this.m_scriptableWeapon.name)
            {
                print("Changed weapon");
                GetScriptableValues();
            }

        }

    }

    void Update()
    {
        GetAmmo();

        CheckCurrentWeapon();

        // Autofire
        if (m_fireTimer < 0 && m_currentAmmo > 0 && m_remainingReloadTime < 0 && m_autofire == true && gameObject.GetComponentInParent<PlayerController>().isHolding == 1)
        {
            m_currentAmmo -= 1;
            m_fireTimer = m_fireRate;
            FireBullet();
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

        if (m_currentAmmo == 0 && canReload == true && m_ammoToLoad < m_equippedAmmo)
        {
            StartReload();
        }

    }

    void OnReload()
    {
        GetAmmo();
        m_ammoToLoad = m_maxAmmo - m_currentAmmo;


        if (canReload == true && m_currentAmmo < m_maxAmmo && m_ammoToLoad < m_equippedAmmo)
        {
            StartReload();
        }

    }

    void StartReload()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_reloadSFX);

        canReload = false;
        m_remainingReloadTime = m_reloadTime;
    }

    void FinishReload()
    {

        FMODUnity.RuntimeManager.PlayOneShot(m_endReloadSFX);

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Light)
        {
            playerInventory.ammoLightCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Magnum)
        {
            playerInventory.ammoMagnumCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Assault)
        {
            playerInventory.ammoAssaultCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Heavy)
        {
            playerInventory.ammoHeavyCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Shell)
        {
            playerInventory.ammoShellCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Explosive)
        {
            playerInventory.ammoExplosiveCount -= m_ammoToLoad;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Energy)
        {
            playerInventory.ammoEnergyCount -= m_ammoToLoad;
        }


        m_currentAmmo = m_maxAmmo;
        canReload = true;

        GetAmmo();
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

        }
    }

}
