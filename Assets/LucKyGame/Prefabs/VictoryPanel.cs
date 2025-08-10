using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rubik.LuckyGame;

namespace Rubik.ClawMachine
{
    public class VictoryPanel : MonoBehaviour
    {
        private Vector3 origiPos;
        private float duration = 0.5f;
        [SerializeField] Image avar;
        [SerializeField] private TextMeshProUGUI goldText;
        private void Awake()
        {
            this.origiPos = transform.position;
        }
        public void MoveToTarget(Transform target, Action action = null)
        {
            Init();
            Vector3 startPos = transform.position;
            transform.localScale = Vector3.zero;

            transform.DOMove(target.position, duration).SetEase(Ease.InSine);
            transform.DOScale(Vector3.one, duration).SetEase(Ease.InSine)
                .OnComplete(() =>
                {
                    action?.Invoke();
                });
        }

        public void MoveToOriPos()
        {
            transform.position = this.origiPos;
        }
        public void Init()
        {
            BabyDataLucky babyData = LuckyGameManager.Instance.SetBabyData();
            BabyThreeData babyThreeData;
            if (babyData != null)
            {
                babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(babyData.id);
            }
            else
            {
                babyData = LuckyGameManager.Instance.boxuserData.BabyDatas[LuckyGameManager.Instance.boxuserData.BabyDatas.Count - 1];
                babyThreeData = DataAssets.Instance.GetBabyThreeDatabyID(babyData.id);
            }

            this.avar.sprite = babyThreeData.Avatar;
            this.goldText.text = LuckyGameManager.Instance.boxBabyThree.goldBonous.ToString();
        }
    }
}