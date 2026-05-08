using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 多媒體播放器
{
    public partial class frmMediaPlayer : Form
    {
        public frmMediaPlayer()
        {
            InitializeComponent();
        }

        private void frmMediaPlayer_Load(object sender, EventArgs e)
        {
            wmpVideo.uiMode = "none";
            // 設定播放器的初始音量與 TrackBar 的預設值同步
            wmpVideo.settings.volume = trackBarVolume.Value;

            // 確保表單可以接收按鍵事件
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmMediaPlayer_KeyDown);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WMV files (*.wmv)|*.wmv|MP4 files (*.mp4)|*.mp4|AVI files (*.avi)|*.avi|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                wmpVideo.URL = ofd.FileName;
                wmpVideo.Ctlcontrols.stop();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.stop();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            // 將 Windows Media Player 的音量設定為 TrackBar 目前的值
            wmpVideo.settings.volume = trackBarVolume.Value;
        }

        private void frmMediaPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            double jumpSeconds = 5.0; // 設定每次快轉/倒退的秒數

            if (e.KeyCode == Keys.Left)
            {
                // 倒退 5 秒 (保護機制：避免小於 0 秒導致錯誤)
                if (wmpVideo.Ctlcontrols.currentPosition > jumpSeconds)
                    wmpVideo.Ctlcontrols.currentPosition -= jumpSeconds;
                else
                    wmpVideo.Ctlcontrols.currentPosition = 0;

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                // 前進 5 秒
                wmpVideo.Ctlcontrols.currentPosition += jumpSeconds;
                e.Handled = true;
            }
        }
    }
}
