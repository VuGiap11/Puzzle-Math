using UnityEngine;
using Rubik.ClawMachine;
using TMPro;
using NTPackage.UI;

namespace MatchThreeEngine
{
    public class MatchTheeGameManager : MonoBehaviour
    {
        public static MatchTheeGameManager instance;
        [SerializeField] TextMeshProUGUI pointText, goldText, numberSelectText, numberShuffleText;
        public int numberSelectOngame = 6;
        public int numberShuffleOngame = 6;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            //this.numberSelectOngame = 6;
            //this.numberShuffleOngame = 6;
            //InitText();
           
        }

        public void InitText()
        {
            this.pointText.text = BoardManager.Instance.point.ToString();
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            if (numberSelectOngame <= 0)
            {
                this.numberSelectText.text = "+";
            }
            else { this.numberSelectText.text = this.numberSelectOngame.ToString(); }

            if (numberShuffleOngame <= 0)
            {
                this.numberShuffleText.text = "+";
            }
            else
            {
                this.numberShuffleText.text = this.numberShuffleOngame.ToString();
            }


            UserManager.instance.SaveData();
        }

        public void ExitGame()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    SoundController.instance.AudioButton();
                    Ads();
                }else
                {
                    AdsManager.instance.ShowInterstitialAd(Ads);

                }
               
            }
            else
            {
                SoundController.instance.AudioButton();
                Ads();
            }

        }
        public void Ads()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneController.Instance.LoadToSceneStartGame();

        }
    }
}

