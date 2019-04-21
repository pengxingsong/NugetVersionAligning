using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    /// <summary>
    /// 项目关联的项目信息
    /// </summary>
    public class SolutionProjectInfo
    {
        /// <summary>
        /// 项目类型Guid
        /// </summary>
        public string ProjTypeGuid { get; set; }

        /// <summary>
        /// 项目Guid
        /// </summary>
        public string ProjGuid { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjName { get; set; }

        /// <summary>
        /// 项目目标版本
        /// </summary>
        public string ProjTargetFrameworkVersion { get; set; }

        /// <summary>
        /// 项目相对文件
        /// </summary>
        public string ProjFilePath { get; set; }

        /// <summary>
        /// 项目路径
        /// </summary>
        public string ProjDirPath { get; set; }

        /// <summary>
        /// package.config文件中nuget包引用信息
        /// </summary>
        public List<NugetPackageInfo> NugetPackageList { get; set; }

        /// <summary>
        /// .csproj中的nuget引用包信息
        /// </summary>
        public List<ProjFilePackageInfo> ProjPackageList { get; set; }

        /// <summary>
        /// 操作信息记录
        /// </summary>
        public string OptMessage { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsDispose { get { return string.IsNullOrWhiteSpace(OptMessage); } }


    }
}
