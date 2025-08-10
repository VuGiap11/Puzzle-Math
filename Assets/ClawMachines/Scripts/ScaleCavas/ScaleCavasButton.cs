using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleCavasButton : MonoBehaviour
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
        //var x = Screen.width;
        //var y = Screen.height;
        //scaleValue = ((float)x / y) / ((float)1080 / 1920);
        ////float a = (float)x / y;
        //if (scaleValue >= 0.75)
        //{
        //    ss.matchWidthOrHeight = 1;
        //}
        //else if (scaleValue <= 0.4)
        //{
        //    ss.matchWidthOrHeight = 0.5f;
        //}
        //else
        //{
        //    ss.matchWidthOrHeight = (scaleValue);
        //}
        var x = Screen.width;
        var y = Screen.height;
        scaleValue = ((float)x / y) / ((float)1080 / 1920);
        float a = (float)x / y;
        if (a >= 0.75)
        {
            ss.matchWidthOrHeight = 0.8f;
        }
        else if (a <= 0.4)
        {
            ss.matchWidthOrHeight = 0.5f;
        }
        else
        {
            ss.matchWidthOrHeight = (scaleValue);
            //ss.matchWidthOrHeight = (int)(scaleValue);
        }
    }
}