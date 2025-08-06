using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Rubik.math

{

    public class GameOverPanel : PopupUI
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            this.scoreText.text = TestManager.instance.Score.ToString();
            this.highScoreText.text = UserManager.instance.useData.highScore.ToString();
        }

        public override void OffUI()
        {
            base.OffUI();

            //SceneController.Instance.LoadToSceneGamePlay();
           // PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            SceneController.Instance.LoadToSceneStartGame();
        }
    }
}
