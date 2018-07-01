// Decompiled with JetBrains decompiler
// Type: SRPG.UnityPerformanceReporting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.CrashLog;

namespace SRPG
{
  public class UnityPerformanceReporting : MonoBehaviour
  {
    public UnityPerformanceReporting()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      CrashReporting.Init("8d9b4183-a378-4c53-b66a-b5ac3d9a531a", MyApplicationPlugin.get_version(), string.Empty);
    }
  }
}
