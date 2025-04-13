using System;
using System.IO;
using System.Windows.Forms;

namespace ShanghaiTrainer
{
    public partial class Form_GamePath : Form
    {
        #region 游戏路径选择对话框
        // 初始化游戏路径选择对话框
        private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        private void InitializeFolderBrowserDialog()
        {
            folderBrowserDialog.Description = "请选择游戏路径";      // 设置对话框标题
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); // 设置初始目录为桌面
        }
        #endregion

        #region 主窗口方法
        public Form_GamePath()
        {
            InitializeComponent();
            InitializeFolderBrowserDialog();
            textBox_GamePath.Text = GetGamePath();
            textBox_GamePath.SelectionStart = textBox_GamePath.Text.Length;
            textBox_GamePath.SelectionLength = 0;
            textBox_GamePath.Focus();

        }
        #endregion

        #region 游戏路径选择核心功能
       
        /// <summary>
        /// 取游戏路径
        /// </summary>
        /// <returns></returns>
        public string GetGamePath()
        {
            // 初始化INI实例
            var gamePath = new IniHelper();
            
            // 取修改器配置文件路径
            string path;
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configDir = Path.Combine(appDataPath, "52pojie", "ShanghaiTrainer");
            string configPath = Path.Combine(configDir, "Config.ini");
           
            // 读INI
            gamePath.LoadFromFile(configPath);
            try
            {
                //读Config节的GamePath值
                path = gamePath.Read("Config", "GamePath");
                return path;
            }
            catch
            {
                // 若出现问题则写一个空白INI
                gamePath.Write("Config", "GamePath", string.Empty);
                gamePath.SaveToFile(configPath);
                return null;
            }
        }
        #endregion

        #region 各控件事件响应函数
       
        /// <summary>
        /// 选择游戏目录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GamePath_Open_Click(object sender, EventArgs e)
        {
            // 取游戏路径选择对话框所选路径
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) 
            {
                textBox_GamePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            //取游戏目录
            string gamePath = textBox_GamePath.Text;
            
            // 指定游戏EXE文件名
            string gameApp = "shanghai.exe";

            // 检查游戏EXE文件是否存在
            if (!File.Exists(textBox_GamePath.Text + "\\" + gameApp))
            {
                MessageBox.Show($@"请选择《血战上海滩》的根目录！那个目录下有一个<{gameApp}>", "找不到路径", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                // 初始化INI实例
                var writeGamePath = new IniHelper();
                // 取Windows的ApplicationData目录
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                // 置修改器配置文件的文件夹
                string configDir = Path.Combine(appDataPath, "52pojie", "ShanghaiTrainer");
                string configPath = Path.Combine(configDir, "Config.ini");
                
                // 读INI
                writeGamePath.LoadFromFile(configPath);
                // 将游戏路径写入INI
                writeGamePath.Write("Config", "GamePath", textBox_GamePath.Text);
                // 保存文件
                writeGamePath.SaveToFile(configPath);
                // 关闭窗口
                this.Close();
            }
        }

        // 取消按钮
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }
        #endregion
    }
}
