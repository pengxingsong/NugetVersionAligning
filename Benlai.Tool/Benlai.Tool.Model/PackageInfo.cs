using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    /// <summary>
    /// 资源包信息
    /// </summary>
    public class PackageInfo
    {
        /// <summary>
        /// 包名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属项目目标版本
        /// </summary>
        public string ProjTargetFramework { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool StatusOK { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public bool DisposeStatus { get; set; }

        /// <summary>
        /// 所引用的项目列表
        /// </summary>
        public List<PackageRealProjInfo> ProjList { get; set; }

        /// <summary>
        /// 多版本
        /// </summary>
        public List<VersionInfo> VersionList { get; set; }
    }

    /// <summary>
    /// 包被引用的项目信息
    /// </summary>
    public class PackageRealProjInfo
    {
        /// <summary>
        /// 项目GUID
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjName { get; set; }

        /// <summary>
        /// package.config文件中的版本
        /// </summary>
        public string PackageNugetVersion { get; set; }

        /// <summary>
        /// .csproj文件中的版本
        /// </summary>
        public string CsprojNugetVersion { get; set; }

        /// <summary>
        /// 目录地址
        /// </summary>
        public string DirPath { get; set; }
        /// <summary>
        /// package.config文件地址
        /// </summary>
        public string PackageFilePath { get; set; }
        /// <summary>
        /// .csproj文件地址
        /// </summary>
        public string CsprojFilePath { get; set; }
    }

    /// <summary>
    /// 版本信息
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 被引用数
        /// </summary>
        public int RefCount { get; set; }

        public string ShowMsg
        {
            get
            {
                return "("+ RefCount+") "+ Version;
            }
        }
    }
}
