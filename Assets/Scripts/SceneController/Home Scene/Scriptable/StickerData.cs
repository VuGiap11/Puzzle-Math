using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NailSalonGame
{
    [CreateAssetMenu(fileName = "ScriptableObject/StickerData")]
    public class StickerData : ScriptableObject
    {
        public List<StickerModelInfo> stickers;

        
    }

    public class StickerModelInfo
    {

    }
}