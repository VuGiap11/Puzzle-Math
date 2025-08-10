
using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class RankPanel : PopupUI
    {
        public Transform holder;

        public Rank rankPre;
        public RankPlayer rankPlayer;
        //public void SortRank()
        //{
        //    int n = ClawDataAssets.Instance.RankDatas.rankdatas.Count;
        //    for (int i = 0; i < n - 1; i++)
        //    {
        //        for (int j = 0; j < n - i - 1; j++)
        //        {
        //            if (ClawDataAssets.Instance.RankDatas.rankdatas[j].NumberBaby < ClawDataAssets.Instance.RankDatas.rankdatas[j + 1].NumberBaby)
        //            {
        //                RankData temp = ClawDataAssets.Instance.RankDatas.rankdatas[j];
        //                ClawDataAssets.Instance.RankDatas.rankdatas[j] = ClawDataAssets.Instance.RankDatas.rankdatas[j + 1];
        //                ClawDataAssets.Instance.RankDatas.rankdatas[j + 1] = temp;
        //            }
        //        }
        //    }
        //}
        //public void OnEnable()
        //{
        //    SpawnRank();
        //    if (RankDataManager.instance.CheckOntop())
        //    {
        //        this.rankPlayer.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        this.rankPlayer.gameObject.SetActive(true);
        //        this.rankPlayer.Init();
        //    }
          
        //}
        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SpawnRank();
            if (RankDataManager.instance.CheckOntop())
            {
                this.rankPlayer.gameObject.SetActive(false);
            }
            else
            {
                this.rankPlayer.gameObject.SetActive(true);
                this.rankPlayer.Init();
            }
        }
        //public bool CheckOntop()
        //{
        //    if (ClawDataAssets.Instance.RankDatas.rankdatas.Count <= 10)
        //    {
        //        return true;
        //    }
        //    int userId = UserManager.instance.useData.id;
        //    var rankList = ClawDataAssets.Instance.RankDatas.rankdatas;

        //    for (int i = 0; i < rankList.Count && i < 10; i++)
        //    {
        //        if (rankList[i].Id == userId)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        //public void AddPlayer()
        //{
        //    ClawDataAssets.Instance.RankDatas.rankdatas.Clear();
        //    ClawDataAssets.Instance.LoadDataRank();
        //    RankData newRank = new RankData
        //    {
        //        Id = UserManager.instance.useData.id,
        //        Name = UserManager.instance.useData.namePlayer,
        //        NumberBaby = UserManager.instance.useData.numberBaby,
        //        IdAvar = UserManager.instance.useData.idAvar,
        //    };
        //    ClawDataAssets.Instance.RankDatas.rankdatas.Add(newRank);
        //}
        public void SpawnRank()
        {
            MyFunction.ClearChild(this.holder);
             if (DataAssets.Instance.RankDatas.rankdatas.Count <= 10)
            {
                for (int i = 0; i < DataAssets.Instance.RankDatas.rankdatas.Count; i++)
                {
                    Rank rank = Instantiate(this.rankPre);
                    rank.transform.SetParent(this.holder, false);
                    rank.InitRank(DataAssets.Instance.RankDatas.rankdatas[i - 1], i);
                }
            }
            else
            {
                for (int i = 1; i <= 10; i++)
                {
                    //if (i > ClawDataAssets.Instance.RankDatas.rankdatas.Count) return;
                    Rank rank = Instantiate(this.rankPre);
                    rank.transform.SetParent(this.holder, false);
                    rank.InitRank(DataAssets.Instance.RankDatas.rankdatas[i - 1], i);
                }
            }
        }
    }
}