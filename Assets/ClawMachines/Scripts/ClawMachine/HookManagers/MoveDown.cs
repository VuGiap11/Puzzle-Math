using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class MoveDown : MonoBehaviour
    {
        [SerializeField] private GameObject btnOn;
        [SerializeField] private GameObject btnOff;
        public void _MoveDown()
        {
            ClawGameManager.Instance.ClowController.MoveDown();
            btnOn.SetActive(false);
            btnOff.SetActive(true);
        }
        private void OnMouseDown()
        {
           
            //if (UserManager.instance.canIsClaw) return;

            //if (ClawGameManager.Instance.canClaw)
            //{

            //    return;
            //}
            //if (ClawGameManager.Instance.canClaw == false)
            //{
            //    ClawGameManager.Instance.canClaw = true;
            //}
            if (SceneController.Instance.statusGame != StatusGame.StartGame) return;
            AdsPanel adsPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.AdsPanel) as AdsPanel;
            if (adsPanel.isOpen) return;
            SoundController.instance.AudioButton();
            if (UserManager.instance.useData.numberCoin <=0)
            {
                //ClawGameManager.Instance.panelAds.SetActive(true);
                PopupManager.Instance.OnUI(PopupCode.AdsPanel);
                return;
            }
            UserManager.instance.useData.numberCoin -= 1;
            ClawGameManager.Instance.InitGold(UserManager.instance.useData.numberCoin);
            UserManager.instance.SaveData();
            _MoveDown();
            SceneController.Instance.statusGame = StatusGame.Clawing;

        }
        public void DoneMove()
        {
            Debug.Log("donebuutton");
            btnOn.SetActive(true);
            btnOff.SetActive(false);
           // ClawGameManager.Instance.statusGame = StatusGame.StartGame;
        }
    }
}
