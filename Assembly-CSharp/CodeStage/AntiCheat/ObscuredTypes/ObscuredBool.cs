// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredBool : IEquatable<ObscuredBool>
  {
    private static byte cryptoKey = 215;
    [SerializeField]
    private byte currentCryptoKey;
    [SerializeField]
    private int hiddenValue;
    [SerializeField]
    private bool fakeValue;
    [SerializeField]
    private bool fakeValueChanged;
    [SerializeField]
    private bool inited;

    private ObscuredBool(int value)
    {
      this.currentCryptoKey = ObscuredBool.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = false;
      this.fakeValueChanged = false;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte newKey)
    {
      ObscuredBool.cryptoKey = newKey;
    }

    public static int Encrypt(bool value)
    {
      return ObscuredBool.Encrypt(value, (byte) 0);
    }

    public static int Encrypt(bool value, byte key)
    {
      if ((int) key == 0)
        key = ObscuredBool.cryptoKey;
      return (!value ? 181 : 213) ^ (int) key;
    }

    public static bool Decrypt(int value)
    {
      return ObscuredBool.Decrypt(value, (byte) 0);
    }

    public static bool Decrypt(int value, byte key)
    {
      if ((int) key == 0)
        key = ObscuredBool.cryptoKey;
      value ^= (int) key;
      return value != 181;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredBool.cryptoKey)
        return;
      this.hiddenValue = ObscuredBool.Encrypt(this.InternalDecrypt(), ObscuredBool.cryptoKey);
      this.currentCryptoKey = ObscuredBool.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      bool flag = this.InternalDecrypt();
      this.currentCryptoKey = (byte) (Random.get_seed() >> 24);
      this.hiddenValue = ObscuredBool.Encrypt(flag, this.currentCryptoKey);
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
      this.fakeValueChanged = true;
    }

    private bool InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredBool.cryptoKey;
        this.hiddenValue = ObscuredBool.Encrypt(false);
        this.fakeValue = false;
        this.fakeValueChanged = true;
        this.inited = true;
      }
      bool flag = (this.hiddenValue ^ (int) this.currentCryptoKey) != 181;
      if (ObscuredCheatingDetector.IsRunning && this.fakeValueChanged && flag != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return flag;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredBool))
        return false;
      return this.Equals((ObscuredBool) obj);
    }

    public bool Equals(ObscuredBool obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return this.hiddenValue == obj.hiddenValue;
      return ObscuredBool.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredBool.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public override string ToString()
    {
      return this.InternalDecrypt().ToString();
    }

    public static implicit operator ObscuredBool(bool value)
    {
      ObscuredBool obscuredBool = new ObscuredBool(ObscuredBool.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
      {
        obscuredBool.fakeValue = value;
        obscuredBool.fakeValueChanged = true;
      }
      return obscuredBool;
    }

    public static implicit operator bool(ObscuredBool value)
    {
      return value.InternalDecrypt();
    }
  }
}
