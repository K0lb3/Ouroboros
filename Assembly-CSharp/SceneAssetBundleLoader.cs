// Decompiled with JetBrains decompiler
// Type: SceneAssetBundleLoader
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
