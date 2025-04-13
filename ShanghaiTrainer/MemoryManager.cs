using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ShanghaiTrainer
{
    /// <summary>
    /// 内存操作管理类
    /// <para>用于读写游戏内存，保证准确无误的寻找并相关属性</para>
    /// </summary>
    public class MemoryManager
    {
        // 引入必要的Win32 API


        [DllImport("kernel32.dll")]
        static extern bool VirtualProtect(
            IntPtr lpAddress,
            uint dwSize,
            uint flNewProtect,
            out uint lpflOldProtect
        );


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(
            int dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// 进程完全访问权限标志组合
        /// </summary>
        /// <value>
        /// 包含以下权限的组合：
        /// PROCESS_VM_OPERATION(0x0008) | PROCESS_VM_READ(0x0010) | 
        /// PROCESS_VM_WRITE(0x0020) | 其他必要权限
        /// </value>
        const int PROCESS_ALL_ACCESS = 0x001F0FFF;

        /// <summary>
        /// 当前附加进程的操作句柄
        /// </summary>
        /// <remarks>
        /// 生命周期管理：
        /// - 通过OpenProcess获取
        /// - 通过CloseHandle释放
        /// - 无效值为IntPtr.Zero
        /// </remarks>
        private IntPtr _processHandle;

        /// <summary>
        /// &lt;逻辑型&gt; 将内存管理器附加到指定进程
        /// <param name="process">(进程 欲附加的目标进程对象)</param>
        /// <returns><para>成功返回真，否则返回假</para></returns>
        /// <remarks>
        /// <para>需要管理员权限才能获取完整进程访问权限</para>
        /// </remarks>
        /// </summary>
        public bool Attach(Process process)
        {
            // 定义完全访问权限标志（包含读写、操作等权限）
            const int PROCESS_ALL_ACCESS = 0x1F0FFF;
            // 调用OpenProcess API获取进程句柄
            _processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);
            // 验证句柄有效性
            return _processHandle != IntPtr.Zero;
        }

        /// <summary>
        /// &lt;整数型&gt; 从指定内存地址取32位整数
        /// <param name="address"><para>(内存指针 欲读取的内存地址)</para></param>
        /// <returns><para>返回读取到的整数值</para></returns>
        /// <exception cref="Win32Exception">当读取操作失败时抛出</exception>
        /// </summary>
        public int ReadInt(IntPtr address)
        {
            // 分配4字节缓冲区（32位）
            byte[] buffer = new byte[4];
            // 调用底层API读取内存
            ReadProcessMemory(_processHandle, address, buffer, 4, out _);
            // 将字节数组转换为int类型
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// 向指定内存地址写入32位整数
        /// <param name="address">(内存指针 目标内存地址, </param>
        /// <param name="value">整数型 欲写入的整数值)</param>
        /// <exception cref="Win32Exception"><para>当写入操作失败时抛出</para></exception>
        /// </summary>
        public void WriteInt(IntPtr address, int value)
        {
            // 将int转换为字节数组（小端序）
            byte[] buffer = BitConverter.GetBytes(value);
            // 调用底层API写入内存
            WriteProcessMemory(_processHandle, address, buffer, 4, out _);
        }

        /// <summary>
        /// 向指定内存地址写入单个字节
        /// <param name="address">(内存指针 目标内存地址, </param>
        /// <param name="value">整数型 要写入的字节值（0-255）)</param>
        /// <exception cref="ArgumentException"><para>当输入值超过字节范围时抛出<para></exception>
        /// </summary>
        public void WriteByte(IntPtr address, byte value)
        {
            // 创建单字节数组
            byte[] buffer = { value };
            // 调用底层API写入内存（不检查返回值）
            WriteProcessMemory(_processHandle, address, buffer, 1, out _);
        }

        /// <summary>
        /// &lt;内存指针&gt; 解析多级指针链获取最终内存地址
        /// <param name="baseAddress">(整数型 基地址, </param>
        /// <param name="offsets">整数型数组 偏移量数组)</param>
        /// <returns><para>最终解析出的内存地址</para></returns>
        /// </summary>
        public IntPtr ResolvePointerChain(int baseAddress, int[] offsets)
        {
            // 初始化当前地址为基地址
            IntPtr current = (IntPtr)baseAddress;

            // 逐级解析指针链
            foreach (int offset in offsets)
            {
                // 读取当前指针值
                current = (IntPtr)ReadInt(current);
                // 应用偏移量
                current += offset;
            }
            return current;
        }



        uint originalProtect;
        /// <summary>
        /// 内存写保护
        /// <param name="currentWeapon">(内存指针 现行武器, </param>
        /// <param name="enable">逻辑型 是否写保护)</param>
        /// </summary>
        public void ToggleMemoryProtection(IntPtr currentWeapon, bool enable)
        {

            if (enable)
            {
                // 设置为PAGE_READONLY
                VirtualProtect(currentWeapon, 4, 0x02, out originalProtect);
            }
            else
            {
                // 恢复原始权限
                VirtualProtect(currentWeapon, 4, originalProtect, out _);
            }
        }


        /// <summary>
        /// 断开当前附加的进程并释放资源
        /// <remarks>
        /// <para>必须调用此方法释放系统句柄，避免资源泄漏</para>
        /// </remarks>
        /// </summary>
        public void Detach()
        {
            if (_processHandle != IntPtr.Zero)
            {
                // 关闭进程句柄
                CloseHandle(_processHandle);
                // 重置句柄标识
                _processHandle = IntPtr.Zero;
            }
        }
    }
}
