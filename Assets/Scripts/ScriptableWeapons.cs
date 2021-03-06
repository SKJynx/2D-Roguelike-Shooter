﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class ScriptableWeapons : ScriptableObject
{

    public Sprite weaponSprite;
    public GameObject bulletType;

    [FMODUnity.EventRef]
    public string weaponFireSound;
    [FMODUnity.EventRef]
    public string reloadSound;
    [FMODUnity.EventRef]
    public string endReloadSound;
    [FMODUnity.EventRef]
    public string weaponPickupSound;

    public string weaponName;

    public float weaponDamage;
    public float weaponCost;

    //Accuracy in degrees
    public float weaponAccuracy;

    //100 = 100% crit chance
    public float critChance;

    //In ticks
    public float reloadTime;
    public int maxAmmo;

    public int startingAmmo;

    public int projectileCount;

    //50 is *really* high
    public float bulletVelocity;

    //In ticks 60 ticks = 1 sec
    public float fireRate;
    public float criticalMultiplier;

    //Can the weapon fire full auto?
    public bool automatic;


    public int itemID;

    public enum WeaponTypes
    {
        Pistol,
        Magnum,
        SMG,
        Shotgun,
        Rifle,
        Sniper,
        Heavy,
        Energy,
        RocketLauncher,
        GrenadeLauncher,
        Melee
    }
    public WeaponTypes weaponType;

    public enum AmmoType
    {
        Light,
        Magnum,
        Assault,
        Heavy,
        Sniper,
        Explosive,
        Shell,
        Energy,
        Grenade,
        None
    }
    public AmmoType ammoType;

    public enum ReloadType
    {
        Magazine,
        Single,
        Charging
    }
    public ReloadType reloadType;

}

