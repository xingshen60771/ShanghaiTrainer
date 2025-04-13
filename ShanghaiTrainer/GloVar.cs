using System.Collections.Generic;
using System;

namespace ShanghaiTrainer
{
    /// <summary>
    /// 全局变量
    /// </summary>
    internal class GloVar
    {
        /// <summary>
        /// 游戏安装路径
        /// </summary>
        public static string gamePath;

        /// <summary>
        /// 修改器数据变量
        /// </summary>
        public class Trainer
        {
            /// <summary>
            /// 修改马克沁之前变量
            /// </summary>
            public static int weaponBeforemod = 0;
            /// <summary>
            /// 弹夹装弹量
            /// </summary>
            public class ammoCount
            {
                /// <summary>
                /// 手枪弹夹装弹量
                /// </summary>
                public int weapon00 = 0xA;      // 10发

                /// <summary>
                /// 步枪弹夹装弹量
                /// </summary>
                public int weapon01 = 0x5;      // 5发

                /// <summary>
                /// 手榴弹装弹量
                /// </summary>
                public int weapon02 = 0x5;      // 5发

                /// <summary>
                /// 冲锋枪弹夹装弹量
                /// </summary>
                public int weapon03 = 0x1E;     // 30发

                /// <summary>
                /// 马克沁重机枪弹夹装弹量
                /// </summary>
                public int weapon04 = 0xF0;     // 240发   

                /// <summary>
                /// 巴祖卡装弹量
                /// </summary>
                public int weapon05 = 0x8;      // 8发

                /// <summary>
                /// 轻机枪弹夹装弹量
                /// </summary>
                public int weapon06 = 0x2D;     // 45发
            }
        }

        public class PCKHelper
        {
            public static string targetPCK;
            // PCK包内文件列表（文件路径长度，文件路径，文件偏移，文件真实大小，文件物理大小）
          public static List<Tuple<int, string, long, long, long>> pckList = new List<Tuple<int, string, long, long, long>>();
        }
    }
}
