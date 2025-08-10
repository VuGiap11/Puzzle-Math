using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NailSalonGame
{
    public class ItemInPanel : MonoBehaviour
    {
        [SerializeField] Image imageItemSticker, imageItemColorNail, imageItemPatern;
        public void SetItem(ItemModel itemModel)
        {
            Material mat = HomeSceneController.Instance.mat;
            imageItemSticker.gameObject.SetActive(false);
            imageItemColorNail.gameObject.SetActive(false);
            imageItemPatern.transform.parent.gameObject.SetActive(false);
            if (itemModel.itemType == ItemType.Patern)
            {
                imageItemPatern.transform.parent.gameObject.SetActive(true);
                imageItemPatern.sprite = itemModel.getSprite();
                if (itemModel.unlock) imageItemPatern.color = Color.black;
                else
                {
                    imageItemPatern.color = new Color(0.35f, 0.35f, 0.35f, 1);
                    imageItemPatern.transform.parent.GetComponent<Image>().material =mat;
                }
            }
            else if(itemModel.itemType == ItemType.NailColor)
            {
                imageItemColorNail.gameObject.SetActive(true);
                imageItemColorNail.sprite = itemModel.getSprite();
                if (itemModel.unlock) imageItemColorNail.color = new Color(1, 1, 1, 1);
                else imageItemColorNail.GetComponent<Image>().material = mat;
            }
            else
            {
                imageItemSticker.gameObject.SetActive(true);
                imageItemSticker.sprite = itemModel.getSprite();
                
                // Set native size while maintaining aspect ratio
                imageItemSticker.SetNativeSize();
                
                // Get the container's size
                RectTransform container = imageItemSticker.transform.parent.GetComponent<RectTransform>();
                float containerWidth = container.rect.width;
                float containerHeight = container.rect.height;
                
                // Get the image's current size
                RectTransform imageRect = imageItemSticker.GetComponent<RectTransform>();
                float imageWidth = imageRect.rect.width;
                float imageHeight = imageRect.rect.height;
                
                // Calculate scale factor to fit within container while maintaining aspect ratio
                float scaleX = containerWidth / imageWidth;
                float scaleY = containerHeight / imageHeight;
                float scale = Mathf.Min(scaleX, scaleY);
                
                // Apply the scale
                imageRect.localScale = new Vector3(scale, scale, 1f);
                
                if (itemModel.unlock) imageItemSticker.color = new Color(1, 1, 1, 1);
                else imageItemSticker.GetComponent<Image>().material = mat;
            }
            // unlock state show
            //if (itemModel.unlock)
            //{

            //}
            //else
            //{

            //}
        }
    }
}