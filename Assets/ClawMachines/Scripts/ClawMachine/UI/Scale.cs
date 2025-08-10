using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class Scale : MonoBehaviour
    {
        public Vector3 newPos;
        public Vector3 OrigiPos;

        public void ScaleBtn()
        {
            DOTween.Sequence()
                  .Append(transform.DOScale(newPos, 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
                  .Append(transform.DOScale(OrigiPos, 1f).SetEase(Ease.Linear))
                  .SetLoops(-1);
        }

        //public void StopAnimation()
        //{
        //    DOTween.KillAll();
        //}

        private void OnEnable()
        {
            ScaleBtn();

        }
        //private void OnDisable()
        //{
        //    StopAnimation();
        //}


    }
}