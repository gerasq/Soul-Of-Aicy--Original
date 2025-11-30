using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuelatorGames
{
    public class InventorySelectKey : MonoBehaviour
    {
        [SerializeField]
        private int keyNumber;
        [SerializeField]
        private GameObject correspondingSlot;

        private Text text;
        private string inputText;
        private Inventory playerInventory;
        private bool slotIsFull;

        [SerializeField]
        private KeyCode inputKey;

        private void Awake()
        {
            text = this.GetComponent<Text>();
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        private void Update()
        {
            SetTextAndInputValue();
            SelectItem();
        }
        private void SetTextAndInputValue()
        {
            string inputKeyString = inputKey.ToString();
            char lastLetter = inputKeyString[inputKeyString.Length - 1];
            text.text = lastLetter.ToString();
        }
        private void SelectItem()
        {
            int slotInt = 0;
            for (int i = 0; i < playerInventory.slots.Length; i++)
            {
                if (playerInventory.slots[i] == correspondingSlot)
                {
                    slotInt = i;
                }
            }
            slotIsFull = playerInventory.isFull[slotInt];

            if (slotIsFull)
            {
                if (Input.GetKeyDown(inputKey))
                {
                    for (int i = 0; i < playerInventory.slots.Length; i++)
                    {
                        if (playerInventory.slots[i] == correspondingSlot)
                        {
                            playerInventory.slots[i].transform.GetComponentInChildren<InventoryIcon>().isSelected = true;
                            FindObjectOfType<FreeHandSlot>().GetComponent<FreeHandSlot>().isSelected = false;
                        }
                        else if (playerInventory.slots[i] != correspondingSlot && playerInventory.slots[i].transform.GetComponentInChildren<InventoryIcon>() != null)
                        {
                            playerInventory.slots[i].transform.GetComponentInChildren<InventoryIcon>().isSelected = false;
                        }
                    }
                }
            }

        }
    }
}
