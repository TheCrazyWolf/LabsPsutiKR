using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsPsutiKR.ViewModel
{
    [Serializable]
    internal partial class StartWindowViewModel : ObservableObject
    {
        /* Значение А*/
        [ObservableProperty] private double selectedValueA;

        /* Список заданий и выбранное */
        [ObservableProperty] private int[] listTasks;
        [ObservableProperty] private int selectedTask;

        /* X и ИГРЕС*/
        [ObservableProperty] private List<double> arrayX;
        [ObservableProperty] private List<double> arrayY;

        /* Сортировка */
        [ObservableProperty] private string textResult = "Результат пол или отриц чисел";
        [ObservableProperty] private List<double> sortArray;

        public StartWindowViewModel()
        {
            ListTasks = new int[] { 3 };

            arrayY = new();
            arrayX = new();
        }

        [RelayCommand]
        private void DoResult()
        {
            double start = 0.0; // Начальное значение диапазона x
            double end = 5.0; // Конечное значение диапазона x
            double step = 0.5; // Шаг

            double A = 0.0; // Значение A

            for (double x = start; x <= end; x += step)
            {
                double y = SolveEquation(x, A);


                if (double.IsNaN(x) || double.IsNaN(y))
                    continue;

                arrayX.Add(x);
                arrayY.Add(y);
               
            }
        }

        public static double SolveEquation(double x, double A)
        {
            double y;

            // Решение уравнения y(x) = e^(2-x)
            y = Math.Exp(2 - x);

            // Решение уравнения y(x) = (a + ln(x)^2)
            y = A + Math.Pow(Math.Log(x), 2);

            // Решение уравнения y(x) = (x / (a - x))
            y = x / (A - x);

            return y;
        }







    }
}
