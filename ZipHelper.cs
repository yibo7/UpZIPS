 
using System.IO;   
using ICSharpCode.SharpZipLib.Zip;

namespace UpZips
{
	 
	public abstract class ZipHelper
	{
       
        /// <summary>
        /// 解压ZIP包到指定目录
        /// </summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <param name="unZipDir">The un zip dir.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool UnZipFile(string zipFilePath, string unZipDir)
        {
            if (unZipDir == string.Empty)
            {
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            }
            if (!unZipDir.EndsWith(@"\"))
            {
                unZipDir = unZipDir + @"\";
            }
            if (!Directory.Exists(unZipDir))
            {
                Directory.CreateDirectory(unZipDir);
            }
            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry entry;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(unZipDir + directoryName);
                    }
                    if (!directoryName.EndsWith(@"\"))
                    {
                        directoryName = directoryName + @"\";
                    }
                    if (fileName != string.Empty)
                    {
                        using (FileStream stream2 = File.Create(unZipDir + entry.Name))
                        {
                            bool flag2;
                            int count = 0x800;
                            byte[] buffer = new byte[0x800];
                            goto Label_0134;
                        Label_0101:
                            count = stream.Read(buffer, 0, buffer.Length);
                            if (count > 0)
                            {
                                stream2.Write(buffer, 0, count);
                            }
                            else
                            {
                                goto Label_0152;
                            }
                        Label_0134:
                            flag2 = true;
                            goto Label_0101;
                        }
                    Label_0152: ;
                    }
                }
            }
            return true;
        }
         

    }

	
}
