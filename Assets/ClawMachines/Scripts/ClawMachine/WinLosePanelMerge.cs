using DG.Tweening;
using NTPackage.UI;
using Rubik.ClawMachine;
using TMPro;
using UnityEngine;

namespace Rubik.MergeGame
{

    public class WinLosePanelMerge : PopupUI
    {
        [SerializeField] private TextMeshProUGUI pointText, highPointText, goldAddText, goldText, doubleGoldText;
        public GameObject notice;
        public override void OnUI(object data = null)
        {

            this.notice.SetActive(false);
            base.OnUI(data);
            this.goldAddText.text = "Gold+" + GameManager.instance.goldMerge.ToString();
            this.pointText.text = "Score:" + GameManager.instance.CurrentScore.ToString();
            this.highPointText.text = "HighScore:" + UserManager.instance.useData.highScore.ToString();
            this.goldText.text = "+" + GameManager.instance.goldMerge.ToString();
            int number = (GameManager.instance.goldMerge * 2);
            this.doubleGoldText.text = "+" + number.ToString();


        }
        public override void OffUI()
        {
            GameManager.instance.isOpen = false;
            base.OffUI();
        }

        public void BackGame()
        {
            base.OffUI();
            UserManager.instance.useData.gold += GameManager.instance.goldMerge;
            UserManager.instance.SaveData();
            SceneController.Instance.LoadToSceneMergeGame();
        }
        public void DoubleGame()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet())
            {
                if (UserManager.instance.useData.numberAds >= 3)
                {
                    if (!AdsManager.instance.IsRewardAdReady())
                    {
                        PopupManager.Instance.OnUI(PopupCode.NoAds);
                    }
                    else
                    {
                        AdsManager.instance.ShowRewardedAd(AddGold);
                    }


                }
                else
                {
                    if (!AdsManager.instance.IsRewardedInterstitialAdReady())
                    {
                        PopupManager.Instance.OnUI(PopupCode.NoAds);
                    }
                    else
                    {
                        AdsManager.instance.ShowRewardedInterstitialAd(AddGold);
                    }

                }

            }
            else
            {
                this.notice.SetActive(true);
                DOVirtual.DelayedCall(0.2f, () => this.notice.SetActive(false));
            }
        }


        public void AddGold()
        {
            base.OffUI();
            int number = GameManager.instance.goldMerge * 2;
            UserManager.instance.useData.gold += number;
            UserManager.instance.SaveData();
            SceneController.Instance.LoadToSceneMergeGame();
        }
    }
}

