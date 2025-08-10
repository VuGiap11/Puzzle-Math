
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ButtonPress : MonoBehaviour
    {
        public GameObject icoinOn;

        public GameObject icoinOff;
        public void OneClick()
        {
            //SoundController.instance.PressButtonAudio();
            //if (ClawGameManager.Instance.statusGame == StatusGame.Clawing) return;
            //if (ClawGameManager.Instance.canClaw) return;
            //ClawGameManager.Instance.PresButtonClose();

        }

        public void DonePress()
        {
            icoinOff.SetActive(false);
            icoinOn.SetActive(true);
        }

        public void Press()
        {
            icoinOff.SetActive(true);
            icoinOn.SetActive(false);
        }
    }
}