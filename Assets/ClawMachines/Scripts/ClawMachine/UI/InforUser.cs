
using NailSalonGame;
using NTPackage.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class InforUser : PopupUI
    {
        public TMP_InputField inputFieldName;
        public Image avatar;

        private string nameCur;
        public void Init()
        {
            this.inputFieldName.text = UserManager.instance.useData.namePlayer;
            this.avatar.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
            this.nameCur = UserManager.instance.useData.namePlayer;
        }
        private void OnEnable()
        {
            Init();
        }
        public void ChangeName()
        {
            //SoundController.instance.AudioButton();
            //if (!NetworkSettingsOpener.Instance.CheckInternet())
            //{
            //    SetName();
            //}else
            //{
            //    AdsManager.instance.ShowInterstitialAd();
            //    SetName();
            //    //AdsManager.instance.ShowInterstitialAd(() =>
            //    //{
            //    //    SetName();
            //    //});
            //}

            SetName();

        }
        public void ClosePanelInforUser()
        {
            SoundController.instance.AudioButton();
            UserManager.instance.useData.namePlayer = this.nameCur;
            //this.gameObject.SetActive(false);
            UserManager.instance.SaveData();
            this.OffUI();
        }
        public void SetName()
        {
            UserManager.instance.useData.namePlayer = this.inputFieldName.text;
            if (string.IsNullOrEmpty(UserManager.instance.useData.namePlayer))
            {
                UserManager.instance.useData.namePlayer = "BabyThree";
            }
            UserManager.instance.SaveData();
            this.OffUI();
            RankDataManager.instance.LoadRank();
        }

        public void ChangeAvar()
        {
            SoundController.instance.AudioButton();
            // this.OffUI();


            SoundController.instance.AudioButton();
            ChanegeAvarADS();
            //if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            //{
            //    if (!AdsManager.instance.IsInterstitialAdReady())
            //    {
            //        ChanegeAvarADS();
            //    }
            //    else
            //    {
            //        AdsManager.instance.ShowInterstitialAd(ChanegeAvarADS);
            //    }


            //}
            //else
            //{
            //    ChanegeAvarADS();
            //}

        }

        public void ChanegeAvarADS()
        {
            AvarPanel avarPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.AvatarChangeUI) as AvarPanel;
            if (avarPanel != null)
            {
                avarPanel.OnUI();
            }
            else
            {
                Debug.Log("hopepanel is null");
            }
        }

        public void SetAvatar()
        {
            this.avatar.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
            HomeSceneController.Instance.avartarPlayer.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
        }
    }
}