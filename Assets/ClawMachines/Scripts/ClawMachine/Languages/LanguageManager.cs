using System.Collections.Generic;
using UnityEngine;
using NTPackage.Functions;



public enum LanguageType
{
    England,
    VietNam,
    Japan,
    China
}

[System.Serializable]
public class TranslationData
{
    public string key;
    public string Englandtext;
    public string Vietnamtext;
}
[System.Serializable]
public class TranslationDatas
{
   public List<TranslationData> DataLanguages;
}

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    //private Dictionary<string, TranslationData> localizedTexts;
    public NTDictionary<TranslationData> LanguageDataCfDic;
    //private string currentLanguage = "en";
    public TranslationDatas translationDatas;
    public TextAsset LanguageDataText;
    public LanguageType languageType;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
        LoadDataLanguage();
    }
    public void LoadDataLanguage()
    {
        this.translationDatas = JsonUtility.FromJson<TranslationDatas>(this.LanguageDataText.text);
        this.LanguageDataCfDic = new NTDictionary<TranslationData>();
        foreach (TranslationData item in this.translationDatas.DataLanguages)
        {
            this.LanguageDataCfDic.Add(item.key.ToString(), item);
        }
        this.languageType = (LanguageType)PlayerPrefs.GetInt("SelectedIndexLanguage");
    }
    public TranslationData GetHeroDataCfByIndex(string key)
    {
        return this.LanguageDataCfDic.Get(key.ToString());

    }
    public string GetText(string key)
    {
        TranslationData TranslationData = GetHeroDataCfByIndex(key);
        Debug.Log(TranslationData + "áa" + key);
        if (TranslationData != null)
        {
            switch (languageType)
            {
                case LanguageType.England: return TranslationData.Englandtext;
                case LanguageType.VietNam: return TranslationData.Vietnamtext;
                default: return key;
            }
        }else
        {
            return key;
        }
    }
}
