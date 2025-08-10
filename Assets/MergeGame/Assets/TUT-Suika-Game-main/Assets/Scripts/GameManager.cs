using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rubik.ClawMachine;
using NTPackage.UI;
using UnityEngine.Rendering;
using Unity.Jobs;

namespace Rubik.MergeGame
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        //public int CurrentScore { get; set; }
        public int CurrentScore;

        [SerializeField] private TextMeshProUGUI _scoreText;
        //[SerializeField] private Image _gameOverPanel;
        //[SerializeField] private float _fadeTime = 2f;

        public float TimeTillGameOver = 1.5f;

        public GameObject BoxObj;
        float referenceWidth = 1080f;  // Chi?u r?ng tham chi?u
        float referenceHeight = 1920f; // Chi?u cao tham chi?u
        public bool isOpen = false;

        public int goldMerge = 0;
        // public TextMeshProUGUI goldMergeText;
        private void OnEnable()
        {
            //SceneManager.sceneLoaded += FadeGame;
        }

        private void Start()
        {
            this.goldMerge = 0;
            // InitGoldMerge();

        }
        private void OnDisable()
        {
            //SceneManager.sceneLoaded -= FadeGame;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            _scoreText.text = CurrentScore.ToString("0");
        }

        public void IncreaseScore(int amount)
        {
            CurrentScore += amount;
            if (this.CurrentScore >= UserManager.instance.useData.highScore)
            {
                UserManager.instance.useData.highScore = this.CurrentScore;
                UserManager.instance.SaveData();
            }
            _scoreText.text = CurrentScore.ToString("0");
            //InitGoldMerge();
        }

        //public void InitGoldMerge()
        //{
        //    this.goldMergeText.text = goldMerge.ToString();
        //}
        //public void GameOver()
        //{
        //    StartCoroutine(ResetGame());
        //}

        //private IEnumerator ResetGame()
        //{
        //    _gameOverPanel.gameObject.SetActive(true);

        //    Color startColor = _gameOverPanel.color;
        //    startColor.a = 0f;
        //    _gameOverPanel.color = startColor;

        //    float elapsedTime = 0f;
        //    while(elapsedTime < _fadeTime)
        //    {
        //        elapsedTime += Time.deltaTime;

        //        float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / _fadeTime));
        //        startColor.a = newAlpha;
        //        _gameOverPanel.color = startColor;

        //        yield return null;
        //    }

        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        //private void FadeGame(Scene scene, LoadSceneMode mode)
        //{
        //    StartCoroutine(FadeGameIn());
        //}

        //private IEnumerator FadeGameIn()
        //{
        //    _gameOverPanel.gameObject.SetActive(true);
        //    Color startColor = _gameOverPanel.color;
        //    startColor.a = 1f;
        //    _gameOverPanel.color = startColor;

        //    float elapsedTime = 0f;
        //    while(elapsedTime < _fadeTime)
        //    {
        //        elapsedTime += Time.deltaTime;

        //        float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / _fadeTime));
        //        startColor.a = newAlpha;
        //        _gameOverPanel.color = startColor;

        //        yield return null;
        //    }

        //    _gameOverPanel.gameObject.SetActive(false);
        //}
        public void ScaleClawMachine()
        {
            this.isOpen = false;
            float referenceAspect = referenceWidth / referenceHeight;
            float scaleFactor = (float)Screen.width / referenceWidth;
            float aspectRatio = (float)Screen.width / Screen.height;
            if (aspectRatio >= 2.1f) // ?i?n tho?i siêu dài (21:9)
            {
                BoxObj.transform.localScale = new Vector3(0.82f, 0.82f, 1);
            }
            else if (aspectRatio >= 1.8f) // ?i?n tho?i ph? thông (19.5:9, 18:9)
            {
                BoxObj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
            }
            else if (aspectRatio >= 1.5f) // Tablet ho?c ?i?n tho?i c? (16:10, 4:3)
            {
                //1.5
                BoxObj.transform.localScale = new Vector3(0.9f, 0.9f, 1);
            }
            else // Màn hình vuông ho?c nh?
            {
                float scaleFactorWidth = (float)Screen.width / referenceWidth;  // H? s? theo chi?u r?ng
                if (scaleFactorWidth >= 1f)
                {
                    BoxObj.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    BoxObj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
            }

        }

        public void ExitGame()
        {
            UserManager.instance.useData.gold += GameManager.instance.goldMerge;
            UserManager.instance.SaveData();
            SoundController.instance.AudioButton();
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    LoadToStartGame();

                }
                else
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
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneController.Instance.LoadToSceneStartGame();
        }
    }

}