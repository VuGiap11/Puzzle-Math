using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class ScaleCamera : MonoBehaviour
    {
        private float referenceWidth = 1080f;
        private float referenceHeight = 1920f;
        private float Sizecamera = 10.5f;

        private void Start()
        {
            _ScaleCamera();
        }
        public void _ScaleCamera()
        {
            //Camera cam = Camera.main;
            //if (cam == null)
            //{
            //    Debug.LogError("Camera.main không được tìm thấy.");
            //    return;
            //}
            //float screenWidth = Screen.width;
            //float screenHeight = Screen.height;
            //float scaleX = screenWidth / referenceWidth;
            //float scaleY = screenHeight / referenceHeight;
            //Debug.Log("screenWidth" + screenWidth);
            //Debug.Log("screenHeight" + screenHeight);
            //Debug.Log("scalex" + scaleX);
            //Debug.Log("scaley" + scaleY);
            //float scale = Mathf.Max(scaleX, scaleY);
            //cam.orthographicSize = Sizecamera/scale;
            //Debug.Log("cam" + cam.orthographicSize);
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Camera.main không được tìm thấy.");
                return;
            }

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Tính toán tỷ lệ màn hình so với kích thước tham chiếu
            float targetAspect = referenceWidth / referenceHeight;
            float currentAspect = screenWidth / screenHeight;
            // Tính toán orthographicSize mới cho camera
            if (currentAspect >= targetAspect)
            {
                // Màn hình rộng hơn hoặc bằng tỷ lệ tham chiếu
                cam.orthographicSize = Sizecamera;
            }
            else
            {
                // Màn hình cao hơn tỷ lệ tham chiếu, cần điều chỉnh orthographicSize
                float differenceInSize = targetAspect / currentAspect;
                cam.orthographicSize = Sizecamera * differenceInSize;
                Debug.Log("differenceInsize" + differenceInSize);
            }
           
            Debug.Log("Camera orthographicSize: " + cam.orthographicSize);
        }

        //void Start()
        //{
        //    float aspectRatio = (float)Screen.width / Screen.height;

        //    if (aspectRatio >= 2.1f) // Điện thoại siêu dài (21:9)
        //    {
        //        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        //    }
        //    else if (aspectRatio >= 1.8f) // Điện thoại phổ thông (19.5:9, 18:9)
        //    {
        //        transform.localScale = new Vector3(1.0f, 1.0f, 1);
        //    }
        //    else if (aspectRatio >= 1.5f) // Tablet hoặc điện thoại cũ (16:10, 4:3)
        //    {
        //        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        //    }
        //    else // Màn hình vuông hoặc nhỏ
        //    {
        //        transform.localScale = new Vector3(0.6f, 0.6f, 1);
        //    }
        //}
    }
}

