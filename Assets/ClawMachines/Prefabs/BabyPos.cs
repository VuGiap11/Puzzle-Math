
using UnityEngine;
//using UnityEngine.Rendering.UI;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class BabyPos : MonoBehaviour
    {
        public BabyThreeUI babyThreeUI;
        public Image imagePanel;
        public void Init()
        {
            if (babyThreeUI != null) 
            {
                imagePanel.GetComponent<Image>().enabled = true;
            }
            else
            {
                imagePanel.GetComponent<Image>().enabled = false;
            }
        }

        public void CheckPosition()
        {
            if (imagePanel.GetComponent<Image>().enabled == true)
            {
                return;
            }
            imagePanel.GetComponent<Image>().enabled = true;
        }
    }
}