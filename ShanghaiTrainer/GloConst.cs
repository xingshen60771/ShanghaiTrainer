namespace ShanghaiTrainer
{
    /// <summary>
    /// 全局常量
    /// </summary>
    internal class GloConst
    {
        /// <summary>
        /// 修改器数据常量
        /// </summary>
        public class Trainer
        {
            /// <summary>
            /// 游戏基址
            /// </summary>
            public const int baseAddress = 0x005DCFE4;

            /// <summary>
            /// 当前武器
            /// </summary>
            public const int offsetCurrentWeapon = 0xE8;

            /// <summary>
            /// 玩家生命条数偏移
            /// </summary>
            public const int offsetPlayerLife = 0x174;

            /// <summary>
            /// 误伤平民偏移
            /// </summary>
            public const int offsetCivilian = 0x1B0;

            /// <summary>
            /// 杀敌得分偏移
            /// </summary>
            public const int offsetKillScore = 0x194;

            /// <summary>
            /// 杀敌数偏移
            /// </summary>
            public const int offsetKills = 0x188;

            /// <summary>
            /// 武器激活状态
            /// </summary>
            public class WeaponActiveState
            {
                /// <summary>
                /// 步枪激活状态
                /// </summary>
                public const int offsetWeapon01 = 0x108;

                /// <summary>
                /// 手榴弹激活状态
                /// </summary>
                public const int offsetWeapon02 = 0x118;

                /// <summary>
                /// 冲锋枪激活状态
                /// </summary>
                public const int offsetWeapon03 = 0x128;

                /// <summary>
                /// 马克沁重机枪激活状态
                /// </summary>
                public const int offsetWeapon04 = 0x138;

                /// <summary>
                /// 巴祖卡激活状态
                /// </summary>
                public const int offsetWeapon05 = 0x148;

                /// <summary>
                /// 轻机枪激活状态
                /// </summary>
                public const int offsetWeapon06 = 0x158;
            }

            /// <summary>
            /// 弹夹地址
            /// </summary>
            public class AmmoAddress
            {
                /// <summary>
                /// 手枪弹夹地址偏移
                /// </summary>
                public const int offsetWeapon00 = 0xEC;

                /// <summary>
                /// 步枪弹夹地址
                /// </summary>
                public const int offsetWeapon01 = 0x10;

                /// <summary>
                /// 手榴弹夹地址
                /// </summary>
                public const int offsetWeapon02 = 0x20;

                /// <summary>
                /// 冲锋枪夹地址
                /// </summary>
                public const int offsetWeapon03 = 0x30;

                /// <summary>
                /// 马克沁重机枪弹夹地址
                /// </summary>
                public const int offsetWeapon04 = 0x40;

                /// <summary>
                /// 巴祖卡夹地址
                /// </summary>
                public const int offsetWeapon05 = 0x50;

                /// <summary>
                /// 轻机枪夹地址
                /// </summary>
                public const int offsetWeapon06 = 0x60;
            }
        }
    }
}

