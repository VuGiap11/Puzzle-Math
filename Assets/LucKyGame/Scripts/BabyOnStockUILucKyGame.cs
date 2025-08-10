using Rubik.ClawMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.LuckyGame
{

    public class BabyOnStockUILucKyGame : MonoBehaviour
    {
        public Image avarOn;
        public Image avarOff;
        public void Init(BabyDataLucky Animaldata)
        {
            BabyThreeData animalData = DataAssets.Instance.GetBabyThreeDatabyID(Animaldata.id);
            this.avarOff.sprite = animalData.Avatar;
            this.avarOn.sprite = animalData.Avatar;
            if (Animaldata.isDone)
            {
                avarOn.gameObject.SetActive(true);
                avarOff.gameObject.SetActive(false);
            }
            else
            {
                avarOn.gameObject.SetActive(false);
                avarOff.gameObject.SetActive(true);
            }
        }
    }

}