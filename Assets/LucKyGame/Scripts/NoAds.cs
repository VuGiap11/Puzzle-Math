using DG.Tweening;
using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class NoAds : PopupUI
    {
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            DOVirtual.DelayedCall(2f, () => OffUI());
        }
        public override void OffUI()
        {
            base.OffUI();
        }
    }
}
