using UnityEngine;
using Rubik.ClawMachine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using RubikCasual.Roulette;
using System.Collections;
using DG.Tweening;
using NTPackage.UI;
using Unity.Jobs;
using UnityEngineInternal;
using Unity.VisualScripting;
namespace Rubik.LuckyGame
{
    public class LuckyGameManager : MonoBehaviour
    {
        public static LuckyGameManager Instance;
        public BoxBabyThree boxBabyThree;
        public BoxUserData boxuserData;
        public TypeBox type;
        public string idChoose;
        public string idPerfect;
        public Image avarBabyChoose;
        public Image avarBaby;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI amounBabyText;
        private int indexCandyOpen = -1;
        [SerializeField] private GameObject nextObj, PreviousObj;
        public List<Slot> lsSlot;
        public RouletteController RouletteController;
        // public BabyLucky defeat;
        public SpinBabyDone win;
        public bool isWin = false;
        public bool isSpinning = false;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Start()
        {
            Init();
            isSpinning = false;
            this.indexCandyOpen = 0;
            if (indexCandyOpen <= 0)
            {
                PreviousObj.SetActive(false);
            }
            DoneTime();
            DOVirtual.DelayedCall(0.2f, () => PopupManager.Instance.OnUI(PopupCode.Tutorial));
            //PopupManager.Instance.OnUI(PopupCode.Tutorial);
        }

        public void SetAvar()
        {
            BabyThreeData babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(this.idChoose);
            this.avarBabyChoose.sprite = babyThreeData.Avatar;
        }

        private int random = 0, Loop = 10, DistanceSpin = 10;
        public void Choose()
        {

            BoxBabyThree boxBabyThree = DataAssets.Instance.GetBoxBabyThreebyType(this.type);
            this.random = Random.Range(0, this.lsSlot.Count);
            this.idPerfect = this.lsSlot[this.random].id;
            //if (random <= this.lsSlot.Count)
            //{

            //}
            //else
            //{
            //    int number = Random.Range(0, boxBabyThree.IdBaby.Count);
            //    this.idPerfect = boxBabyThree.IdBaby[number];
            //}

            if (this.idPerfect == this.idChoose)
            {
                Debug.Log("win");
                this.isWin = true;
            }
            else
            {
                this.isWin = false;
                Debug.Log("Defeat");
            }

            Debug.Log("id" + this.idPerfect);
            BabyThreeData babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(this.idPerfect);
            //this.babyObj.GetComponent<Image>().sprite = babyThreeData.Avatar;
            //DataController.instance.SaveData();
            //this.btnClaim.gameObject.SetActive(false);
            //this.btnClose.gameObject.SetActive(false);
        }

        public void SetWinLose()
        {
            if (isWin)
            {
                this.win.gameObject.SetActive(true);
                SoundController.instance.AudioVictory();
                this.win.Init();
                Debug.Log("win");
            }
            else
            {
                //this.defeat.gameObject.SetActive(true);
                //this.defeat.Init();
                PopupManager.Instance.OnUI(PopupCode.BabyLucKyPanel);
                SoundController.instance.AudioDefeat();
            }
        }

        public void Init()
        {
            this.boxBabyThree = DataAssets.Instance.GetBoxBabyThreebyType(this.type);
            this.boxuserData = UserManager.instance.GetBoxUserDatabyType(this.type);
            this.idChoose = boxuserData.IdBabyHold;
            BabyDataLucky babyData = SetBabyData();
            BabyThreeData babyThreeData;
            if (babyData != null)
            {
                babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(babyData.id);
            }
            else
            {
                babyData = this.boxuserData.BabyDatas[this.boxuserData.BabyDatas.Count - 1];
                babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(babyData.id);
            }

            this.avarBaby.sprite = babyThreeData.Avatar;
            SetAvar();
            this.priceText.text = this.boxBabyThree.price.ToString();
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            InitText(babyData.amount);
            InitSlot();
            UserManager.instance.SaveData();
        }

        public BabyDataLucky SetBabyData()
        {
            BabyDataLucky babyData = null;
            for (int i = 0; i < this.boxuserData.BabyDatas.Count; i++)
            {
                if (this.boxuserData.BabyDatas[i].isDone) continue;
                babyData = this.boxuserData.BabyDatas[i];
                return babyData;
            }
            return babyData = null;
        }

        public void InitText(int a)
        {
            this.amounBabyText.text = a.ToString() + "/" + this.boxBabyThree.cap.ToString();
        }
        public void NextCandy()
        {
            if (isSpinning) return;
            SoundController.instance.AudioButton();
            indexCandyOpen++;
            if (indexCandyOpen >= 3)
            {
                nextObj.SetActive(false);
            }
            if (!PreviousObj.gameObject.activeSelf)
            {
                PreviousObj.SetActive(true);
            }
            this.type = (TypeBox)((int)this.type + 1);
            Init();
        }

