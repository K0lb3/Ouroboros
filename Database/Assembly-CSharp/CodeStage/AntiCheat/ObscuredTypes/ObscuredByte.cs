// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredByte : IFormattable, IEquatable<ObscuredByte>
  {
    private static byte cryptoKey = 244;
    private byte currentCryptoKey;
    private byte hiddenValue;
    private byte fakeValue;
    private bool inited;

    private ObscuredByte(byte value)
    {
      this.currentCryptoKey = ObscuredByte.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = (byte) 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte newKey)
    {
      ObscuredByte.cryptoKey = newKey;
    }

    public static byte EncryptDecrypt(byte value)
    {
      return ObscuredByte.EncryptDecrypt(value, (byte) 0);
    }

    public static byte EncryptDecrypt(byte value, byte key)
    {
      if ((int) key == 0)
        return (byte) ((uint) value ^ (uint) ObscuredByte.cryptoKey);
      return (byte) ((uint) value ^ (uint) key);
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredByte.cryptoKey)
        return;
      this.hiddenValue = ObscuredByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredByte.cryptoKey);
      this.currentCryptoKey = ObscuredByte.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      byte num = this.InternalDecrypt();
      this.currentCryptoKey = (byte) (Random.get_seed() >> 24);
      this.hiddenValue = ObscuredByte.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public byte GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(byte encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private byte InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredByte.cryptoKey;
        this.hiddenValue = ObscuredByte.EncryptDecrypt((byte) 0);
        this.fakeValue = (byte) 0;
        this.inited = true;
      }
      byte num = ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && (int) this.fakeValue != 0 && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredByte))
        return false;
      return this.Equals((ObscuredByte) obj);
    }

    public bool Equals(ObscuredByte obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return (int) this.hiddenValue == (int) obj.hiddenValue;
      return (int) ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredByte.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
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

    public static implicit operator ObscuredByte(byte value)
    {
      ObscuredByte obscuredByte = new ObscuredByte(ObscuredByte.EncryptDecrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredByte.fakeValue = value;
      return obscuredByte;
    }

    public static implicit operator byte(ObscuredByte value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredByte operator ++(ObscuredByte input)
    {
      byte num = (byte) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredByte.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredByte operator --(ObscuredByte input)
    {
      byte num = (byte) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredByte.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
