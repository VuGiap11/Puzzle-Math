
using NTPackage.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rubik.math
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        //public void LoadScene(string secene)
        //{
        //    if (loadingScene != null)
        //    {
        //        StopCoroutine(loadingScene);
        //    }
        //    loadingScene = StartCoroutine(LoadSceneAsync(secene));
        //}
        //Coroutine loadingScene;
        //private IEnumerator LoadSceneAsync(string secene)
        //{
        //    HudCavasManager.instance.loadingPopUpUi.gameObject.SetActive(true);;
        //    HudCavasManager.instance.progressBar.fillAmount = 0f;
        //    float fakeProgress = 0f;
        //    while (fakeProgress < 1f)
        //    {
        //        fakeProgress += Time.deltaTime*1.5f;
        //        HudCavasManager.instance.progressBar.fillAmount = fakeProgress;
        //        yield return null;
        //    }
        //    SceneManager.LoadScene(secene);
        //    yield return new WaitForSeconds(0.005f);
        //    HudCavasManager.instance.loadingPopUpUi.gameObject.SetActive(false);
        //    if (secene == "StartScene")
        //    {
        //        SoundController.instance.PlayContinueMusicBackGround();
        //    }
        //    else if(secene == "ClawMachineGame")
        //    {
        //        SoundController.instance.PlayMusicClawing();
        //    }

        //    loadingScene = null;
        //}

        public void LoadToSceneStartGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneManager.LoadScene(Contans.StartScene);
            PopupManager.Instance.OffUI(PopupCode.AnswerPanel);
            PopupManager.Instance.OffUI(PopupCode.MathPanel);
            PopupManager.Instance.OffUI(PopupCode.MathTestPanel);
        }
        public void LoadToSceneGamePlay()
        {

            switch (GameController.instance.mathType)
            {
                case MathType.Addition:
                    SceneManager.LoadScene(Contans.AdditionScene);
                    break;
                case MathType.Subtraction:
                    SceneManager.LoadScene(Contans.SubtractionScene);
                    break;
                case MathType.Multipacation:
                    SceneManager.LoadScene(Contans.MultipcationScene);
                    break;
                case MathType.Division:
                    SceneManager.LoadScene(Contans.DivisionScene);
                    break;
                case MathType.Test:
                    SceneManager.LoadScene(Contans.TestScene);
                    break;
            }
            
        }

    }
}