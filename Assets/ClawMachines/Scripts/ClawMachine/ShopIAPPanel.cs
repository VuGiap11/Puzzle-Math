using DG.Tweening;
using NTPackage.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace Rubik.ClawMachine
{
    public class ShopIAPPanel : PopupUI
    {
        public List<BuyClaw> lsbuyClaws;
        [SerializeField] private GameObject noticeObj;
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
    
            IAP.IAPManager.Instance.InitActionSuccess(Contans.AdsClaw100, OnBuySuccess);
            IAP.IAPManager.Instance.InitActionFail(Contans.AdsClaw100, OnBuyFail);
            SetData();
        }

        public void SetData()
        {
            if (this.lsbuyClaws.Count <= 0) return;
            for (int i = 0; i < this.lsbuyClaws.Count; i++)
            {
                this.lsbuyClaws[i].Init();
            }
        }

        //protected override void Start()
        //{
        //    base.Start();
        //    IAP.IAPManager.Instance.InitActionSuccess("100Claw", OnBuySuccess);
        //    IAP.IAPManager.Instance.InitActionFail("100Claw", OnBuyFail);
        //}

        public void OneClick()
        {
            SoundController.instance.AudioButton();
            IAP.IAPManager.Instance.OnPurchaseButtonClick(Contans.AdsClaw100);
        }

        public void OnBuySuccess()
        {
            Debug.Log("Mua thành công");
            // Thực hiện hành động sau khi mua thành công, ví dụ như mở khóa tính năng
            //OffUI();
            UserManager.instance.useData.numberCoin += 100;
            UserManager.instance.SaveData();
            HomeController.instance.InitText();
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