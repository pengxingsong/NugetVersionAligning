using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    /// <summary>
    /// Nuget包信息
    /// </summary>
    public class NugetPackageInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 目标平台版本
        /// </summary>
        public string TargetFramework { get; set; }

        /// <summary>
        /// 拼接package节点信息
        /// </summary>
        public string PackageInfo
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.Append("<package");
                str.Append(" id=\"");
                str.Append(Name+"\"");
                str.Append(" version=\"");
                str.Append(Version + "\"");
                str.Append(" targetFramework=\"");
                str.Append(TargetFramework + "\"");
                str.Append("/>");
                return str.ToString();
            }
        }
    }
}
