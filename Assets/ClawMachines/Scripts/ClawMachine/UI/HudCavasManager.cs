using NTPackage.UI;
using Rubik.Sort_Challenge.Data.Loading;
using UnityEngine;
using UnityEngine.UI;


namespace Rubik.ClawMachine
{
    public class HudCavasManager : MonoBehaviour
    {
        public static HudCavasManager instance;
        public PopupUI loadingPopUpUi;
        public ItemLoading itemLoading;
        public Image progressBar;
        private void Awake()
        {
            if (instance == null)
            instance = this;
        }
    }
}