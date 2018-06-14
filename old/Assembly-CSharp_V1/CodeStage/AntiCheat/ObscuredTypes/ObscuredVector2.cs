// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredVector2
  {
    private static int cryptoKey = 120206;
    private static readonly Vector2 initialFakeValue = Vector2.get_zero();
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector2.RawEncryptedVector2 hiddenValue;
    [SerializeField]
    private Vector2 fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredVector2(ObscuredVector2.RawEncryptedVector2 value)
    {
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = ObscuredVector2.initialFakeValue;
      this.inited = true;
    }

    public float x
    {
      get
      {
        float num = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.IsRunning && !((Vector2) @this.fakeValue).Equals((object) ObscuredVector2.initialFakeValue) && (double) Math.Abs(num - (float) this.fakeValue.x) > (double) ObscuredCheatingDetector.Instance.vector2Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return num;
      }
      set
      {
        this.hiddenValue.x = this.InternalEncryptField(value);
        if (!ObscuredCheatingDetector.IsRunning)
          return;
        this.fakeValue.x = (__Null) (double) value;
      }
    }

    public float y
    {
      get
      {
        float num = this.InternalDecryptField(this.hiddenValue.y);
        if (ObscuredCheatingDetector.IsRunning && !((Vector2) @this.fakeValue).Equals((object) ObscuredVector2.initialFakeValue) && (double) Math.Abs(num - (float) this.fakeValue.y) > (double) ObscuredCheatingDetector.Instance.vector2Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return num;
      }
      set
      {
        this.hiddenValue.y = this.InternalEncryptField(value);
        if (!ObscuredCheatingDetector.IsRunning)
          return;
        this.fakeValue.y = (__Null) (double) value;
      }
    }

    public float this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return this.x;
          case 1:
            return this.y;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
        }
      }
      set
      {
        switch (index)
        {
          case 0:
            this.x = value;
            break;
          case 1:
            this.y = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
        }
      }
    }

    public static void SetNewCryptoKey(int newKey)
    {
      ObscuredVector2.cryptoKey = newKey;
    }

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value)
    {
      return ObscuredVector2.Encrypt(value, 0);
    }

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value, int key)
    {
      if (key == 0)
        key = ObscuredVector2.cryptoKey;
      ObscuredVector2.RawEncryptedVector2 encryptedVector2;
      encryptedVector2.x = ObscuredFloat.Encrypt((float) value.x, key);
      encryptedVector2.y = ObscuredFloat.Encrypt((float) value.y, key);
      return encryptedVector2;
    }

    public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value)
    {
      return ObscuredVector2.Decrypt(value, 0);
    }

    public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value, int key)
    {
      if (key == 0)
        key = ObscuredVector2.cryptoKey;
      Vector2 vector2;
      vector2.x = (__Null) (double) ObscuredFloat.Decrypt(value.x, key);
      vector2.y = (__Null) (double) ObscuredFloat.Decrypt(value.y, key);
      return vector2;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector2.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector2.Encrypt(this.InternalDecrypt(), ObscuredVector2.cryptoKey);
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector2 vector2 = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed();
      this.hiddenValue = ObscuredVector2.Encrypt(vector2, this.currentCryptoKey);
    }

    public ObscuredVector2.RawEncryptedVector2 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector2.RawEncryptedVector2 encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private Vector2 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector2.cryptoKey;
        this.hiddenValue = ObscuredVector2.Encrypt(ObscuredVector2.initialFakeValue);
        this.fakeValue = ObscuredVector2.initialFakeValue;
        this.inited = true;
      }
      Vector2 vector1;
      vector1.x = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector1.y = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      // ISSUE: explicit reference operation
      if (ObscuredCheatingDetector.IsRunning && !((Vector2) @this.fakeValue).Equals((object) ObscuredVector2.initialFakeValue) && !this.CompareVectorsWithTolerance(vector1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector1;
    }

    private bool CompareVectorsWithTolerance(Vector2 vector1, Vector2 vector2)
    {
      float vector2Epsilon = ObscuredCheatingDetector.Instance.vector2Epsilon;
      if ((double) Math.Abs((float) (vector1.x - vector2.x)) < (double) vector2Epsilon)
        return (double) Math.Abs((float) (vector1.y - vector2.y)) < (double) vector2Epsilon;
      return false;
    }

    private float InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector2.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(float encrypted)
    {
      return ObscuredFloat.Encrypt(encrypted, ObscuredVector2.cryptoKey);
    }

    public override int GetHashCode()
    {
      Vector2 vector2 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector2) @vector2).GetHashCode();
    }

    public override string ToString()
    {
      Vector2 vector2 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector2) @vector2).ToString();
    }

    public string ToString(string format)
    {
      Vector2 vector2 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector2) @vector2).ToString(format);
    }

    public static implicit operator ObscuredVector2(Vector2 value)
    {
      ObscuredVector2 obscuredVector2 = new ObscuredVector2(ObscuredVector2.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredVector2.fakeValue = value;
      return obscuredVector2;
    }

    public static implicit operator Vector2(ObscuredVector2 value)
    {
      return value.InternalDecrypt();
    }

    public static implicit operator Vector3(ObscuredVector2 value)
    {
      Vector2 vector2 = value.InternalDecrypt();
      return new Vector3((float) vector2.x, (float) vector2.y, 0.0f);
    }

    [Serializable]
    public struct RawEncryptedVector2
    {
      public int x;
      public int y;
    }
  }
}
