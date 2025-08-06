using NTPackage.UI;
using Rubik.math;
using TMPro;
using UnityEngine;

public class GoldPracticePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    public void Init()
    {
        this.goldText.text = UserManager.instance.useData.gold.ToString();
    }
}
