using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    [CreateAssetMenu(fileName = "Data", menuName = "BabyData")]
    public class BabyData : ScriptableObject
    {
        public string Id;
        public Sprite Avatar;
        public int Price;
    }
}