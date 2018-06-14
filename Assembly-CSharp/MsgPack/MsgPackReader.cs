// Decompiled with JetBrains decompiler
// Type: MsgPack.MsgPackReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;

namespace MsgPack
{
  public class MsgPackReader
  {
    private byte[] _tmp0 = new byte[8];
    private byte[] _tmp1 = new byte[8];
    private Encoding _encoding = Encoding.UTF8;
    private byte[] _buf = new byte[64];
    private Stream _strm;

    public MsgPackReader(Stream strm)
    {
      this._strm = strm;
    }

    public TypePrefixes Type { get; private set; }

    public bool ValueBoolean { get; private set; }

    public uint Length { get; private set; }

    public uint ValueUnsigned { get; private set; }

    public ulong ValueUnsigned64 { get; private set; }

    public int ValueSigned { get; private set; }

    public long ValueSigned64 { get; private set; }

    public float ValueFloat { get; private set; }

    public double ValueDouble { get; private set; }

    public bool IsSigned()
    {
      if (this.Type != TypePrefixes.NegativeFixNum && this.Type != TypePrefixes.PositiveFixNum && (this.Type != TypePrefixes.Int8 && this.Type != TypePrefixes.Int16))
        return this.Type == TypePrefixes.Int32;
      return true;
    }

    public bool IsBoolean()
    {
      if (this.Type != TypePrefixes.True)
        return this.Type == TypePrefixes.False;
      return true;
    }

    public bool IsSigned64()
    {
      return this.Type == TypePrefixes.Int64;
    }

    public bool IsUnsigned()
    {
      if (this.Type != TypePrefixes.PositiveFixNum && this.Type != TypePrefixes.UInt8 && this.Type != TypePrefixes.UInt16)
        return this.Type == TypePrefixes.UInt32;
      return true;
    }

    public bool IsUnsigned64()
    {
      return this.Type == TypePrefixes.UInt64;
    }

    public bool IsRaw()
    {
      if (this.Type != TypePrefixes.FixRaw && this.Type != TypePrefixes.Raw16)
        return this.Type == TypePrefixes.Raw32;
      return true;
    }

    public bool IsArray()
    {
      if (this.Type != TypePrefixes.FixArray && this.Type != TypePrefixes.Array16)
        return this.Type == TypePrefixes.Array32;
      return true;
    }

    public bool IsMap()
    {
      if (this.Type != TypePrefixes.FixMap && this.Type != TypePrefixes.Map16)
        return this.Type == TypePrefixes.Map32;
      return true;
    }

