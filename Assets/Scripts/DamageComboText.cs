using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageComboText : MonoBehaviour
{
    float currentDamageStat;

    [SerializeField]
    float comboHighScore;

    Text damageComboText;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        damageComboText = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        DisplayText();

        if (PlayerStatsManager.playerComboDamage > comboHighScore)
        {
            comboHighScore = PlayerStatsManager.playerComboDamage;
        }

        if (currentDamageStat != PlayerStatsManager.playerDamageDealt)
        {
            PlayDamageAnimation();
            currentDamageStat = PlayerStatsManager.playerDamageDealt;
        }
    }

    void DisplayText()
    {
        damageComboText.text = $"DAMAGE COMBO \n {PlayerStatsManager.playerComboDamage}";
    }

    void PlayDamageAnimation()
    {
        anim.Play("DamageText_Anim", -1, 0);
    }

    public void LostComboChain()
    {
        PlayerStatsManager.playerComboDamage = 0;
    }
}
