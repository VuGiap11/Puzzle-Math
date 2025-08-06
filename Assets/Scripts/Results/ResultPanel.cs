using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Rubik.math
{
    public class ResultPanel : PopupUI
    {
        public AvarResult AvarResult;
        [SerializeField] private TextMeshProUGUI resultText;
        public Number number;
        public Transform holder;
        private float spawnInterval = 0.15f;
        private float delayBeforeTypeB = 1.5f;
        public bool isSpawning = false;
        public int numberBaby = 0;
        public List<AvarResult> lsAvarResult = new List<AvarResult>();
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            Init();
            //SpawnBaby();
        }

        public void Init()
        {
            //this.number = GameManager.instance.number;
            this.number = GameController.instance.number;
            switch (GameController.instance.mathType)
            {
                case MathType.Addition:
                    this.resultText.text = this.number.num1.ToString() + "+" + this.number.num2.ToString() + "=" + this.number.result.ToString();
                    break;
                case MathType.Subtraction:
                    this.resultText.text = this.number.num1.ToString() + "-" + this.number.num2.ToString() + "=" + this.number.result.ToString();
                    break;
                case MathType.Multipacation:
                    this.resultText.text = this.number.num1.ToString() + "*" + this.number.num2.ToString() + "=" + this.number.result.ToString();
                    break;
                case MathType.Division:
                    this.resultText.text = this.number.num1.ToString() + "/" + this.number.num2.ToString() + "=" + this.number.result.ToString();
                    break;
            }

        }

        public void SpawnBaby()
        {
            if (spwan != null)
            {
                StopCoroutine(spwan);
            }
            spwan = StartCoroutine(SpawnEnemies());
        }

        Coroutine spwan;
        IEnumerator SpawnEnemies()
        {
            MyFunction.ClearChild(this.holder);
            isSpawning = true;
            this.numberBaby = 0;
            this.lsAvarResult.Clear();
            //if (this.number.num1 > 0)
            //{
            //    for (int i = 0; i < this.number.num1; i++)
            //    {
            //        AvarResult avarResult = Instantiate(AvarResult, holder);
            //        avarResult.Init(GameManager.instance.IdAvar, i);
            //        yield return new WaitForSeconds(spawnInterval);
            //    }
            //}
            //yield return new WaitForSeconds(delayBeforeTypeB);
            //if (this.number.num2 > 0)
            //{
            //    for (int i = 0; i < this.number.num2; i++)
            //    {
            //        AvarResult avarResult = Instantiate(AvarResult, holder);
            //        avarResult.Init(GameManager.instance.IdAvar, i);
            //        yield return new WaitForSeconds(spawnInterval);
            //    }
            //}
            switch (GameController.instance.mathType)
            {
                case MathType.Addition:
                    if (this.number.num1 > 0)
                    {
                        for (int i = 0; i < this.number.num1; i++)
                        {
                            this.numberBaby++;
                            AvarResult avarResult = Instantiate(AvarResult, holder);
                            avarResult.Init(UserManager.instance.IdAvar, this.numberBaby);
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
                    yield return new WaitForSeconds(delayBeforeTypeB);
                    if (this.number.num2 > 0)
                    {
                        for (int i = 0; i < this.number.num2; i++)
                        {
                            this.numberBaby++;
                            AvarResult avarResult = Instantiate(AvarResult, holder);
                            avarResult.Init(UserManager.instance.IdAvar, this.numberBaby);
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
                    break;
                case MathType.Subtraction:
                    if (this.number.num1 > 0)
                    {
                        for (int i = 0; i < this.number.num1; i++)
                        {
                            this.numberBaby++;
                            AvarResult avarResult = Instantiate(AvarResult, holder);
                            avarResult.Init(UserManager.instance.IdAvar, this.numberBaby);
                            this.lsAvarResult.Add(avarResult);
                            yield return new WaitForSeconds(spawnInterval);
                        }
                    }
                    yield return new WaitForSeconds(delayBeforeTypeB);
                    if (this.number.num2 > 0)
                    {
                        int startIndex = this.lsAvarResult.Count - 1;
                        int endIndex = this.lsAvarResult.Count - this.number.num2;

                        for (int i = startIndex; i >= endIndex && i >= 0; i--)
                        {
                            UnityEngine.Debug.Log("lsAvarResult" + lsAvarResult.Count);
                            this.lsAvarResult[i].gameObject.SetActive(false);
                            yield return new WaitForSeconds(spawnInterval * 2);
                        }
                    }
                    break;
            }
            isSpawning = false;
        }
        //public override void OffUI()
        //{
        //    base.OffUI();
        //    GameManager.instance.SetQuestion();
        //}

        public void OffResult()
        {
            //if (isSpawning)
            //{
            //    return;
            //}
            //else
            //{
            //    base.OffUI();
            //    GameManager.instance.SetQuestion();

            //}
            base.OffUI();
            GameController.instance.SetQuestion();

        }
    }
}