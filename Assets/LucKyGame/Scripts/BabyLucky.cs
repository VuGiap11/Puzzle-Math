using DG.Tweening;
using NTPackage.UI;
using Rubik.LuckyGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    public class BabyLucky : PopupUI
    {
        //public Image avatar;
        //public void Init()
        //{
        //    this.avatar.sprite = LuckyGameManager.Instance.SetAvatar();
        //    OnEmori();

        //    //MoveEye();
        //}
        //[SerializeField] private ParticleSystem particleEmori;
        //[SerializeField] private GameObject eye;
        //private float duration = 0.3f;
        //public Vector3 OrigisPos;
        //private void Awake()
        //{
        //    this.OrigisPos = eye.transform.position;
        //}
        //public void OnEmori()
        //{
        //    //if (this.particleEmori != null)
        //    //{
        //    //    this.particleEmori.Play();
        //    //}
        //    transform.localScale = Vector3.zero;
        //    this.eye.SetActive(false);
        //    transform.DOScale(Vector3.one, duration).SetEase(Ease.InSine)
        //    .OnComplete(() =>
        //    {
        //        MoveEye();
        //    });
        //}


        //private void MoveEye()
        //{
        //    float timer = UnityEngine.Random.Range(1f, 3.5f);
        //    this.eye.transform.DOKill();
        //    this.eye.transform.position = OrigisPos;
        //    this.eye.SetActive(true);
        //    this.eye.transform.DOMoveY(this.OrigisPos.y - 1f, timer).SetEase(Ease.InSine)
        //        .OnComplete(() =>
        //        {
        //            this.eye.SetActive(false);
        //            this.eye.transform.position = OrigisPos;
        //            DOVirtual.DelayedCall(0.5f, () =>
        //            {
        //                this.eye.SetActive(true);
        //            });
        //        })
        //        .SetDelay(0.3f)
        //       .SetLoops(-1, LoopType.Restart);
        //}

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            DOVirtual.DelayedCall(5f, Off);
        }


        public void Off()
        {
            LuckyGameManager.Instance.isSpinning = false;
            LuckyGameManager.Instance.DoneTime();
            base.OffUI();
        }
    }
}