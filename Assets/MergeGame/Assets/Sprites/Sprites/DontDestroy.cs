using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    //private static DontDestroy instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {

    //        Destroy(gameObject); // Nếu đã có instance, hủy đối tượng mới
    //    }
    //}
    private static DontDestroy instance;
    [SerializeField] private Canvas canvasHud;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += (scene, mode) => canvasHud.worldCamera = Camera.main;
        }
        else Destroy(gameObject);
        AssignCameraAndLayer();
        //if (canvasHud) canvasHud.worldCamera = Camera.main;
    }
    private void AssignCameraAndLayer()
    {
        if (canvasHud)
        {
            canvasHud.renderMode = RenderMode.ScreenSpaceCamera; // Đặt chế độ Screen Space - Camera
            canvasHud.worldCamera = Camera.main; // Gán camera chính
            canvasHud.sortingOrder = 15;
        }
    }
}

