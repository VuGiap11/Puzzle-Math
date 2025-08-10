using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Rubik.ClawMachine
{
    public class SetActiveGameObject : MonoBehaviour
    {
        public GameObject Object;
        private Vector3 originAvarPos;
        private bool isClickable;
        private void Awake()
        {
            originAvarPos = Object.transform.localScale;
        }
        public void SetObject(bool a)
        {
            SoundController.instance.AudioButton();
            
            Transform babyChild = Object.transform.GetChild(0);
            if (babyChild == null) return;
            if (a)
            {
                Object.SetActive(a);
                if (isClickable) return;
                isClickable = true;
                babyChild.localScale = Vector3.zero;
                //   DOTween.Sequence()
                //.Append(babyChild.DOScale(new Vector3(originAvarPos.x + 0.05f, originAvarPos.y + 0.05f, originAvarPos.z + 0.05f), 0.25f).SetEase(Ease.Linear)).SetDelay(0.01f)
                //.Append(babyChild.transform.DOScale(originAvarPos, 0.12f).SetEase(Ease.Linear));
                babyChild.DOScale(new Vector3(originAvarPos.x + 0.05f, originAvarPos.y + 0.05f, originAvarPos.z + 0.05f), 0.45f).SetEase(Ease.OutQuart)
                .OnComplete(() =>
                         {
                             babyChild.DOScale(originAvarPos, 0.25f).SetEase(Ease.Linear)
                             .OnComplete(() =>
                             {
                                 isClickable = false;
                             });
                         });


            }
            else
            {

                //DOTween.Sequence()
                //.Append(babyChild.DOScale(new Vector3(0f, 0f, 0f), 2f).SetEase(Ease.Linear));
                //.Append(babyChild.DOScale(originAvarPos, 1f).SetEase(Ease.Linear));
                if (isClickable) return;
                isClickable = true;
                babyChild.DOScale(new Vector3(0f, 0f, 0f), 0.35f).SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    Object.SetActive(a);
                    isClickable = false;
                });


            }
        }

    }
}

