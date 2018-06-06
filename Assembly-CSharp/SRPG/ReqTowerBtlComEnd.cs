// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerBtlComEnd
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqTowerBtlComEnd : WebAPI
  {
    public ReqTowerBtlComEnd(long btlid, Unit[] Player, Unit[] Enemy, int actCount, int round, byte floor, BtlResultTypes result, RandDeckResult[] deck, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null, string maxdata = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/btl/end";
      stringBuilder.Length = 0;
      this.SetValue(ref stringBuilder, "\"btlid\":", btlid);
      stringBuilder.Append("\"btlendparam\":{");
      if (Player != null)
      {
        stringBuilder.Append("\"pdeck\":[");
        for (int index = 0; index < Player.Length; ++index)
        {
          Unit unit = Player[index];
          if (unit.Side == EUnitSide.Player && unit.UnitData.UniqueID != 0L)
          {
            stringBuilder.Append("{");
            this.SetValue(ref stringBuilder, "\"iid\":", unit.UnitData.UniqueID);
            this.SetValue(ref stringBuilder, "\"iname\":\"", unit.UnitData.UnitParam.iname, "\",");
            int num = Mathf.Max(unit.CalcTowerDamege() - MonoSingleton<GameManager>.Instance.FindTowerFloor(SceneBattle.Instance.Battle.QuestID).CalcHelaNum((int) unit.MaximumStatus.param.hp), 0);
            this.SetValue(ref stringBuilder, "\"damage\":", (long) num);
            this.SetValue(ref stringBuilder, "\"is_died\":", !unit.IsDeadCondition() ? 0L : 1L, string.Empty);
            stringBuilder.Append("},");
          }
        }
        --stringBuilder.Length;
        stringBuilder.Append("],");
      }
      stringBuilder.Append("\"status\":\"");
      switch (result)
      {
        case BtlResultTypes.Win:
          stringBuilder.Append("win");
          break;
        case BtlResultTypes.Lose:
          stringBuilder.Append("lose");
          break;
        case BtlResultTypes.Retire:
          stringBuilder.Append("retire");
          break;
        case BtlResultTypes.Cancel:
          stringBuilder.Append("cancel");
          break;
      }
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"turn\":");
      stringBuilder.Append(actCount);
      stringBuilder.Append(",\"round\":");
      stringBuilder.Append(round);
      stringBuilder.Append(",\"floor\":");
      stringBuilder.Append(floor);
      if (result == BtlResultTypes.Lose)
      {
        stringBuilder.Append(",\"edeck\":[");
        List<Unit> unitList = new List<Unit>((IEnumerable<Unit>) Enemy);
        unitList.RemoveAll((Predicate<Unit>) (x => x.Side != EUnitSide.Enemy));
        if (MonoSingleton<GameManager>.Instance.TowerResuponse.edeck != null)
        {
          List<TowerResuponse.EnemyUnit> edeck = MonoSingleton<GameManager>.Instance.TowerResuponse.edeck;
          int index1 = 0;
          for (int index2 = 0; index2 < edeck.Count; ++index2)
          {
            if (edeck[index2].hp == 0)
            {
              stringBuilder.Append("{");
              this.SetValue(ref stringBuilder, "\"eid\":\"", (long) index2, "\",");
              this.SetValue(ref stringBuilder, "\"iname\":\"", edeck[index2].iname, "\",");
              this.SetValue(ref stringBuilder, "\"hp\":", 0L);
              this.SetValue(ref stringBuilder, "\"jewel\":", 0L, string.Empty);
              stringBuilder.Append("},");
            }
            else
            {
              Unit unit = unitList[index1];
              ++index1;
              if (unit.Side == EUnitSide.Enemy)
              {
                stringBuilder.Append("{");
                this.SetValue(ref stringBuilder, "\"eid\":\"", (long) index2, "\",");
                this.SetValue(ref stringBuilder, "\"iname\":\"", unit.UnitParam.iname, "\",");
                this.SetValue(ref stringBuilder, "\"hp\":", !unit.IsDead ? (long) (int) unit.CurrentStatus.param.hp : 0L);
                this.SetValue(ref stringBuilder, "\"jewel\":", (long) (int) unit.CurrentStatus.param.mp, string.Empty);
                stringBuilder.Append("},");
              }
            }
          }
        }
        else
        {
          for (int index = 0; index < unitList.Count; ++index)
          {
            Unit unit = unitList[index];
            if (unit.Side == EUnitSide.Enemy)
            {
              stringBuilder.Append("{");
              this.SetValue(ref stringBuilder, "\"eid\":\"", (long) index, "\",");
              this.SetValue(ref stringBuilder, "\"iname\":\"", unit.UnitParam.iname, "\",");
              this.SetValue(ref stringBuilder, "\"hp\":", !unit.IsDead ? (long) (int) unit.CurrentStatus.param.hp : 0L);
              this.SetValue(ref stringBuilder, "\"jewel\":", (long) (int) unit.CurrentStatus.param.mp, string.Empty);
              stringBuilder.Append("},");
            }
          }
        }
        --stringBuilder.Length;
        stringBuilder.Append("]");
      }
      SupportData supportData = GlobalVars.SelectedSupport.Get();
      if (GlobalVars.SelectedFriendID != null && supportData != null)
      {
        stringBuilder.Append(",\"help\":{\"fuid\":\"");
        stringBuilder.Append(GlobalVars.SelectedFriendID);
        stringBuilder.Append("\",\"cost\":");
        stringBuilder.Append(supportData.Cost);
        stringBuilder.Append("}");
      }
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      if (!string.IsNullOrEmpty(maxdata))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(maxdata);
      }
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public void SetValue(ref StringBuilder sb, string name, long value)
    {
      sb.Append(name);
      sb.Append(value);
      sb.Append(",");
    }

    public void SetValue(ref StringBuilder sb, string name, string value)
    {
      sb.Append(name);
      sb.Append(value);
      sb.Append(",");
    }

    public void SetValue(ref StringBuilder sb, string name, long value, string end)
    {
      sb.Append(name);
      sb.Append(value);
      sb.Append(end);
    }

    public void SetValue(ref StringBuilder sb, string name, string value, string end)
    {
      sb.Append(name);
      sb.Append(value);
      sb.Append(end);
    }
  }
}
