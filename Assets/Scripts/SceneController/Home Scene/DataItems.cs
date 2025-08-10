using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NailSalonGame
{
    public class DataItems : MonoBehaviour
    {
        public static DataItems Instance;
        private void Awake()
        {
            Instance = this;
        }

        public void UnlockItem(ItemType _type, int _id)
        {
            ItemModel item = new ItemModel(_type, _id);
            item.unlock = true;
        }
        public void LockItem(ItemType _type, int _id)
        {
            ItemModel item = new ItemModel(_type, _id);
            item.unlock = false;
        }
        public ItemModel GetItem(ItemType _type, int _id)
        {
            ItemModel item = new ItemModel(_type, _id);
            return item;
        }
    }
    public class ItemModel
    {
        public ItemModel(ItemType _type, int _id)
        {
            this.itemType = _type;
            this.id = _id;
        }
        public ItemType itemType;
        public int id;
        public bool unlock
        {
            set
            {
                PlayerPrefs.SetInt("UnlockStateItem_" + (int)itemType + id, value == true ? 1 : 0);
            }
            get
            {
                int _unlockState = PlayerPrefs.GetInt("UnlockStateItem_" + (int)itemType + id, 0);
                return _unlockState == 0 ? false : true;
            }
        }
        public Sprite getSprite()
        {
            Sprite s = null;
            string path = "Items/";
            switch (itemType)
            {
                case ItemType.Sticker:
                    path += "Stickers/";
                    break;
                case ItemType.NailColor:
                    path += "Bottle/";
                    break;
                case ItemType.Patern:
                    path += "Patern/";
                    break;
                default:
                    path += "Stickers/";
                    break;
            }
            path += id;
            s = Resources.Load<Sprite>(path);
            if (s == null) Debug.Log("Miss "+path);
            return s;
        }
    }
    public enum ItemType
    {
        Sticker,
        NailColor,
        Patern
    }
}