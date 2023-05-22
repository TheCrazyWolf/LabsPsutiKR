using LabsPsutiKR.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabsPsutiKR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StartWindowViewModel();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult v = 
                MessageBox.Show("Вы действительно хотите закрыть окно?", "Внимание", 
                MessageBoxButton.OKCancel, MessageBoxImage.Question);

            switch (v)
            {
                case MessageBoxResult.OK:
                case MessageBoxResult.Yes:
                    e.Cancel = false;
                    break;
                case MessageBoxResult.Cancel:
                case MessageBoxResult.None:
                case MessageBoxResult.No:
                default:
                    e.Cancel = true;
                    break;
            }

            base.OnClosing(e);
        }
    }
}
