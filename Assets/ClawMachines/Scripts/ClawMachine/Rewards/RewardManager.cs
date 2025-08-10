using NTPackage;
using Rubik.ClawMachine;
using System;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.UI;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    //public TimeSpan timeLeft;
    public bool canReceived;
    DateTime firstLaunchTime;
    public bool isStartGame;

    public int IndexDay;
    public string LastDay;
    public double timeLeft;

    public NTDictionary<string, Action> Notification;
    //public NTDictionary<string, Action> TimeNotification;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        //if (!canReceived)
        //{
        //    CheckRewardAvailability();
        //}
        //else
        //{
        //    return;
        //}

    }

    public void LoadData()
    {
        this.LastDay = PlayerPrefs.GetString(Contans.LastRewartTime, "");
        this.IndexDay = PlayerPrefs.GetInt(Contans.NumberIndexDay, 0);
        this.Notification = new NTDictionary<string, Action>();
        //this.TimeNotification = new NTDictionary<string, Action>();
        this.NewDay();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(Contans.LastRewartTime, this.LastDay);
        PlayerPrefs.SetInt(Contans.NumberIndexDay, this.IndexDay);
    }

    [ContextMenu("IsNewDay")]
    public bool IsNewDay()
    {
        string now = this.ParseTimeTo_yyyymmdd(DateTime.Now);
        bool isNew = this.LastDay != now;
        Debug.Log(isNew);
        return isNew;
    }

    public bool AvailableClaim(int day)
    {
        if (!this.IsNewDay()) return false;
        if (day == this.IndexDay + 1) return true;
        return false;
    }

    public bool IsClaimed(RewardData rewardData)
    {
        return rewardData.day <= this.IndexDay;
    }

    //public void CheckRewardAvailability()
    //{
    //if (PlayerPrefs.HasKey(Contans.LastRewartTime))
    //{
    //    //isStartGame = false;
    //    string lastRewardTimeString = PlayerPrefs.GetString(Contans.LastRewartTime);
    //    DateTime lastRewardTime = DateTime.Parse(lastRewardTimeString);
    //    DateTime currentDate = DateTime.Now.Date;
    //    DateTime lastRewardDate = lastRewardTime.Date;
    //    int daysElapsed = (currentDate - lastRewardDate).Days;
    //    if (daysElapsed < 0)
    //    {
    //        return;
    //    }
    //    else if (daysElapsed >= 1)
    //    {
    //        this.timeLeft = TimeSpan.Zero;
    //        canReceived = true;
    //    }
    //    else
    //    {
    //        DateTime nextRewardTime = lastRewardTime.Date.AddDays(1).AddHours(0); // Reset vào 00:00 hôm sau
    //        this.timeLeft = nextRewardTime - DateTime.Now;
    //        canReceived = false;
    //    }
    //}
    //else
    //{
    //    isStartGame = true;
    //    string firstLaunchTimeString = PlayerPrefs.GetString(Contans.FirstRewartTime);
    //    if (firstLaunchTimeString == null || firstLaunchTimeString == "")
    //    {
    //        string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
    //        PlayerPrefs.SetString(Contans.FirstRewartTime, currentTime);
    //        firstLaunchTimeString = currentTime;
    //        this.timeLeft = TimeSpan.Zero;
    //        canReceived = true;
    //    }
    //    Debug.Log(firstLaunchTimeString);
    //    try
    //    {
    //        firstLaunchTime = DateTime.Parse(firstLaunchTimeString);
    //        Debug.Log("Parsed date: " + firstLaunchTime);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError(e);
    //    }
    //    int timeSinceFirstLaunch = (DateTime.Now.Date - firstLaunchTime).Days;
    //    Debug.Log("timeSinceFirstLaunch" + timeSinceFirstLaunch);
    //    //}
    //}

    //claim
    [ContextMenu("Claim")]
    public void Claim()
    {
        if (!IsNewDay()) return;
        SoundController.instance.AudioReward();
        //Re
        // (nhaan thuuong)

        //Save
        //
        if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.isRemoveAds == false && UserManager.instance.useData.numberAds >= 3)
        {
            if (!AdsManager.instance.IsRewardAdReady())
            {
                SetClaim();
            }
            else
            {
                AdsManager.instance.ShowRewardedAd(SetClaim);
            }
        }
        else
        {
            SetClaim();
        }
    }
    public void SetClaim()
    {
        this.IndexDay++;
        UserManager.instance.useData.numberAds++;
        UserManager.instance.SaveData();
        if (this.IndexDay >= 30)
        {
            this.IndexDay = 0;
        }
        this.LastDay = this.ParseTimeTo_yyyymmdd(DateTime.Now);
        this.SaveData();
        UserManager.instance.SaveData();
        this.NewDay();
    }
    public string ParseTimeToDay()
    {
        int hours = Mathf.FloorToInt((float)timeLeft / 3600);
        int minutes = Mathf.FloorToInt(((float)timeLeft % 3600) / 60);
        int seconds = Mathf.FloorToInt((float)timeLeft % 60);

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
    public string GetTimeUntilNextReward()
    {
        if (PlayerPrefs.HasKey(Contans.LastRewartTime))
        {
            string lastRewardTimeString = PlayerPrefs.GetString(Contans.LastRewartTime);
            DateTime lastRewardTime = DateTime.Parse(lastRewardTimeString);
            DateTime nextRewardTime = lastRewardTime.Date.AddDays(1).AddHours(0); // Reset vào 00:00 hôm sau
            TimeSpan timeLeft = nextRewardTime - DateTime.Now;

            if (timeLeft > TimeSpan.Zero)
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);
            }
        }
        return "00:00:00";
    }

    public Coroutine CorCountTime;
    public IEnumerator CountTime()
    {
        DateTime nextRewardTime = DateTime.Now.Date.AddDays(1).AddHours(0);
        TimeSpan timeLeft = nextRewardTime - DateTime.Now;
        this.timeLeft = timeLeft.TotalSeconds;
        while (true)
        {
            yield return new WaitForSeconds(1);
            this.timeLeft--;
            //foreach (Action item in this.TimeNotification.ToList())
            //{
            //    item?.Invoke();
            //}
            if (this.timeLeft < 0) break;
        }
        NewDay();
    }

    public void NewDay()
    {
        Debug.Log("New Day");
        if (CorCountTime != null) StopCoroutine(CorCountTime);
        CorCountTime = StartCoroutine(CountTime());
        foreach (Action item in this.Notification.ToList())
        {
            item?.Invoke();
        }

    }

    public string ParseTimeTo_yyyymmdd(DateTime time)
    {
        int year = time.Year;
        int month = time.Month;
        int day = time.Day;
        return year + "" + (month < 10 ? "0" + month : month) + "" + (day < 10 ? "0" + day : day);
    }
}
