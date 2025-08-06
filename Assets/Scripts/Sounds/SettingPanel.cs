
using NTPackage.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class SettingPanel : PopupUI
    {
        [SerializeField] private Image SoundONIcon;
        [SerializeField] private Image SoundOFFIcon;
        [SerializeField] private Image MusicONIcon;
        [SerializeField] private Image MusicOFFIcon;
        [SerializeField] private Image MusicOFFIconBG, MusicOnIconBG, SoundOFFIconBG, SoundOnIconBG;
        public bool mutedSound = false;
        public bool mutedMusic = false;

        public override void OnUI(object data = null)
        {
            base.OnUI(data);
            SetSoundStartGame();
            UpdateButtonIcon();
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
            SoundController.instance.AudioButton();
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
            SoundController.instance.AudioButton();
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