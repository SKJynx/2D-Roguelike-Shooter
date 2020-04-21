using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "NPCS/Enemy")]
public class ScriptableEnemies : ScriptableObject
{
   
    public GameObject projectile;

    public string enemyName;
    public float attackFrequency;
    public float attackDamage;
    public float health;
    public float attackSpeed;

    public enum EnemyTypes
    {
        Melee,
        Ranged,
        Boss,
    }
    public EnemyTypes enemyType;
}
