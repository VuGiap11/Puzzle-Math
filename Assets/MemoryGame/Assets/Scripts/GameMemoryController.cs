
using NTPackage.UI;
using System;
using System.Collections;
using TMPro;
using Tool.HammerBreak;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class GameMemoryController : MonoBehaviour
    {
        public static GameMemoryController instance;
        public TextMeshProUGUI timeText;
        public float timer = 150f;
        public float maxTime = 150f;
        public int level = 0;
        public HpBar hpbarTime;
        public Transform startHammer;
        public Vector3 endHammer;
        public GameObject hammer;
        public TextMeshProUGUI numberHammerText, numberTimeText, levelText, coinText;
        public Transform endCftPos, startCftPos;
        public ConfettiManager confettiManager;
        public int goldMemory = 0;
        public int numberHammer = 3;
        public int numberResetTime = 3;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            //SetTime();
            //if (this.CorCountTime != null)
            //{
            //    StopCoroutine(this.CorCountTime);
            //}
            //this.CorCountTime = StartCoroutine(CountTime());
            this.hammer.SetActive(false);
            this.numberHammer = 3;
            this.numberResetTime = 3;
            this.hammer.transform.rotation = Quaternion.Euler(0, 0, 0);
            InitText();

        }

        public void SetTime()
        {
            this.timeText.text = ParseTimeToDay();
            this.hpbarTime.UpdateHpBar((int)this.maxTime, (int)this.timer);
        }
        public string ParseTimeToDay()
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt((float)timer % 60);

            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }

        public void PauseGame()
        {
            if (this.CorCountTime != null)
            {
                StopCoroutine(this.CorCountTime);
            }
        }
        public void ContinuteGame()
        {
            if (this.CorCountTime != null)
            {
                StopCoroutine(this.CorCountTime);
            }
            this.CorCountTime = StartCoroutine(CountTime());
        }
        public Coroutine CorCountTime;

        public IEnumerator CountTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.timer--;
                SetTime();
                if (this.timer <= 0) break;
            }
            EndGame();
        }
        public void StopTime()
        {
            if (this.CorCountTime != null)
            {
                StopCoroutine(this.CorCountTime);
            }
            this.timer = this.maxTime;
            SetTime();

        }
        public void ResetTime()
        {
            this.timer = this.maxTime;
            SetTime();
            if (this.CorCountTime != null)
            {
                StopCoroutine(this.CorCountTime);
            }
            this.CorCountTime = StartCoroutine(CountTime());
        }
        public void EndGame()
        {
            PopupManager.Instance.OnUI(PopupCode.WinLosePanel);
            Debug.Log("EndGame");
        }

        Card card;
        private bool activeHamer = false;
        public void HammerActive()
        {
            if (CardsController.instance.canSelect)
            {
                return;
            }
            if (activeHamer) return;
            if (!activeHamer)
            {
                activeHamer = true;
            }
            if (this.numberHammer <= 0)
            {
                this.activeHamer = false;
                PopupManager.Instance.OnUI(PopupCode.ShopHammerPanel);
                return;
            }
            else
            {
                this.numberHammer -= 1;
                this.card = CardsController.instance.SetCardToHammer();
                this.endHammer = new Vector3(this.card.transform.position.x + 0.6f, this.card.transform.position.y, this.card.transform.position.z);
                this.hammer.SetActive(true);
                //this.endHammer = this.card.transform;
                this.hammer.SetActive(true);
                this.hammer.transform.position = startHammer.position;
                this.hammer.GetComponent<HammerMemoyrController>().MoveHammer(this.endHammer, Move);
                InitText();
            }

        }

        public void AddTime()
        {
            if (this.numberResetTime <= 0)
            {
                PopupManager.Instance.OnUI(PopupCode.ShopTimePanel);
                return;
            }
            else
            {
                this.numberResetTime -= 1;
                if (maxTime - this.timer <= 10)
                {
                    this.timer = this.maxTime;

                }
                else
                {
                    this.timer += 10;
                }
                SetTime();
                InitText();
            }

        }

        public void InitText()
        {
            //this.timeText.text = this.timer.ToString();
            this.numberHammerText.text = this.numberHammer.ToString();
            this.numberTimeText.text = this.numberResetTime.ToString();
            this.levelText.text = this.level.ToString();
            //this.coinText.text = UserManager.instance.useData.gold.ToString();
            InitCoinTex();
            if (this.numberHammer <= 0)
            {
                this.numberHammerText.text = "+";
            }
            if (this.numberResetTime <= 0)
            {
                this.numberTimeText.text = "+";
            }

        }
        public void Move()
        {
            this.activeHamer = false;
            this.card._OneClick();
            Debug.Log("Hammer");
        }

        public void StartConfety()
        {
            this.confettiManager.MoveToTarget(this.endCftPos);
        }
        public void EndConfetty()
        {
            this.confettiManager.BackToOriPoss(this.startCftPos);
        }

        public void ExitGamePlay()
        {
            SoundController.instance.AudioButton();
            if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
            {
                if (!AdsManager.instance.IsInterstitialAdReady())
                {
                    LoadToStartGame();

                }
                else
                {
                    AdsManager.instance.ShowInterstitialAd(LoadToStartGame);
                }

            }
            else
            {
                LoadToStartGame();
            }

        }

        public void LoadToStartGame()
        {
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneController.Instance.LoadToSceneStartGame();
        }

        public void InitCoinTex()
        {
            this.coinText.text = UserManager.instance.useData.gold.ToString();
        }
    }
}