using Rubik.ClawMachine;
using System.Collections;
using Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Sort_Challenge.Data.Loading
{
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] ItemLoading itemLoading;
        [SerializeField] Slider sliderLoading;
        [SerializeField] Text textLoading;
        [SerializeField] float timeLoading = 1.5f;
        private void Start()
        {
            //StartLoadingScene(100, "ClawMachineGame");
        }
        public void StartLoadingScene(int value, string sceneName)
        {
            //UserManager.instance.canIsClaw = true;
            gameObject.SetActive(true);
            itemLoading.StartAnimLoading();
            StartCoroutine(ActionText.ShowCountTextPercent(0, value, textLoading, sliderLoading, 0, timeLoading));
            StartCoroutine(WaitLoading(sceneName));
        }
        IEnumerator WaitLoading(string sceneName)
        {
            yield return new WaitForSeconds(timeLoading / 2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            yield return new WaitForSeconds(timeLoading/2f);
            itemLoading.StopAnimLoading();
            gameObject.SetActive(false);
           // UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            //UserManager.instance.canIsClaw = false;
        }
    }

}