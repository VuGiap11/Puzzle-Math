using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class HookCollision : MonoBehaviour
    {
        //private Rigidbody2D rb;

        //private void Start()
        //{
        //    rb = GetComponent<Rigidbody2D>();
        //}

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("ground"))
            {
                HookManager.instance.MoveBack();
            }
        }
    }
}