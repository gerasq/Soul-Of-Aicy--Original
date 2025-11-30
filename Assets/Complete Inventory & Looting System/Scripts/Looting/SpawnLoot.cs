using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelatorGames
{
    public class SpawnLoot : MonoBehaviour
    {
        [SerializeField]
        private LootItem[] LootPool;

        private void Start()
        {
            HandleRandomSpawning();
        }
        private void HandleRandomSpawning()
        {

            float randomNumber = Random.Range(1, 100);

            foreach (var lootItem in LootPool)
            {
                if (randomNumber >= lootItem.minChancePercentage && randomNumber <= lootItem.maxChancePercentage)
                {
                    var newLootItem = Instantiate(lootItem.Item, transform);
                    newLootItem.transform.SetSiblingIndex(0);
                }
            }

        }

        [System.Serializable]
        public class LootItem
        {
            public GameObject Item;
            public float minChancePercentage;
            public float maxChancePercentage;
        }
    }
}

