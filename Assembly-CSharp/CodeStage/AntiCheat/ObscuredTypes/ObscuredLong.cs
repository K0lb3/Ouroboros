// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredLong : IFormattable, IEquatable<ObscuredLong>
  {
    private static long cryptoKey = 444442;
    [SerializeField]
    private long currentCryptoKey;
    [SerializeField]
    private long hiddenValue;
    [SerializeField]
    private long fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredLong(long value)
    {
      this.currentCryptoKey = ObscuredLong.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0L;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey)
    {
      ObscuredLong.cryptoKey = newKey;
    }

    public static long Encrypt(long value)
    {
      return ObscuredLong.Encrypt(value, 0L);
    }

    public static long Decrypt(long value)
    {
      return ObscuredLong.Decrypt(value, 0L);
    }

    public static long Encrypt(long value, long key)
    {
      if (key == 0L)
        return value ^ ObscuredLong.cryptoKey;
      return value ^ key;
    }

    public static long Decrypt(long value, long key)
    {
      if (key == 0L)
        return value ^ ObscuredLong.cryptoKey;
      return value ^ key;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredLong.cryptoKey)
        return;
      this.hiddenValue = ObscuredLong.Encrypt(this.InternalDecrypt(), ObscuredLong.cryptoKey);
      this.currentCryptoKey = ObscuredLong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      long num = this.InternalDecrypt();
      this.currentCryptoKey = (long) Random.get_seed();
      this.hiddenValue = ObscuredLong.Encrypt(num, this.currentCryptoKey);
    }

    public long GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(long encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private long InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredLong.cryptoKey;
        this.hiddenValue = ObscuredLong.Encrypt(0L);
        this.fakeValue = 0L;
        this.inited = true;
      }
      long num = ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0L && num != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredLong))
        return false;
      return this.Equals((ObscuredLong) obj);
    }

    public bool Equals(ObscuredLong obj)
    {
      if (this.currentCryptoKey == obj.currentCryptoKey)
        return this.hiddenValue == obj.hiddenValue;
      return ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredLong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
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

    public static implicit operator ObscuredLong(long value)
    {
      ObscuredLong obscuredLong = new ObscuredLong(ObscuredLong.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredLong.fakeValue = value;
      return obscuredLong;
    }

    public static implicit operator long(ObscuredLong value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredLong operator ++(ObscuredLong input)
    {
      long num = input.InternalDecrypt() + 1L;
      input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredLong operator --(ObscuredLong input)
    {
      long num = input.InternalDecrypt() - 1L;
      input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
