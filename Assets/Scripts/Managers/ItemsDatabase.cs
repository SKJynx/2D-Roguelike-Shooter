using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemsDatabase : MonoBehaviour
{
    public List<ScriptableWeapons> ItemProperties;

    private void Start()
    {
        ItemProperties = new List<ScriptableWeapons>();
        RegisterItems();
    }

    private void RegisterItems()
    {
        ScriptableWeapons preset = ScriptableObject.CreateInstance<ScriptableWeapons>();
        preset.weaponName = "A8 Sidearm";
        preset = Resources.Load<ScriptableWeapons>("A8 Sidearm");

        ItemProperties.Add(preset);
    }
}
