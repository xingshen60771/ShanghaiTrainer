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
            /// 子弹递减指令静态地址：shanghai.exe+2F377 = 0x0042F377
            /// 原始指令：49 (dec ecx)
            /// </summary>
            public const int addressAmmoDecrease = 0x0042F377;

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

            /// <summary>
            /// 武器连发冷却时间
            /// 数值越小，射速越快
            /// </summary>
            public class WeaponFireCooldown
            {
                /// <summary>
                /// 手枪
                /// </summary>
                public const float weapon00 = 0.25f;

                /// <summary>
                /// 步枪
                /// </summary>
                public const float weapon01 = 0.50f;

                /// <summary>
                /// 手榴弹
                /// 暂时不启用连发修改，先保留参数
                /// </summary>
                public const float weapon02 = 1.25f;

                /// <summary>
                /// 冲锋枪
                /// </summary>
                public const float weapon03 = 0.10f;

                /// <summary>
                /// 马克沁重机枪
                /// </summary>
                public const float weapon04 = 1.0f / 7.0f;

                /// <summary>
                /// 巴祖卡
                /// </summary>
                public const float weapon05 = 1.0f / 0.6f;

                /// <summary>
                /// 轻机枪
                /// </summary>
                public const float weapon06 = 1.0f / 9.0f;
            }

            /// <summary>
            /// 武器连发冷却时间偏移
            /// </summary>
            public class WeaponFireCooldownOffset
            {
                /// <summary>
                /// 玩家对象 -> 武器管理器
                /// </summary>
                public const int offsetWeaponManager = 0x5C;

                /// <summary>
                /// 武器管理器 -> 武器列表
                /// </summary>
                public const int offsetWeaponList = 0x10;

                /// <summary>
                /// 手枪武器对象指针
                /// </summary>
                public const int offsetWeapon00 = 0x100;

                /// <summary>
                /// 步枪武器对象指针
                /// </summary>
                public const int offsetWeapon01 = 0x104;

                /// <summary>
                /// 手榴弹武器对象指针
                /// 暂时不启用修改，先保留偏移
                /// </summary>
                public const int offsetWeapon02 = 0x108;

                /// <summary>
                /// 冲锋枪武器对象指针
                /// </summary>
                public const int offsetWeapon03 = 0x10C;

                /// <summary>
                /// 马克沁重机枪武器对象指针
                /// </summary>
                public const int offsetWeapon04 = 0x110;

                /// <summary>
                /// 巴祖卡武器对象指针
                /// </summary>
                public const int offsetWeapon05 = 0x114;

                /// <summary>
                /// 轻机枪武器对象指针
                /// </summary>
                public const int offsetWeapon06 = 0x118;

                /// <summary>
                /// 武器对象内部的真实连发冷却时间
                /// Float
                /// </summary>
                public const int offsetCooldown = 0x04;
            }
        }
    }
}

