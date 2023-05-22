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
        [ObservableProperty] private double[] arrayX;
        [ObservableProperty] private double[] arrayY;

        /* Сортировка */
        [ObservableProperty] private string textResult = "Результат пол или отриц чисел";
        [ObservableProperty] private double[] sortArray;

        public StartWindowViewModel()
        {
            ListTasks = new int[] { 3 };
        }

        [RelayCommand]
        private void DoResult()
        {

        }

    }
}
