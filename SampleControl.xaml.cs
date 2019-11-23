using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;
using System.Net;

namespace FingerDePrint
{
    /// <summary>
    /// SampleControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SampleControl : UserControl
    {
        //Image myImage;
        BitmapImage bitmapImage;
        Uri uri;
        String encodeImage;

        public SampleControl()
        {
            InitializeComponent();
        }

        private void button_fileopen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                uri = new Uri(op.FileName);
                bitmapImage = new BitmapImage(uri);
                sampleImage.Source = bitmapImage;

                /*
                byte[] data = File.ReadAllBytes(bitmapImage.ToString());
                string result = Convert.ToBase64String(data);
                log.Text = result;

                Bitmap bmp = new Bitmap(op.FileName);
                MemoryStream mem_stream = new MemoryStream();
                bmp.Save(mem_stream, System.Drawing.Imaging.ImageFormat.Bmp);
                log.Text = bmp;
                */

                byte[] data = File.ReadAllBytes(op.FileName);
                encodeImage = Convert.ToBase64String(data);
                log.Text = encodeImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //POST방식
            // 요청을 보내는 URI
            string strUri = "http://13.67.63.66/image";

            // POST, GET 보낼 데이터 입력
            StringBuilder dataParams = new StringBuilder();
            dataParams.Append("image=" + encodeImage);
            

            // 요청 String -> 요청 Byte 변환
            byte[] byteDataParams = UTF8Encoding.UTF8.GetBytes(dataParams.ToString());
            //byte[] byteDataParams = Encoding.ASCII.GetBytes(dataParams.ToString());
            /////////////////////////////////////////////////////////////////////////////////////
            // POST //
             // HttpWebRequest 객체 생성, 설정
             
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
             request.Method = "POST"; 
             request.ContentType = "application/x-www-form-urlencoded";
             request.ContentLength = byteDataParams.Length;
             
            // GET //
            // GET 방식은 Uri 뒤에 보낼 데이터를 입력하시면 됩니다.
            
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri + "?" + "image=asdf");
            //request.Method = "GET";
            
            //////////////////////////////////////////////////////////////////////////////////////
            
            // 요청 Byte -> 요청 Stream 변환
            Stream stDataParams = request.GetRequestStream();
            stDataParams.Write(byteDataParams, 0, byteDataParams.Length);
            stDataParams.Close();

            // 요청, 응답 받기
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // 응답 Stream 읽기
            Stream stReadData = response.GetResponseStream();
            StreamReader srReadData = new StreamReader(stReadData, Encoding.Default);

            // 응답 Stream -> 응답 String 변환
            string strResult = srReadData.ReadToEnd();

            Console.WriteLine(strResult);
            Console.ReadLine();
            


            //아래는 Get방식. 주석풀고 사용
            /*HttpWebRequest wReq;
            HttpWebResponse wRes;
            uri = new Uri("http://13.67.63.66/image?image=" + encodeImage); // string 을 Uri 로 형변환
            wReq = (HttpWebRequest)WebRequest.Create(uri); // WebRequest 객체 형성 및 HttpWebRequest 로 형변환
            wReq.Method = "GET"; // 전송 방법 "GET" or "POST"
            wReq.ServicePoint.Expect100Continue = false;
            //wReq.CookieContainer = new CookieContainer();
            //WebProxy proxy = new WebProxy("https://13.67.63.66/image", true);
            //proxy.Credentials = new NetworkCredential("ENable", "dpstiq", "ADomain");
            //wReq.Proxy = proxy;
            wReq.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            //wReq.CookieContainer.SetCookies(uri, cookie);
            using (wRes = (HttpWebResponse)wReq.GetResponse())
            {
                Stream respPostStream = wRes.GetResponseStream();
                StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("EUC-KR"), true);

                String resResult = readerPost.ReadToEnd();
                log.Text = resResult;
            }
            */

            


        }

    }
}
