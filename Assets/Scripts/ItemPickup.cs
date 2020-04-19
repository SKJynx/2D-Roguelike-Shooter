using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    SpriteRenderer sr;

    public ScriptableWeapons sciptableWeapon;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (itemType == ItemTypes.Weapon)
        {
            sr.sprite = this.sciptableWeapon.weaponSprite;
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

                other.GetComponentInChildren<SpriteRenderer>().sprite = this.sciptableWeapon.weaponSprite;

                playerWeapon.m_weaponName = this.sciptableWeapon.weaponName;

                playerWeapon.m_weaponDamage = this.sciptableWeapon.weaponDamage;
                playerWeapon.m_weaponCost = this.sciptableWeapon.weaponCost;
                playerWeapon.m_weaponAccuracy = this.sciptableWeapon.weaponAccuracy;
                playerWeapon.m_critChance = this.sciptableWeapon.critChance;
                playerWeapon.m_reloadTime = this.sciptableWeapon.reloadTime;
                playerWeapon.m_maxAmmo = this.sciptableWeapon.maxAmmo;
                playerWeapon.m_currentAmmo = this.sciptableWeapon.maxAmmo;
                playerWeapon.m_fireRate = this.sciptableWeapon.fireRate;
                playerWeapon.m_bulletSpeed = this.sciptableWeapon.bulletVelocity;
                playerWeapon.m_autofire = this.sciptableWeapon.automatic;
                playerWeapon.m_critMultiplier = this.sciptableWeapon.criticalMultiplier;
                playerWeapon.m_remainingReloadTime = 0;

                // Sound effects get passed through [FMODUnity.EventRef]
                playerWeapon.m_fireSFX = this.sciptableWeapon.weaponFireSound;
                playerWeapon.m_reloadSFX = this.sciptableWeapon.reloadSound;
                playerWeapon.m_endReloadSFX = this.sciptableWeapon.endReloadSound;
                playerWeapon.m_pickupSFX = this.sciptableWeapon.weaponPickupSound;

                // Type of bullet the weapon shoots
                playerWeapon.bulletPrefab = this.sciptableWeapon.bulletType;

                playerWeapon.itemID = this.sciptableWeapon.itemID;
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
