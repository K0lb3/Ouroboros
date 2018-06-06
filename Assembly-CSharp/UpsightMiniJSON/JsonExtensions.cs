// Decompiled with JetBrains decompiler
// Type: UpsightMiniJSON.JsonExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace UpsightMiniJSON
{
  public static class JsonExtensions
  {
    public static T GetPrimitive<T>(this Dictionary<string, object> dict, string key) where T : IConvertible
    {
      return (T) Convert.ChangeType(dict[key], typeof (T));
    }

    public static bool TryGetPrimitive<T>(this Dictionary<string, object> dict, string key, out T value) where T : IConvertible
    {
      try
      {
        if (dict != null)
        {
          if (dict.ContainsKey(key))
          {
            object obj = Convert.ChangeType(dict[key], typeof (T));
            if (obj is T)
            {
              if (obj != null)
              {
                value = (T) obj;
                return true;
              }
            }
          }
        }
      }
      catch
      {
      }
      value = default (T);
      return false;
    }

    public static List<object> GetJsonArray(this Dictionary<string, object> dict, string key)
    {
      if (dict != null && dict.ContainsKey(key))
        return dict[key] as List<object>;
      return (List<object>) null;
    }

    public static Dictionary<string, object> GetJsonObject(this Dictionary<string, object> dict, string key)
    {
      if (dict != null && dict.ContainsKey(key))
        return dict[key] as Dictionary<string, object>;
      return (Dictionary<string, object>) null;
    }

    public static List<T> GetPrimitiveArray<T>(this Dictionary<string, object> dict, string key) where T : IConvertible
    {
      List<object> jsonArray = dict.GetJsonArray(key);
      if (jsonArray == null)
        return (List<T>) null;
      List<T> objList = new List<T>(jsonArray.Count);
      for (int index = 0; index < jsonArray.Count; ++index)
        objList[index] = jsonArray.GetPrimitive<T>(index);
      return objList;
    }

    public static Dictionary<string, T> GetPrimitiveDictionary<T>(this Dictionary<string, object> dict, string key) where T : IConvertible
    {
      Dictionary<string, object> jsonObject = dict.GetJsonObject(key);
      if (jsonObject == null)
        return (Dictionary<string, T>) null;
      Dictionary<string, T> dictionary = new Dictionary<string, T>();
      using (Dictionary<string, object>.KeyCollection.Enumerator enumerator = jsonObject.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          dictionary.Add(current, jsonObject.GetPrimitive<T>(current));
        }
      }
      return dictionary;
    }

    public static T GetPrimitive<T>(this List<object> list, int index) where T : IConvertible
    {
      return (T) Convert.ChangeType(list[index], typeof (T));
    }

    public static bool TryGetPrimitive<T>(this List<object> list, int index, out T value) where T : IConvertible
    {
      try
      {
        if (list != null)
        {
          if (index > 0)
          {
            if (index < list.Count)
            {
              object obj = Convert.ChangeType(list[index], typeof (T));
              if (obj is T)
              {
                if (obj != null)
                {
                  value = (T) obj;
                  return true;
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      value = default (T);
      return false;
    }

    public static List<object> GetJsonArray(this List<object> list, int index)
    {
      if (list != null && index > 0 && index < list.Count)
        return list[index] as List<object>;
      return (List<object>) null;
    }

    public static Dictionary<string, object> GetJsonObject(this List<object> list, int index)
    {
      if (list != null && index > 0 && index < list.Count)
        return list[index] as Dictionary<string, object>;
      return (Dictionary<string, object>) null;
    }

    public static List<T> GetPrimitiveArray<T>(this List<object> list, int index) where T : IConvertible
    {
      List<object> jsonArray = list.GetJsonArray(index);
      if (jsonArray == null)
        return (List<T>) null;
      List<T> objList = new List<T>(jsonArray.Count);
      for (int index1 = 0; index1 < jsonArray.Count; ++index1)
        objList[index1] = jsonArray.GetPrimitive<T>(index1);
      return objList;
    }

    public static Dictionary<string, T> GetPrimitiveDictionary<T>(this List<object> list, int index) where T : IConvertible
    {
      Dictionary<string, object> jsonObject = list.GetJsonObject(index);
      if (jsonObject == null)
        return (Dictionary<string, T>) null;
      Dictionary<string, T> dictionary = new Dictionary<string, T>();
      using (Dictionary<string, object>.KeyCollection.Enumerator enumerator = jsonObject.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          dictionary.Add(current, jsonObject.GetPrimitive<T>(current));
        }
      }
      return dictionary;
    }
  }
}
