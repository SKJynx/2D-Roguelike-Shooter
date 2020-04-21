using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public ScriptableWeapons m_scriptableWeapon;

    public ScriptableWeapons m_Unarmed;

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
    PlayerController playerController;

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

    [SerializeField]
    bool willPartialReload;

    public bool canSwapWeapon;


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
        m_Unarmed = Resources.Load<ScriptableWeapons>("ScriptableObjects/Unarmed");
        willPartialReload = false;
        canReload = true;

        playerController = GetComponentInParent<PlayerController>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        firingAnimator = firingAnimation.GetComponentInChildren<Animator>();
    }

    //Gets the values for all the weapons and feeds it from a scriptable object.
    public void GetScriptableValues()
    {
        if (m_scriptableWeapon != null)
        {
            sr.sprite = this.m_scriptableWeapon.weaponSprite;

            m_weaponName = this.m_scriptableWeapon.weaponName;

            m_weaponDamage = this.m_scriptableWeapon.weaponDamage;
            m_weaponCost = this.m_scriptableWeapon.weaponCost;
            m_weaponAccuracy = this.m_scriptableWeapon.weaponAccuracy;
            m_critChance = this.m_scriptableWeapon.critChance;
            m_reloadTime = this.m_scriptableWeapon.reloadTime;
            m_maxAmmo = this.m_scriptableWeapon.maxAmmo;
            //m_currentAmmo = this.m_scriptableWeapon.maxAmmo;
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
    }


    //When called, sets the ammo type for the currently equipped weapon to get and match the player's inventory reserve amount.
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
        if (m_scriptableWeapon != null && m_scriptableWeapon != m_Unarmed)
        {
            isEquipped = true;
        }
        else
        {
            isEquipped = false;
        }

        if (isEquipped == false)
        {
            m_scriptableWeapon = m_Unarmed;
        }

        if (m_scriptableWeapon != null)
        {
            if (m_weaponName != this.m_scriptableWeapon.name)
            {
                GetScriptableValues();
            }
        }

        m_scriptableWeapon = playerInventory.savedWeapon[playerController.currentWeaponSlot].scriptableWeapon;

        //m_scriptableWeapon = playerInventory.scriptableWeaponSlot[playerController.currentWeaponSlot];
    }

    void Update()
    {

        m_fireTimer -=( 1*60 )*Time.deltaTime;
        m_remainingReloadTime -= (1 * 60) * Time.deltaTime; ;

        if (m_remainingReloadTime < 0 && canReload == false && willPartialReload == true)
        {
            print("Performed partial reload");
            PartialReload();
        }
        else if (m_remainingReloadTime < 0 && canReload == false)
        {
            print("Performed full reload");
            FinishReload();
        }

        if (m_remainingReloadTime > 0)
        {
            canSwapWeapon = false;
        }
        else
        {
            canSwapWeapon = true;
        }
        //If there is a scriptable weapon assigned, get its ammo type.
        if (m_scriptableWeapon != null)
        {
            GetAmmo();
        }

        CheckCurrentWeapon();


        // Autofire
        if (m_fireTimer < 0 && m_currentAmmo > 0 && m_remainingReloadTime < 0 && m_autofire == true && gameObject.GetComponentInParent<PlayerController>().isHolding == 1)
        {

            m_currentAmmo -= 1;
            m_fireTimer = m_fireRate;
            FireBullet();
            playerInventory.UpdateAmmo();
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
            playerInventory.UpdateAmmo();

        }


    }

    void OnReload()
    {
        GetAmmo();
        m_ammoToLoad = m_maxAmmo - m_currentAmmo;


        if (canReload == true && m_currentAmmo < m_maxAmmo && m_ammoToLoad < m_equippedAmmo && m_equippedAmmo != 0)
        {
            StartReload();
        }
        else if (canReload == true && m_currentAmmo < m_maxAmmo && m_ammoToLoad >= m_equippedAmmo && m_equippedAmmo != 0)
        {
            willPartialReload = true;
            StartReload();
        }

    }

    void StartReload()
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_reloadSFX);

        canReload = false;
        m_remainingReloadTime = m_reloadTime;
    }

    //Grabs the ammo from the correct ammo pool when doing a partial reload, and checks to make sure you don't go below 0.
    void PartialReload()
    {

        FMODUnity.RuntimeManager.PlayOneShot(m_endReloadSFX);

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Light)
        {
            m_currentAmmo += playerInventory.ammoLightCount;

            playerInventory.ammoLightCount -= playerInventory.ammoLightCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Magnum)
        {
            m_currentAmmo += playerInventory.ammoMagnumCount;

            playerInventory.ammoMagnumCount -= playerInventory.ammoMagnumCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Assault)
        {
            m_currentAmmo += playerInventory.ammoAssaultCount;

            playerInventory.ammoAssaultCount -= playerInventory.ammoAssaultCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Heavy)
        {
            m_currentAmmo += playerInventory.ammoHeavyCount;

            playerInventory.ammoHeavyCount -= playerInventory.ammoHeavyCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Shell)
        {
            m_currentAmmo += playerInventory.ammoShellCount;

            playerInventory.ammoShellCount -= playerInventory.ammoShellCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Explosive)
        {
            m_currentAmmo += playerInventory.ammoExplosiveCount;

            playerInventory.ammoExplosiveCount -= playerInventory.ammoExplosiveCount;
        }

        if (m_scriptableWeapon.ammoType == ScriptableWeapons.AmmoType.Energy)
        {
            m_currentAmmo += playerInventory.ammoEnergyCount;

            playerInventory.ammoEnergyCount -= playerInventory.ammoEnergyCount;
        }

        canReload = true;
        willPartialReload = false;
        GetAmmo();
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
