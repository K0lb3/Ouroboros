// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredInt
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredInt : IFormattable, IEquatable<ObscuredInt>
  {
    private static int cryptoKey = 444444;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private int hiddenValue;
    [SerializeField]
    private int fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredInt(int value)
    {
      this.currentCryptoKey = ObscuredInt.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey)
    {
      ObscuredInt.cryptoKey = newKey;
    }

    public static int Encrypt(int value)
    {
      return ObscuredInt.Encrypt(value, 0);
    }

    public static int Encrypt(int value, int key)
    {
      if (key == 0)
        return value ^ ObscuredInt.cryptoKey;
      return value ^ key;
    }

    public static int Decrypt(int value)
    {
      return ObscuredInt.Decrypt(value, 0);
    }

    public static int Decrypt(int value, int key)
    {
      if (key == 0)
        return value ^ ObscuredInt.cryptoKey;
      return value ^ key;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredInt.cryptoKey)
        return;
      this.hiddenValue = ObscuredInt.Encrypt(this.InternalDecrypt(), ObscuredInt.cryptoKey);
      this.currentCryptoKey = ObscuredInt.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      this.hiddenValue = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed();
      this.hiddenValue = ObscuredInt.Encrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(int encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private int InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredInt.cryptoKey;
        this.hiddenValue = ObscuredInt.Encrypt(0);
        this.fakeValue = 0;
        this.inited = true;
      }
      int num = ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0 && num != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredInt))
        return false;
      return this.Equals((ObscuredInt) obj);
    }

    public bool Equals(ObscuredInt obj)
    {
      if (this.currentCryptoKey == obj.currentCryptoKey)
        return this.hiddenValue == obj.hiddenValue;
      return ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
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

    public static implicit operator ObscuredInt(int value)
    {
      ObscuredInt obscuredInt = new ObscuredInt(ObscuredInt.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredInt.fakeValue = value;
      return obscuredInt;
    }

    public static implicit operator int(ObscuredInt value)
    {
      return value.InternalDecrypt();
    }

    public static explicit operator ObscuredUInt(ObscuredInt value)
    {
      return (ObscuredUInt) ((uint) value.InternalDecrypt());
    }

    public static ObscuredInt operator ++(ObscuredInt input)
    {
      int num = input.InternalDecrypt() + 1;
      input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredInt operator --(ObscuredInt input)
    {
      int num = input.InternalDecrypt() - 1;
      input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }
  }
}
