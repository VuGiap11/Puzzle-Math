using DG.Tweening;
using NTPackage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.math
{

    public class RewardPanel : PopupUI
    {
        public List<RewardDay> rewardDays;
        DateTime firstLaunchTime;
        public RewardDay rewardDayPre;
        [SerializeField] Transform holder;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] GameObject kimdongho;

        private bool isCount;
        //public GameObject noticeObj;

        private void OnDisable()
        {
            isCount = false;
        }
        //private void OnEnable()
        //{
        //    isCount = true;
        //    SpawnReward();
        //    Rotate360();

        //}
        public void UpdateData()
        {
            this.timeText.text = RewardManager.Instance.ParseTimeToDay();
        }

        public Coroutine CorCountTime;
        public IEnumerator CountTimeText()
        {
            while (isCount)
            {
                this.timeText.text = RewardManager.Instance.ParseTimeToDay();
                yield return new WaitForSeconds(1);
            }
        }
        public override void OnUI(object data = null)
        {
            isCount = true;
            base.OnUI(data);
            SpawnReward();
           // ResetRewardDay(UserManager.instance.useData.indexRewardDay, RewardDataManager.Instance.isStartGame);
            Rotate360();
            //RewardManager.Instance.TimeNotification.Add("RewardPanel", () => this.UpdateData());
            if (CorCountTime != null) StopCoroutine(CorCountTime);
            CorCountTime = StartCoroutine(CountTimeText());
        }
        void Rotate360()
        {
            kimdongho.transform.rotation = Quaternion.Euler(0, 0, 0);
            kimdongho.transform.DORotate(new Vector3(0, 0, -360), 6f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
        }
        private void SpawnReward()
        {
            this.rewardDays.Clear();
            MyFunction.ClearChild(this.holder);
            for (int i = 0; i < DataAssets.instance.rewardConfigs.rewardatas.Count; i++)
            {
                RewardDay rewardDay = Instantiate(this.rewardDayPre, this.holder);
                rewardDay.transform.SetParent(holder.transform, false);
                rewardDay.Init(DataAssets.instance.rewardConfigs.rewardatas[i]);
                this.rewardDays.Add(rewardDay);
            }
        }
    }
}