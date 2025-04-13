using System;
using System.IO;

namespace ShanghaiTrainer
{
    /// <summary>
    /// 记录PCK错误日志
    /// </summary>
    internal class ErrorRecorder
    {
        /// <summary>
        /// 写错误日志
        /// <param name="filePath">(文本型 欲保存的路径 ,</param>
        /// <param name="errFile">文本型 出错的文件, </param>
        /// <param name="errCode">文本型 欲写入的错误码)</param>
        /// </summary>
        public static void WriteErrLog(string saveDict, string errFile, string errCode)
        {
            // 置错误日志文件名
            string logFile = saveDict + "\\" + "UNPackErrors.log";

            // 检查日志文件是否存在，不存在则创建
            if (!File.Exists(logFile))
            {
                File.Create(logFile).Close();
            }

            // 定义可能需要用到的符号，将其ASCII码转为字符
            char symbolDash = (char)45;                         //破折号
            char symbolColon = (char)58;                        //冒号
            char symbolSpace = (char)32;                        //空格
            char symbolsquareBrackets_L = (char)91;             //左方括号
            char symbolsquareBrackets_R = (char)93;             //右方括号
            char symbolTab = (char)9;                           //制表符

            // 取现行时间并转换成 xxxx-xx-xx xx:xx:xx
            DateTime nowTime = DateTime.Now;

            // 置错误发生时间
            string errTime = nowTime.Year.ToString("D4") + symbolDash + nowTime.Month.ToString("D2") + symbolDash + nowTime.Day.ToString("D2") + symbolSpace + nowTime.Hour.ToString("D2") + symbolColon + nowTime.Minute.ToString("D2") + symbolColon + nowTime.Second.ToString("D2");

            // 写入错误事件日志
            string errMsg = symbolsquareBrackets_L + errTime + symbolsquareBrackets_R + symbolTab + "Error code:" + errCode + symbolTab + "Error file:" + errFile+"\r\n";
            using (FileStream fs = new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                // 将文件指针移动到文件末尾
                fs.Seek(0, SeekOrigin.End);

                // 要写入的数据
                byte[] data = System.Text.Encoding.UTF8.GetBytes(errMsg);

                // 从当前位置（文件末尾）写入数据
                fs.Write(data, 0, data.Length);

                // 关闭文件流
                fs.Close();
            }
        }
    }
}
