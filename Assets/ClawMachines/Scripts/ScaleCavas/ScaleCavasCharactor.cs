
using UnityEngine;
//using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class ScaleCavasCharactor : MonoBehaviour
{
    private float scaleValue;
    CanvasScaler ss;
    void Awake()
    {
        ss = this.GetComponent<CanvasScaler>();

         ScaleScr();
        //Scale();
    }
    public void ScaleScr()
    {

        //var x = Screen.width;
        //var y = Screen.height;
        //scaleValue = ((float)x / y) / ((float)1080 / 1920);
        //ss.matchWidthOrHeight = (int)(scaleValue);
        ////ss.matchWidthOrHeight = (scaleValue);
        ///
        var x = Screen.width;
        var y = Screen.height;
        scaleValue = ((float)x / y) / ((float)1080 / 1920);
        float a = (float)x / y;
        if (a >= 0.75)
        {
            ss.matchWidthOrHeight = 0.8f;
        }else if (a <= 0.4)
        { 
            ss.matchWidthOrHeight = 0.5f;
        }
        else
        {
            ss.matchWidthOrHeight = (scaleValue);
            //ss.matchWidthOrHeight = (int)(scaleValue);
        }
    }
    public void Scale()
    {
        //float aspectRatio = (float)Screen.height / Screen.width;
        //Debug.Log("Aspect Ratio: " + aspectRatio);
        //Debug.Log("Screen.width Ratio: " + Screen.width);
        //Debug.Log("Screen.height Ratio: " + Screen.height);
        //if (aspectRatio >= 2.1f) // Điện thoại siêu dài (21:9)
        //{
        //    ss.matchWidthOrHeight = 0.3f;
        //}
        //else if (aspectRatio >= 1.8f) // Điện thoại phổ thông (19.5:9, 18:9)
        //{
        //    ss.matchWidthOrHeight = 0f;
        //}
        //else if (aspectRatio >= 1.5f) // Tablet hoặc điện thoại cũ (16:10, 4:3)
        //{
        //    ss.matchWidthOrHeight = 0f;
        //}
        ////else if (aspectRatio >= 1.2f) // Màn hình gập (Galaxy Z Fold 2 - 5:4)
        ////{
        ////    ss.matchWidthOrHeight = 0.3f; // Giá trị tùy chỉnh
        ////}
        //else // Màn hình vuông hoặc nhỏ
        //{
        //    ss.matchWidthOrHeight = 1;
        //}

        //Debug.Log("ss.matchWidthOrHeight: " + ss.matchWidthOrHeight);
    }
}
