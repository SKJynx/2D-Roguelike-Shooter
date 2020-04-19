using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerSFXList : MonoBehaviour
{

    // Enable referencing of sound effects for EventRef
    public MyStruct[] structArray;
    [System.Serializable]
    public struct MyStruct
    {
        public string title;
        public int number;
    }


    [FMODUnity.EventRef]
    public string[] soundEffect;

    public void FootstepSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundEffect[0]);
    }

}
