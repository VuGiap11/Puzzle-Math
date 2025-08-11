
using NailSalonGame;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


namespace Rubik.ClawMachine
{
    public class RewardDay : MonoBehaviour
    {
        //public Button BtnReward;
        //public GameObject dailyClear;
        //public GameObject NextDailyReward;
        //public GameObject backGroundReward;
        //public double TimeDay;
        //[ContextMenu("ReveiveReward")]
        //public void ReveiveReward(int gold)
        //{
        //    SoundController.instance.AudioReward();
        //    UserManager.instance.useData.gold += gold;
        //    DoneReceive();
        //    UserManager.instance.useData.indexRewardDay++;
        //    if (UserManager.instance.useData.indexRewardDay >= 7)
        //    {
        //        UserManager.instance.useData.indexRewardDay = 0;
        //    }
        //    PlayerPrefs.SetString(Contans.LastRewartTime, System.DateTime.Now.ToString());
        //    UserManager.instance.SaveHeroData();
        //    //BtnReward.GetComponent<Button>().enabled = false;
        //    //RewardManager.instance.ResetReward();
        //    //HomeController.instance.Init();
        //    HomeController.instance.rewardPanel.CheckRewardAvailability();
        //}
        //public void DoneReceive()
        //{
        //    this.dailyClear.SetActive(true);
        //    this.BtnReward.interactable = false;
        //    this.BtnReward.GetComponent<Button>().enabled = false;
        //    this.backGroundReward.SetActive(false);
        //    this.NextDailyReward.SetActive(false);
        //}
        //public void NextDay()
        //{
        //    this.dailyClear.SetActive(false);
        //    this.BtnReward.interactable = true;
        //    this.BtnReward.GetComponent<Button>().enabled = false;
        //    this.backGroundReward.SetActive(false);
        //    this.NextDailyReward.SetActive(true);
        //}
        //public void OrigiReward()
        //{
        //    this.BtnReward.interactable = true;
        //    this.BtnReward.GetComponent<Button>().enabled = false;
        //    this.backGroundReward.SetActive(false);
        //    this.NextDailyReward.SetActive(false);
        //    this.dailyClear.SetActive(false);

        //}
        //public void RewardCanOpen()
        //{
        //    this.BtnReward.interactable = true;
        //    this.BtnReward.GetComponent<Button>().enabled = true;
        //    this.backGroundReward.SetActive(true);
        //    this.NextDailyReward.SetActive(false);
        //    this.dailyClear.SetActive(false);
        //}
        public Button BtnReward;
        public GameObject icoinOn, icoinOff, icoinDone, icoinCanOpen;
        public RewardData rewardData;
        [SerializeField] TextMeshProUGUI dayText, numberText;
        [SerializeField] Image avar;
        [SerializeField] Sprite imageGold;
        public void Init(RewardData rewardData)
        {
            this.rewardData = rewardData;
            RewardManager.Instance.Notification.Add("RewardDay" + rewardData.day, () => this.UpdateData());
            UpdateData();
        }

        public void UpdateData()
        {
            if (rewardData.type == RewardType.rewardGold)
            {
                this.avar.sprite = this.imageGold;
                this.numberText.text = "x" + this.rewardData.amount.ToString();
            }
            else
            {
                this.numberText.text = "x" + this.rewardData.numberCandy.ToString();
                CandyData candyData = DataAssets.Instance.GetCandyById(this.rewardData.idCandy);
                this.avar.sprite = candyData.Avatar;
            }
            if (LanguageManager.Instance.languageType == LanguageType.England)
            {
                this.dayText.text = "Day" + (this.rewardData.day).ToString();
            }
            else
            {
                this.dayText.text = "Ngày" + (this.rewardData.day).ToString();
            }

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
            RewardManager.Instance.Claim();
            if (this.rewardData.type == RewardType.rewardGold)
            {
                UserManager.instance.useData.gold += this.rewardData.amount;
                HomeSceneController.Instance.InitText();
            }
            else
            {
                UserManager.instance.SetCandyOnStock(this.rewardData.idCandy, this.rewardData.numberCandy);
            }
        
            this.UpdateData();
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