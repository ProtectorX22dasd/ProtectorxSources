using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace protector_x_v5
{
    /// <summary>
    /// downloadmsg.xaml etkileşim mantığı
    /// </summary>
    public partial class downloadmsg : Window
    {
        public downloadmsg()
        {
           // new Task(delegate { //});
            InitializeComponent();
       // });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool drag = Mouse.LeftButton == MouseButtonState.Pressed;
            if (drag)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            downloadlabel.Content = "Protector X Downloading\nFiles Please Wait.";
        }
    }
}
