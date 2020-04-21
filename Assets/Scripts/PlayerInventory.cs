using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    PlayerController playerController;
    WeaponController weaponController;

    public MyStruct[] savedWeapon;
    [System.Serializable]
    public struct MyStruct
    {
        public ScriptableWeapons scriptableWeapon;
        public string weaponName;
        public int ammoCount;
    }

    //public ScriptableWeapons[] scriptableWeaponSlot;
    //public int[] savedAmmoCount;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponentInChildren<WeaponController>();
    }

    void Update()
    {
        GetCurrentWeaponInfo();

        //savedAmmoCount[playerController.currentWeaponSlot] = weaponController.m_currentAmmo;
    }

    public void GetCurrentWeaponInfo()
    {

        savedWeapon[playerController.currentWeaponSlot].scriptableWeapon = weaponController.m_scriptableWeapon;
        savedWeapon[playerController.currentWeaponSlot].weaponName = weaponController.m_weaponName;
    }

    public void UpdateAmmo()
    {
        savedWeapon[playerController.currentWeaponSlot].ammoCount = weaponController.m_currentAmmo;
    }

    public int ammoLightCount;
    public int ammoMagnumCount;
    public int ammoAssaultCount;
    public int ammoHeavyCount;
    public int ammoSniperCount;
    public int ammoExplosiveCount;
    public int ammoShellCount;
    public int ammoEnergyCount;
    public int grenadeCount;

}
