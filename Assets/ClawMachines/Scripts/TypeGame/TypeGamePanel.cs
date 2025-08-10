using NTPackage.UI;
using Rubik.ClawMachine;
using UnityEngine;

public class TypeGamePanel : PopupUI
{
    public override void OnUI(object data = null)
    {
        base.OnUI(data);
    }

    public override void OffUI()
    {
        base.OffUI();
    }

    public void LoadToMemoryGame()
    {
        if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
        {
            if (!AdsManager.instance.IsInterstitialAdReady())
            {
                Debug.Log("bbbbbbbbbbbbbbbbbbb");
                MemoryGame();
            }
            else
            {
                AdsManager.instance.ShowInterstitialAd(MemoryGame);

            }

        }
        else
        {
            MemoryGame();
        }

    }
    public void LoadToMergeGame()
    {
        if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
        {
            if (!AdsManager.instance.IsInterstitialAdReady())
            {
                MergeGame();
            }else
            {
                AdsManager.instance.ShowInterstitialAd(MergeGame);
            }

                
        }
        else
        {
            MergeGame();
        }

    }

    public void MergeGame()
    {
        
        SceneController.Instance.LoadToSceneMergeGame();
        this.OffUI();

    }
    public void MemoryGame()
    {
        SceneController.Instance.LoadToSceneMemoryGame();
        this.OffUI();
    }
    public void LoadToBabythreeSweetSagaGame()
    {
        if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
        {

            if (!AdsManager.instance.IsInterstitialAdReady())
            {
                BabythreeSweetSaga();
            }else
            {
                AdsManager.instance.ShowInterstitialAd(BabythreeSweetSaga);
            }
        }
          
        else
        {
            BabythreeSweetSaga();
        }

    }
    public void BabythreeSweetSaga()
    {
        SceneController.Instance.BabythreeSweetSagaGame();
        this.OffUI();
    }
    public void LoadToLuckyGame()
    {
        if (NetworkSettingsOpener.Instance.CheckInternet() && UserManager.instance.useData.numberAds >= 3 && UserManager.instance.useData.isRemoveAds == false)
        {
            if (!AdsManager.instance.IsInterstitialAdReady())
            {
                LuckyGame();
            }else
            {
                AdsManager.instance.ShowInterstitialAd(LuckyGame);
            }
    
        }
        else
        {
            LuckyGame();
        }
    }
    public void LuckyGame()
    {
        SceneController.Instance.LoadToLukyBabyThreeGame();
        this.OffUI();
    }
}
