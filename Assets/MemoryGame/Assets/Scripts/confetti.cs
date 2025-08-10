using DG.Tweening;
using UnityEngine;

namespace Rubik.ClawMachine
{

    public class confetti : MonoBehaviour
    {
        public GameObject phaohoa;
        public ParticleSystem particleSystemConfetti;
        public float scaleDuration = 0.2f; // Thời gian scale
        private Vector3 originalScale;
        public AudioClip phaohoaSouund;
        public Transform originalpos, targetPos;

        void Start()
        {
            originalScale = transform.localScale;
        }

        [ContextMenu("StartScaling")]
        public void StartScaling()
        {
            Vector3 targetScaleDown = originalScale * 0.6f;
            Vector3 targetScaleUp = originalScale * 1.2f;
            phaohoa.transform.DOScale(targetScaleDown, 0.4f).OnComplete(() =>
            {
                phaohoa.transform.DOScale(targetScaleUp, scaleDuration).OnComplete(() =>
                {
                    phaohoa.transform.DOScale(originalScale, 0.1f)
                    .OnComplete(() =>
                    {
                        //confettiIcoin.SetActive(true);
                        MoveConfetti();
                        Move();
                    });
                });
            });
        }
        public void MoveConfetti()
        {
            SoundController.instance.PlaySingle(this.phaohoaSouund);
            particleSystemConfetti.Play();
            //});
        }
        public void Move()
        {
            this.particleSystemConfetti.transform.position = originalpos.position;
            particleSystemConfetti.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            particleSystemConfetti.gameObject.transform.DOScale(new Vector3(300f, 300f, 300f), 0.6f);
            particleSystemConfetti.transform.DOMove(targetPos.position, 0.6f).OnComplete(() =>
            {
            });

        }
    }
}
