// Decompiled with JetBrains decompiler
// Type: SRPG.Scene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public abstract class Scene : MonoBehaviour
  {
    protected Scene()
    {
      base.\u002Ector();
    }

    protected bool IsLoaded { get; set; }

    protected void Awake()
    {
      MonoSingleton<SystemInstance>.Instance.Ensure();
      GameUtility.RemoveDuplicatedMainCamera();
    }
  }
}
