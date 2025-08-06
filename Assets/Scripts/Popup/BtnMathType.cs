using NTPackage.UI;
using System.ComponentModel.Design;
using UnityEngine;

namespace Rubik.math
{
    public class BtnMathType : MonoBehaviour
    {
        public MathType MathType;
        public void _OneClick()
        {
            SoundController.instance.AudioButton();
            GameController.instance.mathType = this.MathType;
           //UserManager.instance.SetType();
            GameController.instance.SetType();
            SceneController.Instance.LoadToSceneGamePlay();
            PopupManager.Instance.OffUI(PopupCode.SelectTypePanel);
            //if (UserManager.instance.mathType != MathType.Test)
            //{
            //    PopupManager.Instance.OnUI(PopupCode.PracticePanel);
            //    PopupManager.Instance.OffUI(PopupCode.TestPanel);
            //}
            //else
            //{
            //    PopupManager.Instance.OffUI(PopupCode.PracticePanel);
            //    PopupManager.Instance.OnUI(PopupCode.TestPanel);
            //}
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
        
        }
    }
}

