using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.MergeGame
{

    public class TriggerLoss : MonoBehaviour
    {
        private float _timer = 0f;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _timer += Time.deltaTime;
                if (_timer > GameManager.instance.TimeTillGameOver)
                {
                    Debug.Log("gamover");
                    //GameManager.instance.GameOver();
                    if (GameManager.instance.isOpen == false)
                    {
                        GameManager.instance.isOpen = true;
                        PopupManager.Instance.OnUI(PopupCode.WinLoseMergePanel);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _timer = 0f;
            }
        }
    }

}