
using Rubik.LuckyGame;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.ClawMachine
{

    [Serializable]
    public class Animaldata
    {
        public string id;
        public int number;
        public int slot;
        public bool isDone;
        public Animaldata(string Id)
        {
            this.id = Id;
            this.number = 0;
            this.slot = -1;
            this.isDone = false;
            //for (int i = 0; i < ClawGameManager.Instance.BabyThreeUIStorePanel.lsBabyPos.Count; i++)
            //{
            //    if (ClawGameManager.Instance.BabyThreeUIStorePanel.lsBabyPos[i].babyThreeUI != null) continue;
            //    this.slot = i;
            //    return;
            //}
        }
    }
    [Serializable]
    public class Animaldatas
    {
        public List<Animaldata> animaldatas;
    }

    [Serializable]
    public class Candydata
    {
        public string id;
        public int number;
        public Candydata(string Id)
        {
            this.id = Id;
            this.number = 0;
        }
    }
    [Serializable]
    public class Candydatas
    {
        public List<Candydata> candydatas;
    }

    [Serializable]
    public class DataPlayer
    {
        public int slot;
        public DataPlayer(int slot)
        {
            this.slot = slot;
        }
    }
    [Serializable]
    public class UseData
    {
        public List<string> slots;
        public int gold;
        public int level;
        public string namePlayer;
        public int idAvar;
        public int indexRewardDay;
        public int numberBaby;
        public int id;
        public int numberCoin;
        public bool isRemoveAds;
        //public int numberHammer;
        //public int numberResetTime;
        public int highLevel;
        public int highScore;
        public int numberAds;
        public int highScoreMatch3;
    }

    //luckygam

    [Serializable]
    public class BoxUserDatas
    {
        public List<BoxUserData> data;
    }
    public class UserManager : MonoBehaviour
    {

        public static UserManager instance;
        public Animaldatas Animaldatas;
        public Candydatas Candydatas;
        public UseData useData;
        // public bool canIsClaw;

        //luckygame
        public BoxUserDatas BoxUserDatas;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public Animaldata GetAnimalDatabyID(string id)
        {
            return Animaldatas.animaldatas.Find(a => { return a.id == id; });
        }
        public Candydata GetCandyDatabyID(string id)
        {
            return Candydatas.candydatas.Find(a => { return a.id == id; });
        }
        public void LoadData()
        {
            string jsonData = PlayerPrefs.GetString("PlayerData");
            //Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonData))
            {

                Animaldatas = JsonUtility.FromJson<Animaldatas>(jsonData);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                Debug.LogWarning("No saved ");
                //Animaldatas.animaldatas = new List<Animaldata>();
                for (int i = 0; i < DataAssets.Instance.ListAnimals.Count; i++)
                {
                    Animaldatas.animaldatas.Add(new Animaldata(DataAssets.Instance.ListAnimals[i].Id));
                }
            }
            string jsonDataCandy = PlayerPrefs.GetString("PlayerDataCandy");
            Debug.Log(jsonDataCandy);
            if (!string.IsNullOrEmpty(jsonDataCandy))
            {

                Candydatas = JsonUtility.FromJson<Candydatas>(jsonDataCandy);
                //Debug.LogWarning("No saved 1 ");
            }
            else
            {
                //Debug.LogWarning("No saved ");
                //Candydatas.candydatas = new List<Candydata>();
                for (int i = 0; i < DataAssets.Instance.candies.Count; i++)
                {
                    Candydata candyData = new Candydata(DataAssets.Instance.candies[i].Id);
                    Candydatas.candydatas.Add(candyData);
                }
            }
            string jsonUserData = PlayerPrefs.GetString("UserData");
            Debug.Log(jsonData);
            if (!string.IsNullOrEmpty(jsonUserData))
            {

                this.useData = JsonUtility.FromJson<UseData>(jsonUserData);
                //Debug.LogWarning("No saved 1 ");
            }
            else
            {
                //Debug.LogWarning("No saved ");
                //for (int i = 0; i < ClawGameManager.Instance.BabyThreeUIStorePanel.lsBabyPos.Count; i++)
                //{
                //    string index = "A0000";
                //    this.useData.slots.Add(index);
                //}

                for (int i = 0; i < 48; i++)
                {
                    string index = "A0000";
                    this.useData.slots.Add(index);
                }
                this.useData.gold = 200;
                this.useData.level = 0;
                this.useData.namePlayer = "BabyThree";
                this.useData.idAvar = 0;
                this.useData.indexRewardDay = 0;
                this.useData.numberBaby = 0;
                this.useData.id = 1000000;
                this.useData.numberCoin = 5;
                this.useData.isRemoveAds = false;
                //this.useData.numberHammer = 3;
                //this.useData.numberResetTime = 3;
                this.useData.highLevel = 0;
                this.useData.highScore = 0;
                this.useData.numberAds = 0;
                this.useData.highScoreMatch3 = 0;

            }

            string jsonDataCandyLucKy = PlayerPrefs.GetString("PlayerDataCandyLucKy");
            Debug.Log(jsonDataCandyLucKy);
            if (!string.IsNullOrEmpty(jsonDataCandyLucKy))
            {
                BoxUserDatas = JsonUtility.FromJson<BoxUserDatas>(jsonDataCandyLucKy);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                for (int i = 0; i < DataAssets.Instance.boxBabyThrees.Count; i++)
                {
                    BoxUserData box = new BoxUserData();
                    box = boxUserData(DataAssets.Instance.boxBabyThrees[i].typeBaby);
                    BoxUserDatas.data.Add(box);
                }
            }

            //ClawGameManager.Instance.InitGold(this.useData.gold);
            SaveData();
        }
        //public List<string> slots;
        //public int gold;
        //public int level;
        //public string namePlayer;
        //public int idAvar;
        //public int indexRewardDay;
        //public int numberBaby;
        //public int id;
        public BoxUserData boxUserData(TypeBox typeBox)
        {
            BoxUserData value = new BoxUserData();
            BoxBabyThree boxBabyThree = DataAssets.Instance.GetBoxBabyThreebyType(typeBox);
            value.typeBox = typeBox;
            value.IdBabyHold = boxBabyThree.IdBaby[0];
            value.BabyDatas = new List<BabyDataLucky>();
            for (int i = 0; i < boxBabyThree.RewardBaby.Count; i++)
            {
                value.BabyDatas.Add(new BabyDataLucky(boxBabyThree.RewardBaby[i]));
            }
            return value;
        }
        public void SaveData()
        {

            string jsonData = JsonUtility.ToJson(this.Animaldatas);
            PlayerPrefs.SetString("PlayerData", jsonData);
            string jsonDataCandy = JsonUtility.ToJson(this.Candydatas);
            PlayerPrefs.SetString("PlayerDataCandy", jsonDataCandy);
            string jsonUserData = JsonUtility.ToJson(this.useData);
            PlayerPrefs.SetString("UserData", jsonUserData);

            //luucky
            string jsonDataCandyLucKy = JsonUtility.ToJson(this.BoxUserDatas);
            PlayerPrefs.SetString("PlayerDataCandyLucKy", jsonDataCandyLucKy);
        }
        public void SetAnimalOnStock(string id, int number)
        {
            if (this.Animaldatas == null || this.Animaldatas.animaldatas == null)
            {
                Console.WriteLine("Animaldatas or animaldatas is null.");
                return;
            }
            for (int i = 0; i < this.Animaldatas.animaldatas.Count; i++)
            {
                if (this.Animaldatas.animaldatas[i].id == id && this.Animaldatas.animaldatas[i].isDone == false )
                {
                    this.Animaldatas.animaldatas[i].number += number;
                    this.useData.numberBaby++;
                    this.Animaldatas.animaldatas[i].isDone = true;
                    return;
                }else if (this.Animaldatas.animaldatas[i].id == id && this.Animaldatas.animaldatas[i].isDone == true)
                {
                    this.Animaldatas.animaldatas[i].number +=number;
                    return;
                }
            }

            //Animaldata animalData = new Animaldata(id);
            //animalData.slot = SetSlotBabyOnStore();
            //this.Animaldatas.animaldatas.Add(new Animaldata(id));
            this.Animaldatas.animaldatas.Add(new Animaldata(id));
            //SetSlotBabyOnStore(id);
        }
        public void SetSlotBabyOnStore(string id)
        {
            for (int i = 0; i < this.useData.slots.Count; i++)
            {
                if (this.useData.slots[i] != "A0000") continue;
                this.useData.slots[i] = id;
                return;
            }

        }
        public void SetCandyOnStock(string id, int number)
        {
            if (this.Candydatas == null || this.Candydatas.candydatas == null)
            {
                Console.WriteLine("Animaldatas or animaldatas is null.");
                return;
            }

            for (int i = 0; i < this.Candydatas.candydatas.Count; i++)
            {
                if (this.Candydatas.candydatas[i].id == id)
                {
                    //this.Candydatas.candydatas[i].number++;
                    this.Candydatas.candydatas[i].number += number;
                    return;
                }
            }
            this.Candydatas.candydatas.Add(new Candydata(id));
        }
        public BoxUserData GetBoxUserDatabyType(TypeBox typeBox)
        {
            return BoxUserDatas.data.Find(a => { return a.typeBox == typeBox; });
        }

    }
}

