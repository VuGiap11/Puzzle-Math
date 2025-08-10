using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace NailSalonGame
{
    public class ButtonController : MonoBehaviour, IPointerClickHandler
    {
        public bool on;
        public GameObject onSprite;
        public GameObject offSprite;
        private void Awake()
        {
            on = true;
            onSprite.SetActive(true);
            offSprite.SetActive(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(on)
            {
                onSprite.SetActive(false);
                offSprite.SetActive(true);
            }
            else
            {
                onSprite.SetActive(true);
                offSprite.SetActive(false);
            }
            on = !on;
        }
    }
}

