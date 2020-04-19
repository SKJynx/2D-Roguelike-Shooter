using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{

    public List<ScriptableWeapons> scriptabeWeaponSlot;

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
