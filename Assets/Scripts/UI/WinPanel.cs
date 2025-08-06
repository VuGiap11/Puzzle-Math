using NTPackage.UI;
using TMPro;
using UnityEngine;


namespace Rubik.math
{
    public class WinPanel : PopupUI
    {
        public TextMeshProUGUI scoreText, highScoreText;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            Init();
        } 
        public void Init()
        {
            this.scoreText.text = TestManager.instance.Score.ToString();
            this.highScoreText.text = UserManager.instance.useData.highScore.ToString();
        }

        public override void OffUI()
        {
            base.OffUI();
           SceneController.Instance.LoadToSceneStartGame();
        }
    }
}