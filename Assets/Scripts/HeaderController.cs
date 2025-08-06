using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class HeaderController : MonoBehaviour
    {
        public static HeaderController instance;
        public GoldPracticePanel goldPracticePanel;
        public GoldTestPanel goldTestPanel;
        private void Awake()
        {
            if(instance == null)
            instance = this;
        }
        private void Start()
        {
            
        }
        public void SetHeader()
        {
            //if (UserManager.instance.mathType == MathType.Test)
            //{
            //    this.goldPracticePanel.gameObject.SetActive(false);
            //    this.goldTestPanel.gameObject.SetActive(true);
            //    this.goldTestPanel.Init();
            //}
            //else
            //{
            //    this.goldPracticePanel.gameObject.SetActive(true);
            //    this.goldTestPanel.gameObject.SetActive(false);
            //    this.goldPracticePanel.Init();
            //}
        }
    }
}