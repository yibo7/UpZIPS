﻿using System;
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

namespace U
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            XS.Core.Log.InfoLog.ErrorFormat("软件打开:{0}",DateTime.Now);
            
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
                    //lbStateInfo
                    //this.Invoke(dlgShowScanInfo, string.Concat("正在计算列表:", filepath));
                    filecollect = Directory.GetFileSystemEntries(filepath);


                }
                catch (Exception ex)
                {
                    XS.Core.Log.ErrorLog.ErrorFormat("在获取计算时发生错误:{0}", ex.ToString());
                    //MessageBox.Show("出错了！" + ex.Message);
                    //ex.ToString();
                }

                if (!Equals(filecollect, null))
                {
                    foreach (string file in filecollect)
                    {

                        //lbFindingInfo.Invoke(dlgShowScanInfo, file);

                        if (Directory.Exists(file))
                        {
                            ScanFiles(file);
                        }
                        else
                        {
                            //if (iZipType == 0)
                            //{
                            //    if (file.ToLower().EndsWith(".zip"))
                            //    {
                            //        lbStateInfo.Invoke(dlgDelegateAddItem, file);
                            //    }
                            //}
                            //else if (iZipType == 1)
                            //{
                            //    if (file.ToLower().EndsWith(".rar"))
                            //    {
                            //        lbStateInfo.Invoke(dlgDelegateAddItem, file);
                            //    }
                            //}

                            lbStateInfo.Invoke(dlgDelegateAddItem, file);

                            iReadCount++;
                            lbStateInfo.Invoke(dlgDelegateShowInfo, string.Format("文件读入:打包文件：{0}/所有文件：{1}", ZipCount, iReadCount));

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
                if (!IsDelFile)
                {
                    if (sFilePath.ToLower().EndsWith(".rar"))
                    {
                        ZipCount++;
                        DeCompressRar(sFilePath, ToPath);
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
        /// 将格式为rar的压缩文件解压到指定的目录
        /// </summary>
        /// <param name="rarFileName">要解压rar文件的路径</param>
        /// <param name="saveDir">解压后要保存到的目录</param>
        public static void DeCompressRar(string rarFileName, string saveDir)
        {
            string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKey);
            string winrarPath = registryKey.GetValue("").ToString();
            registryKey.Close();
            string winrarDir = System.IO.Path.GetDirectoryName(winrarPath);
            String commandOptions = string.Format(@"x ""{0}"" ""{1}"" -y", rarFileName, saveDir);
            
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = System.IO.Path.Combine(winrarDir, "rar.exe"); //
            processStartInfo.Arguments = commandOptions;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
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

            Thread st = new Thread(() =>
            {
                lbStateInfo.Invoke(dlgDelegateShowInfo, "文件读入中...");

                ScanFiles(sPath);
                //int icount = lstData.Count; // lvDataList.Items.Count;
                lbStateInfo.Invoke(dlgDelegateShowInfo,
                    string.Format("读入完毕,总共:{0}，Zip包:{1}，出错:{2}", iReadCount, ZipCount, ZipErrCount));
                //if (icount > 0)
                //    this.Invoke(dlgDelegateChuangeBtnState);
            });
            st.Start();
        }
    }
}
