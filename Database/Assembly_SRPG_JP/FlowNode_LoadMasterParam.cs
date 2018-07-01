// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoadMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Threading;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Finished", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/LoadMasterParam", 16777215)]
  public class FlowNode_LoadMasterParam : FlowNode
  {
    private Mutex mMutex;
    private int mResult;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      this.mResult = 0;
      if (GameUtility.Config_UseAssetBundles.Value)
      {
        ((Behaviour) this).set_enabled(true);
        CriticalSection.Enter(CriticalSections.Default);
        FlowNode_LoadMasterParam.ThreadStartParam threadStartParam = new FlowNode_LoadMasterParam.ThreadStartParam();
        threadStartParam.self = this;
        threadStartParam.GameManager = MonoSingleton<GameManager>.Instance;
        Debug.Log((object) "Starting Thread");
        this.mMutex = new Mutex();
        new Thread(new ParameterizedThreadStart(FlowNode_LoadMasterParam.LoadMasterDataThread)).Start((object) threadStartParam);
      }
      else
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(100);
      }
    }

    protected override void OnDestroy()
    {
      if (this.mMutex != null)
      {
        this.mMutex.WaitOne();
        this.mMutex.ReleaseMutex();
        this.mMutex.Close();
        this.mMutex = (Mutex) null;
      }
      base.OnDestroy();
    }

    private void Update()
    {
      if (this.mMutex == null)
        return;
      this.mMutex.WaitOne();
      bool flag = this.mResult != 0;
      this.mMutex.ReleaseMutex();
      if (!flag)
        return;
      this.mMutex.Close();
      this.mMutex = (Mutex) null;
      if (this.mResult < 0)
        DebugUtility.LogError("Failed to load MasterParam");
      ((Behaviour) this).set_enabled(false);
      CriticalSection.Leave(CriticalSections.Default);
      MonoSingleton<GameManager>.Instance.MasterParam.DumpLoadedLog();
      this.ActivateOutputLinks(100);
    }

    private static void LoadMasterDataThread(object param)
    {
      Debug.Log((object) nameof (LoadMasterDataThread));
      FlowNode_LoadMasterParam.ThreadStartParam threadStartParam = (FlowNode_LoadMasterParam.ThreadStartParam) param;
      Debug.Log((object) "LoadMasterDataThread START");
      int num = -1;
      try
      {
        num = !threadStartParam.GameManager.ReloadMasterData((string) null, (string) null) ? -1 : 1;
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      Debug.Log((object) "LoadMasterDataThread END");
      if (threadStartParam.self.mMutex == null)
        return;
      threadStartParam.self.mMutex.WaitOne();
      threadStartParam.self.mResult = num;
      threadStartParam.self.mMutex.ReleaseMutex();
    }

    private struct ThreadStartParam
    {
      public FlowNode_LoadMasterParam self;
      public GameManager GameManager;
    }
  }
}
