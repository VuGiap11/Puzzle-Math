using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ScaleUI : MonoBehaviour
    {
        private float referenceWidth = 1080f;
        private float referenceHeight = 1920f;
        void Start()
        {
            ScaleToScreenSize();
        }

        void Update()
        {
            ScaleToScreenSize();
        }

        void ScaleToScreenSize()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float scaleX = screenWidth / referenceWidth;
            float scaleY = screenHeight / referenceHeight;
            float scale = Mathf.Min(scaleX, scaleY);

            RectTransform rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localScale = new Vector3(scale, scale, 1);
            }
        }
    }
}
