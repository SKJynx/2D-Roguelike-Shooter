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

                playerWeapon.GetScriptableValues();
                playerWeapon.CheckCurrentWeapon();


  
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
