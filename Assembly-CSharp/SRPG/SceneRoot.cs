// Decompiled with JetBrains decompiler
// Type: SRPG.SceneRoot
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  public class SceneRoot : MonoBehaviour
  {
    public SceneRoot()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      SceneAwakeObserver.Invoke(((Component) this).get_gameObject());
    }
  }
}
