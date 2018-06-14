// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCacheEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MsgPack
{
  public class ReflectionCacheEntry
  {
    private const BindingFlags FieldBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField;

    public ReflectionCacheEntry(Type t)
    {
      FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField);
      IDictionary<string, FieldInfo> dictionary = (IDictionary<string, FieldInfo>) new Dictionary<string, FieldInfo>(fields.Length);
      for (int index1 = 0; index1 < fields.Length; ++index1)
      {
        FieldInfo fieldInfo = fields[index1];
        string index2 = fieldInfo.Name;
        int num;
        if ((int) index2[0] == 60 && (num = index2.IndexOf('>')) > 1)
          index2 = index2.Substring(1, num - 1);
        dictionary[index2] = fieldInfo;
      }
      this.FieldMap = dictionary;
    }

    public IDictionary<string, FieldInfo> FieldMap { get; private set; }
  }
}
