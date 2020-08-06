using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName = "Default";


    [TextArea]
    public string description = "";

    [Tooltip("Chance that card will be added to the pool")]
    public int value = 1;
//------------------------------------------------------------------------------------------
    [Header("Damage")]

    [Tooltip("Check if this card does damage")]
    public bool doDamage = false;

    [Tooltip("How attack targets")]
    public DamageType targetType;

    [Tooltip("Number of targets")]
    public int numberOfTargets = 0;

    public int damageAmount = 0;
//----------------------------------------------------------------------------------------
    [Header("Defense")]

    [Tooltip("Check if this card does defense")]
    public bool doDefense = false;

    [Tooltip("Put defense on player")]
    public bool defendPlayer = false;

    [Tooltip("Put defense on player")]
    public bool defendFighter = false;

    public int negateAmount = 0;

    //------------------------------------------------------------------------------------------
    [Header("Spawn fighters")]

    [Tooltip("Check if target should spawn")]
    public bool doSpawn = false;

    public int spawnAmount = 0;

    [Tooltip("If target should fire when it spawns")]
    public bool fireWhenSpawned=false;


}
