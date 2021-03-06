﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    SpriteRenderer sr;

    public int heldAmmo;

    public ScriptableWeapons scriptableWeapon;

    void Start()
    {
        //Checks ItemTypes enum to ensure an item always gets tagged if the enum is set
        if (ItemTypes.Weapon == itemType)
        {
            switch (itemType)
            {
                case ItemTypes.Weapon:
                    gameObject.tag = "Item";
                    break;
                case ItemTypes.Healing:
                    gameObject.tag = "Item";
                    break;
                case ItemTypes.Throwable:
                    gameObject.tag = "Item";
                    break;
                case ItemTypes.Misc:
                    gameObject.tag = "Item";
                    break;
                default:
                    print("Null tag set, couldn't find type");
                    break;
            }
        }
        sr = GetComponent<SpriteRenderer>();

        if (itemType == ItemTypes.Weapon)
        {
            sr.sprite = this.scriptableWeapon.weaponSprite;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        PlayerController playerController = other.GetComponent<PlayerController>();
        WeaponController playerWeapon = other.GetComponentInChildren<WeaponController>();

        if (other.GetComponentInChildren<WeaponController>() != null && playerController.pickupIsActive == 1 && playerInventory.savedWeapon[playerController.currentWeaponSlot].scriptableWeapon == null)
        {
            if (itemType == ItemTypes.Weapon)
            {
                playerWeapon.m_pickupSFX = this.scriptableWeapon.weaponPickupSound;

                FMODUnity.RuntimeManager.PlayOneShot(playerWeapon.m_pickupSFX);

                playerInventory.savedWeapon[playerController.currentWeaponSlot].scriptableWeapon = this.scriptableWeapon;

                playerWeapon.m_currentAmmo = this.heldAmmo;
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
