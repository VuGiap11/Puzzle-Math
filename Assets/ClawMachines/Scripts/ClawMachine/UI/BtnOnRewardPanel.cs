using NTPackage.UI;
using Rubik.ClawMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rubik.ClawMachine
{
    public class BtnOnRewardPanel : BtnOnPopupUI
    {
        public Transform Noti;
        void Start()
        {
            RewardManager.Instance.Notification.Add("BtnOnRewardPanel", () => this.UpdateData());
            this.UpdateData();
        }

        public void UpdateData()
        {
            this.Noti.gameObject.SetActive(RewardManager.Instance.IsNewDay());
        }
    }
}