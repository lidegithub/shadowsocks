using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shadowsocks.Controller;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

namespace Shadowsocks.View
{
    public partial class LoginForm : BaseForm
    {
        private int _loginErrorTimes = 1;
        private const int MAXLOGININPUTTIMES = 5;
        private const string USER_LOGIN_FILE = "user-login.txt";
        public Boolean autologin = false;

        public LoginForm()
        {
            InitializeComponent();
            //读取本地用户信息，账号密码及是否自动登陆等
            string logininfo = GetLoginFileContent();
            string[] infos = logininfo.Split('|');
            if (infos[0] == "true" || infos[0].Equals("true"))
            {
                checkBox1.Checked = true;
                skinTextBox1.SkinTxt.Text = infos[2];
                skinTextBox2.SkinTxt.Text = infos[3];
            }

            if (infos[1] == "true" || infos[1].Equals("true"))
            {
                checkBox2.Checked = true;
                autologin = validateAndLogin();
            }
                
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
            //Boolean input_error = judgeInputErrorAsync();
            //if (input_error)
            //{
            //    if (_loginErrorTimes < MAXLOGININPUTTIMES)
            //    {
            //        linkLabel3.Text = "密码或账号错误，您还有" + (MAXLOGININPUTTIMES - (_loginErrorTimes)) + "次机会";
            //        linkLabel3.Visible = true;
            //        linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
            //        _loginErrorTimes++;
            //    }
            //    else
            //    {
            //        linkLabel3.Text = "您输入错了" + MAXLOGININPUTTIMES + "次，请找回密码";
            //        Console.WriteLine(linkLabel3.Width);
            //        linkLabel3.Visible = true;
            //        linkLabel3.LinkVisited = true;
            //        Console.WriteLine(linkLabel3.Text);
            //        Console.WriteLine(linkLabel3.Text.IndexOf("请"));
            //        int infolen = linkLabel3.Text.IndexOf("码") - linkLabel3.Text.IndexOf("请");
            //        linkLabel3.LinkArea = new LinkArea(linkLabel3.Text.IndexOf("请")+1, infolen);
            //        button1.Enabled = false;
            //    }
            //}
            //else
            //{
            //    if (checkBox1.Checked || checkBox2.Checked)
            //    {
            //        //记住密码，checkBox1 checked，将账号密码保存到本地
            //        //自动登陆，checkBox2 checked，自动登陆标记为至true
            //        string logininfo = "";
            //        if (checkBox1.Checked)
            //            logininfo += "true";
            //        else
            //            logininfo += "false";
            //        if (checkBox2.Checked)
            //            logininfo += "|true";
            //        else
            //            logininfo += "|false";
            //        if (checkBox1.Checked)
            //            logininfo += "|" + skinTextBox1.SkinTxt.Text + "|" + skinTextBox2.SkinTxt.Text;
            //        TouchUserLoginFile(logininfo);
            //    }
            //    ShadowsocksController controller = new ShadowsocksController();

            //    MenuViewController viewController = new MenuViewController(controller);

            //    controller.Start();

            //    this.Hide();

            //    FunctionForm funcForm = new FunctionForm();
            //    funcForm.Show();
            //}
            if (validateAndLogin())
            {
                this.Hide();
                FunctionForm funcForm = new FunctionForm();
                funcForm.Show();
            }
        }

        private Boolean validateAndLogin()
        {
            Boolean autologin = false;
            Boolean input_error = judgeInputErrorAsync();
            if (input_error)
            {
                if (_loginErrorTimes < MAXLOGININPUTTIMES)
                {
                    linkLabel3.Text = "密码或账号错误，您还有" + (MAXLOGININPUTTIMES - (_loginErrorTimes)) + "次机会";
                    linkLabel3.Visible = true;
                    linkLabel3.LinkBehavior = LinkBehavior.NeverUnderline;
                    _loginErrorTimes++;
                }
                else
                {
                    linkLabel3.Text = "您输入错了" + MAXLOGININPUTTIMES + "次，请找回密码";
                    Console.WriteLine(linkLabel3.Width);
                    linkLabel3.Visible = true;
                    linkLabel3.LinkVisited = true;
                    Console.WriteLine(linkLabel3.Text);
                    Console.WriteLine(linkLabel3.Text.IndexOf("请"));
                    int infolen = linkLabel3.Text.IndexOf("码") - linkLabel3.Text.IndexOf("请");
                    linkLabel3.LinkArea = new LinkArea(linkLabel3.Text.IndexOf("请") + 1, infolen);
                    button1.Enabled = false;
                }
            }
            else
            {
                if (checkBox1.Checked || checkBox2.Checked)
                {
                    //记住密码，checkBox1 checked，将账号密码保存到本地
                    //自动登陆，checkBox2 checked，自动登陆标记为至true
                    string logininfo = "";
                    if (checkBox1.Checked)
                        logininfo += "true";
                    else
                        logininfo += "false";
                    if (checkBox2.Checked)
                        logininfo += "|true";
                    else
                        logininfo += "|false";
                    if (checkBox1.Checked)
                        logininfo += "|" + skinTextBox1.SkinTxt.Text + "|" + skinTextBox2.SkinTxt.Text;
                    TouchUserLoginFile(logininfo);
                }
                ShadowsocksController controller = new ShadowsocksController();

                MenuViewController viewController = new MenuViewController(controller);

                controller.Start();

                //this.Hide();

                //FunctionForm funcForm = new FunctionForm();
                //funcForm.Show();
                autologin = true;
            }

            return autologin;
        }

        //判断账号或者密码输入是否有误
        //有误返回true
        private Boolean judgeInputErrorAsync()
        {

            
            String name = skinTextBox1.SkinTxt.Text;
            String password = skinTextBox2.SkinTxt.Text;

            if (null == name || "".Equals(name) || null == password || "".Equals(password))
            {
                return true;
            }

            //string uri = "http://www.baidu.com/";
            //HttpClient client = new HttpClient();
            //HttpResponseMessage res = client.GetAsync(new Uri(uri)).Result;
            //String result = res.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(result);
            //MessageBox.Show(result);

            //System.Threading.Tasks.Task<string> body = await client.GetStringAsync(uri);

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
            linkLabel3.Visible = false;
            linkLabel3.LinkArea = new LinkArea();
            button1.Enabled = true;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private string TouchUserLoginFile(String logininfo)
        {
            //if (File.Exists(USER_LOGIN_FILE))
            //{
            //    return USER_LOGIN_FILE;
            //}
            //else
            //{
                File.WriteAllText(USER_LOGIN_FILE, logininfo);
                return USER_LOGIN_FILE;
            //}
        }

        private string GetLoginFileContent()
        {
            if (File.Exists(USER_LOGIN_FILE))
            {
                return File.ReadAllText(USER_LOGIN_FILE, Encoding.UTF8);
            }
            else
            {
                return "false|false";
            }
        }

    }
}
