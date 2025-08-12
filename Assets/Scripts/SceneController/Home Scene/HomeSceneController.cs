using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
using DG.Tweening;
using Rubik.ClawMachine;
namespace NailSalonGame
{
    public class HomeSceneController : MonoBehaviour
    {
        public static HomeSceneController Instance;
        #region Fields
        [SerializeField] private GameObject[] panelPrefab;
        [SerializeField] private Toggle panel;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private SimpleScrollSnap scrollSnap;
        [SerializeField] private GameObject mainBackground;
        [SerializeField] private Transform[] transformsLayout;
       // [SerializeField] private GameObject panelStickers, panelNailColor, panelPatern;
        [SerializeField] private Transform particalHeart;
        [SerializeField] private TextMeshProUGUI textFps;
        public Material mat;
        public RectTransform scrollnapTransform;
        public RectTransform mainbgScroll;
        private float toggleWidth;
        public int indexPanel = 0;

        public TextMeshProUGUI goldText, coinNumbertext;
        public Image avartarPlayer;
        #endregion
        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
            scrollnapTransform = scrollSnap.Content.GetComponent<RectTransform>();
            mainbgScroll = mainBackground.GetComponent<RectTransform>();
        }
        void Start()
        {
           // InitText();
            Invoke("Add", 0.2f);
        }
       
        void UpdatePositionMainBackground()
        {
            mainbgScroll.localPosition = new Vector3(scrollnapTransform.localPosition.x, mainbgScroll.localPosition.y, 0) ;
        //    Debug.Log(scrollnapTransform.localPosition.x);
        }
        public void Add()
        {
            int index = 0;
            foreach(var child in panelPrefab)
            {
                Toggle toggle = Instantiate(panel, scrollSnap.Pagination.transform.position + new Vector3(toggleWidth * (scrollSnap.NumberOfPanels + 1), 0, 0), Quaternion.identity, scrollSnap.Pagination.transform);
                toggle.group = toggleGroup;
                scrollSnap.Pagination.transform.position -= new Vector3(toggleWidth / 2f, 0, 0);
                scrollSnap.Add(child, index);
               
                Debug.Log($"{child.name} width = {((RectTransform)child.transform).rect.width}");

                //try
                //{
                //    child.transform.position = transformsLayout[index].transform.position;

                //}catch(Exception e)
                //{
                //    Debug.LogError(e);
                //}
                index++;
            }
            // Pagination
           Invoke("SetPositionPanelElemen", 0.2f);
        }
        void SetPositionPanelElemen()
        {
            
            for(int i=0; i< transformsLayout.Length; i++)
            {
                scrollSnap.Content.GetChild(i).transform.position = new Vector3(transformsLayout[i].transform.position.x, scrollSnap.Content.GetChild(i).transform.position.y, scrollSnap.  Content.GetChild(i).transform.position.z);
            }
            scrollSnap.GoToPanel(this.indexPanel);
        }
        // Update is called once per frame
        void Update()
        {
            UpdatePositionMainBackground();
           
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                particalHeart.position = Camera.main.ScreenToWorldPoint(touch.position);
            }
#if UNITY_EDITOR
            Vector2 posFx = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            particalHeart.position = posFx;
#endif
            Application.targetFrameRate = 60;
            if (textFps != null)
            {
                textFps.text = "fps: "+ ((int)(1f / Time.deltaTime)).ToString();
            }
        }
        public void GotoMergeScene()
        {
            SceneController.Instance.LoadToSceneMergeGame();
        }
        public void GotoMemoryScene()
        {
            SceneController.Instance.LoadToSceneMemoryGame();
        }
        public void GotoClawMachineScene()
        {
            SceneController.Instance.LoadToSceneGamePlay();
        }
        public void GotoSweetBabyScene()
        {
            SceneController.Instance.LoadToLukyBabyThreeGame();
        }
        public void GotoLuckyScene()
        {
            SceneController.Instance.LoadToLukyBabyThreeGame();
        }


        public void InitText()
        {
            this.avartarPlayer.sprite = DataAssets.Instance.imageAvar[UserManager.instance.useData.idAvar];
            this.goldText.text = UserManager.instance.useData.gold.ToString();
            this.coinNumbertext.text = UserManager.instance.useData.numberCoin.ToString();
            //Debug.Log("idavar" + UserDataController.instance.dataPlayerController.idAvar);
        }

    }
}

