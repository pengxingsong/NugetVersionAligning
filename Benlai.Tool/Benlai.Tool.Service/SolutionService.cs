using Benlai.Tool.Model;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Benlai.Tool.Service
{
    /// <summary>
    /// 解决方案工程 服务
    /// </summary>
    public class SolutionService
    {
        private static readonly Lazy<SolutionService> InstanceLazy = new Lazy<SolutionService>();
        public static SolutionService Instance
        {
            get { return InstanceLazy.Value; }
        }

        /// <summary>
        /// 加载解决方案项目工程信息
        /// </summary>
        /// <param name="slnFilePath">sln文件绝对路径地址</param>
        /// <returns></returns>
        public SolutionInfo LoadSlnProjectInfo(string slnFilePath)
        {
            try
            {
                /*
                一段project节点信息
                Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Benlai.Tool.Service", "Benlai.Tool.Service\Benlai.Tool.Service.csproj", "{05F22A2C-64E6-4E6E-BE56-35E007993CF4}"
                EndProject
                */
                slnFilePath = Path.GetFullPath(slnFilePath);
                var slnContent = File.ReadAllText(slnFilePath);
                if (string.IsNullOrWhiteSpace(slnContent))
                {
                    throw new Exception($"读取文件信息为空,请检查文件是否存在!文件路径:{slnFilePath}");
                }
                var dirPath = Path.GetDirectoryName(slnFilePath);
                List<SolutionProjectInfo> projList = new List<SolutionProjectInfo>();
                var projectReg = new Regex(AppConst.Exp_ProjNode, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var projectContexts = projectReg.Matches(slnContent); //正则匹配sln文件中的 project节点信息
                SolutionProjectInfo projInfo = null;
                foreach (var projectContext in projectContexts)
                {
                    var projectContextArr = projectContext.ToString().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (projectContextArr == null || projectContextArr.Length != 2)
                        continue;
                    var projectTypeGuid = new Regex(AppConst.Exp_ProjGuid, RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(projectContextArr[0].Trim()).Value;
                    if (projectTypeGuid.ToUpper() != AppConst.CSharpProjGUID_WIN) continue; //不为window C#类型的项目跳过
                    var projectValueArr = projectContextArr[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); //取出项目属性文本信息
                    if (projectValueArr.Length != 3) continue;

                    projInfo = new SolutionProjectInfo()
                    {
                        ProjTypeGuid = projectTypeGuid,
                        ProjName = new Regex(AppConst.Exp_ProjName, RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(projectValueArr[0].Trim()).Value,
                        ProjFilePath = new Regex(AppConst.Exp_ProjFilePath, RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(projectValueArr[1].Trim()).Value,
                        ProjGuid = new Regex(AppConst.Exp_ProjGuid, RegexOptions.Compiled | RegexOptions.IgnoreCase).Match(projectValueArr[2].Trim()).Value
                    };
                    projInfo.ProjDirPath = Path.GetDirectoryName(dirPath.TrimEnd(new char[] { '\\', '\\' }) + "\\" + projInfo.ProjFilePath.TrimStart(new char[] { '\\', '\\' }));
                    projList.Add(projInfo);
                }
                return new SolutionInfo()
                {
                    DirPath = dirPath,
                    FullFilePath = slnFilePath,
                    ProjectInfos = projList
                };
            }
            catch (Exception ex)
            {
                throw new Exception("读取解决方案项目工程信息发生异常.", ex);
            }
        }

        /// <summary>
        /// 加载解决方案中各个工程项目的包信息
        /// </summary>
        /// <param name="slnInfo"></param>
        /// <returns></returns>
        public SolutionInfo LoadProjectInfo(SolutionInfo slnInfo)
        {
            try
            {
                if (slnInfo == null)
                {
                    throw new Exception("解决方案信息为空!");
                }
                if (slnInfo.ProjectInfos == null || slnInfo.ProjectInfos.Count == 0)
                {
                    throw new Exception("没有需要加载的项目工程!");
                }
                string projFullFilePath = "";
                string packageFullFilePath = "";
                string dirPath = slnInfo.DirPath.TrimEnd(new char[] { '\\' });
                ProjectCollection projInfos = new ProjectCollection();
                Project projInfo = null;
                foreach (var proj in slnInfo.ProjectInfos)
                {
                    #region 加载包文件下的包信息
                    packageFullFilePath = proj.ProjDirPath + "\\" + "packages.config";
                    if (!File.Exists(packageFullFilePath))
                    {
                        proj.OptMessage += "没有找到package.config文件,解读为此项目未引用nuget包.";
                        continue;
                    }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(packageFullFilePath);
                    XmlNode root = xmlDoc.SelectSingleNode("/packages");
                    //获取节点下所有直接子节点
                    if (!root.HasChildNodes)
                    {
                        proj.OptMessage += "package.config文件下的packages节点没有子节点,解读为此项目未引用nuget包.";
                        continue;
                    }
                    proj.NugetPackageList = new List<NugetPackageInfo>();
                    XmlNodeList childlist = root.ChildNodes;
                    foreach (XmlNode node in childlist)
                    {
                        if (node.Name != "package") continue;
                        var nugetPackage = new NugetPackageInfo()
                        {
                            Name = node.Attributes["id"].Value.ToString(),
                            Version = node.Attributes["version"].Value.ToString(),
                            TargetFramework = node.Attributes["targetFramework"].Value.ToString(),
                        };
                        proj.NugetPackageList.Add(nugetPackage);
                    }
                    #endregion
                    #region 加载项目文件下的包信息
                    projFullFilePath = dirPath + "\\" + proj.ProjFilePath.TrimStart(new char[] { '\\' });
                    projInfo = projInfos.LoadProject(projFullFilePath);//加载项目文件
                    if (projInfo == null)
                    {
                        proj.OptMessage += "加载.csproj文件为空!";
                        continue;
                    }
                    proj.ProjTargetFrameworkVersion = projInfo.AllEvaluatedProperties.Where(x => x.Name == "TargetFrameworkVersion").FirstOrDefault().EvaluatedValue.TrimStart('v');
                    //筛选包含HintPath子节点的Reference节点
                    var projReferenceItems = projInfo.AllEvaluatedItems.Where(e => e.ItemType == "Reference" && e.Metadata.Any(x => x.Name == "HintPath" && x.EvaluatedValue.Contains(@"..\packages\"))).ToList();
                    if (projReferenceItems == null || projReferenceItems.Count == 0)
                    {
                        proj.OptMessage += "加载.csproj文件时未找到包含HintPath子节点的Reference节点!";
                        continue;
                    }
                    proj.ProjPackageList = new List<ProjFilePackageInfo>();
                    foreach (var projReference in projReferenceItems)
                    {
                        var pack = ConvetProjectPackage(projReference.Xml);
                        proj.ProjPackageList.Add(pack);
                    }
                    #endregion
                }
                return slnInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("加载工程项目包信息时异常!", ex);
            }
        }

        private ProjFilePackageInfo ConvetProjectPackage(ProjectItemElement ele)
        {
            ProjFilePackageInfo info = new ProjFilePackageInfo();
            if (ele == null) return null;
            string includeStr = ele.Include;
            var includeArr = includeStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            info.Include_Name = includeArr[0];
            foreach (var item in includeArr)
            {
                if (item.Contains("Version"))
                {
                    info.Include_Version = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
                else if (item.Contains("Culture"))
                {
                    info.Include_Culture = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
                else if (item.Contains("PublicKeyToken"))
                {
                    info.Include_PublicKeyToken = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
                else if (item.Contains("processorArchitecture"))
                {
                    info.Include_ProcessorArchitecture = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }
            }

            foreach (var item in ele.Metadata)
            {
                if (item.Name == "HintPath")
                {
                    info.HintPath = item.Value.ToString().Trim();
                }
                else if (item.Name == "Private")
                {
                    info.Private = item.Value.ToString().Trim();
                }
            }
            return info;
        }

        /// <summary>
        /// 获取包维度的信息
        /// </summary>
        /// <param name="slnInfo"></param>
        /// <returns></returns>
        public List<PackageInfo> GetPackInfos(SolutionInfo slnInfo)
        {
            try
            {
                List<PackageInfo> result = new List<PackageInfo>();
                foreach (var proj in slnInfo.ProjectInfos)
                {
                    if (proj.ProjPackageList == null || proj.ProjPackageList.Count == 0) continue;
                    foreach (var projPack in proj.ProjPackageList)
                    {
                        var nugetPack = proj.NugetPackageList.Where(x => x.Name == projPack.NugetPackName).FirstOrDefault();
                        var pack = result.Where(x => x.Name == projPack.NugetPackName).FirstOrDefault();
                        var packProj = new PackageRealProjInfo()
                        {
                            GUID = proj.ProjGuid,
                            ProjName = proj.ProjName,
                            CsprojNugetVersion = projPack.NugetVersion,
                            PackageNugetVersion = nugetPack?.Version,
                            DirPath = proj.ProjDirPath,
                            CsprojFilePath = proj.ProjDirPath + "\\" + proj.ProjName + ".csproj",
                            PackageFilePath = proj.ProjDirPath + "\\" + "packages.config",
                        };
                        bool isOk = packProj.CsprojNugetVersion == packProj.PackageNugetVersion;
                        if (pack == null)
                        {
                            result.Add(new PackageInfo()
                            {
                                Name = projPack.NugetPackName,
                                StatusOK = isOk,
                                ProjTargetFramework = "net"+proj.ProjTargetFrameworkVersion.Replace(".",""),
                                ProjList = new List<PackageRealProjInfo>()
                                {
                                    packProj
                                },
                            });
                        }
                        else
                        {
                            pack.ProjList = pack.ProjList ?? new List<PackageRealProjInfo>();
                            if (!pack.ProjList.Any(x => x.GUID == proj.ProjGuid))
                            {
                                pack.StatusOK = isOk;
                                pack.ProjList.Add(packProj);
                            }
                            if (pack.ProjList.Count > 1)
                            {
                                pack.StatusOK = pack.ProjList.GroupBy(x => new { x.PackageNugetVersion, x.CsprojNugetVersion }).Select(c => c.Key).Count() == 1;
                                pack.VersionList = pack.ProjList.GroupBy(x => new { x.PackageNugetVersion }).Select(x => new VersionInfo()
                                {
                                    Version = x.Key.PackageNugetVersion,
                                    RefCount = x.Count(),
                                }).OrderByDescending(x => x.RefCount).ToList();
                            }
                        }

                    }
                }
                return result.OrderBy(x => x.StatusOK).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("获取包维度的信息时异常!", ex);
            }
        }

        /// <summary>
        /// 对齐包版本
        /// </summary>
        /// <param name="packInfo"></param>
        /// <returns></returns>
        public bool DisposeVersion(PackageInfo packInfo, string version = "")
        {
            try
            {
                if (packInfo == null || packInfo.ProjList == null)
                {
                    throw new Exception("资源包对象为空!");
                }
                if (!packInfo.StatusOK && packInfo.DisposeStatus)
                {
                    return true;
                }
                string newVersion = "";
                #region 检查&获取修复版本号
                if (string.IsNullOrWhiteSpace(version))
                {
                    if (packInfo.VersionList != null && packInfo.VersionList.Count > 0)
                    {
                        newVersion = GetReferVersion(packInfo.VersionList);
                    }
                    else
                    {
                        newVersion = packInfo.ProjList[0].PackageNugetVersion;
                        if (string.IsNullOrWhiteSpace(newVersion))
                        {
                            newVersion = packInfo.ProjList[0].CsprojNugetVersion;
                        }
                    }
                }
                else
                {
                    if (!packInfo.ProjList.Any(x => x.CsprojNugetVersion != version && x.PackageNugetVersion != version))
                    {
                        newVersion = version;
                    }
                }
                if (string.IsNullOrWhiteSpace(newVersion))
                {
                    throw new Exception($"未找到有效版本号!{(version != "" ? "传入版本号:" + version : "")}");
                }
                #endregion
                newVersion = newVersion.Trim();
                //待修复的项目
                var disposeingProjList = packInfo.ProjList.Where(proj => !(!string.IsNullOrWhiteSpace(proj.CsprojNugetVersion)
                        && !string.IsNullOrWhiteSpace(proj.PackageNugetVersion)
                        && proj.CsprojNugetVersion == newVersion
                        && proj.PackageNugetVersion == newVersion)).Select(x => x);
                if (disposeingProjList == null || disposeingProjList.Count() == 0)
                    return true;
                foreach (var proj in disposeingProjList)
                {
                    #region 修复packages.config
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(proj.PackageFilePath);
                    XmlNode root = xmlDoc.SelectSingleNode("/packages");
                    if (root.HasChildNodes)
                    {
                        XmlNodeList childlist = root.ChildNodes;
                        XmlNode validNode = null;
                        bool isDispose = false;
                        bool hasPackageNode = false;
                        foreach (XmlNode node in childlist)
                        {
                            if (node.Name != "package") continue;
                            hasPackageNode = node.Attributes["id"].Value.ToString().Trim() == packInfo.Name.Trim();
                            if (hasPackageNode  && node.Attributes["version"].Value.ToString().Trim() != newVersion)
                            {
                                validNode = node;
                                //只修复版本号
                                node.Attributes["version"].Value = newVersion;
                                isDispose = true;
                            }
                        }
                        if (!hasPackageNode && validNode == null)
                        {
                            //未找到该包时，创建节点
                            XmlElement elem = xmlDoc.CreateElement("package");
                            elem.SetAttribute("id", packInfo.Name);
                            elem.SetAttribute("version", newVersion);
                            elem.SetAttribute("targetFramework", packInfo.ProjTargetFramework);
                            root.AppendChild(elem);
                            isDispose = true;
                        }
                        if (isDispose)
                            xmlDoc.Save(proj.PackageFilePath);
                    }

                    #endregion

                    #region 修复.csproj
                    ProjectCollection projInfos = new ProjectCollection();
                    var projInfo = projInfos.LoadProject(proj.CsprojFilePath);//加载项目文件
                    var csprojContent = File.ReadAllText(proj.CsprojFilePath);
                    if (projInfo != null)
                    {
                        var projReferenceItems = projInfo.AllEvaluatedItems.Where(e => e.ItemType == "Reference" && e.Metadata.Any(x => x.Name == "HintPath" && x.EvaluatedValue.Contains(@"..\packages\"))).ToList();
                        if (projReferenceItems != null)
                        {
                            foreach (var projReference in projReferenceItems)
                            {
                                var pack = ConvetProjectPackage(projReference.Xml);
                                if (pack.NugetPackName == packInfo.Name
                                    && pack.NugetVersion != newVersion)
                                {
                                    var newHintPath = pack.GetNewHintPath(newVersion, packInfo.ProjTargetFramework);
                                    csprojContent = csprojContent.Replace(pack.HintPath, newHintPath);
                                    File.WriteAllText(proj.CsprojFilePath, csprojContent);
                                }
                            }
                        }
                    }
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("对齐包版本发生异常!", ex);
            }
        }

        /// <summary>
        /// 获取准版本号
        /// </summary>
        /// <param name="versionList"></param>
        /// <returns></returns>
        public string GetReferVersion(List<VersionInfo> versionList)
        {
            if (versionList == null || versionList.Count == 0)
                return "";
            if (versionList.Count == 1)
                return versionList[0].Version;
            var maxRefCount = versionList.Max(x => x.RefCount);
            var maxRefProjList = versionList.Where(x => x.RefCount == maxRefCount).ToList();
            var version = maxRefProjList.FirstOrDefault().Version;
            if (maxRefProjList.Count > 1)
            {
                //如果存在引用数平分的情况那将以最大版本号为准
                version = maxRefProjList.OrderByDescending(x => x.Version).FirstOrDefault().Version;
            }
            return version;

        }
    }
}
