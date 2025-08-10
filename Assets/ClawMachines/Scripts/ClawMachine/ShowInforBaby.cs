using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class ShowInforBaby : MonoBehaviour
    {
        public TextMeshProUGUI numberBabyText;
        public Image avar;
        public void Init(Animaldata Animaldata)
        {
            numberBabyText.text = Animaldata.number.ToString();
            AnimalData animal = DataAssets.Instance.GetAnimalById(Animaldata.id);
            avar.sprite = animal.Avatar;
        }
    }
}