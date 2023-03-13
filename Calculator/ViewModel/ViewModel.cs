using System;
using System.Globalization;
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
            if (isDecimal)
            {
                string str = CurrentNumber.ToString();
                if (!str.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                    str += NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                CurrentNumber = decimal.Parse(str + number);
            }
            else
            {
            CurrentNumber *= 10;
            CurrentNumber += decimal.Parse(number);
            }
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

        [RelayCommand]
        public void clear()
        {
            CurrentNumber = 0;
            firstNumber = null;
            currentOperator = null;
        }

        [RelayCommand]
        public void inverse()
        {
            CurrentNumber = -CurrentNumber;
        }

        [RelayCommand]
        public void decimalClick() 
        {
            isDecimal = true;
        }

        [RelayCommand]
        public void percentageClick()
        {
            if (firstNumber is null || currentOperator is null) return;

            switch (currentOperator)
            {
                case Operator.Addition:
                    CurrentNumber = (decimal)(firstNumber + CurrentNumber / 100 * firstNumber);
                    break;
                case Operator.Subtraction:
                    CurrentNumber = (decimal)(firstNumber - CurrentNumber / 100 * firstNumber);
                    break;
                case Operator.Multiplication:
                    CurrentNumber = (decimal)(firstNumber * CurrentNumber / 100);
                    break;
                case Operator.Division:
                    CurrentNumber = (decimal)(firstNumber / CurrentNumber * 100);
                    break;
            }
        }

        [RelayCommand]
        public void compute()
        {
            if (firstNumber == null) return;

            switch (currentOperator)
            {
                case Operator.Addition:
                    CurrentNumber = (decimal)(firstNumber + CurrentNumber);
                    firstNumber = null;
                    break;
                case Operator.Subtraction:
                    CurrentNumber = (decimal)(firstNumber - CurrentNumber);
                    firstNumber = null;
                    break;
                case Operator.Multiplication:
                    CurrentNumber = (decimal)(firstNumber * CurrentNumber);
                    firstNumber = null;
                    break;
                case Operator.Division:
                    CurrentNumber = (decimal)(firstNumber / CurrentNumber);
                    firstNumber = null;
                    break;
            }
        }
    }
}