using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    SpriteRenderer sr;

    public ScriptableWeapons scriptableWeapon;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (itemType == ItemTypes.Weapon)
        {
            sr.sprite = this.scriptableWeapon.weaponSprite;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponentInChildren<WeaponController>() != null && other.GetComponent<PlayerController>().pickupIsActive == 1)
        {
            if (itemType == ItemTypes.Weapon)
            {
                WeaponController playerWeapon = other.GetComponentInChildren<WeaponController>();

                FMODUnity.RuntimeManager.PlayOneShot(playerWeapon.m_pickupSFX);

                other.GetComponentInChildren<SpriteRenderer>().sprite = this.scriptableWeapon.weaponSprite;

                playerWeapon.m_scriptableWeapon = this.scriptableWeapon;

                playerWeapon.m_weaponName = this.scriptableWeapon.weaponName;

                playerWeapon.m_weaponDamage = this.scriptableWeapon.weaponDamage;
                playerWeapon.m_weaponCost = this.scriptableWeapon.weaponCost;
                playerWeapon.m_weaponAccuracy = this.scriptableWeapon.weaponAccuracy;
                playerWeapon.m_critChance = this.scriptableWeapon.critChance;
                playerWeapon.m_reloadTime = this.scriptableWeapon.reloadTime;
                playerWeapon.m_maxAmmo = this.scriptableWeapon.maxAmmo;
                playerWeapon.m_currentAmmo = this.scriptableWeapon.maxAmmo;
                playerWeapon.m_fireRate = this.scriptableWeapon.fireRate;
                playerWeapon.m_bulletSpeed = this.scriptableWeapon.bulletVelocity;
                playerWeapon.m_autofire = this.scriptableWeapon.automatic;
                playerWeapon.m_critMultiplier = this.scriptableWeapon.criticalMultiplier;
                playerWeapon.m_remainingReloadTime = 0;

                // Sound effects get passed through [FMODUnity.EventRef]
                playerWeapon.m_fireSFX = this.scriptableWeapon.weaponFireSound;
                playerWeapon.m_reloadSFX = this.scriptableWeapon.reloadSound;
                playerWeapon.m_endReloadSFX = this.scriptableWeapon.endReloadSound;
                playerWeapon.m_pickupSFX = this.scriptableWeapon.weaponPickupSound;

                // Type of bullet the weapon shoots
                playerWeapon.bulletPrefab = this.scriptableWeapon.bulletType;

                playerWeapon.itemID = this.scriptableWeapon.itemID;
            }

            Destroy(this.gameObject);

        }
    }

    public enum ItemTypes
    {
        Weapon,
        Healing,
        Throwable,
        Misc
    }
    public ItemTypes itemType;

}
