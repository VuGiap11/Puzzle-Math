using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rubik.LuckyGame
{
    public class CollectionPanel : PopupUI
    {
        [SerializeField] BoxBabyUI BoxBabyUI;
        [SerializeField] Transform holder;
        public override void OnUI(object data = null)
        {
            base.OnUI();
            SpawnUI();
        }

        private void SpawnUI()
        {
            MyFunction.ClearChild(this.holder);
            for (int i = 0; i < LuckyGameManager.Instance.boxuserData.BabyDatas.Count; i++)
            {
                BoxBabyUI Box = Instantiate(this.BoxBabyUI, holder.position, Quaternion.identity);
                Box.transform.SetParent(holder.transform, false);
                Box.Init(LuckyGameManager.Instance.boxuserData.BabyDatas[i]);

            }
        }
    }

}


