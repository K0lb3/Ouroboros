// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Parser.Des
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.DOM;
using Gsc.DOM.Generic;
using Gsc.Network.Data;
using Gsc.Support.Obfuscation;
using System;
using System.Collections.Generic;

namespace Gsc.Network.Parser
{
  public class Des
  {
    public static readonly Des Ins = new Des();
    private static readonly Dictionary<Type, Delegate> defaultConverters = new Dictionary<Type, Delegate>();
    private Stack<Delegate> stack = new Stack<Delegate>();
    private List<Delegate> functions = new List<Delegate>();

    private Des()
    {
    }

    static Des()
    {
      Des.defaultConverters.Add(typeof (DateTime), (Delegate) new Func<IValue, DateTime>(Des.ToDateTime));
      Des.defaultConverters.Add(typeof (string), (Delegate) new Func<IValue, string>(Des.ToString));
      Des.defaultConverters.Add(typeof (bool), (Delegate) new Func<IValue, bool>(Des.ToBool));
      Des.defaultConverters.Add(typeof (sbyte), (Delegate) new Func<IValue, sbyte>(Des.ToSByte));
      Des.defaultConverters.Add(typeof (byte), (Delegate) new Func<IValue, byte>(Des.ToByte));
      Des.defaultConverters.Add(typeof (short), (Delegate) new Func<IValue, short>(Des.ToShort));
      Des.defaultConverters.Add(typeof (ushort), (Delegate) new Func<IValue, ushort>(Des.ToUShort));
      Des.defaultConverters.Add(typeof (int), (Delegate) new Func<IValue, int>(Des.ToInt));
      Des.defaultConverters.Add(typeof (uint), (Delegate) new Func<IValue, uint>(Des.ToUInt));
      Des.defaultConverters.Add(typeof (long), (Delegate) new Func<IValue, long>(Des.ToLong));
      Des.defaultConverters.Add(typeof (ulong), (Delegate) new Func<IValue, ulong>(Des.ToULong));
      Des.defaultConverters.Add(typeof (float), (Delegate) new Func<IValue, float>(Des.ToFloat));
      Des.defaultConverters.Add(typeof (double), (Delegate) new Func<IValue, double>(Des.ToDouble));
      Des.defaultConverters.Add(typeof (ObfuscatedBool), (Delegate) new Func<IValue, ObfuscatedBool>(Des.ToObfuscatedType.boolean));
      Des.defaultConverters.Add(typeof (ObfuscatedSByte), (Delegate) new Func<IValue, ObfuscatedSByte>(Des.ToObfuscatedType.int8));
      Des.defaultConverters.Add(typeof (ObfuscatedByte), (Delegate) new Func<IValue, ObfuscatedByte>(Des.ToObfuscatedType.uint8));
      Des.defaultConverters.Add(typeof (ObfuscatedShort), (Delegate) new Func<IValue, ObfuscatedShort>(Des.ToObfuscatedType.int16));
      Des.defaultConverters.Add(typeof (ObfuscatedUShort), (Delegate) new Func<IValue, ObfuscatedUShort>(Des.ToObfuscatedType.uint16));
      Des.defaultConverters.Add(typeof (ObfuscatedInt), (Delegate) new Func<IValue, ObfuscatedInt>(Des.ToObfuscatedType.int32));
      Des.defaultConverters.Add(typeof (ObfuscatedUInt), (Delegate) new Func<IValue, ObfuscatedUInt>(Des.ToObfuscatedType.uint32));
      Des.defaultConverters.Add(typeof (ObfuscatedLong), (Delegate) new Func<IValue, ObfuscatedLong>(Des.ToObfuscatedType.int64));
      Des.defaultConverters.Add(typeof (ObfuscatedULong), (Delegate) new Func<IValue, ObfuscatedULong>(Des.ToObfuscatedType.uint64));
      Des.defaultConverters.Add(typeof (ObfuscatedFloat), (Delegate) new Func<IValue, ObfuscatedFloat>(Des.ToObfuscatedType.float32));
      Des.defaultConverters.Add(typeof (ObfuscatedDouble), (Delegate) new Func<IValue, ObfuscatedDouble>(Des.ToObfuscatedType.float64));
    }

    public T Deserialize<T>(IValue source)
    {
      return this.Deserialize<T, IValue>(source);
    }

    private T Deserialize<T, TSource>(TSource source)
    {
      for (int index = this.functions.Count - 1; index >= 0; --index)
        this.stack.Push(this.functions[index]);
      this.functions.Clear();
      return ((Func<TSource, T>) this.stack.Pop())(source);
    }

    public Des Add<T>(Func<IValue, T> func)
    {
      this.functions.Add((Delegate) func);
      return this;
    }

    public Des Arr<T>()
    {
      this.Add<T[]>(new Func<IValue, T[]>(this.ToArray<T>));
      return this;
    }

