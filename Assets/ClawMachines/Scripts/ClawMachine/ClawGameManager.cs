using DG.Tweening;
using NTPackage.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.ClawMachine
{

   

    public class ClawGameManager : MonoBehaviour
    {
        public static ClawGameManager Instance;
        [SerializeField] private List<Transform> LsPositionSpawns = new List<Transform>();
        [SerializeField] private Transform holderPos;
        public Transform targetPosMove;
        public List<GameObject> Lamps;
        public List<GameObject> LampsTopLs;
        public Transform rightPos, leftPos;
        public ClowController ClowController;
        [SerializeField] private TextMeshProUGUI Textcoin;
        public int number;
        public int numberCandyandBaby;
        public GameObject clawMachineObj;
        public bool canIsClaw;
        public Button exitBtn;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Start()
        {
            if (Blink != null)
            {
                StopCoroutine(Blink);
            }
            Blink = StartCoroutine(BlinkInCircle());
            //this.statusGame = StatusGame.StartGame;
            StartGame();
            ScaleClawMachine();
            this.exitBtn.onClick.AddListener(ExitGame);
            //SoundController.instance.PlayMusicClawing();
        }
        float referenceWidth = 1080f;  // Chiều rộng tham chiếu
        float referenceHeight = 1920f; // Chiều cao tham chiếu
        /*float referenceAspect = referenceWidth / referenceHeight; */// 9:16 = 0.5625
        float currentAspect = (float)Screen.width / Screen.height; // Tỷ lệ màn hình hiện tại
        //float scaleFactor = (float)Screen.width / referenceWidth;
        void ScaleClawMachine()
        {
            float referenceAspect = referenceWidth / referenceHeight;
            float scaleFactor = (float)Screen.width / referenceWidth;
            float aspectRatio = (float)Screen.width / Screen.height;
            //float aspectRatio = (float)Screen.height / Screen.width;
            Debug.Log("aspectRatio" + aspectRatio);
            Debug.Log("width" + Screen.width);
            Debug.Log("height" + Screen.height);
            if (aspectRatio >= 2.1f) // Điện thoại siêu dài (21:9)
            {
                clawMachineObj.transform.localScale = new Vector3(0.82f, 0.82f, 1);
            }
            else if (aspectRatio >= 1.8f) // Điện thoại phổ thông (19.5:9, 18:9)
            {
                clawMachineObj.transform.localScale = new Vector3(1.0f, 1.0f, 1);
            }
            else if (aspectRatio >= 1.5f) // Tablet hoặc điện thoại cũ (16:10, 4:3)
            {
                //1.5
                clawMachineObj.transform.localScale = new Vector3(0.9f, 0.9f, 1);
            }
            else // Màn hình vuông hoặc nhỏ
            {
                float scaleFactorWidth = (float)Screen.width / referenceWidth;  // Hệ số theo chiều rộng
                if (scaleFactorWidth >= 1f)
                {
                    clawMachineObj.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    clawMachineObj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
            }

        }
        Coroutine Blink;
        private IEnumerator BlinkInCircle()
        {
            int count = Lamps.Count;
            if (count == 0) yield break;
            int index = 0;
            while (true)
            {
                foreach (GameObject go in Lamps)
                {
                    go.SetActive(false);
                }
                Lamps[index].SetActive(true);
                index = (index + 1) % count;
                yield return new WaitForSeconds(0.2f);
            }
        }
        Coroutine BlinkClawing;
        private IEnumerator BlinkInClawing()
        {
            int count = LampsTopLs.Count;
            if (count == 0) yield break;
            int index = 0;
            while (true)
            {
                foreach (GameObject go in LampsTopLs)
                {
                    go.SetActive(false);
                }
                LampsTopLs[index].SetActive(true);
                index = (index + 1) % count;
                yield return new WaitForSeconds(0.3F);
            }
        }
        public void DonePress()
        {
            //this.buttonPress.DonePress();
            if (this.BlinkClawing != null)
            {
                StopCoroutine(BlinkClawing);
            }
            for (int i = 0; i < LampsTopLs.Count; i++)
            {
                this.LampsTopLs[i].SetActive(false);
            }

        }
        //private List<int> GetRandomNumbers()
        //{
        //    List<int> numbers = new List<int>();
        //    for (int i = 0; i < DataAssets.Instance.animals.Count; i++)
        //    {
        //        numbers.Add(i);
        //    }
        //    List<int> results = new List<int>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
        //        results.Add(numbers[randomIndex]);
        //        //numbers.RemoveAt(randomIndex);
        //    }
        //    return results;
        //}

        //[ContextMenu("SpawnAnimal")]
        //public void SpawnAnimal()
        //{
        //    for (int i = 0; i <= 10; i++)
        //    {
        //        int randomIndex = UnityEngine.Random.Range(0, DataAssets.Instance.animals.Count);
        //        int posIndex = UnityEngine.Random.Range(0, this.LsPositionSpawns.Count);
        //        GameObject animal = Instantiate(DataAssets.Instance.animals[randomIndex].AnimalObj);
        //        animal.transform.position = this.LsPositionSpawns[posIndex].position;
        //        animal.transform.SetParent(this.holderPos, true);
        //    }
        //    //for (int i = 0; i <= 29; i++)
        //    //{
        //    //    int posIndex = UnityEngine.Random.Range(0, this.LsPositionSpawns.Count);
        //    //    GameObject gold = Instantiate(goldObj);
        //    //    gold.transform.position = this.LsPositionSpawns[posIndex].position;
        //    //    gold.transform.SetParent(this.holderPos, true);
        //    //}
        //}

        [ContextMenu("StartGame")]
        public void StartGame()
        {
            //this.LsBaby.Clear();
            this.number = 0;
            this.numberCandyandBaby = 0;
            MyFunction.ClearChild(this.holderPos);
            UserManager.instance.useData.level = RandomLevel();
            //for (int i = 0; i < ClawDataAssets.Instance.levelData.LevelDatas[UserManager.instance.useData.level].Id.Count; i++)
            //{
            //    int posIndex = Random.Range(0, this.LsPositionSpawns.Count);
            //    if (ClawDataAssets.Instance.AnimalPre != null)
            //    {
            //        Animal a = Instantiate(ClawDataAssets.Instance.AnimalPre);
            //        this.numberCandyandBaby++;
            //        a.transform.position = this.LsPositionSpawns[posIndex].position;
            //        a.transform.SetParent(this.holderPos, true);
            //        a.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            //       AnimalData animalData = ClawDataAssets.Instance.GetAnimalById(ClawDataAssets.Instance.levelData.LevelDatas[UserManager.instance.useData.level].Id[i]);
            //        //this.LsBaby.Add(a.gameObject);
            //        a.Init(animalData);
            //    }
            //}
            for (int i = 0; i < 10; i++)
            {
                int posIndex = Random.Range(0, this.LsPositionSpawns.Count);
                int number = Random.Range(0, DataAssets.Instance.ListAnimals.Count);
                if (DataAssets.Instance.AnimalPre != null)
                {
                    AnimalData animalData = DataAssets.Instance.GetAnimalById(DataAssets.Instance.ListAnimals[number].Id);
                    Animal a = Instantiate(DataAssets.Instance.AnimalPre);
                    this.numberCandyandBaby++;
                    a.transform.position = this.LsPositionSpawns[posIndex].position;
                    a.transform.SetParent(this.holderPos, true);
                    a.transform.localScale = new Vector3(animalData.valueScale, animalData.valueScale, animalData.valueScale);
                    a.Init(animalData);
                }
            }

            //for (int i = 0; i < ClawDataAssets.Instance.levelData.LevelDatas[UserManager.instance.useData.level].IdCandy.Count; i++)
            //{
            //    int posIndex = Random.Range(0, this.LsPositionSpawns.Count);
            //    if (ClawDataAssets.Instance.candyPre != null)
            //    {
            //        CandyData candyData = ClawDataAssets.Instance.GetCandyById(ClawDataAssets.Instance.levelData.LevelDatas[UserManager.instance.useData.level].IdCandy[i]);
            //        Candy a = Instantiate(ClawDataAssets.Instance.candyPre);
            //        this.numberCandyandBaby++;
            //        a.transform.position = this.LsPositionSpawns[posIndex].position;
            //        a.transform.SetParent(this.holderPos, true);
            //        a.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                   
            //        a.Init(candyData);
            //    }
            //}
            for (int i = 0; i < 18; i++)
            {
                int posIndex = Random.Range(0, this.LsPositionSpawns.Count);
                int number = Random.Range(0, DataAssets.Instance.candies.Count);
                if (DataAssets.Instance.candyPre != null)
                {
                    CandyData candyData = DataAssets.Instance.GetCandyById(DataAssets.Instance.candies[number].Id);
                    Candy a = Instantiate(DataAssets.Instance.candyPre);
                    this.numberCandyandBaby++;
                    a.transform.position = this.LsPositionSpawns[posIndex].position;
                    a.transform.SetParent(this.holderPos, true);
                    a.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);

                    a.Init(candyData);
                }
            }

            for (int i = 0; i < DataAssets.Instance.levelData.LevelDatas[UserManager.instance.useData.level].numberBallon; i++)
            {
                int posIndex = Random.Range(0, this.LsPositionSpawns.Count);
                if (DataAssets.Instance.coin != null)
                {
                    GameObject a = Instantiate(DataAssets.Instance.coin);
                    a.transform.position = this.LsPositionSpawns[posIndex].position;
                    a.transform.SetParent(this.holderPos, true);
                    a.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

                }
            }
            // InitGold(ClawUserManager.instance.useData.gold);
            InitTextStartGame();
            //for (int i = 0; i < DataAssets.Instance.levelData.LevelDatas[this.level].numberBallon; i++)
            //{
            //    int posIndex = UnityEngine.Random.Range(0, this.LsPositionSpawns.Count);
            //    GameObject gold = Instantiate(goldObj);
            //    gold.transform.position = this.LsPositionSpawns[posIndex].position;
            //    gold.transform.SetParent(this.holderPos, true);
            //}
        }
        private void InitTextStartGame()
        {
            InitGold(UserManager.instance.useData.numberCoin);
        }
        private int RandomLevel()
        {
            int number = 0;
            number = Random.Range(0, DataAssets.Instance.levelData.LevelDatas.Count);
            return number;
        }
        public void InitGold(int gold)
        {
            this.Textcoin.text = gold.ToString();
        }
        public void RewardAds()
        {
            UserManager.instance.useData.numberCoin += 1;
            InitGold(UserManager.instance.useData.numberCoin);
            UserManager.instance.SaveData();
            //this.statusGame = StatusGame.StartGame;
            //this.panelAds.SetActive(false);
            PopupManager.Instance.OffUI(PopupCode.AdsPanel);
        }
        public void AddGoldFromAds()
        {
            if (!NetworkSettingsOpener.Instance.CheckInternet())
            {
                return;
            }
            SoundController.instance.PressButtonAudio();
            //AdsManager.instance.ShowRewardedInterstitialAd(() => RewardAds());
            RewardAds();
        }

        //public void ExitClaw(string sceneName)
        //{
        //    SoundController.instance.PressButtonAudio();
        //    SoundController.instance.PlayContinueMusicBackGround();
        //    if (canClaw) return;
        //    ClawDataAssets.Instance.LoadScene(sceneName);
        //}

        public void ResetBabyAfterWin()
        {
           // ClawDataAssets.Instance.LoadScene("ClawMachineGame");
            SceneController.Instance.LoadToSceneGamePlay();
        }
        //public bool canClaw;
        private void ExitGame()
        {
          //  AdsManager.instance.ShowInterstitialAd();
            if (SceneController.Instance.statusGame != StatusGame.StartGame) return;
            SceneController.Instance.LoadToSceneStartGame();
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
        }
    }
}