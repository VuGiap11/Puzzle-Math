using NTPackage.UI;
using UnityEngine;
using Rubik.LuckyGame;
using DG.Tweening;

namespace Rubik.ClawMachine
{

    public class NoticeAds : PopupUI
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