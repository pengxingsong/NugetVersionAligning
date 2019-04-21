using Benlai.Tool.Model;
using Benlai.Tool.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Benlai.Tool.WinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static List<PackageInfo> PackageInfoList = new List<PackageInfo>();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region 方法
        private void LoadPackage()
        {
            var slnInfo = SolutionService.Instance.LoadSlnProjectInfo(txtFilePath.Text.Trim());
            slnInfo = SolutionService.Instance.LoadProjectInfo(slnInfo);
            PackageInfoList = SolutionService.Instance.GetPackInfos(slnInfo);
        }

        private DataTable GetPackDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("包名称");
            dt.Columns.Add("版本状态");
            dt.Columns.Add("被引项目数量");
            dt.Columns.Add("版本项目差异量");
            dt.Columns.Add("修复结果");
            dt.Columns.Add("描述");
            if (PackageInfoList == null || PackageInfoList.Count == 0)
                return dt;
            DataRow dr;
            foreach (var item in PackageInfoList)
            {
                dr = dt.NewRow();
                dr["包名称"] = item.Name;
                dr["版本状态"] = item.StatusOK ? "正常" : "异常";
                dr["版本项目差异量"] = "0";
                var c = item.VersionList?.Count - 1;
                if (c > 0)
                    dr["版本项目差异量"] = c.ToString();
                dr["被引项目数量"] = item.ProjList?.Count;
                dr["修复结果"] = item.StatusOK ? "无需修复" : "待修复";
                if (c == 0 && !item.StatusOK)
                {
                    dr["描述"] = "此异常为引用项目中的package与csproj版本不一致";
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }


        public DataTable GetProjDT(string packName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("项目名称");
            dt.Columns.Add("项目Nuget版本差异");
            dt.Columns.Add("package中版本");
            dt.Columns.Add("csproj中版本");
            if (string.IsNullOrWhiteSpace(packName) || PackageInfoList == null || PackageInfoList.Count == 0)
                return dt;
            var packInfo = PackageInfoList.Where(x => x.Name == packName).FirstOrDefault();
            DataRow dr;
            foreach (var item in packInfo.ProjList)
            {
                dr = dt.NewRow();
                dr["项目名称"] = item.ProjName;
                dr["package中版本"] = item.PackageNugetVersion;
                dr["csproj中版本"] = item.CsprojNugetVersion;
                dr["项目Nuget版本差异"] = item.PackageNugetVersion != item.CsprojNugetVersion ? "异常" : "正常";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void BindPackDvg()
        {
            var dt = GetPackDT();
            dgvPackData.DataSource = dt;
            var t = dt.Rows.Count;
            var s = dt.Select("版本状态='正常'").Count();
            lblPackTotal.Text = $"总量:{t},正常量:{s},异常量:{t - s}";
        }
        private void BindProjDvg(string packName)
        {
            var dt = GetProjDT(packName);
            dgvProjData.DataSource = dt;
            var t = dt.Rows.Count;
            var s = dt.Select("项目Nuget版本差异='正常'").Count();
            lblProjTotal.Text = $"总量:{t},正常量:{s},异常量:{t - s}";
            var packInfo = PackageInfoList.Where(x => x.Name == packName).FirstOrDefault();
            if (packInfo != null && packInfo.VersionList != null)
            {
                cbVersionList.Visible = true;
                cbVersionList.DisplayMember = "ShowMsg";
                cbVersionList.ValueMember = "Version";
                cbVersionList.DataSource = packInfo.VersionList;
            }
        }


        private void btnSeleFile_Click(object sender, EventArgs e)
        {
            if (ofdFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofdFileDialog.FileName;
            }
        }




        public void Clear()
        {
            SetMessage("*");
            lblPackname.Text = "*";
            lblPackTotal.Text = "*";
            lblProjTotal.Text = "*";
            dgvPackData.DataSource = null;
            dgvProjData.DataSource = null;
            cbVersionList.Visible = false;
            cbVersionList.DataSource = null;
        }

        private void SetMessage(string msg, bool error = false)
        {
            lblMessage.Text = msg;
            lblMessage.ForeColor = error ? Color.Red : Color.Black;
        }
        #endregion

        #region 事件

        private void btnLoadNuget_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                SetMessage("Nuget包信息正在加载中...", true);
                LoadPackage();
                BindPackDvg();
                SetMessage("加载Nuget包信息完成!");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                SetMessage("加载Nuget包信息异常!" + ex.Message, true);
            }
        }

        private void dgvPackData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPackData.DataSource == null || dgvPackData.Rows.Count == 0) return;
                int index = dgvPackData.CurrentRow.Index;
                var packName = dgvPackData.Rows[index].Cells[0].Value.ToString();
                lblPackname.Text = $"(Nuget包:{packName})";
                lblPackname.ForeColor = Color.Blue;
                BindProjDvg(packName);
                SetMessage("加载被引项目信息完成");
            }
            catch (Exception ex)
            {
                SetMessage("加载被引项目信息异常!" + ex.Message, true);
            }
        }

        private void dgvPackData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvPackData.Rows.Count == 0) return;
            foreach (DataGridViewRow dgRow in dgvPackData.Rows)
            {
                if (dgRow.Cells["版本状态"].Value.ToString() != "正常")
                {
                    dgRow.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    dgRow.Cells["版本状态"].Style.ForeColor = Color.Green;
                }
            }
        }

        private void dgvProjData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvProjData.Rows.Count == 0) return;
            foreach (DataGridViewRow dgRow in dgvProjData.Rows)
            {
                if (dgRow.Cells["项目Nuget版本差异"].Value.ToString() != "正常")
                {
                    dgRow.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    dgRow.Cells["项目Nuget版本差异"].Style.ForeColor = Color.Green;
                }
            }
        }

        //版本对齐事件
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dgvPackData.CurrentRow.Index;
                var packName = dgvPackData.Rows[index].Cells[0].Value.ToString();
                var packInfo = PackageInfoList.Where(x => x.Name == packName).FirstOrDefault();
                if (packInfo == null || packInfo.StatusOK)
                {
                    MessageBox.Show($"包:{packName}无需对齐版本!");
                    return;
                }
                var message = $"即将对包:{packName}进行版本对齐,请确认操作.";
                if (packInfo.VersionList != null)
                {
                    var version = SolutionService.Instance.GetReferVersion(packInfo.VersionList);
                    message = $"即将对包:{packName} 按照版本:{version} 进行版本对齐,请确认操作.";
                }
                var dialogResult = MessageBox.Show(message, "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    packInfo.DisposeStatus = SolutionService.Instance.DisposeVersion(packInfo, "");
                    dgvPackData.Rows[index].Cells["修复结果"].Value = packInfo.DisposeStatus ? "修复完成" : "修复失败";
                }
                SetMessage("版本对齐完成!请重新点击加载nuget信息按钮刷新数据");
                //btnLoadNuget_Click(null, null);
            }
            catch (Exception ex)
            {
                SetMessage("版本对齐异常!" + ex.Message, true);
            }

        }
        #endregion

        private void label5_Click(object sender, EventArgs e)
        {
            new FmHelp().Show();
        }
    }
}

