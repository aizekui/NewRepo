using Newtonsoft.Json;
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

namespace WpfApp1
{
    /// <summary>
    /// Regin.xaml 的交互逻辑
    /// </summary>
    public partial class Regin : Window
    {
        public Regin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string? username = name.Text;
            string? password = pass.Text;
            string? email = mail.Text;
            string? Tencent = qq.Text;
            if (username != null && username.Length == 0)
            {
                MessageBox.Show("账号不能为空");
                return;
            }
            else if (password != null && password.Length == 0)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            else if (email != null && email.Length == 0)
            {
                MessageBox.Show("邮箱不能为空");
                return;
            }
            else if (Tencent != null && Tencent.Length == 0)
            {
                MessageBox.Show("QQ不能为空");
                return;
            }
            else if (username != null && username.Length < 6)
            {
                MessageBox.Show("账号长度不能小于6位");
                return;
            }
            else if (password != null && password.Length < 6)
            {
                MessageBox.Show("密码长度不能小于6位");
                return;
            }
            string can51;
            if (!Code.IsRegeditItemExist(@"SOFTWARE\Microsoft", "Rarts"))
            {
                can51 = Code.Xxreg();
            }
            else
            {
                can51 = Code.Qvalue();
            }
            string jsonParam = JsonConvert.SerializeObject(new
            {
                can1 = System.Web.HttpUtility.UrlEncode(Rss.Encrypt(username)),
                can2 = System.Web.HttpUtility.UrlEncode(Rss.Encrypt(password)),
                can3 = email,
                can4 = Tencent,
                can5 = can51
            });
            string? blog = Http.HttpPost(Init.IP + "Regv2/regv2", jsonParam, Init.httpClient);
            if (blog != null)
            {
                RootObject? dyn = JsonConvert.DeserializeObject<RootObject>(blog);
                if (dyn != null && dyn.Status == 1)
                {
                    MessageBox.Show(dyn.Msg, "错误");
                }
                else if (dyn != null && dyn.Status == 2)
                {
                    MessageBox.Show(dyn.Msg, "注册成功");
                    MainWindow forms = Init._form;
                    forms.username.Text = username;
                    forms.password.Text = password;
                    //forms.checkBox1.Checked = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("登录器注册错误，请联系管理员");
            }
        }
    }
}
