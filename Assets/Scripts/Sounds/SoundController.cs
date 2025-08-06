using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController instance;
        public AudioSource efxSource1;
        public AudioSource efxSource2;
        public AudioSource efxSource3;
        public AudioSource musicSource;
        public float lowPitchRange = 0.95f;
        public float highPitchRange = 1.05f;

        //[SerializeField] private Image SoundONIcon;
        //[SerializeField] private Image SoundOFFIcon;
        public bool muted = false;
        public AudioClip audioSide;
        public AudioClip trueAudio,falseAudio;
        [SerializeField] private AudioClip buttonAudio;
        public AudioClip audioReceiReward;
        //public AudioSource musicSourceWinLose;
        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            if (PlayerPrefs.GetInt("FirstPlay", 0) == 0)
            {
                PlayerPrefs.SetInt("FirstPlay", 1);
                PlayerPrefs.SetInt("MusicOn", 1);
                PlayerPrefs.SetInt("SoundOn", 1);
            }
        }
        private void Start()
        {
  
        }

        public void AudioStartGame()
        {
            if (!PlayerPrefs.HasKey("muted"))
            {
                PlayerPrefs.SetInt("muted", 0);
                Load();
            }
            else
            {
                Load();
            }
            //UpdateButtonIcon();
            AudioListener.pause = muted;
            SettingPanel settingPanel = PopupManager.Instance.GetPopupUIByCode(PopupCode.SettingPanel) as SettingPanel;
            if (settingPanel != null)
            {
                settingPanel.SetSoundStartGame();
                Debug.Log("setsouund");
            }
            PlayMusicBackGround();
        }
        public void PlayMusicBackGround()
        {
            PlayMusic(this.audioSide);
        }
        public void AudioButton()
        {
            PlaySingle(this.buttonAudio);
        }

        public void TrueAudio()
        {
            PlaySingle(this.trueAudio);

        }

        public void FalseAudio()
        {
            PlaySingle(this.falseAudio);
        }

        public void PlayMusic(AudioClip clip)
        {
            //if (GameController.Instance.IsSoundOn)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
        public void PlayContinueMusicBackGround()
        {

            //if (musicSource.clip == audioClawiing)
            //{
            //    musicSource.Stop();
            //}
            if (audioSide != null)
            {
                PlayMusic(this.audioSide);
            }
        }

        public void PlayMusicClawing()
        {
            //if (musicSource.isPlaying && musicSource.clip == audioSide)
            //{
            //    musicSource.Stop();
            //}
            if (musicSource.clip == audioSide)
            {
                musicSource.Stop();
            }
            //if (audioClawiing != null)
            //{
            //    PlayMusic(audioClawiing);
            //}
        }
        public void PlaySingle(AudioClip clip)
        {
            if (efxSource1.isPlaying)
            {
                PlaySecond(clip);
            }
            else
            {
                efxSource1.PlayOneShot(clip);
            }
        }

        private void PlaySecond(AudioClip clip)
        {
            if (efxSource2.isPlaying)
            {
                PlayThird(clip);
            }
            else
            {
                efxSource2.PlayOneShot(clip);
            }
        }

        private void PlayThird(AudioClip clip)
        {
            efxSource3.PlayOneShot(clip);
        }
        public void Mute()
        {
            musicSource.volume = 0;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        public void ContinueMusic()
        {
            musicSource.volume = 0.5f;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        public void ToggleSound(bool isOn)
        {

            if (!isOn)
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
                PlayerPrefs.SetInt("SoundOn", 0);
            }
            else
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 1f;
                PlayerPrefs.SetInt("SoundOn", 1);
            }
        }

        private void Load()
        {
            muted = PlayerPrefs.GetInt("muted") == 1;
        }
        private void Save()
        {
            PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        }
        public void PressButtonAudio()
        {
            PlaySingle(buttonAudio);
        }
        public void SoundOnOff(bool muted)
        {

            if (!muted)
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
            }
            else
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 0f;
            }
        }
        public void MusicOnOff(bool muted)
        {

            if (!muted)
            {
                //musicSource.volume = musicSourceWinLose.volume = 1;
                musicSource.volume = 1;
            }
            else
            {
               // musicSource.volume = musicSourceWinLose.volume = 0f;
                musicSource.volume = 0f;
            }
        }
        public void AudioReward()
        {
            PlaySingle(this.audioReceiReward);
            Debug.Log("audioreward");
        }
    }
}