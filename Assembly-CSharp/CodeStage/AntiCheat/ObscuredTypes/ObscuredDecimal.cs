// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDecimal
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredDecimal : IFormattable, IEquatable<ObscuredDecimal>
  {
    private static long cryptoKey = 209208;
    private long currentCryptoKey;
    private byte[] hiddenValue;
    private Decimal fakeValue;
    private bool inited;

    private ObscuredDecimal(byte[] value)
    {
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = new Decimal(0);
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey)
    {
      ObscuredDecimal.cryptoKey = newKey;
    }

    public static Decimal Encrypt(Decimal value)
    {
      return ObscuredDecimal.Encrypt(value, ObscuredDecimal.cryptoKey);
    }

    public static Decimal Encrypt(Decimal value, long key)
    {
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= key;
      decimalLongBytesUnion.l2 ^= key;
      return decimalLongBytesUnion.d;
    }

    private static byte[] InternalEncrypt(Decimal value)
    {
      return ObscuredDecimal.InternalEncrypt(value, 0L);
    }

    private static byte[] InternalEncrypt(Decimal value, long key)
    {
      long num = key;
      if (num == 0L)
        num = ObscuredDecimal.cryptoKey;
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= num;
      decimalLongBytesUnion.l2 ^= num;
      return new byte[16]{ decimalLongBytesUnion.b1, decimalLongBytesUnion.b2, decimalLongBytesUnion.b3, decimalLongBytesUnion.b4, decimalLongBytesUnion.b5, decimalLongBytesUnion.b6, decimalLongBytesUnion.b7, decimalLongBytesUnion.b8, decimalLongBytesUnion.b9, decimalLongBytesUnion.b10, decimalLongBytesUnion.b11, decimalLongBytesUnion.b12, decimalLongBytesUnion.b13, decimalLongBytesUnion.b14, decimalLongBytesUnion.b15, decimalLongBytesUnion.b16 };
    }

    public static Decimal Decrypt(Decimal value)
    {
      return ObscuredDecimal.Decrypt(value, ObscuredDecimal.cryptoKey);
    }

    public static Decimal Decrypt(Decimal value, long key)
    {
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= key;
      decimalLongBytesUnion.l2 ^= key;
      return decimalLongBytesUnion.d;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDecimal.cryptoKey)
        return;
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(this.InternalDecrypt(), ObscuredDecimal.cryptoKey);
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Decimal num = this.InternalDecrypt();
      this.currentCryptoKey = (long) Random.get_seed();
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(num, this.currentCryptoKey);
    }

    public Decimal GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredDecimal.DecimalLongBytesUnion() { b1 = this.hiddenValue[0], b2 = this.hiddenValue[1], b3 = this.hiddenValue[2], b4 = this.hiddenValue[3], b5 = this.hiddenValue[4], b6 = this.hiddenValue[5], b7 = this.hiddenValue[6], b8 = this.hiddenValue[7], b9 = this.hiddenValue[8], b10 = this.hiddenValue[9], b11 = this.hiddenValue[10], b12 = this.hiddenValue[11], b13 = this.hiddenValue[12], b14 = this.hiddenValue[13], b15 = this.hiddenValue[14], b16 = this.hiddenValue[15] }.d;
    }

    public void SetEncrypted(Decimal encrypted)
    {
      this.inited = true;
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = encrypted;
      this.hiddenValue = new byte[16]
      {
        decimalLongBytesUnion.b1,
        decimalLongBytesUnion.b2,
        decimalLongBytesUnion.b3,
        decimalLongBytesUnion.b4,
        decimalLongBytesUnion.b5,
        decimalLongBytesUnion.b6,
        decimalLongBytesUnion.b7,
        decimalLongBytesUnion.b8,
        decimalLongBytesUnion.b9,
        decimalLongBytesUnion.b10,
        decimalLongBytesUnion.b11,
        decimalLongBytesUnion.b12,
        decimalLongBytesUnion.b13,
        decimalLongBytesUnion.b14,
        decimalLongBytesUnion.b15,
        decimalLongBytesUnion.b16
      };
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private Decimal InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDecimal.cryptoKey;
        this.hiddenValue = ObscuredDecimal.InternalEncrypt(new Decimal(0));
        this.fakeValue = new Decimal(0);
        this.inited = true;
      }
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.b1 = this.hiddenValue[0];
      decimalLongBytesUnion.b2 = this.hiddenValue[1];
      decimalLongBytesUnion.b3 = this.hiddenValue[2];
      decimalLongBytesUnion.b4 = this.hiddenValue[3];
      decimalLongBytesUnion.b5 = this.hiddenValue[4];
      decimalLongBytesUnion.b6 = this.hiddenValue[5];
      decimalLongBytesUnion.b7 = this.hiddenValue[6];
      decimalLongBytesUnion.b8 = this.hiddenValue[7];
      decimalLongBytesUnion.b9 = this.hiddenValue[8];
      decimalLongBytesUnion.b10 = this.hiddenValue[9];
      decimalLongBytesUnion.b11 = this.hiddenValue[10];
      decimalLongBytesUnion.b12 = this.hiddenValue[11];
      decimalLongBytesUnion.b13 = this.hiddenValue[12];
      decimalLongBytesUnion.b14 = this.hiddenValue[13];
      decimalLongBytesUnion.b15 = this.hiddenValue[14];
      decimalLongBytesUnion.b16 = this.hiddenValue[15];
      decimalLongBytesUnion.l1 ^= this.currentCryptoKey;
      decimalLongBytesUnion.l2 ^= this.currentCryptoKey;
      Decimal d = decimalLongBytesUnion.d;
      if (ObscuredCheatingDetector.IsRunning && this.fakeValue != new Decimal(0) && d != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return d;
    }

    public override string ToString()
    {
      return this.InternalDecrypt().ToString();
    }

    public string ToString(string format)
    {
      return this.InternalDecrypt().ToString(format);
    }

    public string ToString(IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(format, provider);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredDecimal))
        return false;
      return this.Equals((ObscuredDecimal) obj);
    }

    public bool Equals(ObscuredDecimal obj)
    {
      return obj.InternalDecrypt().Equals(this.InternalDecrypt());
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public static implicit operator ObscuredDecimal(Decimal value)
    {
      ObscuredDecimal obscuredDecimal = new ObscuredDecimal(ObscuredDecimal.InternalEncrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredDecimal.fakeValue = value;
      return obscuredDecimal;
    }

    public static implicit operator Decimal(ObscuredDecimal value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredDecimal operator ++(ObscuredDecimal input)
    {
      Decimal num = input.InternalDecrypt() + new Decimal(1);
      input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredDecimal operator --(ObscuredDecimal input)
    {
      Decimal num = input.InternalDecrypt() - new Decimal(1);
      input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct DecimalLongBytesUnion
    {
      [FieldOffset(0)]
      public Decimal d;
      [FieldOffset(0)]
      public long l1;
      [FieldOffset(8)]
      public long l2;
      [FieldOffset(0)]
      public byte b1;
      [FieldOffset(1)]
      public byte b2;
      [FieldOffset(2)]
      public byte b3;
      [FieldOffset(3)]
      public byte b4;
      [FieldOffset(4)]
      public byte b5;
      [FieldOffset(5)]
      public byte b6;
      [FieldOffset(6)]
      public byte b7;
      [FieldOffset(7)]
      public byte b8;
      [FieldOffset(8)]
      public byte b9;
      [FieldOffset(9)]
      public byte b10;
      [FieldOffset(10)]
      public byte b11;
      [FieldOffset(11)]
      public byte b12;
      [FieldOffset(12)]
      public byte b13;
      [FieldOffset(13)]
      public byte b14;
      [FieldOffset(14)]
      public byte b15;
      [FieldOffset(15)]
      public byte b16;
    }
  }
}
