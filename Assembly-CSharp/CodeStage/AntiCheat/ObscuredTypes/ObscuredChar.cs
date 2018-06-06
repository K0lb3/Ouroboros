// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredChar
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredChar : IEquatable<ObscuredChar>
  {
    private static char cryptoKey = '—';
    private char currentCryptoKey;
    private char hiddenValue;
    private char fakeValue;
    private bool inited;

    private ObscuredChar(char value)
    {
      this.currentCryptoKey = ObscuredChar.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = char.MinValue;
      this.inited = true;
    }

    public static void SetNewCryptoKey(char newKey)
    {
      ObscuredChar.cryptoKey = newKey;
    }

    public static char EncryptDecrypt(char value)
    {
      return ObscuredChar.EncryptDecrypt(value, char.MinValue);
    }

    public static char EncryptDecrypt(char value, char key)
    {
      if ((int) key == 0)
        return (char) ((uint) value ^ (uint) ObscuredChar.cryptoKey);
      return (char) ((uint) value ^ (uint) key);
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredChar.cryptoKey)
        return;
      this.hiddenValue = ObscuredChar.EncryptDecrypt(this.InternalDecrypt(), ObscuredChar.cryptoKey);
      this.currentCryptoKey = ObscuredChar.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      char ch = this.InternalDecrypt();
      this.currentCryptoKey = (char) (Random.get_seed() >> 24);
      this.hiddenValue = ObscuredChar.EncryptDecrypt(ch, this.currentCryptoKey);
    }

    public char GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(char encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private char InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredChar.cryptoKey;
        this.hiddenValue = ObscuredChar.EncryptDecrypt(char.MinValue);
        this.fakeValue = char.MinValue;
        this.inited = true;
      }
      char ch = ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && (int) this.fakeValue != 0 && (int) ch != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return ch;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredChar))
        return false;
      return this.Equals((ObscuredChar) obj);
    }

    public bool Equals(ObscuredChar obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return (int) this.hiddenValue == (int) obj.hiddenValue;
      return (int) ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredChar.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public override string ToString()
    {
      return this.InternalDecrypt().ToString();
    }

    public string ToString(IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(provider);
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public static implicit operator ObscuredChar(char value)
    {
      ObscuredChar obscuredChar = new ObscuredChar(ObscuredChar.EncryptDecrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredChar.fakeValue = value;
      return obscuredChar;
    }

    public static implicit operator char(ObscuredChar value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredChar operator ++(ObscuredChar input)
    {
      char ch = (char) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredChar.EncryptDecrypt(ch, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = ch;
      return input;
    }

    public static ObscuredChar operator --(ObscuredChar input)
    {
      char ch = (char) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredChar.EncryptDecrypt(ch, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = ch;
      return input;
    }
  }
}
