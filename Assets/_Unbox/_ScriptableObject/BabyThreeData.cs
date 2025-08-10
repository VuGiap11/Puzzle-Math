using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rubik.Unbox
{
    public enum TypeBox
    {
        Type1 =0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3
    }

    [CreateAssetMenu(fileName = "Data", menuName = "BabyThree")]
    public class BabyThreeData : ScriptableObject
    {
        public string Id;
        public Sprite Avatar;
        public TypeBox typeBaby;
    }
}