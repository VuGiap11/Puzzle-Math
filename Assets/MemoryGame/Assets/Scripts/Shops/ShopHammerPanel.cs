using DG.Tweening;
using NTPackage.UI;
using TMPro;
using UnityEngine;
namespace Rubik.ClawMachine
{

    public class ShopHammerPanel : PopupUI
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
            // SoundController.instance.AudioButton();
            //GameController.instance.ContinuteGame();

            base.OffUI();
        }
        public void CloseShopHammer()
        {
            SoundController.instance.AudioButton();
            GameMemoryController.instance.ContinuteGame();
            OffUI();
            

        }
        public void BuyHammer()
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
               GameMemoryController.instance.numberHammer += 1;
                this.coinText.text = UserManager.instance.useData.gold.ToString();
                GameMemoryController.instance.InitText();
                UserManager.instance.SaveData();
                GameMemoryController.instance.ContinuteGame();
                OffUI();
            }
           
        }

        public void BuyHammerAds()
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
            GameMemoryController.instance.numberHammer += 1;
            this.coinText.text = UserManager.instance.useData.gold.ToString();
            GameMemoryController.instance.InitText();
            UserManager.instance.SaveData();
            GameMemoryController.instance.ContinuteGame();
            OffUI();
        }
    }

}