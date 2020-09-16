using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Slider sliderLife;
    public Slider sliderStamina;
    public Text messageItemToTake;
    public InventoryController inventory;

    public static UIController instance;

    public float timeToShowMessage;
    private float currentTimeToShowMessage;

    public void SetLife(float maxLife, float currentLife)
    {
        float newPositionSlider = currentLife * 1 / maxLife;

        sliderLife.value = newPositionSlider;
    }

    public void SetStamina(float maxStamina, float currentStamina)
    {
        float newPositionSlider = currentStamina * 1 / maxStamina;

        sliderStamina.value = newPositionSlider;
    }

    public void ShowMessageTakeItem()
    {
        messageItemToTake.gameObject.SetActive(true);
        currentTimeToShowMessage = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory.gameObject.SetActive(false);
        messageItemToTake.gameObject.SetActive(false);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject inventoryGO = inventory.gameObject;

            inventoryGO.SetActive(!inventoryGO.activeSelf);

            if (inventoryGO.activeSelf)
            {

            }
        }

        if (messageItemToTake.gameObject.activeSelf)
        {
            currentTimeToShowMessage += Time.deltaTime;

            if(currentTimeToShowMessage > timeToShowMessage)
            {
                messageItemToTake.gameObject.SetActive(false);

            }
        }
    }
}
