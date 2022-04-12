using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Newtonsoft.Json;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient httpClient = new();
        public static readonly HttpStatusCode StatusCode = HttpStatusCode.OK;
        public static ImageBrush brush1 = new();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (!IsRunAsAdmin())
            {
                MessageBox.Show("请以管理员身份运行");
                Close();
            }   
        }
        public static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_NCLBUTTONDBLCLK = 0xA3;//鼠标双击标题栏消息
            switch (msg)
            {
                case WM_NCLBUTTONDBLCLK:
                    handled = true;
                    break;
            }
            //return IntPtr.Zero;
            return hwnd;
        }
        private async void Login_Click_1(object sender, RoutedEventArgs e)
        {
            if (Init.Numc != 0)
            {
                return;
            }
            if (username.Text != null && username.Text.Length == 0)
            {
                MessageBox.Show("账号不能为空");
                return;
            }
            else if (password.Text != null && password.Text.Length == 0)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            else if (username.Text == password.Text)
            {
                MessageBox.Show("账号密码相同");
                return;
            }
            string can5;
            if (!Code.IsRegeditItemExist(@"SOFTWARE\Microsoft", "Rarts"))
            {
                can5 = Code.Xxreg();
            }
            else
            {
                can5 = Code.Qvalue();
            }
            string jsonParam = JsonConvert.SerializeObject(new
            {
                can11 = username.Text,
                can22 = password.Text,
                can33 = Rss.GetstrMD5(username.Text + "3!sx@1"),
                can44 = System.Web.HttpUtility.UrlEncode(Rss.Encrypt(Rss.GetstrMD5(password.Text + "3!sx@2"))),
                can55 = can5
            });
            string? blog = Http.HttpPost(Init.IP + "logv2/logv2", jsonParam, httpClient);
            if (blog == null)
            {
                MessageBox.Show("发生异常，请联系管理员处理", "异常");
                return;
            }
            RootObject? dyn = JsonConvert.DeserializeObject<RootObject>(blog);
            if (dyn != null && dyn.Status == 1)
            {
                MessageBox.Show(dyn.Msg, "错误");
                return;
            }
            else if (dyn != null && dyn.Status == 2)
            {
                //await Backs();
                Code.Xml_tw();
                Code.Xmls(Init.IP_xml, Init.port);
                try
                {
                    if (Init.Login)
                    {
                        if (checkBox1.IsChecked == true && username.Text.Length != 0 && password.Text.Length != 0)
                        {
                            string config = Environment.CurrentDirectory + @"\Data.dat";
                            string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config);
                            Hashtable para = new();
                            para.Add("ZH", username.Text);
                            para.Add("MM", password.Text);
                            para.Add("star", true);
                            EncryptUtilSeal.EncryptObject(para, ConfigFilePath);
                        }
                        System.Diagnostics.Process p = new();
                        //p.StartInfo.WorkingDirectory = 
                        p.StartInfo.FileName = Backlist.Eexe;
                        p.StartInfo.Arguments = "-u " + dyn.Msg + " -p " + Rss.GetstrMD5(dyn.Msg ?? "123554!") + " -z 1";
                        p.Start();
                        videos.Pause();
                        WindowState = WindowState.Minimized;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常");
                    Close();
                }

            }
        }
        public static async Task Backs()
        {
            MainWindow f = Init._form;
            if (Init.Login)
            {
                Init.Login = false;
            }
            string md5s = await Task.Run(() => Code.GetMD5(Backlist.Dj2_log));
            if (md5s != "873B7CAE713770E2E0A8B5C059BF330C")
            {
                MessageBox.Show("请重新解压登录器");
                f.Close();
            }
            if (Directory.Exists(Environment.CurrentDirectory + @"\game\lang"))
            {
                try
                {
                    Directory.Delete(Environment.CurrentDirectory + @"\game\lang");
                }
                catch
                {
                    f.Close();
                }
            }
            Code.Xml_tw();
            string dj2s = Code.GetMD5(Backlist.Dj2);
            string? blog1 = Http.HttpGet(Init.IP + "test/path?path=" + System.Web.HttpUtility.UrlEncode(Rss.Encrypt("1")), false, httpClient);
            if (blog1 != null)
            {
                if (dj2s != blog1)
                {
                    MessageBox.Show("文件损坏，请重新解压" + blog1);
                    f.Close();
                }
            }
            else
            {
                MessageBox.Show("初始化错误，请重试");
                f.Close();
            }
            string? lists = Http.HttpGet(Init.IP + "test/list", false, httpClient);
            if (lists == null)
            {
                MessageBox.Show("初始化失败，请重试");
                f.Close();
            }
            dynamic? dyn = JsonConvert.DeserializeObject(lists);
            Backlist model = new();
            var dict = Init.GetProperties(model);
            string? md5 = null;
            string[] back = new string[(int)dyn.Count * 2];
            int num = 0;
            Dictionary<string, int> dictionary = new();
            foreach (var item in dyn)
            {
                foreach (var ss in item)
                {
                    if (((string)ss.Value).Length > 30)
                    {
                        back[num] = (string)ss.Value;
                        num++;
                    }
                    else
                    {
                        dictionary.Add(ss.Name, (int)ss.Value);
                    }
                }
            }
            int num1 = 0;
            foreach (var i in dict)
            {
                md5 = await Task.Run(() => Code.GetMD5(i.Value.ToString()));
                if (md5 != back[num1])
                {
                    if (i.Key.ToString() != null)
                    {
                        Init.Numc++;
                        f.progressBar1.Visibility = Visibility.Visible;
                        f.resultLabel.Visibility = Visibility.Visible;
                        await Task.Run(() => Getback(i.Key.ToString() + ".spk", i.Value.ToString(), dictionary[i.Key.ToString() + "1"], httpClient));
                    }
                    else
                    {
                        MessageBox.Show("初始化失败，请重试");
                        f.Close();
                    }
                }
                num1++;
            }
            Pd(true);
        }
        public static void Pd(bool b)
        {
            if (Init.Numc == 0)
            {
                Init._form.Dispatcher.Invoke(delegate () {
                    Init.Login = b;
                    Init._form.Regin.IsEnabled = b;
                    Init._form.Retin.IsEnabled = b;
                    Init._form.Login.IsEnabled = b;
                    Init._form.progressBar1.Visibility = Visibility.Collapsed;
                    Init._form.resultLabel.Visibility = Visibility.Collapsed;
                });
            }
        }
       
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Init._form = this;
            Init.httpClient = httpClient;
            Code.Admin();
            Process[] myProcessA = Process.GetProcessesByName("dj2");
            foreach (Process process in myProcessA)
            {
                //MessageBox.Show(process.ToString());
                process.Kill();
            }
            
            resultLabel.Visibility = Visibility.Collapsed;
            progressBar1.Visibility = Visibility.Collapsed;
            videos.Source = new Uri(Backlist.Videos);
            new Thread(VideosPlay).Start();
            if (PresentationSource.FromVisual(this) is HwndSource source) source.AddHook(WndProc);
            string config = Environment.CurrentDirectory + @"\Data.dat";
            string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config);
            if (File.Exists(config))
            {
                //读取
                try
                {
                    Hashtable? para1 = new();
                    object obj = EncryptUtilSeal.DecryptObject(ConfigFilePath);
                    if (obj != null && para1 != null)
                    {
                        para1 = obj as Hashtable;
                        if ((bool)para1["star"])
                        {
                            checkBox1.IsChecked = true;
                            username.Text = para1["ZH"].ToString();
                            password.Text = para1["MM"].ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("配置文件定义为空", "错误");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "初始化错误-01");
                    this.Close();
                }
            }
            else
            {
                //写入
                Hashtable para = new();
                para.Add("ZH", "username");
                para.Add("MM", "password");
                para.Add("star", false);
                EncryptUtilSeal.EncryptObject(para, ConfigFilePath);
            }
            if (!Code.IsRegeditItemExist(@"SOFTWARE\Microsoft", "Rarts"))
            {
                Code.Xxreg();
            }

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            string? blog = Http.HttpGet(Init.IP + "test/v?v=" + System.Web.HttpUtility.UrlEncode(Rss.Encrypt("v01")), false, httpClient);
            if (blog != "1")
            {
                MessageBox.Show("登录器版本过旧，请更新");
                this.Close();
            }
            //await Backs();
        }
        public static async void Getback(string back, string filepath, int len, HttpClient httpClient)
        {
            using FileStream fs = new(filepath, FileMode.Truncate, FileAccess.ReadWrite);
            var buffer = new byte[0x1000];
            var bytes = await httpClient.GetStreamAsync(Init.IP + Init.Filedrv + back).ConfigureAwait(false);
            int down;
            float cent = 0;
            long totalRead = 0;
            while ((down = await bytes.ReadAsync(buffer)) > 0)
            {
                await fs.WriteAsync(buffer.AsMemory(0, down));
                totalRead += down;
                cent = (float)totalRead / len * 100;
                Ui_send(cent);
            }
            //Ui_send(cent, back, false);
            Init.Numc--;
            Pd(true);
        }
        public static void Ui_send(float cents)
        {
            Init._form.Dispatcher.Invoke(delegate ()
            {
                Init._form.progressBar1.Value = (int)cents;
                Init._form.resultLabel.Content = Init.Numc + " 下载进度:" + cents.ToString("#0.00") + "%";
            });
        }
        public void VideosPlay()
        {
            videos.Play();
        }
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Mini_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Regin_Click(object sender, RoutedEventArgs e)
        {
            Regin mainWindow = new();
            mainWindow.ShowDialog();
        }
        private void Retin_Click(object sender, RoutedEventArgs e)
        {
            Retin mainWindow = new();
            mainWindow.ShowDialog();
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {
            string config = Environment.CurrentDirectory + @"\Data.dat";
            string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config);
            if (checkBox1.IsChecked == true)
            {
                Hashtable para = new();
                if (username.Text.Length != 0 && password.Text.Length != 0)
                {
                    para.Add("ZH", username.Text);
                    para.Add("MM", password.Text);
                    para.Add("star", true);
                    EncryptUtilSeal.EncryptObject(para, ConfigFilePath);
                }
                else
                {
                    checkBox1.IsChecked = false;
                }
            }
            else
            {
                Hashtable para = new();
                para.Add("ZH", "username");
                para.Add("MM", "password");
                para.Add("star", false);
                EncryptUtilSeal.EncryptObject(para, ConfigFilePath);
            }
        }
    }
}
