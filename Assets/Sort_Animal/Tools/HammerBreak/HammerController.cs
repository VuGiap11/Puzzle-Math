using System;
using DG.Tweening;
using Rubik.ClawMachine;
using UnityEngine;

namespace Tool.HammerBreak
{
    public class HammerController : MonoBehaviour
    {
        [SerializeField] Transform transHammerTarget;
        [SerializeField] GameObject gbHammerLeftHead, gbHammerRightHead, gbHammerBody;
        Vector3 oriScaleHammer, oriRotateHammer, oriPosHammer, oriPosHammerLeftHead, oriPosHammerRightHead, oriPosHammerBody;
        float durationMoveHammer = 1f, multiplierScaleHammer = 5f;
        public AudioClip audioHammer;
        void Awake()
        {
            oriScaleHammer = transform.localScale;
            oriRotateHammer = transform.rotation.eulerAngles;
            oriPosHammer = transform.position;
            oriPosHammerLeftHead = gbHammerLeftHead.transform.localPosition;
            oriPosHammerRightHead = gbHammerRightHead.transform.localPosition;
            oriPosHammerBody = gbHammerBody.transform.localPosition;
        }
        public void MoveHammer(Action actionCallBackDoneAnim = null)
        {
            transform.DOMove(transHammerTarget.position, 0);
            gbHammerLeftHead.transform.DOMove(oriPosHammer, 0);
            gbHammerRightHead.transform.DOMove(oriPosHammer, 0);
            gbHammerBody.transform.DOMove(oriPosHammer, 0);


            DOTween.Sequence()
            .Append(gbHammerRightHead.transform.DOLocalMove(oriPosHammerRightHead, durationMoveHammer).SetEase(Ease.InOutSine))
            .Join(gbHammerBody.transform.DOLocalMove(oriPosHammerBody, durationMoveHammer).SetEase(Ease.InOutSine).SetDelay(0.1f))
            .Join(gbHammerLeftHead.transform.DOLocalMove(oriPosHammerLeftHead, durationMoveHammer).SetEase(Ease.InOutSine).SetDelay(0.2f))

            .Join(transform.DORotate(new Vector3(0, 0, 0), durationMoveHammer).SetEase(Ease.InOutSine))
            .Append(transform.DOScale(transform.localScale * multiplierScaleHammer, durationMoveHammer / 2f).SetEase(Ease.InOutSine))
            .OnComplete(() =>
            {
                AnimHammering(actionCallBackDoneAnim);
            });
        }
        public void ResetHammer()
        {

            transform.localScale = oriScaleHammer;
            transform.rotation = Quaternion.Euler(oriRotateHammer);
            transform.position = oriPosHammer;
            gbHammerLeftHead.transform.localPosition = oriPosHammerLeftHead;
            gbHammerRightHead.transform.localPosition = oriPosHammerRightHead;
            gbHammerBody.transform.localPosition = oriPosHammerBody;
            //them
            this.gameObject.SetActive(false);
        }
        float durationAnimHammering = 0.5f;
        void AnimHammering(Action CallBackDoneAnim)
        {
            DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, -10), durationAnimHammering).SetEase(Ease.InOutSine))
            .Append(transform.DORotate(new Vector3(0, 0, 50), durationAnimHammering / 2f).SetEase(Ease.InOutSine))
            .Append(gbHammerLeftHead.transform.DOScaleX(gbHammerLeftHead.transform.localScale.x * 0.5f, durationAnimHammering / 5f).SetEase(Ease.InOutSine)
            .OnComplete(() =>
                    {
                        SoundController.instance.PlaySingle(this.audioHammer);
                        CallBackDoneAnim?.Invoke();
                    }))

            .Join(gbHammerRightHead.transform.DOScaleX(gbHammerRightHead.transform.localScale.x * 1.15f, durationAnimHammering / 5f).SetEase(Ease.InOutSine))
            .Append(gbHammerLeftHead.transform.DOScaleX(1, durationAnimHammering / 2f).SetEase(Ease.InOutSine))
            .Join(gbHammerRightHead.transform.DOScaleX(1, durationAnimHammering / 3f).SetEase(Ease.InOutSine))
            .OnComplete(() =>
                         {
                             ResetHammer();
                         });
        
        }
    }
}
