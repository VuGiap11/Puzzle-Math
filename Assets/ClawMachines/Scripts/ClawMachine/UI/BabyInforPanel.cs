
using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class BabyInforPanel : PopupUI
    {
        [SerializeField] Transform holder;
        [SerializeField] BabyOnStockUI baybyUiOnStockPre;
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SpawnBabyUiOnStock();
        }
        private void SpawnBabyUiOnStock()
        {
            MyFunction.ClearChild(this.holder);
            for (int i = 0; i < UserManager.instance.Animaldatas.animaldatas.Count; i++)
            {
                BabyOnStockUI a = Instantiate(this.baybyUiOnStockPre, this.holder);
                a.gameObject.transform.SetParent(this.holder);
                a.Init(UserManager.instance.Animaldatas.animaldatas[i]);
            }

        }  
    }
}