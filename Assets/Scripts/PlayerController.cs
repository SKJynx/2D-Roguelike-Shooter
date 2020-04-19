using System.Collections;
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

    public float dodgeTimer;
    public float max_dodgeTimer;

    public bool facingRight = true;
    public float isHolding;

    public float pickupIsActive;
    [SerializeField]
    float ability1IsActive;
    [SerializeField]
    float slowMotionAmount;

    [SerializeField]
    GameObject reticle;

    [SerializeField]
    GameObject heldItem;
    [SerializeField]
    GameObject playerSprite;

    Vector2 movement;

    Rigidbody2D rb2d;

    Animator playerAnimator;

    Animator anim;

    PlayerInventory playerInventory;

    WeaponController weaponController;

    float fixedDeltaTime;

    int currentWeaponSlot;

    int maxWeaponSlots;

    private void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponSlot = 0;
        maxWeaponSlots = 4;
        weaponController = gameObject.GetComponentInChildren<WeaponController>();
        playerInventory = this.GetComponent<PlayerInventory>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponentInChildren<Animator>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        dodgeTimer -= 1;

        if (canInput == true)
        {
            rb2d.velocity = movement * maxSpeed;
        }
    }

    void Update()
    {
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


    //Experimental
    void SlowMotionAbility()
    {

        if (ability1IsActive == 1)
        {
            Time.timeScale = slowMotionAmount;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void OnSwitchWeaponNext()
    {
        if (currentWeaponSlot < playerInventory.scriptabeWeaponSlot.Count - 1)
        {
            if (currentWeaponSlot != maxWeaponSlots)
            {
                currentWeaponSlot++;
            }
            else
            {
                currentWeaponSlot = 0;
            }
            weaponController.m_scriptableWeapon = playerInventory.scriptabeWeaponSlot[currentWeaponSlot];
            weaponController.GetScriptableValues();
            weaponController.CheckCurrentWeapon();
        }
    }

    public void OnSwitchWeaponPrevious()
    {
        if (currentWeaponSlot > 0)
        {
            if (currentWeaponSlot != 0)
            {
                currentWeaponSlot--;
            }
            else
            {
                currentWeaponSlot = playerInventory.scriptabeWeaponSlot.Count;
            }
            weaponController.m_scriptableWeapon = playerInventory.scriptabeWeaponSlot[currentWeaponSlot];
            weaponController.GetScriptableValues();
            weaponController.CheckCurrentWeapon();

        }
    }


    // DEBUG TOOLS

    void AddAmmoCheat()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerInventory.ammoLightCount += 300;
            playerInventory.ammoMagnumCount += 60;
            playerInventory.ammoHeavyCount += 800;
            playerInventory.ammoAssaultCount += 500;
            playerInventory.ammoSniperCount += 80;
            playerInventory.ammoShellCount += 140;
            playerInventory.ammoExplosiveCount += 5;
            playerInventory.ammoEnergyCount += 1000;
        }

    }
}
