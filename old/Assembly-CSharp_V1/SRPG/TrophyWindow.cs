// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1000, "デイリー", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "レコード", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  public class TrophyWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_DAILY = 1000;
    public const int PIN_RECORD = 1001;
    public Toggle[] TrophyTab;

    public TrophyWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      foreach (TrophyParam trophy in MonoSingleton<GameManager>.Instance.Trophies)
        player.GetTrophyCounter(trophy);
      this.DailyCheck();
      this.RecordCheck();
      if (GlobalVars.SelectedTrophyType == TrophyType.Daily)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }

    private bool DailyCheck()
    {
      TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
      for (int index1 = 0; index1 < trophyStates.Length; ++index1)
      {
        if (trophyStates[index1].Param.Days == 1 && trophyStates[index1].Param.IsShowBadge(trophyStates[index1]))
        {
          TrophyParam trophyParam = trophyStates[index1].Param;
          int hour = TimeManager.ServerTime.Hour;
          for (int index2 = trophyParam.Objectives.Length - 1; index2 >= 0; --index2)
          {
            if (trophyParam.Objectives[index2].type != TrophyConditionTypes.stamina)
              return true;
            int num1 = int.Parse(trophyParam.Objectives[index2].sval.Substring(0, 2));
            int num2 = int.Parse(trophyParam.Objectives[index2].sval.Substring(3, 2));
            if (num1 <= hour && hour < num2)
              return true;
          }
        }
      }
      return false;
    }

    private bool RecordCheck()
    {
      TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
      bool[] flagArray = new bool[4];
      for (int index = 0; index < trophyStates.Length; ++index)
      {
        if (trophyStates[index].Param.Days == 0)
        {
          int tabIndex = (int) this.TrophyCategorysToTabIndex(trophyStates[index].Param.Category);
          if (!flagArray[tabIndex] && trophyStates[index].Param.IsShowBadge(trophyStates[index]))
            flagArray[tabIndex] = true;
        }
      }
      this.SetToggleIsOn(0);
      for (int index = 0; index < 4; ++index)
      {
        if (flagArray[index])
        {
          this.SetToggleIsOn(index);
          return true;
        }
      }
      return false;
    }

    private TrophyWindow.TabIndex TrophyCategorysToTabIndex(TrophyCategorys category)
    {
      switch (category)
      {
        case TrophyCategorys.Story:
          return TrophyWindow.TabIndex.Story;
        case TrophyCategorys.Event:
          return TrophyWindow.TabIndex.Event;
        case TrophyCategorys.Training:
          return TrophyWindow.TabIndex.Training;
        default:
          return TrophyWindow.TabIndex.Other;
      }
    }

    private void SetToggleIsOn(int index)
    {
      for (int index1 = 0; index1 < this.TrophyTab.Length; ++index1)
      {
        if (Object.op_Implicit((Object) this.TrophyTab[index1]))
          this.TrophyTab[index1].set_isOn(index1 == index);
      }
    }

    private enum TabIndex
    {
      Story,
      Event,
      Training,
      Other,
      MAX,
    }
  }
}
