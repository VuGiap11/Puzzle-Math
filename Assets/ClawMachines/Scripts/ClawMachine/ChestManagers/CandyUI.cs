
using UnityEngine;
using UnityEngine.UI;


namespace Rubik.ClawMachine
{
    public class CandyUI : MonoBehaviour
    {
        public Image avatar;
        public void Init(AnimalData AnimalData)
        {
            avatar.sprite = AnimalData.Avatar;
        }
    }
}