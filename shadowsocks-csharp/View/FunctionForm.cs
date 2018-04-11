using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Shadowsocks.View
{
    public partial class FunctionForm : BaseForm
    {
        private Thread _ramThread;
        int flag = 0;
        //https://gitee.com/lidechenyan/images/raw/master/Apache.PNG
        //https://gitee.com/lidechenyan/images/raw/master/mysql.PNG
        //https://gitee.com/lidechenyan/images/raw/master/php.PNG
        String[] bannerUrls = { "https://gitee.com/lidechenyan/images/raw/master/Apache.PNG",
                    "https://gitee.com/lidechenyan/images/raw/master/mysql.PNG",
                    "https://gitee.com/lidechenyan/images/raw/master/php.PNG" };

        public FunctionForm()
        {
            InitializeComponent();
            StartBannerShow();
        }

        private void Login2Form_Load(object sender, EventArgs e)
        {

        }

        private void StartBannerShow()
        {
            _ramThread = new Thread(new ThreadStart(BannerShow));
            _ramThread.IsBackground = true;
            _ramThread.Start();
        }

        private void BannerShow()
        {
            
            while (true)
            { 
                skinPictureBox3.ImageLocation = bannerUrls[flag];
                Thread.Sleep(6 * 1000);
                flag++;
                if (flag == bannerUrls.Length) flag = 0;
            }
        }

        private void skinPictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(bannerUrls[flag]);
        }
    }
}
