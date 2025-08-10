using UnityEngine;
using UnityEngine.UIElements;


namespace Rubik.ClawMachine
{
    public class ScaleGameObject : MonoBehaviour
    {
        //void Start()
        //{
        //    ScaleToScreenSize();
        //}
        //void ScaleToScreenSize()
        //{
        //    float screenWidth = Screen.width;
        //    float screenHeight = Screen.height;
        //    Vector3 objectSize = GetComponent<Renderer>().bounds.size;
        //    float scaleX = screenWidth / objectSize.x;
        //    float scaleY = screenHeight / objectSize.y;
        //    transform.localScale = new Vector3(scaleX, scaleY, 1);
        //}
        private float referenceWidth = 1080f;
        private float referenceHeight = 1920f;

        void Start()
        {
            ScaleToScreenSize();
        }

        private void Update()
        {
            //ScaleToScreenSize();
        }

        void ScaleToScreenSize()
        {
            float x = transform.localScale.x;
            float y = transform.localScale.y;
            Debug.Log("x" + x);
            Debug.Log("y" + y);
            //transform.localScale = new Vector3(1, 1, 1);
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float scaleX = screenWidth / referenceWidth;
            float scaleY = screenHeight / referenceHeight;
            Debug.Log("screenWidth" + screenWidth);
            Debug.Log("screenHeight" + screenHeight);
            Debug.Log("referenceWidth" + referenceWidth);
            Debug.Log("referenceHeight" + referenceHeight);
            //float scale = Mathf.Min(scaleX, scaleY);
            transform.localScale = new Vector3(x*scaleX, y*scaleY, 1);
            Debug.Log($"scaleX: {x * scaleX}, scaleY: {y*scaleY}");
            //transform.localScale = new Vector3(scale, scale, 1);
        }

        //public RectTransform uiImage; // Kéo thả ảnh UI vào đây trong Unity Inspector
        //private float referenceWidth = 1080f;
        //private float referenceHeight = 1920f;

        //void Start()
        //{
        //    ScaleToImage();
        //}
        //private void Update()
        //{
        //    ScaleToImage();
        //}

        //void ScaleToImage()
        //{
        //    //transform.localScale = new Vector3(1, 1, 1);
        //    // Lấy kích thước của ảnh trong UI
        //    float imageWidth = uiImage.rect.width;
        //    float imageHeight = uiImage.rect.height;
        //    Debug.Log("imageWidth" + imageWidth);
        //    Debug.Log("imageHeight" + imageHeight);
        //    // Tính toán tỷ lệ với kích thước ảnh trong UI
        //    float scaleX = imageWidth / referenceWidth;
        //    float scaleY = imageHeight / referenceHeight;
        //    transform.localScale = new Vector3(scaleX, scaleY, 1);
        //}

        //public RectTransform uiImage; // Kéo thả ảnh UI vào đây trong Unity Inspector
        //public SpriteRenderer spriteRenderer; // Kéo thả SpriteRenderer vào đây trong Unity Inspector

        //void Start()
        //{
        //    //spriteRenderer = GetComponent<SpriteRenderer>();
        //    MatchSpriteToUIImage();
        //}

        //void MatchSpriteToUIImage()
        //{
        //    if (uiImage != null && spriteRenderer != null)
        //    {
        //        // Lấy kích thước của ảnh trong UI
        //        float imageWidth = uiImage.rect.width;
        //        float imageHeight = uiImage.rect.height;
        //        Debug.Log("imageWidth" + imageWidth);
        //        Debug.Log("imageHeight" + imageHeight);
        //        // Lấy kích thước gốc của Sprite
        //        //float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        //        //float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        //        float spriteWidth = spriteRenderer.sprite.textureRect.width;
        //        float spriteHeight = spriteRenderer.sprite.textureRect.height;
        //        Debug.Log("spriteWidth" + spriteWidth);
        //        Debug.Log("spriteHeight" + spriteHeight);
        //        // Tính toán tỷ lệ để khớp kích thước
        //        float scaleX = imageWidth / spriteWidth;
        //        float scaleY = imageHeight / spriteHeight;

        //        // Áp dụng tỷ lệ vào SpriteRenderer
        //        spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("uiImage hoặc spriteRenderer chưa được gán trong Inspector.");
        //    }
        //}
    }
}