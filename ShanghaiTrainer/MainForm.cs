using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;

/*

血战上海滩八项属性修改器源代码

52pojie@烟99

转载务必保留此部分注释！本源码首发平台为GitHub，如果你是通
过付费下载（充值会员、下载点等），表示你已经上当受骗！转载
过的源码安全性无法保证，请尽快删除，并到本源码首发链接下载。

本源码发布链接：https://github.com/xingshen60771/ShanghaiTrainer



【基本介绍】
本修改器主要讨论内存修改、INI文件解析、字节集XOR逻辑运算、
资源文件打包解包三个课题，涵盖了Windows的API引用、内存读
写、窗体控件控制、包文件结构分析等知识点，既可以作为修改器
使用，也可以作为源代码，适合C#初学者学习！

【郑重声明】
本源码（含成品）仅供技术学习与交流讨论使用！严禁任何非法用途！
*/

namespace ShanghaiTrainer
{
    public partial class MainForm : Form
    {
        // 修改器网站
        string website = "https://www.52pojie.cn/?fromuid=XXX";     //把XXX改成你的UID可以在每年的周年庆、暑期、双十一开放注册时引流。

        #region 初始化主要实例
        // 初始化内存管理器和计时器
        private readonly MemoryManager _memMgr = new MemoryManager();
        private Timer _processCheckTimer;       //进程检测计时器
        private Timer _ammoLockTimer;           //弹药锁定计时器

        // 初始化ToolTip用于气泡框信息提示
        private ToolTip toolTip;

        // 初始化打开INI文件对话框
        OpenFileDialog openINIDialog = new OpenFileDialog();

        // 初始化PCK文件打开保存对话框及PCK打包解包目录对话框
        OpenFileDialog openPCKDialog = new OpenFileDialog();
        SaveFileDialog savePCKDialog = new SaveFileDialog();
        FolderBrowserDialog folderBrowserPCKDialog = new FolderBrowserDialog();

        // 初始化游戏进程监控计时器
        private Timer _gameMonitorTimer;
        private int _attachedProcessId = -1;

        // 初始化键盘钩子成员
        private KeyboardHookLib _keyboardHook;

        /// <summary>
        /// 初始化游戏目录
        /// </summary>
        private void InitializeGamePath()
        {
            // 初始化并读取配置文件
            PublicFunction.InitializeConfigFile();

            // 初始化IniHelper，定位Config.ini
            var ini = new IniHelper();
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configDir = Path.Combine(appDataPath, "52pojie", "ShanghaiTrainer");
            string configPath = Path.Combine(configDir, "Config.ini");
            ini.LoadFromFile(configPath);

            // 读配置
            GloVar.gamePath = ini.Read("Config", "GamePath");

            // 若没有设置游戏路径，询问是否设置
            if (GloVar.gamePath == string.Empty)
            {
                DialogResult msg = MessageBox.Show("发现未设置游戏路径，“关卡全开”和“不使用PCK包文件运行游戏”功能需要有游戏路径的支持，是否设置？\n非强制性要求，可以暂不设置。", "未设置游戏路径", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    Label_GamePath_Click(this, EventArgs.Empty);
                }
            }

        }

        /// <summary>
        /// 初始化ToolTip并设置气泡框
        /// </summary>
        private void InitializeToolTip()
        {
            // 创建 ToolTip 实例
            toolTip = new ToolTip();

            // 配置基础属性
            toolTip.ToolTipTitle = "这是什么意思？";       // 必须设置非空标题才能显示图标
            toolTip.ToolTipIcon = ToolTipIcon.Info;        // 系统的信息图标
            toolTip.IsBalloon = true;                      // 启用气泡框样式
            toolTip.AutoPopDelay = 60000;                  // 给与60秒的阅读时间

            // 关联控件与提示文本
            toolTip.SetToolTip(checkBox_NoPCK, "本选项专为需要的同学准备的。《血战上海滩》将角色、地图、音\n" +
                                               "效等资源文件压缩并封装在了一个名为shanghai.pck的包文件中，\n" +
                                               "游戏默认使用包文件载游戏，不使用PCK文件则需要使用附加命令。\n" +
                                               "如果你把shanghai.pck解包出来的资源文件放在了游戏根目录下并\n" +
                                               "且删除了shanghai.pck或者是不希望使用shanghai.pck加载游戏，\n" +
                                               "那请务必勾选此项目，以告知修改器通过附加命令启动游戏！否则\n" +
                                               "游戏会闪退！");
        }

