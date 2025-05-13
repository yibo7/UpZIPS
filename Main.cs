using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using UpZips;
using XS.Core.FSO;
using XS.Core.Strings;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace U
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            txtHzs.Text = ".rar,.zip";
            
        }


        private void btnSelPath_Click(object sender, EventArgs e)
        {
            
            //DeCompressRar(txtPath.Text, Path.GetDirectoryName(txtPath.Text));
            //return;
            FolderBrowserDialog fld = new FolderBrowserDialog();

            //首次defaultfilePath为空，按FolderBrowserDialog默认设置（即桌面）选择  
            if (!string.IsNullOrEmpty(Configs.Instance.Cf.Model.LastPath))
            {
                //设置此次默认目录为上一次选中目录  
                fld.SelectedPath = Configs.Instance.Cf.Model.LastPath;
            }

            fld.ShowDialog();


            string sPath = fld.SelectedPath.Trim();
            txtPath.Text = sPath;
            Configs.Instance.Cf.Model.LastPath = sPath;
            Configs.Instance.Cf.Save();
        }

        private delegate void DelegateShowInfo(string info); //用来在扫描过程中回调
        private DelegateShowInfo dlgDelegateShowInfo;
        private string[] aScanFileType = {".zip"};
        private int iReadCount = 0;

        private delegate void DelegateAddItem(string info); //用来在扫描过程中回调
        private DelegateAddItem dlgDelegateAddItem;

        private void ScanFiles(string filepath)
        {
            if (filepath.Trim().Length > 0)
            {

                string[] filecollect = null;
                try
                {
                   
                    filecollect = Directory.GetFileSystemEntries(filepath);
                     
                }
                catch (Exception ex)
                {
                    XS.Core.Log.ErrorLog.ErrorFormat("在获取计算时发生错误:{0}", ex.ToString());
                     
                }

                if (!Equals(filecollect, null))
                {
                    foreach (string file in filecollect)
                    {
                         
                        if (Directory.Exists(file))
                        {
                            ScanFiles(file);
                        }
                        else
                        { 
                            iReadCount++;
                            lbStateInfo.Invoke(dlgDelegateShowInfo, string.Format("正在读入：{0}/{1}\n\t{2}", ZipCount, iReadCount, file));
                            lbStateInfo.Invoke(dlgDelegateAddItem, file);

                            

                        }
                    }
                }


            }
        }

        private void ShowScanInfo(string sInfo)
        {
            lbStateInfo.Text = sInfo;


        }

        private int ZipCount = 0;
        private int ZipErrCount = 0;
        private bool IsDelFile = false;//0zip,1rar
        private void AddTagToList(string sFilePath)
        {
            
            try
            {
                string ToPath = Path.GetDirectoryName(sFilePath);
                string fileName = Path.GetFileNameWithoutExtension(sFilePath);
                ToPath = $"{ToPath}\\{fileName}\\";
                if (!IsDelFile)
                {
                    if (sFilePath.ToLower().EndsWith(".rar"))
                    {
                        ZipCount++;
                       string err = DeCompressRar(sFilePath, ToPath);
                        if (!string.IsNullOrEmpty(err)) { 
                            ZipErrCount++;
                            lbStateInfo.Invoke(dlgDelegateShowInfo, $"出错了：{err}");
                            XS.Core.Log.InfoLog.ErrorFormat(sFilePath);
                        }
                    }
                    else if (sFilePath.ToLower().EndsWith(".zip"))
                    {
                        ZipCount++;
                        //System.IO.Compression.ZipFile.ExtractToDirectory(sFilePath, ToPath);
                        ZipHelper.UnZipFile(sFilePath, ToPath);
                    }
                }
                else {
                    if (sFilePath.ToLower().EndsWith(".rar")|| sFilePath.ToLower().EndsWith(".zip"))
                    {
                        ZipCount++;
                        FObject.Delete(sFilePath, FsoMethod.File);
                    }
                        
                }
                
                    
                

            }
            catch (Exception ex)
            {
                ZipErrCount++;
                XS.Core.Log.ErrorLog.ErrorFormat("对文件:{0}的操作发生错误:{1}", sFilePath, ex.Message);

                string sCopyFileUrl = GetString.GetNewNameByDate(sFilePath);
                sCopyFileUrl = string.Concat("/errfile/", sCopyFileUrl);
                string sCopyFilePath = string.Concat(Application.StartupPath, "\\",sCopyFileUrl.Replace("/", "\\"));
                FObject.ExistsDirectory(sCopyFilePath);
                XS.Core.FSO.FObject.CopyFile(sFilePath, sCopyFilePath);
            }
           

        }
        /// <summary>
        /// 解压 RAR 文件，支持异常处理（密码保护、缺失分卷等）
        /// </summary>
        /// <param name="rarFileName">RAR 文件路径</param>
        /// <param name="saveDir">解压到的文件夹</param>
        /// <param name="timeoutMs">最大解压时间（毫秒），默认10分钟</param>
        public static string DeCompressRar(string rarFileName, string saveDir, int timeoutMinutes = 5)
        {
            try
            {
                string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKey);
                if (registryKey == null)
                    return "WinRAR 未安装或未在注册表中找到。";

                string winrarPath = registryKey.GetValue("")?.ToString();
                registryKey.Close();

                if (string.IsNullOrWhiteSpace(winrarPath) || !File.Exists(winrarPath))
                    return "未找到 WinRAR 执行文件路径。";

                string winrarDir = Path.GetDirectoryName(winrarPath);
                string rarExePath = Path.Combine(winrarDir, "rar.exe");
                if (!File.Exists(rarExePath))
                    return "未找到 rar.exe，请确认 WinRAR 安装完整。";

                string commandOptions = $@"x ""{rarFileName}"" ""{saveDir}"" -y";

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = rarExePath,
                    Arguments = commandOptions,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(936),
                    StandardErrorEncoding = Encoding.GetEncoding(936)
                };

                using (Process process = new Process())
                {
                    StringBuilder outputBuilder = new StringBuilder();
                    bool detectedError = false;
                    string detectedErrorMessage = null;

                    process.StartInfo = processStartInfo;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null) return;
                        outputBuilder.AppendLine(e.Data);

                        if (CheckForFatalMessage(e.Data, out var reason))
                        {
                            detectedError = true;
                            detectedErrorMessage = reason;
                            try { process.Kill(); } catch { }
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null) return;
                        outputBuilder.AppendLine(e.Data);

                        if (CheckForFatalMessage(e.Data, out var reason))
                        {
                            detectedError = true;
                            detectedErrorMessage = reason;
                            try { process.Kill(); } catch { }
                        }

                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    int timeoutMs = timeoutMinutes * 60 * 1000;
                    if (!process.WaitForExit(timeoutMs))
                    {
                        try { process.Kill(); } catch { }
                        return "解压超时，可能卡在输入密码或缺少分卷。";
                    }

                    if (detectedError)
                        return "解压失败：" + detectedErrorMessage;

                    if (process.ExitCode != 0)
                        return $"解压失败，WinRAR 退出码：{process.ExitCode}";
                }

                return string.Empty; // 没有错误
            }
            catch (Exception ex)
            {
                return "解压异常：" + ex.Message;
            }
        }

        private static bool CheckForFatalMessage(string line, out string reason)
        {
            //line = line.ToLowerInvariant();

            if (line.Contains("密码") || line.Contains("password"))
            {
                reason = "压缩包需要密码";
                return true;
            }

            if (line.Contains("分卷") || line.Contains("cannot open next volume"))
            {
                reason = "缺少分卷文件";
                return true;
            }

            if (line.Contains("错误") || line.Contains("error"))
            {
                reason = "发生未知错误：" + line;
                return true;
            }

            reason = null;
            return false;
        }



        private void btnStartUpZip_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ExistsWinRar()))
            {
                DialogResult dr = MessageBox.Show("还没有安装WinRAR,将无法解压rar文件,是否还要进行？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK)
                {
                    return;
                }  
            }
            IsDelFile = false;
            FileHander();
        
        }

        //private void rabZip_CheckedChanged(object sender, EventArgs e)
        //{
        //    iZipType = rabZip.Checked ? 0 : 1;

        //}

        //private void rabRar_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ExistsWinRar()))
        //    {
        //        iZipType = rabRar.Checked ? 1 : 0;
        //    }
        //    else
        //    {
        //        rabRar.Checked = false;
        //        rabZip.Checked = true;

        //        MessageBox.Show("还没有安装WinRAR");
        //    }
        //}

        public static string ExistsWinRar()
        {
            string result = string.Empty;

            string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
            if (registryKey != null)
            {
                result = registryKey.GetValue("").ToString();
                registryKey.Close();
            }
            

            return result;
        }

        private void btn_del_all_ziprar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("此操作将删除当前目录及子目录的zip文件与rar文件,是否还要进行？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }
            IsDelFile = true;
            FileHander();

        }

        private void FileHander()
        {
            ZipCount = 0;
            ZipErrCount = 0;
            dlgDelegateAddItem = AddTagToList;
            dlgDelegateShowInfo = ShowScanInfo;
            //dlgDelegateChuangeBtnState = UpToDataBaseEnabled;

            string sPath = txtPath.Text.Trim();

            if (string.IsNullOrWhiteSpace(sPath))
            {
                MessageBox.Show("请选择源目录。");
                return ;
            }

            Thread st = new Thread(() =>
            {
                lbStateInfo.Invoke(dlgDelegateShowInfo, "文件读入中...");

                ScanFiles(sPath);
                //int icount = lstData.Count; // lvDataList.Items.Count;
                lbStateInfo.Invoke(dlgDelegateShowInfo,
                    string.Format("读入完毕,总共:{0}，Zip包:{1}，出错:{2} {3}", iReadCount, ZipCount, ZipErrCount, ZipErrCount>0?"出错文件请查看Info日志":""));
                //if (icount > 0)
                //    this.Invoke(dlgDelegateChuangeBtnState);
            });
            st.Start();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private string outputDir;
        private void btnStartFind_Click(object sender, EventArgs e)
        {
            string sPath = txtPath.Text.Trim();


            if (string.IsNullOrWhiteSpace(sPath))
            {
                MessageBox.Show("请选择源目录。");
                return;
            }


            dlgDelegateAddItem = FindFile;
            dlgDelegateShowInfo = ShowScanInfo;

            string sHz = txtHzs.Text.Trim();
            if (string.IsNullOrWhiteSpace(sHz))
            {
                MessageBox.Show("请输入后缀，多个用逗号分开。");
                return;
            }
            allowedExtensions = sHz.Split(',');

            // 获取当前程序目录
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            outputDir = Path.Combine(currentDir, "output");

            // 创建output目录（如果不存在）
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            iReadCount = 0;
            ZipCount = 0;

            Thread st = new Thread(() =>
            {
                lbStateInfo.Invoke(dlgDelegateShowInfo, "文件读入中...");
                ScanFiles(sPath); 
                lbStateInfo.Invoke(dlgDelegateShowInfo,string.Format("读入完毕,总共:{0}个文件，找到{1}个文件", iReadCount,ZipCount));
                 
            });
            st.Start();
        }
        // 要检查的扩展名列表
        string[] allowedExtensions = { ".rar", ".zip", ".png" };
        private void FindFile(string sFilePath)
        {
            try
            {

                //iReadCount++;
                // 获取文件扩展名并统一转为小写
                string ext = Path.GetExtension(sFilePath)?.ToLower();

               

                // 判断是否为指定类型的文件
                if (allowedExtensions.Contains(ext))
                {
                    ZipCount++;

                    // 获取文件名和扩展名
                    string fileName = Path.GetFileNameWithoutExtension(sFilePath);
                    string extension = Path.GetExtension(sFilePath);

                    // 构建初始目标路径
                    string destFilePath = Path.Combine(outputDir, fileName + extension);
                    int copyIndex = 1;

                    // 如果目标文件已存在，自动重命名
                    while (File.Exists(destFilePath))
                    {
                        string newFileName = $"{fileName}_rename_({copyIndex}){extension}";
                        destFilePath = Path.Combine(outputDir, newFileName);
                        copyIndex++;
                    }

                    // 复制文件到目标路径
                    File.Copy(sFilePath, destFilePath);

                }
            }
            catch (Exception ex)
            {
                ZipErrCount++;
                lbStateInfo.Invoke(dlgDelegateShowInfo, $"提取文件:{sFilePath} 错误:{ex.Message}");
                XS.Core.Log.ErrorLog.ErrorFormat("提取文件:{0} 错误:{1}", sFilePath, ex.Message);
            }

        }

        private void btnDelSames_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("将根据文件MD5值判断是否相同，确定要删除重复文件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }
            string sPath = txtPath.Text.Trim();


            if (string.IsNullOrWhiteSpace(sPath))
            {
                MessageBox.Show("请选择源目录。");
                return;
            }


            dlgDelegateAddItem = DelSameFile;
            dlgDelegateShowInfo = ShowScanInfo;

            fileMd5Dict.Clear();
            iReadCount = 0;
            ZipCount = 0;
            Thread st = new Thread(() =>
            {
                lbStateInfo.Invoke(dlgDelegateShowInfo, "文件读入中...");
                ScanFiles(sPath);
                lbStateInfo.Invoke(dlgDelegateShowInfo, string.Format("读入完毕,总共:{0}个文件，删除{1}个文件", iReadCount, ZipCount));

            });
            st.Start();
        }

        // 声明一个用于存储 MD5 值的字典（建议放在类中作为成员变量）
        private Dictionary<string, string> fileMd5Dict = new Dictionary<string, string>();

        private void DelSameFile(string sFilePath)
        {
            try
            {
               
                // 计算文件的 MD5 值
                string md5Hash = GetFileMd5(sFilePath);

                if (fileMd5Dict.ContainsKey(md5Hash))
                {
                    // 如果已经存在相同的 MD5，说明是重复文件，删除
                    File.Delete(sFilePath);
                    ZipCount++;
                    lbStateInfo.Invoke(dlgDelegateShowInfo, $"删除重复文件: {sFilePath}");

                    //XS.Core.Log.InfoLog.InfoFormat("删除重复文件: {0}", sFilePath);
                }
                else
                {
                    // 否则加入字典中
                    fileMd5Dict[md5Hash] = sFilePath;
                }
            }
            catch (Exception ex)
            {
                ZipErrCount++;
                lbStateInfo.Invoke(dlgDelegateShowInfo, $"删除文件:{sFilePath} 错误:{ex.Message}");
                XS.Core.Log.ErrorLog.ErrorFormat("删除文件:{0} 错误:{1}", sFilePath, ex.Message);
            }
        }

        // 计算文件 MD5 的辅助方法
        private string GetFileMd5(string filePath)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

    }
}
