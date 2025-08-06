
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


namespace Rubik.math
{
    public class RewardDay : MonoBehaviour
    {
        public Button BtnReward;
        public GameObject icoinOn, icoinOff, icoinDone, icoinCanOpen;
        public RewardData rewardData;
        [SerializeField] TextMeshProUGUI dayText, numberText;
        //[SerializeField] Image avar;
        //[SerializeField] Sprite imageGold;
        public void Init(RewardData rewardData)
        {
            this.rewardData = rewardData;
            RewardManager.Instance.Notification.Add("RewardDay" + rewardData.day, () => this.UpdateData());
            UpdateData();
        }

        public void UpdateData()
        {
            //if (LanguageManager.Instance.languageType == LanguageType.England)
            //{
            //    this.dayText.text = "Day" + (this.rewardData.day).ToString();
            //}
            //else
            //{
            //    this.dayText.text = "Ngày" + (this.rewardData.day).ToString();
            //}
            this.numberText.text = this.rewardData.gold.ToString();
            this.dayText.text = "day" + this.rewardData.day.ToString();
            if (RewardManager.Instance.IsClaimed(rewardData))
            {
                DoneReceive();
                return;
            }
            if (RewardManager.Instance.AvailableClaim(rewardData.day))
            {
                RewardCanOpen();
            }
            else
            {
                OrigiReward();
            }
        }
        [ContextMenu("ReveiveReward")]
        public void ReveiveReward()
        {
            UserManager.instance.useData.gold += this.rewardData.gold;
            RewardManager.Instance.Claim();
            this.UpdateData();
            HomeManager.instance.Init();
            //UserManager.instance.SaveData();
            return;
        }
        public void DoneReceive()
        {
           
            this.BtnReward.GetComponent<Button>().enabled = false;
            this.icoinDone.SetActive(true);
            this.icoinOff.SetActive(true);
            this.icoinOn.SetActive(false);
            this.icoinCanOpen.SetActive(false);
        }
        public void OrigiReward()
        {
            this.BtnReward.GetComponent<Button>().enabled = false;
            this.icoinDone.SetActive(false);
            this.icoinOff.SetActive(false);
            this.icoinOn.SetActive(true);
            this.icoinCanOpen.SetActive(false);

        }
        public void RewardCanOpen()
        {
            this.BtnReward.GetComponent<Button>().enabled = true;
            this.icoinDone.SetActive(false);
            this.icoinOff.SetActive(false);
            this.icoinOn.SetActive(false);
            this.icoinCanOpen.SetActive(true);
        }
    }
}