        /// <summary>
        /// 初始化INI文件打开对话框属性
        /// </summary>
        private void InitializeOpenINIDialog()
        {
            // 置对话框标题
            openINIDialog.Title = "请选择要加密/解密的INI文件:";
            // 置文件过滤器
            openINIDialog.Filter = "INI配置文件|*.ini|CFG配置文件|*.cfg|所有文件|*.*";

            // 如果设置过游戏运行目录，则直接跳转到游戏根目录下的ini文件夹，否则默认桌面目录
            if (GloVar.gamePath != string.Empty)
            {
                openINIDialog.InitialDirectory = GloVar.gamePath + "ini";
            }
            else
            {
                openINIDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
        }

        /// <summary>
        /// 初始化PCK打包控件状态
        /// </summary>
        private void InitializePCKTool()
        {
            // 置解包状态、百分比、操作按钮、组等控件的可视属性为假
            label_PCKState.Visible = false;
            label_PCKPercent.Visible = false;
            btn_PCKPackTips.Visible = false;
            btn_PCKView.Visible = false;
            groupBox_PCKSetting.Visible = false;

            // 默认选中“打包模式”
            radio_PackMode.Checked = true;
            Radio_PackMode_CheckedChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// 初始化计时器
        /// </summary>
        private void InitializeTimers()
        {
            // 进程检测计时器（检测间隔1000毫秒）
            _processCheckTimer = new Timer { Interval = 1000 };
            _processCheckTimer.Tick += ProcessCheckTick;         // 添加时钟周期事件
            _processCheckTimer.Start();                          // 启动此计时器

            // 游戏进程监控计时器（检测间隔1000毫秒）
            _gameMonitorTimer = new Timer { Interval = 1000 };
            _gameMonitorTimer.Tick += GameMonitorTick;           // 添加时钟周期事件

            // 弹药锁定计时器（检测间隔500毫秒）
            _ammoLockTimer = new Timer { Interval = 500 };
            _ammoLockTimer.Tick += AmmoLockTick;                // 添加时钟周期事件

            // 以下是与计时器状态有关的控件状态控制操作

            // 在标签控件中显示激活状态
            label_ProcessCheck.Text = "游戏未启动！";
            label_ProcessCheck.ForeColor = Color.Red;
            label_ProcessCheck.Font = new Font(label_ProcessCheck.Font, FontStyle.Bold);

            // 禁用属性修改项目控件
            checkBox_Allweapon.Enabled = false;
            checkBox_HaveMaxim.Enabled = false;
            checkBox_AmmoLock.Enabled = false;
            btn_Life.Enabled = false;
            btn_Score.Enabled = false;
            btn_Kills.Enabled = false;
            btn_CivilianClear.Enabled = false;
            label_Key1.Enabled = false;
            label_Key2.Enabled = false;
            label_Key3.Enabled = false;
            label_Key4.Enabled = false;
            label_Key5.Enabled = false;
            label_Key6.Enabled = false;
            label_Key7.Enabled = false;
        }

        /// <summary>
        /// 初始化键盘钩子
        /// </summary>
        private void InitializeKeyboardHook()
        {
            _keyboardHook = new KeyboardHookLib();
            _keyboardHook.HotkeyPressed += OnHotkeyPressed;
        }

        #endregion

        #region 快捷键键盘钩子
        /// <summary>
        /// 快捷键处理逻辑
        /// </summary>
        /// <param name="functionNumber"></param>
        private void OnHotkeyPressed(int functionNumber)
        {
            // 确保在UI线程执行操作
            if (InvokeRequired)
            {
                Invoke((Action<int>)OnHotkeyPressed, functionNumber);
                return;
            }

            // 快捷键是否允许激活，根据激活状态标签的颜色来判断
            if (label_ProcessCheck.ForeColor == Color.FromArgb(76, 175, 80))
            {
                // F1-F7功能
                switch (functionNumber)
                {
                    // 全武器解锁
                    case 1:
                        checkBox_Allweapon.Checked = !checkBox_Allweapon.Checked;
                        break;
                    // 拥有马克沁重机枪
                    case 2:
                        checkBox_HaveMaxim.Checked = !checkBox_HaveMaxim.Checked;
                        break;
                    // 弹药锁定
                    case 3:
                        checkBox_AmmoLock.Checked = !checkBox_AmmoLock.Checked;
                        break;
                    // 追加复活次数
                    case 4:
                        AddPlayerLife();
                        break;
                    // 追加杀敌积分
                    case 5:
                        AddKillScore();
                        break;
                    //追加杀敌数
                    case 6:
                        AddKillCount();
                        break;
                    // 误伤平民数清空
                    case 7:
                        CivilianDeathClear();
                        break;
                }
            }
        }
        #endregion

        #region 各种检测回调
        /// <summary>
        /// 进程检测回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessCheckTick(object sender, EventArgs e)
        {
            // 取进程
            var processes = Process.GetProcesses();

            // 按任务栏标题寻找进程，找到进程“战上海”并且成功附加到进程方可执行操作
            foreach (var p in processes)
            {
                if (p.MainWindowTitle == "战上海" && _memMgr.Attach(p))
                {
                    // 保存进程ID
                    _attachedProcessId = p.Id;

                    // 停止进程检测计时器
                    _processCheckTimer.Stop();

                    // 启动游戏进程监控
                    _gameMonitorTimer.Start();

                    // 更新控件状态
                    label_ProcessCheck.Text = "游戏已启动，修改器激活成功！";
                    label_ProcessCheck.ForeColor = Color.FromArgb(76, 175, 80);
                    checkBox_Allweapon.Enabled = true;
                    checkBox_HaveMaxim.Enabled = true;
                    checkBox_AmmoLock.Enabled = true;
                    btn_Life.Enabled = true;
                    btn_Score.Enabled = true;
                    btn_Kills.Enabled = true;
                    btn_CivilianClear.Enabled = true;
                    label_Key1.Enabled = true;
                    label_Key2.Enabled = true;
                    label_Key3.Enabled = true;
                    label_Key4.Enabled = true;
                    label_Key5.Enabled = true;
                    label_Key6.Enabled = true;
                    label_Key7.Enabled = true;
                    SoundPlay.PlaySound(1);     //播放提示音
                    return;
                }
            }
        }

        ///  <summary>
        /// 游戏进程监控回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameMonitorTick(object sender, EventArgs e)
        {
            try
            {
                // 通过进程ID检测进程是否存在
                var process = Process.GetProcessById(_attachedProcessId);

                // 如果进程存在但已退出（防僵尸进程）
                if (process.HasExited) throw new InvalidOperationException();
            }
            catch
            {
                // 进程不存在时的处理
                _gameMonitorTimer.Stop();
                _attachedProcessId = -1;

                // 重置内存管理器
                _memMgr.Detach();

                // 重新启动进程检测
                _processCheckTimer.Start();

                // 更新控件状态
                label_ProcessCheck.Text = "游戏未启动！";
                label_ProcessCheck.ForeColor = Color.Red;
                checkBox_Allweapon.Enabled = false;
                checkBox_HaveMaxim.Enabled = false;
                checkBox_AmmoLock.Enabled = false;
                btn_Life.Enabled = false;
                btn_Score.Enabled = false;
                btn_Kills.Enabled = false;
                btn_CivilianClear.Enabled = false;
                checkBox_Allweapon.Checked = false;
                checkBox_HaveMaxim.Checked = false;
                checkBox_AmmoLock.Checked = false;
                label_Key1.Enabled = false;
                label_Key2.Enabled = false;
                label_Key3.Enabled = false;
                label_Key4.Enabled = false;
                label_Key5.Enabled = false;
                label_Key6.Enabled = false;
                label_Key7.Enabled = false;
                AllWeapon(false);
                AmmoLock(false);
                SoundPlay.PlaySound(3);
            }
        }

        #endregion

        #region 主窗口方法
        public MainForm()
        {
            InitializeGamePath();
            InitializeComponent();
            InitializeTimers();
            InitializeKeyboardHook();
            InitializeToolTip();
            InitializeOpenINIDialog();
            LoadDescription();
            LoadAbout();
            label_UnlockLevelOK.Visible = false;
        }
        #endregion

        #region 修改器核心功能    
        /// <summary>
        /// 作弊功能1：解锁所有武器
        /// </summary>
        private void AllWeapon(bool enable)
        {
            // 武器激活状态地址内只有十进制0和1，分别代表未激活和已激活

            // 武器激活状态指针链 [基址 -> 各武器偏移]
            var weaponOffsets = new[]
            {
                GloConst.Trainer.WeaponActiveState.offsetWeapon01,  // 步枪
                GloConst.Trainer.WeaponActiveState.offsetWeapon02,  // 手榴弹
                GloConst.Trainer.WeaponActiveState.offsetWeapon03,  // 马克沁
                GloConst.Trainer.WeaponActiveState.offsetWeapon04,  // 冲锋枪
                GloConst.Trainer.WeaponActiveState.offsetWeapon05,  // 巴祖卡
                GloConst.Trainer.WeaponActiveState.offsetWeapon06,  // 轻机枪
            };

            // 遍历武器偏移，并将相应的地址填入0x01
            foreach (int offset in weaponOffsets)
            {
                // 通过指针取动态地址
                IntPtr addr = _memMgr.ResolvePointerChain(
                    GloConst.Trainer.baseAddress,
                    new[] { offset });

                if (enable)
                {
                    // 写入0x01
                    _memMgr.WriteByte(addr, 0x01);

                    // 武器解锁后是没有弹药的，还要填满弹药
                    AmmoFull();
                    SoundPlay.PlaySound(1);     //播放提示音
                }
                else
                {
                    // 取消解锁武器
                    _memMgr.WriteByte(addr, 0x00);
                    SoundPlay.PlaySound(2);     //播放提示音
                }

            }
        }

        /// <summary>
        /// 所有弹药填满
        /// </summary>
        private void AmmoFull()
        {
            var capacity = new GloVar.Trainer.ammoCount();

            // 手枪弹药
            IntPtr addrAmmo0 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon00 });
            _memMgr.WriteInt(addrAmmo0, capacity.weapon00);

            // 步枪弹药
            IntPtr addrAmmo1 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon01 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;

            _memMgr.WriteInt(addrAmmo1, capacity.weapon01);

            // 手榴弹弹药
            IntPtr addrAmmo2 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon02 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;

            _memMgr.WriteInt(addrAmmo2, capacity.weapon02);

            // 冲锋枪弹药
            IntPtr addrAmmo3 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon03 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;
            _memMgr.WriteInt(addrAmmo3, capacity.weapon03);

            // 马克沁枪弹药
            IntPtr addrAmmo4 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon04 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;

            _memMgr.WriteInt(addrAmmo4, capacity.weapon04);

            // 巴祖卡弹药
            IntPtr addrAmmo5 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon05 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;

            _memMgr.WriteInt(addrAmmo5, capacity.weapon05);

            // 轻机枪弹药
            IntPtr addrAmmo6 = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon06 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;

            _memMgr.WriteInt(addrAmmo6, capacity.weapon06);
        }

