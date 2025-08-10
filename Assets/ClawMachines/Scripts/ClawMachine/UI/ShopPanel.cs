using DG.Tweening;
using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class ShopPanel : PopupUI
    {
        [SerializeField] Button buttonBuy;
        [SerializeField] GameObject TextAddObj;
        // private Vector3 origisPosText;
        //public bool canClick;
        public GameObject avar;
        public GameObject Notice, noticeInternet;
        private void Awake()
        {
            //this.origisPosText = TextAddObj.transform.position;
        }
        private void OnEnable()
        {
            //CheckOnOffButton();
            //Rotate360();
        }
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            //CheckOnOffButton();
            //Rotate360();
            this.noticeInternet.SetActive(false);
            this.canclickInternet = false;
            //this.canClick = false;
            this.TextAddObj.SetActive(false);
        }
        private void OnDisable()
        {
            //DOTween.KillAll();
        }

        //public override void OffUI()
        //{
        //    base.OffUI();
        //    this.canclickInternet = false;
        //}
        private bool canclickInternet;
        public void ClickButtonAds()
        {
            if (canclickInternet) return;
            if (!canclickInternet)
            {
                canclickInternet = true;
            }
            SoundController.instance.AudioButton();
            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                this.noticeInternet.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.noticeInternet.SetActive(false);
                });
                this.canclickInternet = false;
                return;
            }
            if (UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsRewardAdReady())
                {
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                }
                else
                {
                    AdsManager.instance.ShowRewardedAd(() =>
                    {
                        AddCoin();
                    });
                }

            }
            else
            {
                if (!AdsManager.instance.IsRewardedInterstitialAdReady())
                {
                    PopupManager.Instance.OnUI(PopupCode.NoAds);
                }
                else
                {
                    AdsManager.instance.ShowRewardedInterstitialAd(() =>
                {
                    AddCoin();
                });
                }
            }
            this.canclickInternet = false;

            //AddCoin();
        }
        public void ClickButtonBuy()
        {
            //if (canClick) return;
            //if (!canClick)
            //{
            //    canClick = true;
            //}
            SoundController.instance.AudioButton();
            if (UserManager.instance.useData.gold < 100)
            {
                this.Notice.SetActive(true);
                DOVirtual.DelayedCall(1f, delegate
                {
                    this.Notice.SetActive(false);
                });
                //this.canClick = false;
                return;
            }
            UserManager.instance.useData.gold -= 100;
            UserManager.instance.useData.numberCoin += 5;
            //CheckOnOffButton();

            UserManager.instance.SaveData();
            HomeController.instance.InitText();
            Move();
        }
        private void AddCoin()
        {
            SoundController.instance.AudioButton();
            UserManager.instance.useData.numberCoin += 5;
            UserManager.instance.SaveData();
            HomeController.instance.InitText();
            this.canclickInternet = false;
            Move();
        }
        //private void Move()
        //{
        //    this.TextAddObj.SetActive(true);
        //    this.TextAddObj.SetActive(false, 0.5f);
        //    //this.TextAddObj.transform.DOMoveY(this.origisPosText.y + 1.5f, 0.5f).SetEase(Ease.Linear)
        //    //    .OnComplete(() =>
        //    //    {
        //    //        this.TextAddObj.SetActive(false);
        //    //        this.TextAddObj.transform.position = this.origisPosText;
        //    //        HomeController.instance.InitText();
        //    //        canClick = false;
        //    //    });
        //}
        private void Move()
        {
            this.TextAddObj.SetActive(true);
            Invoke("DisableTextObj", 1f);
        }

        private void DisableTextObj()
        {
            this.TextAddObj.SetActive(false);
        }
        void Rotate360()
        {
            avar.transform.rotation = Quaternion.Euler(0, 0, 0);
            avar.transform.DORotate(new Vector3(0, -360, 0), 4f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
        }

    }
}