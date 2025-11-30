using UnityEngine;

namespace DuelatorGames
{
    public class RemoveButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject alertConfirmation;

        private InventoryUIManager invUIManager;
        private FreeHandSlot freeHandSlot;

        [Header("DO NOT EDIT")]
        public InventoryIcon currentItem;

        private void Awake()
        {
            invUIManager = FindObjectOfType<InventoryUIManager>();
            freeHandSlot = FindObjectOfType<FreeHandSlot>();
        }
        public void RemoveItem()
        {
            if (currentItem != null)
            {
                currentItem.isSelected = false;
                Destroy(currentItem.gameObject, 0.00001f); //delay to make sure that the object player is holding gets removed by isSelected = false, then only destroy
                freeHandSlot.isSelected = true;
                alertConfirmation.SetActive(false);
                invUIManager.ShrinkInventory();
            }
        }
        public void ShowAlert()
        {
            alertConfirmation.SetActive(true);
        }
        public void HideAlert()
        {
            alertConfirmation.SetActive(false);
        }
    }
}
