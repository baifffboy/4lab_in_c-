using System.IO;
using System.IO.Pipes;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4._1
{
    public partial class MainWindow : Window
    {
        System.Windows.Controls.Label label1 = new System.Windows.Controls.Label();
        System.Windows.Controls.Label label2 = new System.Windows.Controls.Label();
        TextBox box1 = new TextBox();
        TextBox box2 = new TextBox();
        TextBox box3 = new TextBox();
        TextBox box4 = new TextBox();
        Button button1 = new Button();
        List<string> list = new List<string>();
        const string path = @"C:\Users\ilya_\source\repos\Lab4.1\slovar.txt";

        public MainWindow()
        {
            InitializeComponent();
            Create_Elements();
            Application.Current.MainWindow.Height = 450;
            Application.Current.MainWindow.Width = 835;
            list = new List<string>();
        }

        private void Create_Elements()
        {
            label1 = new System.Windows.Controls.Label();
            label1.HorizontalAlignment = HorizontalAlignment.Left;
            label1.Height = 50;
            label1.Margin = new Thickness(10,  10, 0, 0);
            label1.VerticalAlignment = VerticalAlignment.Top;
            label1.Width = 113;
            label1.Content = "Введите первое \nслово";

            label2 = new System.Windows.Controls.Label();
            label2.HorizontalAlignment = HorizontalAlignment.Center;
            label2.Height = 50;
            label2.Margin = new Thickness(677, 10, 0, 0);
            label2.VerticalAlignment = VerticalAlignment.Top;
            label2.Width = 113;
            label2.Content = "Введите второе \nслово";

            box1 = new TextBox();
            box1.HorizontalAlignment = HorizontalAlignment.Left;
            box1.Height = 92;
            box1.Margin = new Thickness(10, 65, 0, 0);
            box1.TextWrapping = TextWrapping.NoWrap;
            box1.Text = "";
            box1.IsReadOnly = false;
            box1.VerticalAlignment = VerticalAlignment.Top;
            box1.Width = 113;

            box3 = new TextBox();
            box3.HorizontalAlignment = HorizontalAlignment.Left;
            box3.Height = 92;
            box3.Margin = new Thickness(693, 65, 0, 0);
            box3.TextWrapping = TextWrapping.NoWrap;
            box3.Text = "";
            box3.IsReadOnly = false;
            box3.VerticalAlignment = VerticalAlignment.Top;
            box3.Width = 113;

            box2 = new TextBox();
            box2.HorizontalAlignment = HorizontalAlignment.Center;
            box2.Height = 147;
            box2.Margin = new Thickness(-5, 10, 0, 0);
            box2.TextWrapping = TextWrapping.Wrap;
            box2.Text = "";
            box2.IsReadOnly = true;
            box2.VerticalAlignment = VerticalAlignment.Top;
            box2.Width = 544;

            box4 = new TextBox();
            box4.HorizontalAlignment = HorizontalAlignment.Left;
            box4.Height = 124;
            box4.Margin = new Thickness(170, 180, 0, 0);
            box4.TextWrapping = TextWrapping.Wrap;
            box4.Text = "";
            box4.IsReadOnly = false;
            box4.VerticalAlignment = VerticalAlignment.Top;
            box4.Width = 467;

            button1 = new Button();
            button1.HorizontalAlignment = HorizontalAlignment.Left;
            button1.Height = 42;
            button1.Margin = new Thickness(0, 330, 0, 0);
            button1.VerticalAlignment = VerticalAlignment.Top;
            button1.Width = 220;
            button1.Content = "Add Word";
            button1.HorizontalAlignment = HorizontalAlignment.Center;
            button1.Click += new RoutedEventHandler(ButtonCreatedByCode_Click);

            grid.Children.Add(box1);
            grid.Children.Add(box2);
            grid.Children.Add(box4);
            grid.Children.Add(box3);
            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(button1);
        }

        private void ButtonCreatedByCode_Click(object sender, RoutedEventArgs e)
        {
            if ((box1.Text != "" && box3.Text != "" && box4.Text != "") && (box1.Text.Length == box3.Text.Length))
            {
                box1.IsReadOnly = true;
                box3.IsReadOnly = true;
                bool t = false;
                int f = box1.Text.Length;
                if (list.Count == 0) {
                    list.Add(box1.Text + " -> ");
                }       
                string a = box4.Text;
                if (a.Length != box1.Text.Length)
                {
                    MessageBox.Show("Вы ввели слово, в котором больше/меньше букв, чем в изначальном");
                }
                else
                {
                    if ((proverca_na_sushestv(a) && a != box3.Text) && (onlyoneletter(a, f) && a.Length == box1.Text.Length))
                    {
                        list.Add(a + " -> ");
                        box2.Text += a + " -> ";
                        box4.Text = "";
                        t = true;
                    }

                    if (a == box3.Text)
                    {
                        list.Add(a);
                        box2.Text += a;
                        MessageBox.Show("Вы выйграли!\nВаша цепочка слов:\n" + list[0] + box2.Text + "\nСлов: " + list.Count);
                        System.Windows.Application.Current.Shutdown();
                    }
                    if (onlyoneletter(a, f) == false && t == false)
                    {
                        MessageBox.Show("Вы изменили больше одной буквы за ход");
                    }
                    if (proverca_na_sushestv(a) == false)
                    {
                        MessageBox.Show("Слова не существует!");
                    }
                    if (box1.Text.Length != box3.Text.Length)
                    {
                        MessageBox.Show("Размеры слов 1 и 2 должны быть одинаковыми!");
                    }
                }
            }
            else MessageBox.Show("Введите недостающие слова в поля и сделайте их одинаковыми по длине");
        }

        private bool onlyoneletter(string a, int f)
        {
            int m = 0;
            for (int i = 0; i<f; i++) {
                for (int j = 0; j < f; j++)
                {
                    if (i == j && list[list.Count - 1][i] == a[i]) {
                        m++;
                        break;
                    }
                }
            }
            if (m == f - 1) { 
                return true;
            }
            return false;          
        }

        private bool proverca_na_sushestv(string a)
        {
            foreach (string line in File.ReadLines(path))
            {
                if (line.Contains(a))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
// кот — дот — рот — пот или час — пас — пес — лес — вес
//липа, лупа, лапа,лара, тара,
//клён, клан, план, плен, плед
//бубен бубон бутон батон барон барин баран варан таран тиран тираж мираж вираж визаж
