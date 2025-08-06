using Rubik.math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    public List<PlayEffect> playEffectsWin = new List<PlayEffect>();
    public List<PlayEffect> playEffectsLose = new List<PlayEffect>();
    public static EffectController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void Correct()
    {
        for (int i = 0; i < this.playEffectsWin.Count; i++)
        {
            this.playEffectsWin[i].Play();
        }
    }
    public void DisCorrect()
    {
        for (int i = 0; i < this.playEffectsLose.Count; i++)
        {
            this.playEffectsLose[i].Play();
        }
    }
}
