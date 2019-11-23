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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FingerDePrint
{
    /// <summary>
    /// TestImageControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TestImageControl : UserControl
    {
        public TestImageControl()
        {
            InitializeComponent();
            CreateTestImage();
        }

        public void CreateTestImage()
        {
            for(int i=0; i<8; i++)
            {
                Button btn = new Button();
                Image img = new Image();
                BitmapImage bitmapImage = 
                    new BitmapImage(new System.Uri(@"/image/image" + i.ToString()+".jpg", UriKind.Relative));
                img.Source = bitmapImage;
                btn.Content = img;
                btn.Height = Double.NaN;
                btn.Background = Brushes.Transparent;
                btn.Click += Image_bt_Click;
                ImagesGrid.Children.Add(btn);
                Grid.SetRow(btn, i/3);
                Grid.SetColumn(btn, i%3);
            }
        }

        private void Image_bt_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            Button btn = (Button)sender;
            image.Source = ((Image)btn.Content).Source;
        }
    }
}
