// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PartyUnitSlot : MonoBehaviour
  {
    public static PartyUnitSlot Active;
    [HelpBox("パーティ編成画面のユニットを割り当てるスロット。 選択状態あわせて StateAnimator に指定された Animator の bool 値を切り替えます。イベントにSelect()を登録してください。")]
    public Animator StateAnimator;
    public string AnimatorBoolName;
    public GameObject[] HideIfEmpty;
    public int Index;
    public bool ToggleButtonIfEmpty;
    private Button mButton;

    public PartyUnitSlot()
    {
      base.\u002Ector();
    }

    private void ToggleButton()
    {
      if (!this.ToggleButtonIfEmpty || Object.op_Equality((Object) this.mButton, (Object) null))
        return;
      ((Selectable) this.mButton).set_interactable(DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null) != null);
    }

    public void Select()
    {
      PartyUnitSlot.Active = this;
    }

    private void Awake()
    {
      if (this.ToggleButtonIfEmpty)
        this.mButton = (Button) ((Component) this).get_gameObject().GetComponent<Button>();
      this.ToggleButton();
    }

    public void Update()
    {
      if (Object.op_Inequality((Object) this.StateAnimator, (Object) null) && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, Object.op_Equality((Object) PartyUnitSlot.Active, (Object) this));
      bool flag = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null) != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
      this.ToggleButton();
    }
  }
}
