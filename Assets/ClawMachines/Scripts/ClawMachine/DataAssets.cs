
using Rubik.Sort_Challenge.Data.Loading;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rubik.LuckyGame;

namespace Rubik.ClawMachine
{
    [System.Serializable]
    public class RankData
    {
        public int Id;
        public string Name;
        public int NumberBaby;
        public int IdAvar;

    }
    [System.Serializable]
    public class RankDatas
    {
        public List<RankData> rankdatas;
    }
    [System.Serializable]
    public class LevelDataCfs
    {
        public List<LevelDataCf> LevelDatas;
    }
    [System.Serializable]
    public class LevelDataCf
    {
        //public int level;
        public List<string> Id;
        public List<string> IdCandy;
        public int numberBallon; // số lượng bong bóng
        //public int numberCandy;
    }
    [Serializable]
    public class RewardData
    {
        public int day;
        public RewardType type;
        public int amount; // Chỉ sử dụng khi type là rewardGold
        public string idCandy; // Chỉ sử dụng khi type là rewardCandy
        public int numberCandy;
    }

    [Serializable]
    public class RewardConfig
    {
        public List<RewardData> rewardatas;
    }

    [Serializable]
    public class IdPositioncf
    {
        public List<int> id;
    }
    [Serializable]
    public class IdPositioncfs
    {
        public List<IdPositioncf> idPositionDatas;
    }
    //gamelucky
    [Serializable]
    public class BoxBabyThree
    {
        public TypeBox typeBaby;
        public int cap;
        public int price;
        public List<string> IdBaby;
        public List<string> RewardBaby;
        public int goldBonous;
    }
    public class DataAssets : MonoBehaviour
    {
        public static DataAssets Instance;
        //public List<AnimalData> animals;
        public List<AnimalData> ListAnimals;
        public List<AnimalData> ListAnimalCommons;
        public List<AnimalData> ListAnimalPreniums;
        public List<AnimalData> ListAnimalLegendarys;
        public List<AnimalData> ListAnimalMythicals;
        public List<CandyData> candies;
        public TextAsset levelDataText;
        public TextAsset RewardDataText;
        public LevelDataCfs levelData;
        public GameObject coin;
        public LoadingController loadingController;
        public List<Sprite> imageAvar;
        public RankDatas RankDatas;
        public TextAsset rankDataText;
        public List<Sprite> imageMedal;
        public Animal AnimalPre;
        public Candy candyPre;
        public RewardConfig rewardConfigs;

        [Header("MeryGame")]
        public IdPositioncfs idPositioncfs;
        public TextAsset IdPositionDataText;
        [Header("LuckyGame")]
        public List<BabyThreeData> babyThreeDatas;
        public List<BoxBabyThree> boxBabyThrees;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            //LoadData();
            //LoadDataRank();

        }
        private void Start()
        {
            //UserManager.instance.LoadData();
            //HomeController.instance.Init();
        }
        public void LoadData()
        {
            this.idPositioncfs = JsonUtility.FromJson<IdPositioncfs>(this.IdPositionDataText.text);
            this.levelData = JsonUtility.FromJson<LevelDataCfs>(levelDataText.text);
            SetAnimal();
            LoadDataRank();
            LoadRewardData();
        }
        public void LoadDataRank()
        {
            this.RankDatas = JsonUtility.FromJson<RankDatas>(this.rankDataText.text);
        }
        public void LoadRewardData()
        {
            this.rewardConfigs = JsonUtility.FromJson<RewardConfig>(this.RewardDataText.text);
        }
        public void SetAnimal()
        {
            if (this.ListAnimals == null || this.ListAnimals.Count <= 0) return;
            for (int i = 0; i < this.ListAnimals.Count; i++)
            {
                AnimalData animal = this.ListAnimals[i];
                if (this.ListAnimals[i].RarityAnimal == RarityAnimal.Common)
                {
                    this.ListAnimalCommons.Add(animal);
                }
                if (this.ListAnimals[i].RarityAnimal == RarityAnimal.Prenium)
                {
                    this.ListAnimalPreniums.Add(animal);
                }
                if (this.ListAnimals[i].RarityAnimal == RarityAnimal.Legendary)
                {
                    this.ListAnimalLegendarys.Add(animal);
                }
                if (this.ListAnimals[i].RarityAnimal == RarityAnimal.Mythical)
                {
                    this.ListAnimalMythicals.Add(animal);
                }
            }
        }
        //public LevelDataCf GetLevelDataCfByLevel(int level)
        //{
        //    return levelData.LevelDatas.Find(a => { return a.level == level; });
        //}

        public AnimalData GetAnimalById(string id)
        {
            return ListAnimals.Find(a => { return a.Id == id; });
        }
        public CandyData GetCandyById(string id)
        {
            return candies.Find(a => { return a.Id == id; });
        }

        public void LoadScene(string sceneName)
        {
            loadingController.gameObject.SetActive(true);
            loadingController.StartLoadingScene(100, sceneName);
        }
        public BabyThreeData GetBabyThreeDatabyID(string id)
        {
            return babyThreeDatas.Find(a => { return a.Id == id; });
        }
        public BoxBabyThree GetBoxBabyThreebyType(TypeBox typeBaby)
        {
            return boxBabyThrees.Find(a => { return a.typeBaby == typeBaby; });
        }


    }

}
