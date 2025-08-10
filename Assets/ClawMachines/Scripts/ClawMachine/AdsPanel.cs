using DG.Tweening;
using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{

    public class AdsPanel : PopupUI
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
          
            if (UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsRewardAdReady())
                {
                    this.isOpen = false;
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                    return;
                }else
                {
                    AdsManager.instance.ShowRewardedAd(() =>
                    {
                        AddCoin();
                    });
                }
               

            }
            else
            {
                if (!AdsManager.instance.IsRewardedInterstitialAdReady())
                {
                    this.isOpen = false;
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                    return;
                }else
                {
                    AdsManager.instance.ShowRewardedInterstitialAd(() =>
                    {
                        AddCoin();
                    });
                }
               
            }
            this.isOpen = false;
            // AddCoin();
        }


        private void AddCoin()
        {
            UserManager.instance.useData.numberCoin += 1;
            ClawGameManager.Instance.InitGold(UserManager.instance.useData.numberCoin);
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