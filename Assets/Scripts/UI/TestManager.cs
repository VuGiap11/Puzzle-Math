using DG.Tweening;
using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Rubik.math
{
    public class TestManager : MonoBehaviour
    {
        public static TestManager instance;
        public int numberheart = 5;
        public int Score = 0;
        public List<Answer> answers = new List<Answer>();
        [SerializeField] private Image timeBarSprite;
        public bool isWin = false;
        public float fillTime = 15f;
        private float targetFill = 1f;
        // public Number number;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            StartTest();
            PopupManager.Instance.OnUI(PopupCode.MathTestPanel);

        }

        public void StartTest()
        {
            this.isWin = false;
            GameController.instance.isAnswer = false;
            GameController.instance.SetType();
            GameController.instance.GenerateRandomSumExpression();
            this.numberheart = 5;
            this.Score = 0;
            SpawnAnswer();
            LoadingTime();

        }

        public void SetQuestion()
        {
            GameController.instance.SetType();
            GameController.instance.GenerateRandomSumExpression();
            PopupManager.Instance.UpdateDataUI(PopupCode.MathTestPanel);
            SpawnAnswer();
            LoadingTime();

        }
        public void AnswerDefeat()
        {
            this.numberheart -= 1;
            if (this.numberheart <= 0)
            {
                this.isWin = true;
                PopupManager.Instance.OnUI(PopupCode.WinPanel);
            }
            else
            {
                PopupManager.Instance.UpdateDataUI(PopupCode.MathTestPanel);
            }
        }

        public void SpawnAnswer()
        {
            List<int> numbers = GameController.instance.SpawnNumbers(GameController.instance.number.result);

            if (numbers.Count <= 0 || numbers.Count != this.answers.Count) return;
            for (int i = 0; i < this.answers.Count; i++)
            {
                //this.answers[i].numberAnswer = numbers[i];
                Debug.Log("number" + numbers[i]);
                this.answers[i].Init(numbers[i], UserManager.instance.IdAvar);
            }
        }
        public void SetResult()
        {
            Debug.Log("count" + this.answers.Count);
            for (int i = 0; i < this.answers.Count; i++)
            {
                if (this.answers[i].numberAnswer == GameController.instance.number.result)
                {
                    this.answers[i].SetTrue();
                    Debug.Log("trrueeeeeee");
                }
                else
                {

                    Debug.Log("falesssssssssssss");
                }
            }
        }

        public void LoadingTime()
        {
            if (this.loadingTime != null)
            {
                StopCoroutine(this.loadingTime);
            }
            this.loadingTime = StartCoroutine(fillBarTime());
        }


        public void StopTime()
        {
            if (this.loadingTime != null)
            {
                StopCoroutine(this.loadingTime);
            }
        }
        Coroutine loadingTime;
        private IEnumerator fillBarTime()
        {
            float startFill = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < fillTime)
            {
                this.timeBarSprite.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / fillTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            this.timeBarSprite.fillAmount = targetFill;
            EndQuestion();
        }

        public void EndQuestion()
        {
            for (int i = 0; i < this.answers.Count; i++)
            {
                if (this.answers[i].numberAnswer == GameController.instance.number.result)
                {
                    this.answers[i].SetTrue();
                    Debug.Log("trrueeeeeee");
                }
                else
                {

                    Debug.Log("falesssssssssssss");
                }
            }
            EffectController.instance.DisCorrect();
            this.numberheart -= 1;
            if (this.numberheart <= 0)
            {
                this.isWin = true;
                PopupManager.Instance.OnUI(PopupCode.WinPanel);
            }
            else
            {
                SoundController.instance.FalseAudio();
                PopupManager.Instance.UpdateDataUI(PopupCode.MathTestPanel);
                DOVirtual.DelayedCall(2.5f, delegate
                {
                    GameController.instance.isAnswer = false;
                    GameController.instance.SetQuestion();
                });
            }
        }
    }
}