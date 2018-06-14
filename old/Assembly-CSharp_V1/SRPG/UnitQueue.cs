// Decompiled with JetBrains decompiler
// Type: SRPG.UnitQueue
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitQueue : MonoBehaviour
  {
    public static UnitQueue Instance;
    public GameObject[] Items;
    public UnitQueue.Layer[] Units;
    public Button[] UnitButtons;
    public GameObject LastUnit;
    private Unit[] mCurrentUnits;

    public UnitQueue()
    {
      base.\u002Ector();
    }

    public void Refresh(Unit unit)
    {
      for (int index1 = 0; index1 < this.mCurrentUnits.Length; ++index1)
      {
        if (this.mCurrentUnits[index1] == unit)
        {
          if (index1 < this.Items.Length && Object.op_Inequality((Object) this.Items[index1], (Object) null))
            GameParameter.UpdateAll(this.Items[index1]);
          if (index1 < this.Units.Length && this.Units[index1].Layers != null)
          {
            for (int index2 = 0; index2 < this.Units[index1].Layers.Length; ++index2)
            {
              if (Object.op_Inequality((Object) this.Units[index1].Layers[index2], (Object) null))
                GameParameter.UpdateAll(this.Units[index1].Layers[index2]);
            }
          }
        }
      }
    }

    private void Start()
    {
      this.mCurrentUnits = new Unit[Mathf.Max(this.Items.Length, this.Units.Length)];
    }

    public void Refresh(int offset = 0)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.Order.Count == 0)
        return;
      int num = Mathf.Max(this.Items.Length, this.Units.Length);
      for (int index1 = 0; index1 < num; ++index1)
      {
        Unit unit = battle.Order[index1 % battle.Order.Count].Unit;
        if (index1 < this.mCurrentUnits.Length)
          this.mCurrentUnits[index1] = unit;
        if (index1 < this.Items.Length && Object.op_Inequality((Object) this.Items[index1], (Object) null))
        {
          DataSource.Bind<Unit>(this.Items[index1], unit);
          GameParameter.UpdateAll(this.Items[index1]);
        }
        if (index1 < this.Units.Length)
        {
          for (int index2 = 0; index2 < this.Units[index1].Layers.Length; ++index2)
          {
            if (!Object.op_Equality((Object) this.Units[index1].Layers[index2], (Object) null))
            {
              DataSource.Bind<Unit>(this.Units[index1].Layers[index2], unit);
              GameParameter.UpdateAll(this.Units[index1].Layers[index2]);
            }
          }
        }
      }
      if (!Object.op_Inequality((Object) this.LastUnit, (Object) null))
        return;
      DataSource.Bind<Unit>(this.LastUnit, battle.Order[0].Unit);
      GameParameter.UpdateAll(this.LastUnit);
    }

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) UnitQueue.Instance, (Object) null))
        return;
      UnitQueue.Instance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) UnitQueue.Instance, (Object) this))
        return;
      UnitQueue.Instance = (UnitQueue) null;
    }

    [Serializable]
    public struct Layer
    {
      public GameObject[] Layers;
    }
  }
}
