using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class Baby : MonoBehaviour
    {
        public Image avar;
        public void Init(string id)
        {
            BabyData babyData = DataAssets.instance.GetBabyDatabyID(id);
            this.avar.sprite = babyData.Avatar;
        }
    }
}

