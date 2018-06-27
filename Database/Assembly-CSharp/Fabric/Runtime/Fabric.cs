// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Fabric
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Runtime.Internal;
using System;
using System.Reflection;
using UnityEngine;

namespace Fabric.Runtime
{
  public class Fabric
  {
    private static readonly Impl impl = Impl.Make();

    public static void Initialize()
    {
      string str1 = Fabric.Runtime.Fabric.impl.Initialize();
      if (string.IsNullOrEmpty(str1))
        return;
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string kitMethod in str2.Split(chArray))
        Fabric.Runtime.Fabric.Initialize(kitMethod);
    }

    internal static void Initialize(string kitMethod)
    {
      int length = kitMethod.LastIndexOf('.');
      string typeName = kitMethod.Substring(0, length);
      string name = kitMethod.Substring(length + 1);
      Type type = Type.GetType(typeName);
      if ((object) type == null)
        return;
      MethodInfo method = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      if ((object) method == null)
        return;
      object obj = !typeof (ScriptableObject).IsAssignableFrom(type) ? Activator.CreateInstance(type) : (object) ScriptableObject.CreateInstance(type);
      if (obj == null)
        return;
      method.Invoke(obj, new object[0]);
    }
  }
}
