// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEffectRoutine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardEffectRoutine
  {
    private const string mLeveUpPrefabPath = "UI/ConceptCardLevelup";
    private const string mAwakePrefabPath = "UI/ConceptCardLimitUp";
    private const string mTrustMasterPrefabPath = "UI/ConceptCardTrustMaster";
    private const string mTrustMasterRewardPrefabPath = "UI/ConceptCardTrustMasterReward";
    private const string mGroupSkillPowerUpPrefabPath = "UI/ConceptCardSkillPowerUp";
    private const string mGroupMaxPowerUpPrefabPath = "UI/ConceptCardMaxPowerUp";
    private const string mGroupMaxPowerResultPrefabPath = "UI/ConceptCardMaxPowerUpGroupSkillList";

    [DebuggerHidden]
    public IEnumerator PlayLevelup(Canvas overlayCanvas)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayLevelup\u003Ec__IteratorFB()
      {
        overlayCanvas = overlayCanvas,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayAwake(Canvas overlayCanvas)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayAwake\u003Ec__IteratorFC()
      {
        overlayCanvas = overlayCanvas,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayTrustMaster(Canvas overlayCanvas, ConceptCardData data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayTrustMaster\u003Ec__IteratorFD()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u0024\u003Edata = data,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayTrustMasterReward(Canvas overlayCanvas, ConceptCardData data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayTrustMasterReward\u003Ec__IteratorFE()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u0024\u003Edata = data,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayGroupSkilPowerUp(Canvas overlayCanvas, ConceptCardData data, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayGroupSkilPowerUp\u003Ec__IteratorFF()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        altData = altData,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u0024\u003Edata = data,
        \u003C\u0024\u003EaltData = altData,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayGroupSkilMaxPowerUp(Canvas overlayCanvas, ConceptCardData data, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayGroupSkilMaxPowerUp\u003Ec__Iterator100()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        altData = altData,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u0024\u003Edata = data,
        \u003C\u0024\u003EaltData = altData,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator EffectRoutine(Canvas overlayCanvas, string path, ConceptCardEffectRoutine.EffectType type = ConceptCardEffectRoutine.EffectType.DEFAULT, ConceptCardData data = null, ConceptCardEffectRoutine.EffectAltData altData = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CEffectRoutine\u003Ec__Iterator101()
      {
        path = path,
        overlayCanvas = overlayCanvas,
        type = type,
        data = data,
        altData = altData,
        \u003C\u0024\u003Epath = path,
        \u003C\u0024\u003EoverlayCanvas = overlayCanvas,
        \u003C\u0024\u003Etype = type,
        \u003C\u0024\u003Edata = data,
        \u003C\u0024\u003EaltData = altData
      };
    }

    public enum EffectType
    {
      DEFAULT,
      TRUST_MASTER,
      TRUST_MASTER_REWARD,
      GROUP_SKILL_POWER_UP,
      GROUP_SKILL_MAX_POWER_UP,
    }

    public class EffectAltData
    {
      public int prevAwakeCount;
      public int prevLevel;
    }
  }
}
