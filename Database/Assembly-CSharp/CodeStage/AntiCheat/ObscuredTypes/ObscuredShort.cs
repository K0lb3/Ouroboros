// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredShort : IFormattable, IEquatable<ObscuredShort>
  {
    private static short cryptoKey = 214;
    private short currentCryptoKey;
    private short hiddenValue;
    private short fakeValue;
    private bool inited;

    private ObscuredShort(short value)
    {
      this.currentCryptoKey = ObscuredShort.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = (short) 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(short newKey)
    {
      ObscuredShort.cryptoKey = newKey;
    }

    public static short EncryptDecrypt(short value)
    {
      return ObscuredShort.EncryptDecrypt(value, (short) 0);
    }

    public static short EncryptDecrypt(short value, short key)
    {
      if ((int) key == 0)
        return (short) ((int) value ^ (int) ObscuredShort.cryptoKey);
      return (short) ((int) value ^ (int) key);
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredShort.cryptoKey);
      this.currentCryptoKey = ObscuredShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      short num = this.InternalDecrypt();
      this.currentCryptoKey = (short) Random.get_seed();
      this.hiddenValue = ObscuredShort.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public short GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(short encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private short InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredShort.cryptoKey;
        this.hiddenValue = ObscuredShort.EncryptDecrypt((short) 0);
        this.fakeValue = (short) 0;
        this.inited = true;
      }
      short num = ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && (int) this.fakeValue != 0 && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredShort))
        return false;
      return this.Equals((ObscuredShort) obj);
    }

    public bool Equals(ObscuredShort obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return (int) this.hiddenValue == (int) obj.hiddenValue;
      return (int) ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
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

    public static implicit operator ObscuredShort(short value)
    {
      ObscuredShort obscuredShort = new ObscuredShort(ObscuredShort.EncryptDecrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredShort.fakeValue = value;
      return obscuredShort;
    }

    public static implicit operator short(ObscuredShort value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredShort operator ++(ObscuredShort input)
    {
      short num = (short) ((int) input.InternalDecrypt() + 1);
      input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredShort operator --(ObscuredShort input)
    {
      short num = (short) ((int) input.InternalDecrypt() - 1);
      input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
