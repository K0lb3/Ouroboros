// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GenericSlot : MonoBehaviour
  {
    public GenericSlot.SelectEvent OnSelect;
    [Space(10f)]
    public Graphic MainGraphic;
    public Image BGImage;
    public Sprite EmptyBG;
    public Sprite NonEmptyBG;
    [Space(10f)]
    public SRPG_Button SelectButton;

    public GenericSlot()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !((Selectable) button).get_interactable())
        return;
      this.OnSelect(this, ((Selectable) button).IsInteractable());
    }

    public void SetMainColor(Color color)
    {
      if (!Object.op_Inequality((Object) this.MainGraphic, (Object) null))
        return;
      this.MainGraphic.set_color(color);
    }

    public void SetLocked(bool locked)
    {
      GenericSlotFlags[] componentsInChildren = (GenericSlotFlags[]) ((Component) this).GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Locked) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).get_gameObject().SetActive(locked);
      }
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      ((Selectable) this.SelectButton).set_interactable(!locked);
    }

    public void SetSlotData<T>(T data)
    {
      DataSource.Bind<T>(((Component) this).get_gameObject(), data);
      bool flag = (object) data == null;
      if (Object.op_Inequality((Object) this.BGImage, (Object) null))
        this.BGImage.set_sprite(!flag ? this.NonEmptyBG : this.EmptyBG);
      GenericSlotFlags[] componentsInChildren = (GenericSlotFlags[]) ((Component) this).GetComponentsInChildren<GenericSlotFlags>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.Empty) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).get_gameObject().SetActive(flag);
        if ((componentsInChildren[index].Flags & GenericSlotFlags.VisibleFlags.NonEmpty) != (GenericSlotFlags.VisibleFlags) 0)
          ((Component) componentsInChildren[index]).get_gameObject().SetActive(!flag);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public delegate void SelectEvent(GenericSlot slot, bool interactable);
  }
}
