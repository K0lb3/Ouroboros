// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredString
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public sealed class ObscuredString
  {
    private static string cryptoKey = "4441";
    [SerializeField]
    private string currentCryptoKey;
    [SerializeField]
    private byte[] hiddenValue;
    [SerializeField]
    private string fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredString()
    {
    }

    private ObscuredString(byte[] value)
    {
      this.currentCryptoKey = ObscuredString.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = (string) null;
      this.inited = true;
    }

    public static void SetNewCryptoKey(string newKey)
    {
      ObscuredString.cryptoKey = newKey;
    }

    public static string EncryptDecrypt(string value)
    {
      return ObscuredString.EncryptDecrypt(value, string.Empty);
    }

    public static string EncryptDecrypt(string value, string key)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (string.IsNullOrEmpty(key))
        key = ObscuredString.cryptoKey;
      int length1 = key.Length;
      int length2 = value.Length;
      char[] chArray = new char[length2];
      for (int index = 0; index < length2; ++index)
        chArray[index] = (char) ((uint) value[index] ^ (uint) key[index % length1]);
      return new string(chArray);
    }

    public void ApplyNewCryptoKey()
    {
      if (!(this.currentCryptoKey != ObscuredString.cryptoKey))
        return;
      this.hiddenValue = ObscuredString.InternalEncrypt(this.InternalDecrypt());
      this.currentCryptoKey = ObscuredString.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      string str = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed().ToString();
      this.hiddenValue = ObscuredString.InternalEncrypt(str, this.currentCryptoKey);
    }

    public string GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return ObscuredString.GetString(this.hiddenValue);
    }

    public void SetEncrypted(string encrypted)
    {
      this.inited = true;
      this.hiddenValue = ObscuredString.GetBytes(encrypted);
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private static byte[] InternalEncrypt(string value)
    {
      return ObscuredString.InternalEncrypt(value, ObscuredString.cryptoKey);
    }

    private static byte[] InternalEncrypt(string value, string key)
    {
      return ObscuredString.GetBytes(ObscuredString.EncryptDecrypt(value, key));
    }

    private string InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredString.cryptoKey;
        this.hiddenValue = ObscuredString.InternalEncrypt(string.Empty);
        this.fakeValue = string.Empty;
        this.inited = true;
      }
      string key = this.currentCryptoKey;
      if (string.IsNullOrEmpty(key))
        key = ObscuredString.cryptoKey;
      string str = ObscuredString.EncryptDecrypt(ObscuredString.GetString(this.hiddenValue), key);
      if (ObscuredCheatingDetector.IsRunning && !string.IsNullOrEmpty(this.fakeValue) && str != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return str;
    }

    public override string ToString()
    {
      return this.InternalDecrypt();
    }

    public override bool Equals(object obj)
    {
      if ((object) (obj as ObscuredString) == null)
        return false;
      return this.Equals((ObscuredString) obj);
    }

    public bool Equals(ObscuredString value)
    {
      if (value == (ObscuredString) null)
        return false;
      if (this.currentCryptoKey == value.currentCryptoKey)
        return ObscuredString.ArraysEquals(this.hiddenValue, value.hiddenValue);
      return string.Equals(this.InternalDecrypt(), value.InternalDecrypt());
    }

    public bool Equals(ObscuredString value, StringComparison comparisonType)
    {
      if (value == (ObscuredString) null)
        return false;
      return string.Equals(this.InternalDecrypt(), value.InternalDecrypt(), comparisonType);
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    private static byte[] GetBytes(string str)
    {
      byte[] numArray = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    private static string GetString(byte[] bytes)
    {
      char[] chArray = new char[bytes.Length / 2];
      Buffer.BlockCopy((Array) bytes, 0, (Array) chArray, 0, bytes.Length);
      return new string(chArray);
    }

    private static bool ArraysEquals(byte[] a1, byte[] a2)
    {
      if (a1 == a2)
        return true;
      if (a1 == null || a2 == null || a1.Length != a2.Length)
        return false;
      for (int index = 0; index < a1.Length; ++index)
      {
        if ((int) a1[index] != (int) a2[index])
          return false;
      }
      return true;
    }

    public static implicit operator ObscuredString(string value)
    {
      if (value == null)
        return (ObscuredString) null;
      ObscuredString obscuredString = new ObscuredString(ObscuredString.InternalEncrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredString.fakeValue = value;
      return obscuredString;
    }

    public static implicit operator string(ObscuredString value)
    {
      if (value == (ObscuredString) null)
        return (string) null;
      return value.InternalDecrypt();
    }

    public static bool operator ==(ObscuredString a, ObscuredString b)
    {
      if (object.ReferenceEquals((object) a, (object) b))
        return true;
      if ((object) a == null || (object) b == null)
        return false;
      if (a.currentCryptoKey == b.currentCryptoKey)
        return ObscuredString.ArraysEquals(a.hiddenValue, b.hiddenValue);
      return string.Equals(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static bool operator !=(ObscuredString a, ObscuredString b)
    {
      return !(a == b);
    }
  }
}
