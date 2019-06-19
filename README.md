# WPFGluttonousSnake

### 项目说明  
- 入口脚本  
	主入口 -> MainWindow.xaml.cs  
- 场景相关  
	场景管理 -> CSceneManager.cs  
	场景基类 -> CScene.cs  
	开始场景 -> CGameStartScene.cs  
	游戏场景 -> CGameRunScene.cs  
	榜单场景 -> CGameRankScene.cs  
	结束场景 -> CGameOverScene.cs  
	选择场景 -> CGameChooseScene.cs  
- 图片相关  
	自定义图片工具 -> CCustomImage.cs  
- 控件相关  
	控件工具 -> CUtil.cs  
- 英雄相关  
	英雄类 -> CHero.cs  
- 游戏数据  
	数据文件 -> rank.data  
### 游戏说明  
- 操作相关  
	鼠标单击选择选项  
	传统模式 - 键盘'↑' '↓' '←' '→'控制方向  
	无尽模式 - 键盘'←' '→'控制方向  
- 游戏模式  
	传统模式 - 四方向  
	无尽模式 - 可旋转  
- 游戏规则  
	吃到小方块可增加蛇身长度  
	无尽模式下蛇身达到一定长度速度会增加  
	传统模式下蛇头碰到蛇身或碰到墙壁游戏结束  
- 榜单规则  
	分数高于历史榜单记录则可记录上榜  