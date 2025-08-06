using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.math
{
    public class MultipacationManager : MonoBehaviour
    {
        public static MultipacationManager instance;
        [SerializeField] private TextMeshProUGUI mathText;
        public List<MultipacationAnswer> lsmultipacationAnswer = new List<MultipacationAnswer>();
        public Number number;
        public List<Answer> answers = new List<Answer>();

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        private void Start()
        {
            StartGame();
        }
        public void StartGame()
        {

            // this.number = UserManager.instance.GenerateRandomSumExpression();
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            this.mathText.text = number.num1.ToString() + "*" + number.num2.ToString() + "=?";
            if (this.lsmultipacationAnswer.Count <= 0) return;
            for (int i = 0; i < this.lsmultipacationAnswer.Count; i++)
            {
                this.lsmultipacationAnswer[i].ClearBaby();
                //this.lsmultipacationAnswer[i].SpawnBaby(this.number.num2);

            }
            //for (int i = 0; i < this.number.num2; i++)
            //{
            //   // this.lsmultipacationAnswer[i].ClearBaby();
            //    this.lsmultipacationAnswer[i].SpawnBaby(this.number.num1);

            //}
            SpawnBaby();
            // SpawnAnswer();
            PopupManager.Instance.OnUI(PopupCode.AnswerPanel);
            PopupManager.Instance.OnUI(PopupCode.MathPanel);
        }
        Coroutine spawnBaby;
        public void SpawnBaby()
        {
            if (this.spawnBaby != null)
            {
                StopCoroutine(Spawn());
            }
            this.spawnBaby = StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            for (int i = 0; i < this.number.num2; i++)
            {
                this.lsmultipacationAnswer[i].SpawnBaby(this.number.num1);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void SpawnAnswer()
        {
            List<int> numbers = GameController.instance.SpawnNumbers(number.result);

            if (numbers.Count <= 0 || numbers.Count != this.answers.Count) return;
            for (int i = 0; i < this.answers.Count; i++)
            {
                //this.answers[i].numberAnswer = numbers[i];
                Debug.Log("number" + numbers[i]);
                this.answers[i].Init(numbers[i], UserManager.instance.IdAvar);
            }
        }

        public void SetQuestion()
        {
            this.number = GameController.instance.GenerateRandomSumExpression();
            UserManager.instance.RandomIdAvar();
            this.mathText.text = number.num1.ToString() + "*" + number.num2.ToString() + "=?";
            if (this.lsmultipacationAnswer.Count <= 0) return;
            for (int i = 0; i < this.lsmultipacationAnswer.Count; i++)
            {
                this.lsmultipacationAnswer[i].ClearBaby();
                //this.lsmultipacationAnswer[i].SpawnBaby(this.number.num2);

            }
            //for (int i = 0; i < this.number.num2; i++)
            //{
            //   // this.lsmultipacationAnswer[i].ClearBaby();
            //    this.lsmultipacationAnswer[i].SpawnBaby(this.number.num1);

            //}
            SpawnBaby();
            // SpawnAnswer();
            PopupManager.Instance.UpdateDataUI(PopupCode.AnswerPanel);
            PopupManager.Instance.UpdateDataUI(PopupCode.MathPanel);
        }
        public void BackToStartScene()
        {
            SceneController.Instance.LoadToSceneStartGame();
        }
    }
}