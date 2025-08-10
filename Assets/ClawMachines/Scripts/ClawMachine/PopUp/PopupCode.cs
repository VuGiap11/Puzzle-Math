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
        LoadingUI,
        NameChangeUI,
        AvatarChangeUI,
        ChangeSkinPlayerUI,
        ChatUI,
        Emoji_Popup,
        FriendUI,
        SettingPanel,
        RewardPanel,
        RankPanel,
        BabyPanel,
        CandyPanel,
        ShopPanel,
        AdsPanel,
        GiftPanel,
        BabyInfor,
        RemoveAdsPanel,
        ShopIAPPanel,
        ResultIAP,
        LevelComleted,
        WinLosePanel,
        ShopHammerPanel,
        ShopTimePanel,
        TypeGamePanel,
        WinLoseMergePanel,
        WinLosePanelSweet,
        ShopHurtPanel,
        ShopShufflePanel,
        CollectionPanel,
        HopePanel,
        BabyLucKyPanel,
        BabyDonePanel,
        BabyInforOnLucKyGame,
        AdsPanelGold,
        BonousPanel,
        Tutorial,
        NoticeAds,
        NoAds,
    }
}
