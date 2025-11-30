using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DuelatorGames
{
    public class FreeHandSlot : MonoBehaviour
    {
        private string inputText;
        [SerializeField]
        private KeyCode inputKey;
        [SerializeField]
        private Text selectText;
        private Inventory playerInventory;

        public bool isSelected = true;
        private void Awake()
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        // Update is called once per frame
        void Update()
        {
            inputText = inputKey.ToString();
            selectText.text = inputText;

            if (Input.GetKeyDown(inputKey))
            {
                isSelected = true;
            }
            for (int i = 0; i < playerInventory.slots.Length; i++)
            {
                if (isSelected)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.15f);
                    if (playerInventory.slots[i].transform.GetComponentInChildren<InventoryIcon>() != null)
                    {
                        playerInventory.slots[i].transform.GetComponentInChildren<InventoryIcon>().isSelected = false;
                    }
                }
            }
            if (!isSelected)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 0.7f, 0.7f), 0.15f);
            }

        }
    }
}
