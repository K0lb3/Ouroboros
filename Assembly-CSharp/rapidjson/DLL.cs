// Decompiled with JetBrains decompiler
// Type: rapidjson.DLL
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace rapidjson
{
  internal static class DLL
  {
    private const string LIBNAME = "rapidjson";

    public static bool TryGet(ref IntPtr src, string name, out IntPtr dst)
    {
      bool isObject;
      if (DLL._rapidjson_get_value_by_object(src, name, out isObject, out dst))
        return true;
      dst = IntPtr.Zero;
      if (!isObject)
        throw new InvalidOperationException("Not Object Type.");
      return false;
    }

    public static IntPtr Get(ref IntPtr src, string name)
    {
      IntPtr dst;
      if (!DLL.TryGet(ref src, name, out dst))
        throw new KeyNotFoundException(name);
      return dst;
    }

    public static IntPtr Get(ref IntPtr src, uint index)
    {
      bool isArray;
      IntPtr dst;
      if (DLL._rapidjson_get_value_by_array(src, index, out isArray, out dst))
        return dst;
      if (!isArray)
        throw new InvalidOperationException("Not Array Type.");
      throw new IndexOutOfRangeException();
    }

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_new_document_from_memory_bytes(byte[] bytes, uint length, out IntPtr document);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_new_document_from_memory_string(string json, out IntPtr document);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_new_document_from_file([MarshalAs(UnmanagedType.LPStr)] string filepath, out IntPtr document);

    [DllImport("rapidjson")]
    public static extern void _rapidjson_delete_document(out IntPtr document);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_value_by_object(IntPtr src, string name, [MarshalAs(UnmanagedType.I1)] out bool isObject, out IntPtr dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_object_member_count(IntPtr src, out uint size);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_key_value_pair_by_object_index(IntPtr src, uint index, [MarshalAs(UnmanagedType.LPStr)] out string key, out IntPtr value);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_value_by_array(IntPtr src, uint index, [MarshalAs(UnmanagedType.I1)] out bool isArray, out IntPtr dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_array_iterator(IntPtr src, out IntPtr elementsPointer, out uint size, out uint elementSize);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_int(IntPtr src, out int dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_uint(IntPtr src, out uint dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_int64(IntPtr src, out long dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_uint64(IntPtr src, out ulong dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_float(IntPtr src, out float dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_double(IntPtr src, out double dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_string(IntPtr src, [MarshalAs(UnmanagedType.LPStr)] out string dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_bool(IntPtr src, [MarshalAs(UnmanagedType.I1)] out bool dst);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_array(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_object(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_int(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_uint(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_int64(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_uint64(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_float(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_double(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_string(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_bool(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_is_null(IntPtr src);

    [DllImport("rapidjson")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool _rapidjson_get_value_by_pointer(IntPtr src, [MarshalAs(UnmanagedType.LPStr)] string pointer, uint pointerLength, [MarshalAs(UnmanagedType.I1)] out bool isValid, out IntPtr dst);
  }
}
