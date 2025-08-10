using NTPackage.UI;
using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
namespace Rubik.ClawMachine
{
    [Serializable]
    public enum ProductTypeEnum
    {
        Coin100,
        RemoveAds,
        // thêm lo?i khác n?u có
    }
    public class IAPManager : MonoBehaviour
    {
        public static IAPManager Instance;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public static ProductTypeEnum FromString(string name)
        {
            //name = name.ToLower();W
            return (ProductTypeEnum)Enum.Parse(typeof(ProductTypeEnum), name);
        }
        public void OnbuySuccess(Product productinfor)
        {

            SoundController.instance.AudioButton();
            switch (productinfor.definition.id)
            {
                case "100_Claw":
                    Debug.Log("buy 100 claw success");
                    UserManager.instance.useData.numberCoin += 100;
                    UserManager.instance.SaveData();
                    HomeController.instance.InitText();
                    //PopupManager.Instance.OffUI(PopupCode.ShopIAPPanel);
                    break;
                case "Remove_Ads":

                    Debug.Log("removeADs");
                    UserManager.instance.useData.isRemoveAds = true;
                    UserManager.instance.useData.gold += 1000;
                    UserManager.instance.SaveData();
                    PopupManager.Instance.OffUI(PopupCode.RemoveAdsPanel);
                    HomeController.instance.InitText();
                    HomeController.instance.InitButtonRemoveAds();
                    break;
            }
            Debug.Log(productinfor.ToString());
        }

        public void OnbuyFail(Product productinfor, PurchaseFailureDescription reason)
        {
            switch (productinfor.definition.id)
            {
                case "100_Claw":
                    PopupManager.Instance.OffUI(PopupCode.ShopIAPPanel);
                    break;
                case "Remove_Ads":

                    PopupManager.Instance.OffUI(PopupCode.RemoveAdsPanel);
                    break;
            }
        }
    }
}