        public void PreviousCandy()
        {
            if (isSpinning) return;
            SoundController.instance.AudioButton();
            indexCandyOpen--;
            if (indexCandyOpen <= 0)
            {
                PreviousObj.SetActive(false);
            }
            if (!nextObj.gameObject.activeSelf)
            {
                nextObj.SetActive(true);
            }
            this.type = (TypeBox)((int)this.type - 1);
            Init();
        }

        public void InitSlot()
        {
            for (int i = 0; i < this.lsSlot.Count; i++)
            {
                this.lsSlot[i].Init(this.boxBabyThree.IdBaby[i]);
            }
        }

        Sprite avar;
        public Sprite SetAvatar()
        {

            BabyThreeData babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(this.idPerfect);
            avar = babyThreeData.Avatar;
            return avar;
        }
        public float timer = 0f;
        public float maxTime = 10f;
        public bool isDownTime = false;
        public TimeBar hpbarTime;
        public IEnumerator CountTime()
        {
            //while (isDownTime)
            //{
            //    yield return new WaitForSeconds(1);
            //    this.timer++;
            //    if (this.timer >= maxTime)
            //    {
            //        this.timer = 0f;
            //    }
            //    InitTextTime();
            //}
            while (isDownTime)
            {
                this.timer += Time.deltaTime;
                if (this.timer >= maxTime)
                {
                    this.timer = 0f;
                }
                InitTextTime();
                yield return null;
            }
        }
        public void InitTextTime()
        {
            this.hpbarTime.UpdateHpBar(this.maxTime, this.timer);
        }
        Coroutine timeDown;

        public bool isCanSpin = false;
        public void StartSpin()
        {
            if (isSpinning) return;
            if (isCanSpin) return;

            if (UserManager.instance.useData.gold < this.boxBabyThree.price)
            {
                PopupManager.Instance.OnUI(PopupCode.BonousPanel);
                DoneTime();
                isCanSpin = true;
                return;
            }
            this.isDownTime = true;
            this.timer = 0;
            Choose();
            if (this.timeDown != null)
            {
                StopCoroutine(timeDown);
            }
            this.timeDown = StartCoroutine(CountTime());

        }
        public void DoneSpin()
        {
            if (isSpinning) return;
            if (isCanSpin) return;
            if (!isSpinning)
            { isSpinning = true; }
            this.isDownTime = false;
            if (this.timeDown != null)
            {
                StopCoroutine(timeDown);
            }
            SoundController.instance.PlayMusicLucKyGame();
            CheckTimerAndRunFunction(this.timer);
            UserManager.instance.useData.gold -= this.boxBabyThree.price;
            UserManager.instance.SaveData();
            InitText();
            this.RouletteController.RotateSpin(this.random, this.Loop, this.DistanceSpin);
        }
        void CheckTimerAndRunFunction(float timer)
        {
            if (timer < 3f)
            {
                this.Loop = 2;
                this.DistanceSpin = 2;
            }
            else if (timer > 8f)
            {
                this.Loop = 10;
                this.DistanceSpin = 10;
            }
            else if (timer >= 3f && timer <= 5f)
            {
                this.Loop = 5;
                this.DistanceSpin = 5;
            }
            else if (timer > 5f && timer <= 8f)
            {
                this.Loop = 8;
                this.DistanceSpin = 8;
            }
        }
        public void ActionEnd()
        {
            SoundController.instance.AudioButton();
            BabyDataLucky babyData = SetBabyData();
            babyData.amount++;
            if (babyData != null)
            {
                if (babyData.amount >= this.boxBabyThree.cap)
                {
                    babyData.isDone = true;
                }
            }
            else
            {
                babyData = this.boxuserData.BabyDatas[this.boxuserData.BabyDatas.Count - 1];
                if (babyData.amount >= this.boxBabyThree.cap)
                {
                    babyData.amount = this.boxBabyThree.cap;
                    babyData.isDone = true;
                }
            }
            Init();
        }

        public void OpenWish()
        {
            if (isSpinning)
            {
                return;
            }
            PopupManager.Instance.OnUI(PopupCode.HopePanel);
        }
        public void DoneTime()
        {
            this.timer = 0;
            InitTextTime();
        }
        public void InitText()
        {
            this.goldText.text = UserManager.instance.useData.gold.ToString();
        }
        public void BackGame()
        {
            if (isSpinning)
            {
                return;
            }
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    ToStartGame();
                }
                else
                {
                    AdsManager.instance.ShowInterstitialAd(ToStartGame);
                }

            }
            else
            {
                ToStartGame();
            }

        }

        public void ToStartGame()
        {
            SoundController.instance.AudioButton();
            SceneController.Instance.LoadToSceneStartGame();
        }
    }
}