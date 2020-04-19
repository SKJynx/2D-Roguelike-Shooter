using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class WeaponSFX : MonoBehaviour
{

    WeaponController weaponController;

    // Enable referencing of sound effects for EventRef
    public MyStruct[] soundEffects;
    [System.Serializable]
    public struct MyStruct
    {
        [FMODUnity.EventRef]
        public string soundPath;
        public string soundEffectName;
        public int soundIndex;
    }

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
    }

    public void FootstepSFX()
    {

    }

    public void FireWeaponSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(weaponController.m_fireSFX);
    }

}
