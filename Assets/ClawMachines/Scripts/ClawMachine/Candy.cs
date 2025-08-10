using UnityEngine;
namespace Rubik.ClawMachine
{
    public class Candy : MonoBehaviour
    {
        public CandyData CandyData;
        public SpriteRenderer avatar;
        public void Init(CandyData CandyData)
        {
            this.CandyData = CandyData;
            avatar.sprite = this.CandyData.Avatar;
        }
    }
}
