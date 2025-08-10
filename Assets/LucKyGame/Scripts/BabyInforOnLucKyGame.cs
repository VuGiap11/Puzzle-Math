using UnityEngine;
using Rubik.LuckyGame;
using NTPackage.UI;
using NUnit.Framework;
using System.Collections.Generic;
namespace Rubik.ClawMachine
{
    public class BabyInforOnLucKyGame : PopupUI
    {
        [SerializeField] Transform holder;
        [SerializeField] BabyOnStockUILucKyGame baybyUiOnStockPre;
        public List<BabyDataLucky> lsBabyDataLucKy = new List<BabyDataLucky>();
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SpawnBabyUiOnStock();
        }
        public void AddList()
        {
            lsBabyDataLucKy = new List<BabyDataLucky>();
            for (int i = 0; i < UserManager.instance.BoxUserDatas.data.Count; i++) 
            {
                for (int j = 0; j < UserManager.instance.BoxUserDatas.data[i].BabyDatas.Count; j++)
                {
                    lsBabyDataLucKy.Add(UserManager.instance.BoxUserDatas.data[i].BabyDatas[j]);
                }
            }
        }
        private void SpawnBabyUiOnStock()
        {
            MyFunction.ClearChild(this.holder);
            AddList();
            for (int i = 0; i < lsBabyDataLucKy.Count; i++)
            {
                BabyOnStockUILucKyGame a = Instantiate(this.baybyUiOnStockPre, this.holder);
                a.gameObject.transform.SetParent(this.holder);
                a.Init(lsBabyDataLucKy[i]);
            }

        }
    }
}