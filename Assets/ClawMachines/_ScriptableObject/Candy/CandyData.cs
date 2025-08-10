
using UnityEngine;

namespace Rubik.ClawMachine
{
    public enum Rarity
    {
        Common = 0,
        Prenium = 1,
        Legendary = 2,
        Mythical = 3
    }

    [CreateAssetMenu(fileName = "Data", menuName = "Candy")]
    public class CandyData : ScriptableObject
    {
        public string Id;
        public Sprite Avatar;
        public Sprite AvatarLeftOff;
        public Sprite AvatarRightOff;
        public string Name;
        public Rarity Rarity;
        public int percent;
    }
}