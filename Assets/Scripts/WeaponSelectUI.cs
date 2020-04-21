using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_inventoryUISlot;

    [SerializeField]
    GameObject m_inventoryUISelect;

    [SerializeField]
    GameObject m_player;

    PlayerController m_playerController;
    WeaponController m_weaponController;

    SpriteRenderer m_weaponSlotSprite;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = m_player.GetComponent<PlayerController>();
        m_weaponController = m_player.GetComponentInChildren<WeaponController>();
        
        m_weaponSlotSprite = m_inventoryUISlot[m_playerController.currentWeaponSlot].GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_inventoryUISelect.transform.position = m_inventoryUISlot[m_playerController.currentWeaponSlot].transform.position;
        m_weaponSlotSprite.sprite = m_weaponController.m_scriptableWeapon.weaponSprite;
    }
}
