using NTPackage.UI;
using TMPro;
using UnityEngine;
using UnityEngineInternal;

namespace Rubik.math
{
    public class ShopPanel : PopupUI
    {
        public BabyShop babyShop;
        [SerializeField] private Transform holder;
        [SerializeField] private TextMeshProUGUI goldText;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SpawnBaby();
        }

        public void SpawnBaby()
        {
            MyFunction.ClearChild(this.holder);
            for (int i = 0; i < UserManager.instance.Animaldatas.animaldatas.Count; i++)
            {
                BabyShop babyShop = Instantiate(this.babyShop, holder);
                babyShop.transform.SetParent(this.holder, false);
                babyShop.Init(UserManager.instance.Animaldatas.animaldatas[i]);

            }
        }

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            this.goldText.text = UserManager.instance.useData.gold.ToString();
        }

    }
}