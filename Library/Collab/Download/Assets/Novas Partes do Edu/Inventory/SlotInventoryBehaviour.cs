using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SlotInventoryBehaviour : MonoBehaviour
{
    public ItemBase currentItem;
    public Image iconItemSlot;
    public Text nameItem;
    public GameObject AmountIndicator;
    public Text amountText;
    private bool isSelected;
    public Image backGround;
    public Color unselectedColor;
    public Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        SetupSlot();
    }

    public void SetupSlot()
    {
        if (currentItem != null && currentItem.GetAmount() > 0)
        {
            SetActiveSlot(true);

            iconItemSlot.sprite = currentItem.icon;
            nameItem.text = currentItem.nameItem;

            if (currentItem.isTacklabe)
            {
                amountText.text = currentItem.GetAmount().ToString();
            }
            else
            {
                AmountIndicator.SetActive(false);
            }
        }
        else
        {
            SetActiveSlot(false);
        }
    }

    public void SetActiveSlot(bool isactive = true)
    {
        AmountIndicator.SetActive(isactive);
        nameItem.gameObject.SetActive(isactive);
        iconItemSlot.gameObject.SetActive(isactive);
    }

    // Update is called once per frame
    void Update()
    {
        isSelected = InventoryController.instanced.selectedSlot == this;

        backGround.color = isSelected ? selectedColor : unselectedColor;
    }

    public void OnClick()
    {
        if (isSelected)
        {
            InventoryController.instanced.selectedSlot = null;
        }
        else
        {
            InventoryController.instanced.selectedSlot = this;
        }
    }

    private void OnEnable()
    {
        SetupSlot();
    }
}
