// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredULong
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredULong : IFormattable, IEquatable<ObscuredULong>
  {
    private static ulong cryptoKey = 444443;
    private ulong currentCryptoKey;
    private ulong hiddenValue;
    private ulong fakeValue;
    private bool inited;

    private ObscuredULong(ulong value)
    {
      this.currentCryptoKey = ObscuredULong.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0UL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ulong newKey)
    {
      ObscuredULong.cryptoKey = newKey;
    }

    public static ulong Encrypt(ulong value)
    {
      return ObscuredULong.Encrypt(value, 0UL);
    }

    public static ulong Decrypt(ulong value)
    {
      return ObscuredULong.Decrypt(value, 0UL);
    }

    public static ulong Encrypt(ulong value, ulong key)
    {
      if ((long) key == 0L)
        return value ^ ObscuredULong.cryptoKey;
      return value ^ key;
    }

    public static ulong Decrypt(ulong value, ulong key)
    {
      if ((long) key == 0L)
        return value ^ ObscuredULong.cryptoKey;
      return value ^ key;
    }

    public void ApplyNewCryptoKey()
    {
      if ((long) this.currentCryptoKey == (long) ObscuredULong.cryptoKey)
        return;
      this.hiddenValue = ObscuredULong.Encrypt(this.InternalDecrypt(), ObscuredULong.cryptoKey);
      this.currentCryptoKey = ObscuredULong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ulong num = this.InternalDecrypt();
      this.currentCryptoKey = (ulong) Random.get_seed();
      this.hiddenValue = ObscuredULong.Encrypt(num, this.currentCryptoKey);
    }

    public ulong GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ulong encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private ulong InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredULong.cryptoKey;
        this.hiddenValue = ObscuredULong.Encrypt(0UL);
        this.fakeValue = 0UL;
        this.inited = true;
      }
      ulong num = ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && (long) this.fakeValue != 0L && (long) num != (long) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredULong))
        return false;
      return this.Equals((ObscuredULong) obj);
    }

    public bool Equals(ObscuredULong obj)
    {
      if ((long) this.currentCryptoKey == (long) obj.currentCryptoKey)
        return (long) this.hiddenValue == (long) obj.hiddenValue;
      return (long) ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey) == (long) ObscuredULong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
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

    public static implicit operator ObscuredULong(ulong value)
    {
      ObscuredULong obscuredUlong = new ObscuredULong(ObscuredULong.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredUlong.fakeValue = value;
      return obscuredUlong;
    }

    public static implicit operator ulong(ObscuredULong value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredULong operator ++(ObscuredULong input)
    {
      ulong num = input.InternalDecrypt() + 1UL;
      input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredULong operator --(ObscuredULong input)
    {
      ulong num = input.InternalDecrypt() - 1UL;
      input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
