using NTPackage.UI;
using Rubik.LuckyGame;
using UnityEngine;

public class BtnOnPopUpAdsPanel : MonoBehaviour
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
