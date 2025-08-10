using DG.Tweening;
using NTPackage.UI;
using Rubik.ClawMachine;
using Rubik.LuckyGame;
using UnityEngine;
namespace Rubik.ClawMachine
{
    public class AdsPanelGold : PopupUI
    {
        public bool isOpen;
        [SerializeField] private GameObject noticeObj;
        public override void OnUI(object data = null)
        {
            //if (isOpen) return;
            base.OnUI(data);
            //this.isOpen = true;
            isOpen = false;
            this.noticeObj.SetActive(false);
        }
        public void RewardAds()
        {
            if (isOpen) return;
            if (!isOpen)
            {
                isOpen = true;
            }
            SoundController.instance.AudioButton();
            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                this.noticeObj.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeObj.SetActive(false);
                });
                this.isOpen = false;
                return;
            }
            if (!AdsManager.instance.IsRewardedInterstitialAdReady())
            {
                PopupManager.Instance.OnUI(PopupCode.NoAds);
                return;
            }
            AdsManager.instance.ShowRewardedInterstitialAd(() =>
            {
                AddCoin();
            });
            this.isOpen = false;
            // AddCoin();
        }

        private void AddCoin()
        {
            UserManager.instance.useData.gold += 100;
            LuckyGameManager.Instance.InitText();
            UserManager.instance.SaveData();
            this.OffUI();
            this.isOpen = false;
        }
        //public override void OffUI()
        //{
        //    base.OffUI();
        //    this.isOpen = false;
        //}
    }

}