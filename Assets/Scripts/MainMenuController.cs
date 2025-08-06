using Rubik.Sort_Challenge.Data.Loading;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using NTPackage.UI;

namespace Rubik.math
{

    public class MainMenuController : MonoBehaviour
    {
        public static MainMenuController Instance;
        [SerializeField] ItemLoading itemLoading;
        [SerializeField] private Image levelBarSprite;
        public float fillTime = 10f;
        public TextMeshProUGUI percentText;
        private float targetFill = 1f;
        //public List<GameObject> Stars;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public void Start()
        {
            StartGame();
            //RoteIcoinSetting();
        }
        public void StartGame()
        {
            LoadData();
            itemLoading.StartAnimLoading();
            if (loading != null)
            {
                StopCoroutine(loading);
            }
            loading = StartCoroutine(FillBarSmoothly());
        }
        Coroutine loading;
        private IEnumerator FillBarSmoothly()
        {
            //float startFill = levelBarSprite.fillAmount;
            float startFill = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < fillTime)
            {
                levelBarSprite.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / fillTime);
                UpdatePercentageDisplay(levelBarSprite.fillAmount);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            levelBarSprite.fillAmount = targetFill;
            UpdatePercentageDisplay(targetFill);
            itemLoading.StopAnimLoading();
            DOTween.KillAll();
            LoadToSceneStartScene();
        }
        private void UpdatePercentageDisplay(float fillAmount)
        {
            if (percentText != null)
            {
                percentText.text = Mathf.RoundToInt(fillAmount * 100) + "%";
            }
        }
        private void LoadToSceneStartScene()
        {
            if (loading != null)
            {
                StopCoroutine(loading);
            }
            itemLoading.StopAnimLoading();
            //SceneController.Instance.LoadToSceneStartGame();
            SceneManager.LoadScene(Contans.StartScene);
        }
        public void LoadData()
        {
            //DataAssets.instance.LoadData();
            //DataAssets.instance.LoadDataIap();
            //UserDataController.instance.LoadData();
            //RankManager.instance.LoadRank();
            //RewardManager.Instance.LoadData();
            //IAPManager.Instance.Init();
            //AdsManager.instance.Init();
           
            DataAssets.instance.LoadRewardData();
            UserManager.instance.LoadData();
            RewardManager.Instance.LoadData();
        }
        //private void RoteIcoinSetting()
        //{
        //    Vector3 rotate = (Random.Range(0, 2) == 0) ? ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360)) : ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360));
        //    for (int i = 0; i < this.Stars.Count; i++)
        //    {
        //        this.Stars[i].transform.DORotate(rotate, 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

        //    }
        //}
    }
}
