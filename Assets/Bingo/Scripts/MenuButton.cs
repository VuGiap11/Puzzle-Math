using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.BinGo
{
    public class MenuButton : MonoBehaviour
    {
        //public Image imFocus;
        //public void SetFocus(bool focus)
        //{
        //    imFocus.gameObject.SetActive(focus);
        //}
        public GameType gameType;
        public void ChooseGame()
        {
            GameManager.instance.gameType = this.gameType;
            GameManager.instance.StartPlay();
        }
    }
}