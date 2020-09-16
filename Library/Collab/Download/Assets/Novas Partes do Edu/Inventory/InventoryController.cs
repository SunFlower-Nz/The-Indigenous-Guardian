using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public List<SlotInventoryBehaviour> inventorySlots;
    public static InventoryController instanced;
    public int maxInventorySlots;
    public SlotInventoryBehaviour slotPrefab;
    public Transform itemsGrid;

    public SlotInventoryBehaviour selectedSlot;
    public GameObject optionOnSelect;
    public GameObject buttonUse;
    public GameObject buttonEquip;
    internal static object instance;

    public GameObject mao;

    // Start is called before the first frame update
    void Start()
    {
        instanced = this;

        for (int i = 0; i < maxInventorySlots; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(itemsGrid, false);
            inventorySlots.Add(tempSlot.GetComponent<SlotInventoryBehaviour>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedSlot != null && selectedSlot.currentItem != null)
        {
            optionOnSelect.SetActive(true);

            if (selectedSlot.currentItem.canEquip)
            {
                buttonUse.SetActive(false);
            }
            else
            {
                buttonEquip.SetActive(false);
            }
        }
        else
        {
            optionOnSelect.SetActive(false);
        }
    }

    public void AddItemToInventory(ItemBase item)
    {
        bool foundItem = false;
        SlotInventoryBehaviour emptySlot = NextEmptySlot();
        if (item.isTacklabe)
        {
            foreach (SlotInventoryBehaviour slot in inventorySlots)
            {
                if (slot.currentItem != null && slot.currentItem.nameItem == item.nameItem)
                {
                    slot.currentItem.AddItem();
                    foundItem = true;
                }
            }

            if (!foundItem && emptySlot != null)
            {
                emptySlot.currentItem = item;
            }
        }
        else if (emptySlot != null)
        {
            emptySlot.currentItem = item;
        }
        // inventorySlots.Add(item);
        item.gameObject.SetActive(false);
    }

    private SlotInventoryBehaviour NextEmptySlot()
    {
        SlotInventoryBehaviour slotToReturn = null;
        foreach (SlotInventoryBehaviour slot in inventorySlots)
        {
            if (slot.currentItem == null)
            {
                slotToReturn = slot;
                break;
            }
        }
        return slotToReturn;
    }

    private void OnEnable()
    {
        selectedSlot = null;
        buttonEquip.SetActive(true);
        buttonUse.SetActive(true);
    }

    public void UseItem()
    {
        selectedSlot.currentItem.Use();
        selectedSlot.SetupSlot();
    }

    public void RemoveItem()
    {

    }

    public void EquipItem()
    {
        
    }
}
