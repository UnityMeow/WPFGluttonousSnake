using System;
using System.Collections;
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
    class CGameObjList<T>
    {
        T m_Tmp = default(T);
        //物体管理
        Dictionary<int, T> m_ObjList;
        public CGameObjList()
        {
            m_ObjList = new Dictionary<int, T>();
        }
        //加载物体
        public void LoadObj(int id, T obj)
        {
            //安全检测
            if (m_ObjList.ContainsKey(id))
                return;
            //将所需要加载的物体存入链表
            m_ObjList.Add(id, obj);
        }
        //修改物体
        public void SetObj(int id, T obj)
        {
            //安全检测
            if (!m_ObjList.ContainsKey(id))
                return;
            m_ObjList[id] = obj;
        }
        //删除物体
        public void EraseObj(int id)
        {
            //安全检测
            if (!m_ObjList.ContainsKey(id))
                return;
            //移除物体
            m_ObjList.Remove(id);
        }
        //获取物体
        public T GetObject(int id)
        {
            //安全检测
            if (!m_ObjList.ContainsKey(id))
                return m_Tmp;
            return m_ObjList[id];
        }
    }
    class CHero
    {
        //身体
        struct _BODY
        {
            public double x;
            public double y;
            public CCustomImage body;
        }
        //蛇头位置
        struct _HEADPOS
        {
            public double x;
            public double y;
        }
        //身体长度
        int m_BodyLen;
        //身体链表
        CGameObjList<_BODY> m_BodyList;
        //蛇头位置记录
        List<_HEADPOS> m_HeadPosList;
        //蛇头位置记录长度
        int m_HeadPosLen;
        //英雄xywh
        double m_x;
        double m_y;
        double m_bx;
        double m_by;
        //方向向量
        double m_xx;
        double m_yy;
        int m_w;
        int m_h;
        //英雄角度
        double m_rotate;
        //英雄r角度
        double m_Rotate;
        //英雄移动速度
        double m_speed;
        //英雄旋转速度
        int m_rspeed;
        //英雄dir
        int m_dir;
        public CHero(double x, double y, int w, int h,double speed = 5,int rspeed = 5)
        {
            m_dir = 0;
            m_rotate = 0;
            m_speed = speed;
            m_x = x;
            m_y = y;
            m_w = w;
            m_h = h;
            m_xx = 0;
            m_yy = 1;
            m_BodyList = new CGameObjList<_BODY>();
            m_HeadPosList = new List<_HEADPOS>();
            m_BodyLen = 0;
            m_rspeed = rspeed;
            m_Rotate = 0;
            m_bx = 0;
            m_by = 0;
            m_HeadPosLen = 0;
        }
        //添加英雄身体
        public void PushBody()
        {
            _BODY tmp = new _BODY();
            tmp.x = (m_x - m_w / 2) + m_w;
            tmp.y =  m_y;
            tmp.body = new CCustomImage(Properties.Resources.body, m_w, m_h);
            tmp.body.SetPos(tmp.x, tmp.y);
            m_BodyList.LoadObj(m_BodyLen, tmp);
            m_BodyLen++;
        }
        //记录蛇头位置
        public void PushHeadPos()
        {
            _HEADPOS tmp = new _HEADPOS();
            tmp.x = m_x;
            tmp.y = m_y;
            m_HeadPosList.Add(tmp);
            m_HeadPosLen++;
        }
        public void EraseHeadPos()
        {
            m_HeadPosList.RemoveAt(0);
            m_HeadPosLen--;
        }
        public int GetHeadPosLen()
        {
            return m_HeadPosLen;
        }
        //修改身体x
        private void SetBodyX(int id, double x)
        {
            //安全检测
            if (id > m_BodyLen)
                return;
            _BODY tmp = new _BODY();
            tmp.x = x;
            tmp.y = m_BodyList.GetObject(id).y;
            tmp.body = m_BodyList.GetObject(id).body;
            m_BodyList.SetObj(id, tmp);
        }
        //修改身体y
        private void SetBodyY(int id, double y)
        {
            //安全检测
            if (id > m_BodyLen)
                return;
            _BODY tmp = new _BODY();
            tmp.y = y;
            tmp.x = m_BodyList.GetObject(id).x;
            tmp.body = m_BodyList.GetObject(id).body;
            m_BodyList.SetObj(id, tmp);
        }
        //获取身体x
        public double GetBodyX(int id)
        {
            //安全检测
            if (id > m_BodyLen)
                return -1;
            return m_BodyList.GetObject(id).x;
        }
        //获取身体y
        public double GetBodyY(int id)
        {
            //安全检测
            if (id > m_BodyLen)
                return -1;
            return m_BodyList.GetObject(id).y;
        }
        //获取身体长度
        public int GetBodyLen()
        {
            return m_BodyLen;
        }
        //改变身体4方向
        public void ChangeBody4()
        {
            //修改蛇身位置
            for (int i = m_BodyLen - 1; i >= 0; --i)
            {
                SetBodyX(i, m_BodyList.GetObject(i - 1).x);
                SetBodyY(i, m_BodyList.GetObject(i - 1).y);
            }
            if (m_BodyLen != 0)
            {
                switch (m_dir)
                {
                    case 1:
                        {
                            SetBodyX(0, m_x);
                            SetBodyY(0, m_y + m_h);
                        };
                        break;
                    case 2:
                        {
                            SetBodyX(0, m_x);
                            SetBodyY(0, m_y - m_h);
                        };
                        break;
                    case 3:
                        {
                            SetBodyX(0, m_x + m_w);
                            SetBodyY(0, m_y);
                        };
                        break;
                    case 4:
                        {
                            SetBodyX(0, m_x - m_w);
                            SetBodyY(0, m_y);
                        };
                        break;
                }
            }
            for (int i = 0; i < m_BodyLen; ++i)
            {
                double x = m_BodyList.GetObject(i).x;
                double y = m_BodyList.GetObject(i).y;
                m_BodyList.GetObject(i).body.SetPos(x,y);
            }
        }
        //改变身体
        public void ChangeBody()
        {
            int tmp;
            
            for (int i = 0; i < m_BodyLen; i++)
            {
                tmp = m_HeadPosLen - 1 - ((i + 1) * (10 - (int)m_speed - 1));
                tmp = tmp >= 0 ? tmp : 0;
                SetBodyX(i, m_HeadPosList[tmp].x);
                SetBodyY(i, m_HeadPosList[tmp].y);
            }
            for (int i = 0; i < m_BodyLen; ++i)
            {
                double x = m_BodyList.GetObject(i).x;
                double y = m_BodyList.GetObject(i).y;
                m_BodyList.GetObject(i).body.SetPos(x, y);
            }
        }
        //显示身体
        public void ShowBody()
        {
            if (m_BodyLen < 1)
                return;
            CUtil.AddControlInCanvas(m_BodyList.GetObject(m_BodyLen - 1).body);
        }
        //更新移动方向
        public void UpdateMoveDir(double angle)
        {
            UpdateRotate(angle);
            angle = angle / (180 / Math.PI);
            //根据变化角度 计算旋转后的新向量
            m_bx = m_xx;
            m_by = m_yy;
            double x = m_xx * Math.Cos(angle) + m_yy * Math.Sin(angle);
            double y = m_yy * Math.Cos(angle) - m_xx * Math.Sin(angle);
            m_xx = x;
            m_yy = y;
        }
        //更新当前位置
        public void UpdatePos()
        {
            //计算位置变化值  向量各分量*移动速度 得到各方向分速度
            m_x += m_xx * m_speed;
            m_y += m_yy * m_speed;
        }
        //更新角度
        public void UpdateRotate(double rotate)
        {
            m_Rotate += -rotate;
        }
        //英雄移动
        public void Move(bool key)
        {
            if (key)
            {
                UpdateMoveDir(m_rspeed);

            }
            UpdatePos();
        }
        //英雄4方向移动
        public void Move4()
        {
            if (m_dir > 4 || m_dir < 0)
                return;
            switch (m_dir)
            {
                case 1: m_y -= m_speed; break;
                case 2: m_y += m_speed; break;
                case 3: m_x -= m_speed; break;
                case 4: m_x += m_speed; break;
            }
        }
        //设置英雄x
        public void SetX(double x)
        {
            m_x = x;
        }
        //设置英雄y
        public void SetY(double y)
        {
            m_y = y;
        }
        //设置英雄角度
        public void SetRotate(int rotate)
        {
            m_rotate = rotate;
        }
        //获取英雄x
        public double GetX()
        {
            return m_x;
        }
        //获取英雄y
        public double GetY()
        {
            return m_y;
        }
        //获取英雄角度
        public double GetRotate()
        {
            return m_Rotate;
        }
        //获取英雄移动速度
        public double GetSpeed()
        {
            return m_speed;
        }
        //获取英雄旋转速度
        public int GetRSpeed()
        {
            return m_rspeed;
        }
        public void SetRSpeed(int r)
        {
            m_rspeed = r;
        }
        public void SetSpeed(int speed)
        {
            m_speed = speed;
        }
        //获取英雄方向
        public int GetDir()
        {
            return m_dir;
        }
        //获取英雄方向
        public void SetDir(int dir)
        {
            m_dir = dir;
        }
        public int GetW()
        {
            return m_w;
        }
        public int GetH()
        {
            return m_h;
        }

    }
    class CFood
    {
        int m_x;
        int m_y;
        int m_w;
        int m_h;

        public CFood(int x, int y, int w, int h)
        {
            m_x = x;
            m_y = y;
            m_w = w;
            m_h = h;
        }
        public void Random()
        {
            Random r = new Random();
            m_x = r.Next(50, 750);
            m_y = r.Next(50, 370);
        }
        public int GetX()
        {
            return m_x;
        }
        public int GetY()
        {
            return m_y;
        }
        public int GetW()
        {
            return m_w;
        }
        public int GetH()
        {
            return m_h;
        }
    }
    
    class CGameRunScene : CScene
    {
        public static int fen;
        CCustomImage pause;
        CCustomImage runback;
        //背景
        CCustomImage background;
        //英雄
        CCustomImage snake;
        //食物
        CCustomImage food;
        //蛇身
        CFood Food;
        //得分
        Label Score;
        
        bool b;
        CHero Snake;
        //按键状态
        bool m_key;
        public CGameRunScene()
        {
            m_key = false;
            b = true;
        }
        private bool Collide(int x, int y, int rx, int ry, int rw, int rh)
        {
            if (x >= rx && x <= rx + rw &&
                    y >= ry && y <= ry + rh)
                return true;
            else
                return false;
        }
        private bool Collide(int x, int y,int w,int h, int rx, int ry, int rw, int rh)
        {
            if (x + w < rx || x > rx + rw ||
                    y > ry + rh || y + h < ry)
                return false;
            else
                return true;
        }
        //初始化场景
        public override void Init()
        {
            b = true;
            m_key = false;
            fen = 0;
            Score = new Label();
            Score.Content = "总分：  0";
            Score.Width = 200;
            Score.Height = 50;
            Score.FontSize = 22;
            Score.FontFamily = new FontFamily("Consolas");
            Canvas.SetLeft(Score, 600);
            Canvas.SetTop(Score, 20);
            Snake = new CHero(50, 50,30, 30, 2);
            Food = new CFood(350, 200, 15, 15);
            if (MainWindow.On)
            {
                Snake.SetSpeed(2);
            }
            else
            {
                Snake.SetSpeed(30);
            }

            background = new CCustomImage(Properties.Resources.initpintu, 795, 415);
            snake = new CCustomImage(Properties.Resources.head, Snake.GetW(), Snake.GetH());
            food = new CCustomImage(Properties.Resources.brick, Food.GetW(), Food.GetH());
            pause = new CCustomImage(Properties.Resources.pause1, 30, 30);
            runback = new CCustomImage(Properties.Resources.runback, 30, 30);

            pause.SetPos(630, 60);
            runback.SetPos(680, 60);
            background.SetPos(-5, 0);
            food.SetPos(Food.GetX(), Food.GetY());
            snake.SetPos(Snake.GetX(), Snake.GetY());
            snake.SetAngle(0);
            pause.MouseDown += pauseRun;
            runback.MouseDown += runbackRun;
            m_SceneState = 1;
        }
        public override void Run()
        {
            m_SceneState = 0;
            if (b)
            {
                if (MainWindow.On)
                {
                    Snake.Move(m_key);
                    if (Collide((int)Snake.GetX(), (int)Snake.GetY(), Snake.GetW(), Snake.GetH(), Food.GetX(), Food.GetY(), Food.GetW(), Food.GetH()))
                    {
                        Snake.PushBody();
                        Snake.ShowBody();
                        Food.Random();
                        fen += 5;
                    }
                    Snake.PushHeadPos();
                    if (Snake.GetBodyLen() * 10 + 20 >= Snake.GetHeadPosLen())
                    {
                        Snake.PushHeadPos();
                    }
                    else if (Snake.GetBodyLen() * 10 + 20 < Snake.GetHeadPosLen())
                    {
                        Snake.EraseHeadPos();
                    }
                    Snake.ChangeBody();
                    Score.Content = "总分：  " + fen.ToString();
                    food.SetPos(Food.GetX(), Food.GetY());
                    snake.SetPos(Snake.GetX(), Snake.GetY());
                    snake.SetAngle((int)Snake.GetRotate());

                    if (Snake.GetX() > 750
                            || Snake.GetY() > 375
                            || Snake.GetX() < 15
                            || Snake.GetY() < 15)
                        MainWindow.SceneManager.SetNextScene("结束场景");
                    if(fen < 10)
                        Snake.SetSpeed(2);
                    else if (fen > 15 && fen < 40)
                        Snake.SetSpeed(3);
                    else if (fen > 40 && fen < 60)
                        Snake.SetSpeed(4);
                    else if (fen > 60 && fen < 80)
                        Snake.SetSpeed(5);
                    else if (fen > 80 && fen < 150)
                        Snake.SetSpeed(6);
                    else if (fen > 150 && fen < 190)
                        Snake.SetSpeed(7);
                    else if (fen >= 195)
                        Snake.SetSpeed(8);

                }
                else
                {
                    Snake.Move4();
                    if (Collide((int)Snake.GetX(), (int)Snake.GetY(), Snake.GetW(), Snake.GetH(), Food.GetX(), Food.GetY(), Food.GetW(), Food.GetH()))
                    {
                        Snake.PushBody();
                        Snake.ShowBody();
                        Food.Random();
                        fen += 5;
                    }
                    Score.Content = "总分：  " + fen.ToString();
                    Snake.ChangeBody4();
                    food.SetPos(Food.GetX(), Food.GetY());
                    snake.SetPos(Snake.GetX(), Snake.GetY());

                    if (Snake.GetX() > 750
                            || Snake.GetY() > 375
                            || Snake.GetX() < 15
                            || Snake.GetY() < 15)
                        MainWindow.SceneManager.SetNextScene("结束场景");
                    for (int i = 0; i < Snake.GetBodyLen(); ++i)
                    {
                        if (Snake.GetX() == Snake.GetBodyX(i) && Snake.GetY() == Snake.GetBodyY(i))
                            MainWindow.SceneManager.SetNextScene("结束场景");
                    }
                }
            }
        }
        public override void End()
        {
            CUtil.RemoveInCanvas(pause);
            CUtil.RemoveInCanvas(runback);
            Canvas.SetLeft(Score, 300);
            Canvas.SetTop(Score, 200);
            

        }
        //显示Image
        public override void Show()
        {
            CUtil.AddControlInCanvas(background);
            CUtil.AddControlInCanvas(food);
            CUtil.AddControlInCanvas(snake);
            CUtil.AddControlInCanvas(Score);
            CUtil.AddControlInCanvas(pause);
            CUtil.AddControlInCanvas(runback);
        }
        //隐藏Image
        public override void Hide()
        {
            CUtil.Clear();
        }
        //当前放开
        public void KeyUp(Key k)
        {
            switch (k)
            {
                case Key.Left:
                    {    
                        m_key = false;
                    }
                    break;
                case Key.Right:
                    {        
                        m_key = false;
                    }
                    break;
            }
        }
        //当前按下
        public void CheckKey(Key k)
        {
            if (MainWindow.On)
            {
                switch (k)
                {
                    case Key.Left:
                        {
                            Snake.SetRSpeed(5);
                            m_key = true;

                        }
                        break;
                    case Key.Right:
                        {
                            Snake.SetRSpeed(-5);
                            m_key = true;
                        }
                        break;
                }
            }
            else
            {
                switch (k)
                {
                    case Key.Up:
                        {
                            if (Snake.GetDir() != 2)
                            {
                                Snake.SetDir(1);
                                snake.SetAngle(180);
                            }
                        }
                        break;
                    case Key.Down:
                        {
                            if (Snake.GetDir() != 1)
                            {
                                Snake.SetDir(2);
                                snake.SetAngle(0);
                            }
                        }
                        break;
                    case Key.Left:
                        {
                            if (Snake.GetDir() != 4)
                            {
                                Snake.SetDir(3);
                                snake.SetAngle(90);
                            }
                        }
                        break;
                    case Key.Right:
                        {
                            if (Snake.GetDir() != 3)
                            {
                                Snake.SetDir(4);
                                snake.SetAngle(270);
                            }
                        }
                        break;
                }
            }
        }
        //间隔固定时间 执行一次该函数 
        public void pauseRun(object sender, MouseButtonEventArgs e)
        {
            pause.Dispatcher.Invoke
            (
                new Action
                (
                    delegate
                    {
                        b = !b;
                        if(b)
                            pause.SetPic(Properties.Resources.pause1);
                        else
                            pause.SetPic(Properties.Resources.pause);

                    }
                )
            );

        }
        public void runbackRun(object sender, MouseButtonEventArgs e)
        {
            runback.Dispatcher.Invoke
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
    }
}
