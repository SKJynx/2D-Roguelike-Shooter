using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountText : MonoBehaviour
{
    int m_currentAmmoType;

    Text m_ammoCount;

    int m_currentAmmo;

    WeaponController weaponController;
    PlayerInventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameManager.Instance.playerInventory.GetComponent<PlayerInventory>();
        weaponController = GameManager.Instance.weaponController.GetComponent<WeaponController>();

        m_ammoCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        m_currentAmmo = weaponController.m_currentAmmo;

        if (weaponController.isEquipped != false)
        {
            m_ammoCount.text = $"{m_currentAmmo} | {weaponController.m_maxAmmo}\n{weaponController.m_equippedAmmo}\nHP: {PlayerStatsManager.playerHealth}";
        }
        else
        {
            m_ammoCount.text = $"Unarmed\nHP: {PlayerStatsManager.playerHealth}";
        }


    }
}
