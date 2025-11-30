using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace DuelatorGames
{
    public class InventoryUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject expandedInventory;
        [SerializeField]
        private GameObject removeButton;
        [SerializeField]
        private float openCloseTime;
        [SerializeField]
        private GameObject alertConfirmation;
        [SerializeField]
        private KeyCode expandInvInput;

        private bool isExpanded;
        // Update is called once per frame

        private void Awake()
        {
            expandedInventory.transform.SetAsFirstSibling();
        }
        void FixedUpdate()
        {
            if (Input.GetKey(expandInvInput) && !isExpanded)
            {
                ExpandInventory();
            }
            if (Input.GetKey(expandInvInput) && isExpanded)
            {
                ShrinkInventory();
            }
        }
        public void ExpandInventory()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            expandedInventory.SetActive(true);
            transform.localPosition = new Vector3(0, -440, 0);
            StartCoroutine(SetIsExpanded(true));
        }
        public void ShrinkInventory()
        {
            Cursor.lockState = CursorLockMode.Locked;
            expandedInventory.SetActive(false);
            removeButton.SetActive(false);
            alertConfirmation.SetActive(false);
            transform.localPosition = new Vector3(585, -440, 0);
            StartCoroutine(SetIsExpanded(false));
        }
        private IEnumerator SetIsExpanded(bool value)
        {
            yield return new WaitForSeconds(openCloseTime);
            isExpanded = value;
        }
    }
}
