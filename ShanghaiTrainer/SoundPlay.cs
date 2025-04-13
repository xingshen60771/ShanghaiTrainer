using System;
using System.IO;
using System.Media;

namespace ShanghaiTrainer
{
    /// <summary>
    /// 提示音播放操作类
    /// <para>用于播放修改器提示音</para>
    /// </summary>
    public class SoundPlay
    {
        /// <summary>
        /// 播放提示音
        /// <param name="soundID">(整数型 欲播放的提示音代码)</param>
        /// <exception cref="Exception"></exception>
        /// </summary>
        public static void PlaySound(int soundID)
        {
            // 只允许是1-3
            if (soundID < 1 || soundID > 3)
            {
                throw new Exception("无效的声音参数！");
            }

            try
            {
                // 初始化音频数据字节集
                byte[] audioData = null;
                switch (soundID)
                {
                    case 1:
                        audioData = Properties.Resources.Sound1;
                        break;
                    case 2:
                        audioData = Properties.Resources.Sound2;
                        break;
                    case 3:
                        audioData = Properties.Resources.Sound3;
                        break;
                }

                using (MemoryStream stream = new MemoryStream(audioData))
                {
                    SoundPlayer player = new SoundPlayer(stream);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放音频失败: {ex.Message}");
            }
        }
    }
}
