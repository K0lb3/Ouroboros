// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Value
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Gsc.DOM.Generic
{
  public struct Value : IValue
  {
    private Value.InternalValue value;
    private ValueType valueType;

    IObject IValue.GetObject()
    {
      return (IObject) this.GetObject();
    }

    IArray IValue.GetArray()
    {
      return (IArray) this.GetArray();
    }

    IValue IValue.this[string name]
    {
      get
      {
        return (IValue) this[name];
      }
    }

    IValue IValue.this[int index]
    {
      get
      {
        return (IValue) this[index];
      }
    }

    public void SetNull()
    {
      this.valueType = ValueType.Null;
      this.value = new Value.InternalValue();
    }

    public Object SetObject()
    {
      Object o = new Object();
      this.Set(o);
      return o;
    }

    public Array SetArray()
    {
      Array a = new Array();
      this.Set(a);
      return a;
    }

    public void Set(bool b)
    {
      this.valueType = ValueType.Bool;
      this.value.b = b;
    }

    public void Set(string s)
    {
      this.valueType = ValueType.String;
      this.value.s = s;
    }

    public void Set(int i)
    {
      this.Set((long) i, ValueType.Int32);
    }

    public void Set(uint i)
    {
      this.Set((ulong) i, ValueType.UInt32);
    }

    public void Set(long i)
    {
      this.Set(i, ValueType.Int64);
    }

    public void Set(ulong i)
    {
      this.Set(i, ValueType.UInt64);
    }

    public void Set(float f)
    {
      this.Set((double) f, ValueType.Float);
    }

    public void Set(double d)
    {
      this.Set(d, ValueType.Double);
    }

    private void Set(long i, ValueType t)
    {
      this.valueType = t;
      this.value.l = i;
    }

    private void Set(ulong i, ValueType t)
    {
      this.valueType = t;
      this.value.ul = i;
    }

    private void Set(double d, ValueType t)
    {
      this.valueType = t;
      this.value.d = d;
    }

    private void Set(Object o)
    {
      this.valueType = ValueType.Object;
      this.value.o = o;
    }

    private void Set(Array a)
    {
      this.valueType = ValueType.Array;
      this.value.a = a;
    }

    public bool IsNull()
    {
      return this.valueType == ValueType.Null;
    }

    public bool IsObject()
    {
      return this.valueType == ValueType.Object;
    }

    public bool IsArray()
    {
      return this.valueType == ValueType.Array;
    }

    public bool IsBool()
    {
      return this.valueType == ValueType.Bool;
    }

    public bool IsString()
    {
      return this.valueType == ValueType.String;
    }

    public bool IsInt()
    {
      return (this.valueType & ValueType.Int32) != ValueType.Null;
    }

    public bool IsUInt()
    {
      return (this.valueType & ValueType.UInt32) != ValueType.Null;
    }

    public bool IsLong()
    {
      return (this.valueType & (ValueType.Int32 | ValueType.UInt32 | ValueType.Int64)) != ValueType.Null;
    }

    public bool IsULong()
    {
      return (this.valueType & (ValueType.UInt32 | ValueType.UInt64)) != ValueType.Null;
    }

    public bool IsFloat()
    {
      return (this.valueType & ValueType.Float) != ValueType.Null;
    }

    public bool IsDouble()
    {
      return (this.valueType & ValueType.Number) != ValueType.Null;
    }

    public Object GetObject()
    {
      return this.value.o;
    }

    public Array GetArray()
    {
      return this.value.a;
    }

    public bool ToBool()
    {
      return this.value.b;
    }

    public override string ToString()
    {
      return this.value.s;
    }

    public int ToInt()
    {
      return (int) this.value.l;
    }

    public uint ToUInt()
    {
      return (uint) this.value.ul;
    }

    public long ToLong()
    {
      return this.value.l;
    }

    public ulong ToULong()
    {
      return this.value.ul;
    }

    public float ToFloat()
    {
      return (float) this.value.d;
    }

    public double ToDouble()
    {
      return this.value.d;
    }

    public bool GetValueByPointer(string pointer, bool defaultValue)
    {
      return defaultValue;
    }

    public string GetValueByPointer(string pointer, string defaultValue)
    {
      return defaultValue;
    }

    public int GetValueByPointer(string pointer, int defaultValue)
    {
      return defaultValue;
    }

    public uint GetValueByPointer(string pointer, uint defaultValue)
    {
      return defaultValue;
    }

    public long GetValueByPointer(string pointer, long defaultValue)
    {
      return defaultValue;
    }

    public ulong GetValueByPointer(string pointer, ulong defaultValue)
    {
      return defaultValue;
    }

    public float GetValueByPointer(string pointer, float defaultValue)
    {
      return defaultValue;
    }

    public double GetValueByPointer(string pointer, double defaultValue)
    {
      return defaultValue;
    }

    public Value this[string name]
    {
      get
      {
        return this.value.o[name];
      }
    }

    public Value this[int index]
    {
      get
      {
        return this.value.a[index];
      }
    }

    public static implicit operator Value(Object o)
    {
      Value obj = new Value();
      obj.Set(o);
      return obj;
    }

    public static implicit operator Value(Array a)
    {
      Value obj = new Value();
      obj.Set(a);
      return obj;
    }

    public static implicit operator Value(bool b)
    {
      Value obj = new Value();
      obj.Set(b);
      return obj;
    }

    public static implicit operator Value(string s)
    {
      Value obj = new Value();
      obj.Set(s);
      return obj;
    }

    public static implicit operator Value(int i)
    {
      Value obj = new Value();
      obj.Set(i);
      return obj;
    }

    public static implicit operator Value(uint i)
    {
      Value obj = new Value();
      obj.Set(i);
      return obj;
    }

    public static implicit operator Value(long i)
    {
      Value obj = new Value();
      obj.Set(i);
      return obj;
    }

    public static implicit operator Value(ulong i)
    {
      Value obj = new Value();
      obj.Set(i);
      return obj;
    }

    public static implicit operator Value(float f)
    {
      Value obj = new Value();
      obj.Set(f);
      return obj;
    }

    public static implicit operator Value(double d)
    {
      Value obj = new Value();
      obj.Set(d);
      return obj;
    }

    public static explicit operator bool(Value self)
    {
      return self.ToBool();
    }

    public static explicit operator string(Value self)
    {
      return self.ToString();
    }

    public static explicit operator int(Value self)
    {
      return self.ToInt();
    }

    public static explicit operator uint(Value self)
    {
      return self.ToUInt();
    }

    public static explicit operator long(Value self)
    {
      return self.ToLong();
    }

    public static explicit operator ulong(Value self)
    {
      return self.ToULong();
    }

    public static explicit operator float(Value self)
    {
      return self.ToFloat();
    }

    public static explicit operator double(Value self)
    {
      return self.ToDouble();
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InternalValue
    {
      [FieldOffset(0)]
      public Object o;
      [FieldOffset(0)]
      public Array a;
      [FieldOffset(0)]
      public double d;
      [FieldOffset(0)]
      public long l;
      [FieldOffset(0)]
      public ulong ul;
      [FieldOffset(0)]
      public string s;
      [FieldOffset(0)]
      public bool b;
    }
  }
}
