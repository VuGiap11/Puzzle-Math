using NTPackage.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.math
{
    public class HomeManager : MonoBehaviour
    {
        public static HomeManager instance;
        public Image avatar;

        [SerializeField] private TextMeshProUGUI goldText;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        private void Start()
        {
            Init();
            SoundController.instance.AudioStartGame();
        }
        public void PlayGame()
        {
            SceneController.Instance.LoadToSceneGamePlay();
        }
        public void Init()
        {
            //PopupManager.Instance.OffAllPopupUI();
            this.avatar.sprite = DataAssets.instance.imageAvar[UserManager.instance.useData.idAvar];
            this.goldText.text = UserManager.instance.useData.gold.ToString();
        }
    }
} 