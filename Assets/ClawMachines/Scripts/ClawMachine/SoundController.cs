using NTPackage.UI;
using UnityEngine;

namespace Rubik.ClawMachine
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController instance;
        public AudioSource efxSource1;
        public AudioSource efxSource2;
        public AudioSource efxSource3;
        public AudioSource musicSource,musicSourceLucky;
        public float lowPitchRange = 0.95f;
        public float highPitchRange = 1.05f;

        //[SerializeField] private Image SoundONIcon;
        //[SerializeField] private Image SoundOFFIcon;
        public bool muted = false;
        public AudioClip audioSide;
        public AudioClip audioClawiing;
        [SerializeField] private AudioClip buttonAudio;
        public AudioClip audioReceiReward;
        public AudioClip startMerdAudio, doneMeraudio, doneMemory;
        public AudioClip matchSound,audioLucKy, audiovictory, audioDefeat;
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

        public void AudioVictory()
        {
            PlaySingle(this.audiovictory);
        }

        public void AudioDefeat()
        {
            PlaySingle(this.audioDefeat);
        }
        public void MatchSound()
        {
            PlaySingle(this.matchSound);
        }
        public void StartMerdAudio()
        {
            PlaySingle(this.startMerdAudio);

        }
        public void DoneMerdAudio()
        {
            PlaySingle(this.doneMeraudio);

        }
        public void DoneMemoryAudio()
        {
            PlaySingle(this.doneMemory);

        }
        private void Start()
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
        public void PlayMusic(AudioClip clip)
        {
            //if (GameController.Instance.IsSoundOn)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void PlayMusicLucky(AudioClip clip)
        {
            //if (GameController.Instance.IsSoundOn)
            {
                musicSourceLucky.clip = clip;
                musicSourceLucky.Play();
            }
        }
        public void PlayContinueMusicBackGround()
        {
            //if (musicSource.isPlaying && musicSource.clip == audioClawiing)
            //{
            //    musicSource.Stop();
            //}
            if (musicSource.clip == audioClawiing)
            {
                musicSource.Stop();
            }
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
            if (audioClawiing != null)
            {
                PlayMusic(audioClawiing);
            }
        }

        public void PlayMusicLucKyGame()
        {
            PlayMusicLucky(this.audioLucKy);
        }

        public void StopPlayMusicLucKyGame()
        {
            if (musicSourceLucky != null)
            {
                this.musicSourceLucky.Stop();
            }
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
            musicSourceLucky.volume = 0;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        public void ContinueMusic()
        {
            musicSource.volume = 0.5f;

            musicSourceLucky.volume = 0.5f;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        public void ToggleSound(bool isOn)
        {

            if (!isOn)
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
                PlayerPrefs.SetInt("SoundOn", 0);
                //PlayerPrefsX.SetBool("IsSoundOn", true);
            }
            else
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 1f;
                PlayerPrefs.SetInt("SoundOn", 1);
                //PlayerPrefsX.SetBool("IsSoundOn", false);
            }
            //if (withSound)
            //{
            //    PlaySingle(UISfxController.Instance.SfxSettingSound);
            //}
        }

        //public void OnButtonPress()
        //{
        //    //PressButtonAudio();
        //    PlaySingle(audioSide);
        //    if (muted == false)
        //    {
        //        muted = true;
        //        AudioListener.pause = true;
        //    }
        //    else
        //    {
        //        muted = false;
        //        AudioListener.pause = false;
        //    }
        //    Save();
        //    UpdateButtonIcon();
        //}
        //private void UpdateButtonIcon()
        //{
        //    if (muted == false)
        //    {
        //        SoundONIcon.enabled = true;
        //        SoundOFFIcon.enabled = false;
        //    }
        //    else
        //    {
        //        SoundONIcon.enabled = false;
        //        SoundOFFIcon.enabled = true;
        //    }
        //}
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
                musicSourceLucky.volume = 1;
            }
            else
            {
               // musicSource.volume = musicSourceWinLose.volume = 0f;
                musicSource.volume = 0f;
                musicSourceLucky.volume = 0f;
            }
        }
        //public void PlayMusicWinLose(AudioClip clip)
        //{
        //    //if (GameController.Instance.IsSoundOn)
        //    {
        //        musicSourceWinLose.clip = clip;
        //        musicSourceWinLose.Play();
        //    }
        //}


        //public void StopAudioToClaim()
        //{
        //    if (this.musicSourceWinLose.isPlaying)
        //    {
        //        this.musicSourceWinLose.Stop();
        //    }
        //}
        public void AudioReward()
        {
            PlaySingle(this.audioReceiReward);
            Debug.Log("audioreward");
        }
    }
}