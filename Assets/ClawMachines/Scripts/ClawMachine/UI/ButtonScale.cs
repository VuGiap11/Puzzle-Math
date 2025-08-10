using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Rubik.ClawMachine
{
    public class ButtonScale : MonoBehaviour
    {
        public Vector3 newPos;
        public Vector3 OrigiPos;
        Sequence sequence;
        public void ScaleBtn()
        {
            sequence= DOTween.Sequence()
                  .Append(transform.DOScale(newPos, 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
                  .Append(transform.DOScale(OrigiPos, 1f).SetEase(Ease.Linear))
                  .SetLoops(-1);
        }

        public void StopAnimation()
        {
            sequence.Kill();
        }
        // Start is called before the first frame update

    }
}

