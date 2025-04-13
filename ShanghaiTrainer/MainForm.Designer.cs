namespace ShanghaiTrainer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl_Main = new System.Windows.Forms.TabControl();
            this.tabPage_Trainer = new System.Windows.Forms.TabPage();
            this.checkBox_WindowMode = new System.Windows.Forms.CheckBox();
            this.label_Key2 = new System.Windows.Forms.Label();
            this.checkBox_HaveMaxim = new System.Windows.Forms.CheckBox();
            this.label_Tips = new System.Windows.Forms.Label();
            this.label_UnlockLevelOK = new System.Windows.Forms.Label();
            this.checkBox_NoPCK = new System.Windows.Forms.CheckBox();
            this.label_RunGame = new System.Windows.Forms.Label();
            this.label_GamePath = new System.Windows.Forms.Label();
            this.label_Item = new System.Windows.Forms.Label();
            this.label_Key0 = new System.Windows.Forms.Label();
            this.label_Key7 = new System.Windows.Forms.Label();
            this.label_Key6 = new System.Windows.Forms.Label();
            this.label_Key5 = new System.Windows.Forms.Label();
            this.label_Key4 = new System.Windows.Forms.Label();
            this.label_Key3 = new System.Windows.Forms.Label();
            this.label_Key1 = new System.Windows.Forms.Label();
            this.checkBox_Allweapon = new System.Windows.Forms.CheckBox();
            this.label_ProcessCheck = new System.Windows.Forms.Label();
            this.checkBox_AmmoLock = new System.Windows.Forms.CheckBox();
            this.btn_CivilianClear = new System.Windows.Forms.Button();
            this.btn_UnlockLevel = new System.Windows.Forms.Button();
            this.btn_Kills = new System.Windows.Forms.Button();
            this.btn_Score = new System.Windows.Forms.Button();
            this.btn_Life = new System.Windows.Forms.Button();
            this.pictureBox_Cover = new System.Windows.Forms.PictureBox();
            this.tabPage_INIEncryption = new System.Windows.Forms.TabPage();
            this.btn_INIEncrypt = new System.Windows.Forms.Button();
            this.btn_INIDecrypt = new System.Windows.Forms.Button();
            this.btn_INIOpen = new System.Windows.Forms.Button();
            this.textBox_INIPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_UNPCK = new System.Windows.Forms.TabPage();
            this.btn_PCKPackTips = new System.Windows.Forms.Button();
            this.groupBox_PCKSetting = new System.Windows.Forms.GroupBox();
            this.label_AreZlib = new System.Windows.Forms.Label();
            this.label_Recommended = new System.Windows.Forms.Label();
            this.radio_AreCompress_No = new System.Windows.Forms.RadioButton();
            this.radio_AreCompress_Yes = new System.Windows.Forms.RadioButton();
            this.numericUpDown_compressLevel = new System.Windows.Forms.NumericUpDown();
            this.btn_PCKExecute = new System.Windows.Forms.Button();
            this.label_PCKPercent = new System.Windows.Forms.Label();
            this.label_PCKState = new System.Windows.Forms.Label();
            this.progressBar_PCK = new System.Windows.Forms.ProgressBar();
            this.btn_PCKView = new System.Windows.Forms.Button();
            this.btn_IOPatch = new System.Windows.Forms.Button();
            this.btn_PCKPath = new System.Windows.Forms.Button();
            this.label_IOPath = new System.Windows.Forms.Label();
            this.label_PCKPath = new System.Windows.Forms.Label();
            this.textBox_IOpatch = new System.Windows.Forms.TextBox();
            this.textBox_PCKPath = new System.Windows.Forms.TextBox();
            this.radio_UnPackMode = new System.Windows.Forms.RadioButton();
            this.radio_PackMode = new System.Windows.Forms.RadioButton();
            this.tabPage_Description = new System.Windows.Forms.TabPage();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.tabPage_About = new System.Windows.Forms.TabPage();
            this.pictureBox_52pojieLogo = new System.Windows.Forms.PictureBox();
            this.label_About_Title = new System.Windows.Forms.Label();
            this.label_WelcomeTo52pojie = new System.Windows.Forms.Label();
            this.label_Website = new System.Windows.Forms.Label();
            this.tabControl_Main.SuspendLayout();
            this.tabPage_Trainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cover)).BeginInit();
            this.tabPage_INIEncryption.SuspendLayout();
            this.tabPage_UNPCK.SuspendLayout();
            this.groupBox_PCKSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_compressLevel)).BeginInit();
            this.tabPage_Description.SuspendLayout();
            this.tabPage_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_52pojieLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.Controls.Add(this.tabPage_Trainer);
            this.tabControl_Main.Controls.Add(this.tabPage_INIEncryption);
            this.tabControl_Main.Controls.Add(this.tabPage_UNPCK);
            this.tabControl_Main.Controls.Add(this.tabPage_Description);
            this.tabControl_Main.Controls.Add(this.tabPage_About);
            this.tabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Main.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.SelectedIndex = 0;
            this.tabControl_Main.Size = new System.Drawing.Size(1006, 503);
            this.tabControl_Main.TabIndex = 0;
            this.tabControl_Main.SelectedIndexChanged += new System.EventHandler(this.TabControl_Main_SelectedIndexChanged);
            this.tabControl_Main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl_Main_Selecting);
            // 
            // tabPage_Trainer
            // 
            this.tabPage_Trainer.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Trainer.Controls.Add(this.checkBox_WindowMode);
            this.tabPage_Trainer.Controls.Add(this.label_Key2);
            this.tabPage_Trainer.Controls.Add(this.checkBox_HaveMaxim);
            this.tabPage_Trainer.Controls.Add(this.label_Tips);
            this.tabPage_Trainer.Controls.Add(this.label_UnlockLevelOK);
            this.tabPage_Trainer.Controls.Add(this.checkBox_NoPCK);
            this.tabPage_Trainer.Controls.Add(this.label_RunGame);
            this.tabPage_Trainer.Controls.Add(this.label_GamePath);
            this.tabPage_Trainer.Controls.Add(this.label_Item);
            this.tabPage_Trainer.Controls.Add(this.label_Key0);
            this.tabPage_Trainer.Controls.Add(this.label_Key7);
            this.tabPage_Trainer.Controls.Add(this.label_Key6);
            this.tabPage_Trainer.Controls.Add(this.label_Key5);
            this.tabPage_Trainer.Controls.Add(this.label_Key4);
            this.tabPage_Trainer.Controls.Add(this.label_Key3);
            this.tabPage_Trainer.Controls.Add(this.label_Key1);
            this.tabPage_Trainer.Controls.Add(this.checkBox_Allweapon);
            this.tabPage_Trainer.Controls.Add(this.label_ProcessCheck);
            this.tabPage_Trainer.Controls.Add(this.checkBox_AmmoLock);
            this.tabPage_Trainer.Controls.Add(this.btn_CivilianClear);
            this.tabPage_Trainer.Controls.Add(this.btn_UnlockLevel);
            this.tabPage_Trainer.Controls.Add(this.btn_Kills);
            this.tabPage_Trainer.Controls.Add(this.btn_Score);
            this.tabPage_Trainer.Controls.Add(this.btn_Life);
            this.tabPage_Trainer.Controls.Add(this.pictureBox_Cover);
            this.tabPage_Trainer.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Trainer.Name = "tabPage_Trainer";
            this.tabPage_Trainer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Trainer.Size = new System.Drawing.Size(998, 474);
            this.tabPage_Trainer.TabIndex = 0;
            this.tabPage_Trainer.Text = "游戏修改";
            // 
            // checkBox_WindowMode
            // 
            this.checkBox_WindowMode.AutoSize = true;
            this.checkBox_WindowMode.Location = new System.Drawing.Point(747, 443);
            this.checkBox_WindowMode.Name = "checkBox_WindowMode";
            this.checkBox_WindowMode.Size = new System.Drawing.Size(89, 19);
            this.checkBox_WindowMode.TabIndex = 28;
            this.checkBox_WindowMode.Text = "窗口模式";
            this.checkBox_WindowMode.UseVisualStyleBackColor = true;
            // 
            // label_Key2
            // 
            this.label_Key2.AutoSize = true;
            this.label_Key2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key2.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key2.Location = new System.Drawing.Point(541, 83);
            this.label_Key2.Name = "label_Key2";
            this.label_Key2.Size = new System.Drawing.Size(97, 15);
            this.label_Key2.TabIndex = 27;
            this.label_Key2.Text = "Shift + F2";
            this.label_Key2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_HaveMaxim
            // 
            this.checkBox_HaveMaxim.AutoSize = true;
            this.checkBox_HaveMaxim.Location = new System.Drawing.Point(380, 81);
            this.checkBox_HaveMaxim.Name = "checkBox_HaveMaxim";
            this.checkBox_HaveMaxim.Size = new System.Drawing.Size(149, 19);
            this.checkBox_HaveMaxim.TabIndex = 26;
            this.checkBox_HaveMaxim.Text = "给我马克沁重机枪";
            this.checkBox_HaveMaxim.UseVisualStyleBackColor = true;
            this.checkBox_HaveMaxim.CheckedChanged += new System.EventHandler(this.checkBox_HaveMaxim_CheckedChanged);
            // 
            // label_Tips
            // 
            this.label_Tips.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Tips.ForeColor = System.Drawing.Color.Red;
            this.label_Tips.Location = new System.Drawing.Point(541, 378);
            this.label_Tips.Name = "label_Tips";
            this.label_Tips.Size = new System.Drawing.Size(323, 41);
            this.label_Tips.TabIndex = 25;
            this.label_Tips.Text = "请注意，弹夹和马克沁锁定后，若重开新局则须先解锁再锁定才能生效！";
            this.label_Tips.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_UnlockLevelOK
            // 
            this.label_UnlockLevelOK.AutoSize = true;
            this.label_UnlockLevelOK.Location = new System.Drawing.Point(541, 349);
            this.label_UnlockLevelOK.Name = "label_UnlockLevelOK";
            this.label_UnlockLevelOK.Size = new System.Drawing.Size(55, 15);
            this.label_UnlockLevelOK.TabIndex = 24;
            this.label_UnlockLevelOK.Text = "label2";
            // 
            // checkBox_NoPCK
            // 
            this.checkBox_NoPCK.AutoSize = true;
            this.checkBox_NoPCK.Location = new System.Drawing.Point(842, 443);
            this.checkBox_NoPCK.Name = "checkBox_NoPCK";
            this.checkBox_NoPCK.Size = new System.Drawing.Size(143, 19);
            this.checkBox_NoPCK.TabIndex = 23;
            this.checkBox_NoPCK.Text = "PCK文件已被解包";
            this.checkBox_NoPCK.UseVisualStyleBackColor = true;
            // 
            // label_RunGame
            // 
            this.label_RunGame.AutoSize = true;
            this.label_RunGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_RunGame.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_RunGame.ForeColor = System.Drawing.Color.Red;
            this.label_RunGame.Location = new System.Drawing.Point(664, 445);
            this.label_RunGame.Name = "label_RunGame";
            this.label_RunGame.Size = new System.Drawing.Size(71, 15);
            this.label_RunGame.TabIndex = 22;
            this.label_RunGame.Text = "启动游戏";
            this.label_RunGame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_RunGame.Click += new System.EventHandler(this.Label_RunGame_Click);
            // 
            // label_GamePath
            // 
            this.label_GamePath.AutoSize = true;
            this.label_GamePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_GamePath.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_GamePath.ForeColor = System.Drawing.Color.Blue;
            this.label_GamePath.Location = new System.Drawing.Point(377, 399);
            this.label_GamePath.Name = "label_GamePath";
            this.label_GamePath.Size = new System.Drawing.Size(103, 15);
            this.label_GamePath.TabIndex = 21;
            this.label_GamePath.Text = "设置游戏路径";
            this.label_GamePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_GamePath.Click += new System.EventHandler(this.Label_GamePath_Click);
            // 
            // label_Item
            // 
            this.label_Item.AutoSize = true;
            this.label_Item.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Item.ForeColor = System.Drawing.Color.Blue;
            this.label_Item.Location = new System.Drawing.Point(380, 17);
            this.label_Item.Name = "label_Item";
            this.label_Item.Size = new System.Drawing.Size(39, 15);
            this.label_Item.TabIndex = 20;
            this.label_Item.Text = "功能";
            this.label_Item.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key0
            // 
            this.label_Key0.AutoSize = true;
            this.label_Key0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key0.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key0.Location = new System.Drawing.Point(541, 17);
            this.label_Key0.Name = "label_Key0";
            this.label_Key0.Size = new System.Drawing.Size(87, 15);
            this.label_Key0.TabIndex = 19;
            this.label_Key0.Text = "对应快捷键";
            this.label_Key0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key7
            // 
            this.label_Key7.AutoSize = true;
            this.label_Key7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key7.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key7.Location = new System.Drawing.Point(541, 302);
            this.label_Key7.Name = "label_Key7";
            this.label_Key7.Size = new System.Drawing.Size(97, 15);
            this.label_Key7.TabIndex = 18;
            this.label_Key7.Text = "Shift + F7";
            this.label_Key7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key6
            // 
            this.label_Key6.AutoSize = true;
            this.label_Key6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key6.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key6.Location = new System.Drawing.Point(541, 255);
            this.label_Key6.Name = "label_Key6";
            this.label_Key6.Size = new System.Drawing.Size(97, 15);
            this.label_Key6.TabIndex = 16;
            this.label_Key6.Text = "Shift + F6";
            this.label_Key6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key5
            // 
            this.label_Key5.AutoSize = true;
            this.label_Key5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key5.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key5.Location = new System.Drawing.Point(541, 208);
            this.label_Key5.Name = "label_Key5";
            this.label_Key5.Size = new System.Drawing.Size(97, 15);
            this.label_Key5.TabIndex = 15;
            this.label_Key5.Text = "Shift + F5";
            this.label_Key5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key4
            // 
            this.label_Key4.AutoSize = true;
            this.label_Key4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key4.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key4.Location = new System.Drawing.Point(541, 161);
            this.label_Key4.Name = "label_Key4";
            this.label_Key4.Size = new System.Drawing.Size(97, 15);
            this.label_Key4.TabIndex = 14;
            this.label_Key4.Text = "Shift + F4";
            this.label_Key4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key3
            // 
            this.label_Key3.AutoSize = true;
            this.label_Key3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key3.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key3.Location = new System.Drawing.Point(541, 119);
            this.label_Key3.Name = "label_Key3";
            this.label_Key3.Size = new System.Drawing.Size(97, 15);
            this.label_Key3.TabIndex = 13;
            this.label_Key3.Text = "Shift + F3";
            this.label_Key3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Key1
            // 
            this.label_Key1.AutoSize = true;
            this.label_Key1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Key1.ForeColor = System.Drawing.Color.Fuchsia;
            this.label_Key1.Location = new System.Drawing.Point(541, 47);
            this.label_Key1.Name = "label_Key1";
            this.label_Key1.Size = new System.Drawing.Size(97, 15);
            this.label_Key1.TabIndex = 12;
            this.label_Key1.Text = "Shift + F1";
            this.label_Key1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_Allweapon
            // 
            this.checkBox_Allweapon.AutoSize = true;
            this.checkBox_Allweapon.Location = new System.Drawing.Point(380, 45);
            this.checkBox_Allweapon.Name = "checkBox_Allweapon";
            this.checkBox_Allweapon.Size = new System.Drawing.Size(119, 19);
            this.checkBox_Allweapon.TabIndex = 9;
            this.checkBox_Allweapon.Text = "拥有所有武器";
            this.checkBox_Allweapon.UseVisualStyleBackColor = true;
            this.checkBox_Allweapon.CheckedChanged += new System.EventHandler(this.CheckBox_Allweapon_CheckedChanged);
            // 
            // label_ProcessCheck
            // 
            this.label_ProcessCheck.AutoSize = true;
            this.label_ProcessCheck.Location = new System.Drawing.Point(377, 445);
            this.label_ProcessCheck.Name = "label_ProcessCheck";
            this.label_ProcessCheck.Size = new System.Drawing.Size(55, 15);
            this.label_ProcessCheck.TabIndex = 8;
            this.label_ProcessCheck.Text = "label1";
            this.label_ProcessCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_AmmoLock
            // 
            this.checkBox_AmmoLock.AutoSize = true;
            this.checkBox_AmmoLock.Location = new System.Drawing.Point(380, 117);
            this.checkBox_AmmoLock.Name = "checkBox_AmmoLock";
            this.checkBox_AmmoLock.Size = new System.Drawing.Size(134, 19);
            this.checkBox_AmmoLock.TabIndex = 7;
            this.checkBox_AmmoLock.Text = "永远不用换弹夹";
            this.checkBox_AmmoLock.UseVisualStyleBackColor = true;
            this.checkBox_AmmoLock.CheckedChanged += new System.EventHandler(this.CheckBox_AmmoLock_CheckedChanged);
            // 
            // btn_CivilianClear
            // 
            this.btn_CivilianClear.Location = new System.Drawing.Point(380, 294);
            this.btn_CivilianClear.Name = "btn_CivilianClear";
            this.btn_CivilianClear.Size = new System.Drawing.Size(150, 30);
            this.btn_CivilianClear.TabIndex = 5;
            this.btn_CivilianClear.Text = "清空误伤平民数";
            this.btn_CivilianClear.UseVisualStyleBackColor = true;
            this.btn_CivilianClear.Click += new System.EventHandler(this.Btn_CivilianClear_Click);
            // 
            // btn_UnlockLevel
            // 
            this.btn_UnlockLevel.Location = new System.Drawing.Point(380, 341);
            this.btn_UnlockLevel.Name = "btn_UnlockLevel";
            this.btn_UnlockLevel.Size = new System.Drawing.Size(150, 30);
            this.btn_UnlockLevel.TabIndex = 4;
            this.btn_UnlockLevel.Text = "解锁所有关卡";
            this.btn_UnlockLevel.UseVisualStyleBackColor = true;
            this.btn_UnlockLevel.Click += new System.EventHandler(this.Btn_UnlockLevel_Click);
            // 
            // btn_Kills
            // 
            this.btn_Kills.Location = new System.Drawing.Point(380, 247);
            this.btn_Kills.Name = "btn_Kills";
            this.btn_Kills.Size = new System.Drawing.Size(150, 30);
            this.btn_Kills.TabIndex = 3;
            this.btn_Kills.Text = "给我加100杀敌";
            this.btn_Kills.UseVisualStyleBackColor = true;
            this.btn_Kills.Click += new System.EventHandler(this.Btn_Kills_Click);
            // 
            // btn_Score
            // 
            this.btn_Score.Location = new System.Drawing.Point(380, 200);
            this.btn_Score.Name = "btn_Score";
            this.btn_Score.Size = new System.Drawing.Size(150, 30);
            this.btn_Score.TabIndex = 2;
            this.btn_Score.Text = "给我加1000分";
            this.btn_Score.UseVisualStyleBackColor = true;
            this.btn_Score.Click += new System.EventHandler(this.Btn_Score_Click);
            // 
            // btn_Life
            // 
            this.btn_Life.Location = new System.Drawing.Point(380, 153);
            this.btn_Life.Name = "btn_Life";
            this.btn_Life.Size = new System.Drawing.Size(150, 30);
            this.btn_Life.TabIndex = 1;
            this.btn_Life.Text = "给我九条命";
            this.btn_Life.UseVisualStyleBackColor = true;
            this.btn_Life.Click += new System.EventHandler(this.Btn_Life_Click);
            // 
            // pictureBox_Cover
            // 
            this.pictureBox_Cover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Cover.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Cover.Image")));
            this.pictureBox_Cover.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Cover.Name = "pictureBox_Cover";
            this.pictureBox_Cover.Size = new System.Drawing.Size(356, 466);
            this.pictureBox_Cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Cover.TabIndex = 0;
            this.pictureBox_Cover.TabStop = false;
            // 
            // tabPage_INIEncryption
            // 
            this.tabPage_INIEncryption.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_INIEncryption.Controls.Add(this.btn_INIEncrypt);
            this.tabPage_INIEncryption.Controls.Add(this.btn_INIDecrypt);
            this.tabPage_INIEncryption.Controls.Add(this.btn_INIOpen);
            this.tabPage_INIEncryption.Controls.Add(this.textBox_INIPath);
            this.tabPage_INIEncryption.Controls.Add(this.label1);
            this.tabPage_INIEncryption.Location = new System.Drawing.Point(4, 25);
            this.tabPage_INIEncryption.Name = "tabPage_INIEncryption";
            this.tabPage_INIEncryption.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_INIEncryption.Size = new System.Drawing.Size(998, 474);
            this.tabPage_INIEncryption.TabIndex = 1;
            this.tabPage_INIEncryption.Text = "INI加解密";
            // 
            // btn_INIEncrypt
            // 
            this.btn_INIEncrypt.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_INIEncrypt.Location = new System.Drawing.Point(283, 168);
            this.btn_INIEncrypt.Name = "btn_INIEncrypt";
            this.btn_INIEncrypt.Size = new System.Drawing.Size(150, 100);
            this.btn_INIEncrypt.TabIndex = 5;
            this.btn_INIEncrypt.Text = "加密INI";
            this.btn_INIEncrypt.UseVisualStyleBackColor = true;
            this.btn_INIEncrypt.Click += new System.EventHandler(this.Btn_INIEncrypt_Click);
            // 
            // btn_INIDecrypt
            // 
            this.btn_INIDecrypt.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_INIDecrypt.Location = new System.Drawing.Point(94, 168);
            this.btn_INIDecrypt.Name = "btn_INIDecrypt";
            this.btn_INIDecrypt.Size = new System.Drawing.Size(150, 100);
            this.btn_INIDecrypt.TabIndex = 4;
            this.btn_INIDecrypt.Text = "解密INI";
            this.btn_INIDecrypt.UseVisualStyleBackColor = true;
            this.btn_INIDecrypt.Click += new System.EventHandler(this.Btn_INIDecrypt_Click);
            // 
            // btn_INIOpen
            // 
            this.btn_INIOpen.Location = new System.Drawing.Point(426, 95);
            this.btn_INIOpen.Name = "btn_INIOpen";
            this.btn_INIOpen.Size = new System.Drawing.Size(100, 30);
            this.btn_INIOpen.TabIndex = 3;
            this.btn_INIOpen.Text = "打开…(&O)";
            this.btn_INIOpen.UseVisualStyleBackColor = true;
            this.btn_INIOpen.Click += new System.EventHandler(this.Btn_INIOpen_Click);
            // 
            // textBox_INIPath
            // 
            this.textBox_INIPath.Location = new System.Drawing.Point(69, 95);
            this.textBox_INIPath.Name = "textBox_INIPath";
            this.textBox_INIPath.Size = new System.Drawing.Size(342, 25);
            this.textBox_INIPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择要加密/解密的文件：";
            // 
            // tabPage_UNPCK
            // 
            this.tabPage_UNPCK.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_UNPCK.Controls.Add(this.btn_PCKPackTips);
            this.tabPage_UNPCK.Controls.Add(this.groupBox_PCKSetting);
            this.tabPage_UNPCK.Controls.Add(this.btn_PCKExecute);
            this.tabPage_UNPCK.Controls.Add(this.label_PCKPercent);
            this.tabPage_UNPCK.Controls.Add(this.label_PCKState);
            this.tabPage_UNPCK.Controls.Add(this.progressBar_PCK);
            this.tabPage_UNPCK.Controls.Add(this.btn_PCKView);
            this.tabPage_UNPCK.Controls.Add(this.btn_IOPatch);
            this.tabPage_UNPCK.Controls.Add(this.btn_PCKPath);
            this.tabPage_UNPCK.Controls.Add(this.label_IOPath);
            this.tabPage_UNPCK.Controls.Add(this.label_PCKPath);
            this.tabPage_UNPCK.Controls.Add(this.textBox_IOpatch);
            this.tabPage_UNPCK.Controls.Add(this.textBox_PCKPath);
            this.tabPage_UNPCK.Controls.Add(this.radio_UnPackMode);
            this.tabPage_UNPCK.Controls.Add(this.radio_PackMode);
            this.tabPage_UNPCK.Location = new System.Drawing.Point(4, 25);
            this.tabPage_UNPCK.Name = "tabPage_UNPCK";
            this.tabPage_UNPCK.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_UNPCK.Size = new System.Drawing.Size(998, 474);
            this.tabPage_UNPCK.TabIndex = 2;
            this.tabPage_UNPCK.Text = "PCK打包解包";
            // 
            // btn_PCKPackTips
            // 
            this.btn_PCKPackTips.Location = new System.Drawing.Point(881, 162);
            this.btn_PCKPackTips.Name = "btn_PCKPackTips";
            this.btn_PCKPackTips.Size = new System.Drawing.Size(100, 30);
            this.btn_PCKPackTips.TabIndex = 14;
            this.btn_PCKPackTips.Text = "打包须知";
            this.btn_PCKPackTips.UseVisualStyleBackColor = true;
            this.btn_PCKPackTips.Click += new System.EventHandler(this.Btn_PCKPackTips_Click);
            // 
            // groupBox_PCKSetting
            // 
            this.groupBox_PCKSetting.Controls.Add(this.label_AreZlib);
            this.groupBox_PCKSetting.Controls.Add(this.label_Recommended);
            this.groupBox_PCKSetting.Controls.Add(this.radio_AreCompress_No);
            this.groupBox_PCKSetting.Controls.Add(this.radio_AreCompress_Yes);
            this.groupBox_PCKSetting.Controls.Add(this.numericUpDown_compressLevel);
            this.groupBox_PCKSetting.Location = new System.Drawing.Point(27, 162);
            this.groupBox_PCKSetting.Name = "groupBox_PCKSetting";
            this.groupBox_PCKSetting.Size = new System.Drawing.Size(846, 150);
            this.groupBox_PCKSetting.TabIndex = 13;
            this.groupBox_PCKSetting.TabStop = false;
            this.groupBox_PCKSetting.Text = "配置PCK打包参数";
            // 
            // label_AreZlib
            // 
            this.label_AreZlib.AutoSize = true;
            this.label_AreZlib.Location = new System.Drawing.Point(6, 31);
            this.label_AreZlib.Name = "label_AreZlib";
            this.label_AreZlib.Size = new System.Drawing.Size(144, 15);
            this.label_AreZlib.TabIndex = 17;
            this.label_AreZlib.Text = "是否启用Zlib压缩？";
            // 
            // label_Recommended
            // 
            this.label_Recommended.AutoSize = true;
            this.label_Recommended.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Recommended.ForeColor = System.Drawing.Color.Blue;
            this.label_Recommended.Location = new System.Drawing.Point(6, 70);
            this.label_Recommended.Name = "label_Recommended";
            this.label_Recommended.Size = new System.Drawing.Size(57, 15);
            this.label_Recommended.TabIndex = 16;
            this.label_Recommended.Text = "(推荐)";
            // 
            // radio_AreCompress_No
            // 
            this.radio_AreCompress_No.Location = new System.Drawing.Point(69, 102);
            this.radio_AreCompress_No.Name = "radio_AreCompress_No";
            this.radio_AreCompress_No.Size = new System.Drawing.Size(565, 42);
            this.radio_AreCompress_No.TabIndex = 15;
            this.radio_AreCompress_No.Text = "否，不压缩直接打包。(被打包的原始数据会被拼合在一起形成一个PCK文件)";
            this.radio_AreCompress_No.UseVisualStyleBackColor = true;
            this.radio_AreCompress_No.CheckedChanged += new System.EventHandler(this.Radio_AreCompress_No_CheckedChanged);
            // 
            // radio_AreCompress_Yes
            // 
            this.radio_AreCompress_Yes.Checked = true;
            this.radio_AreCompress_Yes.Location = new System.Drawing.Point(69, 58);
            this.radio_AreCompress_Yes.Name = "radio_AreCompress_Yes";
            this.radio_AreCompress_Yes.Size = new System.Drawing.Size(471, 38);
            this.radio_AreCompress_Yes.TabIndex = 14;
            this.radio_AreCompress_Yes.TabStop = true;
            this.radio_AreCompress_Yes.Text = "是，启用Zlib压缩。(请设置压缩等级，范围0～9)";
            this.radio_AreCompress_Yes.UseVisualStyleBackColor = true;
            this.radio_AreCompress_Yes.CheckedChanged += new System.EventHandler(this.Radio_AreCompress_Yes_CheckedChanged);
            // 
            // numericUpDown_compressLevel
            // 
            this.numericUpDown_compressLevel.Location = new System.Drawing.Point(546, 65);
            this.numericUpDown_compressLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown_compressLevel.Name = "numericUpDown_compressLevel";
            this.numericUpDown_compressLevel.Size = new System.Drawing.Size(66, 25);
            this.numericUpDown_compressLevel.TabIndex = 13;
            this.numericUpDown_compressLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_PCKExecute
            // 
            this.btn_PCKExecute.Location = new System.Drawing.Point(881, 436);
            this.btn_PCKExecute.Name = "btn_PCKExecute";
            this.btn_PCKExecute.Size = new System.Drawing.Size(100, 30);
            this.btn_PCKExecute.TabIndex = 12;
            this.btn_PCKExecute.Text = "button3";
            this.btn_PCKExecute.UseVisualStyleBackColor = true;
            this.btn_PCKExecute.Click += new System.EventHandler(this.Btn_PCKExecute_Click);
            // 
            // label_PCKPercent
            // 
            this.label_PCKPercent.AutoSize = true;
            this.label_PCKPercent.Location = new System.Drawing.Point(784, 444);
            this.label_PCKPercent.Name = "label_PCKPercent";
            this.label_PCKPercent.Size = new System.Drawing.Size(55, 15);
            this.label_PCKPercent.TabIndex = 11;
            this.label_PCKPercent.Text = "label3";
            // 
            // label_PCKState
            // 
            this.label_PCKState.Location = new System.Drawing.Point(27, 400);
            this.label_PCKState.Name = "label_PCKState";
            this.label_PCKState.Size = new System.Drawing.Size(857, 30);
            this.label_PCKState.TabIndex = 10;
            this.label_PCKState.Text = "label3";
            // 
            // progressBar_PCK
            // 
            this.progressBar_PCK.Location = new System.Drawing.Point(27, 440);
            this.progressBar_PCK.Name = "progressBar_PCK";
            this.progressBar_PCK.Size = new System.Drawing.Size(751, 23);
            this.progressBar_PCK.TabIndex = 9;
            // 
            // btn_PCKView
            // 
            this.btn_PCKView.Location = new System.Drawing.Point(881, 400);
            this.btn_PCKView.Name = "btn_PCKView";
            this.btn_PCKView.Size = new System.Drawing.Size(100, 30);
            this.btn_PCKView.TabIndex = 8;
            this.btn_PCKView.Text = "查看PCK";
            this.btn_PCKView.UseVisualStyleBackColor = true;
            this.btn_PCKView.Click += new System.EventHandler(this.btn_PCKView_Click);
            // 
            // btn_IOPatch
            // 
            this.btn_IOPatch.Location = new System.Drawing.Point(881, 106);
            this.btn_IOPatch.Name = "btn_IOPatch";
            this.btn_IOPatch.Size = new System.Drawing.Size(100, 30);
            this.btn_IOPatch.TabIndex = 7;
            this.btn_IOPatch.Text = "button2";
            this.btn_IOPatch.UseVisualStyleBackColor = true;
            this.btn_IOPatch.Click += new System.EventHandler(this.Btn_IOPatch_Click);
            // 
            // btn_PCKPath
            // 
            this.btn_PCKPath.Location = new System.Drawing.Point(881, 42);
            this.btn_PCKPath.Name = "btn_PCKPath";
            this.btn_PCKPath.Size = new System.Drawing.Size(100, 30);
            this.btn_PCKPath.TabIndex = 6;
            this.btn_PCKPath.Text = "button1";
            this.btn_PCKPath.UseVisualStyleBackColor = true;
            this.btn_PCKPath.Click += new System.EventHandler(this.Btn_PCKPath_Click);
            // 
            // label_IOPath
            // 
            this.label_IOPath.AutoSize = true;
            this.label_IOPath.Location = new System.Drawing.Point(141, 81);
            this.label_IOPath.Name = "label_IOPath";
            this.label_IOPath.Size = new System.Drawing.Size(55, 15);
            this.label_IOPath.TabIndex = 5;
            this.label_IOPath.Text = "label2";
            // 
            // label_PCKPath
            // 
            this.label_PCKPath.AutoSize = true;
            this.label_PCKPath.Location = new System.Drawing.Point(141, 19);
            this.label_PCKPath.Name = "label_PCKPath";
            this.label_PCKPath.Size = new System.Drawing.Size(55, 15);
            this.label_PCKPath.TabIndex = 4;
            this.label_PCKPath.Text = "label1";
            // 
            // textBox_IOpatch
            // 
            this.textBox_IOpatch.Location = new System.Drawing.Point(141, 109);
            this.textBox_IOpatch.Name = "textBox_IOpatch";
            this.textBox_IOpatch.Size = new System.Drawing.Size(732, 25);
            this.textBox_IOpatch.TabIndex = 3;
            // 
            // textBox_PCKPath
            // 
            this.textBox_PCKPath.Location = new System.Drawing.Point(141, 45);
            this.textBox_PCKPath.Name = "textBox_PCKPath";
            this.textBox_PCKPath.Size = new System.Drawing.Size(732, 25);
            this.textBox_PCKPath.TabIndex = 2;
            // 
            // radio_UnPackMode
            // 
            this.radio_UnPackMode.AutoSize = true;
            this.radio_UnPackMode.Location = new System.Drawing.Point(32, 112);
            this.radio_UnPackMode.Name = "radio_UnPackMode";
            this.radio_UnPackMode.Size = new System.Drawing.Size(88, 19);
            this.radio_UnPackMode.TabIndex = 1;
            this.radio_UnPackMode.Text = "解包模式";
            this.radio_UnPackMode.UseVisualStyleBackColor = true;
            this.radio_UnPackMode.CheckedChanged += new System.EventHandler(this.Radio_UnPackMode_CheckedChanged);
            // 
            // radio_PackMode
            // 
            this.radio_PackMode.AutoSize = true;
            this.radio_PackMode.Checked = true;
            this.radio_PackMode.Location = new System.Drawing.Point(32, 48);
            this.radio_PackMode.Name = "radio_PackMode";
            this.radio_PackMode.Size = new System.Drawing.Size(88, 19);
            this.radio_PackMode.TabIndex = 0;
            this.radio_PackMode.TabStop = true;
            this.radio_PackMode.Text = "打包模式";
            this.radio_PackMode.UseVisualStyleBackColor = true;
            this.radio_PackMode.CheckedChanged += new System.EventHandler(this.Radio_PackMode_CheckedChanged);
            this.radio_PackMode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Radio_PackMode_MouseUp);
            // 
            // tabPage_Description
            // 
            this.tabPage_Description.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Description.Controls.Add(this.textBox_Description);
            this.tabPage_Description.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Description.Name = "tabPage_Description";
            this.tabPage_Description.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Description.Size = new System.Drawing.Size(998, 474);
            this.tabPage_Description.TabIndex = 3;
            this.tabPage_Description.Text = "说明";
            // 
            // textBox_Description
            // 
            this.textBox_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Description.Font = new System.Drawing.Font("宋体", 16F);
            this.textBox_Description.Location = new System.Drawing.Point(3, 3);
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ReadOnly = true;
            this.textBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Description.Size = new System.Drawing.Size(992, 468);
            this.textBox_Description.TabIndex = 0;
            // 
            // tabPage_About
            // 
            this.tabPage_About.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_About.Controls.Add(this.label_Website);
            this.tabPage_About.Controls.Add(this.label_WelcomeTo52pojie);
            this.tabPage_About.Controls.Add(this.label_About_Title);
            this.tabPage_About.Controls.Add(this.pictureBox_52pojieLogo);
            this.tabPage_About.Location = new System.Drawing.Point(4, 25);
            this.tabPage_About.Name = "tabPage_About";
            this.tabPage_About.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_About.Size = new System.Drawing.Size(998, 474);
            this.tabPage_About.TabIndex = 4;
            this.tabPage_About.Text = "关于修改器";
            // 
            // pictureBox_52pojieLogo
            // 
            this.pictureBox_52pojieLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_52pojieLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_52pojieLogo.Image")));
            this.pictureBox_52pojieLogo.Location = new System.Drawing.Point(869, 388);
            this.pictureBox_52pojieLogo.Name = "pictureBox_52pojieLogo";
            this.pictureBox_52pojieLogo.Size = new System.Drawing.Size(121, 75);
            this.pictureBox_52pojieLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_52pojieLogo.TabIndex = 0;
            this.pictureBox_52pojieLogo.TabStop = false;
            this.pictureBox_52pojieLogo.Click += new System.EventHandler(this.pictureBox_52pojieLogo_Click);
            // 
            // label_About_Title
            // 
            this.label_About_Title.AutoSize = true;
            this.label_About_Title.Font = new System.Drawing.Font("宋体", 12F);
            this.label_About_Title.Location = new System.Drawing.Point(74, 46);
            this.label_About_Title.Name = "label_About_Title";
            this.label_About_Title.Size = new System.Drawing.Size(69, 20);
            this.label_About_Title.TabIndex = 1;
            this.label_About_Title.Text = "label1";
            // 
            // label_WelcomeTo52pojie
            // 
            this.label_WelcomeTo52pojie.AutoSize = true;
            this.label_WelcomeTo52pojie.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_WelcomeTo52pojie.Location = new System.Drawing.Point(385, 428);
            this.label_WelcomeTo52pojie.Name = "label_WelcomeTo52pojie";
            this.label_WelcomeTo52pojie.Size = new System.Drawing.Size(149, 20);
            this.label_WelcomeTo52pojie.TabIndex = 2;
            this.label_WelcomeTo52pojie.Text = "吾爱破解欢迎您";
            // 
            // label_Website
            // 
            this.label_Website.AutoSize = true;
            this.label_Website.BackColor = System.Drawing.SystemColors.Control;
            this.label_Website.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Website.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Website.ForeColor = System.Drawing.Color.Blue;
            this.label_Website.Location = new System.Drawing.Point(555, 428);
            this.label_Website.Name = "label_Website";
            this.label_Website.Size = new System.Drawing.Size(75, 20);
            this.label_Website.TabIndex = 3;
            this.label_Website.Text = "label2";
            this.label_Website.Click += new System.EventHandler(this.label_Website_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 503);
            this.Controls.Add(this.tabControl_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "《血战上海滩》八项属性修改器";
            this.tabControl_Main.ResumeLayout(false);
            this.tabPage_Trainer.ResumeLayout(false);
            this.tabPage_Trainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Cover)).EndInit();
            this.tabPage_INIEncryption.ResumeLayout(false);
            this.tabPage_INIEncryption.PerformLayout();
            this.tabPage_UNPCK.ResumeLayout(false);
            this.tabPage_UNPCK.PerformLayout();
            this.groupBox_PCKSetting.ResumeLayout(false);
            this.groupBox_PCKSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_compressLevel)).EndInit();
            this.tabPage_Description.ResumeLayout(false);
            this.tabPage_Description.PerformLayout();
            this.tabPage_About.ResumeLayout(false);
            this.tabPage_About.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_52pojieLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Main;
        private System.Windows.Forms.TabPage tabPage_INIEncryption;
        private System.Windows.Forms.TabPage tabPage_UNPCK;
        private System.Windows.Forms.TabPage tabPage_Description;
        private System.Windows.Forms.TabPage tabPage_About;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage_Trainer;
        private System.Windows.Forms.CheckBox checkBox_NoPCK;
        private System.Windows.Forms.Label label_RunGame;
        private System.Windows.Forms.Label label_GamePath;
        private System.Windows.Forms.Label label_Item;
        private System.Windows.Forms.Label label_Key0;
        private System.Windows.Forms.Label label_Key7;
        private System.Windows.Forms.Label label_Key6;
        private System.Windows.Forms.Label label_Key5;
        private System.Windows.Forms.Label label_Key4;
        private System.Windows.Forms.Label label_Key3;
        private System.Windows.Forms.Label label_Key1;
        private System.Windows.Forms.CheckBox checkBox_Allweapon;
        private System.Windows.Forms.Label label_ProcessCheck;
        private System.Windows.Forms.CheckBox checkBox_AmmoLock;
        private System.Windows.Forms.Button btn_CivilianClear;
        private System.Windows.Forms.Button btn_UnlockLevel;
        private System.Windows.Forms.Button btn_Kills;
        private System.Windows.Forms.Button btn_Score;
        private System.Windows.Forms.Button btn_Life;
        private System.Windows.Forms.PictureBox pictureBox_Cover;
        private System.Windows.Forms.Label label_UnlockLevelOK;
        private System.Windows.Forms.TextBox textBox_INIPath;
        private System.Windows.Forms.Button btn_INIOpen;
        private System.Windows.Forms.Button btn_INIDecrypt;
        private System.Windows.Forms.Button btn_INIEncrypt;
        private System.Windows.Forms.Label label_IOPath;
        private System.Windows.Forms.Label label_PCKPath;
        private System.Windows.Forms.TextBox textBox_IOpatch;
        private System.Windows.Forms.TextBox textBox_PCKPath;
        private System.Windows.Forms.RadioButton radio_UnPackMode;
        private System.Windows.Forms.RadioButton radio_PackMode;
        private System.Windows.Forms.Label label_PCKPercent;
        private System.Windows.Forms.Label label_PCKState;
        private System.Windows.Forms.ProgressBar progressBar_PCK;
        private System.Windows.Forms.Button btn_PCKView;
        private System.Windows.Forms.Button btn_IOPatch;
        private System.Windows.Forms.Button btn_PCKPath;
        private System.Windows.Forms.Button btn_PCKExecute;
        private System.Windows.Forms.GroupBox groupBox_PCKSetting;
        private System.Windows.Forms.Label label_Recommended;
        private System.Windows.Forms.RadioButton radio_AreCompress_No;
        private System.Windows.Forms.RadioButton radio_AreCompress_Yes;
        private System.Windows.Forms.NumericUpDown numericUpDown_compressLevel;
        private System.Windows.Forms.Label label_Tips;
        private System.Windows.Forms.Button btn_PCKPackTips;
        private System.Windows.Forms.Label label_AreZlib;
        private System.Windows.Forms.Label label_Key2;
        private System.Windows.Forms.CheckBox checkBox_HaveMaxim;
        private System.Windows.Forms.CheckBox checkBox_WindowMode;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.PictureBox pictureBox_52pojieLogo;
        private System.Windows.Forms.Label label_About_Title;
        private System.Windows.Forms.Label label_Website;
        private System.Windows.Forms.Label label_WelcomeTo52pojie;
    }
}

