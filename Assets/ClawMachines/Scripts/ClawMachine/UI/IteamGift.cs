
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class IteamGift : MonoBehaviour
    {
        public Image avartar;

        public void Init(Iteam Iteam)
        {
            if (Iteam.typeIteam == TypeIteam.animal)
            {
                this.avartar.sprite = Iteam.GetComponent<Animal>().animalData.Avatar;
            }
            else if (Iteam.typeIteam == TypeIteam.candy)
            {
                this.avartar.sprite = Iteam.GetComponent<Candy>().CandyData.Avatar;
            }
        }
    }
}