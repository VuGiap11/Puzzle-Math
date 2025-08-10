using DG.Tweening;
using Rubik.ClawMachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik.BinGo
{
    public class ButtonMath : MonoBehaviour
    {
        public TextMeshProUGUI numberText;
        public int indexX;
        public int indexY;
        public int result;
        public Image avarAnimal;
        public Image avarAnimalOff;
        public GameObject BtnMath;
        public Button buttonMath;
        public Animal animal;
        public bool isDone;
        public bool isAnswer;
        [SerializeField] private Transform holder;

        GameObject baby;
        public Transform pointA; // Điểm đầu
        public Transform pointB; // Điểm cuối (có thể thay đổi)
        //public float controlPointOffset = 2f; // Độ lệch theo trục Y cho điểm điều khiển
        public float duration = 0.02f; // Thời gian di chuyển
        public PathType pathType = PathType.CatmullRom; // 
        Vector3 scale = new Vector3(0.4f, 0.4f, 0.4f);
        public GameObject gameAnswer;
        private void Start()
        { 
            avarAnimal.gameObject.SetActive(false);
        }
        public void Init()
        {
            numberText.text = result.ToString();
            //avarAnimal.gameObject.SetActive(false);
            avarAnimalOff.sprite = animal.animalData.Avatar;
            avarAnimal.sprite = animal.animalData.Avatar;
            this.isDone = false;
            this.isAnswer = false;
            this.gameAnswer.SetActive(false);
        }
        public void ClickButton()
        {
            if (isDone) return;
            this.gameAnswer.SetActive(false);
            if (GameManager.instance.ListQuestions[GameManager.instance.number].result == this.result)
            {
                Debug.Log("true");
                avarAnimal.gameObject.SetActive(true);
                //this.buttonMath.interactable = false;
                BtnMath.SetActive(false);
                this.isAnswer = true;
                isDone = true;
                baby = Instantiate(avarAnimal.gameObject);
                //baby.transform.position = transform.position;
                baby.transform.SetParent(this.holder, false);
                baby.transform.localScale = new Vector3(3f, 3f, 3f);
                pointA = transform;
                pointB = GameManager.instance.target;
                CreateSmoothCurve();

            }
            else
            {
                GameManager.instance.SetFalse();
                this.isAnswer = false;
            }
            //this.buttonMath.interactable = false;
            DOVirtual.DelayedCall(1f, delegate
            {
                GameManager.instance.NextQuestion();
                DataAsset.instance.numberQuestion++;
                if (DataAsset.instance.numberQuestion >= 25)
                {
                    GameManager.instance.NextRound();
                }
            });
        }
        public bool SetResult(int result)
        {
            if (this.result == result)
            {
                return true;
            }
            else return false;
        }
        void CreateSmoothCurve()
        {
            Vector3 controlPoint1 = pointA.position + (pointB.position - pointA.position) / 3 + Vector3.up * 2;
            Vector3 controlPoint2 = pointA.position + 2 * (pointB.position - pointA.position) / 3 + Vector3.up * 2;

            Vector3[] pathPoints = new Vector3[] { pointA.position, controlPoint1, controlPoint2, pointB.position };
            baby.transform.DOPath(pathPoints, duration, pathType)
                     .SetEase(Ease.Linear)
                     //.SetSpeedBased()
                     .OnComplete(() =>
                     {
                        // UserManagers.instance.SetBayByID(this.animal.animalData.Id);
                         Destroy(this.baby);
                         GameManager.instance.InitBaby();
                     });
            baby.transform.DOScale(this.scale, duration)
            .SetEase(Ease.Linear);
            //.SetSpeedBased();
            //.SetLookAt(0.01f);
        }

    }
}


