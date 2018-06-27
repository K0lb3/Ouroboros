// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Value
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.DOM.Json
{
  public struct Value : IValue
  {
    private readonly rapidjson.Value value;

    public Value(rapidjson.Value value)
    {
      this.value = value;
    }

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

    public bool IsNull()
    {
      return this.value.IsNull();
    }

    public bool IsObject()
    {
      return this.value.IsObject();
    }

    public bool IsArray()
    {
      return this.value.IsArray();
    }

    public bool IsBool()
    {
      return this.value.IsBool();
    }

    public bool IsString()
    {
      return this.value.IsString();
    }

    public bool IsInt()
    {
      return this.value.IsInt();
    }

    public bool IsUInt()
    {
      return this.value.IsUInt();
    }

    public bool IsLong()
    {
      return this.value.IsLong();
    }

    public bool IsULong()
    {
      return this.value.IsULong();
    }

    public bool IsFloat()
    {
      return this.value.IsFloat();
    }

    public bool IsDouble()
    {
      return this.value.IsDouble();
    }

    public Object GetObject()
    {
      return new Object(this.value.GetObject());
    }

    public Array GetArray()
    {
      return new Array(this.value.GetArray());
    }

    public bool ToBool()
    {
      return this.value.ToBool();
    }

    public override string ToString()
    {
      return this.value.ToString();
    }

    public int ToInt()
    {
      return this.value.ToInt();
    }

    public uint ToUInt()
    {
      return this.value.ToUInt();
    }

    public long ToLong()
    {
      return this.value.ToLong();
    }

    public ulong ToULong()
    {
      return this.value.ToULong();
    }

    public float ToFloat()
    {
      return this.value.ToFloat();
    }

    public double ToDouble()
    {
      return this.value.ToDouble();
    }

    public bool GetValueByPointer(string pointer, bool defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public string GetValueByPointer(string pointer, string defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public int GetValueByPointer(string pointer, int defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public uint GetValueByPointer(string pointer, uint defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public long GetValueByPointer(string pointer, long defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public ulong GetValueByPointer(string pointer, ulong defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public float GetValueByPointer(string pointer, float defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public double GetValueByPointer(string pointer, double defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public Value this[string name]
    {
      get
      {
        return new Value(this.value[name]);
      }
    }

    public Value this[int index]
    {
      get
      {
        return new Value(this.value[index]);
      }
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
  }
}
