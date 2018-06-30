namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class ScreenSizeManager : MonoBehaviour
    {
        private IntPtr window_handle;
        private MYRECT window_rect;
        private MYRECT client_rect;
        private int current_client_width;
        private int current_client_height;
        private int next_client_width;
        private int next_client_height;
        private int frame_width;
        private int frame_height;

        public ScreenSizeManager()
        {
            base..ctor();
            return;
        }

        private unsafe void Awake()
        {
            int num;
            int num2;
            int num3;
            this.window_handle = FindWindow(null, Application.get_productName());
            GetClientRect(this.window_handle, &this.client_rect);
            this.next_client_width = this.current_client_width = &this.client_rect.right - &this.client_rect.left;
            this.next_client_height = this.current_client_height = &this.client_rect.bottom - &this.client_rect.top;
            GetWindowRect(this.window_handle, &this.window_rect);
            num = &this.window_rect.right - &this.window_rect.left;
            num2 = &this.window_rect.bottom - &this.window_rect.top;
            this.frame_width = num - this.current_client_width;
            this.frame_height = num2 - this.current_client_height;
            return;
        }

        private unsafe void EditWindowSizeH(int _new_window_height, bool _size_check)
        {
            int num;
            int num2;
            int num3;
            int num4;
            Resolution resolution;
            Resolution resolution2;
            if (IsZoomed(this.window_handle) == null)
            {
                goto Label_0039;
            }
            num = Mathf.Max(_new_window_height, 270);
            num2 = (int) ((((float) num) / 270f) * 480f);
            this.Resize(num2, num);
            goto Label_009C;
        Label_0039:
            num3 = Mathf.Clamp(_new_window_height, 270, &Screen.get_currentResolution().get_height() - this.frame_height);
            num4 = (int) ((((float) num3) / 270f) * 480f);
            if (_size_check == null)
            {
                goto Label_0094;
            }
            if (num4 <= (&Screen.get_currentResolution().get_width() - this.frame_width))
            {
                goto Label_0094;
            }
            this.EditWindowsSizeW(num4, 0);
            return;
        Label_0094:
            this.Resize(num4, num3);
        Label_009C:
            return;
        }

        private unsafe void EditWindowsSizeW(int _new_window_width, bool _size_check)
        {
            int num;
            int num2;
            int num3;
            int num4;
            Resolution resolution;
            Resolution resolution2;
            if (IsZoomed(this.window_handle) == null)
            {
                goto Label_0039;
            }
            num = Mathf.Max(_new_window_width, 480);
            num2 = (int) ((((float) num) / 480f) * 270f);
            this.Resize(num, num2);
            goto Label_009C;
        Label_0039:
            num3 = Mathf.Clamp(_new_window_width, 480, &Screen.get_currentResolution().get_width() - this.frame_width);
            num4 = (int) ((((float) num3) / 480f) * 270f);
            if (_size_check == null)
            {
                goto Label_0094;
            }
            if (num4 <= (&Screen.get_currentResolution().get_height() - this.frame_height))
            {
                goto Label_0094;
            }
            this.EditWindowSizeH(num4, 0);
            return;
        Label_0094:
            this.Resize(num3, num4);
        Label_009C:
            return;
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr FindWindow(string className, string windowName);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern bool GetClientRect(IntPtr hWnd, out MYRECT rect);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int GetWindowRect(IntPtr hWnd, out MYRECT rect);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern bool IsZoomed(IntPtr hWnd);
        private unsafe void LateUpdate()
        {
            int num;
            int num2;
            GetClientRect(this.window_handle, &this.client_rect);
            this.next_client_width = &this.client_rect.right - &this.client_rect.left;
            this.next_client_height = &this.client_rect.bottom - &this.client_rect.top;
            if (this.current_client_width == this.next_client_width)
            {
                goto Label_00BB;
            }
            if (this.current_client_height == this.next_client_height)
            {
                goto Label_00BB;
            }
            num = Mathf.Abs(this.next_client_width - this.current_client_width);
            num2 = Mathf.Abs(this.next_client_height - this.current_client_height);
            if (num < num2)
            {
                goto Label_00AD;
            }
            this.EditWindowsSizeW(this.next_client_width, 1);
            goto Label_00BA;
        Label_00AD:
            this.EditWindowSizeH(this.next_client_height, 1);
        Label_00BA:
            return;
        Label_00BB:
            if (this.current_client_width == this.next_client_width)
            {
                goto Label_00DA;
            }
            this.EditWindowsSizeW(this.next_client_width, 1);
            return;
        Label_00DA:
            if (this.current_client_height == this.next_client_height)
            {
                goto Label_00F9;
            }
            this.EditWindowSizeH(this.next_client_height, 1);
            return;
        Label_00F9:
            return;
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, int bRepaint);
        private unsafe void Resize(int _width, int _height)
        {
            this.current_client_width = _width;
            this.current_client_height = _height;
            GetWindowRect(this.window_handle, &this.window_rect);
            MoveWindow(this.window_handle, &this.window_rect.left, &this.window_rect.top, _width + this.frame_width, _height + this.frame_height, 1);
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MYRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}

