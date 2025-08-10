
using NTPackage.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{
    [Serializable]
    public class LanguageInfor
    {
        public string id;
        public string countryName;
        public Sprite avarCountry;
        public LanguageType LanguageType;
    }

    public class SettingPanel : PopupUI
    {
        [SerializeField] private Image SoundONIcon;
        [SerializeField] private Image SoundOFFIcon;
        [SerializeField] private Image MusicONIcon;
        [SerializeField] private Image MusicOFFIcon;
        [SerializeField] private Image MusicOFFIconBG, MusicOnIconBG, SoundOFFIconBG, SoundOnIconBG;
        public bool mutedSound = false;
        public bool mutedMusic = false;

        public List<LanguageInfor> languages;
        public Image avatar;
        public TextMeshProUGUI languageText;
        private int curentIndex =0; // id ngôn ngữ đã lưuu
        private int tempIndex; // id ngôn ngữ lưu tạm thời;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            UpdateLanguageUI(PlayerPrefs.GetInt("SelectedIndexLanguage"));
            SetSoundStartGame();
            UpdateButtonIcon();
        }

        public void ConfirmLanguage()
        {
            SoundController.instance.AudioButton();
            this.curentIndex = this.tempIndex;
            PlayerPrefs.SetInt("SelectedIndexLanguage", curentIndex);
            PlayerPrefs.SetString("Language", this.languages[curentIndex].id);
            PlayerPrefs.Save();
            LanguageManager.Instance.languageType = this.languages[PlayerPrefs.GetInt("SelectedIndexLanguage")].LanguageType;
            //this.gameObject.SetActive(false);
            this.OffUI();
            SceneController.Instance.LoadToSceneStartGame();
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            //SceneManager.LoadScene("StartScene");
        }

        public void CancelChangeLanguage()
        {
            SoundController.instance.AudioButton();
            SoundController.instance.AudioButton();
            this.tempIndex = this.curentIndex;
            UpdateLanguageUI(this.tempIndex);
        }
        public void SetLanguageStartGame()
        {
            this.curentIndex = PlayerPrefs.GetInt("SelectedIndexLanguage", 0);
            this.tempIndex = curentIndex;
            UpdateLanguageUI(this.tempIndex);
        }

        public void NextLanguage()
        {
            SoundController.instance.AudioButton();
            this.tempIndex = (tempIndex + 1) % languages.Count;
            UpdateLanguageUI(this.tempIndex);
        }
        public void PrevLanguage()
        {
            SoundController.instance.AudioButton();
            this.tempIndex = (tempIndex - 1 + languages.Count) % languages.Count;
            UpdateLanguageUI(this.tempIndex);
        }
        private void UpdateLanguageUI(int index)
        {
            this.avatar.sprite = this.languages[index].avarCountry;
            this.languageText.text = this.languages[index].countryName;
        }
        public void SetSoundStartGame()
        {
            if (!PlayerPrefs.HasKey("mutedSound"))
            {
                PlayerPrefs.SetInt("mutedSound", 0);
                Load();
            }
            else
            {
                Load();
            }
            if (!PlayerPrefs.HasKey("mutedMusic"))
            {
                PlayerPrefs.SetInt("mutedMusic", 0);
                Load();
            }
            else
            {
                Load();
            }
            UpdateButtonIcon();
            UpdateButtonIconSoundMusic();
            SoundController.instance.SoundOnOff(mutedSound);
            SoundController.instance.MusicOnOff(mutedMusic);
        }
        private void Load()
        {
            mutedSound = PlayerPrefs.GetInt("mutedSound") == 1;
            mutedMusic = PlayerPrefs.GetInt("mutedMusic") == 1;
        }

        public void Save()
        {
            SoundController.instance.PressButtonAudio();
            PlayerPrefs.SetInt("mutedSound", mutedSound ? 1 : 0);
            PlayerPrefs.SetInt("mutedMusic", mutedMusic ? 1 : 0);
        }

        public void OnButtonPressSound()
        {
            //PressButtonAudio();

            if (mutedSound == false)
            {
                mutedSound = true;
            }
            else
            {
                mutedSound = false;
            }
            Save();
            UpdateButtonIcon();
            SoundController.instance.SoundOnOff(mutedSound);
        }
        private void UpdateButtonIcon()
        {
            if (mutedSound == false)
            {
                SoundONIcon.enabled = true;
                SoundOFFIcon.enabled = false;
                this.SoundOnIconBG.gameObject.SetActive(true);
                this.SoundOFFIconBG.gameObject.SetActive(false);
            }
            else
            {
                SoundONIcon.enabled = false;
                SoundOFFIcon.enabled = true;
                this.SoundOnIconBG.gameObject.SetActive(false);
                this.SoundOFFIconBG.gameObject.SetActive(true);
            }
        }
        public void OnButtonPressMusic()
        {
            //PressButtonAudio();
            //SoundController.instance.PressButtonAudio();
            if (mutedMusic == false)
            {
                mutedMusic = true;
            }
            else
            {
                mutedMusic = false;
            }
            Save();
            UpdateButtonIconSoundMusic();
            SoundController.instance.MusicOnOff(mutedMusic);
        }
        private void UpdateButtonIconSoundMusic()
        {
            if (mutedMusic == false)
            {
                MusicONIcon.enabled = true;
                MusicOFFIcon.enabled = false;
                this.MusicOnIconBG.gameObject.SetActive(true);
                this.MusicOFFIconBG.gameObject.SetActive(false);
            }
            else
            {
                MusicONIcon.enabled = false;
                MusicOFFIcon.enabled = true;
                this.MusicOnIconBG.gameObject.SetActive(false);
                this.MusicOFFIconBG.gameObject.SetActive(true);
            }
        }
    }
}