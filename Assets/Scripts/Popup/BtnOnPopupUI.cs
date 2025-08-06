using Rubik.math;
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
            SoundController.instance.AudioButton();
            PopupManager.Instance.OnUI(PopupCode);
            Debug.Log("PopUpcode" + PopupCode);
        }
    }
}