
using NTPackage.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class CandyManager : PopupUI
    {
        public static CandyManager instance;
        private int indexCandyOpen = -1;
        [SerializeField] private GameObject nextObj, PreviousObj;
        public CandyITem[] candyITems;
        public Candydata candyData;
        [SerializeField] private Button btnOpen1Chest;
        //[SerializeField] private Button btnOpen10Chest;
        [SerializeField] private Image avarCandy;
   
        [SerializeField] private GameObject candyUi;
        //[SerializeField] private Transform holder;
        public OpenCandy openCandy;
        private void Awake()
        {
            if (instance == null) { }
            instance = this;
        }
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            InitData();
        }
        public void InitData()
        {
            this.indexCandyOpen = -1;
            for (int i = 0; i < candyITems.Length; i++)
            {
                candyITems[i].Candydata = UserManager.instance.Candydatas.candydatas[i];
                candyITems[i].InitText();

            }
            BtnCandyMenuClick(candyITems[0]);
        }
        public void NextCandy()
        {
            SoundController.instance.PressButtonAudio();
            indexCandyOpen++;
            if (indexCandyOpen >= 4)
            {
                nextObj.SetActive(false);
            }
            if (!PreviousObj.gameObject.activeSelf)
            {
                PreviousObj.SetActive(true);
            }
            BtnCandyMenuClick(candyITems[indexCandyOpen]);
        }

        public void ResetBTextButton()
        {
            BtnCandyMenuClick(candyITems[indexCandyOpen]);
        }
        public void PreviousCandy()
        {
            SoundController.instance.PressButtonAudio();
            indexCandyOpen--;
            if (indexCandyOpen <= 0)
            {
                PreviousObj.SetActive(false);
            }
            if (!nextObj.gameObject.activeSelf)
            {
                nextObj.SetActive(true);
            }
            BtnCandyMenuClick(candyITems[indexCandyOpen]);
        }

        public void BtnCandyMenuClick(CandyITem candyITem)
        {
            SoundController.instance.PressButtonAudio();
            for (int i = 0; i < this.candyITems.Length; i++)
            {
                this.candyITems[i].SetFocus(false);
            }
            candyITem.SetFocus(true);
            this.candyData = candyITem.Candydata;
            this.indexCandyOpen = candyITem.indexCandy;

            if (indexCandyOpen <= 0)
            {
                PreviousObj.SetActive(false);
                nextObj.SetActive(true);
            }
            else if (indexCandyOpen >= 3)
            {
                nextObj.SetActive(false);
                PreviousObj.SetActive(true);
            }
            else
            {
                PreviousObj.SetActive(true);
                nextObj.SetActive(true);
            }
            SetAvar(candyITem);
            SetOnOffBtn(candyITem);
        }
        public void SetOnOffBtn(CandyITem candyITem)
        {
            if (candyITem.Candydata.number >= 1)
            {
                btnOpen1Chest.interactable = true;
                //btnOpen1Chest.GetComponent<ButtonScale>().ScaleBtn();
            }
            else

            {
                btnOpen1Chest.interactable = false;
            }
        }

        public void SetAvar(CandyITem candyITem)
        {
            CandyData candy = DataAssets.Instance.GetCandyById(candyITem.Candydata.id);
            this.avarCandy.sprite = candy.Avatar;
        }
        public void OpenOneCandy()
        {
            SoundController.instance.PressButtonAudio();
            this.openCandy.gameObject.SetActive(true);
            CandyData candy = DataAssets.Instance.GetCandyById(this.candyData.id);
            this.openCandy.Init(candy);
        }
        public void Claim()
        {
            SoundController.instance.PressButtonAudio();
            this.openCandy.Claim();
            this.openCandy.gameObject.SetActive(false);
           
            ResetBTextButton();
            ShowUI();
        }

        public void ShowUI()
        {
            for (int i = 0; i < this.candyITems.Length; i++)
            {
                this.candyITems[i].InitText();
            }
        }
     
    }
}