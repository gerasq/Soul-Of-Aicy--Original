using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuelatorGames
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private bool oldBool;
        private InventoryIcon thisInvIcon;

        private GameObject removeButton;
        [Header("DO NOT EDIT")]
        public Transform oldParent;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            thisInvIcon = GetComponent<InventoryIcon>();
            removeButton = FindObjectOfType<RemoveButton>(true).gameObject;
            //removeButton.SetActive(false);
        }
        private void Start()
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 0.8f;
            canvasGroup.blocksRaycasts = false;
            if (thisInvIcon.isSelected)
            {
                oldBool = true;
            }
            else
            {
                oldBool = false;
            }
            thisInvIcon.isSelected = false;
            if (transform.GetComponentInParent<InventorySlot>() != null)
            {
                oldParent = transform.parent;
            }
            rectTransform.SetParent(transform.parent.parent);
            oldParent.transform.localScale = Vector3.Lerp(oldParent.transform.localScale, new Vector3(transform.localScale.x + 0.25f, transform.localScale.y + 0.25f, 1), 0.15f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            oldParent.transform.localScale = Vector3.Lerp(oldParent.transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), 0.15f);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            thisInvIcon.isSelected = oldBool;
            if (!eventData.pointerEnter.CompareTag("Inventory Slot") && eventData.pointerEnter.GetComponent<InventoryIcon>() == null) //to make sure that no change happens if dropped place is not inventory slot and is not Inventory Icon
            {
                transform.SetParent(oldParent);
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            removeButton.SetActive(true);
            removeButton.GetComponent<RemoveButton>().currentItem = GetComponent<InventoryIcon>();
        }
    }
}
