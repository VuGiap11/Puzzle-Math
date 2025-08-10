
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI numberGoldText;
    public void Init(int gold)
    {
       this.numberGoldText.text =  gold.ToString();
    }
}
