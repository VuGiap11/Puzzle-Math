
using NTPackage.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class InforUser : PopupUI
    {
        public TMP_InputField inputFieldName;
        public Image avatar;

        private string nameCur;
        public void ChangeName()
        {
            SoundController.instance.AudioButton();  
            SetName();

        }
        public void ClosePanelInforUser()
        {
            //SoundController.instance.AudioButton();
            //UserManager.instance.useData.namePlayer = this.nameCur;
            ////this.gameObject.SetActive(false);
            //UserManager.instance.SaveHeroData();
            this.OffUI();
        }
        public void SetName()
        {
            //DataController.instance.dataPlayer.namePlayer = this.inputFieldName.text;
            UserManager.instance.useData.namePlayer = this.inputFieldName.text;
            if (string.IsNullOrEmpty(UserManager.instance.useData.namePlayer))
            {
                UserManager.instance.useData.namePlayer = "Player";
            }
            UserManager.instance.SaveData();
            this.OffUI();
        }

        public void ChangeAvar()
        {
            SoundController.instance.AudioButton();
            PopupManager.Instance.OnUI(PopupCode.AvatarChangeUI);
        }
        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            this.avatar.sprite = DataAssets.instance.imageAvar[UserManager.instance.useData.idAvar];
            HomeManager.instance.avatar.sprite = DataAssets.instance.imageAvar[UserManager.instance.useData.idAvar];
            if (string.IsNullOrEmpty(UserManager.instance.useData.namePlayer))
            {
                UserManager.instance.useData.namePlayer = "Player";
            }
            else
            {
                this.inputFieldName.text = UserManager.instance.useData.namePlayer;
            }
            UserManager.instance.SaveData();
        }
    }
}