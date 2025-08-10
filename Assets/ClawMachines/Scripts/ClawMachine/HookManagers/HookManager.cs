using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
//using UnityEngine.Rendering.UI;

namespace Rubik.ClawMachine
{
    public class HookManager : MonoBehaviour
    {
        public static HookManager instance;
        [SerializeField] private GameObject hook;
        [SerializeField] private float speedHook;
        [SerializeField] private float maxX, minX;

        public Rigidbody2D rb;
        private Vector3 initialPosition;

        public bool isDown;
        private Tweener hookTweener;
        private Vector3 temp;
        [SerializeField] private LineRender line;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        void Start()
        {
            rb = hook.GetComponent<Rigidbody2D>();
            //rb.useGravity = true;


        }
        private void Update()
        {
            //MoveLeft();
            //MoveRight();
        }
        public void MoveDown()
        {
            initialPosition = hook.transform.position;
            line.startPointLine = hook.transform.position;
            rb.gravityScale = 1;
            isDown = true;
        }
        public void MoveBack()
        {
            if (moveBack != null) StopCoroutine(moveBack);
            moveBack = StartCoroutine(ResetPositionAfterDelay(0.5f));
        }
        Coroutine moveBack;
        private IEnumerator ResetPositionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            //rb.velocity = Vector3.zero; // Đặt vận tốc về 0 để tránh di chuyển không mong muốn
            //transform.position = initialPosition;
            Debug.Log("rb" + rb.gravityScale);
            rb.gravityScale = 0;
            hookTweener = hook.transform.DOMoveY(initialPosition.y, 10f, false)
         .SetSpeedBased(true)
         .OnUpdate(delegate
         {
             temp = hook.transform.position;
             line.MakeLineRender(temp);
         }).OnComplete(delegate
         {

         });

        }

        public void MoveLeft()
        {
            if (isDown) return;
            Vector3 temp = transform.position;
            temp.x -= speedHook * Time.deltaTime;
            if (temp.x <= minX)
            {
                temp.x = minX;
            }
            transform.position = temp;
        }

        public void MoveRight()
        {
            if (isDown) return;
            Vector3 temp = transform.position;
            temp.x += speedHook * Time.deltaTime;
            if (temp.x >= maxX)
            {
                temp.x = maxX;
            }
            transform.position = temp;
        }
    }
}