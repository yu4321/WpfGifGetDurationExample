using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;
using XamlAnimatedGif;

namespace WpfApplication7
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string neutral = "https://mblogthumb-phinf.pstatic.net/20120403_221/kmy9357_1333388898095t5iGH_GIF/01.gif?type=w2";
        private string walk = "https://mblogthumb-phinf.pstatic.net/20120403_285/kmy9357_1333388898604Qw1In_GIF/02.gif?type=w2";
        private string run = "https://mblogthumb-phinf.pstatic.net/20120403_28/kmy9357_1333388903585uCtDl_GIF/05.gif?type=w2";
        private string move = "https://w.namu.la/s/263c3a81083187762788047911a925ecbaac6cc9f8f4b8286e59feb91c9f54202345305af812cd3f494ac569950deec62d45af243ff9f6f190d61ab7383a57adf7bbca85f516563b6dd2fdb83c15bd42554000eba109aaa2968047cf3d8fe1e1";

        private BitmapImage[] imgs;
        private TimeSpan[] durations;
        private DispatcherTimer timer;
        private int count = 0;
        public MainWindow()
        {
            InitializeComponent();

            MakeGifLists(new[] {
                neutral,walk,run,move
            });

            //var b1 = new WebClient().DownloadData(neutral);
            //var img1 = new BitmapImage();
            //img1.BeginInit();
            //img1.CacheOption = BitmapCacheOption.OnLoad;
            //img1.StreamSource = new MemoryStream(b1);
            //img1.EndInit();

            //var b2 = new WebClient().DownloadData(walk);
            //var img2 = new BitmapImage();
            //img2.BeginInit();
            //img2.CacheOption = BitmapCacheOption.OnLoad;
            //img2.StreamSource = new MemoryStream(b2);
            //img2.EndInit();

            //var b3 = new WebClient().DownloadData(run);
            //var img3 = new BitmapImage();
            //img3.BeginInit();
            //img3.CacheOption = BitmapCacheOption.OnLoad;
            //img3.StreamSource = new MemoryStream(b3);
            //img3.EndInit();

            //imgs = new[]
            //{
            //    img1,img2,img3
            //};

            //durations = new[]
            //{
            //    GifExtension.GetGifDuration(img1), GifExtension.GetGifDuration(img2), GifExtension.GetGifDuration(img3)
            //};
           
            //btnNeutral_Click(null, null);
        }
        
        private async void MakeGifLists(string[] urls)
        {
            List<BitmapImage> imgs = new List<BitmapImage>();
            List<TimeSpan> tss = new List<TimeSpan>();

            Console.WriteLine("Start Init");
            foreach(var url in urls)
            {
                Console.WriteLine($"Download start - " + url);
                var b1 = new WebClient().DownloadData(url);
                var img1 = new BitmapImage();
                img1.BeginInit();
                img1.CacheOption = BitmapCacheOption.OnLoad;
                img1.StreamSource = new MemoryStream(b1);
                img1.EndInit();

                var duration = GifExtension.GetGifDuration(img1);

                imgs.Add(img1);
                tss.Add(duration);
                Console.WriteLine($"Download And Set Complete - " + url);
            }

            this.imgs = imgs.ToArray();
            this.durations = tss.ToArray();

            Console.WriteLine("End Init");

            btnNeutral_Click(null, null);
        }

        private void btnNeutral_Click(object sender, RoutedEventArgs e)
        {
            AnimationBehavior.SetSourceStream(holder, imgs[0].StreamSource);
            StartRepeatChecker(durations[0]);
            //AnimationBehavior.SetSourceUri(holder, imgs[0].UriSource);
        }

        private void btnWalk_Click(object sender, RoutedEventArgs e)
        {
            AnimationBehavior.SetSourceStream(holder, imgs[1].StreamSource);
            StartRepeatChecker(durations[1]);
            //AnimationBehavior.SetSourceUri(holder, imgs[1].UriSource);
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            AnimationBehavior.SetSourceStream(holder, imgs[2].StreamSource);
            StartRepeatChecker(durations[2]);
            //AnimationBehavior.SetSourceUri(holder, imgs[2].UriSource);
        }

        private void StartRepeatChecker(TimeSpan sp)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            count = 0;
            duration.Text = sp.ToString(@"mm\:ss\.ff");
            repeated.Text = "0";

            timer = new DispatcherTimer();
            timer.Interval = sp;

            timer.Tick += (s, e) =>
            {
                count++;
                Console.WriteLine($"completed {count}");
                repeated.Text = count.ToString();
            };

            timer.Start();
        }

        private void holder_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("loaded");
        }

        private void holder_AnimationCompleted(DependencyObject d, AnimationCompletedEventArgs e)
        {
            Console.WriteLine("completed");
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            AnimationBehavior.SetSourceStream(holder, imgs[3].StreamSource);
            StartRepeatChecker(durations[3]);
        }
    }

   
}
