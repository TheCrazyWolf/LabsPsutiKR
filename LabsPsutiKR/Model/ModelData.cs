using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsPsutiKR.Model
{
    [Serializable]
    internal class ModelData
    {
        public double selectedValueA { get; set; }
        public ObservableCollection<double> arrayX { get; set; }
        public ObservableCollection<double> arrayY { get; set; }
        public ObservableCollection<double> sortArray { get; set; }
    }
}
