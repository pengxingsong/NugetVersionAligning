using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    public class SolutionInfo
    {
        /// <summary>
        /// sln绝对文件路径
        /// </summary>
        public string FullFilePath { get; set; }

        /// <summary>
        /// sln目录
        /// </summary>
        public string DirPath { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        public List<SolutionProjectInfo> ProjectInfos { get; set; }
    }
}
