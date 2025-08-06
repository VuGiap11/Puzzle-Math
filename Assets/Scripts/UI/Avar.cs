using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.math
{
    public class Avar : MonoBehaviour
    {
        public int id;
        public Image avar;
        public GameObject icoinOn;
        public void Init(Sprite sprite)
        {
            this.avar.sprite = sprite;
            if (this.id == UserManager.instance.useData.idAvar)
            {
                icoinOn.SetActive(true);
            }
            else
            {
                icoinOn.SetActive(false);
            }
        }
        public void OneClick()
        {
             SoundController.instance.AudioButton();
            //DataController.instance.dataPlayer.idAvar = this.id;
            UserManager.instance.useData.idAvar = this.id;
            //AvarPanel avarPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.AvatarChangeUI) as AvarPanel;
            //if (avarPanel != null)
            //    avarPanel.SetAvar();
            PopupManager.Instance.UpdateDataUI(PopupCode.AvatarChangeUI);
        }

    }
}
