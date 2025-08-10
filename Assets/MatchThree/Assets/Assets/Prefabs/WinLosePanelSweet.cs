using MatchThreeEngine;
using NTPackage.UI;
using TMPro;
using UnityEngine;
namespace Rubik.ClawMachine
{
    public class WinLosePanelSweet : PopupUI
    {
        [SerializeField] private TextMeshProUGUI levelText, highLevelText;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.levelText.text = "Score:" + BoardManager.Instance.point.ToString();
            this.highLevelText.text = "HighScore:" + UserManager.instance.useData.highScoreMatch3.ToString();
        }
        public void BackGame()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    ADS();
                }
                else
                {
                    AdsManager.instance.ShowInterstitialAd(ADS);

                }


            }
            else
            {
                ADS();
            }

        }

        public void ADS()
        {
            SoundController.instance.AudioButton();
            base.OffUI();
            BoardManager.Instance.isEndGame = false;
            SceneController.Instance.BabythreeSweetSagaGame();
        }
    }

}