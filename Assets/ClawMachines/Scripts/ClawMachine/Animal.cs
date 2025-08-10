
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class Animal : MonoBehaviour
    {
        public SpriteRenderer avatar;
        public AnimalData animalData;
        public void Init(AnimalData animalData)
        {
            this.animalData = animalData;
            avatar.sprite = this.animalData.Avatar;
        }
    }
}