        /// <summary>
        /// 作弊功能2：拥有马克沁重机枪
        /// <param name="enable">(逻辑型 是否拥有)</param>
        /// </summary>
        private void HaveMaximLock(bool enable)
        {

            // 取当前武器地址
            IntPtr currentWeapon = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.offsetCurrentWeapon });

            // 马克沁弹药
            IntPtr addr = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.AmmoAddress.offsetWeapon04 }) + GloConst.Trainer.AmmoAddress.offsetWeapon00;


            // 如果想拥有就写入数据，否则恢复之前的武器
            if (enable)
            {
                //取当前武器并记录
                GloVar.Trainer.weaponBeforemod = _memMgr.ReadInt(currentWeapon);
                _memMgr.WriteInt(currentWeapon, 0x4);
                _memMgr.WriteInt(addr, 240);
                SoundPlay.PlaySound(1);     //播放提示音
            }
            else
            {
                _memMgr.WriteInt(currentWeapon, GloVar.Trainer.weaponBeforemod);
                SoundPlay.PlaySound(2);     //播放提示音
            }

            // 开启/关闭内存写保护，以防游戏时按下其他键造成主视角枪械混乱
            _memMgr.ToggleMemoryProtection(currentWeapon, enable);
        }

        /// <summary>
        /// 弹药锁定计时器时钟周期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmmoLockTick(object sender, EventArgs e)
        {
            // 直接调用所有弹药填满方法
            AmmoFull();
        }

        /// <summary>
        /// 作弊功能3：弹药锁定
        /// </summary>
        /// <param name="enable"></param>
        private void AmmoLock(bool enable)
        {
            if (enable)
            {
                _ammoLockTimer.Start();
                SoundPlay.PlaySound(1);
            }
            else
            {
                _ammoLockTimer.Stop();
                SoundPlay.PlaySound(2);
            }
        }

        /// <summary>
        /// 作弊功能4：追加九条命
        /// </summary>
        private void AddPlayerLife()
        {
            // 取玩家生命数地址
            IntPtr lifeAddr = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.offsetPlayerLife });

            // 取玩家可复活次数
            int currentLife = _memMgr.ReadInt(lifeAddr);

            // 向地址内追加九条命
            _memMgr.WriteInt(lifeAddr, currentLife + 9);
            SoundPlay.PlaySound(1);     //播放提示音
        }

        /// <summary>
        /// 作弊功能5：追加1000杀敌得分
        /// </summary>
        private void AddKillScore()
        {
            // 取杀敌得分地址
            IntPtr scoreAddr = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.offsetKillScore });

            // 取当前杀敌积分
            int currentScore = _memMgr.ReadInt(scoreAddr);

            // 向地址内追加1000得分
            _memMgr.WriteInt(scoreAddr, currentScore + 1000);
            SoundPlay.PlaySound(1);    //播放提示音
        }

        /// <summary>
        //// 作弊功能6：追加100杀敌数
        /// </summary>
        private void AddKillCount()
        {
            // 取杀敌数地址
            IntPtr killCountAddr = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.offsetKills });

            // 取当前杀敌数
            int currentKillCount = _memMgr.ReadInt(killCountAddr);
            _memMgr.WriteInt(killCountAddr, currentKillCount + 100);
            SoundPlay.PlaySound(1);    //播放提示音
        }

        /// <summary>
        /// 作弊功能7：清空误伤平民
        /// </summary>
        private void CivilianDeathClear()
        {
            // 取杀误伤平民数地址
            IntPtr civilianDeathAddr = _memMgr.ResolvePointerChain(
                GloConst.Trainer.baseAddress,
                new[] { GloConst.Trainer.offsetCivilian });

            // 误伤平民数直接填零
            _memMgr.WriteInt(civilianDeathAddr, 0);
            SoundPlay.PlaySound(1);    //播放提示音;
        }


        /// <summary>
        /// 作弊功能8：解锁所有关卡
        /// </summary>
        private void UnlockLevel()
        {
            // 检查是否设置了游戏路径
            if (GloVar.gamePath == string.Empty)
            {
                // 没有则点击一下设置游戏路径标签
                DialogResult msg = MessageBox.Show("未设置游戏路径！此修改项目通过静态修改游戏存档实现，需要游戏路径的支持，是否设置？", "是否设置？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msg == DialogResult.Yes)
                {
                    Label_GamePath_Click(this, EventArgs.Empty);
                }
                else
                {
                    return;
                }
            }
            // 设置了游戏路径，继续操作
            else
            {
                // 取mission.cfg绝对路径并检查 是否存在                  
                string gameSaveFile = GloVar.gamePath + "ini\\mission.cfg";
                if (!File.Exists(gameSaveFile))
                {
                    Console.WriteLine(gameSaveFile);
                    MessageBox.Show($@"抱歉！ {gameSaveFile} 不存在！", "找不到游戏存档文件", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 尝试解密写入
                try
                {
                    // 创建INI实例
                    var gameSaveData = new IniHelper();

                    // 加载加密文件
                    byte[] encryptedData = File.ReadAllBytes(gameSaveFile);
                    string decryptedText = gameSaveData.INIDecrypt(encryptedData);
                    gameSaveData.LoadFromBytes(Encoding.Unicode.GetBytes(decryptedText), Encoding.Unicode);

                    // 解密完成，开始读取已解锁关卡
                    Console.WriteLine("INI解密成功！\nINI明文:\n\n" + decryptedText);  //调试输出
                    string currentLevel = gameSaveData.Read("plan", "mis");            //取“plan”节的mis值
                    Console.WriteLine($@"当前已解锁{currentLevel}");                   //调试输出

                    // 如果已经解锁到4-4，则终止操作
                    if (currentLevel == 16.ToString())
                    {
                        // 更新控件状态
                        label_UnlockLevelOK.Visible = true;
                        label_UnlockLevelOK.Text = "关卡已全部解锁，不需要重复操作！";
                        label_UnlockLevelOK.ForeColor = Color.Red;
                        label_UnlockLevelOK.Font = new Font(label_ProcessCheck.Font, FontStyle.Bold);
                        SoundPlay.PlaySound(2);         //播放提示音
                        return;
                    }

                    // 写INI
                    gameSaveData.Write("plan", "mis", 16.ToString());

                    // 保存并加密
                    byte[] newData = gameSaveData.SaveToBytes(Encoding.Unicode);
                    byte[] encryptedNewData = gameSaveData.INIEncrypt(Encoding.Unicode.GetString(newData));
                    File.WriteAllBytes(gameSaveFile, encryptedNewData);

                    // 保存成功，更新控件状态
                    label_UnlockLevelOK.Visible = true;
                    label_UnlockLevelOK.Text = "所以关卡解锁成功！重启游戏生效！";
                    label_UnlockLevelOK.ForeColor = Color.Red;
                    label_UnlockLevelOK.Font = new Font(label_ProcessCheck.Font, FontStyle.Bold);
                    SoundPlay.PlaySound(1);
                }
                catch (Exception ex)
                {
                    // INI读写失败容错提示
                    MessageBox.Show($"改写配置文件出现错误，关卡解锁失败！\n    {ex.Message}", "解锁关卡失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region 非修改器核心功能
        /// <summary>
        /// 启动游戏
        /// </summary>
        private void RunGame()
        {
            try
            {
                // 从游戏路径取文件名
                string exePath = GloVar.gamePath + "shanghai.exe";

                // 检查文件是否存在，存在则执行else
                if (!File.Exists(exePath))
                {
                    MessageBox.Show($"未找到游戏目录：{GloVar.gamePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    // 定义命令行参数
                    string launchArgsWindowMode = "-windows";   // 窗口模式运行游戏
                    string launchArgsNoPck = "-nousepck";       // 不使用PCK文件加载游戏

                    // 根据用户选项设置命令行参数
                    string launchArgs = string.Empty;           //初始化命令行                
                    launchArgs = $"{(checkBox_WindowMode.Checked ? launchArgsWindowMode : string.Empty)} {(checkBox_NoPCK.Checked ? launchArgsNoPck : string.Empty)}";

                    // 检查文件是否存在
                    if (!File.Exists(exePath))
                    {
                        MessageBox.Show($"未找到游戏主程序：{exePath}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 构建启动参数
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                        WorkingDirectory = Path.GetDirectoryName(exePath),
                        Arguments = launchArgs                               // 根据复选框状态设置参数
                    };

                    // 启动游戏进程
                    Process.Start(startInfo);

                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"游戏主程序路径错误：{ex.Message}", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show($"启动游戏失败：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"未知错误：{ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加密INI
        /// <param name="filePath">(文本型 欲加密的INI)</param>
        /// </summary>
        private void INIEncrypt(string filePath)
        {
            // 初始化INI实例
            var iniEncrypt = new IniHelper();
            try
            {
                // 读入欲加密的INI
                string plainText = File.ReadAllText(filePath);
                byte[] encryptedData = iniEncrypt.INIEncrypt(plainText);

                // 保存加密的INI
                string newFileName = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileName(filePath) + "_Ecrypted.ini";
                File.WriteAllBytes(newFileName, encryptedData);

                // 弹出提示
                MessageBox.Show($@"加密INI完成！已保存在{newFileName}", "解密完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                // 错误提示
                MessageBox.Show("加密INI失败！", "加密失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 解密INI
        /// <param name="filePath">(文本型 欲解密的INI)</param>
        /// </summary>
        private void INIDecrypt(string filePath)
        {
            // 初始化INI实例
            var iniDecrypt = new IniHelper();

            try
            {
                // 读入加密的INI
                byte[] encryptedData = File.ReadAllBytes(filePath);
                string decryptedText = iniDecrypt.INIDecrypt(encryptedData);

                // 保存解密的INI
                string newFileName = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileName(filePath) + "_Decrypted.ini";
                File.WriteAllText(newFileName, decryptedText);

                // 弹出提示
                MessageBox.Show($@"解密INI完成！已保存在{newFileName}", "解密完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                // 错误提示
                MessageBox.Show("解密INI失败！", "解密失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 弹出PCK打包提示
        /// </summary>
        private void ShowPCKPackTips()
        {
            MessageBox.Show(
                "打包须知：\r\n" +
                "1、打包前尽量备份原来的PCK文件及修改过的PCK包内文件，以防修改的文件存在问题导致游戏崩溃、闪退等未知错误。\r\n" +
                "2、建议不要随意修改游戏的配置文件，特别是模型配置文件，否则可能会导致模型无法加载或显示异常从而造成游戏卡关。\r\n" +
                "3、Zlib压缩比越高，游戏加载速度越慢，因此不提倡使用太高压缩比的Zlib，保持1级压缩或不压缩即可。\r\n",
               "打包须知",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        /// <summary>
        /// 打包模式
        /// </summary>
        private void PackMode()
        {
            // 目标PCK文件变量留空
            GloVar.PCKHelper.targetPCK = string.Empty;

            // 默认压缩等级为1
            numericUpDown_compressLevel.Value = 1;

            // 进度条归零
            progressBar_PCK.Value = 0;

            // 清空PCK路径、目录
            textBox_PCKPath.Text = string.Empty;
            textBox_IOpatch.Text = string.Empty;

            // 更新控件状态
            label_PCKState.Visible = false;
            label_PCKState.ForeColor = SystemColors.ControlText;
            label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Regular);
            label_PCKState.Text = string.Empty;
            groupBox_PCKSetting.Visible = true;
            btn_PCKPackTips.Visible = true;
            btn_PCKView.Visible = false;
            btn_PCKView.Enabled = false;

            // 更新控件文本
            label_PCKPath.Text = "请选择要保存的PCK文件位置";
            label_IOPath.Text = "请选择要打包的文件夹路径:";
            btn_PCKPath.Text = "保存到…";
            btn_IOPatch.Text = "选择";
            btn_PCKExecute.Text = "打包PCK";
            savePCKDialog.Title = "请选择要保存的PCK文件路径:";
            savePCKDialog.Filter = "PCK资源文件|*.pck|所有文件|*.*";
            folderBrowserPCKDialog.Description = "请选择要打包PCK的文件夹:";
        }

        /// <summary>
        /// 解包模式
        /// </summary>
        private void UnPackMode()
        {
            // 目标PCK文件变量留空
            GloVar.PCKHelper.targetPCK = string.Empty;

            // 进度条归零
            progressBar_PCK.Value = 0;

            // 更新控件状态
            label_PCKState.Visible = false;
            label_PCKState.ForeColor = SystemColors.ControlText;
            label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Regular);
            label_PCKState.Text = string.Empty;
            groupBox_PCKSetting.Visible = false;
            btn_PCKPackTips.Visible = false;
            btn_PCKView.Visible = false;
            btn_PCKView.Enabled = false;
            textBox_PCKPath.Text = string.Empty;
            textBox_IOpatch.Text = string.Empty;

            // 更新控件文本
            label_PCKPath.Text = "请选择要解包的PCK文件:";
            label_IOPath.Text = "请选择要解包的文件夹路径:";
            btn_PCKPath.Text = "选择PCK";
            btn_IOPatch.Text = "解包到…";
            btn_PCKExecute.Text = "解包PCK";
            openPCKDialog.Title = "请选择要解包的PCK文件路径:";
            openPCKDialog.Filter = "PCK资源文件|*.pck|所有文件|*.*";
            folderBrowserPCKDialog.Description = "请选择PCK文件解包文件夹:";
        }

        /// <summary>
        /// 选择PCK
        /// </summary>
        private void ChoosePCK()
        {

            if (radio_PackMode.Checked)                         // 如果是打包模式
            {
                // 弹出文件保存对话框，读入文件路径
                if (savePCKDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox_PCKPath.Text = savePCKDialog.FileName;
                }
            }
            else if (radio_UnPackMode.Checked)                  // 如果是解包模式
            {
                // 弹出文件选择对话框，读入文件路径
                if (openPCKDialog.ShowDialog() == DialogResult.OK)
                {
                    // 弹出风险提示，接受则读入PCK文件
                    DialogResult r = MessageBox.Show(
                        "警告，修改器的解包功能仅用于逆向技术研究使用，游戏资源文件之版权归相关公司所有，严禁将所获得的游戏资源文件用于其他用途，否则修改器作者概不承担因而造成的一切后果。如果接受并继续，请点击<是>，否则请不要使用，点击<否>。",
                        "郑重声明",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                        );
                    if (r == DialogResult.Yes)
                    {
                        // 读入PCK文件
                        textBox_PCKPath.Text = openPCKDialog.FileName;
                        LoadPCK(textBox_PCKPath.Text);
                    }
                    else
                    {
                        // 不同意操作则清空PCK目录
                        textBox_IOpatch.Text = string.Empty;
                        textBox_PCKPath.Text = string.Empty;
                        return;
                    }
                }
            }
            else
            {
                // 容灾处理，打包模式和解包模式均未选中则弹出提示
                MessageBox.Show($"未指定模式", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读入PCK文件
        /// </summary>
        /// <param name="filename"></param>
        private void LoadPCK(string filename)
        {
            //尝试读入PCK文件
            try
            {
                // 解析PCK文件列表并放入指定的全局变量
                GloVar.PCKHelper.pckList = PCKHelper.UnPack.GetPCKInformation(filename);

                // 取PCK包内文件数
                int pckFileCount = GloVar.PCKHelper.pckList.Count;

                // 更新控件状态
                label_PCKState.Visible = true;
                label_PCKState.ForeColor = Color.FromArgb(76, 175, 80);
                label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Bold);
                label_PCKState.Text = $"支持的格式，可以解包！共有 {pckFileCount} 个文件";
                btn_PCKView.Visible = true;
                btn_PCKView.Enabled = true;
                // 将被操作的PCK文件绝对路径放入指定的全局变量
                GloVar.PCKHelper.targetPCK = filename;

                // 播放提示音
                SoundPlay.PlaySound(1);
            }
            catch (Exception ex)        //读入失败时
            {
                //更新控件状态
                label_PCKState.Visible = true;
                label_PCKState.ForeColor = Color.Red;
                label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Bold);
                label_PCKState.Text = $"无法解包！原因:{ex.Message}";          //告知错误原因
                //播放提示音
                SystemSounds.Hand.Play();
            }
        }

        /// <summary>
        /// 执行PCK打包/解包
        /// </summary>
        private void PCKExecute()
        {
            // 检查目录是否合规并将抛出的异常用信息框接住
            try
            {
                PatchCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "不好意思", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 打包模式下的操作
            if (radio_PackMode.Checked)
            {
                // 询问是否要操作
                DialogResult r = MessageBox.Show(
                    "警告，修改器的打包功能仅用于研究修改技术使用，游戏版权归相关公司所有，严禁将修改版PCK文件用于其他用途，否则修改器作者概不承担因而造成的一切后果。如果接受并继续，请点击<是>，否则请不要使用，点击<否>。",
                    "郑重声明",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );
                if (r == DialogResult.Yes)
                {
                    DoPack();                       //开始打包
                }
                else
                {
                    // 不同意操作则清空PCK目录
                    textBox_IOpatch.Text = string.Empty;
                    textBox_PCKPath.Text = string.Empty;
                    return;
                }
            }
            else if (radio_UnPackMode.Checked)      // 解包模式下的操作
            {
                DoUnPack();
            }
        }

        /// <summary>
        /// PCK打包解包目录检查
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void PatchCheck()
        {
            // 取PCK文件及目录
            string filepath = textBox_PCKPath.Text;
            string directory = textBox_IOpatch.Text;

            try
            {
                // 检查目录是否合法
                new PublicFunction.PathValidator().ValidatePaths(
                      filepath,
                      directory,
                      radio_PackMode.Checked ? true : false
                  );
            }
            catch (Exception ex)
            {
                // 出现问题接住异常抛给上一层
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 执行打包
        /// </summary>
        private async void DoPack()
        {
            // 压缩等级默认为-1
            int compressLevel = -1;

            // 需要压缩PCK数据时，压缩等级以调节器数值为准，不需要时压缩等级依然为-1
            if (radio_AreCompress_Yes.Checked)
            {
                compressLevel = (int)numericUpDown_compressLevel.Value;
            }
            else if (radio_AreCompress_No.Checked)
            {
                compressLevel = -1;
            }

            // 执行打包操作（异步进行）
            await PCKHelper.Pack.PackPckAsync(textBox_IOpatch.Text, textBox_PCKPath.Text, compressLevel, progress =>
            {
                // 更新控件状态
                label_PCKPath.Enabled = false;
                label_IOPath.Enabled = false;
                radio_PackMode.Enabled = false;
                radio_UnPackMode.Enabled = false;
                textBox_PCKPath.Enabled = false;
                textBox_IOpatch.Enabled = false;
                btn_PCKPath.Enabled = false;
                btn_IOPatch.Enabled = false;
                btn_PCKView.Enabled = false;
                btn_PCKExecute.Enabled = false;
                groupBox_PCKSetting.Enabled = false;
                btn_PCKPackTips.Enabled = false;
                label_PCKState.Visible = true;
                label_PCKState.ForeColor = SystemColors.ControlText;
                label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Regular);
                label_PCKState.Text = progress.state;
                label_PCKPercent.Visible = true;

                // 置进度条百分比
                progressBar_PCK.Value = progress.percentage;
                label_PCKPercent.Text = progress.percentage.ToString() + "%";

                // 进度为100%时的解除控件禁用、弹出提示
                if (progress.percentage == 100)
                {
                    // 更新控件状态
                    label_PCKPath.Enabled = true;
                    label_IOPath.Enabled = true;
                    radio_PackMode.Enabled = true;
                    radio_UnPackMode.Enabled = true;
                    textBox_PCKPath.Enabled = true;
                    textBox_IOpatch.Enabled = true;
                    btn_PCKPath.Enabled = true;
                    btn_IOPatch.Enabled = true;
                    btn_PCKView.Enabled = true;
                    btn_PCKExecute.Enabled = true;
                    groupBox_PCKSetting.Enabled = true;
                    btn_PCKPackTips.Enabled = true;
                    label_PCKState.ForeColor = Color.Blue;
                    label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Bold);
                    label_PCKPercent.Visible = false;
                    label_PCKState.Text = "打包完成！";
                    label_PCKPercent.Text = string.Empty;

                    // 询问是否打开输出文件夹
                    DialogResult r = MessageBox.Show("打包PCK完成，是否打开输出的文件夹？", "打包完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string targetPCK = textBox_PCKPath.Text;
                        PublicFunction.GetOutputFolder(targetPCK);
                    }
                    else
                    {
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 执行解包
        /// </summary>
        private async void DoUnPack()
        {
            await PCKHelper.UnPack.ExtractAllFileAsync(GloVar.PCKHelper.targetPCK, GloVar.PCKHelper.pckList, textBox_IOpatch.Text, progress =>
            {
                // 更新控件状态
                label_PCKPath.Enabled = false;
                label_IOPath.Enabled = false;
                radio_PackMode.Enabled = false;
                radio_UnPackMode.Enabled = false;
                textBox_PCKPath.Enabled = false;
                textBox_IOpatch.Enabled = false;
                btn_PCKPath.Enabled = false;
                btn_IOPatch.Enabled = false;
                btn_PCKView.Enabled = false;
                btn_PCKExecute.Enabled = false;
                groupBox_PCKSetting.Enabled = false;
                btn_PCKPackTips.Enabled = false;
                label_PCKState.Visible = true;
                label_PCKState.ForeColor = SystemColors.ControlText;
                label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Regular);
                label_PCKState.Text = progress.state;
                label_PCKPercent.Visible = true;

                // 置进度条百分比
                progressBar_PCK.Value = progress.percentage;
                label_PCKPercent.Text = progress.percentage.ToString() + "%";

                // 进度为100%时的解除控件禁用、弹出提示
                if (progress.percentage == 100)
                {
                    // 更新控件状态
                    label_PCKPath.Enabled = true;
                    label_IOPath.Enabled = true;
                    radio_PackMode.Enabled = true;
                    radio_UnPackMode.Enabled = true;
                    textBox_PCKPath.Enabled = true;
                    textBox_IOpatch.Enabled = true;
                    btn_PCKPath.Enabled = true;
                    btn_IOPatch.Enabled = true;
                    btn_PCKView.Enabled = true;
                    btn_PCKExecute.Enabled = true;
                    groupBox_PCKSetting.Enabled = true;
                    btn_PCKPackTips.Enabled = true;
                    label_PCKState.ForeColor = Color.FromArgb(76, 175, 80);
                    label_PCKState.Font = new Font(label_PCKState.Font, FontStyle.Bold);
                    label_PCKState.Text = "解包完成！";
                    label_PCKPercent.Visible = false;
                    label_PCKPercent.Text = string.Empty;

                    // 询问是否打开输出文件夹
                    DialogResult r = MessageBox.Show("解包PCK完成，是否打开输出的文件夹？", "解包完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string targetPCK = textBox_IOpatch.Text;
                        PublicFunction.GetOutputFolder(targetPCK);
                    }
                    else
                    {
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 载入修改器描述
        /// </summary>
        private void LoadDescription()
        {
            // 置文本框颜色
            textBox_Description.BackColor = Color.FromArgb(199, 237, 204);
            // 载入文本
            textBox_Description.Text = Properties.Resources.Description;
        }

        /// <summary>
        /// 载入关于页面信息
        /// </summary>
        private void LoadAbout()
        {
            // 显示修改器信息
            label_About_Title.Text =
                $"{PublicFunction.GetAPPInformation(1)}\r\n" +
                $"{PublicFunction.GetAPPInformation(2)}\r\n" +
                $"by {PublicFunction.GetAPPInformation(3)}\r\n\r\n\r\n" +
                $"转载务必保留此信息！本源码首发平台为GitHub，如果你是通过付\r\n" +
                $"费下载（充值会员、下载点等），表示你已经上当受骗！转载过的\r\n" +
                $"源码安全性无法保证，请尽快删除，并到本源码首发链接下载。\r\n\r\n" +
                $"本源码发布链接：https://github.com/xingshen60771/ShanghaiTrainer";

            // 显示网址信息(不要显示后面的推广信息)
            label_Website.Text = website.Substring(0, 23);
        }
        #endregion

        #region 各控件事件响应函数

        /// <summary>
        /// 解锁所有武器复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Allweapon_CheckedChanged(object sender, EventArgs e)
        {
            AllWeapon(checkBox_Allweapon.Checked);
        }

        /// <summary>
        /// 拥有马克沁重机枪复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_HaveMaxim_CheckedChanged(object sender, EventArgs e)
        {
            HaveMaximLock(checkBox_HaveMaxim.Checked);
        }

        /// <summary>
        /// 弹药锁定复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_AmmoLock_CheckedChanged(object sender, EventArgs e)
        {
            AmmoLock(checkBox_AmmoLock.Checked);
        }

        /// <summary>
        /// 追加九条命按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Life_Click(object sender, EventArgs e)
        {
            AddPlayerLife();
        }

        /// <summary>
        /// 追加1000杀敌得分按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Score_Click(object sender, EventArgs e)
        {
            AddKillScore();
        }

        /// <summary>
        /// 追加100杀敌数按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Kills_Click(object sender, EventArgs e)
        {
            AddKillCount();
        }

        /// <summary>
        /// 清空误伤平民按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CivilianClear_Click(object sender, EventArgs e)
        {
            CivilianDeathClear();
        }

        /// <summary>
        /// 解锁所有关卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UnlockLevel_Click(object sender, EventArgs e)
        {
            UnlockLevel();
        }

        /// <summary>
        /// 设置游戏路径标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_GamePath_Click(object sender, EventArgs e)
        {
            // 打开设置游戏路径窗口
            Form_GamePath Form_GamePath = new Form_GamePath();
            Form_GamePath.ShowDialog();
        }

        /// <summary>
        /// 启动游戏标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_RunGame_Click(object sender, EventArgs e)
        {
            RunGame();
        }

        /// <summary>
        /// 打开INI文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_INIOpen_Click(object sender, EventArgs e)
        {
            // 将选中的INI文件加入到INI文本框
            if (openINIDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_INIPath.Text = openINIDialog.FileName;
            }
        }

        /// <summary>
        /// 解密INI按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_INIDecrypt_Click(object sender, EventArgs e)
        {
            INIDecrypt(textBox_INIPath.Text);
        }

        /// <summary>
        /// 加密INI按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_INIEncrypt_Click(object sender, EventArgs e)
        {
            INIEncrypt(textBox_INIPath.Text);
        }

        /// <summary>
        /// PCK打包模式复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_PackMode_CheckedChanged(object sender, EventArgs e)
        {
            PackMode();
        }

        /// <summary>
        /// PCK打包模式复选框松开鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_PackMode_MouseUp(object sender, MouseEventArgs e)
        {
            // 弹出打包提示
            ShowPCKPackTips();
        }

        /// <summary>
        /// 解包模式复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_UnPackMode_CheckedChanged(object sender, EventArgs e)
        {
            UnPackMode();
        }

        /// <summary>
        /// 选项卡索引发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选项卡索引为2时，初始化PCK打包解包工具
            if (tabControl_Main.SelectedIndex == 2)
            {
                InitializePCKTool();
                ShowPCKPackTips();
            }
        }

        /// <summary>
        /// 选项卡索引正在改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // 打包或解包过程中，相关控件将禁用，当禁用时且选项卡索引不为2时，禁止切换
            if (label_PCKPath.Enabled == false && tabControl_Main.SelectedIndex != 2)
            {
                MessageBox.Show("必须等到打包或解包操作完成后才能切换选项卡！", "不好意思", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }


        /// <summary>
        /// 执行PCK打包解包按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PCKExecute_Click(object sender, EventArgs e)
        {
            PCKExecute();
        }

        /// <summary>
        /// 选择PCK文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PCKPath_Click(object sender, EventArgs e)
        {
            ChoosePCK();
        }

        /// <summary>
        /// PCK输入输出路径按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_IOPatch_Click(object sender, EventArgs e)
        {
            // 置PCK输入输出路径文本框文本为目录选择对话框所选路径
            if (folderBrowserPCKDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_IOpatch.Text = folderBrowserPCKDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 浏览PCK文件列表按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PCKView_Click(object sender, EventArgs e)
        {
            Form_PCKView form_PCKView = new Form_PCKView();
            form_PCKView.ShowDialog();
        }

        /// <summary>
        /// 启用PCK数据压缩复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_AreCompress_Yes_CheckedChanged(object sender, EventArgs e)
        {
            // 启用PCK数据压缩的控件状态逻辑
            numericUpDown_compressLevel.Enabled = true;
        }

        /// <summary>
        /// 不启用PCK数据压缩复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Radio_AreCompress_No_CheckedChanged(object sender, EventArgs e)
        {
            // 不启用PCK数据压缩的控件状态逻辑
            numericUpDown_compressLevel.Enabled = false;
            numericUpDown_compressLevel.Value = 1;
        }

        /// <summary>
        /// 弹出PCK打包提示按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PCKPackTips_Click(object sender, EventArgs e)
        {
            ShowPCKPackTips();
        }

        /// <summary>
        /// 网站链接标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Website_Click(object sender, EventArgs e)
        {
            // 访问网页
            PublicFunction.OpenURL(website);
        }

        private void pictureBox_52pojieLogo_Click(object sender, EventArgs e)
        {
            label_Website_Click(this, EventArgs.Empty);
        }
        #endregion


    }
}
