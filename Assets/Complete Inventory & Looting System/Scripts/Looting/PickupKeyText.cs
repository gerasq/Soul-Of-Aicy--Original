using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuelatorGames
{
    public class PickupKeyText : MonoBehaviour
    {
        private TextMesh text;
        [SerializeField]
        private KeyCode inputKey;
        private string inputText;

        private void Awake()
        {
            text = this.GetComponent<TextMesh>();
            this.gameObject.SetActive(false);
        }

        private void Update()
        {
            SetTextValue();
        }

        private void SetTextValue()
        {

            inputText = inputKey.ToString();

            text.text = inputText;
        }
    }
}
