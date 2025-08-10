
using NTPackage.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class HomeController : MonoBehaviour
    {
        public static HomeController instance;
        public InforUser inforUser;
        public AvarPanel avarPanel;
        public Image avartarPlayer;
        public TextMeshProUGUI goldText, coinNumbertext;
        public Button btnPlay;
        public GameObject buttonAds;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            Init();

        }
        public void Init()
        {
            InitText();
            InitButtonRemoveAds();
            this.btnPlay.onClick.AddListener(PlayGame);
        }
        public void InitText()
        {
            this.avartarPlayer.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            this.coinNumbertext.text = UserManager.instance.useData.numberCoin.ToString();
            //Debug.Log("idavar" + UserDataController.instance.dataPlayerController.idAvar);
        }
        public void InitButtonRemoveAds()
        {
            if (UserManager.instance.useData.isRemoveAds)
            {
                this.buttonAds.SetActive(false);
            }
            else
            {
                this.buttonAds.SetActive(true);
            }
        }
        public void SetAvar()
        {
            if (this.avarPanel.listsAva.Count != DataAssets.Instance.imageAvar.Count) return;
            for (int i = 0; i < this.avarPanel.listsAva.Count; i++)
            {
                this.avarPanel.listsAva[i].Init(DataAssets.Instance.imageAvar[i]);
            }
        }

        private void PlayGame()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    SceneController.Instance.LoadToSceneGamePlay();
                }
                else
                {
                    AdsManager.instance.ShowInterstitialAd(() => SceneController.Instance.LoadToSceneGamePlay());
                }
                
            }
            else
            {
                SceneController.Instance.LoadToSceneGamePlay();
            }

            // AdsManager.instance.ShowInterstitialAd();
            //PopupManager.Instance.OnUI(PopupCode.LoadingUI);
        }
        //string GenerateUniqueID()
        //{
        //    return Guid.NewGuid().ToString(); // T?o chu?i ID duy nh?t
        //}
    }
}