// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredFloat
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
  public struct ObscuredFloat : IFormattable, IEquatable<ObscuredFloat>
  {
    private static int cryptoKey = 230887;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private byte[] hiddenValue;
    [SerializeField]
    private float fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredFloat(byte[] value)
    {
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0.0f;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey)
    {
      ObscuredFloat.cryptoKey = newKey;
    }

    public static int Encrypt(float value)
    {
      return ObscuredFloat.Encrypt(value, ObscuredFloat.cryptoKey);
    }

    public static int Encrypt(float value, int key)
    {
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion();
      floatIntBytesUnion.f = value;
      floatIntBytesUnion.i ^= key;
      return floatIntBytesUnion.i;
    }

    private static byte[] InternalEncrypt(float value)
    {
      return ObscuredFloat.InternalEncrypt(value, 0);
    }

    private static byte[] InternalEncrypt(float value, int key)
    {
      int num = key;
      if (num == 0)
        num = ObscuredFloat.cryptoKey;
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion();
      floatIntBytesUnion.f = value;
      floatIntBytesUnion.i ^= num;
      return new byte[4]{ floatIntBytesUnion.b1, floatIntBytesUnion.b2, floatIntBytesUnion.b3, floatIntBytesUnion.b4 };
    }

    public static float Decrypt(int value)
    {
      return ObscuredFloat.Decrypt(value, ObscuredFloat.cryptoKey);
    }

    public static float Decrypt(int value, int key)
    {
      return new ObscuredFloat.FloatIntBytesUnion() { i = (value ^ key) }.f;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredFloat.cryptoKey)
        return;
      this.hiddenValue = ObscuredFloat.InternalEncrypt(this.InternalDecrypt(), ObscuredFloat.cryptoKey);
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      float num = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed();
      this.hiddenValue = ObscuredFloat.InternalEncrypt(num, this.currentCryptoKey);
    }

    public int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredFloat.FloatIntBytesUnion() { b1 = this.hiddenValue[0], b2 = this.hiddenValue[1], b3 = this.hiddenValue[2], b4 = this.hiddenValue[3] }.i;
    }

    public void SetEncrypted(int encrypted)
    {
      this.inited = true;
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion();
      floatIntBytesUnion.i = encrypted;
      this.hiddenValue = new byte[4]
      {
        floatIntBytesUnion.b1,
        floatIntBytesUnion.b2,
        floatIntBytesUnion.b3,
        floatIntBytesUnion.b4
      };
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private float InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredFloat.cryptoKey;
        this.hiddenValue = ObscuredFloat.InternalEncrypt(0.0f);
        this.fakeValue = 0.0f;
        this.inited = true;
      }
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion();
      floatIntBytesUnion.b1 = this.hiddenValue[0];
      floatIntBytesUnion.b2 = this.hiddenValue[1];
      floatIntBytesUnion.b3 = this.hiddenValue[2];
      floatIntBytesUnion.b4 = this.hiddenValue[3];
      floatIntBytesUnion.i ^= this.currentCryptoKey;
      float f = floatIntBytesUnion.f;
      if (ObscuredCheatingDetector.IsRunning && (double) this.fakeValue != 0.0 && (double) Math.Abs(f - this.fakeValue) > (double) ObscuredCheatingDetector.Instance.floatEpsilon)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return f;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredFloat))
        return false;
      return this.Equals((ObscuredFloat) obj);
    }

    public bool Equals(ObscuredFloat obj)
    {
      return ((double) obj.InternalDecrypt()).Equals((double) this.InternalDecrypt());
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
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

    public static implicit operator ObscuredFloat(float value)
    {
      ObscuredFloat obscuredFloat = new ObscuredFloat(ObscuredFloat.InternalEncrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredFloat.fakeValue = value;
      return obscuredFloat;
    }

    public static implicit operator float(ObscuredFloat value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredFloat operator ++(ObscuredFloat input)
    {
      float num = input.InternalDecrypt() + 1f;
      input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredFloat operator --(ObscuredFloat input)
    {
      float num = input.InternalDecrypt() - 1f;
      input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct FloatIntBytesUnion
    {
      [FieldOffset(0)]
      public float f;
      [FieldOffset(0)]
      public int i;
      [FieldOffset(0)]
      public byte b1;
      [FieldOffset(1)]
      public byte b2;
      [FieldOffset(2)]
      public byte b3;
      [FieldOffset(3)]
      public byte b4;
    }
  }
}
