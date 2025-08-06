using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class BuildingController : MonoBehaviour
    {
        public List<AvaHomeMove> lsavaHomeMoves;
        public List<BabyHomeMove> lsbabyHomeMoves;
        Coroutine coroutineMove, coroutineBabyMove;
        private void Start()
        {
            SetOriStatus();
            // RandomBabyMove();
            if (coroutineMove != null)
            {
                StopCoroutine(coroutineMove);
            }
            if (coroutineBabyMove != null)
            {
                StopCoroutine(coroutineBabyMove);
            }
            this.coroutineMove = StartCoroutine("MoveBaby");
            this.coroutineBabyMove = StartCoroutine("MoveBabyHome");
        }
        private void OnDisable()
        {
            if (coroutineMove != null)
            {
                StopCoroutine(coroutineMove);
            }
        }
        public void SetOriStatus()
        {
            if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count <= 0) return;
            if (this.lsbabyHomeMoves == null || this.lsbabyHomeMoves.Count <= 0) return;
            for (int i = 0; i < this.lsavaHomeMoves.Count; i++)
            {
                this.lsavaHomeMoves[i].SetOriStatus();
            }
            for (int i = 0; i < this.lsbabyHomeMoves.Count; i++)
            {
                this.lsbabyHomeMoves[i].SetOriStatus();
            }
        }
        [ContextMenu("RandomBabyMove")]
        public void RandomBabyMove()
        {
            if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count <= 0) return;
            int number = Random.Range(0, this.lsavaHomeMoves.Count);
            Debug.Log(number);
            this.lsavaHomeMoves[number].MoveToEndPos();
        }
        IEnumerator MoveBaby()
        {
            if (this.lsavaHomeMoves == null || this.lsavaHomeMoves.Count == 0)
                yield break; // hoặc Debug.LogWarning để biết danh sách rỗng

            int number = Random.Range(0, this.lsavaHomeMoves.Count);
            // Debug.Log(number);
            this.lsavaHomeMoves[number].MoveToEndPos();
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => this.lsavaHomeMoves[number].isDone == false);
            //yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveBaby());
        }

        IEnumerator MoveBabyHome()
        {
            if (this.lsbabyHomeMoves == null || this.lsbabyHomeMoves.Count == 0)
                yield break;
            for (int i = 0; i < this.lsbabyHomeMoves.Count; i++)
            {
                this.lsbabyHomeMoves[i].MoveToEndPos();
                yield return new WaitUntil(() => this.lsbabyHomeMoves[i].isDone == false);
            }

            yield return new WaitForSeconds(2.5f);

            for (int i = 0; i < this.lsbabyHomeMoves.Count; i++)
            {
                this.lsbabyHomeMoves[i].SetOriStatus();
                yield return new WaitForSeconds(0.15f);
            }

            StartCoroutine(MoveBabyHome());
        }
    }
}