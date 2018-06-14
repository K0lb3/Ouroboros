// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitChg
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitChg : MonoBehaviour
  {
    private const int SUB_UNIT_MAX = 2;
    public BattleUnitChg.SelectEvent OnSelectUnit;
    public RectTransform ListParent;
    public ListItemEvents UnitTemplate;
    public ListItemEvents EmptyTemplate;
    private List<Unit> mSubUnitLists;
    private List<ListItemEvents> mUnitEvents;

    public BattleUnitChg()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
      {
        ((Component) this.UnitTemplate).get_gameObject().SetActive(false);
        if (Object.op_Equality((Object) this.ListParent, (Object) null))
          this.ListParent = ((Component) this.UnitTemplate).get_transform().get_parent() as RectTransform;
      }
      if (Object.op_Inequality((Object) this.EmptyTemplate, (Object) null))
        ((Component) this.EmptyTemplate).get_gameObject().SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mUnitEvents);
      this.mUnitEvents.Clear();
      if (Object.op_Equality((Object) this.UnitTemplate, (Object) null) || Object.op_Equality((Object) this.EmptyTemplate, (Object) null) || Object.op_Equality((Object) this.ListParent, (Object) null))
        return;
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if (Object.op_Implicit((Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return;
      this.mSubUnitLists.Clear();
      using (List<Unit>.Enumerator enumerator = battleCore.Player.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit current = enumerator.Current;
          if (!battleCore.StartingMembers.Contains(current))
            this.mSubUnitLists.Add(current);
        }
      }
      for (int index1 = 0; index1 < 2; ++index1)
      {
        if (index1 >= this.mSubUnitLists.Count)
        {
          ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.EmptyTemplate);
          ((Component) listItemEvents).get_transform().SetParent((Transform) this.ListParent, false);
          this.mUnitEvents.Add(listItemEvents);
          ((Component) listItemEvents).get_gameObject().SetActive(true);
        }
        else
        {
          Unit mSubUnitList = this.mSubUnitLists[index1];
          if (mSubUnitList != null)
          {
            ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.UnitTemplate);
            DataSource.Bind<Unit>(((Component) listItemEvents).get_gameObject(), mSubUnitList);
            ((Component) listItemEvents).get_transform().SetParent((Transform) this.ListParent, false);
            this.mUnitEvents.Add(listItemEvents);
            ((Component) listItemEvents).get_gameObject().SetActive(true);
            bool flag = !mSubUnitList.IsDead && mSubUnitList.IsEntry && mSubUnitList.IsSub;
            Selectable[] componentsInChildren = (Selectable[]) ((Component) listItemEvents).get_gameObject().GetComponentsInChildren<Selectable>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
                componentsInChildren[index2].set_interactable(flag);
            }
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
            {
              if (this.OnSelectUnit == null)
                return;
              Unit dataOfClass = DataSource.FindDataOfClass<Unit>(go, (Unit) null);
              if (dataOfClass == null)
                return;
              this.OnSelectUnit(dataOfClass);
            });
          }
        }
      }
    }

    public delegate void SelectEvent(Unit unit);
  }
}