    public bool Read()
    {
      byte[] tmp0 = this._tmp0;
      byte[] tmp1 = this._tmp1;
      int num1 = this._strm.ReadByte();
      if (num1 < 0)
        return false;
      this.Type = num1 < 0 || num1 > (int) sbyte.MaxValue ? (num1 < 224 || num1 > (int) byte.MaxValue ? (num1 < 160 || num1 > 191 ? (num1 < 144 || num1 > 159 ? (num1 < 128 || num1 > 143 ? (TypePrefixes) num1 : TypePrefixes.FixMap) : TypePrefixes.FixArray) : TypePrefixes.FixRaw) : TypePrefixes.NegativeFixNum) : TypePrefixes.PositiveFixNum;
      TypePrefixes type = this.Type;
      switch (type)
      {
        case TypePrefixes.Nil:
          return true;
        case TypePrefixes.False:
          this.ValueBoolean = false;
          goto case TypePrefixes.Nil;
        case TypePrefixes.True:
          this.ValueBoolean = true;
          goto case TypePrefixes.Nil;
        case TypePrefixes.Float:
          this._strm.Read(tmp0, 0, 4);
          if (BitConverter.IsLittleEndian)
          {
            tmp1[0] = tmp0[3];
            tmp1[1] = tmp0[2];
            tmp1[2] = tmp0[1];
            tmp1[3] = tmp0[0];
            this.ValueFloat = BitConverter.ToSingle(tmp1, 0);
            goto case TypePrefixes.Nil;
          }
          else
          {
            this.ValueFloat = BitConverter.ToSingle(tmp0, 0);
            goto case TypePrefixes.Nil;
          }
        case TypePrefixes.Double:
          this._strm.Read(tmp0, 0, 8);
          if (BitConverter.IsLittleEndian)
          {
            tmp1[0] = tmp0[7];
            tmp1[1] = tmp0[6];
            tmp1[2] = tmp0[5];
            tmp1[3] = tmp0[4];
            tmp1[4] = tmp0[3];
            tmp1[5] = tmp0[2];
            tmp1[6] = tmp0[1];
            tmp1[7] = tmp0[0];
            this.ValueDouble = BitConverter.ToDouble(tmp1, 0);
            goto case TypePrefixes.Nil;
          }
          else
          {
            this.ValueDouble = BitConverter.ToDouble(tmp0, 0);
            goto case TypePrefixes.Nil;
          }
        case TypePrefixes.UInt8:
          int num2 = this._strm.ReadByte();
          if (num2 < 0)
            throw new FormatException();
          this.ValueUnsigned = (uint) num2;
          goto case TypePrefixes.Nil;
        case TypePrefixes.UInt16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.ValueUnsigned = (uint) tmp0[0] << 8 | (uint) tmp0[1];
          goto case TypePrefixes.Nil;
        case TypePrefixes.UInt32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.ValueUnsigned = (uint) ((int) tmp0[0] << 24 | (int) tmp0[1] << 16 | (int) tmp0[2] << 8) | (uint) tmp0[3];
          goto case TypePrefixes.Nil;
        case TypePrefixes.UInt64:
          if (this._strm.Read(tmp0, 0, 8) != 8)
            throw new FormatException();
          this.ValueUnsigned64 = (ulong) ((long) tmp0[0] << 56 | (long) tmp0[1] << 48 | (long) tmp0[2] << 40 | (long) tmp0[3] << 32 | (long) tmp0[4] << 24 | (long) tmp0[5] << 16 | (long) tmp0[6] << 8) | (ulong) tmp0[7];
          goto case TypePrefixes.Nil;
        case TypePrefixes.Int8:
          int num3 = this._strm.ReadByte();
          if (num3 < 0)
            throw new FormatException();
          this.ValueSigned = (int) (sbyte) num3;
          goto case TypePrefixes.Nil;
        case TypePrefixes.Int16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.ValueSigned = (int) (short) ((int) tmp0[0] << 8 | (int) tmp0[1]);
          goto case TypePrefixes.Nil;
        case TypePrefixes.Int32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.ValueSigned = (int) tmp0[0] << 24 | (int) tmp0[1] << 16 | (int) tmp0[2] << 8 | (int) tmp0[3];
          goto case TypePrefixes.Nil;
        case TypePrefixes.Int64:
          if (this._strm.Read(tmp0, 0, 8) != 8)
            throw new FormatException();
          this.ValueSigned64 = (long) tmp0[0] << 56 | (long) tmp0[1] << 48 | (long) tmp0[2] << 40 | (long) tmp0[3] << 32 | (long) tmp0[4] << 24 | (long) tmp0[5] << 16 | (long) tmp0[6] << 8 | (long) tmp0[7];
          goto case TypePrefixes.Nil;
        case TypePrefixes.Raw16:
        case TypePrefixes.Array16:
        case TypePrefixes.Map16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.Length = (uint) tmp0[0] << 8 | (uint) tmp0[1];
          goto case TypePrefixes.Nil;
        case TypePrefixes.Raw32:
        case TypePrefixes.Array32:
        case TypePrefixes.Map32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.Length = (uint) ((int) tmp0[0] << 24 | (int) tmp0[1] << 16 | (int) tmp0[2] << 8) | (uint) tmp0[3];
          goto case TypePrefixes.Nil;
        case TypePrefixes.NegativeFixNum:
          this.ValueSigned = (num1 & 31) - 32;
          goto case TypePrefixes.Nil;
        default:
          if (type != TypePrefixes.PositiveFixNum)
          {
            if (type != TypePrefixes.FixMap && type != TypePrefixes.FixArray)
            {
              if (type != TypePrefixes.FixRaw)
                throw new FormatException();
              this.Length = (uint) (num1 & 31);
              goto case TypePrefixes.Nil;
            }
            else
            {
              this.Length = (uint) (num1 & 15);
              goto case TypePrefixes.Nil;
            }
          }
          else
          {
            this.ValueSigned = num1 & (int) sbyte.MaxValue;
            this.ValueUnsigned = (uint) this.ValueSigned;
            goto case TypePrefixes.Nil;
          }
      }
    }

    public int ReadValueRaw(byte[] buf, int offset, int count)
    {
      return this._strm.Read(buf, offset, count);
    }

    public string ReadRawString()
    {
      return this.ReadRawString(this._buf);
    }

    public string ReadRawString(byte[] buf)
    {
      if ((long) this.Length < (long) buf.Length)
      {
        if ((long) this.ReadValueRaw(buf, 0, (int) this.Length) != (long) this.Length)
          throw new FormatException();
        return this._encoding.GetString(buf, 0, (int) this.Length);
      }
      byte[] numArray = new byte[(int) this.Length];
      if (this.ReadValueRaw(numArray, 0, numArray.Length) != numArray.Length)
        throw new FormatException();
      return this._encoding.GetString(numArray);
    }
  }
}
