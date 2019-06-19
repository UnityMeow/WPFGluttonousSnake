using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GluttonousSnakeWPF
{
    class CGameChooseScene : CScene
    {
        //转圈圈
        CCustomImage pattern1;
        //吃吃吃
        CCustomImage pattern2;
        CCustomImage presen;
        //返回
        CCustomImage back;
        CCustomImage go;
        CCustomImage xuanwo;
        //背景
        CCustomImage background;
        int b;
        //初始化场景
        public override void Init()
        {
            b = 0;
            //初始化Image
            xuanwo = new CCustomImage(Properties.Resources.xuanwo, 100, 70);
            pattern1 = new CCustomImage(Properties.Resources.zhuan, 150, 150);
            pattern2 = new CCustomImage(Properties.Resources.chi, 150, 150);
            presen = new CCustomImage(Properties.Resources.wujin, 400, 100);
            back = new CCustomImage(Properties.Resources.mback, 170, 100);
            go = new CCustomImage(Properties.Resources.Go, 100, 100);
            background = new CCustomImage(Properties.Resources.initpintu1, 795, 415);
            pattern1.SetPos(150, 80);
            pattern2.SetPos(400, 80);
            presen.SetPos(150, 250);
            xuanwo.SetPos(250, 50);
            go.SetPos(520, 260);
            back.SetPos(-5, 0);
            background.SetPos(-5, 0);
            //设置场景状态
            m_SceneState = 1;
            pattern1.MouseDown += pattern1Run;
            pattern2.MouseDown += pattern2Run;
            go.MouseDown += goRun;
            back.MouseDown += backRun;
        }
        //间隔固定时间 执行一次该函数 
        public void pattern1Run(object sender, MouseButtonEventArgs e)
        {
            pattern1.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.On = true;
                        b = 1;
                    }
                )
            );
        }
        //间隔固定时间 执行一次该函数 
        public void pattern2Run(object sender, MouseButtonEventArgs e)
        {
            pattern2.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.On = false;
                        b = 2;
                    }
                )
            );
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
        public void goRun(object sender, MouseButtonEventArgs e)
        {
            go.Dispatcher.Invoke
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
        public override void Run()
        {
            m_SceneState = 0;
            if (b == 1)
            {
                presen.SetPos(150, 250);
                presen.SetPic(Properties.Resources.wujin);
                xuanwo.SetPos(500, 50);
                go.SetPos(520, 260);
            }
            else if (b == 2)
            {
                presen.SetPos(150, 250);
                presen.SetPic(Properties.Resources.jingdian);
                xuanwo.SetPos(250, 50);
                go.SetPos(520, 260);
            }
            else
            {
                presen.SetPos(999, 999);
                go.SetPos(999, 999);
            }

            //MainWindow.SceneManager.SetNextScene("开始场景");
        }
        public override void End()
        {
            m_SceneState = -1;
        }
        //显示Image
        public override void Show()
        {
            CUtil.AddControlInCanvas(background);
            CUtil.AddControlInCanvas(pattern1);
            CUtil.AddControlInCanvas(pattern2);
            CUtil.AddControlInCanvas(back);
            CUtil.AddControlInCanvas(presen);
            CUtil.AddControlInCanvas(go);
            CUtil.AddControlInCanvas(xuanwo);
        }
        //隐藏Image
        public override void Hide()
        {
            CUtil.Clear();
        }
    }
}
