using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelatorGames
{
    public class InventoryIcon : MonoBehaviour
    {
        private Transform player;
        public bool isSelected;
        public GameObject correspondingLootIcon;
        public GameObject correspondingItemObject;
        private Inventory playerInventory;
        private Animator playerAnimator;
        [Header("DO NOT EDIT")]
        public GameObject instantiatedItem;
        private bool instantiated;

        [SerializeField]
        private Vector3 itemLocation;
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isSelected)
            {
                SelectItem();
                if (transform.parent.CompareTag("Inventory Slot"))
                {
                    transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, Vector3.one, 0.15f);
                }
            }
            else if (!isSelected)
            {
                DeselectItem();
                if (transform.parent.CompareTag("Inventory Slot"))
                {
                    transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, new Vector3(0.7f, 0.7f, 0.7f), 0.15f);
                }
            }
        }
        private void SelectItem()
        {
            if (!instantiated)
            {
                instantiatedItem = Instantiate(correspondingItemObject, player);
                instantiatedItem.transform.localPosition = itemLocation;
                instantiated = true;
            }
        }
        private void DeselectItem()
        {
            if (instantiatedItem != null)
            {
                Destroy(instantiatedItem);
                instantiated = false;
            }
        }
    }
}
