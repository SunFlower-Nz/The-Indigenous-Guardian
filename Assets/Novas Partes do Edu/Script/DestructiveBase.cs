using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructiveBase : MonoBehaviour
{
    protected bool isDead;
    public float currentLife;
    public float totalLife;

    public float attackRate;
    protected float currentAttackRate;

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public void ApplyDamage(int damage)
    {
        if (isDead) return;

        currentLife -= damage;

        if(currentLife <= 0)
        {
            isDead = true;
            OnDestroyed();
        }

        OnApplyDamage();
    }

    public void Addlife(int life)
    {
        currentLife += life;

        if(currentLife > totalLife)
        {
            currentLife = totalLife;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public abstract void OnDestroyed();
    public abstract void OnApplyDamage();

}
