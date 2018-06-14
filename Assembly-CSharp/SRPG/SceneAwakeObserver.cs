// Decompiled with JetBrains decompiler
// Type: SRPG.SceneAwakeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public static class SceneAwakeObserver
  {
    private static SceneAwakeObserver.SceneEvent mListeners = (SceneAwakeObserver.SceneEvent) (go => {});

    public static void ClearListeners()
    {
      SceneAwakeObserver.mListeners = (SceneAwakeObserver.SceneEvent) (go => {});
    }

    public static void AddListener(SceneAwakeObserver.SceneEvent listener)
    {
      SceneAwakeObserver.mListeners += listener;
    }

    public static void RemoveListener(SceneAwakeObserver.SceneEvent listener)
    {
      SceneAwakeObserver.mListeners -= listener;
    }

    public static void Invoke(GameObject scene)
    {
      if (SceneAwakeObserver.mListeners == null)
        return;
      System.Delegate[] invocationList = SceneAwakeObserver.mListeners.GetInvocationList();
      for (int index = 0; index < invocationList.Length; ++index)
      {
        if (!(invocationList[index].Target is UnityEngine.Object) || UnityEngine.Object.op_Inequality((UnityEngine.Object) invocationList[index].Target, (UnityEngine.Object) null))
          ((SceneAwakeObserver.SceneEvent) invocationList[index])(scene);
      }
    }

    public delegate void SceneEvent(GameObject sceneRoot);
  }
}
