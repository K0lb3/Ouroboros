// Decompiled with JetBrains decompiler
// Type: MsgPack.MsgPackWriter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;

namespace MsgPack
{
  public class MsgPackWriter
  {
    private Encoder _encoder = Encoding.UTF8.GetEncoder();
    private byte[] _tmp = new byte[9];
    private byte[] _buf = new byte[64];
    private Stream _strm;

    public MsgPackWriter(Stream strm)
    {
      this._strm = strm;
    }

    public void Write(byte x)
    {
      if ((int) x < 128)
      {
        this._strm.WriteByte(x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 204;
        tmp[1] = x;
        this._strm.Write(tmp, 0, 2);
      }
    }

    public void Write(ushort x)
    {
      if ((int) x < 256)
      {
        this.Write((byte) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 205;
        tmp[1] = (byte) ((uint) x >> 8);
        tmp[2] = (byte) x;
        this._strm.Write(tmp, 0, 3);
      }
    }

    public void Write(char x)
    {
      this.Write((ushort) x);
    }

    public void Write(uint x)
    {
      if (x < 65536U)
      {
        this.Write((ushort) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 206;
        tmp[1] = (byte) (x >> 24);
        tmp[2] = (byte) (x >> 16);
        tmp[3] = (byte) (x >> 8);
        tmp[4] = (byte) x;
        this._strm.Write(tmp, 0, 5);
      }
    }

    public void Write(ulong x)
    {
      if (x < 4294967296UL)
      {
        this.Write((uint) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 207;
        tmp[1] = (byte) (x >> 56);
        tmp[2] = (byte) (x >> 48);
        tmp[3] = (byte) (x >> 40);
        tmp[4] = (byte) (x >> 32);
        tmp[5] = (byte) (x >> 24);
        tmp[6] = (byte) (x >> 16);
        tmp[7] = (byte) (x >> 8);
        tmp[8] = (byte) x;
        this._strm.Write(tmp, 0, 9);
      }
    }

    public void Write(sbyte x)
    {
      if ((int) x >= -32 && (int) x <= -1)
        this._strm.WriteByte((byte) (224U | (uint) (byte) x));
      else if ((int) x >= 0 && (int) x <= (int) sbyte.MaxValue)
      {
        this._strm.WriteByte((byte) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 208;
        tmp[1] = (byte) x;
        this._strm.Write(tmp, 0, 2);
      }
    }

    public void Write(short x)
    {
      if ((int) x >= (int) sbyte.MinValue && (int) x <= (int) sbyte.MaxValue)
      {
        this.Write((sbyte) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 209;
        tmp[1] = (byte) ((uint) x >> 8);
        tmp[2] = (byte) x;
        this._strm.Write(tmp, 0, 3);
      }
    }

    public void Write(int x)
    {
      if (x >= (int) short.MinValue && x <= (int) short.MaxValue)
      {
        this.Write((short) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 210;
        tmp[1] = (byte) (x >> 24);
        tmp[2] = (byte) (x >> 16);
        tmp[3] = (byte) (x >> 8);
        tmp[4] = (byte) x;
        this._strm.Write(tmp, 0, 5);
      }
    }

    public void Write(long x)
    {
      if (x >= (long) int.MinValue && x <= (long) int.MaxValue)
      {
        this.Write((int) x);
      }
      else
      {
        byte[] tmp = this._tmp;
        tmp[0] = (byte) 211;
        tmp[1] = (byte) (x >> 56);
        tmp[2] = (byte) (x >> 48);
        tmp[3] = (byte) (x >> 40);
        tmp[4] = (byte) (x >> 32);
        tmp[5] = (byte) (x >> 24);
        tmp[6] = (byte) (x >> 16);
        tmp[7] = (byte) (x >> 8);
        tmp[8] = (byte) x;
        this._strm.Write(tmp, 0, 9);
      }
    }

    public void WriteNil()
    {
      this._strm.WriteByte((byte) 192);
    }

    public void Write(bool x)
    {
      this._strm.WriteByte(!x ? (byte) 194 : (byte) 195);
    }

    public void Write(float x)
    {
      byte[] bytes = BitConverter.GetBytes(x);
      byte[] tmp = this._tmp;
      tmp[0] = (byte) 202;
      if (BitConverter.IsLittleEndian)
      {
        tmp[1] = bytes[3];
        tmp[2] = bytes[2];
        tmp[3] = bytes[1];
        tmp[4] = bytes[0];
      }
      else
      {
        tmp[1] = bytes[0];
        tmp[2] = bytes[1];
        tmp[3] = bytes[2];
        tmp[4] = bytes[3];
      }
      this._strm.Write(tmp, 0, 5);
    }

    public void Write(double x)
    {
      byte[] bytes = BitConverter.GetBytes(x);
      byte[] tmp = this._tmp;
      tmp[0] = (byte) 203;
      if (BitConverter.IsLittleEndian)
      {
        tmp[1] = bytes[7];
        tmp[2] = bytes[6];
        tmp[3] = bytes[5];
        tmp[4] = bytes[4];
        tmp[5] = bytes[3];
        tmp[6] = bytes[2];
        tmp[7] = bytes[1];
        tmp[8] = bytes[0];
      }
      else
      {
        tmp[1] = bytes[0];
        tmp[2] = bytes[1];
        tmp[3] = bytes[2];
        tmp[4] = bytes[3];
        tmp[5] = bytes[4];
        tmp[6] = bytes[5];
        tmp[7] = bytes[6];
        tmp[8] = bytes[7];
      }
      this._strm.Write(tmp, 0, 9);
    }

    public void Write(byte[] bytes)
    {
      this.WriteRawHeader(bytes.Length);
      this._strm.Write(bytes, 0, bytes.Length);
    }

    public void WriteRawHeader(int N)
    {
      this.WriteLengthHeader(N, 32, (byte) 160, (byte) 218, (byte) 219);
    }

    public void WriteArrayHeader(int N)
    {
      this.WriteLengthHeader(N, 16, (byte) 144, (byte) 220, (byte) 221);
    }

    public void WriteMapHeader(int N)
    {
      this.WriteLengthHeader(N, 16, (byte) 128, (byte) 222, (byte) 223);
    }

    private void WriteLengthHeader(int N, int fix_length, byte fix_prefix, byte len16bit_prefix, byte len32bit_prefix)
    {
      if (N < fix_length)
      {
        this._strm.WriteByte((byte) ((uint) fix_prefix | (uint) N));
      }
      else
      {
        byte[] tmp = this._tmp;
        int count;
        if (N < 65536)
        {
          tmp[0] = len16bit_prefix;
          tmp[1] = (byte) (N >> 8);
          tmp[2] = (byte) N;
          count = 3;
        }
        else
        {
          tmp[0] = len32bit_prefix;
          tmp[1] = (byte) (N >> 24);
          tmp[2] = (byte) (N >> 16);
          tmp[3] = (byte) (N >> 8);
          tmp[4] = (byte) N;
          count = 5;
        }
        this._strm.Write(tmp, 0, count);
      }
    }

    public void Write(string x)
    {
      this.Write(x, false);
    }

    public void Write(string x, bool highProbAscii)
    {
      this.Write(x, this._buf, highProbAscii);
    }

    public void Write(string x, byte[] buf)
    {
      this.Write(x, buf, false);
    }

    public void Write(string x, byte[] buf, bool highProbAscii)
    {
      Encoder encoder = this._encoder;
      char[] charArray = x.ToCharArray();
      if (highProbAscii && x.Length <= buf.Length)
      {
        bool flag = true;
        for (int index = 0; index < x.Length; ++index)
        {
          int num = (int) x[index];
          if (num > (int) sbyte.MaxValue)
          {
            flag = false;
            break;
          }
          buf[index] = (byte) num;
        }
        if (flag)
        {
          this.WriteRawHeader(x.Length);
          this._strm.Write(buf, 0, x.Length);
          return;
        }
      }
      this.WriteRawHeader(encoder.GetByteCount(charArray, 0, x.Length, true));
      int length = x.Length;
      bool completed = true;
      int charIndex = 0;
      while (length > 0 || !completed)
      {
        int charsUsed;
        int bytesUsed;
        encoder.Convert(charArray, charIndex, length, buf, 0, buf.Length, false, out charsUsed, out bytesUsed, out completed);
        this._strm.Write(buf, 0, bytesUsed);
        length -= charsUsed;
        charIndex += charsUsed;
      }
    }
  }
}
