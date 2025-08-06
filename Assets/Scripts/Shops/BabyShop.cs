using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class BabyShop : MonoBehaviour
    {
        public Animaldata animaldata;
        public Image avartar;
        public GameObject panelPrice;
        public TextMeshProUGUI priceText;

        public void Init(Animaldata animaldata)
        {
            this.animaldata = animaldata;
            BabyData babyData = DataAssets.instance.GetBabyDatabyID(this.animaldata.id);
            this.avartar.sprite = babyData.Avatar;
            this.priceText.text = babyData.Price.ToString();
            if (this.animaldata.isDone)
            {
                panelPrice.SetActive(false);
            }else
            {
                panelPrice.SetActive(true);
            }
        }

        public void OneClick()
        {
            SoundController.instance.AudioButton();
            BabyData babyData = DataAssets.instance.GetBabyDatabyID(this.animaldata.id);
            if (babyData.Price > UserManager.instance.useData.gold)
            {
                return;
            }else
            {
                UserManager.instance.useData.gold -= babyData.Price;
                this.animaldata.isDone = true;
                PopupManager.Instance.UpdateDataUI(PopupCode.ShopPanel);
                panelPrice.SetActive(false);
                UserManager.instance.useData.babyBoughts.Add(this.animaldata.id);
                UserManager.instance.SaveData();
                HomeManager.instance.Init();
            }
        }
    }
}