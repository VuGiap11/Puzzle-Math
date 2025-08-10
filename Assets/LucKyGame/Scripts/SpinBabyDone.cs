using DG.Tweening;
using UnityEngine;
using Rubik.ClawMachine;
using UnityEngine.UI;
using NTPackage.UI;

namespace Rubik.LuckyGame
{

    public class SpinBabyDone : MonoBehaviour
    {
        private float duration = 0.3f;
        public Vector3 OrigisPos;
        public Image avatar;
        public Transform endPos;
        public VictoryPanel VictoryPanel;
        public ParticleSystem particleSystemOpenPanel;
        public void Init()
        {
            this.avatar.sprite = LuckyGameManager.Instance.SetAvatar();
            OnEmori();
            //MoveEye();
        }
        private void OnEnable()
        {
            VictoryPanel.MoveToOriPos();
        }
        public void OnEmori()
        {
            //if (this.particleEmori != null)
            //{
            //    this.particleEmori.Play();
            //}
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, duration).SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                VictoryPanel.MoveToTarget(this.endPos);
            });
        }

        public void Claim()
        {
            LuckyGameManager.Instance.isSpinning = false;
            LuckyGameManager.Instance.DoneTime();
            BabyDataLucky babyData = LuckyGameManager.Instance.SetBabyData();
          UserManager.instance.useData.gold  += LuckyGameManager.Instance.boxBabyThree.goldBonous;
        
            if (babyData != null)
            {
                // babyData.amount++;
                if (babyData.amount >= LuckyGameManager.Instance.boxBabyThree.cap - 1)
                {
                    this.particleSystemOpenPanel.Play();
                    float time = this.particleSystemOpenPanel.main.duration;
                    DOVirtual.DelayedCall(time, delegate
                    {
                        PopupManager.Instance.OnUI(PopupCode.BabyDonePanel);
                        this.gameObject.SetActive(false);
                    });
                }
                else
                {
                    babyData.amount++;
                    this.gameObject.SetActive(false);
                }
                
            }
            else
            {
                this.gameObject.SetActive(false);
            }
            LuckyGameManager.Instance.InitText();
            LuckyGameManager.Instance.Init();
            UserManager.instance.SaveData();
        }
    }
}
