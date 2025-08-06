
using System.Collections;
using UnityEngine;

namespace Rubik.Unbox
{

public class NetworkSettingsOpener : MonoBehaviour
{

    public static NetworkSettingsOpener Instance;
    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }
    //private void Start()
    //{
    //    CheckInternet();
    //}
    //private bool isLoadingData = false; // Biến kiểm soát tránh gọi LoadData nhiều lần

    //private void Start()
    //{
    //   // StartCoroutine(CheckInternetRoutine()); // Chạy coroutine kiểm tra mạng
    //}

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
//    }

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

#if UNITY_IOS
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void OpenIOSSettings();

    private void OpenIOSNetworkSettings()
    {
        OpenIOSSettings();
    }
#endif

    //public void CheckInternet()
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        OpenNetworkSettings();
    //    }
    //    else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
    //    { 
    //        Debug.Log("have wifi");

    //        LoadData();
    //    }
    //    else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
    //    {
    //        Debug.Log("have internet");
    //        LoadData();
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
    //    //UserDataController.instance.LoadData();
    //    //DataAssets.instance.LoadData();
    //    //DataAssets.instance.LoadScene("StartGame");

    //}
}



}