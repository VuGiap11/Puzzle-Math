using NTPackage.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public enum StatusGame
    {
        StartGame,
        Loading,
        Clawing,
        Result
    }
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance;
        public StatusGame statusGame;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        void Start()
        {
        }
        public void LoadScene(string secene)
        {
            if (loadingScene != null)
            {
                StopCoroutine(loadingScene);
            }
            loadingScene = StartCoroutine(LoadSceneAsync(secene));
        }
        Coroutine loadingScene;
        private IEnumerator LoadSceneAsync(string secene)
        {
            HudCavasManager.instance.loadingPopUpUi.gameObject.SetActive(true);
            HudCavasManager.instance.progressBar.fillAmount = 0f;
            float fakeProgress = 0f;
            while (fakeProgress < 1f)
            {
                fakeProgress += Time.deltaTime*1.5f;
                HudCavasManager.instance.progressBar.fillAmount = fakeProgress;
                yield return null;
            }
            SceneManager.LoadScene(secene);
            yield return new WaitForSeconds(0.005f);
            HudCavasManager.instance.loadingPopUpUi.gameObject.SetActive(false);
            if (secene == "StartScene")
            {
                SoundController.instance.PlayContinueMusicBackGround();
            }
            else if(secene == "ClawMachineGame")
            {
                SoundController.instance.PlayMusicClawing();
            }

            loadingScene = null;
        }
        public void LoadToSceneStartGame()
        {
            SceneManager.LoadScene(Contans.HomeScene);
            SoundController.instance.PlayContinueMusicBackGround();
        }
        public void LoadToSceneGamePlay()
        {
            SceneManager.LoadScene(Contans.GamePlayScene);
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SoundController.instance.PlayMusicClawing();
        }

        public void LoadToSceneMemoryGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.MemoryGameScene);
        }
        public void LoadToSceneMergeGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.MergeGameScene);

        }

        public void BabythreeSweetSagaGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.BabythreeSweetSagaScene);

        }
        public void LoadToLukyBabyThreeGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.BabythreeLuckyScene);
        }
        public void LoadToHomeScene()
        {
           // PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.HomeScene);
        }
    }
}