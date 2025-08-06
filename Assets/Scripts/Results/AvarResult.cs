using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Rubik.math
{
    public class AvarResult : MonoBehaviour
    {
        public Image avar;
        public TextMeshProUGUI numberText;

        public void Init(string id, int number)
        {
            BabyData babyData= DataAssets.instance.GetBabyDatabyID(id);
            this.avar.sprite = babyData.Avatar;
            this.numberText.text = number.ToString();
        }
    }
}