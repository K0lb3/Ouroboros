// Decompiled with JetBrains decompiler
// Type: SceneAssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
