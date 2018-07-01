// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DownloadAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/アセットのダウンロード", 16711935)]
  [FlowNode.Pin(99, "エラー発生", FlowNode.PinTypes.Output, 99)]
  [FlowNode.Pin(100, "キャンセル", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(11, "ダウンロード完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "ダウンロード開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "確認", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "ダウンロード", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_DownloadAssets : FlowNode
  {
    public string[] AssetPaths = new string[0];
    public string[] DownloadQuests = new string[0];
    public string[] DownloadUnits = new string[0];
    public bool AutoRetry = true;
    public bool UpdateFileList;
    [BitMask]
    public FlowNode_DownloadAssets.DownloadAssets Download;
    public string ConfirmText;
    public string AlreadyDownloadText;
    public string CompleteText;
    public bool ProgressBar;
    public bool SkipIfTutIncomplete;
    private List<AssetList.Item> mQueue;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 1 || ((Behaviour) this).get_enabled())
        return;
      if (!GameUtility.Config_UseAssetBundles.Value)
        this.ActivateOutputLinks(11);
      else if (this.SkipIfTutIncomplete && (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
      {
        this.ActivateOutputLinks(11);
      }
      else
      {
        ((Behaviour) this).set_enabled(true);
        this.StartCoroutine(this.AsyncWork(pinID == 1));
      }
    }

    private void OnDownloadStart(GameObject dialog)
    {
      FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", "1");
      this.StartCoroutine(this.AsyncWork(false));
      this.ActivateOutputLinks(10);
    }

    private void OnDownloadCancel(GameObject dialog)
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    [DebuggerHidden]
    private IEnumerator AsyncWork(bool confirm)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadAssets.\u003CAsyncWork\u003Ec__IteratorB2()
      {
        confirm = confirm,
        \u003C\u0024\u003Econfirm = confirm,
        \u003C\u003Ef__this = this
      };
    }

    private void AddAssets()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (this.mQueue != null)
      {
        for (int index = 0; index < this.mQueue.Count; ++index)
          AssetDownloader.Add(this.mQueue[index].IDStr);
        this.mQueue = (List<AssetList.Item>) null;
      }
      if (this.Download == (FlowNode_DownloadAssets.DownloadAssets) -1)
        return;
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.Tutorial) != (FlowNode_DownloadAssets.DownloadAssets) 0 && instanceDirect.HasTutorialDLAssets)
        instanceDirect.DownloadTutorialAssets();
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.OwnUnits) != (FlowNode_DownloadAssets.DownloadAssets) 0)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
        {
          for (int index = 0; index < player.Units.Count; ++index)
          {
            DownloadUtility.DownloadUnit(player.Units[index].UnitParam, player.Units[index].Jobs);
            if (player.Units[index].ConceptCard != null)
              DownloadUtility.DownloadConceptCard(player.Units[index].ConceptCard.Param);
          }
        }
      }
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.AllUnits) != (FlowNode_DownloadAssets.DownloadAssets) 0)
      {
        UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam?.GetAllUnits();
        if (allUnits != null)
        {
          for (int index = 0; index < allUnits.Length; ++index)
            DownloadUtility.DownloadUnit(allUnits[index], (JobData[]) null);
        }
      }
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.LoginBonus) != (FlowNode_DownloadAssets.DownloadAssets) 0)
      {
        Json_LoginBonusTable loginBonus28days = MonoSingleton<GameManager>.Instance.Player.LoginBonus28days;
        if (loginBonus28days != null && loginBonus28days.bonus_units != null && loginBonus28days.bonus_units.Length > 0)
        {
          MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
          foreach (string bonusUnit in loginBonus28days.bonus_units)
            DownloadUtility.DownloadUnit(masterParam.GetUnitParam(bonusUnit), (JobData[]) null);
        }
      }
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.Artifacts) != (FlowNode_DownloadAssets.DownloadAssets) 0)
      {
        List<ArtifactData> artifacts = MonoSingleton<GameManager>.Instance.Player.Artifacts;
        if (artifacts != null && artifacts.Count > 0)
        {
          for (int index = 0; index < artifacts.Count; ++index)
            DownloadUtility.DownloadArtifact(artifacts[index].ArtifactParam);
        }
      }
      if ((this.Download & FlowNode_DownloadAssets.DownloadAssets.OwnConceptCard) != (FlowNode_DownloadAssets.DownloadAssets) 0)
      {
        List<ConceptCardData> conceptCards = MonoSingleton<GameManager>.Instance.Player.ConceptCards;
        if (conceptCards != null && conceptCards.Count > 0)
        {
          for (int index = 0; index < conceptCards.Count; ++index)
            DownloadUtility.DownloadConceptCard(conceptCards[index].Param);
        }
        List<ConceptCardMaterialData> cardExpMaterials = MonoSingleton<GameManager>.Instance.Player.ConceptCardExpMaterials;
        if (cardExpMaterials != null && cardExpMaterials.Count > 0)
        {
          for (int index = 0; index < cardExpMaterials.Count; ++index)
            DownloadUtility.DownloadConceptCard(cardExpMaterials[index].Param);
        }
        List<ConceptCardMaterialData> cardTrustMaterials = MonoSingleton<GameManager>.Instance.Player.ConceptCardTrustMaterials;
        if (cardTrustMaterials != null && cardTrustMaterials.Count > 0)
        {
          for (int index = 0; index < cardTrustMaterials.Count; ++index)
            DownloadUtility.DownloadConceptCard(cardTrustMaterials[index].Param);
        }
      }
      for (int index = 0; index < this.AssetPaths.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.AssetPaths[index]))
          AssetManager.PrepareAssets(this.AssetPaths[index]);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.DownloadUnits.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.DownloadUnits[index]))
        {
          UnitParam unitParam = instanceDirect.GetUnitParam(this.DownloadUnits[index]);
          if (unitParam != null)
            DownloadUtility.DownloadUnit(unitParam, (JobData[]) null);
        }
      }
      for (int index = 0; index < this.DownloadQuests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.DownloadQuests[index]))
        {
          QuestParam quest = instanceDirect.FindQuest(this.DownloadQuests[index]);
          if (quest == null)
            DebugUtility.LogError("Can't download " + this.DownloadQuests[index]);
          else
            DownloadUtility.DownloadQuestBase(quest);
        }
      }
    }

    [Flags]
    public enum DownloadAssets
    {
      Tutorial = 1,
      OwnUnits = 2,
      AllUnits = 4,
      ItemIcons = 8,
      Multiplay = 16, // 0x00000010
      Artifacts = 32, // 0x00000020
      LoginBonus = 64, // 0x00000040
      OwnConceptCard = 128, // 0x00000080
    }
  }
}
