using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Sort_Challenge.Data.Loading
{
    public class ItemLoading : MonoBehaviour
    {
        [SerializeField] List<Image> lsImgTinyItem;
        List<float> lsOriScaleTinyItem;
        void Awake()
        {
            lsOriScaleTinyItem = new List<float>();
            foreach (var item in lsImgTinyItem)
            {
                lsOriScaleTinyItem.Add(item.transform.localScale.x);
            }
        }
        bool isStopLoading = false;
        public void StopAnimLoading()
        {
            isStopLoading = true;
        }
        public void StartAnimLoading()
        {
            isStopLoading = false;
            StartCoroutine(AnimTinyItemLoading());
        }
        IEnumerator AnimTinyItemLoading()
        {
            for (int i = 0; i < lsImgTinyItem.Count; i++)
            {
                Tween tween = lsImgTinyItem[i].transform.DOScale(0, 0.15f).SetEase(Ease.Linear).OnComplete(() => lsImgTinyItem[i].transform.DOScale(1f, 0.5f).SetEase(Ease.Linear));
                yield return tween.WaitForCompletion();
            }
            for (int i = 0; i < lsImgTinyItem.Count; i++)
            {
                Tween tween = lsImgTinyItem[i].transform.DOScale(lsOriScaleTinyItem[i], 0.15f).SetEase(Ease.Linear).OnComplete(() => lsImgTinyItem[i].transform.DOScale(1f, 0.5f).SetEase(Ease.Linear));
                yield return tween.WaitForCompletion();
            }
            if (!isStopLoading)
            {
                StartCoroutine(AnimTinyItemLoading());
            }
        }

        public void StopAnimation()
        {
            DOTween.KillAll();
        }
    }

}