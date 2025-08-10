using UnityEngine;
using UnityEngine.EventSystems;
using Rubik.ClawMachine;

namespace Rubik.MergeGame
{

    public class JoyStick : MonoBehaviour
    {
        public static JoyStick instance;
        private void Awake()
        {
            if (JoyStick.instance != null) Debug.LogError("Only 1 TalentPlayer allow");
            JoyStick.instance = this;
        }
        public GameObject joystick;
        public GameObject joystickBG;
        public Vector2 joystickVec;
        private Vector2 joystickTouchPos;
        private Vector2 joystickOriginalPos;
        public float joystickRadius;
        public float value;
        public PlayerController playerController;

        public float Horizontal { get; internal set; }
        public float Vertical { get; internal set; }

        private void Start()
        {
            this.joystickOriginalPos = joystickBG.transform.position;
            joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        }
        public void PointerDown()
        {
            this.joystickBG.transform.position = Input.mousePosition;
            this.joystickTouchPos = Input.mousePosition;
            Vector3 worldPos = playerController.transform.position;
            worldPos.x = Camera.main.ScreenToWorldPoint(this.joystickTouchPos).x;
            playerController.transform.position = worldPos;

        }

        public void Drag(BaseEventData baseEventData)
        {
            PointerEventData pointerEventData = baseEventData as PointerEventData;
            Vector2 dragPos = pointerEventData.position;
            this.joystickVec = (dragPos - joystickTouchPos).normalized;

            float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
            float scale = joystickDist / this.joystickRadius;
            if (scale > 1) scale = 1;
            this.joystickVec *= scale;
            this.joystick.transform.position = this.joystickTouchPos + joystickVec * this.joystickRadius;
            this.value = Mathf.Sqrt(joystickVec.x * joystickVec.x + joystickVec.y * joystickVec.y);
        }
        public void PointerUp()
        {
            SoundController.instance.StartMerdAudio();
            ThrowFruitController.instance.SpwanFruitOnclick();
            this.joystickVec = Vector2.zero;
            this.joystick.GetComponent<RectTransform>().localPosition = Vector3.zero;
            this.joystickBG.transform.position = this.joystickOriginalPos;
        }

    }
}