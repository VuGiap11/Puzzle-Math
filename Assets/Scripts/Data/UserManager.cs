using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Animaldata
{
    public string id;
    public bool isDone;
    public Animaldata(string Id)
    {
        this.id = Id;
        this.isDone = false;
    }
}
[Serializable]
public class Animaldatas
{
    public List<Animaldata> animaldatas;
}
[Serializable]
public class UseData
{
    public int gold;
    public List<string> babyBoughts;
    public string namePlayer;
    public int idAvar;
    public int highScore;
    //public bool isRemoveAds;
}
namespace Rubik.math
{
    public class UserManager : MonoBehaviour
    {
        public static UserManager instance;
        public Animaldatas Animaldatas;
        public UseData useData;
        //public MathType mathType;
        //public OperationType operationType;
        public string IdAvar;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public void LoadData()
        {
            string jsonData = PlayerPrefs.GetString("PlayerData");
            if (!string.IsNullOrEmpty(jsonData))
            {

                Animaldatas = JsonUtility.FromJson<Animaldatas>(jsonData);
                Debug.LogWarning("No saved 1 ");
            }
            else
            {
                Debug.LogWarning("No saved ");

                for (int i = 0; i < DataAssets.instance.babyDatas.Count; i++)
                {
                    this.Animaldatas.animaldatas.Add(new Animaldata(DataAssets.instance.babyDatas[i].Id));
                }
                this.Animaldatas.animaldatas[0].isDone = true;
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
                this.useData.gold = 200;
                this.useData.babyBoughts.Add(DataAssets.instance.babyDatas[0].Id);
                this.useData.namePlayer = "Player";
                this.useData.idAvar = 0;
                this.useData.highScore = 0;
                //this.useData.isRemoveAds = false;
            }
            SaveData();
        }
        public void SaveData()
        {

            string jsonData = JsonUtility.ToJson(this.Animaldatas);
            PlayerPrefs.SetString("PlayerData", jsonData);
            string jsonUserData = JsonUtility.ToJson(this.useData);
            PlayerPrefs.SetString("UserData", jsonUserData);
        }
        public Animaldata GetAnimalDatabyID(string id)
        {
            return Animaldatas.animaldatas.Find(a => { return a.id == id; });
        }

        public void RandomIdAvar()
        {
            int numberBaby = UnityEngine.Random.Range(0, this.useData.babyBoughts.Count);
            this.IdAvar = UserManager.instance.useData.babyBoughts[numberBaby];
        }
        //private int number1, number2, result;
        //public Number GenerateRandomSumExpression()
        //{
        //    switch (this.operationType)
        //    {
        //        case OperationType.Addition:
        //            this.number1 = UnityEngine.Random.Range(0, 100);
        //            this.number2 = UnityEngine.Random.Range(0, 100 - number1);
        //            this.result = number1 + number2;
        //            break;
        //        case OperationType.Subtraction:
        //            this.result = UnityEngine.Random.Range(0, 100);
        //            int number = 100 - result;
        //            this.number2 = UnityEngine.Random.Range(0, number);
        //            this.number1 = number2 + result;
        //            break;
        //        case OperationType.Multipacation:
        //            this.number1 = UnityEngine.Random.Range(1, 11);
        //            this.number2 = UnityEngine.Random.Range(1, 11);
        //            this.result = number1 * number2;
        //            break;
        //        case OperationType.Division:
        //            this.result = UnityEngine.Random.Range(1, 11);
        //            this.number2 = UnityEngine.Random.Range(1, 11);
        //            this.number1 = number2 * result;
        //            break;
        //    }
        //    return new Number(number1, number2, result);
        //}

        //public void SetType()
        //{
        //    if (this.mathType == MathType.Test)
        //    {
        //        OperationType[] operations = new OperationType[]
        //   {
        //    OperationType.Addition,
        //    OperationType.Subtraction,
        //    OperationType.Multipacation,
        //    OperationType.Division
        //   };

        //        System.Random random = new System.Random();
        //        int index = random.Next(operations.Length);
        //        this.operationType = operations[index];
        //    }
        //    else if (this.mathType == MathType.Addition)
        //    {
        //        this.operationType = OperationType.Addition;
        //    }
        //    else if (this.mathType == MathType.Subtraction)
        //    {
        //        this.operationType = OperationType.Subtraction;
        //    }
        //    else if (this.mathType == MathType.Multipacation)
        //    {
        //        this.operationType = OperationType.Multipacation;
        //    }
        //    else if (this.mathType == MathType.Division)
        //    {
        //        this.operationType = OperationType.Division;
        //    }

        //}
    }
}