
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class CandyITem : MonoBehaviour
    {
        public Candydata Candydata;
        public int indexCandy;
        [SerializeField] private GameObject IcoinOffCandy;
        [SerializeField] private GameObject IcoinOnCandy;
        [SerializeField] private Image IcoinOff;
        [SerializeField] private Image IcoinOn;
        [SerializeField] private TextMeshProUGUI numberTextOff;
        [SerializeField] private TextMeshProUGUI numberTextOn;

        public void SetFocus(bool focus)
        {
            IcoinOnCandy.SetActive(focus);
            IcoinOffCandy.SetActive(!focus);
            if (focus)
            {
                numberTextOn.text = ("X" + Candydata.number).ToString();
            }
            else
            {
                numberTextOff.text = ("X" + Candydata.number).ToString();
            }
        }

        public void InitText()
        {
            CandyData candy = DataAssets.Instance.GetCandyById(this.Candydata.id);
            this.IcoinOff.sprite = candy.Avatar;
            this.IcoinOn.sprite = candy.Avatar;
            numberTextOn.text = ("X" + Candydata.number).ToString();
            numberTextOff.text = ("X" + Candydata.number).ToString();
        }
        public void ShowCandy()
        {
            SoundController.instance.PressButtonAudio();
            //SoundManager.instance.PressButtonAudio();
            CandyManager.instance.BtnCandyMenuClick(this);
        }
    }
}