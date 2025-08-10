
using TMPro;
using UnityEngine;

public enum TypeLocal
{
    AutoChange,
    OnableChange

}

public class LocalizeText : MonoBehaviour
{
    public string key;
    [SerializeField] TextMeshProUGUI textComponent;
    public TypeLocal TypeLocal;

    private void Start()
    {
        if (this.TypeLocal == TypeLocal.AutoChange)
        {
            textComponent.text = LanguageManager.Instance.GetText(key);
        }
    }

    private void OnEnable()
    {
        if (this.TypeLocal == TypeLocal.OnableChange)
        {
            textComponent.text = LanguageManager.Instance.GetText(key);
        }
    }
}
