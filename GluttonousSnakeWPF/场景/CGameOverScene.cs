using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GluttonousSnakeWPF
{
    class CGameOverScene : CScene
    {
        Label lable;
        CCustomImage again;
        CCustomImage home;
        string m_path;
        byte[] m_rdata;
        //初始化场景
        public override void Init()
        {
            lable = new Label();
            lable.Content = "Game Over";
            lable.Width = 250;
            lable.Height = 50;
            lable.FontSize = 35;
            lable.FontFamily = new FontFamily("Consolas");
            Canvas.SetLeft(lable,280);
            Canvas.SetTop(lable, 100);
            again = new CCustomImage(Properties.Resources.again, 130, 100);
            again.SetPos(550,250);
            home = new CCustomImage(Properties.Resources.home, 130, 100);
            home.SetPos(380, 245);
            m_SceneState = 1;
            again.MouseDown += againRun;
            home.MouseDown += homeRun;

            m_path = "..\\..\\rank.data";
            m_rdata = new byte[8];
            //安全检测
            if (!File.Exists(m_path))
                return;
            //打开文件
            FileStream stream = new FileStream(m_path, FileMode.OpenOrCreate);
            //移动指针位置
            stream.Seek(0, SeekOrigin.Begin);
            //读取文件信息
            stream.Read(m_rdata, 0, m_rdata.Length);
            //关闭文件
            stream.Close();
            int Score = CGameRunScene.fen / 5;
            if ((byte)Score >= m_rdata[m_rdata.Length - 1])
            {
                m_rdata[m_rdata.Length - 1] = (byte)Score;
            }
            for (int i = m_rdata.Length - 1; i > 0;)
            {

                if (m_rdata[i] > m_rdata[i - 1])
                {
                    byte bTmp = m_rdata[i];
                    m_rdata[i] = m_rdata[i - 1];
                    m_rdata[i - 1] = bTmp;
                }
                i--;
            }
            FileStream stream1 = new FileStream(m_path, FileMode.OpenOrCreate);
            //移动指针位置
            stream1.Seek(0, SeekOrigin.Begin);
            //读取文件信息
            stream1.Write(m_rdata, 0, 8);
            //关闭文件
            stream1.Close();
        }
        //间隔固定时间 执行一次该函数 
        public void againRun(object sender, MouseButtonEventArgs e)
        {
            again.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.SceneManager.SetNextScene("游戏场景");
                    }
                )
            );

        }
        public void homeRun(object sender, MouseButtonEventArgs e)
        {
            again.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.SceneManager.SetNextScene("开始场景");
                    }
                )
            );

        }
        public override void Run()
        {
            m_SceneState = 0;
        }
        public override void End()
        { }
        //显示Image
        public override void Show()
        {
            CUtil.AddControlInCanvas(lable);
            CUtil.AddControlInCanvas(again);
            CUtil.AddControlInCanvas(home);
        }
        //隐藏Image
        public override void Hide()
        {
            CUtil.Clear();
            
        }
    }
}
