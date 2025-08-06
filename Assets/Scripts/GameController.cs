
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.math
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public Number number;
        public OperationType operationType;
        public MathType mathType; 
       
        public bool isAnswer;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        public List<int> SpawnNumbers(int a)
        {
            List<int> numbers = new List<int> { a };
            int count = 1;

            while (count < 4)
            {
                int randomNum = Random.Range(0, 101);
                if (randomNum != a && !numbers.Contains(randomNum))
                {
                    numbers.Add(randomNum);
                    count++;
                }
            }
            numbers = ShuffleList(numbers);
            return numbers;
        }
        private List<int> ShuffleList(List<int> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
            return list;
        }
        //private int number1, number2, result;
        //public Number GenerateRandomSumExpression()
        //{
        //    switch (this.operationType)
        //    {
        //        case OperationType.Addition:
        //            this.number1 = Random.Range(0, 100);
        //            this.number2 = Random.Range(0, 100 - number1);
        //            this.result = number1 + number2;
        //            break;
        //        case OperationType.Subtraction:
        //            this.result = Random.Range(0, 100);
        //            int number = 100 - result;
        //            this.number2 = Random.Range(0, number);
        //            this.number1 = number2 + result;
        //            break;
        //        case OperationType.Multipacation:
        //            this.number1 = Random.Range(1, 11);
        //            this.number2 = Random.Range(1, 11);
        //            this.result = number1 * number2;
        //            break;
        //        case OperationType.Division:
        //            this.result = Random.Range(1, 11);
        //            this.number2 = Random.Range(1, 11);
        //            this.number1 = number2 * result;
        //            break;
        //    }
        //    return new Number(number1, number2, result);
        //}
        public Number GenerateRandomSumExpression()
        {
            switch (this.operationType)
            {
                case OperationType.Addition:
                    this.number.num1 = Random.Range(0, 100);
                    this.number.num2 = Random.Range(0, 100 - this.number.num1);
                    this.number.result = this.number.num1 + this.number.num2;
                    break;
                case OperationType.Subtraction:
                    this.number.result = Random.Range(0, 100);
                    int number = 100 - this.number.result;
                    this.number.num2 = Random.Range(0, number);
                    this.number.num1 = this.number.num2 + this.number.result;
                    break;
                case OperationType.Multipacation:
                    this.number.num1 = Random.Range(1, 11);
                    this.number.num2 = Random.Range(1, 11);
                    this.number.result = this.number.num1 * this.number.num2;
                    break;
                case OperationType.Division:
                    this.number.result = Random.Range(1, 11);
                    this.number.num2 = Random.Range(1, 11);
                    this.number.num1 = this.number.num2 * this.number.result;
                    break;
            }
            // return new Number(number1, number2, result);
            return this.number;
        }
        public void SetType()
        {
            if (this.mathType == MathType.Test)
            {
                OperationType[] operations = new OperationType[]
           {
            OperationType.Addition,
            OperationType.Subtraction,
            OperationType.Multipacation,
            OperationType.Division
           };

                System.Random random = new System.Random();
                int index = random.Next(operations.Length);
                this.operationType = operations[index];
            }
            else if (this.mathType == MathType.Addition)
            {
                this.operationType = OperationType.Addition;
            }
            else if (this.mathType == MathType.Subtraction)
            {
                this.operationType = OperationType.Subtraction;
            }
            else if (this.mathType == MathType.Multipacation)
            {
                this.operationType = OperationType.Multipacation;
            }
            else if (this.mathType == MathType.Division)
            {
                this.operationType = OperationType.Division;
            }
        }

        public void SetQuestion()
        {
            switch (this.mathType)
            {
                case MathType.Addition:
                    AdditionManager.instance.SetQuestion();
                    break;
                case MathType.Subtraction:
                    SubtractionManager.instance.SetQuestion();
                    break;
                case MathType.Multipacation:
                    MultipacationManager.instance.SetQuestion();
                    break;
                case MathType.Division:
                    DivisionManager.instance.SetQuestion();
                    break;

                case MathType.Test:
                    TestManager.instance.SetQuestion();
                    break;
            }
        }

    }
}
