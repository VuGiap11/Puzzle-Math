using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rubik.LuckyGame
{
    //loai box mà ng??i dùng thu th?p ???c
    [Serializable]
    public class BoxUserData
    {
        //tyoe
        // amount;
        public TypeBox typeBox;
        public string IdBabyHold;
        public List<BabyDataLucky> BabyDatas;
    }
    [Serializable]
    public class BabyDataLucky
    {
        public string id;
        public int amount;
        public bool isDone;
        public BabyDataLucky(string id)
        {
            this.id = id;
            this.amount = 0;
            this.isDone = false;
        }
    }
}

