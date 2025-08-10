using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.UI
{
    public class BtnOnPopupUI : MonoBehaviour
    {
        public PopupCode PopupCode;
        public void _OneClick()
        {
            PopupManager.Instance.OnUI(PopupCode);
            
        }
    }
}