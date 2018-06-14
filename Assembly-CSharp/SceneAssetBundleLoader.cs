// Decompiled with JetBrains decompiler
// Type: SceneAssetBundleLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SceneAssetBundleLoader : MonoBehaviour
{
  public static Object SceneBundle;

  public SceneAssetBundleLoader()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if (Object.op_Inequality(SceneAssetBundleLoader.SceneBundle, (Object) null))
    {
      Object.Instantiate(SceneAssetBundleLoader.SceneBundle);
      SceneAssetBundleLoader.SceneBundle = (Object) null;
    }
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
