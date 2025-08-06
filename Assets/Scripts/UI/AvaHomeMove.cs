
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Rubik.math
{
    public class AvaHomeMove : MonoBehaviour
    {
        private Vector3 oriPos;
        [SerializeField] private GameObject avatar;
        [SerializeField] private Transform target;
        [SerializeField] private List<Sprite> lsAvar;
        private float timeMove = 2.5f;
        Tween move;
        public bool isDone = false;
        private void Awake()
        {
            oriPos = this.avatar.transform.position;
        }
        [ContextMenu("MoveToEndPos")]
        public void MoveToEndPos()
        {
            isDone = true;
            if (this.lsAvar.Count <= 0) return;
            int number = Random.Range(0, this.lsAvar.Count);
            avatar.GetComponent<Image>().sprite = lsAvar[number];
            this.avatar.gameObject.SetActive(true);
            this.avatar.transform.position = oriPos;
            this.move = this.avatar.transform.DOMove(target.position, timeMove)
             .SetEase(Ease.InOutSine)
             .SetDelay(0.3f)
             .OnComplete(() =>
             {
                 this.avatar.transform.DOMove(oriPos, 0.2f).SetEase(Ease.InOutSine)
                 .OnComplete(() =>
                 {

                     this.avatar.gameObject.SetActive(false);
                     isDone = false;
                     //this.avatar.gameObject.transform.position = oriPos;
                 });
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
