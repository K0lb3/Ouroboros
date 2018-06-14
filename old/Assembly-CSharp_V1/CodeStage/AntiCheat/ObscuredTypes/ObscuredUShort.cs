// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUShort
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredUShort : IFormattable, IEquatable<ObscuredUShort>
  {
    private static ushort cryptoKey = 224;
    private ushort currentCryptoKey;
    private ushort hiddenValue;
    private ushort fakeValue;
    private bool inited;

    private ObscuredUShort(ushort value)
    {
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = (ushort) 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ushort newKey)
    {
      ObscuredUShort.cryptoKey = newKey;
    }

    public static ushort EncryptDecrypt(ushort value)
    {
      return ObscuredUShort.EncryptDecrypt(value, (ushort) 0);
    }

    public static ushort EncryptDecrypt(ushort value, ushort key)
    {
      if ((int) key == 0)
        return (ushort) ((uint) value ^ (uint) ObscuredUShort.cryptoKey);
      return (ushort) ((uint) value ^ (uint) key);
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredUShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredUShort.cryptoKey);
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ushort num = this.InternalDecrypt();
      this.currentCryptoKey = (ushort) Random.get_seed();
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public ushort GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ushort encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private ushort InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUShort.cryptoKey;
        this.hiddenValue = ObscuredUShort.EncryptDecrypt((ushort) 0);
        this.fakeValue = (ushort) 0;
        this.inited = true;
      }
      ushort num = ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && (int) this.fakeValue != 0 && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredUShort))
        return false;
      return this.Equals((ObscuredUShort) obj);
    }

    public bool Equals(ObscuredUShort obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return (int) this.hiddenValue == (int) obj.hiddenValue;
      return (int) ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public override string ToString()
    {
      return this.InternalDecrypt().ToString();
    }

    public string ToString(string format)
    {
      return this.InternalDecrypt().ToString(format);
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public string ToString(IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(format, provider);
    }

    public static implicit operator ObscuredUShort(ushort value)
    {
      ObscuredUShort obscuredUshort = new ObscuredUShort(ObscuredUShort.EncryptDecrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredUshort.fakeValue = value;
      return obscuredUshort;
    }

    public static implicit operator ushort(ObscuredUShort value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredUShort operator ++(ObscuredUShort input)
    {
      ushort num = (ushort) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredUShort operator --(ObscuredUShort input)
    {
      ushort num = (ushort) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
