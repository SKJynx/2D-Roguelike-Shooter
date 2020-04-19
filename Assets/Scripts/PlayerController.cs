using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    bool canInput;

    public bool facingRight = true;
    public float isHolding;

    public float pickupIsActive;

    [SerializeField]
    GameObject reticle;

    [SerializeField]
    GameObject heldItem;
    [SerializeField]
    GameObject playerSprite;

    Vector2 movement;

    Rigidbody2D rb2d;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = playerSprite.GetComponentInChildren<Animator>();
 
    }

    // Update is called once per frame
    void Update()
    {
        FixWeaponOrientation();
        LookAtReticle();


        if (canInput == true)
        {
            rb2d.velocity = movement * maxSpeed;


        }

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
}
