using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCountText : MonoBehaviour
{
    Text m_ammoCount;

    int m_currentAmmo;

    GameObject player;

    WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        weaponController = GameManager.Instance.weaponController.GetComponent<WeaponController>();
        m_ammoCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentAmmo = weaponController.m_currentAmmo;

        m_ammoCount.text = $"{m_currentAmmo} | {weaponController.m_maxAmmo}";
    }
}
