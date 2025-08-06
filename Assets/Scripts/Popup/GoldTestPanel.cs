using NTPackage.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.math
{
    public class GoldTestPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI timeText;
        public List<GameObject> lsHearts = new List<GameObject>();

        public void Init()
        {
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            InitHeart();
        }
        public void InitHeart()
        {
            for (int i = 0; i < this.lsHearts.Count; i++)
            {
                Debug.Log(this.lsHearts[i] + ":" + i, this.lsHearts[i]);
                //this.lsHearts[i].SetActive(i < GameManager.instance.numberheart);
            }

        }

    }
}