using NTPackage.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class AvarPanel : PopupUI
    {
        public List<Avar> listsAva;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.SetAvar();

        }
        public void ChangeAvar()
        {
            SoundController.instance.AudioButton();
           if (NetworkSettingsOpener.Instance.CheckInternet()&&UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    ChangeAvatar();
                }else
                {
                    AdsManager.instance.ShowInterstitialAd(ChangeAvatar);
                }
                
            }
            else
            {
                ChangeAvatar();
            }


        }

        public void ChangeAvatar()
        {
            UserManager.instance.SaveData();
            InforUser inforUser = PopupManager.Instance.GetPopupUIByCode(PopupCode.NameChangeUI) as InforUser;
            if (inforUser != null)
            {
                inforUser.SetAvatar();
            }
            else
            {
                Debug.Log("hopepanel is null");
            }
            this.OffUI();
        }
        public void SetAvar()
        {
            if (this.listsAva.Count != DataAssets.Instance.imageAvar.Count) return;
            for (int i = 0; i < this.listsAva.Count; i++)
            {
                this.listsAva[i].Init(DataAssets.Instance.imageAvar[i]);
            }
        }
    }
}