﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float dodgeSpeed;

    [SerializeField]
    bool canInput;

    public bool canBeHurt;

    public float dodgeTimer;
    public float max_dodgeTimer;

    public bool facingRight = true;
    public float isHolding;

    public float pickupIsActive;
    [SerializeField]
    float ability1IsActive;


    [SerializeField]
    GameObject reticle;

    [SerializeField]
    GameObject heldItem;
    [SerializeField]
    GameObject playerSprite;
    [SerializeField]
    GameObject blankItemPrefab;

    Vector2 movement;

    Rigidbody2D rb2d;

    Animator playerAnimator;

    Animator anim;

    PlayerInventory playerInventory;

    WeaponController weaponController;

    float fixedDeltaTime;

    public int currentWeaponSlot;

    public int maxWeaponSlots;
    //EXPERIMENTAL CODE------------------
    [SerializeField]
    float slowMotionAmount;


    //-----------------------------------

    private void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsManager.playerHealth = 100;

        canBeHurt = true;
        currentWeaponSlot = 0;
        maxWeaponSlots = 4;
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        playerInventory = this.GetComponent<PlayerInventory>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponentInChildren<Animator>();
        playerAnimator = this.gameObject.GetComponent<Animator>();

    }


    void Update()
    {
        ;
        dodgeTimer -= (1 * 60) * Time.deltaTime;

        if (canInput == true)
        {
            rb2d.velocity = movement * maxSpeed;
        }

        AddAmmoCheat();

        // EXPERIMENTAL CODE  ------------
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;


        SlowMotionAbility();
        // -------------------------------

        FixWeaponOrientation();
        LookAtReticle();

        anim.SetFloat("horizontalSpeed", Mathf.Abs(rb2d.velocity.x));
        anim.SetFloat("verticalSpeed", Mathf.Abs(rb2d.velocity.y));
    }

    //Experimental
    void SlowMotionAbility()
    {


        if (ability1IsActive == 1)
        {
            if (Time.timeScale > 0.3f)
            {
                Time.timeScale -= 2.0f * Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0.3f;
            }

        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void CheckHealth()
    {
        if (PlayerStatsManager.playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        isHolding = value.Get<float>();

        if (GetComponentInChildren<WeaponController>().isEquipped == true)
        {
            gameObject.GetComponentInChildren<WeaponController>().shootWeapon();
        }
    }

    public void OnMeleeSwing()
    {
        print("swung");
    }

    void OnAbility1(InputValue value)
    {
        ability1IsActive = value.Get<float>();
    }

    public void OnDodge()
    {
        if (dodgeTimer < 0)
        {
            dodgeTimer = max_dodgeTimer;

            canInput = false;

            playerAnimator.Play("Player_Dodge", -1, 0);
            rb2d.velocity = new Vector2(movement.x, movement.y).normalized * dodgeSpeed;
        }
    }

    public void OnInteract(InputValue value)
    {
        pickupIsActive = value.Get<float>();
    }

    void FixWeaponOrientation()
    {
        // TODO - This is a pretty trash way of setting the proper weapon orientation. Use proper angles later.
        if (reticle.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            heldItem.transform.localScale = new Vector2(-1, -1);
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            heldItem.transform.localScale = new Vector2(1, 1);
        }
    }

    void LookAtReticle()
    {
        Vector3 difference = reticle.transform.position - heldItem.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        heldItem.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

    }




    public void OnSwitchWeaponNext()
    {
        if (weaponController.canSwapWeapon == true)
        {
            if (currentWeaponSlot < maxWeaponSlots - 1)
            {
                currentWeaponSlot++;

            }
            else
            {
                currentWeaponSlot = 0;
            }
            weaponController.m_currentAmmo = playerInventory.savedWeapon[currentWeaponSlot].ammoCount;
        }

    }

    public void OnSwitchWeaponPrevious()
    {
        if (weaponController.canSwapWeapon == true)
        {
            if (currentWeaponSlot > 0)
            {
                currentWeaponSlot--;

            }

            else
            {
                currentWeaponSlot = maxWeaponSlots - 1;
            }
            weaponController.m_currentAmmo = playerInventory.savedWeapon[currentWeaponSlot].ammoCount;
        }

    }

    public void OnDropItem()
    {
        // Simple destroys the current ScriptableWeapon enabled in the inventory 
        if (playerInventory.savedWeapon[currentWeaponSlot].scriptableWeapon != null && weaponController.canSwapWeapon == true)
        {
            GameObject droppedWeapon = Instantiate(blankItemPrefab, transform.position, Quaternion.identity);
            droppedWeapon.GetComponent<ItemPickup>().scriptableWeapon = weaponController.m_scriptableWeapon;
            droppedWeapon.GetComponent<ItemPickup>().heldAmmo = weaponController.m_currentAmmo;

            playerInventory.savedWeapon[currentWeaponSlot].scriptableWeapon = null;
        }
    }


    // DEBUG TOOLS
    void AddAmmoCheat()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerInventory.ammoLightCount += 200;
            playerInventory.ammoMagnumCount += 30;
            playerInventory.ammoHeavyCount += 200;
            playerInventory.ammoAssaultCount += 150;
            playerInventory.ammoSniperCount += 20;
            playerInventory.ammoShellCount += 30;
            playerInventory.ammoExplosiveCount += 5;
            playerInventory.ammoEnergyCount += 1000;
        }

    }
}
