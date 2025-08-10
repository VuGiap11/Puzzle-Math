using Rubik.ClawMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSettingsOpener : MonoBehaviour
{
    public static NetworkSettingsOpener Instance;
    private bool isLoadingData = false; // Biến kiểm soát tránh gọi LoadData nhiều lần

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }
    //private void Start()
    //{
    //    //if (checkInternet != null)
    //    //{
    //    //    StopCoroutine(checkInternet);
    //    //}
    //    //StartCoroutine(CheckInternetRoutine()); // Chạy coroutine kiểm tra mạng
    //}
    //Coroutine checkInternet;
    //private IEnumerator CheckInternetRoutine()
    //{
    //    while (true)
    //    {
    //        CheckInternet();
    //        yield return new WaitForSeconds(5f); // Kiểm tra mỗi 5 giây
    //    }
    //}

//    public void OpenNetworkSettings()
//    {
//#if UNITY_ANDROID
//        OpenAndroidNetworkSettings();
//#elif UNITY_IOS
//        OpenIOSNetworkSettings();
//#else
//        Debug.Log("Chức năng này chỉ hoạt động trên Android và iOS.");
//#endif
////    }

//#if UNITY_ANDROID
//    private void OpenAndroidNetworkSettings()
//    {
//        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
//        {
//            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
//            {
//                using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.settings.WIRELESS_SETTINGS"))
//                {
//                    currentActivity.Call("startActivity", intent);
//                }
//            }
//        }
//    }
//#endif

//#if UNITY_IOS
//    [System.Runtime.InteropServices.DllImport("__Internal")]
//    private static extern void OpenIOSSettings();

//    private void OpenIOSNetworkSettings()
//    {
//        OpenIOSSettings();
//    }
//#endif
    //public void CheckInternet()
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        Debug.Log("Không có kết nối mạng.");
    //        isLoadingData = false; // Reset trạng thái load dữ liệu để có thể load lại sau này
    //        OpenNetworkSettings();
    //    }
    //    else
    //    {
    //        if (!isLoadingData) // Chỉ load lại dữ liệu nếu chưa load trước đó
    //        {
    //            Debug.Log("Mạng đã kết nối, đang load dữ liệu...");
    //            LoadData();
    //            isLoadingData = true; // Đánh dấu đã load dữ liệu
    //        }
    //    }
    //}
    public bool CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Không có kết nối mạng.");
            return false;
        }
        else
        {
            return true;
        }
    }
    //public void LoadData()
    //{
      
    //    ClawDataAssets.Instance.LoadData();
    //    UserManager.instance.LoadData();
    //    RankDataManager.instance.LoadRank();
    //    ClawDataAssets.Instance.LoadScene("StartScene");
    //}
}


