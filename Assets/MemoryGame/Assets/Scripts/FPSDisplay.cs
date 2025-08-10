using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NTPackage;
using NTPackage.Functions;

namespace Rubik.TimeCity
{
    public class FPSDisplay : MonoBehaviour
    {
       public TextMeshProUGUI fpsDisPlay;
        public TextMeshProUGUI FpsDisPlay
        {
            get
            {
                this.LoadFpsDisPlay();
                return this.fpsDisPlay;
            }
        }


        public void LoadComponents()
        {
            // base.LoadComponents();
            this.LoadFpsDisPlay();
        }

        protected void LoadFpsDisPlay()
        {
            if (this.fpsDisPlay != null) return;
            this.fpsDisPlay = transform.GetComponent<TextMeshProUGUI>();
        }

        public float pollingTime = 1f;
        public float time = 0;
        public int frameCount = 0;
        private void Update()
        {
            time += Time.deltaTime;
            frameCount++;
            if (this.time >= this.pollingTime)
            {
                int frameRate = Mathf.RoundToInt(this.frameCount / time);
                //this.FpsDisPlay.text = "FPS " + frameRate.ToString();
                time -= pollingTime;
                frameCount = 0;
            }
        }
    }
}