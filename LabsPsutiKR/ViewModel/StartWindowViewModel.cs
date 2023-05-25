using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LabsPsutiKR.Model;
using LabsPsutiKR.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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
        [ObservableProperty] private ObservableCollection<double> arrayX;
        [ObservableProperty] private ObservableCollection<double> arrayY;

        /* Сортировка */
        [ObservableProperty] private string textResult = "Результат пол или отриц чисел";
        [ObservableProperty] private ObservableCollection<double> sortArray;

        public Action _eventWindowClose;

        public StartWindowViewModel()
        {
            ListTasks = new int[] { 3 };
        }

        [RelayCommand]
        private void CloseThisWindow()
        {
            _eventWindowClose();
        }

        [RelayCommand]
        private void DoResult()
        {

            ArrayX = new();
            ArrayY = new();

            double start = 0.0; // Начальное значение диапазона x
            double end = 5.0; // Конечное значение диапазона x
            double step = 0.5; // Шаг

            double A = SelectedValueA; // Значение A

            for (double x = start; x <= end; x += step)
            {
                double y = SolveEquation(x, A);

                if (double.IsNaN(x) || double.IsNaN(y))
                    continue;

                ArrayX.Add(x);
                ArrayY.Add(y);
               
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


        [RelayCommand]
        private void MaxItemArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            MessageBox.Show(templist.Max().ToString());
        }

        [RelayCommand]
        private void MinItemArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            MessageBox.Show(templist.Min().ToString());
        }

        [RelayCommand]
        private void SumItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            MessageBox.Show(templist.Sum().ToString());
        }

        [RelayCommand]
        private void MultipleItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);

            double result = 0;

            foreach (var item in templist)
            {
                if (result == 0)
                    result = item;

                result *= item;
            }
            
            MessageBox.Show(result.ToString());
        }


        [RelayCommand]
        private void SumNegativeItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            MessageBox.Show(templist.Where(x=> x <= 0).Sum().ToString());
        }

        [RelayCommand]
        private void SumPositiveItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            MessageBox.Show(templist.Where(x => x >= 0).Sum().ToString());
        }


        [RelayCommand]
        private void MultiplePsotiveItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            var s = templist.Where(x => x >= 0);
            double result = 0;

            foreach (var item in s)
            {
                if (result == 0)
                    result = item;
                result *= item;
            }

            MessageBox.Show(result.ToString());
        }

        [RelayCommand]
        private void MultipleNegativeItemsArray()
        {
            ObservableCollection<double> templist = ServiceTwoListToOne(ArrayX, ArrayY);
            var s = templist.Where(x => x <= 0);
            double result = 0;

            foreach (var item in s)
            {
                if (result == 0)
                    result = item;
                result *= item;
            }

            MessageBox.Show(result.ToString());
        }


        [RelayCommand]
        private void SortPositiveArray()
        {
            TextResult = "Пол элементы массива";
            var s = ServiceTwoListToOne(ArrayX, ArrayY).Where(x => x >= 0).Order().ToList();
            SortArray = new();
            foreach (var item in s)
            {
                SortArray.Add(item);
            }
        }

        [RelayCommand]
        private void SortNegativeArray()
        {
            TextResult = "Пол элементы массива";
            var s = ServiceTwoListToOne(ArrayX, ArrayY).Where(x => x <= 0).Order().ToList();
            SortArray = new();
            foreach (var item in s)
            {
                SortArray.Add(item);
            }
        }


        [RelayCommand]
        private void ShowAboutWindows()
        {
            new About().ShowDialog();
        }

        [RelayCommand]
        private void SaveData()
        {
            ModelData data = new ModelData()
            {
                arrayX = ArrayX,
                arrayY = ArrayY,
                selectedValueA = SelectedValueA,
                sortArray = SortArray
            };

            var json = JsonSerializer.Serialize(data);

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text File (*.json)|*.json|Show All Files (*.*)|*.*",
                FileName = "Untitled",
            };

            dialog.ShowDialog();

            File.WriteAllText(dialog.FileName, json);
        }


        [RelayCommand]
        private void OpenData()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Text File (*.json)|*.json|Show All Files (*.*)|*.*",
            };

            dialog.ShowDialog();
            try
            {
                var json = File.ReadAllText(dialog.FileName);
                ModelData obj = JsonSerializer.Deserialize<ModelData>(json);
                ArrayX = obj.arrayX;
                ArrayY = obj.arrayY;
                SortArray = obj.sortArray;
                SelectedValueA = obj.selectedValueA;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии файла");
;            }
                         
        }

        private ObservableCollection<double> ServiceTwoListToOne(ObservableCollection<double> one, 
            ObservableCollection<double> two)
        {
            ObservableCollection<double> newList = new();

            foreach (var item in one)
            {
                if (newList.Contains(item))
                    continue;
                newList.Add(item);
            }

            foreach (var item in two)
            {
                if (newList.Contains(item))
                    continue;
                newList.Add(item);
            }

            return newList;
        }

    }
}
