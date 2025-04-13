using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ShanghaiTrainer
{
   /// <summary>
   /// 公共方法
   /// </summary>
    internal class PublicFunction
    {
        ///<summary>
        /// &lt;文本型&gt; 取软件基本信息 
        /// <param name="paramcode">(整数型 要获取的信息代码)<para>参数代码含义:</para>1：取软件名称；2:取软件版本；3:取软件开发者；4、取软件产品名称。<para></para></param>
        /// <returns><para></para>返回文本型基本信息结果，失败使用了除1-4以外的参数则返回"InvalidRequest"。</returns> 
        /// </summary>
        public static string GetAPPInformation(int paramcode)
        {
            //取程序名称、版本、公司、产品名称、版权信息
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyTitleAttribute asmTitle = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyTitleAttribute));
            Version asmVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            AssemblyCompanyAttribute asmCompany = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            AssemblyProductAttribute asmProduct = (AssemblyProductAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyProductAttribute));
            AssemblyCopyrightAttribute asmCopyRight = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));

            // 将获取接管转为文本型
            string version = "Ver " + asmVersion.ToString();
            string title = asmTitle.Title;
            string company = asmCompany.Company;
            string appEnglishNane = asmProduct.Product;
            string copyRight = asmCopyRight.Copyright.ToString();

            // 参数接到不同代码返回不同的信息
            if (paramcode == 1)                 // 返回软件名称
            {
                return title;
            }
            else if (paramcode == 2)            // 返回软件版本
            {
                return version;
            }
            else if (paramcode == 3)            // 返回软件公司
            {
                return company;
            }
            else if (paramcode == 4)            // 返回软件产品名称
            {
                return appEnglishNane;
            }
            else if (paramcode == 5)            // 返回版权信息
            {
                return copyRight;
            }
            else                                // 输入其他字符则返回"InvalidRequest"
            {
                return "InvalidRequest";
            }
        }

        /// <summary>
        /// 访问网页
        /// <param name="url">(文本型 欲访问的网页)</param>
        /// </summary>
        public static void OpenURL(string url)
        {
            // 空字符检查
            if (string.IsNullOrWhiteSpace(url))
            {
               throw new Exception("URL cannot be empty"); 
            }

            //格式检查
            if (!(url.StartsWith("http://") || url.StartsWith("https://")))
            {
                throw new Exception("Unexpected URL format!");
            }


            // 用Process类打开网址
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// 路径检查
        /// </summary>
        public class PathValidator
        {
            /// <summary>
            /// 检查打包解包路径
            /// <param name="filepath">(文本型 欲打包或解包的文件路径, </param>
            /// <param name="directory">文本型 欲打包或解包的文件夹绝对路径, </param>
            /// <param name="arePackMode">逻辑型 是否为打包模式)</param>
            /// </summary>
            public void ValidatePaths(string filepath, string directory, bool arePackMode)
            {

                // 根据参数3来显示相应的文本
                string mode =  arePackMode? "打包" : "解包";

                // 欲打包或解包的文件路径为空抛出异常
                if (filepath == string.Empty)
                {
                    throw new Exception($"请指定要{mode}PCK文件！");
                }

                // 欲打包或解包的文件夹绝对路径抛出异常
                if (directory == string.Empty)
                {
                    throw new Exception($"请指定PCK{mode}目录！");
                }


            }           
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        public static void InitializeConfigFile()
        {
            // 获取AppData/Roaming特殊目录路径
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // 构建目标文件夹路径
            string targetFolder = Path.Combine(appDataPath, "52pojie", "ShanghaiTrainer");

            // 创建目录（如果不存在）
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
                Console.WriteLine($"目录已创建：{targetFolder}");
            }
            else
            {
                Console.WriteLine("目录已存在，无需创建");
            }

            // 构建文件路径
            string filePath = Path.Combine(targetFolder, "Config.ini");

            // 初始化INI实例
            var configCreate = new IniHelper();

            // 创建文件（如果不存在）
            if (!File.Exists(filePath))
            {
                configCreate.Write("Config", "GamePath", string.Empty);
                configCreate.SaveToFile(filePath);
                Console.WriteLine($"文件已创建：{filePath}");
            }
            else
            {
                Console.WriteLine("文件已存在，无需创建");
            }
        }

        /// <summary>
        /// &lt;字节集&gt; 到四字节低位数据
        /// <param name="hexValue">(长整型 欲转换的数值)</param>
        /// <returns><para>返回一组四字节低位数据</para></returns>
        /// </summary>
        public static byte[] HexToFourByteSet(long value)
        {
            // 创建一个四字节的数组
            byte[] littleEndianBytes = new byte[4];

            // 按照小端序填充数组
            littleEndianBytes[0] = (byte)(value & 0xFF);        // 最低位字节 CF
            littleEndianBytes[1] = (byte)((value >> 8) & 0xFF); // 次低位字节 4A
            littleEndianBytes[2] = (byte)((value >> 16) & 0xFF);// 次高位字节 36
            littleEndianBytes[3] = (byte)((value >> 24) & 0xFF);// 最高位字节 00

            // 返回小端序字节数组
            return littleEndianBytes;
        }

        /// <summary>
        ///  &lt;字节集&gt; 八字节补零
        /// <param name="bytes">(字节集 欲填充0x00的8字节字节集数组)</param>
        /// <returns><para>返回处理后的8字节数组</para></returns>
        /// </summary>
        public static byte[] EightByteConverter(byte[] bytes)
        {
            // 补到8字节用于Bit转换
            byte[] newByees = new byte[8];
            Array.Copy(bytes, 0, newByees, 0, bytes.Length);
            for (int i = bytes.Length; i < 8; i++)
            {
                newByees[i] = 0x00; // 补充0x00
            }
            // 将处理完的字节集数组交还给bytes变量
            return newByees;
        }

        /// <summary>
        /// &lt;文本型&gt; 字节大小数值转换
        /// <param name="bytes">(长整型 欲转换的字节大小数组)</param>
        /// <returns><para>成功返回字节大小</para></returns>
        /// </summary>
        public static string BytesToSize(long size)
        {
            var num = 1024.00; //byte
            if (size < num)
                return size + " Byte";
            if (size < Math.Pow(num, 2))
                return (size / num).ToString("f2") + " KB";
            if (size < Math.Pow(num, 3))
                return (size / Math.Pow(num, 2)).ToString("f2") + " MB";
            if (size < Math.Pow(num, 4))
                return (size / Math.Pow(num, 3)).ToString("f2") + " GB";
            if (size < Math.Pow(num, 5))
                return (size / Math.Pow(num, 4)).ToString("f2") + " TB";
            if (size < Math.Pow(num, 6))
                return (size / Math.Pow(num, 5)).ToString("f2") + " PB";
            if (size < Math.Pow(num, 7))
                return (size / Math.Pow(num, 6)).ToString("f2") + " EB";
            if (size < Math.Pow(num, 8))
                return (size / Math.Pow(num, 7)).ToString("f2") + " ZB";
            if (size < Math.Pow(num, 9))
                return (size / Math.Pow(num, 8)).ToString("f2") + " YB";
            if (size < Math.Pow(num, 10))
                return (size / Math.Pow(num, 9)).ToString("f2") + "DB";
            return (size / Math.Pow(num, 10)).ToString("f2") + "NB";
        }

        /// <summary>
        /// 定位输出文件夹(文本型 欲定位的路径)
        /// </summary>
        /// <param name="path"></param>
        public static void GetOutputFolder(string path)
        {
            try
            {
                // 创建一个ProcessStartInfo对象来配置进程启动的信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    // 要定位和打开的文件夹路径
                    FileName = "explorer.exe",
                    Arguments = $"/e,/select, \"{path}\""
                };
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                // 处理可能出现的异常
                Console.WriteLine($"无法打开文件夹: {ex.Message}");
            }
        }
    }
}
