using DG.Tweening;
using NTPackage.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rubik.ClawMachine
{

    public class NextLevel : PopupUI
    {
        public GameObject logo;
        private Vector3 oriPos;
        [SerializeField] private Transform pointB; // V? trí k?t thúc
        [SerializeField] private float moveDuration = 1.5f; // Th?i gian di chuy?n
        private void Awake()
        {
            this.oriPos = logo.transform.position;
        }
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            DOVirtual.DelayedCall(0.5f, delegate
            {
                MoveFromAToB();
            });
        }
        public override void OffUI()
        {
            base.OffUI();
            this.logo.transform.position = oriPos;
        }

        public void MoveFromAToB()
        {
            if (pointB == null) return;
            transform.DOMove(pointB.position, moveDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, delegate
                {
                    this.OffUI();
                    CardsController.instance.Init();
                });
            });

        }
    }

}