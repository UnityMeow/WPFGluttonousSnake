using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluttonousSnakeWPF
{
    public class CScene
    {
        protected int m_SceneState;
        public CScene()
        {
            m_SceneState = 0;
        }
        public virtual void Init()
        { }
        public virtual void Run()
        { }
        public virtual void End()
        { }
        public virtual void Show()
        { }
        public virtual void Hide()
        { }
        public virtual int GetState()
        {
            return m_SceneState;
        }
    }
}
