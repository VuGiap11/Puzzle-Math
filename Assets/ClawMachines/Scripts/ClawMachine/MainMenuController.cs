using DG.Tweening;
using Rubik.ClawMachine;
using Rubik.Sort_Challenge.Data.Loading;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    [SerializeField] ItemLoading itemLoading;
    [SerializeField] private Image levelBarSprite;
    public float fillTime = 10f;
    public TextMeshProUGUI percentText;
    private float targetFill = 1f;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        LoadData();
        itemLoading.StartAnimLoading();
        if (loading != null)
        {
            StopCoroutine(loading);
        }
        loading = StartCoroutine(FillBarSmoothly());
    }
    Coroutine loading;
    private IEnumerator FillBarSmoothly()
    {
        //float startFill = levelBarSprite.fillAmount;
        float startFill = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            levelBarSprite.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / fillTime);
            UpdatePercentageDisplay(levelBarSprite.fillAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelBarSprite.fillAmount = targetFill;
        UpdatePercentageDisplay(targetFill);
        //itemLoading.StopAnimLoading();
        DOTween.KillAll();
        LoadToSceneStartScene();
    }
    private void UpdatePercentageDisplay(float fillAmount)
    {
        if (percentText != null)
        {
            percentText.text = Mathf.RoundToInt(fillAmount * 100) + "%";
        }
    }

    private void LoadToSceneStartScene()
    {
        if (loading != null)
        {
            StopCoroutine(loading);
        }
        itemLoading.StopAnimLoading();
        SceneController.Instance.LoadToHomeScene();
    }
    public void LoadData()
    {
        DataAssets.Instance.LoadData();
        UserManager.instance.LoadData();
        RankDataManager.instance.LoadRank();
        RewardManager.Instance.LoadData();
        AdsManager.instance.Init();
    }
}