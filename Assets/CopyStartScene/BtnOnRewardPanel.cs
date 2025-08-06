using NTPackage.UI;
using UnityEngine;


namespace Rubik.math
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