// Decompiled with JetBrains decompiler
// Type: SRPG.Scene
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
