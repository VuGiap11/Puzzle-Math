
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class BuyClaw : MonoBehaviour
    {
        public GameObject Notice;
        public int gold;
        public int numberClaw;
        [SerializeField] private TextMeshProUGUI goldText, numberClawtext;
        private bool canclick;
        public void ClickButtonBuy()
        {
            SoundController.instance.AudioButton();
            if (this.canclick) return;
            if (UserManager.instance.useData.gold < this.gold)
            {
                this.canclick = true;
                this.Notice.SetActive(true);
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    this.canclick = false;
                    this.Notice.SetActive(false);
                });
                return;
            }
            UserManager.instance.useData.gold -= this.gold;
            UserManager.instance.useData.numberCoin += this.numberClaw;
            //CheckOnOffButton();

            UserManager.instance.SaveData();
            HomeController.instance.InitText();
        }

        public void Init()
        {
            this.Notice.SetActive(false);
            this.goldText.text = this.gold.ToString();
            this.numberClawtext.text = this.numberClaw.ToString();
            
        }
    }
}