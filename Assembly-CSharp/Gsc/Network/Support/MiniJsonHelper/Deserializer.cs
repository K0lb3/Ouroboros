// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Support.MiniJsonHelper.Deserializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network.Data;
using Gsc.Support.Obfuscation;
using System;
using System.Collections.Generic;

namespace Gsc.Network.Support.MiniJsonHelper
{
  public class Deserializer
  {
    public static readonly Deserializer Instance = new Deserializer();
    private Stack<Delegate> stack = new Stack<Delegate>();
    private List<Delegate> functions = new List<Delegate>();

    private Deserializer()
    {
    }

    public Deserializer WithArray<T>()
    {
      this.Add<T[]>(new Func<object, T[]>(this.ToArray<T>));
      return this;
    }

    public Deserializer WithArray<T>(Func<object, T> func)
    {
      this.Add<T[]>(new Func<object, T[]>(this.ToArray<T>));
      this.Add<T>(func);
      return this;
    }

    public Deserializer WithDict<TKey, TValue>()
    {
      this.Add<Dictionary<TKey, TValue>>(new Func<object, Dictionary<TKey, TValue>>(this.ToDictionary<TKey, TValue>));
      return this;
    }

    public Deserializer WithDict<TKey, TValue>(Func<object, TKey> keyFunc, Func<object, TValue> valueFunc)
    {
      this.Add<Dictionary<TKey, TValue>>(new Func<object, Dictionary<TKey, TValue>>(this.ToDictionary<TKey, TValue>));
      this.Add<TKey>(keyFunc);
      this.Add<TValue>(valueFunc);
      return this;
    }

    public Deserializer Add<T>(Func<object, T> func)
    {
      this.functions.Add((Delegate) func);
      return this;
    }

    public T Deserialize<T>(object source)
    {
      for (int index = this.functions.Count - 1; index >= 0; --index)
        this.stack.Push(this.functions[index]);
      this.functions.Clear();
      return ((Func<object, T>) this.stack.Pop())(source);
    }

    private T[] ToArray<T>(object source_)
    {
      List<object> objectList = (List<object>) source_;
      int count = objectList.Count;
      T[] objArray = new T[count];
      Func<object, T> func = (Func<object, T>) this.stack.Pop();
      for (int index = 0; index < count; ++index)
        objArray[index] = func(objectList[index]);
      return objArray;
    }

    private Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(object source_)
    {
      Dictionary<string, object> dictionary1 = (Dictionary<string, object>) source_;
      List<object> objectList1 = (List<object>) dictionary1["keys"];
      List<object> objectList2 = (List<object>) dictionary1["values"];
      int count = objectList1.Count;
      Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(count);
      Func<object, TKey> func1 = (Func<object, TKey>) this.stack.Pop();
      Func<object, TValue> func2 = (Func<object, TValue>) this.stack.Pop();
      for (int index = 0; index < count; ++index)
        dictionary2.Add(func1(objectList1[index]), func2(objectList2[index]));
      return dictionary2;
    }

    public static T ToModelObjectCache<T>(object source) where T : Entity<T>
    {
      if (source == null)
        return (T) null;
      Type type = source.GetType();
      if ((object) type == (object) typeof (int) || (object) type == (object) typeof (long) || ((object) type == (object) typeof (uint) || (object) type == (object) typeof (ulong)))
        return EntityRepository.Get<T>(source.ToString());
      return EntityRepository.Get<T>((string) source);
    }

    public static T ModelObject<T>(object source) where T : Entity<T>
    {
      if (source == null)
        return (T) null;
      return AssemblySupport.CreateInstance<T>(new object[1]{ source });
    }

    public static T ToObject<T>(object source) where T : class, IResponseObject
    {
      if (source == null)
        return (T) null;
      return AssemblySupport.CreateInstance<T>(new object[1]{ source });
    }

    public static DateTime ToDateTime(object source)
    {
      if (source == null)
        return DateTime.MinValue;
      return DateTime.Parse((string) source);
    }

    public static T To<T>(object source)
    {
      return (T) source;
    }

    public static class ToIntegerType
    {
      public static sbyte int8(object source)
      {
        return (sbyte) (long) source;
      }

      public static short int16(object source)
      {
        return (short) (long) source;
      }

      public static int int32(object source)
      {
        return (int) (long) source;
      }

      public static long int64(object source)
      {
        return (long) source;
      }

      public static byte uint8(object source)
      {
        return (byte) (long) source;
      }

      public static ushort uint16(object source)
      {
        return (ushort) (long) source;
      }

      public static uint uint32(object source)
      {
        return (uint) (long) source;
      }

      public static ulong uint64(object source)
      {
        return (ulong) (long) source;
      }
    }

    public static class ToNumberType
    {
      public static float float32(object source)
      {
        try
        {
          return (float) (double) source;
        }
        catch (InvalidCastException ex)
        {
          return (float) (long) source;
        }
      }

      public static double float64(object source)
      {
        try
        {
          return (double) source;
        }
        catch (InvalidCastException ex)
        {
          return (double) (long) source;
        }
      }
    }

    public static class ToObfuscatedType
    {
      public static ObfuscatedBool boolean(object source)
      {
        return ObfuscatedBool.op_Implicit((bool) source);
      }

      public static ObfuscatedSByte int8(object source)
      {
        return ObfuscatedSByte.op_Implicit(Deserializer.ToIntegerType.int8(source));
      }

      public static ObfuscatedShort int16(object source)
      {
        return ObfuscatedShort.op_Implicit(Deserializer.ToIntegerType.int16(source));
      }

      public static ObfuscatedInt int32(object source)
      {
        return ObfuscatedInt.op_Implicit(Deserializer.ToIntegerType.int32(source));
      }

      public static ObfuscatedLong int64(object source)
      {
        return ObfuscatedLong.op_Implicit(Deserializer.ToIntegerType.int64(source));
      }

      public static ObfuscatedByte uint8(object source)
      {
        return ObfuscatedByte.op_Implicit(Deserializer.ToIntegerType.uint8(source));
      }

      public static ObfuscatedUShort uint16(object source)
      {
        return ObfuscatedUShort.op_Implicit(Deserializer.ToIntegerType.uint16(source));
      }

      public static ObfuscatedUInt uint32(object source)
      {
        return ObfuscatedUInt.op_Implicit(Deserializer.ToIntegerType.uint32(source));
      }

      public static ObfuscatedULong uint64(object source)
      {
        return ObfuscatedULong.op_Implicit(Deserializer.ToIntegerType.uint64(source));
      }

      public static ObfuscatedFloat float32(object source)
      {
        return ObfuscatedFloat.op_Implicit(Deserializer.ToNumberType.float32(source));
      }

      public static ObfuscatedDouble float64(object source)
      {
        return ObfuscatedDouble.op_Implicit(Deserializer.ToNumberType.float64(source));
      }
    }
  }
}
