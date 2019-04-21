using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benlai.Tool.Model
{
    /// <summary>
    /// 常量
    /// </summary>
    public class AppConst
    {
        /// <summary>
        /// 正则 项目Guid
        /// </summary>
        public const string Exp_ProjGuid = @"\w{8}-(\w{4}-){3}\w{12}";

        /// <summary>
        /// 正则 项目名称
        /// </summary>
        public const string Exp_ProjName = @"[a-z][\s\.\-\w]+";

        /// <summary>
        /// 正则 项目相对路径
        /// </summary>
        public const string Exp_ProjFilePath= @"(\\?([a-z][\s\.\-\w]+))+";

        /// <summary>
        /// 正则 解决方案Project节点信息
        /// </summary>
        public static string Exp_ProjNode
        {
            get
            {
                return string.Format("Project\\(\"{{{0}}}\"\\)\\s*=\\s*\"{1}\"\\s*,\\s*\"{2}\"\\s*,\\s*\"{{{3}}}\"", Exp_ProjGuid, Exp_ProjName, Exp_ProjFilePath, Exp_ProjGuid);
            }
        }

        /// <summary>
        /// C#项目类型GUID windows
        /// </summary>
        public const string CSharpProjGUID_WIN = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC";


    }
}
