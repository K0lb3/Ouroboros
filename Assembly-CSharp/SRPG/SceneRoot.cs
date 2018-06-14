// Decompiled with JetBrains decompiler
// Type: SRPG.SceneRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  [DisallowMultipleComponent]
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
