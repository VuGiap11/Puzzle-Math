
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rubik.ClawMachine;

namespace Rubik.LuckyGame
{
    public class BoxBabyUI : MonoBehaviour
    {
        public Image avar;
        public Image avarOn;
        public HpBar hpBar;
        public TextMeshProUGUI NumberbabyText;
        public void Init(BabyDataLucky babyData)
        {
            string id = babyData.id;
            BabyThreeData babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(id);
            this.avar.sprite = babyThreeData.Avatar;
            this.avarOn.sprite = babyThreeData.Avatar;
            this.NumberbabyText.text = babyData.amount.ToString() + "/" + LuckyGameManager.Instance.boxBabyThree.cap.ToString();
            this.hpBar.UpdateHpBar(LuckyGameManager.Instance.boxBabyThree.cap, babyData.amount);
        }
    }
}