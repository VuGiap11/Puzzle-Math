using UnityEngine;

namespace Rubik.ClawMachine
{
    public enum RarityAnimal
    {
        Common = 0,
        Prenium = 1,
        Legendary = 2,
        Mythical = 3
    }
    [CreateAssetMenu(fileName = "Data", menuName = "AnimalData")]
    public class AnimalData : ScriptableObject
    {
        public string Id;
        public Sprite Avatar;
        public string Name;
        //public GameObject AnimalObj;
        public RarityAnimal RarityAnimal;
        public float valueScale;
        //public int slot;
    }
}