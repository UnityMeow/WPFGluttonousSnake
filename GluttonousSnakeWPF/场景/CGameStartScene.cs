using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GluttonousSnakeWPF
{
    class CGameStartScene : CScene
    {
        //标题
        CCustomImage title;
        //开始
        CCustomImage begin;
        //排行
        CCustomImage order;
        //结束
        CCustomImage gameover;
        //背景
        CCustomImage background;
        //初始化场景
        public override void Init()
        {
            //初始化Image
            title = new CCustomImage(Properties.Resources.title, 424 / 2, 124 / 2);
            begin = new CCustomImage(Properties.Resources.begingame, 337 / 3, 119 / 3);
            order = new CCustomImage(Properties.Resources.order, 337 / 3, 119 / 3);
            background = new CCustomImage(Properties.Resources.initpintu1, 795, 415);
            gameover = new CCustomImage(Properties.Resources.gameover, 337 / 3, 119 / 3);
            title.SetPos(280,60);
            begin.SetPos(330,180);
            order.SetPos(330,240);
            gameover.SetPos(330,300);
            background.SetPos(-5, 0);
            //设置场景状态
            m_SceneState = 1;
            begin.MouseDown += BeginRun;
            order.MouseDown += OrderRun;
            gameover.MouseDown += overRun;
        }
        //间隔固定时间 执行一次该函数 
        public void BeginRun(object sender, MouseButtonEventArgs e)
        {
            begin.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.SceneManager.SetNextScene("选择场景");
                    }
                )
            );
        }
        //间隔固定时间 执行一次该函数 
        public void overRun(object sender, MouseButtonEventArgs e)
        {
            MainWindow.timer.Enabled = false;
            Application.Current.Shutdown();
        }
        //间隔固定时间 执行一次该函数 
        public void OrderRun(object sender, MouseButtonEventArgs e)
        {
            order.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        MainWindow.SceneManager.SetNextScene("榜单场景");
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
            CUtil.AddControlInCanvas(title);
            CUtil.AddControlInCanvas(begin);
            CUtil.AddControlInCanvas(order);
            CUtil.AddControlInCanvas(gameover);

        }
        //隐藏Image
        public override void Hide()
        {
            CUtil.Clear();
        }
    }
}
