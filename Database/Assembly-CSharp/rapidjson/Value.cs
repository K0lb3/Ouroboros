// Decompiled with JetBrains decompiler
// Type: rapidjson.Value
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace rapidjson
{
  public struct Value
  {
    private readonly Document doc;
    private IntPtr ptr;

    public Value(Document doc, ref IntPtr ptr)
    {
      this.doc = doc;
      this.ptr = ptr;
    }

    public Object GetObject()
    {
      return new Object(this.doc, ref this.ptr);
    }

    public bool IsAllocated()
    {
      if (this.doc != null)
        return this.ptr != IntPtr.Zero;
      return false;
    }

    public bool IsObject()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_object(this.ptr);
    }

    public Array GetArray()
    {
      return new Array(this.doc, ref this.ptr);
    }

    public bool IsArray()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_array(this.ptr);
    }

    public Value this[string name]
    {
      get
      {
        this.doc.CheckDisposed();
        IntPtr ptr = DLL.Get(ref this.ptr, name);
        return new Value(this.doc, ref ptr);
      }
    }

    public Value this[int index]
    {
      get
      {
        if (index < 0)
          throw new IndexOutOfRangeException();
        this.doc.CheckDisposed();
        IntPtr ptr = DLL.Get(ref this.ptr, (uint) index);
        return new Value(this.doc, ref ptr);
      }
    }

    public int ToInt()
    {
      this.doc.CheckDisposed();
      int dst;
      if (!DLL._rapidjson_get_int(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsInt()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_int(this.ptr);
    }

    public uint ToUInt()
    {
      this.doc.CheckDisposed();
      uint dst;
      if (!DLL._rapidjson_get_uint(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsUInt()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_uint(this.ptr);
    }

    public long ToLong()
    {
      this.doc.CheckDisposed();
      long dst;
      if (!DLL._rapidjson_get_int64(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsLong()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_int64(this.ptr);
    }

    public ulong ToULong()
    {
      this.doc.CheckDisposed();
      ulong dst;
      if (!DLL._rapidjson_get_uint64(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsULong()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_uint64(this.ptr);
    }

    public float ToFloat()
    {
      this.doc.CheckDisposed();
      float dst;
      if (!DLL._rapidjson_get_float(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsFloat()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_float(this.ptr);
    }

    public double ToDouble()
    {
      this.doc.CheckDisposed();
      double dst;
      if (!DLL._rapidjson_get_double(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsDouble()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_double(this.ptr);
    }

    public bool ToBool()
    {
      this.doc.CheckDisposed();
      bool dst;
      if (!DLL._rapidjson_get_bool(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsBool()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_bool(this.ptr);
    }

    public override string ToString()
    {
      this.doc.CheckDisposed();
      string dst;
      if (!DLL._rapidjson_get_string(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsString()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_string(this.ptr);
    }

    public bool IsNull()
    {
      if (this.ptr == IntPtr.Zero)
        return true;
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_null(this.ptr);
    }

    public bool TryGetValueByPointer(string pointer, out Value value)
    {
      this.doc.CheckDisposed();
      IntPtr dst = IntPtr.Zero;
      bool isValid = false;
      bool valueByPointer = DLL._rapidjson_get_value_by_pointer(this.ptr, pointer, (uint) pointer.Length, out isValid, out dst);
      if (!isValid)
        throw new InvalidPointerError(pointer);
      value = new Value(!valueByPointer ? (Document) null : this.doc, ref dst);
      return valueByPointer;
    }

    public int GetValueByPointer(string pointer, int defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsInt())
        return (int) obj;
      return defaultValue;
    }

    public uint GetValueByPointer(string pointer, uint defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsUInt())
        return (uint) obj;
      return defaultValue;
    }

    public long GetValueByPointer(string pointer, long defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsLong())
        return (long) obj;
      return defaultValue;
    }

    public ulong GetValueByPointer(string pointer, ulong defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsULong())
        return (ulong) obj;
      return defaultValue;
    }

    public float GetValueByPointer(string pointer, float defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsFloat())
        return (float) obj;
      return defaultValue;
    }

    public double GetValueByPointer(string pointer, double defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsDouble())
        return (double) obj;
      return defaultValue;
    }

    public bool GetValueByPointer(string pointer, bool defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsBool())
        return (bool) obj;
      return defaultValue;
    }

    public string GetValueByPointer(string pointer, string defaultValue)
    {
      Value obj;
      if (this.TryGetValueByPointer(pointer, out obj) && obj.IsString())
        return (string) obj;
      return defaultValue;
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

    public static explicit operator bool(Value self)
    {
      return self.ToBool();
    }

    public static explicit operator string(Value self)
    {
      return self.ToString();
    }
  }
}
