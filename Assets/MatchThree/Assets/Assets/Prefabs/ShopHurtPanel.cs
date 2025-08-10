using DG.Tweening;
using MatchThreeEngine;
using NTPackage.UI;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ShopHurtPanel : PopupUI
    {
        public GameObject noticeTime, noticeInternet;
        public TextMeshProUGUI coinText;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.noticeInternet.SetActive(false);
            this.noticeTime.SetActive(false);
            this.coinText.text = UserManager.instance.useData.gold.ToString();
        }
        public override void OffUI()
        {
            SoundController.instance.AudioButton();
            base.OffUI();
        }
        public void BuyHurt()
        {
            SoundController.instance.AudioButton();

            if (UserManager.instance.useData.gold < 100)
            {
                this.noticeTime.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeTime.SetActive(false);
                });
            }
            else
            {
                UserManager.instance.useData.gold -= 100;
                MatchTheeGameManager.instance.numberSelectOngame++;
                MatchTheeGameManager.instance.InitText();
                this.coinText.text = UserManager.instance.useData.gold.ToString();
                UserManager.instance.SaveData();
                OffUI();
            }
        }
        public void BuyHurtAds()
        {
            SoundController.instance.AudioButton();

            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                this.noticeInternet.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeInternet.SetActive(false);
                });
            }
            else
            {
                if (UserManager.instance.useData.numberAds >= 3)
                {
                    if (!AdsManager.instance.IsRewardAdReady())
                    {
                        PopupManager.Instance.OnUI(PopupCode.NoAds);
                    }
                    else
                    {
                        AdsManager.instance.ShowRewardedAd(ADS);
                    }
                   
                }else
                {
                    if (!AdsManager.instance.IsRewardedInterstitialAdReady())
                    {
                        PopupManager.Instance.OnUI(PopupCode.NoAds);
                    }else
                    {
                        AdsManager.instance.ShowRewardedInterstitialAd(ADS);

                    }
                       
                }
              

            }
        }

        public void ADS()
        {
            MatchTheeGameManager.instance.numberSelectOngame++;
            MatchTheeGameManager.instance.InitText();
            OffUI();
        }
    }
}