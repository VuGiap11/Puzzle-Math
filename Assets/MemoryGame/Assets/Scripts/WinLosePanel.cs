using DG.Tweening;
using NTPackage.UI;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{

    public class WinLosePanel : PopupUI
    {
        //public GameObject notice;
        [SerializeField] private TextMeshProUGUI levelText, highLevelText;
        public override void OnUI(object data = null)
        {

            base.OnUI(data);
            this.levelText.text ="Score:" +GameMemoryController.instance.level.ToString();
            this.highLevelText.text = "HighScore:" + UserManager.instance.useData.highLevel.ToString();
            //this.goldAddText.text = "Gold +"+ GameMemoryController.instance.goldMemory.ToString();
            //this.goldMemoryText.text = "+" + GameMemoryController.instance.goldMemory.ToString();
            //int number = (GameMemoryController.instance.goldMemory * 2);
           // this.goldDoubleMemoryText.text = "+" + number.ToString();
           // this.notice.SetActive(false);
        }
        public override void OffUI()
        {
            base.OffUI();
        }

        public void BackGame()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    LoadToStartGame();
                }else
                {
                    AdsManager.instance.ShowInterstitialAd(LoadToStartGame);
                }
               
            }
            else
            {
                LoadToStartGame();
            }
           
        }

        public void LoadToStartGame()
        {
           // UserManager.instance.useData.gold += GameMemoryController.instance.goldMemory;
            UserManager.instance.SaveData();
            base.OffUI();
            SceneController.Instance.LoadToSceneMemoryGame();

        }
        //public void DoubleGame()
        //{
        //    if (NetworkSettingsOpener.Instance.CheckInternet())
        //    {
        //        AdsManager.instance.ShowRewardedInterstitialAd(AddGold);

        //    }
        //    else
        //    {
        //        this.notice.SetActive(true);
        //        DOVirtual.DelayedCall(0.2f, () => this.notice.SetActive(false));
        //    }
        //}
        //public void AddGold()
        //{
        //    base.OffUI();
        //    int number = GameMemoryController.instance.goldMemory * 2;
        //    UserManager.instance.useData.gold += number;
        //    UserManager.instance.SaveData();
        //    SceneController.Instance.LoadToSceneMemoryGame();
        //}
    }

}