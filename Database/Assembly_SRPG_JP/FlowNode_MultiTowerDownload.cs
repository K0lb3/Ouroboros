// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerDownload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "ダウンロード開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Download/MultiTower", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiTowerDownload : FlowNode
  {
    [SerializeField]
    private int DownloadAssetNums = 10;

    public int DownloadAssetNum
    {
      get
      {
        return this.DownloadAssetNums;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).set_enabled(true);
      if (GameUtility.Config_UseAssetBundles.Value && AssetManager.AssetRevision > 0)
      {
        this.StartCoroutine(this.DownloadFloorParamAsync());
      }
      else
      {
        this.ActivateOutputLinks(1);
        ((Behaviour) this).set_enabled(false);
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParamAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CDownloadFloorParamAsync\u003Ec__IteratorC0()
      {
        \u003C\u003Ef__this = this
      };
    }

    public static void DownloadMapSets(List<MultiTowerFloorParam> floorParams)
    {
      if (floorParams == null)
        return;
      for (int index1 = 0; index1 < floorParams.Count; ++index1)
      {
        if (floorParams[index1].map != null)
        {
          for (int index2 = 0; index2 < floorParams[index1].map.Count; ++index2)
          {
            string mapSetName = floorParams[index1].map[index2].mapSetName;
            if (!string.IsNullOrEmpty(mapSetName))
              AssetManager.PrepareAssets(AssetPath.LocalMap(mapSetName));
          }
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator RequestDownloadFloors(List<MultiTowerFloorParam> floorParams)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CRequestDownloadFloors\u003Ec__IteratorC1()
      {
        floorParams = floorParams,
        \u003C\u0024\u003EfloorParams = floorParams,
        \u003C\u003Ef__this = this
      };
    }

    private void DownloadPlacementAsset(MultiTowerFloorParam param)
    {
      if (param.map == null)
        return;
      for (int index1 = 0; index1 < param.map.Count; ++index1)
      {
        if (!string.IsNullOrEmpty(param.map[index1].mapSetName))
        {
          string src = AssetManager.LoadTextData(AssetPath.LocalMap(param.map[index1].mapSetName));
          if (src != null)
          {
            JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
            if (jsonObject != null)
            {
              for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
                DownloadUtility.LoadUnitIconMedium(jsonObject.enemy[index2].iname);
            }
          }
        }
      }
    }
  }
}
