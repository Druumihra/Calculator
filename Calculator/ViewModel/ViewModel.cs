using System;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator
{

    public partial class CalcViewModel : ObservableObject
    {
        [ObservableProperty]
        private decimal currentNumber;

        private decimal? firstNumber;

        bool isDecimal = false;
        public enum Operator
        {
            Addition = '+',
            Subtraction = '-',
            Division = '/',
            Multiplication = '*'
        }

        private Operator? currentOperator;
        public CalcViewModel()
        {
            currentNumber = 0;
            firstNumber = null;
            currentOperator = null;
        }


        [RelayCommand]
        public void addNumber(string number)
        {
            CurrentNumber *= 10;
            CurrentNumber += decimal.Parse(number);
        }


        [RelayCommand]
        public void operatorClick(string oper)
        {
            switch (oper) {
                case "+":
                    currentOperator = Operator.Addition;
                    break;
                case "-":
                    currentOperator = Operator.Subtraction;
                    break;
                case "/":
                    currentOperator = Operator.Division;
                    break;
                case "*":
                    currentOperator = Operator.Multiplication;
                    break;
                default:
                    return;
            }

            firstNumber = CurrentNumber;
            CurrentNumber = 0;
            isDecimal = false;

        }

    }
}