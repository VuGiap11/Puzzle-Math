using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Rubik.ClawMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.Sort_Challenge.HomeScene.BuildingStore
{
    public class BuildingStoreController : MonoBehaviour
    {
        [SerializeField] List<Image> listImageStar;
        [SerializeField] List<Image> listImageCloud;
        [SerializeField] List<Image> listImageUnicorn;
       
        [SerializeField] List<GameObject> listBowlTree;
        [SerializeField] List<GameObject> listTree;
        [SerializeField] Image imgBaby;
        [SerializeField] GameObject signboard;
        [SerializeField] GameObject rainbow;
        [SerializeField] GameObject lightRope;
        //[SerializeField] GameObject gbTextBuildingStore;
        [SerializeField] GameObject eye;



        Vector3 originBabyPos;
        List<Vector3> listOriginCloudPos = new List<Vector3>();
       
        private List<Image> listImageSignboard;
        private List<Image> listImageRainbow;
        private List<Image> listImageLightRope;
        private int currentSignboardIndex = 0;
        private int currentRainbowIndex = 0;
        private int currentLightRopeIndex = 0;
        private Vector3 originSignboardPos;
        private Vector3 originRainbowPos;
        private Vector3 originLightRopePos;
    
        //[SerializeField] private GameObject icounBonous;
        //[SerializeField] private GameObject icounReward;
        //public LoadingController loadingController;
        //private float scaleValue;
        //public CanvasScaler ss;
        float durationChangeSignboard = 1f, durationChangeRainbow = 1f, durationChangeLightRope = 1f, durationMoveBaby = 3f;
        void Awake()
        {
            DOTween.SetTweensCapacity(500, 200);
            StartCoroutine(DelayStart());

        }
        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(0.01f);
            Init();
            StartAnimation();
        }
        void Init()
        {
            originBabyPos = imgBaby.transform.position;
            originSignboardPos = signboard.transform.position;
            originRainbowPos = rainbow.transform.position;
            originLightRopePos = lightRope.transform.position;

            for (int i = 0; i < listImageCloud.Count; i++)
            {


                listOriginCloudPos.Add(listImageCloud[i].transform.position);
            }
       
            InitSignboard();
            InitRainbow();
            InitLightRope();
        }
        void StartAnimation()
        {
            //Scale();
            RotateStar();
            MoveBaby();
            //RoteEye();
            MoveCloud();
            StartCoroutine(ChangeSignboard());
            StartCoroutine(ChangeRainbow());
            StartCoroutine(ChangeLightRope());
            AnimateUnicorn();
           // AnimTextBuildingStore();
            AnimateBowlTree();
            AnimateTree();
            //ScaleIcoinReward();
            
        }
       //     public void Scale()
       // {
       //     float aspectRatio = (float)Screen.height / Screen.width;
       //     Debug.Log("Aspect Ratio: " + aspectRatio);
       //     Debug.Log("Screen.width Ratio: " + Screen.width);
       //     Debug.Log("Screen.height Ratio: " + Screen.height);
       //     if (aspectRatio >= 2.1f) // Điện thoại siêu dài (21:9)
       //     {
       //         ss.matchWidthOrHeight = 0.3f;
       //     }
       //     else if (aspectRatio >= 1.8f) // Điện thoại phổ thông (19.5:9, 18:9)
       //     {
       //         ss.matchWidthOrHeight = 0f;
       //     }
       //     else if (aspectRatio >= 1.5f) // Tablet hoặc điện thoại cũ (16:10, 4:3)
       //     {
       //         ss.matchWidthOrHeight = 0f;
       //     }
       //     //else if (aspectRatio >= 1.2f) // Màn hình gập (Galaxy Z Fold 2 - 5:4)
       //     //{
       //     //    ss.matchWidthOrHeight = 0.3f; // Giá trị tùy chỉnh
       //     //}
       //     else // Màn hình vuông hoặc nhỏ
       //     {
       //         ss.matchWidthOrHeight = 1;
       //     }

       ////     Debug.Log("ss.matchWidthOrHeight: " + ss.matchWidthOrHeight);
      //  }
        void InitSignboard()
        {
            listImageSignboard = signboard.GetComponentsInChildren<Image>().ToList();
            listImageSignboard.RemoveAt(listImageSignboard.Count - 1);


            foreach (var item in listImageSignboard)
            {
                item.gameObject.SetActive(false);
            }
            listImageSignboard[0].gameObject.SetActive(true);
        }
        void InitRainbow()
        {
            listImageRainbow = rainbow.GetComponentsInChildren<Image>().ToList();


            foreach (var item in listImageRainbow)
            {
                item.gameObject.SetActive(false);
            }
            listImageRainbow[0].gameObject.SetActive(true);
        }
        void InitLightRope()
        {
            listImageLightRope = lightRope.GetComponentsInChildren<Image>().ToList();


            foreach (var item in listImageLightRope)
            {
                item.gameObject.SetActive(false);
            }

            listImageLightRope[0].gameObject.SetActive(true);
        }

        public void StopAllAnimation()
        {
            DOTween.KillAll();
        }
     
        void RoteEye()
        {
            DOTween.Sequence()
                   .Append(eye.transform.DOScaleY(0f, 2.5f).SetEase(Ease.Linear)).SetDelay(1f)
                   .Append(eye.transform.DOScaleY(1f, 1f).SetEase(Ease.Linear))
                   .SetLoops(-1);

        }
  
        void RotateStar()

        {
            for (int i = 0; i < listImageStar.Count; i++)
            {
                Vector3 rotate = (Random.Range(0, 2) == 0) ? ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360)) : ((Random.Range(0, 2) == 0) ? new Vector3(0, 0, 360) : new Vector3(0, 0, -360));
                listImageStar[i].transform.DORotate(rotate, 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            }
        }
        void MoveBaby()
        {
            //DOTween.Sequence()
            //.Append(imgBaby.transform.DOMoveY(originBabyPos.y - 0.5f, 3f).SetEase(Ease.Linear).SetDelay(1f))
            //.Append(imgBaby.transform.DOMoveY(originBabyPos.y, 3f).SetEase(Ease.Linear))
            //.SetLoops(-1);
            Transform babyChild = imgBaby.transform.GetChild(0);
            float blinkDuration = durationMoveBaby / 20f;
            float moveDistance = 0.3f;

            DOTween.Sequence()
                // Blink animation
                .Append(babyChild.DOScale(new Vector3(1, 0, 1), blinkDuration))
                .Append(babyChild.DOScale(Vector3.one, blinkDuration))
                .Append(babyChild.DOScale(new Vector3(1, 0, 1), blinkDuration))
                .Append(babyChild.DOScale(Vector3.one, blinkDuration))
                // Move up and down animation
                .Append(imgBaby.transform.DOMoveY(originBabyPos.y - moveDistance, durationMoveBaby)
                    .SetEase(Ease.Linear)
                    .SetDelay(1f))
                .Append(imgBaby.transform.DOMoveY(originBabyPos.y, durationMoveBaby)
                    .SetEase(Ease.Linear))
                .SetLoops(-1);
        }
        void MoveCloud()
        {
            for (int i = 0; i < listImageCloud.Count; i++)
            {
                var cloud = listImageCloud[i];
                var originY = listOriginCloudPos[i].y;
                float valueYRandom = (Random.Range(0, 2) == 0) ? Random.Range(0.05f, 0.08f) : Random.Range(0.08f, 0.1f);
                // Animation scale
                DOTween.Sequence()
                    .Append(cloud.transform.DOScaleY(0.8f, 3f).SetEase(Ease.Linear))
                    .Append(cloud.transform.DOScaleY(1f, 3f).SetEase(Ease.Linear))
                    .SetLoops(-1);

                // Animation move
                DOTween.Sequence()
                    .Append(cloud.transform.DOMoveY(originY + valueYRandom, 1.5f).SetEase(Ease.Linear))
                    .Append(cloud.transform.DOMoveY(originY - valueYRandom, 3f).SetEase(Ease.Linear))
                    .Append(cloud.transform.DOMoveY(originY, 1.5f).SetEase(Ease.Linear))
                    .SetDelay(i * 0.05f)
                    .SetLoops(-1);
            }
        }
        IEnumerator ChangeSignboard()
        {
            while (true)
            {
                yield return new WaitForSeconds(durationChangeSignboard);

                currentSignboardIndex++;
                for (int i = 0; i < listImageSignboard.Count; i++)
                {
                    listImageSignboard[i].gameObject.SetActive(i == currentSignboardIndex);
                }
                if (currentSignboardIndex >= listImageSignboard.Count - 1)
                {
                    currentSignboardIndex = -1;
                }
            }
        }
        IEnumerator ChangeRainbow()
        {
            while (true)
            {
                yield return new WaitForSeconds(durationChangeRainbow);

                currentRainbowIndex++;
                for (int i = 0; i < listImageRainbow.Count; i++)
                {
                    listImageRainbow[i].gameObject.SetActive(i == currentRainbowIndex);
                }
                if (currentRainbowIndex >= listImageRainbow.Count - 1)
                {
                    currentRainbowIndex = -1;
                }
            }
        }
        IEnumerator ChangeLightRope()
        {
            while (true)
            {
                yield return new WaitForSeconds(durationChangeLightRope);

                currentLightRopeIndex++;
                for (int i = 0; i < listImageLightRope.Count; i++)
                {
                    listImageLightRope[i].gameObject.SetActive(i == currentLightRopeIndex);
                }
                if (currentLightRopeIndex >= listImageLightRope.Count - 1)
                {
                    currentLightRopeIndex = -1;
                }
            }
        }

        void AnimateUnicorn()
        {
            foreach (var item in listImageUnicorn)
            {
                // Chọn ngẫu nhiên góc quay từ 10-30 độ
                int degree = Random.Range(10, 30);
                // Thiết lập góc quay ban đầu
                DOTween.Sequence()
                .Append(item.transform.DOScaleY(0.8f, 2f).SetEase(Ease.Linear))
                .Append(item.transform.DOScaleY(1f, 2f).SetEase(Ease.Linear))
                .SetLoops(-1);


                item.transform.DORotate(new Vector3(0, 0, degree), 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    DOTween.Sequence()
                    .Append(item.transform.DORotate(new Vector3(0, 0, -degree * 2), 3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.Linear)
                        .SetRelative(true))

                    .Append(item.transform.DORotate(new Vector3(0, 0, degree * 2), 3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.Linear)
                        .SetRelative(true))
                    .SetLoops(-1);
                });
            }
        }

        //void AnimTextBuildingStore()
        //{
        //    DOTween.Sequence()
        //    .Append(gbTextBuildingStore.transform.DOScaleX(1.1f, 1f).SetEase(Ease.Linear))
        //    .Join(gbTextBuildingStore.transform.DOScaleY(0.9f, 1f).SetEase(Ease.Linear))

        //    .Append(gbTextBuildingStore.transform.DOScaleX(1f, 1f).SetEase(Ease.Linear))
        //    .Join(gbTextBuildingStore.transform.DOScaleY(1f, 1f).SetEase(Ease.Linear))

        //    .Append(gbTextBuildingStore.transform.DOScaleX(0.9f, 1f).SetEase(Ease.Linear))
        //    .Join(gbTextBuildingStore.transform.DOScaleY(1.1f, 1f).SetEase(Ease.Linear))

        //    .Append(gbTextBuildingStore.transform.DOScaleX(1f, 1f).SetEase(Ease.Linear))
        //    .Join(gbTextBuildingStore.transform.DOScaleY(1f, 1f).SetEase(Ease.Linear))
        //    .SetLoops(-1);
        //}
        void AnimateBowlTree()
        {
            foreach (var item in listBowlTree)
            {
                // Chọn ngẫu nhiên góc quay từ 10-30 độ

                int degree = Random.Range(2, 5);
                // Thiết lập góc quay ban đầu

                item.transform.DORotate(new Vector3(0, 0, degree), 1.5f).SetDelay(0.5f * listBowlTree.FindIndex(f => f == item))
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    DOTween.Sequence()
                    .Append(item.transform.DORotate(new Vector3(0, 0, -degree * 2), 3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.Linear)
                        .SetRelative(true))
                    .Join(item.transform.DOScaleY(0.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        item.transform.DOScaleY(1f, 1.5f).SetEase(Ease.Linear);
                    }))


                    .Append(item.transform.DORotate(new Vector3(0, 0, degree * 2), 3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.Linear)
                        .SetRelative(true))
                    .Join(item.transform.DOScaleY(0.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        item.transform.DOScaleY(1f, 1.5f).SetEase(Ease.Linear);
                    }))

                    .SetLoops(-1);
                });
            }
        }
        void AnimateTree()
        {
            foreach (var item in listTree)
            {
                DOTween.Sequence()
                .Append(item.transform.DOScaleY(0.8f, 1.5f).SetEase(Ease.Linear).SetDelay(Random.Range(0, 0.5f)))
                .Append(item.transform.DOScaleY(1f, 1.5f).SetEase(Ease.Linear))
                .SetLoops(-1);
            }

        }

        public void LoadtoseceneLoading(string sceneName)
        {
            StopAllAnimation();
            //loadingController.gameObject.SetActive(true);
            //loadingController.StartLoadingScene(1000, sceneName);
            DataAssets.Instance.LoadScene(sceneName);
        }
        //private void ScaleIcoinReward()
        //{
        //    DOTween.Sequence()
        //     .Append(this.icounReward.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1f).SetEase(Ease.Linear)).SetDelay(0.5f)
        //     .Append(this.icounReward.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 1f).SetEase(Ease.Linear))
        //     .SetLoops(-1);
        //}

        private void OnDisable()
        {
            StopAllAnimation();
        }

    }
}


