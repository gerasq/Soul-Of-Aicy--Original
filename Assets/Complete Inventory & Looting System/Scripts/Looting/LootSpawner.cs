using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelatorGames
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] pickupKeyTexts;
        [SerializeField]
        private GameObject[] lootSpawners;
        [SerializeField]
        private Camera playerCam;
        [SerializeField]
        private LayerMask hitMask;
        [SerializeField]
        private KeyCode interactInput;

        private bool canInteract;
        private Inventory playerInventory;
        private bool allBoolTrue = false;
        private void Awake()
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        private void OnTriggerEnter(Collider other)
        {
            canInteract = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                for (int i = 0; i < pickupKeyTexts.Length; i++)
                {
                    pickupKeyTexts[i].SetActive(false);
                }
                for (int i = 0; i < lootSpawners.Length; i++)
                {
                    lootSpawners[i].transform.localPosition = new Vector3(lootSpawners[i].transform.localPosition.x, 0, lootSpawners[i].transform.localPosition.z);
                }
            }

            canInteract = false;
        }

        private void Update()
        {
            if (canInteract)
            {
                HandleTextVisibility();
                HandleLootCollection();
            }
        }
        private void HandleTextVisibility()
        {
            RaycastHit hit;
            for (int i = 0; i < lootSpawners.Length; i++)
            {
                if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10f, hitMask, QueryTriggerInteraction.Collide))
                {
                    if (hit.transform.GetComponentInChildren<SpawnLoot>().gameObject == lootSpawners[i]
                    && lootSpawners[i].transform.GetComponentInChildren<LootIcon>() != null)
                    {
                        pickupKeyTexts[i].SetActive(true);
                        lootSpawners[i].transform.localPosition = new Vector3(lootSpawners[i].transform.localPosition.x, Mathf.Lerp(lootSpawners[i].transform.localPosition.y, -0.375f, 0.3f), lootSpawners[i].transform.localPosition.z);
                    }
                    else
                    {
                        pickupKeyTexts[i].SetActive(false);

                        lootSpawners[i].transform.localPosition = new Vector3(lootSpawners[i].transform.localPosition.x, Mathf.Lerp(lootSpawners[i].transform.localPosition.y, 0, 0.3f), lootSpawners[i].transform.localPosition.z);
                    }
                }
                else
                {
                    pickupKeyTexts[i].SetActive(false);
                    lootSpawners[i].transform.localPosition = new Vector3(lootSpawners[i].transform.localPosition.x, Mathf.Lerp(lootSpawners[i].transform.localPosition.y, 0, 0.3f), lootSpawners[i].transform.localPosition.z);
                }
            }
        }

        private void HandleLootCollection()
        {
            foreach (bool b in playerInventory.isFull)
            {
                if (b)
                {
                    allBoolTrue = true;
                }
                else
                {
                    allBoolTrue = false;
                    break;
                }
            }
            for (int i = 0; i < pickupKeyTexts.Length; i++)
            {
                if (pickupKeyTexts[i].activeSelf == true)
                {
                    if (Input.GetKeyDown(interactInput))
                    {
                        for (int j = 0; j < playerInventory.slots.Length; j++)
                        {
                            if (!playerInventory.isFull[j])
                            {
                                Instantiate(lootSpawners[i].transform.GetChild(0).gameObject.GetComponent<LootIcon>().inventoryIcon, playerInventory.slots[j].transform);
                                playerInventory.isFull[j] = true;
                                Destroy(lootSpawners[i].transform.GetComponentInChildren<LootIcon>().gameObject);
                                break;

                            }
                            else if (allBoolTrue && playerInventory.slots[j].GetComponentInChildren<InventoryIcon>().isSelected)
                            {
                                var isntInvIcon = Instantiate(lootSpawners[i].transform.GetComponentInChildren<LootIcon>().inventoryIcon, playerInventory.slots[j].transform);
                                Destroy(lootSpawners[i].transform.GetComponentInChildren<LootIcon>().gameObject);
                                Instantiate(playerInventory.slots[j].GetComponentInChildren<InventoryIcon>().correspondingLootIcon, lootSpawners[i].transform);
                                Destroy(playerInventory.slots[j].GetComponentInChildren<InventoryIcon>().gameObject);
                                isntInvIcon.GetComponent<InventoryIcon>().isSelected = true;
                                break;
                            }

                        }

                    }
                }
            }
        }
    }
}
