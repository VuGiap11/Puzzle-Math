using NTPackage.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rubik.ClawMachine
{
    //public class JoyStick : MonoBehaviour
    //{
    //    //{
    //    //    public static JoyStick instance;
    //    //    private void Awake()
    //    //    {
    //    //        if (JoyStick.instance != null) Debug.LogError("Only 1 TalentPlayer allow");
    //    //        JoyStick.instance = this;
    //    //    }

    //    //    public GameObject joystick;
    //    //    public GameObject joystickBG;
    //    //    public Vector2 joystickVec;
    //    //    private Vector2 joystickTouchPos;
    //    //    private Vector2 joystickOriginalPos;
    //    //    private float joystickRadius;
    //    //    public float value;

    //    //    public float Horizontal { get; internal set; }
    //    //    public float Vertical { get; internal set; }

    //    //    private void Start()
    //    //    {
    //    //        this.joystickOriginalPos = joystickBG.transform.position;
    //    //        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    //    //    }

    //    //    public void PointerDown()
    //    //    {
    //    //        this.joystickBG.transform.position = Input.mousePosition;
    //    //        this.joystickTouchPos = Input.mousePosition;
    //    //    }

    //    //    public void Drag(BaseEventData baseEventData)
    //    //    {
    //    //        PointerEventData pointerEventData = baseEventData as PointerEventData;
    //    //        Vector2 dragPos = pointerEventData.position;
    //    //        this.joystickVec = (dragPos - joystickTouchPos).normalized;

    //    //        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
    //    //        float scale = joystickDist / this.joystickRadius;
    //    //        if (scale > 1) scale = 1;
    //    //        this.joystickVec *= scale;
    //    //        this.joystick.transform.position = this.joystickTouchPos + joystickVec * this.joystickRadius;
    //    //        this.value = Mathf.Sqrt(joystickVec.x * joystickVec.x + joystickVec.y * joystickVec.y);
    //    //    }

    //    //    public void PointerUp()
    //    //    {
    //    //        this.joystickVec = Vector2.zero;
    //    //        this.joystick.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //    //        this.joystickBG.transform.position = this.joystickOriginalPos;
    //    //    }
    //    public GameObject joystick;
    //    //public GameObject joystickBG;
    //    public Vector2 joystickVec; // Giữ lại biến này để sử dụng sau
    //    public Vector2 joystickTouchPos;

    //    private void Start()
    //    {
    //        // Giữ nguyên vị trí ban đầu của joystickBG
    //        joystickTouchPos = joystick.transform.position;
    //        //joystickTouchPos = joystick.transform.localScale;
    //    }
    //    public void PointerDown()
    //    {
    //        Debug.Log("down");
    //        // Lưu vị trí chạm ban đầu khi bắt đầu kéo
    //        joystickTouchPos = Input.mousePosition;
    //    }
    //    public void Drag(BaseEventData baseEventData)
    //    {
    //        Vector2 dragPos = ((PointerEventData)baseEventData).position;
    //        joystickVec = (dragPos - joystickTouchPos).normalized;
    //        //if (joystickVec.y >= 0 && joystickVec.x <= 0) return;// Cập nhật joystickVec
    //        //if (joystickVec.y <= 0 && joystickVec.x <= 0) return;
    //        float angle = Mathf.Atan2(joystickVec.x, joystickVec.y) * Mathf.Rad2Deg;
    //        joystick.transform.localRotation = Quaternion.Euler(0, 0, -angle); // Quay joystick
    //        //Vector2 dragPos = ((PointerEventData)baseEventData).position;
    //        //joystickVec = (dragPos - joystickTouchPos).normalized;
    //        //float verticalMovement = joystickVec.y;
    //        //float angle = Mathf.Atan2(verticalMovement, 0) * Mathf.Rad2Deg;
    //        //joystick.transform.localRotation = Quaternion.Euler(0, 0, angle);
    //    }

    //    public void PointerUp()
    //    {
    //        // Đặt lại góc quay của joystick về ban đầu
    //        //joystick.transform.localRotation = Quaternion.identity;
    //        joystick.transform.localRotation = transform.rotation;
    //        joystickVec = Vector2.zero; // Đặt lại joystickVec về 0
    //    }
    //}
    public class JoyStick : MonoBehaviour
    {

        public GameObject joystick;
        public Vector2 joystickVec; // Giữ lại biến này để sử dụng sau
        public Vector2 joystickTouchPos;
        public bool isMoveJoystick = false;

        private void Start()
        {
            // Giữ nguyên vị trí ban đầu của joystick
            joystickTouchPos = joystick.transform.position;
        }

        private void OnMouseDown()
        {

            //if (ClawGameManager.Instance.statusGame == StatusGame.Store) return;

            Debug.Log("down");
            joystickTouchPos = Input.mousePosition;
        }
        private void OnMouseDrag()
        {

            //if (ClawGameManager.Instance.ClowController.isPlay) return;
            if (SceneController.Instance.statusGame != StatusGame.StartGame) return;
            //if (UserManager.instance.useData.numberCoin <= 0)
            //{
            //    PopupManager.Instance.OnUI(PopupCode.AdsPanel);
            //    return;
            //}
            Vector2 dragPos = Input.mousePosition;
            joystickVec = (dragPos - joystickTouchPos).normalized;

            float angle = Mathf.Atan2(joystickVec.x, joystickVec.y) * Mathf.Rad2Deg;
            if (angle <= -45)
            {
                angle = -45;
            }
            else if (angle >= 45)
            {
                angle = 45;

            }
            joystick.transform.localRotation = Quaternion.Euler(0, 0, -angle); // Quay joystick
        }

        private void OnMouseUp()
        {
            // Đặt lại góc quay của joystick về ban đầu
            joystick.transform.localRotation = Quaternion.identity;
            //joystick.transform.localRotation = transform.rotation;
            joystickVec = Vector2.zero; // Đặt lại joystickVec về 0
        }

    }
}