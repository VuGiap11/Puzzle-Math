using Rubik.ClawMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.BinGo
{
    [Serializable]
    public class Number
    {
        public int num1;
        public int num2;
        public int result;

        public Number(int num1, int num2, int result)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.result = result;
        }
    }
    public class DataAsset : MonoBehaviour
    {
        public static DataAsset instance;
        public ButtonMath[] buttonMaths; // Mảng một chiều
        public List<Number> additionList = new List<Number>();
        public List<Animal> animals = new List<Animal>();
        private int rows = 5;
        private int columns = 5;
        public int numberQuestion;
        public List<Sprite> imageAvar;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        void Start()
        {
            //List<Number> additionList = GenerateRandomAdditions(25);
            //StartGame();
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
            Debug.Log(buttonMaths.Length);
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

        public void SetResult()
        {
            if (this.additionList == null || this.additionList.Count <= 0) return;
            if (additionList.Count < rows * columns) return;
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 25; i++)
            {
                numbers.Add(i); // Thêm các số từ 1 đến 25 vào danh sách
            }
            for (int i = 0; i < 25; i++)  // Lấy 5 số ngẫu nhiên làm ví dụ
            {
                int randomNumber = GetRandomNumber(numbers);
                Debug.Log("Số ngẫu nhiên chọn được là: " + randomNumber);
                buttonMaths[i].result = additionList[randomNumber - 1].result;
                int numberAnimal = UnityEngine.Random.Range(0, this.animals.Count);
                buttonMaths[i].animal = this.animals[numberAnimal];
                buttonMaths[i].Init();

            }
            //for (int i = 0; i < this.buttonMaths.Length; i++) 
            //{

            //}
        }
        int GetRandomNumber(List<int> numberList)
        {
            int randomIndex = UnityEngine.Random.Range(0, numberList.Count); // Lấy chỉ số ngẫu nhiên
            int selectedNumber = numberList[randomIndex]; // Lấy số tại chỉ số ngẫu nhiên
            numberList.RemoveAt(randomIndex); // Loại bỏ số đó khỏi danh sách
            return selectedNumber; // Trả về số đã chọn
        }
        public Animal GetAnimalById(string id)
        {
            return animals.Find(a => { return a.animalData.Id == id; });
        }


    }
}