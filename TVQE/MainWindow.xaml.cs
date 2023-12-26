using Function;
using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TVQE.Context;
using System.Windows.Media.Animation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using TVQE.Models;

namespace TVQE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// дата и время
        /// </summary>
        public DispatcherTimer Timer { get; set; }

        /// <summary>
        /// IP адрес копитера
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// элемент для запуска Аудио
        /// </summary>
        MediaElement MediaElementAudio { get; set; }

        /// <summary>
        /// элемент для запуска Видео
        /// </summary>
        MediaElement mediaElementVideo { get; set; }

        /// <summary>
        /// Вызванные талоны
        /// </summary>
        Queue<CallTickets> CallTickets = new Queue<CallTickets>();

        ///аудио на проигриваниии
        Queue<string> audioFiles;

        List<string> audioFilesVideo;

        private int currentIndexVideo;

        private int currentIndex;

        public MainWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            try
            {
                GetIp();
                //подключение к базе
                EqContext eqContext = new EqContext();
                // Офис  
                HeaderTextBlockOfice.Text = eqContext.SOffices.First(l => l.Id == eqContext.SOfficeScoreboards.First(g => g.ScoreboardIp == Ip).SOfficeId).OfficeName;
                runningText.Text = eqContext.SOfficeScoreboardTexts.First(l => l.IsActive && l.SOfficeScoreboardId == eqContext.SOfficeScoreboards.First(g => g.ScoreboardIp == Ip).Id).TextMonitor.ToString();
               
            }
            catch (Exception ex)
            {

            }

            VideoPlay();
            Table();
            StartRunningTextAnimation();
            DateTimeView();
        }
        #region Video
        private void VideoPlay()
        {
            var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)));
            var pachVideo1 = path + "\\Video\\v1.mp4";
            var pachVideo2 = path + "\\Video\\v2.mp4";
            var pachVideo3 = path + "\\Video\\v3.mp4";

            audioFilesVideo = new List<string> {
                    pachVideo1,
                    pachVideo2,
                    pachVideo3,
            };

            MediaElementVideo.Children.Clear();
            mediaElementVideo = new MediaElement();
            MediaElementVideo.Children.Add(mediaElementVideo);
            mediaElementVideo.VerticalAlignment= VerticalAlignment.Top;
            mediaElementVideo.Source = new Uri(audioFilesVideo[currentIndexVideo]);
            mediaElementVideo.LoadedBehavior = MediaState.Manual;
            mediaElementVideo.UnloadedBehavior = MediaState.Play;
            mediaElementVideo.Play();

            mediaElementVideo.MediaEnded += (s, e) =>
            { 
                MediaEndedVideo(s,e); 
            };
        }
        private void MediaEndedVideo(object sender, RoutedEventArgs e)
        {
            currentIndexVideo++;
            if (currentIndexVideo == audioFilesVideo.Count() - 1) currentIndexVideo = 0;
            MediaElementVideo.Children.Clear();
            mediaElementVideo = new MediaElement();
            mediaElementVideo.Source = new Uri(audioFilesVideo[currentIndexVideo]);
            mediaElementVideo.LoadedBehavior = MediaState.Manual;
            mediaElementVideo.UnloadedBehavior = MediaState.Play;
            mediaElementVideo.VerticalAlignment = VerticalAlignment.Top;
            MediaElementVideo.Children.Add(mediaElementVideo);
            mediaElementVideo.Play();
            mediaElementVideo.MediaEnded += (s, e) =>
            {
                MediaEndedVideo(s, e);
            };
        }
        #endregion


        #region Server
        public async Task StartListeningAsync(Window window, string ip)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(ip), 1235);
            listener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }


        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                byte[] buffer = new byte[1024];
                StringBuilder messageBuilder = new StringBuilder();

                using (NetworkStream stream = client.GetStream())
                {
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        messageBuilder.Append(receivedMessage);
                    }
                }

                string message = messageBuilder.ToString();
                if (message == "Pocess")
                {
                    Table();
                }
                else
                {
                    string[] ident = message.Split(':');

                    string type = ident[0];
                    string value = ident[1];

                    switch (type)
                    {
                        case "Call":
                            try
                            { 
                                EqContext context = new EqContext();
                                var TicketCall = context.DTickets.First(t => t.Id == Convert.ToInt64(value));
                                var windowName = context.SOfficeWindows.First(w => w.Id == TicketCall.SOfficeWindowId).WindowName;
                                CallTickets.Enqueue(new CallTickets(TicketCall.ServicePrefix, TicketCall.TicketNumber.ToString(), windowName));
                                Call.Visibility = Visibility.Visible;
                                if (CallTickets.Count == 1) await CallTicket();
                            }
                            catch (Exception ex)
                            {
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region талоны на обсуживаии
        private void Table()
        {
            try
            {
                EqContext eqContext = new EqContext();
                var tickets = Tickets.SelectTicketServed(eqContext.SOfficeScoreboards.First(r => r.ScoreboardIp == Ip).Id);
                ListQE.Children.Clear();
                WrapPanel wrapPanelH = new WrapPanel();
                wrapPanelH.Width= 900;
                WrapPanel wrapPanel1H = new WrapPanel();
                wrapPanel1H.HorizontalAlignment= HorizontalAlignment.Center; 
                TextBlock textBlock1H = new TextBlock();
                textBlock1H.FontSize = 50;
                wrapPanel1H.Width= 450;
                textBlock1H.TextAlignment = TextAlignment.Center;
                textBlock1H.FontFamily = new FontFamily("Arial");
                textBlock1H.Text = "КЛИЕНТ";

                wrapPanel1H.Children.Add(textBlock1H);
                wrapPanelH.Children.Add(wrapPanel1H);

                WrapPanel wrapPanel2H = new WrapPanel();
                wrapPanel2H.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock textBlock2H = new TextBlock();
                textBlock2H.FontSize = 50;
                wrapPanel2H.Width = 450;
                textBlock2H.TextAlignment = TextAlignment.Center;
                textBlock2H.FontFamily = new FontFamily("Arial");
                textBlock2H.Text = "ОКНО";

                wrapPanel2H.Children.Add(textBlock2H);
                wrapPanelH.Children.Add(wrapPanel2H);

                ListQE.Children.Add(wrapPanelH);

                if (tickets.Any())
                {
                    tickets.ToList().ForEach(t =>
                    {
                        WrapPanel wrapPanel = new WrapPanel();
                        wrapPanel.Width = 900;
                        WrapPanel wrapPanel1 = new WrapPanel(); 
                        TextBlock textBlock1 = new TextBlock();
                        textBlock1.FontSize = 50;
                        textBlock1.Width = 450; 
                        textBlock1.Foreground = new SolidColorBrush(Colors.Red);
                        textBlock1.FontFamily = new FontFamily("Arial");
                        textBlock1.Text = t.TicketNumberFull;
                        wrapPanel1.Children.Add(textBlock1);
                        wrapPanel.Children.Add(wrapPanel1);

                        WrapPanel wrapPanel2 = new WrapPanel();  
                        TextBlock textBlock2 = new TextBlock();
                        textBlock2.Foreground = new SolidColorBrush(Colors.Red);
                        textBlock2.FontSize = 50;
                        textBlock2.Width = 450;
                        textBlock2.FontFamily = new FontFamily("Arial");
                        textBlock2.Text = t.WindowName;
                        wrapPanel2.Children.Add(textBlock2);
                        wrapPanel.Children.Add(wrapPanel2);

                        ListQE.Children.Add(wrapPanel);
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Бегущая строка
        private void StartRunningTextAnimation()
        {
            double canvasWidth = canvas.ActualWidth;
            double textWidth = runningText.Text.Length * 6.5;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = canvasWidth - textWidth;
            animation.EasingFunction = new QuadraticEase();
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.Duration = new Duration(TimeSpan.FromSeconds(10));

            runningText.BeginAnimation(Canvas.LeftProperty, animation);
        }
        #endregion

        #region получение IP
        private async void GetIp()
        {
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            string IpOffise = "";

            foreach (IPAddress address in localIPs)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IpOffise = address.ToString();
                }
            }
            Ip = IpOffise;

            await StartListeningAsync(this, IpOffise);
        }
        #endregion

        #region Дата и время
        private void DateTimeView()
        {

            // Создаем таймер для обновления даты и времени каждую секунду
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

            // Установка начальных значений даты и времени
            UpdateDateTime();
        }
        #endregion

        #region Обновляем дату и время при каждом срабатывании таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновляем дату и время при каждом срабатывании таймера
            UpdateDateTime();
        }
        #endregion

        #region Обновляем значения даты и времени
        private void UpdateDateTime()
        {
            // Обновляем значения даты и времени
            DateTime now = DateTime.Now;
            TextBlock textBlockD = new TextBlock();
            textBlockD.FontFamily = new FontFamily("Area");
            textBlockD.FontSize = 30;
            textBlockD.Foreground = new SolidColorBrush(Color.FromRgb(137, 116, 81));
            textBlockD.Text = now.ToString("D") + "         " + now.ToString("HH:mm:ss");

            TextBlock textBlockdddd = new TextBlock();
            textBlockdddd.FontFamily = new FontFamily("Area");
            textBlockdddd.FontSize = 30;
            textBlockdddd.HorizontalAlignment = HorizontalAlignment.Right;
            textBlockdddd.Foreground = new SolidColorBrush(Color.FromRgb(137, 116, 81));
            textBlockdddd.Text = now.ToString("dddd");
            HeaderTextBlock.Children.Clear();

            HeaderTextBlock.Children.Add(textBlockD);
            HeaderTextBlock.Children.Add(textBlockdddd);
        }
        #endregion

        #region Вызов талона
        private async Task CallTicket()
        {

            MediaElement.Children.Clear();

            foreach (var tickets in CallTickets)
            {
                try
                {

                    MediaElementVideo.Visibility = Visibility.Collapsed;
                    Call.Visibility = Visibility.Visible;
                    MediaElementAudio = new MediaElement();
                    MediaElement.Children.Clear();
                    audioFiles = tickets.QueueAudioFiles;
                    currentIndex = 0;
                    // Добавь MediaElement на контейнер (Grid, StackPanel, и т.д.)
                    MediaElement.Children.Add(MediaElementAudio);
                    // Подпишись на событие MediaEnded для переключения на следующий аудиофайл
                    MediaElementAudio.MediaEnded += async (s, e) =>
                    {
                        await MediaElement_MediaEnded(s, e);
                    };
                    // Запуск проигрывания первого аудиофайла
                    MediaElementAudio.Source = new Uri(tickets.QueueAudioFiles.Dequeue()+".mp3");
                      
                    CallTicketName.Text = tickets.Prefix + tickets.Number;
                    CallWindow.Text = tickets.WindowName;
                }
                catch (Exception ex)
                {
                }
            }

        }

        private async Task MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Переключение на следующий аудиофайл, если он есть
            if (currentIndex < audioFiles.Count - 1)
            {
                try
                {
                    currentIndex++;
                    MediaElementAudio = new MediaElement();
                    MediaElement.Children.Clear();
                    MediaElement.Children.Add(MediaElementAudio);
                    MediaElementAudio.MediaEnded += async (s, e) =>
                    {
                        await MediaElement_MediaEnded(s, e);
                    };
                    MediaElementAudio.Source = new Uri(audioFiles.Dequeue() + ".mp3");
                    MediaElementAudio.Play();
                }
                catch (Exception ex)
                {

                }
            }
            else
            { 
                MediaElementVideo.Visibility = Visibility.Visible;
                Call.Visibility = Visibility.Collapsed; 
                await CallTicket(); 
                mediaElementVideo.Play();
            }
        }
        #endregion
         
    }
}
