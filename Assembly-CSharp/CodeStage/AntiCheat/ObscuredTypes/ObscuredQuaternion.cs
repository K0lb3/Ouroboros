// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredQuaternion
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredQuaternion
  {
    private static int cryptoKey = 120205;
    private static readonly Quaternion initialFakeValue = Quaternion.get_identity();
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredQuaternion.RawEncryptedQuaternion hiddenValue;
    [SerializeField]
    private Quaternion fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredQuaternion(ObscuredQuaternion.RawEncryptedQuaternion value)
    {
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = ObscuredQuaternion.initialFakeValue;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey)
    {
      ObscuredQuaternion.cryptoKey = newKey;
    }

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value)
    {
      return ObscuredQuaternion.Encrypt(value, 0);
    }

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value, int key)
    {
      if (key == 0)
        key = ObscuredQuaternion.cryptoKey;
      ObscuredQuaternion.RawEncryptedQuaternion encryptedQuaternion;
      encryptedQuaternion.x = ObscuredFloat.Encrypt((float) value.x, key);
      encryptedQuaternion.y = ObscuredFloat.Encrypt((float) value.y, key);
      encryptedQuaternion.z = ObscuredFloat.Encrypt((float) value.z, key);
      encryptedQuaternion.w = ObscuredFloat.Encrypt((float) value.w, key);
      return encryptedQuaternion;
    }

    public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value)
    {
      return ObscuredQuaternion.Decrypt(value, 0);
    }

    public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value, int key)
    {
      if (key == 0)
        key = ObscuredQuaternion.cryptoKey;
      Quaternion quaternion;
      quaternion.x = (__Null) (double) ObscuredFloat.Decrypt(value.x, key);
      quaternion.y = (__Null) (double) ObscuredFloat.Decrypt(value.y, key);
      quaternion.z = (__Null) (double) ObscuredFloat.Decrypt(value.z, key);
      quaternion.w = (__Null) (double) ObscuredFloat.Decrypt(value.w, key);
      return quaternion;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredQuaternion.cryptoKey)
        return;
      this.hiddenValue = ObscuredQuaternion.Encrypt(this.InternalDecrypt(), ObscuredQuaternion.cryptoKey);
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Quaternion quaternion = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed();
      this.hiddenValue = ObscuredQuaternion.Encrypt(quaternion, this.currentCryptoKey);
    }

    public ObscuredQuaternion.RawEncryptedQuaternion GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredQuaternion.RawEncryptedQuaternion encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private Quaternion InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
        this.hiddenValue = ObscuredQuaternion.Encrypt(ObscuredQuaternion.initialFakeValue);
        this.fakeValue = ObscuredQuaternion.initialFakeValue;
        this.inited = true;
      }
      Quaternion q1;
      q1.x = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      q1.y = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      q1.z = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      q1.w = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.w, this.currentCryptoKey);
      // ISSUE: explicit reference operation
      if (ObscuredCheatingDetector.IsRunning && !((Quaternion) @this.fakeValue).Equals((object) ObscuredQuaternion.initialFakeValue) && !this.CompareQuaternionsWithTolerance(q1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return q1;
    }

    private bool CompareQuaternionsWithTolerance(Quaternion q1, Quaternion q2)
    {
      float quaternionEpsilon = ObscuredCheatingDetector.Instance.quaternionEpsilon;
      if ((double) Math.Abs((float) (q1.x - q2.x)) < (double) quaternionEpsilon && (double) Math.Abs((float) (q1.y - q2.y)) < (double) quaternionEpsilon && (double) Math.Abs((float) (q1.z - q2.z)) < (double) quaternionEpsilon)
        return (double) Math.Abs((float) (q1.w - q2.w)) < (double) quaternionEpsilon;
      return false;
    }

    public override int GetHashCode()
    {
      Quaternion quaternion = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Quaternion) @quaternion).GetHashCode();
    }

    public override string ToString()
    {
      Quaternion quaternion = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Quaternion) @quaternion).ToString();
    }

    public string ToString(string format)
    {
      Quaternion quaternion = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Quaternion) @quaternion).ToString(format);
    }

    public static implicit operator ObscuredQuaternion(Quaternion value)
    {
      ObscuredQuaternion obscuredQuaternion = new ObscuredQuaternion(ObscuredQuaternion.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredQuaternion.fakeValue = value;
      return obscuredQuaternion;
    }

    public static implicit operator Quaternion(ObscuredQuaternion value)
    {
      return value.InternalDecrypt();
    }

    [Serializable]
    public struct RawEncryptedQuaternion
    {
      public int x;
      public int y;
      public int z;
      public int w;
    }
  }
}
