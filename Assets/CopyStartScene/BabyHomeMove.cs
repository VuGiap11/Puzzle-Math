using DG.Tweening;
using UnityEngine;


namespace Rubik.math
{
    public class BabyHomeMove : MonoBehaviour
    {
        private Vector3 oriPos;
        [SerializeField] private GameObject avatar;
        [SerializeField] private Transform target;
        private float timeMove = 0.8f;
        Tween move;
        public bool isDone = false;
        private void Awake()
        {
            oriPos = this.avatar.transform.position;
        }

        public void MoveToEndPos()
        {
            isDone = true;
            this.avatar.transform.position = oriPos;
            this.avatar.gameObject.SetActive(true);
            this.move = this.avatar.transform.DOMove(target.position, timeMove)
            .SetEase(Ease.InOutSine)
            .SetDelay(0.1f)
            .OnComplete(() =>
            {
                isDone = false;
            });
        }
        private void OnDisable()
        {
            SetOriStatus();
        }
        public void SetOriStatus()
        {
            if (this.move != null)
            {
                this.move.Kill();
            }
            this.avatar.gameObject.SetActive(false);
            this.avatar.transform.position = oriPos;

        }
    }
}