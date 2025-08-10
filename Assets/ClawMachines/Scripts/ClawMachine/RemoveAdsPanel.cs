using DG.Tweening;
using NTPackage.UI;
using System;
using UnityEngine;

namespace Rubik.ClawMachine
{

    public class RemoveAdsPanel : PopupUI
    {
        [SerializeField] private GameObject noticeObj;


        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            IAP.IAPManager.Instance.InitActionSuccess(Contans.RemoveADS, OnBuySuccess);
            IAP.IAPManager.Instance.InitActionFail(Contans.RemoveADS, OnBuyFail);
        }
        //protected override void Start()
        //{
        //    base.Start();
        //    // IAP.IAPManager.Instance.BuySuccessAct.Add("Remove_Ads", OnBuySuccess);
        //    IAP.IAPManager.Instance.InitActionSuccess("Remove_Ads", OnBuySuccess);
        //    IAP.IAPManager.Instance.InitActionFail("Remove_Ads", OnBuyFail);

        //}
        public void OneClick()
        {
            SoundController.instance.AudioButton();
            IAP.IAPManager.Instance.OnPurchaseButtonClick(Contans.RemoveADS);
        }

        public void OnBuySuccess()
        {
            Debug.Log("Mua thành công");
            // Thực hiện hành động sau khi mua thành công, ví dụ như mở khóa tính năng
            OffUI();
            UserManager.instance.useData.isRemoveAds = true;
            UserManager.instance.useData.gold += 1000;
            UserManager.instance.SaveData();
            HomeController.instance.InitText();
            HomeController.instance.InitButtonRemoveAds();
            AdsManager.instance.RemoveAds();
        }

        public void OnBuyFail()
        {
            this.noticeObj.SetActive(true);
            DOVirtual.DelayedCall(1f, delegate
            {
                this.noticeObj.SetActive(false);
            });
        }
    }

}