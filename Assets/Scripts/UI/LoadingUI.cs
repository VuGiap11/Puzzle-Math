using NTPackage.UI;
using Rubik.Sort_Challenge.Data.Loading;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Rubik.ClawMachine
{
    public class LoadingUI : PopupUI
    {
        [SerializeField] ItemLoading itemLoading;
        [SerializeField] private Image levelBarSprite;
        public float fillTime = 2f;
        private float targetFill = 1f;
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            StartLoadingBgUI();
        }
        public void StartLoadingBgUI()
        {
           // SceneController.Instance.statusGame = StatusGame.Loading;
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
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            levelBarSprite.fillAmount = targetFill;
            StopAnim();
            this.OffUI();
            //SceneController.Instance.statusGame = StatusGame.StartGame;
        }
        private void StopAnim()
        {
            if (loading != null)
            {
                StopCoroutine(loading);
            }
            itemLoading.StopAnimLoading();

        }
    }
}