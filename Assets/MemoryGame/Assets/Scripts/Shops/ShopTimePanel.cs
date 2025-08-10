using DG.Tweening;
using NTPackage.UI;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{

    public class ShopTimePanel : PopupUI
    {
        public GameObject noticeTime, noticeInternet;
        public TextMeshProUGUI coinText;

        public override void OnUI(object data = null)
        {
            SoundController.instance.AudioButton();
            GameMemoryController.instance.PauseGame();
            this.noticeInternet.SetActive(false);
            this.noticeTime.SetActive(false);
            this.coinText.text = UserManager.instance.useData.gold.ToString();
            base.OnUI(data);
        }
        public override void OffUI()
        {
            //GameController.instance.ContinuteGame();
            base.OffUI();
        }
        public void CloseShopTime()
        {
            SoundController.instance.AudioButton();
            GameMemoryController.instance.ContinuteGame();
            OffUI();
         
           
          //
        }
        public void BuyTime()
        {
            SoundController.instance.AudioButton();
            if (UserManager.instance.useData.gold < 50)
            {
                this.noticeTime.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeTime.SetActive(false);
                });
            }
            else
            {
                UserManager.instance.useData.gold -= 50;
                GameMemoryController.instance.numberResetTime += 1;
                this.coinText.text = UserManager.instance.useData.gold.ToString();
                GameMemoryController.instance.InitText();
                UserManager.instance.SaveData();
                GameMemoryController.instance.ContinuteGame();
                OffUI();
            }
        }

        public void BuyTimeAds()
        {
            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                this.noticeInternet.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeInternet.SetActive(false);
                });
                return;
            }

            if (UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsRewardAdReady())
                {
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                    return;
                }
                else
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
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                    return;
                }
                else
                {
                    AdsManager.instance.ShowRewardedInterstitialAd(() =>
                    {
                        AddCoin();
                    });
                }
            }
        }
        public void AddCoin()
        {
            GameMemoryController.instance.numberResetTime += 1;
            this.coinText.text = UserManager.instance.useData.gold.ToString();
            GameMemoryController.instance.InitText();
            UserManager.instance.SaveData();
            GameMemoryController.instance.ContinuteGame();
            OffUI();
        }
    }

}