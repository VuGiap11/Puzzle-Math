
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ClawMachine
{
    public class BabyOnStockUI : MonoBehaviour
    {
        public Image avarOn;
        public Image avarOff;
        public void Init(Animaldata Animaldata)
        {
            AnimalData animalData = DataAssets.Instance.GetAnimalById(Animaldata.id);
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