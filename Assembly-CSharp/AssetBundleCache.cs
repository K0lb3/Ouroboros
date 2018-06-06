// Decompiled with JetBrains decompiler
// Type: AssetBundleCache
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AssetBundleCache
{
  public string Name;
  public int HashCode;
  public AssetBundle AssetBundle;
  public float ExpireTime;
  public bool Persistent;
  public AssetBundleCache[] Dependencies;
  public int NumReferencers;

  public AssetBundleCache(string name, AssetBundle ab)
  {
    this.Name = name;
    this.HashCode = name.GetHashCode();
    this.AssetBundle = ab;
  }

  public void AddReferencer(int count)
  {
    this.NumReferencers += count;
  }

  public void RemoveReferencer(int count)
  {
    this.NumReferencers -= count;
  }

  public void Unload()
  {
    if (!Object.op_Inequality((Object) this.AssetBundle, (Object) null))
      return;
    this.AssetBundle.Unload(false);
    this.AssetBundle = (AssetBundle) null;
  }
}
