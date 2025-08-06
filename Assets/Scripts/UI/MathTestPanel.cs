using NTPackage.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.math
{
    public class MathTestPanel : PopupUI
    {
        public TextMeshProUGUI mathText, ScoreText;
        public Number number;
        public List<GameObject> lsHearts = new List<GameObject>();
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            InitHeart();
            InitScore();
        }

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            InitText();
            InitHeart();
        }

        public void InitText()
        {
            InitScore();
            this.number = GameController.instance.number;
            switch (GameController.instance.operationType)
            {
                case OperationType.Addition:
                    this.mathText.text = this.number.num1.ToString() + "+" + this.number.num2.ToString() + "=?";
                    break;
                case OperationType.Subtraction:
                    this.mathText.text = this.number.num1.ToString() + "-" + this.number.num2.ToString() + "=?";
                    break;
                case OperationType.Multipacation:
                    this.mathText.text = this.number.num1.ToString() + "*" + this.number.num2.ToString() + "=?";
                    break;

                case OperationType.Division:
                    this.mathText.text = this.number.num1.ToString() + ":" + this.number.num2.ToString() + "=?";
                    break;
            }
        }

        public void InitScore()
        {
            this.ScoreText.text = TestManager.instance.Score.ToString();
        }
        public void InitHeart()
        {
            for (int i = 0; i < this.lsHearts.Count; i++)
            {
                Debug.Log(this.lsHearts[i] + ":" + i, this.lsHearts[i]);
                this.lsHearts[i].SetActive(i < TestManager.instance.numberheart);
            }

        }
    }
}