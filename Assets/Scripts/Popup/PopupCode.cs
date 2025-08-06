using System;
using UnityEngine;

namespace NTPackage.UI
{
    public class PopupCodeParser
    {
        public static PopupCode FromString(string name)
        {
            //name = name.ToLower();W
            return (PopupCode)Enum.Parse(typeof(PopupCode), name);
        }
    }

    [System.Serializable]
    public enum PopupCode
    {
        Unknown = 0,
        LoadingBgUI,
        NameChangeUI,
        AvatarChangeUI,
        SettingPanel,
        RewardPanel,
        RankPanel,
        BabyPanel,
        ShopPanel,
        ResultPanel,
        BonousPanel,
        LoadingPanel,
        BabyOpenedPanel,
        ResultIAP,
        ShopIAPPanel,
        NoticePanel,
        RemoveAdsPanel,
        SelectTypePanel,
        PracticePanel,
        TestPanel,
        LoadingUI,
        HighScorePanel,
        AnswerPanel,
        MathPanel,
        MathTestPanel,
        WinPanel,
    }
}
