using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rubik.BinGo
{ 
    public enum GameType
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public ButtonMath[] buttonMaths; // Mảng một chiều
        public TextMeshProUGUI questrionText;
        public TextMeshProUGUI arountText;
        public ButtonMath ButtonMath;
        public List<Transform> buttonTransforms;
        public List<ButtonMath> buttonMathsNew;
        public List<Number> ListQuestions = new List<Number>();
        private int rows = 5;
        private int columns = 5;
        public int around = 1;
        public List<GameObject> IcHp = new List<GameObject>();
        public int numberHp = 5;
        public TextMeshProUGUI timerText; // Tham chiếu đến Text UI để hiển thị thời gian
        private float timeRemaining = 210f; // 3 phút 30 giây = 210 giây
        public int numberBaby = 0;
        public TextMeshProUGUI numberBabyText;
        public Transform target;
        public GameObject starPlayObj, playObj;

        public GameType gameType;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                UpdateTimerText();
            }
        }

        void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        public void StartGame()
        {
            this.ListQuestions.Clear();
            this.ListQuestions = GenerateRandomAdditions(25);
            //SetPositionInArray();
            ////SetResult();
            //foreach (Number addition in GameManager.instance.additionList)
            //{
            //    Debug.Log($"{addition.num1} + {addition.num2} = {addition.result}");
            //}
            DataAsset.instance.numberQuestion = 0;
            this.number = 0;
            this.numberHp = 5;

            this.Spawn();
            SetQuestion();

        }
        public void InitBaby()
        {
            this.numberBaby++;
            this.numberBabyText.text = numberBaby.ToString();

        }
        private void Start()
        {
            //StartGame();
            //this.numberBabyText.text = numberBaby.ToString();
        }
        List<Number> GenerateRandomAdditions(int count)
        {
            List<Number> additions = new List<Number>();
            for (int i = 0; i < count; i++)
            {
                int num1 = UnityEngine.Random.Range(1, 101); // Số ngẫu nhiên từ 1 đến 100
                int num2 = UnityEngine.Random.Range(1, 101);
                int result = num1 + num2; // Tính tổng của num1 và num2
                additions.Add(new Number(num1, num2, result)); // Thêm đối tượng Addition vào danh sách
            }
            return additions;
        }
        [ContextMenu("SetPositionInArray")]
        public void SetPositionInArray()
        {
            if (buttonMaths == null || buttonMaths.Length != rows * columns)
            {
                Debug.LogError("ButtonMath array is not properly set.");
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    buttonMaths[index].indexX = i;
                    buttonMaths[index].indexY = j;
                }
            }
        }
        public int number = 0;

        public void SetQuestion()
        {

            Number numberQus = this.ListQuestions[number];
            //this.questrionText.text = numberQus.num1.ToString() + " + " + numberQus.num2.ToString() + " = ?";
            switch (this.gameType)
            {
                case GameType.Addition:
                    this.questrionText.text = numberQus.num1.ToString() + " + " + numberQus.num2.ToString() + " = ?";
                    break;
                case GameType.Subtraction:
                    this.questrionText.text = numberQus.num1.ToString() + " - " + numberQus.num2.ToString() + " = ?";
                    break;
                case GameType.Multiplication:
                    this.questrionText.text = numberQus.num1.ToString() + " * " + numberQus.num2.ToString() + " = ?";
                    break;
                case GameType.Division:
                    this.questrionText.text = numberQus.num1.ToString() + " / " + numberQus.num2.ToString() + " = ?";
                    break;
            }
            this.questrionText.transform.localScale = Vector3.zero;
            this.questrionText.transform.DOScale(Vector3.one, 0.6f);
            SetHp();
        }
        public void SetHp()
        {
            this.arountText.text = around.ToString();
            for (int i = 0; i < this.IcHp.Count; i++)
            {
                this.IcHp[i].SetActive(i < this.numberHp);
            }
        }
        public void WrongAnser()
        {
            this.numberHp--;
            if (this.numberHp <= 0)
            {
                Debug.Log("EndGame");
                return;
            }
            for (int i = 0; i < this.IcHp.Count; i++)
            {
                this.IcHp[i].SetActive(i < this.numberHp);
            }

        }
        public void NextQuestion()
        {
            this.number++;
            if (this.number >= 25) return;
            SetQuestion();
        }

        public void CheckWinTurn(ButtonMath buttonMath)
        {

        }

        public void SpawnButton()
        {

        }

        IEnumerator Open10Candy()
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 25; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.5f);
                int randomNumber = GetRandomNumber(numbers);
                ButtonMath Button = Instantiate(this.ButtonMath);
                Button.transform.SetParent(this.buttonTransforms[randomNumber], false);
            }
        }

        int GetRandomNumber(List<int> numberList)
        {
            int randomIndex = UnityEngine.Random.Range(0, numberList.Count); // Lấy chỉ số ngẫu nhiên
            int selectedNumber = numberList[randomIndex]; // Lấy số tại chỉ số ngẫu nhiên
            numberList.RemoveAt(randomIndex); // Loại bỏ số đó khỏi danh sách
            return selectedNumber; // Trả về số đã chọn
        }


        [ContextMenu("Spawn")]

        public void Spawn()
        {
            if (a != null)
            {
                StopCoroutine(a);
            }
            a = StartCoroutine(_spawn());
        }
        Coroutine a;
        IEnumerator _spawn()
        {
            if (this.buttonTransforms.Count < 25) yield break;
            for (int i = 0; i < this.buttonTransforms.Count; i++)
            {
                MyFunction.ClearChild(this.buttonTransforms[i]);
            }
            int rows = 5;
            int columns = 5;
            List<int> numbers = new List<int>();
            if (this.ListQuestions == null || this.ListQuestions.Count <= 0) yield break;
            if (ListQuestions.Count < rows * columns) yield break;
            this.buttonMathsNew.Clear();
            for (int i = 1; i <= 25; i++)
            {
                numbers.Add(i);
            }
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    int index = row * columns + col;
                    Debug.Log($"Index: {index}, Row: {row}, Column: {col}");
                    ButtonMath Button = Instantiate(this.ButtonMath);
                    Button.transform.SetParent(this.buttonTransforms[index], false);
                    Button.indexX = row;
                    Button.indexY = col;
                    buttonMathsNew.Add(Button);
                    int randomNumber = GetRandomNumber(numbers);
                    Debug.Log("Số ngẫu nhiên chọn được là: " + randomNumber);
                    Button.result = ListQuestions[randomNumber - 1].result;
                    int numberAnimal = UnityEngine.Random.Range(0, DataAsset.instance.animals.Count);
                    Button.animal = DataAsset.instance.animals[numberAnimal];
                    Button.Init();
                    yield return new WaitForSeconds(0.02f);
                }
            }
        }

        public void SetFalse()
        {
            WrongAnser();
            for (int i = 0; i < this.buttonMathsNew.Count; i++)
            {
                if (this.buttonMathsNew[i].isDone) continue;
                if (this.buttonMathsNew[i].result == this.ListQuestions[number].result)
                {
                    this.buttonMathsNew[i].isDone = true;
                    this.buttonMathsNew[i].gameObject.SetActive(false);
                    return;
                }
            }
        }


        public void NextRound()
        {
            if (nextTurn != null)
            {
                StopCoroutine(nextTurn);
            }
            nextTurn = StartCoroutine(_NextRound());
        }

        Coroutine nextTurn;
        IEnumerator _NextRound()
        {
            for (int i = 0; i < this.buttonTransforms.Count; i++)
            {
                yield return new WaitForSeconds(0.05f);
                MyFunction.ClearChild(this.buttonTransforms[i]);
            }
            around++;
            this.arountText.text = around.ToString();
            //StartGame();
            StartPlay();
            }

        public void CheckAnswer()
        {
            for (int i = 0; i < this.buttonMathsNew.Count; i++)
            {
                if (this.buttonMathsNew[i].isDone) continue;
                if (this.buttonMathsNew[i].result == this.ListQuestions[this.number].result)
                {
                    this.buttonMathsNew[i].gameAnswer.SetActive(true);
                    return;
                }
            }
        }


        List<Number> GenerateRandomQuestions(int count)
        {
            //List<Number> additions = new List<Number>();
            //for (int i = 0; i < count; i++)
            //{
            //    int num1 = UnityEngine.Random.Range(1, 101); // Số ngẫu nhiên từ 1 đến 100
            //    int num2 = UnityEngine.Random.Range(1, 101);
            //    int result = num1 + num2; // Tính tổng của num1 và num2
            //    additions.Add(new Number(num1, num2, result)); // Thêm đối tượng Addition vào danh sách
            //}
            //return additions;
            List<Number> additions = new List<Number>();
            switch (this.gameType)
            {
                case GameType.Addition:
                    for (int i = 0; i < count; i++)
                    {
                        int num1 = UnityEngine.Random.Range(0, 100); // Số ngẫu nhiên từ 1 đến 100
                        int num2 = UnityEngine.Random.Range(0, 100);
                        int result = num1 + num2; // Tính tổng của num1 và num2
                        additions.Add(new Number(num1, num2, result)); // Thêm đối tượng Addition vào danh sách
                    }
                    break;
                case GameType.Subtraction:
                    for (int i = 0; i < count; i++)
                    {
                        int result = UnityEngine.Random.Range(1, 10);
                        int num2 = UnityEngine.Random.Range(1, 20);
                        int num1 = result + num2;
                        additions.Add(new Number(num1, num2, result));
                    }
                    break;
                case GameType.Multiplication:
                    for (int i = 0; i < count; i++)
                    {
                        int num1 = UnityEngine.Random.Range(1, 10);
                        int num2 = UnityEngine.Random.Range(1, 20);
                        int result = num1 * num2;
                        additions.Add(new Number(num1, num2, result));
                    }
                    break;
                case GameType.Division:
                    for (int i = 0; i < count; i++)
                    {
                        int result = UnityEngine.Random.Range(0, 10);
                        int num2 = UnityEngine.Random.Range(1, 20);
                        int num1 = result * num2;
                        additions.Add(new Number(num1, num2, result));
                    }
                    break;
            }
            return additions;
        }

        public void StartPlay()
        {
            starPlayObj.SetActive(false);
            playObj.SetActive(true);
            this.ListQuestions.Clear();
            this.ListQuestions = GenerateRandomQuestions(25);
            DataAsset.instance.numberQuestion = 0;
            this.number = 0;
            //this.numberHp = 5;
            //this.numberBaby = 0;
            this.around = 1;
            this.numberBabyText.text = numberBaby.ToString();
            this.Spawn();
            SetQuestion();
        }

        public void Exit()
        {
            starPlayObj.SetActive(true);
            playObj.SetActive(false);
        }
    }
}