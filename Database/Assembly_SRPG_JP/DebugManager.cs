// Decompiled with JetBrains decompiler
// Type: SRPG.DebugManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  [AddComponentMenu("Scripts/SRPG/Manager/Debug")]
  public class DebugManager : MonoSingleton<DebugManager>
  {
    private float mLastCollectNum;
    private int mAllocMem;
    private int mAllocPeak;

    public bool IsShowed { set; get; }

    public bool IsShowedInEditor { set; get; }

    protected override void Initialize()
    {
      this.IsShowed = true;
      this.IsShowedInEditor = false;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    private void Update()
    {
      if (!this.IsShowed || !Application.get_isPlaying() && !this.IsShowedInEditor)
        return;
      if ((double) this.mLastCollectNum == (double) GC.CollectionCount(0))
        ;
      this.mAllocMem = (int) Profiler.get_usedHeapSize();
      this.mAllocPeak = this.mAllocMem <= this.mAllocPeak ? this.mAllocPeak : this.mAllocMem;
    }

    public bool IsWebViewEnable()
    {
      return true;
    }
  }
}
