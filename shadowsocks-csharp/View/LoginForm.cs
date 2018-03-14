using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shadowsocks.Controller;

namespace Shadowsocks.View
{
    public partial class LoginForm : Form
    {
        private int _loginErrorTimes = 1;
        private const int MAXLOGININPUTTIMES = 5;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void ShowWindow()
        {
            this.Opacity = 1;
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.baidu.com");  //利用Process.Start来打开
            linkLabel1.LinkVisited = true;  //链接是否被访问过,仅能代码来实现
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://lidechenyan.club/");  //利用Process.Start来打开
            linkLabel2.LinkVisited = true;  //链接是否被访问过,仅能代码来实现
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean input_error = judgeInputError();
            if (input_error)
            {
                if (_loginErrorTimes < MAXLOGININPUTTIMES)
                {
                    linkLabel3.Text = "密码或账号错误，你还有" + (MAXLOGININPUTTIMES - (_loginErrorTimes)) + "次输入机会";
                    linkLabel3.Visible = true;
                    linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
                    _loginErrorTimes++;
                }
                else
                {
                    linkLabel3.Text = "您输入错了" + MAXLOGININPUTTIMES + "次，请找回密码";
                    linkLabel3.Visible = true;
                    linkLabel3.LinkVisited = true;
                    linkLabel3.LinkArea = new LinkArea(linkLabel3.Text.IndexOf("请") + 1, 4);
                    button1.Enabled = false;
                }

            }
            else
            {
                ShadowsocksController controller = new ShadowsocksController();

                MenuViewController viewController = new MenuViewController(controller);

                controller.Start();

                this.Hide();
            }

        }

        private Boolean judgeInputError()
        {
            String name = textBox1.Text;
            String password = textBox2.Text;

            if (null == name || "".Equals(name) || null == password || "".Equals(password))
            {
                return true;
            }

            if (name.Equals("lide") && password.Equals("lide123"))
            {
                return false;
            }

            return true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://lidechenyan.club/");  //利用Process.Start来打开
            linkLabel3.LinkVisited = true;  //链接是否被访问过,仅能代码来实现
            button1.Enabled = true;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
