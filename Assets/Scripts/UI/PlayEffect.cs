using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class PlayEffect : MonoBehaviour
    {
        public ParticleSystem ParticleSystem;

        public void Play()
        {
            this.ParticleSystem.Play();
        }
    }
}