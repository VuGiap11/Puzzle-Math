using NTPackage.UI;
using Rubik.math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class AnswerPanel : PopupUI
{

    public List<Answer> answers = new List<Answer>();

    public override void OnUI(object data = null)
    {
        base.OnUI(data);
    }

    public override void UpdateData(object data = null)
    {
        base.UpdateData(data);
        SpawnAnswer();
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
        //for (int i = 0; i < this.answers.Count; i++)
        //{
        //    if (this.answers[i].numberAnswer != GameController.instance.number.result)
        //    {
        //        this.answers[i].SetFalse();
        //    }
        //    else
        //    {
        //        this.answers[i].SetTrue();
        //    }
        //}
        for (int i = 0; i < this.answers.Count; i++)
        {
            if (this.answers[i].numberAnswer == GameController.instance.number.result)
            {
                this.answers[i].SetTrue();
            }
            else
            {
                return;
            }
        }
    }
}
