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
    /// Retin.xaml 的交互逻辑
    /// </summary>
    public partial class Retin : Window
    {
        public Retin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ret_Click(object sender, RoutedEventArgs e)
        {
            string username = name.Text;
            string newpswds = newpass.Text;
            string email = mail.Text;
            string tencent = qq.Text;
            if (username != null && username.Length == 0)
            {
                MessageBox.Show("账号不能为空");
                return;
            }
            else if (newpswds != null && newpswds.Length == 0)
            {
                MessageBox.Show("新密码不能为空");
                return;
            }
            else if (email != null && email.Length == 0)
            {
                MessageBox.Show("邮箱不能为空");
                return;
            }
            else if (tencent != null && tencent.Length == 0)
            {
                MessageBox.Show("QQ不能为空");
                return;
            }
            string jsonParam = JsonConvert.SerializeObject(new
            {
                can1 = username,
                can2 = newpswds,
                can3 = email,
                can4 = tencent
            });
            string? resp = Http.HttpPost(Init.IP + "retv2/retv2", jsonParam, Init.httpClient);
            RootObject? dyn = JsonConvert.DeserializeObject<RootObject>(resp);
            if (dyn.Status == 1)
            {
                MessageBox.Show(dyn.Msg, "错误");
            }
            else if (dyn.Status == 2)
            {
                MessageBox.Show(dyn.Msg, "修改成功");
            }
        }
    }
}
