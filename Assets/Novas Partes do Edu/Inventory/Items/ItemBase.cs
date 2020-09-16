using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : MonoBehaviour
{
    public string nameItem;
    public Sprite icon;
    public bool isTacklabe;
    protected int amount = 1;
    private bool canTakeItem;
    public bool canEquip;

    public void AddItem(int amountToAdd = 1)
    {
        amount += amountToAdd;
    }

    public void RemoveItem(int amountToRemove = 1)
    {
        amount -= amountToRemove;

        if(amount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetAmount() 
    {
        return amount;    
    }

    public void Use()
    {
        AfterUse();
    }

    public virtual void AfterUse()
    {
        gameObject.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canTakeItem)
        {
            InventoryController.instanced.AddItemToInventory(this);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject. CompareTag("Player"))
        {
            canTakeItem = true;
            UIController.instance.ShowMessageTakeItem();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTakeItem = false;
            UIController.instance.ShowMessageTakeItem();
        }
    }



}
