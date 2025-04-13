using ComponentAce.Compression.Libs.zlib;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/*
 以下是调用Zlib官方库进行压缩解压操作的方法
    zlib.NET源码略，如需编译请自行到官网获取
 */
namespace ShanghaiTrainer
{
    /// <summary>
    /// Zlib压缩解压操作类
    /// </summary>
    internal static class ZlibHelper
    {
        /// <summary>
        /// &lt;字节集&gt; 压缩字节数据
        /// <param name="input">(字节集 欲压缩的字节集, </param>
        /// <param name="compressionLevel">整数型 压缩等级)</param>
        /// <remarks><para>压缩等级必须在9~9之间</para></remarks>
        /// <returns><para>成功返回压缩后的字节数组</para></returns>
        /// </summary>
        public static byte[] Compress(byte[] input, int compressionLevel)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (compressionLevel < 0 || compressionLevel > 9)
                throw new Exception("压缩等级必须在0~9之间！");

            using (var msOutput = new MemoryStream())
            {
                using (var zlibStream = new ZOutputStream(msOutput, compressionLevel))
                {
                    zlibStream.Write(input, 0, input.Length);
                    zlibStream.Flush();
                    zlibStream.finish();
                }
                return msOutput.ToArray();
            }
        }

        /// <summary>
        /// &lt;字节集&gt; 解压缩字节数据
        /// </summary>
        /// <param name="input">(字节集 压缩后的字节数据, </param>
        /// <param name="expectedSize">长整型 预估解压数据大小)</param>
        /// <remarks><para>参数2不知道可以不填</para></remarks>
        /// <returns><para>成功返回解压缩后的字节数组</para></returns>
        public static byte[] Decompress(byte[] data, long bufferSize=0)
        {
            // 将long转换为int并验证范围
            int expectedLength = checked((int)bufferSize);

            using (var inputStream = new MemoryStream(data))
            using (var decompressor = new ZInputStream(inputStream))
            {
                byte[] resultBuffer = new byte[expectedLength];
                int totalRead = 0;

                // 循环读取确保填满缓冲区
                while (totalRead < expectedLength)
                {
                    int bytesRead = decompressor.read(
                        resultBuffer,    // 目标缓冲区
                        totalRead,       // 偏移量
                        expectedLength - totalRead  // 最大读取长度
                    );

                    if (bytesRead <= 0)
                    {
                        if (totalRead < expectedLength)
                        {
                            throw new InvalidDataException(
                                $"解压数据不完整，预期长度 {expectedLength}，实际解压 {totalRead}"
                            );
                        }
                        break;
                    }
                    totalRead += bytesRead;
                }
                return resultBuffer;
            }
        }

        /// <summary>
        /// &lt;字节集&gt; 异步压缩字节数据
        /// <param name="input">(字节集 欲压缩的字节集, </param>
        /// <param name="compressionLevel">整数型 压缩等级)</param>
        /// <remarks><para>压缩等级必须在9~9之间</para></remarks>
        /// <returns><para>成功压缩后的字节数组</para></returns>
        /// </summary>
        public static Task<byte[]> CompressAsync(byte[] input, int compressionLevel = 1)
        {
            return Task.Run(() => Compress(input, compressionLevel));
        }

        /// <summary>
        /// &lt;字节集&gt; 异步解压缩字节数据
        /// </summary>
        /// <param name="input">(字节集 压缩后的字节数据, </param>
        /// <param name="expectedSize">长整型 预估解压数据大小)</param>
        /// <remarks><para>参数2不知道可以不填</para></remarks>
        /// <returns><para>成功返回解压缩后的字节数组</para></returns>
        public static Task<byte[]> DecompressAsync(byte[] input, long expectedSize = 0)
        {
            return Task.Run(() => Decompress(input, expectedSize));
        }
        


    }
}

