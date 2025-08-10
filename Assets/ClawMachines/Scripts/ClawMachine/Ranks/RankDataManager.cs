using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class RankDataManager : MonoBehaviour
    {
        public static RankDataManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void LoadRank()
        {
            AddPlayer();
            SortRank();
        }
        public void SortRank()
        {
            int n = DataAssets.Instance.RankDatas.rankdatas.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (DataAssets.Instance.RankDatas.rankdatas[j].NumberBaby < DataAssets.Instance.RankDatas.rankdatas[j + 1].NumberBaby)
                    {
                        RankData temp = DataAssets.Instance.RankDatas.rankdatas[j];
                        DataAssets.Instance.RankDatas.rankdatas[j] = DataAssets.Instance.RankDatas.rankdatas[j + 1];
                        DataAssets.Instance.RankDatas.rankdatas[j + 1] = temp;
                    }
                }
            }
        }
        public void AddPlayer()
        {
            DataAssets.Instance.RankDatas.rankdatas.Clear();
            DataAssets.Instance.LoadDataRank();
            RankData newRank = new RankData
            {
                Id = UserManager.instance.useData.id,
                Name = UserManager.instance.useData.namePlayer,
                NumberBaby = UserManager.instance.useData.numberBaby,
                IdAvar = UserManager.instance.useData.idAvar,
            };
            DataAssets.Instance.RankDatas.rankdatas.Add(newRank);
        }
        public bool CheckOntop()
        {
            if (DataAssets.Instance.RankDatas.rankdatas.Count <= 10)
            {
                return true;
            }
            int userId = UserManager.instance.useData.id;
            var rankList = DataAssets.Instance.RankDatas.rankdatas;

            for (int i = 0; i < rankList.Count && i < 10; i++)
            {
                if (rankList[i].Id == userId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}