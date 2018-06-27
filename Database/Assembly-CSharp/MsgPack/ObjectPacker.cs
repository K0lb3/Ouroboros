// Decompiled with JetBrains decompiler
// Type: MsgPack.ObjectPacker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MsgPack
{
  public class ObjectPacker
  {
    private static Dictionary<Type, ObjectPacker.PackDelegate> PackerMapping = new Dictionary<Type, ObjectPacker.PackDelegate>();
    private static Dictionary<Type, ObjectPacker.UnpackDelegate> UnpackerMapping = new Dictionary<Type, ObjectPacker.UnpackDelegate>();
    private byte[] _buf = new byte[64];

    static ObjectPacker()
    {
      ObjectPacker.PackerMapping.Add(typeof (string), new ObjectPacker.PackDelegate(ObjectPacker.StringPacker));
      ObjectPacker.UnpackerMapping.Add(typeof (string), new ObjectPacker.UnpackDelegate(ObjectPacker.StringUnpacker));
    }

    public byte[] Pack(object o)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        this.Pack((Stream) memoryStream, o);
        return memoryStream.ToArray();
      }
    }

    public void Pack(Stream strm, object o)
    {
      if (o != null && o.GetType().IsPrimitive)
        throw new NotSupportedException();
      this.Pack(new MsgPackWriter(strm), o);
    }

    private void Pack(MsgPackWriter writer, object o)
    {
      if (o == null)
      {
        writer.WriteNil();
      }
      else
      {
        Type type = o.GetType();
        if (type.IsPrimitive)
        {
          if (type.Equals(typeof (int)))
            writer.Write((int) o);
          else if (type.Equals(typeof (uint)))
            writer.Write((uint) o);
          else if (type.Equals(typeof (float)))
            writer.Write((float) o);
          else if (type.Equals(typeof (double)))
            writer.Write((double) o);
          else if (type.Equals(typeof (long)))
            writer.Write((long) o);
          else if (type.Equals(typeof (ulong)))
            writer.Write((ulong) o);
          else if (type.Equals(typeof (bool)))
            writer.Write((bool) o);
          else if (type.Equals(typeof (byte)))
            writer.Write((byte) o);
          else if (type.Equals(typeof (sbyte)))
            writer.Write((sbyte) o);
          else if (type.Equals(typeof (short)))
            writer.Write((short) o);
          else if (type.Equals(typeof (ushort)))
          {
            writer.Write((ushort) o);
          }
          else
          {
            if (!type.Equals(typeof (char)))
              throw new NotSupportedException();
            writer.Write((ushort) (char) o);
          }
        }
        else
        {
          ObjectPacker.PackDelegate packDelegate;
          if (ObjectPacker.PackerMapping.TryGetValue(type, out packDelegate))
            packDelegate(this, writer, o);
          else if (type.IsArray)
          {
            Array array = (Array) o;
            writer.WriteArrayHeader(array.Length);
            for (int index = 0; index < array.Length; ++index)
              this.Pack(writer, array.GetValue(index));
          }
          else
          {
            ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(type);
            writer.WriteMapHeader(reflectionCacheEntry.FieldMap.Count);
            foreach (KeyValuePair<string, FieldInfo> field in (IEnumerable<KeyValuePair<string, FieldInfo>>) reflectionCacheEntry.FieldMap)
            {
              writer.Write(field.Key, this._buf);
              object o1 = field.Value.GetValue(o);
              if (field.Value.FieldType.IsInterface && o1 != null)
              {
                writer.WriteArrayHeader(2);
                writer.Write(o1.GetType().FullName);
              }
              this.Pack(writer, o1);
            }
          }
        }
      }
    }

    public T Unpack<T>(byte[] buf)
    {
      return this.Unpack<T>(buf, 0, buf.Length);
    }

    public T Unpack<T>(byte[] buf, int offset, int size)
    {
      using (MemoryStream memoryStream = new MemoryStream(buf, offset, size))
        return this.Unpack<T>((Stream) memoryStream);
    }

    public T Unpack<T>(Stream strm)
    {
      if (typeof (T).IsPrimitive)
        throw new NotSupportedException();
      return (T) this.Unpack(new MsgPackReader(strm), typeof (T));
    }

    public object Unpack(Type type, byte[] buf)
    {
      return this.Unpack(type, buf, 0, buf.Length);
    }

    public object Unpack(Type type, byte[] buf, int offset, int size)
    {
      using (MemoryStream memoryStream = new MemoryStream(buf, offset, size))
        return this.Unpack(type, (Stream) memoryStream);
    }

    public object Unpack(Type type, Stream strm)
    {
      if (type.IsPrimitive)
        throw new NotSupportedException();
      return this.Unpack(new MsgPackReader(strm), type);
    }

    private object Unpack(MsgPackReader reader, Type t)
    {
      if (t.IsPrimitive)
      {
        if (!reader.Read())
          throw new FormatException();
        if (t.Equals(typeof (int)) && reader.IsSigned())
          return (object) reader.ValueSigned;
        if (t.Equals(typeof (uint)) && reader.IsUnsigned())
          return (object) reader.ValueUnsigned;
        if (t.Equals(typeof (float)) && reader.Type == TypePrefixes.Float)
          return (object) reader.ValueFloat;
        if (t.Equals(typeof (double)) && reader.Type == TypePrefixes.Double)
          return (object) reader.ValueDouble;
        if (t.Equals(typeof (long)))
        {
          if (reader.IsSigned64())
            return (object) reader.ValueSigned64;
          if (reader.IsSigned())
            return (object) (long) reader.ValueSigned;
        }
        else if (t.Equals(typeof (ulong)))
        {
          if (reader.IsUnsigned64())
            return (object) reader.ValueUnsigned64;
          if (reader.IsUnsigned())
            return (object) (ulong) reader.ValueUnsigned;
        }
        else
        {
          if (t.Equals(typeof (bool)) && reader.IsBoolean())
            return (object) (reader.Type == TypePrefixes.True);
          if (t.Equals(typeof (byte)) && reader.IsUnsigned())
            return (object) (byte) reader.ValueUnsigned;
          if (t.Equals(typeof (sbyte)) && reader.IsSigned())
            return (object) (sbyte) reader.ValueSigned;
          if (t.Equals(typeof (short)) && reader.IsSigned())
            return (object) (short) reader.ValueSigned;
          if (t.Equals(typeof (ushort)) && reader.IsUnsigned())
            return (object) (ushort) reader.ValueUnsigned;
          if (t.Equals(typeof (char)) && reader.IsUnsigned())
            return (object) (char) reader.ValueUnsigned;
          throw new NotSupportedException();
        }
      }
      ObjectPacker.UnpackDelegate unpackDelegate;
      if (ObjectPacker.UnpackerMapping.TryGetValue(t, out unpackDelegate))
        return unpackDelegate(this, reader);
      if (t.IsArray)
      {
        if (!reader.Read() || !reader.IsArray() && reader.Type != TypePrefixes.Nil)
          throw new FormatException();
        if (reader.Type == TypePrefixes.Nil)
          return (object) null;
        Type elementType = t.GetElementType();
        Array instance = Array.CreateInstance(elementType, (int) reader.Length);
        for (int index = 0; index < instance.Length; ++index)
          instance.SetValue(this.Unpack(reader, elementType), index);
        return (object) instance;
      }
      if (!reader.Read())
        throw new FormatException();
      if (reader.Type == TypePrefixes.Nil)
        return (object) null;
      if (t.IsInterface)
      {
        if (reader.Type != TypePrefixes.FixArray && (int) reader.Length != 2)
          throw new FormatException();
        if (!reader.Read() || !reader.IsRaw())
          throw new FormatException();
        this.CheckBufferSize((int) reader.Length);
        reader.ReadValueRaw(this._buf, 0, (int) reader.Length);
        t = Type.GetType(Encoding.UTF8.GetString(this._buf, 0, (int) reader.Length));
        if (!reader.Read() || reader.Type == TypePrefixes.Nil)
          throw new FormatException();
      }
      if (!reader.IsMap())
        throw new FormatException();
      object uninitializedObject = FormatterServices.GetUninitializedObject(t);
      ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(t);
      int length = (int) reader.Length;
      for (int index = 0; index < length; ++index)
      {
        if (!reader.Read() || !reader.IsRaw())
          throw new FormatException();
        this.CheckBufferSize((int) reader.Length);
        reader.ReadValueRaw(this._buf, 0, (int) reader.Length);
        string key = Encoding.UTF8.GetString(this._buf, 0, (int) reader.Length);
        FieldInfo fieldInfo;
        if (!reflectionCacheEntry.FieldMap.TryGetValue(key, out fieldInfo))
          throw new FormatException();
        fieldInfo.SetValue(uninitializedObject, this.Unpack(reader, fieldInfo.FieldType));
      }
      IDeserializationCallback deserializationCallback = uninitializedObject as IDeserializationCallback;
      if (deserializationCallback != null)
        deserializationCallback.OnDeserialization((object) this);
      return uninitializedObject;
    }

    private void CheckBufferSize(int size)
    {
      if (this._buf.Length >= size)
        return;
      Array.Resize<byte>(ref this._buf, size);
    }

    private static void StringPacker(ObjectPacker packer, MsgPackWriter writer, object o)
    {
      writer.Write(Encoding.UTF8.GetBytes((string) o));
    }

    private static object StringUnpacker(ObjectPacker packer, MsgPackReader reader)
    {
      if (!reader.Read())
        throw new FormatException();
      if (reader.Type == TypePrefixes.Nil)
        return (object) null;
      if (!reader.IsRaw())
        throw new FormatException();
      packer.CheckBufferSize((int) reader.Length);
      reader.ReadValueRaw(packer._buf, 0, (int) reader.Length);
      return (object) Encoding.UTF8.GetString(packer._buf, 0, (int) reader.Length);
    }

    private delegate void PackDelegate(ObjectPacker packer, MsgPackWriter writer, object o);

    private delegate object UnpackDelegate(ObjectPacker packer, MsgPackReader reader);
  }
}