    public static T To<T>(IValue v)
    {
      return ((Func<IValue, T>) Des.defaultConverters[typeof (T)])(v);
    }

    public static T ToEntity<T>(IValue v) where T : Entity<T>
    {
      if (v.IsNull())
        return (T) null;
      if (v.IsLong())
        return EntityRepository.Get<T>(v.ToLong().ToString());
      return EntityRepository.Get<T>(v.ToString());
    }

    public static T ToEntity<T>(object v) where T : Entity<T>
    {
      if (v == null)
        return (T) null;
      return EntityRepository.Get<T>(v.ToString());
    }

    public static T ToObject<T>(IValue v) where T : class, IResponseObject
    {
      if (v.IsNull())
        return (T) null;
      return AssemblySupport.CreateInstance<T>(new object[1]{ (object) v.GetObject() });
    }

    public static DateTime ToDateTime(IValue v)
    {
      if (v.IsNull())
        return DateTime.MinValue;
      return DateTime.Parse(v.ToString());
    }

    public static sbyte ToSByte(IValue v)
    {
      return (sbyte) v.ToInt();
    }

    public static byte ToByte(IValue v)
    {
      return (byte) v.ToInt();
    }

    public static short ToShort(IValue v)
    {
      return (short) (sbyte) v.ToInt();
    }

    public static ushort ToUShort(IValue v)
    {
      return (ushort) v.ToInt();
    }

    public static int ToInt(IValue v)
    {
      return v.ToInt();
    }

    public static uint ToUInt(IValue v)
    {
      return v.ToUInt();
    }

    public static long ToLong(IValue v)
    {
      return v.ToLong();
    }

    public static ulong ToULong(IValue v)
    {
      return v.ToULong();
    }

    public static float ToFloat(IValue v)
    {
      return v.ToFloat();
    }

    public static double ToDouble(IValue v)
    {
      return v.ToDouble();
    }

    public static string ToString(IValue v)
    {
      return v.ToString();
    }

    public static bool ToBool(IValue v)
    {
      return v.ToBool();
    }

    private T[] ToArray<T>(IValue source)
    {
      IArray array = source.GetArray();
      T[] objArray = new T[array.Length];
      Func<IValue, T> func = (Func<IValue, T>) this.stack.Pop();
      for (int index = 0; index < array.Length; ++index)
        objArray[index] = func(array[index]);
      return objArray;
    }

    public static Value ToStringTree(IValue v)
    {
      return !v.IsObject() ? (!v.IsArray() ? (Value) v.ToString() : Des.ToStringTree(v.GetArray())) : Des.ToStringTree(v.GetObject());
    }

    private static Value ToStringTree(Gsc.DOM.IObject v)
    {
      Gsc.DOM.Generic.Object @object = new Gsc.DOM.Generic.Object();
      foreach (IMember member in (IEnumerable<IMember>) v)
        @object.Add(member.Name, Des.ToStringTree(member.Value));
      return (Value) @object;
    }

    private static Value ToStringTree(IArray v)
    {
      Gsc.DOM.Generic.Array array = new Gsc.DOM.Generic.Array();
      foreach (IValue v1 in (IEnumerable<IValue>) v)
        array.Add(Des.ToStringTree(v1));
      return (Value) array;
    }

    public static class ToObfuscatedType
    {
      public static ObfuscatedBool boolean(IValue source)
      {
        return ObfuscatedBool.op_Implicit(source.ToBool());
      }

      public static ObfuscatedSByte int8(IValue source)
      {
        return ObfuscatedSByte.op_Implicit((sbyte) source.ToInt());
      }

      public static ObfuscatedShort int16(IValue source)
      {
        return ObfuscatedShort.op_Implicit((short) source.ToInt());
      }

      public static ObfuscatedInt int32(IValue source)
      {
        return ObfuscatedInt.op_Implicit(source.ToInt());
      }

      public static ObfuscatedLong int64(IValue source)
      {
        return ObfuscatedLong.op_Implicit(source.ToLong());
      }

      public static ObfuscatedByte uint8(IValue source)
      {
        return ObfuscatedByte.op_Implicit((byte) source.ToInt());
      }

      public static ObfuscatedUShort uint16(IValue source)
      {
        return ObfuscatedUShort.op_Implicit((ushort) source.ToInt());
      }

      public static ObfuscatedUInt uint32(IValue source)
      {
        return ObfuscatedUInt.op_Implicit(source.ToUInt());
      }

      public static ObfuscatedULong uint64(IValue source)
      {
        return ObfuscatedULong.op_Implicit(source.ToULong());
      }

      public static ObfuscatedFloat float32(IValue source)
      {
        return ObfuscatedFloat.op_Implicit(source.ToFloat());
      }

      public static ObfuscatedDouble float64(IValue source)
      {
        return ObfuscatedDouble.op_Implicit(source.ToDouble());
      }
    }
  }
}
