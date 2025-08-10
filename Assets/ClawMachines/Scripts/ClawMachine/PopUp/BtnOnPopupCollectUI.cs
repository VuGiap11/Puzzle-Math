using Rubik.LuckyGame;
using UnityEngine;
namespace NTPackage.UI
{

    public class BtnOnPopupCollectUI : MonoBehaviour
    {
        public PopupCode PopupCode;
        public void _OneClick()
        {
            if (LuckyGameManager.Instance.isSpinning)
            {
                return;
            }
            PopupManager.Instance.OnUI(PopupCode);

        }
    }
}