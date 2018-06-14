// Decompiled with JetBrains decompiler
// Type: TunagiScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TunagiScene : MonoBehaviour
{
  private static List<TunagiScene> mScenes = new List<TunagiScene>();
  public static bool AutoDeactivateScene;
  public static TunagiScene.LoadCompleteEvent OnLoadComplete;
  public float ZMin;
  public float ZMax;

  public TunagiScene()
  {
    base.\u002Ector();
  }

  public static int SceneCount
  {
    get
    {
      return TunagiScene.mScenes.Count;
    }
  }

  public static void SetScenesActive(bool active)
  {
    for (int index = 0; index < TunagiScene.mScenes.Count; ++index)
      ((Component) TunagiScene.mScenes[index]).get_gameObject().SetActive(active);
  }

  private void Awake()
  {
    TunagiScene.mScenes.Add(this);
    if (TunagiScene.OnLoadComplete != null)
      TunagiScene.OnLoadComplete(this);
    if (!TunagiScene.AutoDeactivateScene)
      return;
    ((Component) this).get_gameObject().SetActive(false);
    TunagiScene.AutoDeactivateScene = false;
  }

  private void OnDestroy()
  {
    TunagiScene.mScenes.Remove(this);
  }

  public static void PopFirstScene()
  {
    if (TunagiScene.mScenes.Count <= 0)
      return;
    Object.DestroyImmediate((Object) ((Component) TunagiScene.mScenes[0]).get_gameObject());
  }

  public static void DestroyAllScenes()
  {
    while (TunagiScene.mScenes.Count > 0)
      Object.DestroyImmediate((Object) ((Component) TunagiScene.mScenes[TunagiScene.mScenes.Count - 1]).get_gameObject());
  }

  public static TunagiScene LastScene
  {
    get
    {
      if (TunagiScene.mScenes.Count > 0)
        return TunagiScene.mScenes[TunagiScene.mScenes.Count - 1];
      return (TunagiScene) null;
    }
  }

  public static TunagiScene FirstScene
  {
    get
    {
      if (TunagiScene.mScenes.Count > 0)
        return TunagiScene.mScenes[0];
      return (TunagiScene) null;
    }
  }

  public delegate void LoadCompleteEvent(TunagiScene scene);
}
