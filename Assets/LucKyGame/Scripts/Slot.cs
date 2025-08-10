using Rubik.ClawMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.LuckyGame
{

    public class Slot : MonoBehaviour
    {
        public Image avar;
        public string id;
        public void Init(string id)
        {
            this.id = id;
            BabyThreeData BabyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(id);
            this.avar.sprite = BabyThreeData.Avatar;
        }
    }

}