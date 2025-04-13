using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShanghaiTrainer
{
    /// <summary>
    /// PCK打包解包操作类
    /// </summary>
    internal class PCKHelper
    {
        #region 文件状态回调
        /// <summary>
        /// 文件状态输出
        /// </summary>
        public class FileProgress
        {
            public int percentage { get; set; }     //打包解包进程百分比
            public string state { get; set; }       //打包解包状态
        }
        #endregion

        #region PCK打包方法
        /// <summary>
        /// PCK打包操作类
        /// </summary>
        public class Pack()
        {

            /// <summary>
            /// 打包PCK
            /// <param name="packPath">(文本型 欲打包的文件夹绝对路径, </param>
            /// <param name="filePath">文本型 欲保存的PCK文件路径</param>
            /// <param name="compressLevel">整数型 压缩等级)</param>
            /// <exception cref="Exception"></exception>
            /// </summary>
            public static void PackPck(string packPath, string filePath, int compressLevel = -1)
            {
                /* 目录区排序方式：
                 1、文件名长度（占用四个字节，低位地址）
                2、文件名（占用若干字节）
                3、文件物理偏移（占用四个字节，低位地址）
                4、文件压缩前大小（占用四个字节，低位地址）
                5、文件压缩后大小（占用四个字节，低位地址）
                 */
              
                // 打包路径自动添加""\
                if (!packPath.EndsWith("\\"))
                {
                    packPath += "\\";
                }

                // 检查文件是否存在，如果存在，强制删除，如果删除失败，终止操作
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("拒绝访问，覆盖失败！\n具体原因:" + ex.Message + "\n请检查文件是否被其他程序占用。", "覆盖文件出现错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // 遍历要打包的文件列表
                List<string> packList = GetAllFilesInDirectory(packPath);

                // 取打包文件总数
                int fileCount = packList.Count;

                // 开始定义PCK签名档，PCK签名档首尾均为0x03 0x00 0x01 0x00。
                byte[] signStartEnd = { 0x03, 0x00, 0x01, 0x00 };

                // 定义包文件特征
                byte[] packCharact = { 0x41, 0x6E, 0x67, 0x65, 0x6C, 0x69, 0x63, 0x61, 0x20, 0x46, 0x69, 0x6C, 0x65, 0x20, 0x50, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65, 0x2C, 0x20, 0x42, 0x65, 0x69, 0x6A, 0x69, 0x6E, 0x67, 0x20, 0x45, 0x2D, 0x50, 0x69, 0x65, 0x20, 0x45, 0x6E, 0x74, 0x65, 0x72, 0x74, 0x61, 0x69, 0x6E, 0x6D, 0x65, 0x6E, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x32, 0x30, 0x30, 0x32, 0x7E, 0x32, 0x30, 0x30, 0x38, 0x2E, 0x20, 0x41, 0x6C, 0x6C, 0x20, 0x52, 0x69, 0x67, 0x68, 0x74, 0x73, 0x20, 0x52, 0x65, 0x73, 0x65, 0x72, 0x76, 0x65, 0x64, 0x2E, 0x20, 0x00 };

                // 定义目录区字节List
                List<byte> dirArea = new List<byte>();

                // 定义数据区长度
                long dataAreaLengh = 0;

                // 启动文件流，开始写入PCK文件
                using (FileStream fs = new FileStream(filePath, FileMode.Append))
                {
                    // 指针从零开始
                    fs.Seek(0, SeekOrigin.Begin);

                    // 开始处理文件
                    for (int i = 0; i < fileCount; i++)
                    {

                        // 置文件偏移
                        long fileOffset = fs.Position;
                        byte[] fileOffsetBit = PublicFunction.HexToFourByteSet(fileOffset);

                        // 取文件信息
                        FileInfo fileInfo = new FileInfo(packList[i]);

                        // 置压缩前后大小
                        long fileActualsize = 0;
                        long fileCompressionsize = 0;

                        // 开始写入数据，分两种情况，一种为启用Zlib压缩另一种为未启用Zlib压缩
                        byte[] writeData;
                        // 判断是否启用Zlib压缩
                        if (compressLevel >= 0 && compressLevel <= 9)
                        {
                            byte[] actualData = File.ReadAllBytes(packList[i]);
                            writeData = ZlibHelper.Compress(actualData, compressLevel);

                            // 如果压缩后大小大于压缩前大小，则不对此文件压缩
                            if (writeData.Length > actualData.Length)
                            {
                                fs.Write(actualData, 0, actualData.Length);
                                fileActualsize = fileInfo.Length;
                                fileCompressionsize = fileInfo.Length;
                            }
                            else
                            {
                                fs.Write(writeData, 0, writeData.Length);
                                fileActualsize = fileInfo.Length;
                                fileCompressionsize = writeData.Length;
                            }

                        }
                        else if (compressLevel == -1)
                        {
                            writeData = File.ReadAllBytes(packList[i]);
                            fs.Write(writeData, 0, writeData.Length);
                            fileActualsize = fileInfo.Length;
                            fileCompressionsize = fileInfo.Length;
                        }
                        else
                        {
                            throw new Exception("invalid parameter!");
                        }


                        Console.WriteLine("文件名:" + packList[i] +
                            "\n压缩前大小:" + fileActualsize.ToString() + " 字节（十六进制:" + fileActualsize.ToString("X") +
                            ")\n压缩后大小:" + fileCompressionsize.ToString() + " 字节（十六进制:" + fileCompressionsize.ToString("X") + ")");

                        // 处理包内文件完整路径，首先去除打包路径
                        string fileName = packList[i].Replace(packPath, string.Empty);

                        // 取文件名
                        byte[] fileNameBit = new byte[fileName.Length];
                        fileNameBit = Encoding.Default.GetBytes(fileName);

                        byte[] fullPath = new byte[fileNameBit.Length + 1];
                        Array.Copy(fileNameBit, fullPath, fileNameBit.Length);
                        fullPath[fileNameBit.Length] = 0x00;

                        // 取目录长度,并写入四字节
                        byte[] fileNameLengthBit = PublicFunction.HexToFourByteSet((long)fullPath.Length);

                        // 置压缩前大小
                        byte[] fileActualsizeBit = PublicFunction.HexToFourByteSet(fileActualsize);

                        // 置压缩后大小
                        byte[] fileCompressionsizeBit = PublicFunction.HexToFourByteSet(fileCompressionsize);

                        // -----开始写入目录区数据-----                       
                        // 写入文件名长度
                        dirArea.AddRange(fileNameLengthBit);
                        
                        // 写入文件名数据
                        dirArea.AddRange(fullPath);
                        
                        // 写入文件偏移
                        dirArea.AddRange(fileOffsetBit);
                        
                        //写入压缩前大小
                        dirArea.AddRange(fileActualsizeBit);
                        
                        //写入压缩后大小
                        dirArea.AddRange(fileCompressionsizeBit);
                        
                        // 取当前数据区长度
                        dataAreaLengh = fs.Position;
                    }

                    // 填充目录区
                    byte[] dirAreaByte = dirArea.ToArray();
                    fs.Write(dirAreaByte, 0, dirAreaByte.Length);

                    // 填充签名头部
                    fs.Write(signStartEnd, 0, signStartEnd.Length);
                    
                    // 填充数据区总长度
                    byte[] dataAreaLenghBit = PublicFunction.HexToFourByteSet(dataAreaLengh);
                    fs.Write(dataAreaLenghBit, 0, dataAreaLenghBit.Length);

                    // 填充特征
                    fs.Write(packCharact, 0, packCharact.Length);
                    
                    // 填充0x00
                    byte[] byteZero = FillBytes(0x00, 160);
                    fs.Write(byteZero, 0, byteZero.Length);

                    // 填充文件总数
                    byte[] fileCountBit = PublicFunction.HexToFourByteSet((long)fileCount);
                    fs.Write(fileCountBit, 0, fileCountBit.Length);

                    // 填充签名尾部
                    fs.Write(signStartEnd, 0, signStartEnd.Length);

                    // 关闭文件流
                    fs.Close();
                }

                // 字节填充
                static byte[] FillBytes(byte fillByte, int count)
                {
                    // 创建一个指定大小的字节数组
                    byte[] filledBytes = new byte[count];

                    // 使用循环填充数组
                    for (int i = 0; i < count; i++)
                    {
                        filledBytes[i] = fillByte;
                    }

                    // 返回填充后的字节数组
                    return filledBytes;
                }
            }

            /// <summary>
            /// 异步打包PCK
            /// <param name="packPath">(文本型 欲打包的文件夹绝对路径, </param>
            /// <param name="filePath">文本型 欲保存的PCK文件路径</param>
            /// <param name="compressLevel">整数型 压缩等级, </param>
            /// <param name="progressCallback">文件进程 欲引用的回调)</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            /// </summary>
            public static async Task PackPckAsync(string packPath, string filePath, int compressLevel, Action<FileProgress> progressCallback)
            {
                /* 目录区排序方式：
                 1、文件名长度（占用四个字节，低位地址）
                 2、文件名（占用若干字节）
                 3、文件物理偏移（占用四个字节，低位地址）
                 4、文件压缩前大小（占用四个字节，低位地址）
                 5、文件压缩后大小（占用四个字节，低位地址）
                */

                // 打包路径自动添加""\
                if (!packPath.EndsWith("\\"))
                {
                    packPath += "\\";
                }

                // 检查文件是否存在，如果存在，强制删除，如果删除失败，终止操作
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("拒绝访问，覆盖失败！\n具体原因:" + ex.Message + "\n请检查文件是否被其他程序占用。", "覆盖文件出现错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

                // 遍历要打包的文件列表
                List<string> packList = GetAllFilesInDirectory(packPath);

                // 取打包文件总数
                int fileCount = packList.Count;

                // 开始定义PCK签名档，PCK签名档首尾均为0x03 0x00 0x01 0x00。
                byte[] signStartEnd = { 0x03, 0x00, 0x01, 0x00 };

                // 定义包文件特征
                byte[] packCharact = { 0x41, 0x6E, 0x67, 0x65, 0x6C, 0x69, 0x63, 0x61, 0x20, 0x46, 0x69, 0x6C, 0x65, 0x20, 0x50, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65, 0x2C, 0x20, 0x42, 0x65, 0x69, 0x6A, 0x69, 0x6E, 0x67, 0x20, 0x45, 0x2D, 0x50, 0x69, 0x65, 0x20, 0x45, 0x6E, 0x74, 0x65, 0x72, 0x74, 0x61, 0x69, 0x6E, 0x6D, 0x65, 0x6E, 0x74, 0x20, 0x43, 0x6F, 0x72, 0x70, 0x6F, 0x72, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x32, 0x30, 0x30, 0x32, 0x7E, 0x32, 0x30, 0x30, 0x38, 0x2E, 0x20, 0x41, 0x6C, 0x6C, 0x20, 0x52, 0x69, 0x67, 0x68, 0x74, 0x73, 0x20, 0x52, 0x65, 0x73, 0x65, 0x72, 0x76, 0x65, 0x64, 0x2E, 0x20, 0x00 };

                // 定义目录区字节List
                List<byte> dirArea = new List<byte>();

                // 定义数据区长度
                long dataAreaLengh = 0;

                // 启动文件流，开始写入PCK文件
                using (FileStream fs = new FileStream(filePath, FileMode.Append))
                {
                    // 指针从零开始
                    fs.Seek(0, SeekOrigin.Begin);

                    // 开始处理文件
                    for (int i = 0; i < fileCount; i++)
                    {
                        // 置文件偏移
                        long fileOffset = fs.Position;
                        byte[] fileOffsetBit = PublicFunction.HexToFourByteSet(fileOffset);

                        // 取文件信息
                        FileInfo fileInfo = new FileInfo(packList[i]);

                        // 置压缩前后大小
                        long fileActualsize = 0;
                        long fileCompressionsize = 0;

                        // 开始写入数据，分两种情况，一种为启用Zlib压缩另一种为未启用Zlib压缩
                        byte[] writeData;
                        // 判断是否启用Zlib压缩
                        if (compressLevel >= 0 && compressLevel <= 9)
                        {
                            byte[] actualData = File.ReadAllBytes(packList[i]);
                            writeData = await ZlibHelper.CompressAsync(actualData, compressLevel);

                            // 如果压缩后大小大于压缩前大小，则不对此文件压缩
                            if (writeData.Length > actualData.Length)
                            {
                                await fs.WriteAsync(actualData, 0, actualData.Length);
                                fileActualsize = fileInfo.Length;
                                fileCompressionsize = fileInfo.Length;
                            }
                            else
                            {
                                await fs.WriteAsync(writeData, 0, writeData.Length);
                                fileActualsize = fileInfo.Length;
                                fileCompressionsize = writeData.Length;
                            }

                        }
                        else if (compressLevel == -1)
                        {
                            writeData = File.ReadAllBytes(packList[i]);
                            await fs.WriteAsync(writeData, 0, writeData.Length);
                            fileActualsize = fileInfo.Length;
                            fileCompressionsize = fileInfo.Length;
                        }
                        else
                        {
                            throw new Exception("invalid parameter!");
                        }


                        Console.WriteLine("文件名:" + packList[i] +
                            "\n压缩前大小:" + fileActualsize.ToString() + " 字节（十六进制:" + fileActualsize.ToString("X") +
                            ")\n压缩后大小:" + fileCompressionsize.ToString() + " 字节（十六进制:" + fileCompressionsize.ToString("X") + ")");

                        // 处理包内文件完整路径，首先去除打包路径
                        string fileName = packList[i].Replace(packPath, string.Empty);

                        // 取文件名
                        byte[] fileNameBit = new byte[fileName.Length];
                        fileNameBit = Encoding.Default.GetBytes(fileName);

                        byte[] fullPath = new byte[fileNameBit.Length + 1];
                        Array.Copy(fileNameBit, fullPath, fileNameBit.Length);
                        fullPath[fileNameBit.Length] = 0x00;

                        // 取目录长度,并写入四字节
                        byte[] fileNameLengthBit = PublicFunction.HexToFourByteSet((long)fullPath.Length);

                        // 置压缩前大小
                        byte[] fileActualsizeBit = PublicFunction.HexToFourByteSet(fileActualsize);

                        // 置压缩后大小
                        byte[] fileCompressionsizeBit = PublicFunction.HexToFourByteSet(fileCompressionsize);

                        // -----开始写入目录区数据-----
                        // 写入文件名长度
                        dirArea.AddRange(fileNameLengthBit);
                        
                        // 写入文件名数据
                        dirArea.AddRange(fullPath);
                        
                        // 写入文件偏移
                        dirArea.AddRange(fileOffsetBit);
                        
                        //写入压缩前大小
                        dirArea.AddRange(fileActualsizeBit);
                       
                        //写入压缩后大小
                        dirArea.AddRange(fileCompressionsizeBit);

                        // 取当前数据区长度
                        dataAreaLengh = fs.Position;

                        // 计算并报告进度
                        int percent = (int)(((i + 1) / (double)fileCount) * 97);//数据区处理完成后留3%，用于创建目录表、文件头部
                        progressCallback(new FileProgress { percentage = percent, state = "正在写入:\"" + fileName + "\"" });
                    }

                    // 填充目录区
                    progressCallback(new FileProgress { percentage = 98, state = "正在创建目录表..." }); ;
                    byte[] dirAreaByte = dirArea.ToArray();
                    await fs.WriteAsync(dirAreaByte, 0, dirAreaByte.Length);
                    await Task.Delay(1500);

                    // 填充签名头部
                    await fs.WriteAsync(signStartEnd, 0, signStartEnd.Length);
                   
                    // 填充数据区总长度
                    byte[] dataAreaLenghBit = PublicFunction.HexToFourByteSet(dataAreaLengh);
                    await fs.WriteAsync(dataAreaLenghBit, 0, dataAreaLenghBit.Length);
                    progressCallback(new FileProgress { percentage = 99, state = "正在完成打包..." });

                    // 填充特征
                    //fs.Write(packCharact, 0, packCharact.Length);
                    fs.Write(FillBytes(0x00, packCharact.Length), 0, packCharact.Length);
               
                    // 填充0x00
                    byte[] byteZero = FillBytes(0x00, 160);
                    await fs.WriteAsync(byteZero, 0, byteZero.Length);
                    await Task.Delay(500);

                    // 填充文件总数
                    byte[] fileCountBit = PublicFunction.HexToFourByteSet((long)fileCount);
                    await fs.WriteAsync(fileCountBit, 0, fileCountBit.Length);

                    // 填充签名尾部
                    await fs.WriteAsync(signStartEnd, 0, signStartEnd.Length);

                    await Task.Delay(1500);
                    fs.Close();
                    progressCallback(new FileProgress { percentage = 100, state = "" });
                }

                // 字节填充
                static byte[] FillBytes(byte fillByte, int count)
                {
                    // 创建一个指定大小的字节数组
                    byte[] filledBytes = new byte[count];

                    // 使用循环填充数组
                    for (int i = 0; i < count; i++)
                    {
                        filledBytes[i] = fillByte;
                    }

                    // 返回填充后的字节数组
                    return filledBytes;
                }
            }




            /// <summary>
            /// &lt;List&gt; 遍历文件列表
            /// <param name="directoryPath">(文本型 欲打包的目录)</param>
            /// <returns><para>成功返回List</para></returns>
            /// </summary>
            private static List<string> GetAllFilesInDirectory(string directoryPath)
            {
                //初始化List
                List<string> fileList = new List<string>();

                try
                {
                    // 获取当前目录的所有文件，并添加到文件列表中
                    fileList.AddRange(Directory.GetFiles(directoryPath));

                    // 获取当前目录的所有子目录
                    string[] subDirectories = Directory.GetDirectories(directoryPath);

                    // 遍历子目录
                    foreach (string subDirectory in subDirectories)
                    {
                        // 检查子目录是否为空
                        if (Directory.GetFiles(subDirectory).Length > 0 || Directory.GetDirectories(subDirectory).Length > 0)
                        {
                            // 如果子目录不为空，则递归遍历子目录，并将子目录中的文件添加到文件列表中
                            fileList.AddRange(GetAllFilesInDirectory(subDirectory));
                        }
                        // 如果子目录为空，则跳过它（不执行任何操作）
                    }
                }
                catch (Exception ex)
                {
                    // 容错处理
                    throw new Exception("An error occurred while traversing the directory: " + ex.Message);
                }
                return fileList;
            }
        }
        #endregion

        #region PCK解包方法
        public class UnPack()
        {
            /// <summary>
            /// &lt;List&gt; 取PCK内部文件列表
            /// <param name="pckfilePath">(文本型 欲获取内部文件列表的PCK文件) </param>
            /// <returns><para>成功返回PCK文件的单个文件的目录长度、完整文件路径、文件偏移、实际大小、压缩大小，并封装在 &lt;List&gt;中</para></returns>
            /// </summary>
            public static List<Tuple<int, string, long, long, long>> GetPCKInformation(string pckfilePath)
            {
                // 定义一个五元组的<List>，用于返回文件信息，Tuple元素内容依次为：文件路径长度，文件路径，文件偏移，文件真实大小，文件物理大小
                List<Tuple<int, string, long, long, long>> returnList = new List<Tuple<int, string, long, long, long>>();

                // 定义PCK文件特征区大小
                int pckSignatureSize = 272;

                // 取PACK文件大小
                FileInfo fileInfo = new FileInfo(pckfilePath);
                long pckfileSize = fileInfo.Length;

                // 调试输出PCK文件大小
                Console.WriteLine("PCK文件大小：" + fileInfo.Length + " 字节（十六进制：" + fileInfo.Length.ToString("X") + "）");

                // 开始解析文件
                using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
                {
                    // 取PCK特征区数据
                    byte[] pckSignatureByte = new byte[pckSignatureSize];
                    fs.Seek(pckSignatureSize * -1, SeekOrigin.End);
                    fs.Read(pckSignatureByte, 0, pckSignatureSize);

                    // 定义文件魔数
                    /*byte[] magNum = { 0x41, 0x6E, 0x67, 0x65, 0x6C, 0x69, 0x63, 0x61, 0x20, 0x46, 0x69, 0x6C, 0x65, 0x20, 0x50, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65 };

                    // 取目标PCK文件魔数
                    byte[] existMagNum = new byte[magNum.Length];
                    Array.Copy(pckSignatureByte, 8, existMagNum, 0, existMagNum.Length);

                    // 检查是否为PCK文件
                    if (!existMagNum.SequenceEqual(magNum))
                    {
                        throw new Exception("The file\"" + pckfilePath + "\" is not a valid PCK format file!");
                    }*/

                    // PCK魔数
                    byte[] magNum = { 0x03, 0x00, 0x01, 0x00 };

                    // 取目标PCK文件魔数
                    byte[] signatureByteStartWith = new byte[magNum.Length];
                    Array.Copy(pckSignatureByte, 0, signatureByteStartWith, 0, signatureByteStartWith.Length);
                    byte[] signatureByteEndWith = new byte[magNum.Length];
                    Array.Copy(pckSignatureByte, 268, signatureByteEndWith, 0, signatureByteEndWith.Length);

                    // 检查是否为PCK文件
                    if (!signatureByteStartWith.SequenceEqual(magNum) || !signatureByteEndWith.SequenceEqual(magNum))
                    {
                        throw new Exception("The file\"" + pckfilePath + "\" is not a valid PCK format file!");
                    }

                    // 处理数据区物理大小
                    byte[] dataSizeBit = new byte[4];      // 字节集状态下的数据区物理大小数值
                    long dataSize;                         // 长整型的数据区物理大小数值               
                                                           // 定位到文件长度减去PCK文件特征区大小再减去4个字节的位置

                    // 取数据区物理大小数值信息              
                    Array.Copy(pckSignatureByte, 4, dataSizeBit, 0, dataSizeBit.Length);
                    // 将取到的物理大小数值信息补满八字节，然后转换成长整型数值
                    dataSize = BitConverter.ToInt64(PublicFunction.EightByteConverter(dataSizeBit), 0);
                    // 调试输出数据区物理大小
                    Console.WriteLine("该文件的数据区物理大小为 " + dataSize.ToString() + " 字节（十六进制:" + dataSize.ToString("X") + "）");

                    // 处理PCK内部文件文件数量
                    byte[] fileCountBit = new byte[4];      // 字节集状态下的数据区物理大小数值
                    long fileCount;                         // 长整型的数据区物理大小数值
                                                            // 取PCK内部文件数量信息
                    Array.Copy(pckSignatureByte, 264, fileCountBit, 0, fileCountBit.Length);
                    // 将取到的PCK内部文件信息补满八字节，然后转换成长整型数值
                    fileCount = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileCountBit), 0);
                    // 调试输出包内文件数量
                    Console.WriteLine("该PCK文件包应有 " + fileCount.ToString() + " 个文件（十六进制:" + fileCount.ToString("X") + "）");

                    // 开始处理目录区             
                    byte[] fileIdxByte = new byte[pckfileSize - dataSize - pckSignatureSize];        // 文件索引数据
                                                                                                     // 定位到文件索引区的首个字节，即数据区物理大小的值
                    fs.Seek(dataSize, SeekOrigin.Begin);
                    // 开始填充文件索引数据
                    fs.Read(fileIdxByte, 0, fileIdxByte.Length);
                    // 调试输出文件索引数据大小
                    Console.WriteLine("该PCK文件包的文件索引大小为 " + fileIdxByte.Length.ToString() + " 字节（十六进制:" + fileIdxByte.Length.ToString("X") + "）");

                    // 开始创建文件列表
                    int byteOffset = 0;        //字节偏移记录

                    // 开始for循环，循环次数为文件数量
                    for (int i = 0; i < fileCount; i++)
                    {
                        // 取文件目录文本长度
                        byte[] filePathLengthBit = new byte[4];
                        // 从当前偏移位置开始拷贝四个字节
                        Array.Copy(fileIdxByte, byteOffset, filePathLengthBit, 0, filePathLengthBit.Length);
                        // 四字节补到八字节，再转换成整型数值
                        int filePathLength = BitConverter.ToInt32(PublicFunction.EightByteConverter(filePathLengthBit), 0);
                        //字节偏移移到目录的第一个字符
                        byteOffset += 4;

                        // 取文件目录，因为所有目录文本字节后面都填充了一个00，因此要减1
                        byte[] filePathStringBit = new byte[filePathLength - 1];
                        // 从当前的字节偏移位置拷贝文件目录文本字节数据，文本长度变量filePathLength来决定
                        Array.Copy(fileIdxByte, byteOffset, filePathStringBit, 0, filePathStringBit.Length);
                        // 将文件目录文本字节数据转换为文本型数据，包内文件名可能含有中文，需使用.Net框架提供的默认编码，否则中文部分会乱码
                        string filePathString = Encoding.Default.GetString(filePathStringBit);
                        //字节偏移移到当前文件偏移的字节
                        byteOffset += filePathLength;

                        // 取文件偏移
                        byte[] fileOffsetBit = new byte[4];
                        // 从当前偏移位置开始拷贝四个字节
                        Array.Copy(fileIdxByte, byteOffset, fileOffsetBit, 0, fileOffsetBit.Length);
                        // 四字节补到八字节，再转换成长整型数值，
                        long fileOffset = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileOffsetBit), 0);
                        //字节偏移到文件实际大小的字节
                        byteOffset += 4;

                        // 取文件实际大小
                        byte[] fileActualsizeBit = new byte[4];
                        // 从当前偏移位置开始拷贝四个字节
                        Array.Copy(fileIdxByte, byteOffset, fileActualsizeBit, 0, fileActualsizeBit.Length);
                        // 四字节补到八字节，再转换成长整型数值，
                        long fileActualsize = BitConverter.ToInt64(PublicFunction.EightByteConverter(fileActualsizeBit), 0);
                        //字节偏移到文件压缩后大小的字节                 
                        byteOffset += 4;

                        // 取文件压缩大小
                        byte[] filecompressionsizeBit = new byte[4];
                        // 从当前偏移位置开始拷贝四个字节
                        Array.Copy(fileIdxByte, byteOffset, filecompressionsizeBit, 0, filecompressionsizeBit.Length);
                        // 四字节补到八字节，再转换成长整型数值，                    
                        long filecompressionsize = BitConverter.ToInt64(PublicFunction.EightByteConverter(filecompressionsizeBit), 0);
                        //字节偏移到下一个文件长度信息字节，为下一次循环做准备                      
                        byteOffset += 4;

                        // 将获取到的信息添加到五元组的<List>returnList
                        returnList.Add(Tuple.Create(filePathLength, filePathString, fileOffset, fileActualsize, filecompressionsize));
                    }
                    // 关掉文件流以节约内存
                    fs.Close();
                }
                // 循环结束将五元组的<List>returnList返回
                return returnList;
            }


            /// <summary>
            /// &lt;List&gt; 解压所有文件
            /// <param name="pckfilePath">(文本型 欲单解压的PCK文件, </param>
            /// <param name="fileList">List 已处理好的文件列表, </param>
            /// <param name="savePath">欲保存的路径)</param>
            /// </summary>
            public static void ExtractAllFile(string pckfilePath, List<Tuple<int, string, long, long, long>> fileList, string savePath)
            {
                // 取文件数量，即List成员数
                int fileCount = fileList.Count;

                // 设置解压文件夹，保存到文件名+_PCKUnpacked中
                string extractFolder = Path.GetFileNameWithoutExtension(pckfilePath) + "_PCKUnpacked";
                Directory.CreateDirectory(extractFolder);

                //开始解压操作
                using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
                {
                    // 进入for循环
                    for (int i = 0; i < fileCount; i++)
                    {
                        // 取文件名和文件路径
                        string filePath = fileList[i].Item2;
                        string fileDir = Path.GetDirectoryName(filePath);
                        string fileName = Path.GetFileName(filePath);

                        // 置该文件最终绝对路径
                        string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                        long pckOffset = fileList[i].Item3;                     //PCK文件偏移
                        long actualDataSize = fileList[i].Item4;                // 指定文件的真实大小
                        long compressionDataSize = fileList[i].Item5;           // 指定文件的物理大小
                        byte[] actualData = new byte[actualDataSize];                      // 指定文件的物理数据字节集数组
                        byte[] compressionData = new byte[compressionDataSize]; // 指定文件的物理数据字节集数组
                        fs.Seek(pckOffset, SeekOrigin.Begin);
                        fs.Read(compressionData, 0, (int)compressionDataSize);
                        // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                        if (actualDataSize == compressionDataSize)
                        {
                            actualData = compressionData;
                        }
                        else
                        {
                            try
                            {

                                actualData = ZlibHelper.Decompress(compressionData, actualDataSize);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Zlib解压错误！错误代码:\n" + ex.Message, "解压错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                        // 检查目录是否存在，不存在则创建
                        if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                        }

                        // 此轮工作已结束，写到文件
                        File.WriteAllBytes(finalPath, actualData);
                    }
                    // 关掉文件流以节约内存
                    fs.Close();
                }
            }

            /// <summary>
            /// &lt;List&gt; 异步解压所有文件
            /// <param name="pckfilePath">(文本型 欲解压的PCK文件, </param>
            /// <param name="fileList">List 已处理好的文件列表, </param>
            /// <param name="savePath">欲保存的路径)</param>
            /// </summary>
            public static async Task ExtractAllFileAsync(string pckfilePath, List<Tuple<int, string, long, long, long>> targetFile, string savePath, Action<FileProgress> progressCallback)
            {
                // 取文件数量，即List成员数
                int fileCount = targetFile.Count;
                string extractFolder = Path.GetFileNameWithoutExtension(pckfilePath) + "_PCKUnpacked";
                //Directory.CreateDirectory(extractFolder);
                using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
                {
                    for (int i = 0; i < fileCount; i++)
                    {
                        // 取文件名和文件路径
                        string filePath = targetFile[i].Item2;
                        string fileDir = Path.GetDirectoryName(filePath);
                        string fileName = Path.GetFileName(filePath);
                        string finalPath = savePath + "\\" + extractFolder + "\\" + filePath;
                        long pckOffset = targetFile[i].Item3; // PCK文件偏移
                        long actualDataSize = targetFile[i].Item4; // 指定文件的真实大小
                        long compressionDataSize = targetFile[i].Item5; // 指定文件的物理大小
                        byte[] actualData = new byte[actualDataSize]; // 指定文件的物理数据字节集数组
                        byte[] compressionData = new byte[compressionDataSize]; // 指定文件的物理数据字节集数组

                        fs.Seek(pckOffset, SeekOrigin.Begin);
                        await fs.ReadAsync(compressionData, 0, (int)compressionDataSize);
                        // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                        if (actualDataSize == compressionDataSize)
                        {
                            actualData = compressionData;
                        }
                        else
                        {
                            try
                            {
                                actualData = ZlibHelper.Decompress(compressionData, actualDataSize);
                            }
                            catch (Exception ex)
                            {
                                ErrorRecorder.WriteErrLog(savePath + "\\" + extractFolder, filePath, ex.Message);
                                MessageBox.Show("Zlib解压错误！\n无法解压 \"" + filePath + " \"\n错误代码:" + ex.Message + "\n错误以记载到日志文件", "解压错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // 检查目录是否存在，不存在则创建
                        if (!Directory.Exists(Path.GetDirectoryName(finalPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(finalPath));
                        }

                        File.WriteAllBytes(finalPath, actualData);

                        // 计算并报告进度
                        int percent = (int)(((i + 1) / (double)fileCount) * 100);
                        progressCallback(new FileProgress { percentage = percent, state = $"正在解包 \"{filePath}\"" });

                    }

                    // 关掉文件流以节约内存
                    fs.Close();
                }
            }



            /// <summary>
            /// 解压单独文件
            /// <param name="pckfilePath">(文本型 欲单独解压的PCK文件, </param>
            /// <param name="fileList">List 已处理好的文件列表, </param>
            /// <param name="targetNum">(欲解压的文件顺序号, </param>
            /// <param name="savePath">欲保存的路径)</param>
            /// </summary>
            public static void ExtractFileSingle(string pckfilePath, List<Tuple<int, string, long, long, long>> fileList, int targetNum, string savePath)
            {
                // 取单独的文件名
                string fileName = Path.GetFileName(fileList[targetNum].Item2);

                // 开始获取指定文件的物理数据
                long pckOffset = fileList[targetNum].Item3;               //PCK文件偏移
                long actualDataSize = fileList[targetNum].Item4;          // 指定文件的真实大小
                long compressionDataSize = fileList[targetNum].Item5;     // 指定文件的物理大小
                byte[] actualData = new byte[actualDataSize];                                          // 指定文件的物理数据字节集数组
                byte[] compressionData = new byte[compressionDataSize];     // 指定文件的物理数据字节集数组

                // 将物理数据拷贝到文件的物理数据字节集数组中

                using (FileStream fs = new FileStream(pckfilePath, FileMode.Open))
                {
                    fs.Seek(pckOffset, SeekOrigin.Begin);
                    fs.Read(compressionData, 0, (int)compressionDataSize);
                    // 由于极少数文件未压缩，将导致ZLib抛出数据无法识别的异常，因此需要加判断，判断依据为检查压缩前后大小
                    if (actualDataSize == compressionDataSize)
                    {
                        actualData = compressionData;
                    }
                    else
                    {
                        try
                        {
                            actualData = ZlibHelper.Decompress(compressionData, actualDataSize);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Zlib decompression error:" + ex.Message);

                        }
                    }
                    File.WriteAllBytes(savePath, actualData);
                    // 关掉文件流以节约内存
                    fs.Close();
                }
            }
        }
        #endregion
    }
}

