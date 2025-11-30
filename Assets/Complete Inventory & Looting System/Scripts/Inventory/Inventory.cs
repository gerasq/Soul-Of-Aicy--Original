using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelatorGames
{
    public class Inventory : MonoBehaviour
    {
        public bool[] isFull = new bool[5];
        public GameObject[] slots;

        private void Update()
        {
            for (int i = 0; i < isFull.Length; i++)
            {
                if (slots[i].GetComponentInChildren<InventoryIcon>() != null)
                {
                    isFull[i] = true;
                }
                else
                {
                    isFull[i] = false;
                }
            }
        }
    }
}