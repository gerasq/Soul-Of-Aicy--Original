using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuelatorGames
{
    public class DropHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.GetComponentInChildren<InventoryIcon>() != null)
            {
                var oldDroppedParent = eventData.pointerDrag.GetComponent<DragDrop>().oldParent;
                var oldInvIcon = transform.GetComponentInChildren<InventoryIcon>().transform;
                transform.GetComponentInChildren<InventoryIcon>().transform.SetParent(oldDroppedParent);
                oldInvIcon.localPosition = new Vector3(0, 0, 0);
                oldInvIcon.localScale = new Vector3(1, 1, 1);

            }
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = Vector3.one;

        }

    }
}
