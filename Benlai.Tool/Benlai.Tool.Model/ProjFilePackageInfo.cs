using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    /// <summary>
    /// 项目文件包信息
    /// </summary>
    public class ProjFilePackageInfo
    {
        /// <summary>
        /// 具体引用dll 名称
        /// </summary>
        public string Include_Name { get; set; }

        /// <summary>
        /// 具体引用dll 版本
        /// </summary>
        public string Include_Version { get; set; }

        /// <summary>
        /// 具体引用dll  文化类型(neutral 中立)
        /// </summary>
        public string Include_Culture { get; set; }

        /// <summary>
        /// 具体引用dll  公钥
        /// </summary>
        public string Include_PublicKeyToken { get; set; }
        /// <summary>
        /// 具体引用dll  架构 (MSIL 微软)
        /// </summary>
        public string Include_ProcessorArchitecture { get; set; }

        /// <summary>
        /// 资源本地相对路径
        /// </summary>
        public string HintPath { get; set; }

        /// <summary>
        /// 是否私有 (True)
        /// </summary>
        public string Private { get; set; }


        private string _NugetPackFullName;
        public string NugetPackFullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_NugetPackFullName))
                {
                    var regValue = new Regex(@"packages\\(\w|\.)*(\d|\.|-|\w)*\\", RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(HintPath).Value;
                    regValue = regValue.Replace("packages\\", "");
                    regValue = regValue.Replace("\\", "");
                    _NugetPackFullName = regValue;
                }
                return _NugetPackFullName;
            }
        }

        
        private string _NugetVersion;
        /// <summary>
        /// 真正的nuget版本
        /// </summary>
        public string NugetVersion
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_NugetVersion))
                    _NugetVersion = new Regex(@"\d{1,}(\d|\.|-|\w)*", RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(NugetPackFullName).Value;
                return _NugetVersion;
            }
        }

        public string _NugetPackName;
        /// <summary>
        /// nuget包名称
        /// </summary>
        public string NugetPackName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_NugetPackName))
                    _NugetPackName = NugetPackFullName.Replace(NugetVersion, "").Trim('.');
                return _NugetPackName;
            }
        }


        /// <summary>
        ///  拼接Reference节点下Include属性值
        /// </summary>
        public string IncludeInfo
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.Append(Include_Name);
                str.Append(",");
                str.Append("Version=" + Include_Version);
                str.Append(",");
                if (!string.IsNullOrWhiteSpace(Include_Culture))
                {
                    str.Append("Culture=" + Include_Culture);
                    str.Append(",");
                }
                if (!string.IsNullOrWhiteSpace(Include_PublicKeyToken))
                {
                    str.Append("PublicKeyToken=" + Include_PublicKeyToken);
                    str.Append(",");
                }
                if (!string.IsNullOrWhiteSpace(Include_ProcessorArchitecture))
                {
                    str.Append("processorArchitecture=" + Include_ProcessorArchitecture);
                }
                return str.ToString().TrimEnd(',');
            }
        }

        /// <summary>
        /// 拼接Reference节点信息
        /// </summary>
        public string ReferenceNodeInfo
        {
            get
            {
                StringBuilder str = new StringBuilder();
                str.Append("<Reference");
                str.Append(" Include=\"");
                str.Append(IncludeInfo);
                str.Append("\">");
                str.Append("<HintPath>");
                str.Append(HintPath);
                str.Append("</HintPath>");
                if (!string.IsNullOrWhiteSpace(Private))
                {
                    str.Append("<Private>");
                    str.Append(Private);
                    str.Append("</Private>");
                }
                str.Append("</Reference>");
                return str.ToString();
            }
        }

        /// <summary>
        /// 获取新的Reference节点信息
        /// </summary>
        /// <param name="version"></param>
        /// <param name="targetFramework">目标版本(目前未使用)</param>
        /// <returns></returns>
        public string GetNewHintPath(string newVersion, string targetFramework)
        {
            var newHintPath = new Regex(@"\d{1,}(\.|\d)*\\lib\\").Replace(HintPath, $@"{newVersion}\lib\");
            return newHintPath;
        }

    }
}
