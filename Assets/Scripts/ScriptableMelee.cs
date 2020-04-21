using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Melee", order = 1)]
public class ScriptableMelee : ScriptableObject
{
    public Sprite weaponSprite;

    [FMODUnity.EventRef]
    public string swingSFX;
    [FMODUnity.EventRef]
    public string deflectSFX;
    [FMODUnity.EventRef]
    public string impactSFX;
    [FMODUnity.EventRef]
    public string weaponPickupSound;

    public string weaponName;

    public float weaponDamage;
    public float weaponCost;

    //100 = 100% crit chance
    public float critChance;

    //50 is *really* high
    public float deflectionVelocity;

    //In ticks 60 ticks = 1 sec
    public float swingCooldown;

    public int itemID;

    public enum WeaponTypes
    {
        Knife,
        Sword,
        Hammer,
        Staff,
        Axe
    }
    public WeaponTypes weaponType;
}

