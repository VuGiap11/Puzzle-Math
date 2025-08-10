
using DG.Tweening;
using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class GiftPanel : PopupUI
    {
        public IteamGift iteamGift;
        public Transform holder;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            notice.SetActive(false);
            SpwanIteamGift();
        }
        public void SpwanIteamGift()
        {
            MyFunction.ClearChild(this.holder);
            if (ClawGameManager.Instance.ClowController.LsIteams.Count <= 0) return;
            for (int i = 0; i < ClawGameManager.Instance.ClowController.LsIteams.Count; i++)
            {
                if (ClawGameManager.Instance.ClowController.LsIteams[i].typeIteam == TypeIteam.coin) continue;
                IteamGift iteam = Instantiate(this.iteamGift, this.holder);
                iteam.transform.SetParent(this.holder, false);
                iteam.transform.localScale = Vector3.one;
                iteam.Init(ClawGameManager.Instance.ClowController.LsIteams[i]);
            }
        }

        public void ClosePanel()
        {
            SoundController.instance.AudioButton();
            this.OffUI();
            SceneController.Instance.statusGame = StatusGame.StartGame;
            ClawGameManager.Instance.ClowController.MoveIteam(1, ResetBabyOnClawing);

        }

        public void Ads()
        {
            SoundController.instance.AudioButton();
            this.OffUI();
            SceneController.Instance.statusGame = StatusGame.StartGame;
            ClawGameManager.Instance.ClowController.MoveIteam(2, ResetBabyOnClawing);
        }

        public GameObject notice;
        public void DoubleAds()
        {
            if (NetworkSettingsOpener.Instance.CheckInternet())
            {
                if (UserManager.instance.useData.numberAds >= 3)
                {
                    if (!AdsManager.instance.IsRewardAdReady())
                    {
                        PopupManager.Instance.OnUI(PopupCode.NoAds);
                    }
                    else
                    {
                        AdsManager.instance.ShowRewardedAd(Ads);
                    }

                }
                else
                {
                    if (!AdsManager.instance.IsRewardedInterstitialAdReady())
                    {
                        AdsManager.instance.ShowRewardedAd(Ads);
                    }
                    else
                    {
                        AdsManager.instance.ShowRewardedInterstitialAd(Ads);
                    }

                }


            }
            else
            {
                notice.SetActive(true);
                DOVirtual.DelayedCall(0.2f, () => notice.SetActive(false));
            }
        }
        public void ResetBabyOnClawing()
        {
            if (ClawGameManager.Instance.number >= ClawGameManager.Instance.numberCandyandBaby)
            {
                ClawGameManager.Instance.ResetBabyAfterWin();
            }
        }
    }
}