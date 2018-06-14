// Decompiled with JetBrains decompiler
// Type: SceneAssetBundle
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SceneAssetBundle : MonoBehaviour
{
  public int Hash;

  public SceneAssetBundle()
  {
    base.\u002Ector();
  }

  private void LateUpdate()
  {
    this.CheckChildren();
  }

  private void CheckChildren()
  {
    if (((Component) this).get_transform().get_childCount() > 0)
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
