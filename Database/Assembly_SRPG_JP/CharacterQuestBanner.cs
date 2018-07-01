// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class CharacterQuestBanner : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private GameObject UnitIcon1;
    [SerializeField]
    private GameObject UnitIcon2;
    [SerializeField]
    private Image LockIcon;
    [SerializeField]
    private Image CompleteIcon;
    [SerializeField]
    private Image NewIcon;
    [SerializeField]
    private GameObject Unlocks;
    private UnityAction UnitIconClickCallback1;
    private UnityAction UnitIconClickCallback2;

    public CharacterQuestBanner()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.UnitIcon1, (Object) null))
      {
        Button componentInChildren = (Button) this.UnitIcon1.GetComponentInChildren<Button>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) componentInChildren.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnUnitIcon1_Click)));
        }
      }
      if (!Object.op_Inequality((Object) this.UnitIcon2, (Object) null))
        return;
      Button componentInChildren1 = (Button) this.UnitIcon2.GetComponentInChildren<Button>();
      if (!Object.op_Inequality((Object) componentInChildren1, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) componentInChildren1.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnUnitIcon2_Click)));
    }

    public void UpdateValue()
    {
      if (!((Component) this).get_gameObject().get_activeInHierarchy())
        return;
      CharacterQuestData dataOfClass = DataSource.FindDataOfClass<CharacterQuestData>(((Component) this).get_gameObject(), (CharacterQuestData) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.questType == CharacterQuestData.EType.Chara)
        this.DataBind(dataOfClass.unitData1, dataOfClass.unitParam1, this.UnitIcon1);
      else if (dataOfClass.questType == CharacterQuestData.EType.Collabo)
      {
        this.DataBind(dataOfClass.unitData1, dataOfClass.unitParam1, this.UnitIcon1);
        this.DataBind(dataOfClass.unitData2, dataOfClass.unitParam2, this.UnitIcon2);
      }
      this.ChangeStatusIcon(dataOfClass.Status);
    }

    private void DataBind(UnitData unitData, UnitParam unitParam, GameObject target)
    {
      if (unitData != null)
      {
        DataSource.Bind<UnitParam>(target, (UnitParam) null);
        DataSource.Bind<UnitData>(target, unitData);
      }
      else
      {
        if (unitParam == null)
          return;
        DataSource.Bind<UnitData>(target, (UnitData) null);
        DataSource.Bind<UnitParam>(target, unitParam);
      }
    }

    private void ChangeStatusIcon(CharacterQuestData.EStatus status)
    {
      switch (status)
      {
        case CharacterQuestData.EStatus.New:
          GameUtility.SetGameObjectActive((Component) this.NewIcon, true);
          GameUtility.SetGameObjectActive((Component) this.LockIcon, false);
          GameUtility.SetGameObjectActive((Component) this.CompleteIcon, false);
          GameUtility.SetGameObjectActive(this.Unlocks, true);
          break;
        case CharacterQuestData.EStatus.Challenged:
          GameUtility.SetGameObjectActive((Component) this.NewIcon, false);
          GameUtility.SetGameObjectActive((Component) this.LockIcon, false);
          GameUtility.SetGameObjectActive((Component) this.CompleteIcon, false);
          GameUtility.SetGameObjectActive(this.Unlocks, true);
          break;
        case CharacterQuestData.EStatus.Lock:
          GameUtility.SetGameObjectActive((Component) this.NewIcon, false);
          GameUtility.SetGameObjectActive((Component) this.LockIcon, true);
          GameUtility.SetGameObjectActive((Component) this.CompleteIcon, false);
          GameUtility.SetGameObjectActive(this.Unlocks, false);
          break;
        case CharacterQuestData.EStatus.Complete:
          GameUtility.SetGameObjectActive((Component) this.NewIcon, false);
          GameUtility.SetGameObjectActive((Component) this.LockIcon, false);
          GameUtility.SetGameObjectActive((Component) this.CompleteIcon, true);
          GameUtility.SetGameObjectActive(this.Unlocks, true);
          break;
      }
    }

    private void OnUnitIcon1_Click()
    {
      this.OnUnitIconClickInternal(this.UnitIcon1);
    }

    private void OnUnitIcon2_Click()
    {
      this.OnUnitIconClickInternal(this.UnitIcon2);
    }

    private void OnUnitIconClickInternal(GameObject go)
    {
      FlowNode_OnUnitIconClick componentInParent = (FlowNode_OnUnitIconClick) ((Component) this).GetComponentInParent<FlowNode_OnUnitIconClick>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      UnitParam dataOfClass1 = DataSource.FindDataOfClass<UnitParam>(go, (UnitParam) null);
      UnitData dataOfClass2 = DataSource.FindDataOfClass<UnitData>(go, (UnitData) null);
      if (dataOfClass2 != null)
      {
        GlobalVars.UnlockUnitID = dataOfClass2.UnitParam.iname;
        componentInParent.Click();
      }
      else if (dataOfClass1 != null)
      {
        GlobalVars.UnlockUnitID = dataOfClass1.iname;
        componentInParent.Click();
      }
      else
        DebugUtility.LogWarning("UnitDataがバインドされていません");
    }
  }
}
