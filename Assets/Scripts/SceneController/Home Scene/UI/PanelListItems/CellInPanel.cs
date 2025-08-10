using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NailSalonGame
{
    public class CellInPanel : MonoBehaviour
    {
        [SerializeField] ItemInPanel[] arrayItem;
        public void SetCell(List<ItemModel> itemsModel)
        {
            for(int i=0; i< arrayItem.Length; i++)
            {
                if (i < itemsModel.Count)
                {
                    arrayItem[i].SetItem(itemsModel[i]);
                }
                else
                {
                    arrayItem[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
