using NTPackage.UI;
using Rubik.math;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.math
{
    public class SubtractionManager : MonoBehaviour
    {
  
        public static SubtractionManager instance;
        public Number number;
        [SerializeField] private Transform holderLeft, holderRight;
        [SerializeField] private Baby babypre;
        [SerializeField] private TextMeshProUGUI leftText, righText;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            PopupManager.Instance.OnUI(PopupCode.AnswerPanel);
            PopupManager.Instance.OnUI(PopupCode.MathPanel);
            SpawnBaby(this.number);
            this.leftText.text = number.num1.ToString();
            this.righText.text = number.num2.ToString();
        }
        public void SetQuestion()
        {
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            SpawnBaby(this.number);
            PopupManager.Instance.UpdateDataUI(PopupCode.AnswerPanel);
            PopupManager.Instance.UpdateDataUI(PopupCode.MathPanel);
        }

        public void SpawnBaby(Number number)
        {
            MyFunction.ClearChild(this.holderLeft);
            MyFunction.ClearChild(this.holderRight);
            for (int i = 0; i < number.num1; i++)
            {
                Baby baby = Instantiate(babypre);
                baby.transform.localScale = Vector3.one;
                baby.transform.SetParent(holderLeft, false);
                baby.Init(UserManager.instance.IdAvar);
            }

            for (int i = 0; i < number.num2; i++)
            {
                Baby baby = Instantiate(babypre);
                baby.transform.localScale = Vector3.one;
                baby.transform.SetParent(holderRight, false);
                baby.Init(UserManager.instance.IdAvar);
            }
            this.leftText.text = number.num1.ToString();
            this.righText.text = number.num2.ToString();
        }
        public void BackToStartScene()
        {
            SceneController.Instance.LoadToSceneStartGame();
        }
    }
}