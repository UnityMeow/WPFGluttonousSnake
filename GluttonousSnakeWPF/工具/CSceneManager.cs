using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluttonousSnakeWPF
{
    public class CSceneManager
    {
        //场景管理
        Dictionary<string, CScene> m_SceneList;
        //当前场景
        public CScene m_CurScene;

        //切换场景
        public CScene m_NextScene;
        public CSceneManager()
        {
            //初始化场景表
            m_SceneList = new Dictionary<string, CScene>();
            //初始化切换场景
            m_NextScene = null;
        }
        //加载场景
        public bool LoadScene(string id, CScene scene)
        {
            //查找场景id是否已存在
            if (m_SceneList.ContainsKey(id))
                return false;
            //加载场景
            m_SceneList.Add(id, scene);
            return true;
        }
        //设置初次运行场景
        public bool SetInitScene(string id)
        {
            //查找场景是否存在
            if (!m_SceneList.ContainsKey(id))
                return false;
            if (!m_SceneList.TryGetValue(id, out m_CurScene))
                return false;
            //初始化场景
            m_CurScene.Init();
            return true;
        }
        //设置当前场景
        public bool SetCurScene(string id)
        {
            //查找场景是否存在
            if (!m_SceneList.ContainsKey(id))
                return false;
            if (!m_SceneList.TryGetValue(id, out m_CurScene))
                return false;
            
            return true;
        }
        //设置切换场景
        public bool SetNextScene(string id)
        {
            //查找场景是否存在
            if (!m_SceneList.ContainsKey(id))
                return false;
            if (!m_SceneList.TryGetValue(id, out m_NextScene))
                return false;
            return true;
        }
        ////显示场景
        //public bool ShowScene(string id)
        //{
        //    //查找场景是否存在
        //    if (!m_SceneList.ContainsKey(id))
        //        return false;
        //    if (!m_SceneList.TryGetValue(id, out m_scene))
        //        return false;
        //    m_scene.Show();
        //    return true;
        //}
        ////隐藏场景
        //public bool HideScene(string id)
        //{
        //    //查找场景是否存在
        //    if (!m_SceneList.ContainsKey(id))
        //        return false;
        //    if (!m_SceneList.TryGetValue(id, out m_scene))
        //        return false;
        //    m_scene.Hide();
        //    return true;
        //}
    }
}
