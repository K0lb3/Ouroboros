namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MyEncrypt
    {
        public static int EncryptCount;
        public static int DecryptCount;

        static MyEncrypt()
        {
        }

        public MyEncrypt()
        {
            base..ctor();
            return;
        }

        public static byte[] Decrypt(byte[] data)
        {
            byte[] buffer;
            if (data != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            buffer = new byte[(int) data.Length];
            Array.Copy(data, buffer, (int) data.Length);
            GUtility.Decrypt(buffer, (int) buffer.Length);
            return buffer;
        }

        public static string Decrypt(int seed, byte[] data, bool decompress)
        {
            if (data != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            GUtility.Decrypt(data, (int) data.Length);
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] Encrypt(byte[] msg)
        {
            byte[] buffer;
            if (msg != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            buffer = new byte[(int) msg.Length];
            Array.Copy(msg, buffer, (int) msg.Length);
            GUtility.Encrypt(buffer, (int) buffer.Length);
            return buffer;
        }

        public static byte[] Encrypt(int seed, string msg, bool compress)
        {
            byte[] buffer;
            if (string.IsNullOrEmpty(msg) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            buffer = Encoding.UTF8.GetBytes(msg);
            GUtility.Encrypt(buffer, (int) buffer.Length);
            return buffer;
        }
    }
}

