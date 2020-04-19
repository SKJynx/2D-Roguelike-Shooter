using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private static Transform _playerTransform;

    // Declare any public variables that you want to be able 
    // to access throughout your scene
    public WeaponController weaponController;
    public PlayerInventory playerInventory;

    public static GameManager Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // Cache references to all desired variables
        weaponController = FindObjectOfType<WeaponController>();

        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // Cache references to all desired variables
        playerInventory = FindObjectOfType<PlayerInventory>();
    }


    public static Transform PlayerTransform
    {
        get
        {
            if (_playerTransform == null)
            {
                _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

            }
            return _playerTransform;
        }
    }
}
