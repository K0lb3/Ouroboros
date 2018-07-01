// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCpuInfo
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
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "Selected", FlowNode.PinTypes.Output, 200)]
  public class VersusCpuInfo : MonoBehaviour, IFlowInterface
  {
    public ListItemEvents CpuPlayerTemplate;
    public GameObject CpuList;
    public GameObject MapInfo;
    public GameObject PartyInfo;
    public Color[] RankColor;
    private List<ListItemEvents> mVersusMember;

    public VersusCpuInfo()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.StartCoroutine(this.RefreshEnemy());
    }

    private void Awake()
    {
      GlobalVars.SelectedPartyIndex.Set(7);
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.CpuPlayerTemplate, (Object) null))
        ((Component) this.CpuPlayerTemplate).get_gameObject().SetActive(false);
      this.RefreshData();
    }

    private void Update()
    {
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Inequality((Object) this.MapInfo, (Object) null))
      {
        QuestParam quest = instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
        {
          DataSource.Bind<QuestParam>(this.MapInfo, quest);
          GameParameter.UpdateAll(this.MapInfo);
        }
      }
      if (!Object.op_Inequality((Object) this.PartyInfo, (Object) null))
        return;
      GlobalVars.SelectedPartyIndex.Set(7);
      PartyData party = instance.Player.Partys[(int) GlobalVars.SelectedPartyIndex];
      if (party == null)
        return;
      DataSource.Bind<PartyData>(this.PartyInfo, party);
      GameParameter.UpdateAll(this.PartyInfo);
    }

    [DebuggerHidden]
    private IEnumerator RefreshEnemy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusCpuInfo.\u003CRefreshEnemy\u003Ec__Iterator17B()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      VersusCpuInfo.\u003CDownloadUnitImage\u003Ec__Iterator17C imageCIterator17C = new VersusCpuInfo.\u003CDownloadUnitImage\u003Ec__Iterator17C();
      return (IEnumerator) imageCIterator17C;
    }

    private void OnSelect(GameObject go)
    {
      VersusCpuData dataOfClass = DataSource.FindDataOfClass<VersusCpuData>(go, (VersusCpuData) null);
      if (dataOfClass == null)
        return;
      MonoSingleton<GameManager>.Instance.IsVSCpuBattle = true;
      GlobalVars.VersusCpu.Set(dataOfClass);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    private void OnOpenDetail(GameObject _go)
    {
      UnitData unit = DataSource.FindDataOfClass<VersusCpuData>(_go, (VersusCpuData) null).Units[0];
      if (unit == null)
        return;
      unit.ShowTooltip(_go, false, (UnitJobDropdown.ParentObjectEvent) null);
    }
  }
}
