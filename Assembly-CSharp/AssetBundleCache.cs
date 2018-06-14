// Decompiled with JetBrains decompiler
// Type: AssetBundleCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
