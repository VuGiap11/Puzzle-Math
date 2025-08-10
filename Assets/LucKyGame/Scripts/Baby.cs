
using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;
using Rubik.ClawMachine;

namespace Rubik.LuckyGame
{
    public class Baby : MonoBehaviour
    {
        public BabyThreeData babyThreeData;
        public Image avar;
        public GameObject IcoinOn;

        public void Init(BabyThreeData babyThreeData)
        {
            this.babyThreeData = babyThreeData;
            avar.sprite = babyThreeData.Avatar;
            BoxUserData box = UserManager.instance.GetBoxUserDatabyType(LuckyGameManager.Instance.type);
            if (this.babyThreeData.Id == box.IdBabyHold)
            {
                IcoinOn.SetActive(true);
            }else
            {
                IcoinOn.SetActive(false);
            }
        }

        public void ChooseBaby()
        {
            SoundController.instance.AudioButton();
            BoxUserData box = UserManager.instance.GetBoxUserDatabyType(LuckyGameManager.Instance.type);
            box.IdBabyHold = this.babyThreeData.Id;
            LuckyGameManager.Instance.idChoose = this.babyThreeData.Id;
            LuckyGameManager.Instance.SetAvar();
            UserManager.instance.SaveData();
            PopupManager.Instance.UpdateDataUI(PopupCode.HopePanel);
        }
    }
}