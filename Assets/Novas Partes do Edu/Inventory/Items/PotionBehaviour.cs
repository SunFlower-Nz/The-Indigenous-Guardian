using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehaviour : ItemBase
{
    public int amountLiquid;
    private PlayerBehaviour player;

    private void Start()
    {
        player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
    }

    public override void AfterUse()
    {
        ApplyPotion();
        RemoveItem();
    }
    
    public void ApplyPotion()
    {
        player.Addlife(amountLiquid);
    }

}
