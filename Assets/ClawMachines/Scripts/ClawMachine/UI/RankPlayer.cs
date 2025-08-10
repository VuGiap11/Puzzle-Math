
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Rubik.BinGo;

namespace Rubik.ClawMachine
{
    public class RankPlayer : MonoBehaviour
    {
        public TextMeshProUGUI sttText;
        public TextMeshProUGUI namePlayer;
        public TextMeshProUGUI pointPlayer;
        //public RankData RankData;
        public Image avatar;

        //public void Init()
        //{
        //    RankData = new RankData
        //    {
        //        Id = UserDataController.instance.dataPlayerController.id,
        //        Name = UserDataController.instance.dataPlayerController.namePlayer,
        //        NumberBaby = UserDataController.instance.dataPlayerController.numberBaby,
        //        IdAvar = UserDataController.instance.dataPlayerController.idAvar,
        //    };
        //}
        public void Init()
        {
            this.namePlayer.text = UserManager.instance.useData.namePlayer;
            this.pointPlayer.text = UserManager.instance.useData.numberBaby.ToString();
            this.avatar.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
            int number = UserManager.instance.useData.numberBaby;
            if (number >= 20)
            {
                this.sttText.text = "100+";
            }
            else if (number >= 10)
            {
                this.sttText.text = "1000+";
            }
            else if (number <= 1)
            {
                this.sttText.text = "10000+";
            }
        }
    }
}