using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ShanghaiTrainer
{
    public partial class Form_PCKView : Form
    {
        #region 解包单独文件保存对话框
        SaveFileDialog unPackSingleDialog = new SaveFileDialog();

        /// <summary>
        /// 初始化解包单独文件保存对话框
        /// </summary>
        private void InitializeUnPackSingleDialog()
        {
            unPackSingleDialog.Title = "保存到…";
            unPackSingleDialog.Filter = "所有文件|*.*";
        }
        #endregion

        #region 主窗口方法
        public Form_PCKView()
        {
            InitializeComponent();

            // 初始化解包单独文件保存对话框
            InitializeUnPackSingleDialog();

            // 容错处理
            if (GloVar.PCKHelper.pckList.Count == 0 | GloVar.PCKHelper.pckList == null)
            {
                throw new Exception("PCK文件列表不能为空！");
            }

            // 
            LoadPCKList(GloVar.PCKHelper.pckList);


        }
        #endregion

        #region PCK文件浏览框核心功能
        /// <summary>
        /// 读入PCK列表
        /// </summary>
        /// <param name="pckList"></param>
        /// <exception cref="Exception"></exception>
        private void LoadPCKList(List<Tuple<int, string, long, long, long>> pckList)
        {
            try
            {
                // 开始更新列表框
                this.listView_PCKList.BeginUpdate();
                for (int i = 0; i < pckList.Count; i++)
                {
                    ListViewItem pckItemList = new ListViewItem();
                    pckItemList.Text = (i + 1).ToString();                                          // 序号列
                    pckItemList.SubItems.Add(pckList[i].Item2);                                     // 文件名列
                    pckItemList.SubItems.Add("0x" + pckList[i].Item3.ToString("X"));                // 文件偏移列
                    pckItemList.SubItems.Add(PublicFunction.BytesToSize(pckList[i].Item4));         // 文件实际大小列
                    pckItemList.SubItems.Add(PublicFunction.BytesToSize(pckList[i].Item5));         // 文件压缩后大小列
                    this.listView_PCKList.Items.Add(pckItemList);
                }
                this.listView_PCKList.EndUpdate();          // 结束列表框更新

                long globalCompressionSize = 0;             //PCK压缩后大小
                long globalActualSize = 0;                  //PCK压缩前大小
                //计算压缩前后大小
                for (int i = 0; i < pckList.Count; i++)
                {
                    globalActualSize += pckList[i].Item4;
                    globalCompressionSize += pckList[i].Item5;
                }

                // 将文件信息更新到标签控件
                label_PCKInfo.Text = $"文件大小： {PublicFunction.BytesToSize(globalCompressionSize)} ，解包后大小： {PublicFunction.BytesToSize(globalActualSize)} ，共有 {pckList.Count} 个文件";
            }
            catch (Exception ex)     // 当GetPCKInformation抛出异常后要抛给图形界面进行显示
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 解包单独文件
        /// </summary>
        private void UnPackSingle()
        {
            // 文件号默认为-1
            int fileNum = -1;
            try
            {
                // 尝试文件号等于列表框选中项
                fileNum = listView_PCKList.SelectedIndices[0];
            }
            catch (ArgumentOutOfRangeException)
            {
                // 未选中则弹出提示
                MessageBox.Show("请在文件列表中选中要单独解包的文件！", "不好意思", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 置单独解包的文件的默认文件名
            string defaultFileName = Path.GetFileName(GloVar.PCKHelper.pckList[fileNum].Item2);
            unPackSingleDialog.FileName = defaultFileName;

            // 弹出保存对话框
            if (unPackSingleDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 开始解压
                    PCKHelper.UnPack.ExtractFileSingle(GloVar.PCKHelper.targetPCK, GloVar.PCKHelper.pckList, fileNum, unPackSingleDialog.FileName);
                    DialogResult r = MessageBox.Show("解压完成，是否打开解压的文件夹？", "解压完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        string targetPCK = unPackSingleDialog.FileName;
                        PublicFunction.GetOutputFolder(targetPCK);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    {
                        MessageBox.Show($"发生错误，解包失败！\n\n原因:\n   {ex.Message}", "解包出现问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region 各控件事件响应函数

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 解包单独文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UnPackSingle_Click(object sender, EventArgs e)
        {
            UnPackSingle();
        }

        /// <summary>
        /// 列表框双击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_PCKList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 点击解包单独文件按钮
            Btn_UnPackSingle_Click(this, EventArgs.Empty);
        }
        #endregion
    }
}
