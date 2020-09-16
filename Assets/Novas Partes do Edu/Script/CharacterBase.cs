using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicStats
{
    public float startLife;
    public float startMana;
    public int strenght;
    public int magic;
    public int agillity;
    public int baseDefense;
    public int baseAttack;
    public float baseRange;
    public float baseStamina;
}

public abstract class CharacterBase : DestructiveBase
{
    //Basic Atrritutes
    public int currentLevel;
    public BasicStats basicStats;

    

    
    
    // Start is called before the first frame update
    protected void Start()
    {
        currentLife = basicStats.startLife;
        totalLife = currentLife;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
    public override void OnDestroyed()
    {
        //
    }

    public override void OnApplyDamage()
    {

    }
}
