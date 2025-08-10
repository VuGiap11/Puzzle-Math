using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ClawMachine
{
    public class Rank : MonoBehaviour
    {
        public GameObject MedalObj;
        public GameObject SttObj;
        public TextMeshProUGUI sttText;
        public TextMeshProUGUI namePlayer;
        public TextMeshProUGUI pointPlayer;
        public RankData RankData;
        public Image avatar;

        private void SetRank(int index)
        {
            if (index <= 3)
            {
                MedalObj.SetActive(true);
                SttObj.SetActive(false);
                if (index == 1)
                {
                    MedalObj.GetComponent<Image>().sprite = DataAssets.Instance.imageMedal[0];
                }
                else if (index == 2)
                {
                    MedalObj.GetComponent<Image>().sprite = DataAssets.Instance.imageMedal[1];
                }
                else if (index == 3)
                {
                    MedalObj.GetComponent<Image>().sprite = DataAssets.Instance.imageMedal[2];
                }
            }
            else
            {
                MedalObj.SetActive(false);
                SttObj.SetActive(true);
            }
        }

        public void InitRank(RankData rankData, int index)
        {
            this.RankData = rankData;
            namePlayer.text = this.RankData.Name.ToString();
            pointPlayer.text = this.RankData.NumberBaby.ToString();
            SetRank(index);
            sttText.text = (index).ToString();
            this.avatar.sprite = DataAssets.Instance.imageAvar[rankData.IdAvar];
        }
    }
}