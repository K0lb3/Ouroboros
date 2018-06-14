// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDouble
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
  public struct ObscuredDouble : IFormattable, IEquatable<ObscuredDouble>
  {
    private static long cryptoKey = 210987;
    [SerializeField]
    private long currentCryptoKey;
    [SerializeField]
    private byte[] hiddenValue;
    [SerializeField]
    private double fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredDouble(byte[] value)
    {
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0.0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey)
    {
      ObscuredDouble.cryptoKey = newKey;
    }

    public static long Encrypt(double value)
    {
      return ObscuredDouble.Encrypt(value, ObscuredDouble.cryptoKey);
    }

    public static long Encrypt(double value, long key)
    {
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion();
      doubleLongBytesUnion.d = value;
      doubleLongBytesUnion.l ^= key;
      return doubleLongBytesUnion.l;
    }

    private static byte[] InternalEncrypt(double value)
    {
      return ObscuredDouble.InternalEncrypt(value, 0L);
    }

    private static byte[] InternalEncrypt(double value, long key)
    {
      long num = key;
      if (num == 0L)
        num = ObscuredDouble.cryptoKey;
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion();
      doubleLongBytesUnion.d = value;
      doubleLongBytesUnion.l ^= num;
      return new byte[8]{ doubleLongBytesUnion.b1, doubleLongBytesUnion.b2, doubleLongBytesUnion.b3, doubleLongBytesUnion.b4, doubleLongBytesUnion.b5, doubleLongBytesUnion.b6, doubleLongBytesUnion.b7, doubleLongBytesUnion.b8 };
    }

    public static double Decrypt(long value)
    {
      return ObscuredDouble.Decrypt(value, ObscuredDouble.cryptoKey);
    }

    public static double Decrypt(long value, long key)
    {
      return new ObscuredDouble.DoubleLongBytesUnion() { l = (value ^ key) }.d;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDouble.cryptoKey)
        return;
      this.hiddenValue = ObscuredDouble.InternalEncrypt(this.InternalDecrypt(), ObscuredDouble.cryptoKey);
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      double num = this.InternalDecrypt();
      this.currentCryptoKey = (long) Random.get_seed();
      this.hiddenValue = ObscuredDouble.InternalEncrypt(num, this.currentCryptoKey);
    }

    public long GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredDouble.DoubleLongBytesUnion() { b1 = this.hiddenValue[0], b2 = this.hiddenValue[1], b3 = this.hiddenValue[2], b4 = this.hiddenValue[3], b5 = this.hiddenValue[4], b6 = this.hiddenValue[5], b7 = this.hiddenValue[6], b8 = this.hiddenValue[7] }.l;
    }

    public void SetEncrypted(long encrypted)
    {
      this.inited = true;
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion();
      doubleLongBytesUnion.l = encrypted;
      this.hiddenValue = new byte[8]
      {
        doubleLongBytesUnion.b1,
        doubleLongBytesUnion.b2,
        doubleLongBytesUnion.b3,
        doubleLongBytesUnion.b4,
        doubleLongBytesUnion.b5,
        doubleLongBytesUnion.b6,
        doubleLongBytesUnion.b7,
        doubleLongBytesUnion.b8
      };
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private double InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDouble.cryptoKey;
        this.hiddenValue = ObscuredDouble.InternalEncrypt(0.0);
        this.fakeValue = 0.0;
        this.inited = true;
      }
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion();
      doubleLongBytesUnion.b1 = this.hiddenValue[0];
      doubleLongBytesUnion.b2 = this.hiddenValue[1];
      doubleLongBytesUnion.b3 = this.hiddenValue[2];
      doubleLongBytesUnion.b4 = this.hiddenValue[3];
      doubleLongBytesUnion.b5 = this.hiddenValue[4];
      doubleLongBytesUnion.b6 = this.hiddenValue[5];
      doubleLongBytesUnion.b7 = this.hiddenValue[6];
      doubleLongBytesUnion.b8 = this.hiddenValue[7];
      doubleLongBytesUnion.l ^= this.currentCryptoKey;
      double d = doubleLongBytesUnion.d;
      if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0.0 && Math.Abs(d - this.fakeValue) > 1E-06)
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
      if (!(obj is ObscuredDouble))
        return false;
      return this.Equals((ObscuredDouble) obj);
    }

    public bool Equals(ObscuredDouble obj)
    {
      return obj.InternalDecrypt().Equals(this.InternalDecrypt());
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public static implicit operator ObscuredDouble(double value)
    {
      ObscuredDouble obscuredDouble = new ObscuredDouble(ObscuredDouble.InternalEncrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredDouble.fakeValue = value;
      return obscuredDouble;
    }

    public static implicit operator double(ObscuredDouble value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredDouble operator ++(ObscuredDouble input)
    {
      double num = input.InternalDecrypt() + 1.0;
      input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredDouble operator --(ObscuredDouble input)
    {
      double num = input.InternalDecrypt() - 1.0;
      input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct DoubleLongBytesUnion
    {
      [FieldOffset(0)]
      public double d;
      [FieldOffset(0)]
      public long l;
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
    }
  }
}
