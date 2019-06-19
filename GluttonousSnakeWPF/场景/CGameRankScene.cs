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
    class CGameRankScene : CScene
    {
        CCustomImage rank;
        CCustomImage background;
        CCustomImage back;
        Label R;
        List<Label> m_LableList;
        public override void Init()
        {
            string m_path;
            byte[] m_rdata;
            m_rdata = new byte[8];
            m_path = "..\\..\\rank.data";
            //安全检测
            if (!File.Exists(m_path))
                return;
            //打开文件
            FileStream stream = new FileStream(m_path, FileMode.Open);
            //移动指针位置
            stream.Seek(0, SeekOrigin.Begin);
            //读取文件信息
            stream.Read(m_rdata, 0, m_rdata.Length);
            //关闭文件
            stream.Close();

            
            m_LableList = new List<Label>();
            for (int i = 0; i < 8; i++)
            {
                R = new Label();
                R.Width = 400;
                R.Height = 50;
                R.FontSize = 22;
                R.FontFamily = new FontFamily("Consolas");
                Canvas.SetLeft(R, 250);
                Canvas.SetTop(R, 90 + i * 35);
                R.Content = "第 "+ (i + 1).ToString() + " 名：\t" + (((int)m_rdata[i]) * 5).ToString() + " 分";
                m_LableList.Add(R);
            }
            back = new CCustomImage(Properties.Resources.kback, 170, 100);
            background = new CCustomImage(Properties.Resources.initpintu1, 795, 415);
            rank = new CCustomImage(Properties.Resources.rank, 200, 50);
            background.SetPos(-5, 0);
            rank.SetPos(270, 20);
            back.SetPos(632, 312);
            back.MouseDown += backRun;
            m_SceneState = 1;
        }
        //间隔固定时间 执行一次该函数 
        public void backRun(object sender, MouseButtonEventArgs e)
        {
            back.Dispatcher.Invoke
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
        {
            m_SceneState = -1;
        }
        //显示Image
        public override void Show()
        {
            CUtil.AddControlInCanvas(background);
            CUtil.AddControlInCanvas(rank);
            CUtil.AddControlInCanvas(back);
            for (int i = 0; i < m_LableList.Count; i++)
            {
                CUtil.AddControlInCanvas(m_LableList[i]);
            }
        }
        //隐藏Image
        public override void Hide()
        {
            CUtil.Clear();
        }
    }
}
