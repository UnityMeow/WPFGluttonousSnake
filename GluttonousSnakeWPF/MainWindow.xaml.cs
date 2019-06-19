using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GluttonousSnakeWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //计时器
        public static Timer timer;
        //场景管理器
        public static CSceneManager SceneManager = new CSceneManager();
        public static bool On = true;
        public MainWindow()
        {
            InitializeComponent();
            //初始化主面板画布
            CUtil.mainCanvas = _Canvas;
            //初始化计时器
            timer = new Timer();
            //设置定时器调用时间间隔
            timer.Interval = 16;
            timer.Enabled = true;
            //委托
            timer.Elapsed += Updata;
            //加载场景
            SceneManager.LoadScene("开始场景", new CGameStartScene());
            SceneManager.LoadScene("选择场景", new CGameChooseScene());
            SceneManager.LoadScene("榜单场景", new CGameRankScene());
            SceneManager.LoadScene("游戏场景", new CGameRunScene());
            SceneManager.LoadScene("结束场景", new CGameOverScene());
            //设置初次运行场景
            SceneManager.SetInitScene("开始场景");
            
        }
        public void Updata(object sender, ElapsedEventArgs e)
        {
            if (On)
            {
                timer.Interval = 16;
            }
            else
            {
                timer.Interval = 500;
            }
            this.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        //场景显示状态
                        if (SceneManager.m_CurScene.GetState() == 1)
                            SceneManager.m_CurScene.Show();
                        //当前场景为空 return
                        if (SceneManager.m_CurScene == null)
                            return;
                        else
                            //当前场景运行
                            SceneManager.m_CurScene.Run();  
                        //是否需要切换场景
                        if (SceneManager.m_NextScene != null)
                        {
                            //执行当前场景结束函数
                            SceneManager.m_CurScene.End();
                            //当前场景显示状态
                            if (SceneManager.m_CurScene.GetState() == -1)
                                SceneManager.m_CurScene.Hide();
                            //执行切换场景初始化
                            SceneManager.m_NextScene.Init();
                            //当前场景改为要切换的场景
                            SceneManager.m_CurScene = SceneManager.m_NextScene;
                            //重置切换场景为空
                            SceneManager.m_NextScene = null;
                        }
                    }
                )
            );
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (SceneManager.m_CurScene is CGameRunScene)
            {
                (SceneManager.m_CurScene as CGameRunScene).CheckKey(e.Key);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (SceneManager.m_CurScene is CGameRunScene)
            {
                if (On)
                    (SceneManager.m_CurScene as CGameRunScene).KeyUp(e.Key);
            }
        }
    }
}
