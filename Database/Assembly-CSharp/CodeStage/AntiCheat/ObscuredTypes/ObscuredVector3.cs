// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredVector3
  {
    private static int cryptoKey = 120207;
    private static readonly Vector3 initialFakeValue = Vector3.get_zero();
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector3.RawEncryptedVector3 hiddenValue;
    [SerializeField]
    private Vector3 fakeValue;
    [SerializeField]
    private bool inited;

    private ObscuredVector3(ObscuredVector3.RawEncryptedVector3 encrypted)
    {
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
      this.hiddenValue = encrypted;
      this.fakeValue = ObscuredVector3.initialFakeValue;
      this.inited = true;
    }

    public float x
    {
      get
      {
        float num = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.IsRunning && !((Vector3) @this.fakeValue).Equals((object) ObscuredVector3.initialFakeValue) && (double) Math.Abs(num - (float) this.fakeValue.x) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
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
        if (ObscuredCheatingDetector.IsRunning && !((Vector3) @this.fakeValue).Equals((object) ObscuredVector3.initialFakeValue) && (double) Math.Abs(num - (float) this.fakeValue.y) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
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

    public float z
    {
      get
      {
        float num = this.InternalDecryptField(this.hiddenValue.z);
        if (ObscuredCheatingDetector.IsRunning && !((Vector3) @this.fakeValue).Equals((object) ObscuredVector3.initialFakeValue) && (double) Math.Abs(num - (float) this.fakeValue.z) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return num;
      }
      set
      {
        this.hiddenValue.z = this.InternalEncryptField(value);
        if (!ObscuredCheatingDetector.IsRunning)
          return;
        this.fakeValue.z = (__Null) (double) value;
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
          case 2:
            return this.z;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
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
          case 2:
            this.z = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
        }
      }
    }

    public static void SetNewCryptoKey(int newKey)
    {
      ObscuredVector3.cryptoKey = newKey;
    }

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value)
    {
      return ObscuredVector3.Encrypt(value, 0);
    }

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value, int key)
    {
      if (key == 0)
        key = ObscuredVector3.cryptoKey;
      ObscuredVector3.RawEncryptedVector3 encryptedVector3;
      encryptedVector3.x = ObscuredFloat.Encrypt((float) value.x, key);
      encryptedVector3.y = ObscuredFloat.Encrypt((float) value.y, key);
      encryptedVector3.z = ObscuredFloat.Encrypt((float) value.z, key);
      return encryptedVector3;
    }

    public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value)
    {
      return ObscuredVector3.Decrypt(value, 0);
    }

    public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value, int key)
    {
      if (key == 0)
        key = ObscuredVector3.cryptoKey;
      Vector3 vector3;
      vector3.x = (__Null) (double) ObscuredFloat.Decrypt(value.x, key);
      vector3.y = (__Null) (double) ObscuredFloat.Decrypt(value.y, key);
      vector3.z = (__Null) (double) ObscuredFloat.Decrypt(value.z, key);
      return vector3;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector3.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector3.Encrypt(this.InternalDecrypt(), ObscuredVector3.cryptoKey);
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector3 vector3 = this.InternalDecrypt();
      this.currentCryptoKey = Random.get_seed();
      this.hiddenValue = ObscuredVector3.Encrypt(vector3, this.currentCryptoKey);
    }

    public ObscuredVector3.RawEncryptedVector3 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector3.RawEncryptedVector3 encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private Vector3 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector3.cryptoKey;
        this.hiddenValue = ObscuredVector3.Encrypt(ObscuredVector3.initialFakeValue, ObscuredVector3.cryptoKey);
        this.fakeValue = ObscuredVector3.initialFakeValue;
        this.inited = true;
      }
      Vector3 vector1;
      vector1.x = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector1.y = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      vector1.z = (__Null) (double) ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      // ISSUE: explicit reference operation
      if (ObscuredCheatingDetector.IsRunning && !((Vector3) @this.fakeValue).Equals((object) Vector3.get_zero()) && !this.CompareVectorsWithTolerance(vector1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector1;
    }

    private bool CompareVectorsWithTolerance(Vector3 vector1, Vector3 vector2)
    {
      float vector3Epsilon = ObscuredCheatingDetector.Instance.vector3Epsilon;
      if ((double) Math.Abs((float) (vector1.x - vector2.x)) < (double) vector3Epsilon && (double) Math.Abs((float) (vector1.y - vector2.y)) < (double) vector3Epsilon)
        return (double) Math.Abs((float) (vector1.z - vector2.z)) < (double) vector3Epsilon;
      return false;
    }

    private float InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector3.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(float encrypted)
    {
      return ObscuredFloat.Encrypt(encrypted, ObscuredVector3.cryptoKey);
    }

    public override bool Equals(object other)
    {
      Vector3 vector3 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector3) @vector3).Equals(other);
    }

    public override int GetHashCode()
    {
      Vector3 vector3 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector3) @vector3).GetHashCode();
    }

    public override string ToString()
    {
      Vector3 vector3 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector3) @vector3).ToString();
    }

    public string ToString(string format)
    {
      Vector3 vector3 = this.InternalDecrypt();
      // ISSUE: explicit reference operation
      return ((Vector3) @vector3).ToString(format);
    }

    public static implicit operator ObscuredVector3(Vector3 value)
    {
      ObscuredVector3 obscuredVector3 = new ObscuredVector3(ObscuredVector3.Encrypt(value, ObscuredVector3.cryptoKey));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredVector3.fakeValue = value;
      return obscuredVector3;
    }

    public static implicit operator Vector3(ObscuredVector3 value)
    {
      return value.InternalDecrypt();
    }

    public static ObscuredVector3 operator +(ObscuredVector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3 operator +(Vector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a, b.InternalDecrypt());
    }

    public static ObscuredVector3 operator +(ObscuredVector3 a, Vector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3 operator -(Vector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a, b.InternalDecrypt());
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a, Vector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a)
    {
      return (ObscuredVector3) Vector3.op_UnaryNegation(a.InternalDecrypt());
    }

    public static ObscuredVector3 operator *(ObscuredVector3 a, float d)
    {
      return (ObscuredVector3) Vector3.op_Multiply(a.InternalDecrypt(), d);
    }

    public static ObscuredVector3 operator *(float d, ObscuredVector3 a)
    {
      return (ObscuredVector3) Vector3.op_Multiply(d, a.InternalDecrypt());
    }

    public static ObscuredVector3 operator /(ObscuredVector3 a, float d)
    {
      return (ObscuredVector3) Vector3.op_Division(a.InternalDecrypt(), d);
    }

    public static bool operator ==(ObscuredVector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Equality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator ==(Vector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Equality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator ==(ObscuredVector3 lhs, Vector3 rhs)
    {
      return Vector3.op_Equality(lhs.InternalDecrypt(), rhs);
    }

    public static bool operator !=(ObscuredVector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Inequality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator !=(Vector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Inequality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator !=(ObscuredVector3 lhs, Vector3 rhs)
    {
      return Vector3.op_Inequality(lhs.InternalDecrypt(), rhs);
    }

    [Serializable]
    public struct RawEncryptedVector3
    {
      public int x;
      public int y;
      public int z;
    }
  }
}
