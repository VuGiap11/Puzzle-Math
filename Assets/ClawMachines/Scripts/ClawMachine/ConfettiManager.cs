using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConfettiManager : MonoBehaviour
{
    public List<confetti> confettiList;
    Coroutine confettiPlay;

    public void MoveToTarget(Transform Target)
    {
        this.transform.DOMoveY(Target.position.y, 0.5f)
             .OnComplete(() =>
             {
                 PlayConfetti();
             });
    }

    public void BackToOriPoss(Transform oriPos)
    {
        StopAnimation();
        this.transform.DOMoveY(oriPos.position.y, 0.5f);
    }
    public void StopAnimation()
    {
        if (confettiPlay != null)
        {
            StopCoroutine(confettiPlay);
        }
    }
    public void PlayConfetti()
    {
        if (confettiPlay != null)
        {
            StopCoroutine(confettiPlay);
        }
        confettiPlay = StartCoroutine(SetConfetti());
    }
    IEnumerator SetConfetti()
    {
        //for (int i = 0; i < confettiList.Count; i++)
        //{
        //    confettiList[i].gameObject.SetActive(true);
        //}
        for (int i = 0; i < confettiList.Count; i++)
        {
            if (confettiList != null)
            {
                confettiList[i].StartScaling();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
