using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    [Serializable]
    public class RewardData
    {
        public int day;
        public int gold;
    }

    [Serializable]
    public class RewardConfig
    {
        public List<RewardData> rewardatas;
    }
    [Serializable]
    public enum MathType
    {
        Addition,
        Subtraction,
        Multipacation,
        Division,
        Test
    }
    [Serializable]
    public enum OperationType
    {
        Addition,
        Subtraction,
        Multipacation,
        Division
    }

    [Serializable]
    public class Number
    {
        public int num1;
        public int num2;
        public int result;

        public Number(int num1, int num2, int result)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.result = result;
        }
    }
    public class DataAssets : MonoBehaviour
    {
        public static DataAssets instance;
        public List<BabyData> babyDatas = new List<BabyData>();
        public RewardConfig rewardConfigs;
        public TextAsset RewardDataText;
        public List<Sprite> imageAvar = new List<Sprite>();
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public BabyData GetBabyDatabyID(string id)
        {
            return babyDatas.Find(a => { return a.Id == id; });
        }
        public void LoadRewardData()
        {
            this.rewardConfigs = JsonUtility.FromJson<RewardConfig>(this.RewardDataText.text);
        }
    }
}
