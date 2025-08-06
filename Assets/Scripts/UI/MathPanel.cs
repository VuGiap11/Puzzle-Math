using NTPackage.UI;
using TMPro;
using UnityEngine;

namespace Rubik.math
{

    public class MathPanel : PopupUI
    {
        public TextMeshProUGUI mathText, goldText;
        public Number number;
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            InitGold();
        }

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            InitText();
        }

        public void InitText()
        {
            InitGold();
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

        public void InitGold()
        {
            this.goldText.text = UserManager.instance.useData.gold.ToString();
        }
    }
}