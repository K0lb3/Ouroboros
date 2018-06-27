// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace MsgPack
{
  public static class ReflectionCache
  {
    private static Dictionary<Type, ReflectionCacheEntry> _cache = new Dictionary<Type, ReflectionCacheEntry>();

    public static ReflectionCacheEntry Lookup(Type type)
    {
      lock (ReflectionCache._cache)
      {
        ReflectionCacheEntry reflectionCacheEntry;
        if (ReflectionCache._cache.TryGetValue(type, out reflectionCacheEntry))
          return reflectionCacheEntry;
      }
      ReflectionCacheEntry reflectionCacheEntry1 = new ReflectionCacheEntry(type);
      lock (ReflectionCache._cache)
        ReflectionCache._cache[type] = reflectionCacheEntry1;
      return reflectionCacheEntry1;
    }

    public static void RemoveCache(Type type)
    {
      lock (ReflectionCache._cache)
        ReflectionCache._cache.Remove(type);
    }

    public static void Clear()
    {
      lock (ReflectionCache._cache)
        ReflectionCache._cache.Clear();
    }
  }
}
