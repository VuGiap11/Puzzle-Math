using DG.Tweening;
using NTPackage.UI;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Rubik.math
{
    public class Answer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        public GameObject falseObj, trueObj, answer;
        //public Transform holder;
        // public GoldMove goldMovePre;
        public int numberAnswer;
        public Image avar;
        public void Init(int number, string id)
        {
            this.numberAnswer = number;
            this.resultText.text = this.numberAnswer.ToString();
            this.falseObj.SetActive(false);
            this.trueObj.SetActive(false);
            this.answer.SetActive(true);
            if (GameController.instance.mathType == MathType.Test) return;
            BabyData babyData = DataAssets.instance.GetBabyDatabyID(id);
            if (this.avar != null)
            {
                this.avar.sprite = babyData.Avatar;
            }
            else
            {
                return;
            }
        }

        public void OneClick()
        {
            if (GameController.instance.isAnswer) return;
            SoundController.instance.AudioButton();
            GameController.instance.isAnswer = true;
            if (GameController.instance.mathType == MathType.Test)
            {
                TestManager.instance.StopTime();
                if (this.numberAnswer == GameController.instance.number.result)
                {
                    SoundController.instance.TrueAudio();
                    SetTrue();
                    TestManager.instance.Score++;
                    if (TestManager.instance.Score >= UserManager.instance.useData.highScore)
                    {
                        UserManager.instance.useData.highScore = TestManager.instance.Score;
                        UserManager.instance.SaveData();
                    }
                    EffectController.instance.Correct();
                    PopupManager.Instance.UpdateDataUI(PopupCode.MathTestPanel);
                    // SpawnGold();
                    DOVirtual.DelayedCall(2.5f, delegate
                    {
                        GameController.instance.isAnswer = false;
                        GameController.instance.SetQuestion();
                    });
                }
                else
                {
                    SoundController.instance.FalseAudio();
                    SetFalse();
                    TestManager.instance.SetResult();
                    EffectController.instance.DisCorrect();
                    TestManager.instance.AnswerDefeat();
                   
                    if (TestManager.instance.isWin) return;
                    //GameManager.instance.DisCorrect();
                    //GameManager.instance.numberheart--;
                    //if (GameManager.instance.numberheart <= 0)
                    //{
                    //    PopupManager.Instance.OnUI(PopupCode.HighScorePanel);
                    //    return;
                    //}
                    DOVirtual.DelayedCall(2.5f, delegate
                    {
                        GameController.instance.isAnswer = false;
                        GameController.instance.SetQuestion();
                    });
                }

                //HeaderController.instance.goldTestPanel.Init();
             
            }
            else
            {

                if (this.numberAnswer == GameController.instance.number.result)
                {
                    SoundController.instance.TrueAudio();
                    SetTrue();
                    EffectController.instance.Correct();
                    UserManager.instance.useData.gold += 2;
                    UserManager.instance.SaveData();
                    // HeaderController.instance.goldPracticePanel.Init();
                    PopupManager.Instance.UpdateDataUI(PopupCode.MathPanel);
                    //GameManager.instance.InitGold();
                    //SpawnGold();

                    DOVirtual.DelayedCall(2.5f, delegate
                    {
                        GameController.instance.isAnswer = false;
                        //GameManager.instance.SetQuestion();
                        GameController.instance.SetQuestion();
                    });
                }
                else
                {
                    SoundController.instance.FalseAudio();
                    SetFalse();
                    EffectController.instance.DisCorrect();
                    AnswerPanel AnswerPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.AnswerPanel) as AnswerPanel;
                    if (AnswerPanel != null)
                    {
                        AnswerPanel.SetResult();
                    }
                    else
                    {
                        Debug.Log("hopepanel is null");
                    }
                    DOVirtual.DelayedCall(2.2f, delegate
                    {
                        GameController.instance.isAnswer = false;
                        PopupManager.Instance.OnUI(PopupCode.ResultPanel);
                    });
                }

            }

            //if (GameController.instance.mathType == MathType.Test)
            //{
            //    TestManager.instance.SetResult();
            //}else
            //{
            //    AnswerPanel AnswerPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.AnswerPanel) as AnswerPanel;
            //    if (AnswerPanel != null)
            //    {
            //        AnswerPanel.SetResult();
            //    }
            //    else
            //    {
            //        Debug.Log("hopepanel is null");
            //    }
            //}
      

        }

        public void SetTrue()
        {
            this.falseObj.SetActive(false);
            this.trueObj.SetActive(true);
            this.answer.SetActive(false);
        }
        public void SetFalse()
        {
            this.falseObj.SetActive(true);
            this.trueObj.SetActive(false);
            this.answer.SetActive(false);
        }

        //public void SpawnGold()
        //{
        //    GoldMove gold = Instantiate(this.goldMovePre, holder.transform);
        //    gold.transform.SetParent(holder.transform, false);
        //    gold.pointA = holder.transform;
        //    gold.CreateSmoothCurve(GameManager.instance.tarGetPos, GameManager.instance.InitGold);
        //}
    }
}