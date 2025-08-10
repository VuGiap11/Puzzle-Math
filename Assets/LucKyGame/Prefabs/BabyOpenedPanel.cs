using DG.Tweening;
using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rubik.LuckyGame;

namespace Rubik.ClawMachine
{
    public class BabyOpenedPanel : PopupUI
    {
        public List<confetti> confettiList;
        Coroutine confettiPlay;
        public ParticleSystem particleSystemBaby;
        public Image avar;
        public BabyThreeData BabyThreeData;
        public Transform starPos, endPos;
        public GameObject confettiObj;
        public Transform origispos;
        private void Awake()
        {
          //this.origispos = confettiObj.transform.position;
        }
        public override void OnUI(object data = null)
        {


            BabyDataLucky babyData = LuckyGameManager.Instance.SetBabyData();
            if (babyData == null) return;
            this.BabyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(babyData.id);
           // PlayConfetti();
            //this.particleSystemBaby.Play();
            //// UpdateData();
            base.OnUI(data);
        }

        public override void UpdateData(object data = null)
        {
            base.UpdateData(data);
            this.avar.sprite = this.BabyThreeData.Avatar;
        }

        public override void Show()
        {
            base.Show();
            ScaleAvar();
            MoveConfetti();
        }
        public override void OffUI()
        {
            this.confettiObj.transform.position = this.origispos.position;
            base.OffUI();
            if (this.particleSystemBaby.isPlaying)
            {
                this.particleSystemBaby.Stop();
            }
            if (confettiPlay != null)
            {
                StopCoroutine(confettiPlay);
            }
            //for (int i = 0; i < confettiList.Count; i++)
            //{
            //    confettiList[i].gameObject.SetActive(false);
            //}
           
            LuckyGameManager.Instance.ActionEnd();
           
        }
        public void PlayConfetti()
        {
            if (confettiPlay != null)
            {
                StopCoroutine(confettiPlay);
            }
            confettiPlay = StartCoroutine(SetConfetti());
        }
        IEnumerator SetConfetti()
        {
            //for (int i = 0; i < confettiList.Count; i++)
            //{
            //    confettiList[i].gameObject.SetActive(true);
            //}
            for (int i = 0; i < confettiList.Count; i++)
            {
                if (confettiList != null)
                {
                    confettiList[i].StartScaling();
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        private void ScaleAvar()
        {
            this.avar.gameObject.transform.localScale = new Vector3(0, 0, 0);
            this.avar.gameObject.SetActive(true);
            this.avar.gameObject.transform.DOScale(new Vector3(3f, 3f, 3f), 0.5f)
                .OnComplete(() =>
                   {
                       this.particleSystemBaby.Play();
                   });
        }

        private void MoveConfetti()
        {
            this.confettiObj.transform.DOMoveY(endPos.position.y, 0.5f)
               .OnComplete(() =>
               {
                   PlayConfetti();
               });
        }
    }
}