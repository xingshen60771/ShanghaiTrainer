using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShanghaiTrainer
{
    /// <summary>
    /// 钩子相关操作
    /// </summary>
    public class KeyboardHookLib : IDisposable
    {
        // 定义快捷键组合枚举
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Shift = 1
        }

        // 钩子委托声明
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Win32 API声明
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private IntPtr _hookId = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;

        // 快捷键事件
        public event Action<int> HotkeyPressed;

        public KeyboardHookLib()
        {
            _proc = HookCallback;
            InstallHook();
        }

        /// <summary>
        /// 安装全局钩子
        /// </summary>
        private void InstallHook()
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                _hookId = SetWindowsHookEx(WH_KEYBOARD_LL, _proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// 钩子回调处理
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                bool shiftPressed = (GetKeyState((int)Keys.ShiftKey) & 0x8000) != 0;

                // 检查功能键F1-F7 + Shift组合
                if (shiftPressed && vkCode >= (int)Keys.F1 && vkCode <= (int)Keys.F7)
                {
                    int functionNumber = vkCode - (int)Keys.F1 + 1;
                    HotkeyPressed?.Invoke(functionNumber);
                }
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (_hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
        }
    }
}
