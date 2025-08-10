
using NTPackage.UI;
using Rubik.ClawMachine;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using Rubik.LuckyGame;
namespace Rubik.ClawMachine
{
    public class HopePanel : PopupUI
    {
        public List<Baby> babies;
        //public void OneClick()
        //{
        //    SoundManager.instance.SoundButton();
        //    PopupManager.Instance.OnUI(this.popupCode, this.FishData, popup => ((FishInfor)popup).ShowInfor(this.FishData));
        //}
        public void SetIcoin(Baby baby)
        {
            for (int i = 0; i < this.babies.Count; i++)
            {
                this.babies[i].IcoinOn.SetActive(false);
            }
            baby.IcoinOn.SetActive(true);
        }

        public override void OnUI(object data = null)
        {
            SetData();
            base.OnUI(data);
        }
        public void SetData()
        {
            if (this.babies.Count != LuckyGameManager.Instance.boxBabyThree.IdBaby.Count) return;
            for (int i = 0; i < this.babies.Count; i++)
            {
                BabyThreeData BabyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(LuckyGameManager.Instance.boxBabyThree.IdBaby[i]);
                this.babies[i].Init(BabyThreeData);
            }
            Debug.Log("setdata");
        }
        public void Choose()
        {
            //LuckyGameManager.Instance.Choose();
            PopupManager.Instance.OffUI(PopupCode.HopePanel);
        }

        public override void UpdateData(object data = null)
        {
            BoxUserData box = UserManager.instance.GetBoxUserDatabyType(LuckyGameManager.Instance.type);
            base.UpdateData(data);
            for (int i = 0; i < this.babies.Count; i++)
            {
                this.babies[i].IcoinOn.SetActive(false);
                if (this.babies[i].babyThreeData.Id == box.IdBabyHold)
                {
                    this.babies[i].IcoinOn.SetActive(true);
                }
            }
        }
    }
}