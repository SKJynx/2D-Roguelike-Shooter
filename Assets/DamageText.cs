using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    Text text;

    HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        healthManager = GetComponentInParent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{(int)healthManager.health}";
    }
}
