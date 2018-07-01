// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitSlot : MonoBehaviour, IGameParameter
  {
    public Image Frame;
    public Sprite Frame_Leader;
    public Sprite Frame_Main;
    public Sprite Frame_Hero;
    public Sprite Frame_Support;
    public Sprite Frame_Sub;
    [Space(10f)]
    public Image Label;
    public Sprite Label_Leader;
    public Sprite Label_Hero;
    public Sprite Label_Support;
    public Sprite Label_Sub;
    [Space(10f)]
    public GameObject Lock;
    public GameObject Disabled;
    public GameObject Support_Empty;
    [Space(10f)]
    public GameObject OverlayImage;

    public UnitSlot()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null);
      PartySlotData slotData = DataSource.FindDataOfClass<PartySlotData>(((Component) this).get_gameObject(), (PartySlotData) null);
      if (slotData == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Frame, (UnityEngine.Object) null))
      {
        ((Component) this.Frame).get_gameObject().SetActive(true);
        switch (slotData.Index)
        {
          case PartySlotIndex.Main1:
            this.Frame.set_sprite(this.Frame_Leader);
            break;
          case PartySlotIndex.Main2:
          case PartySlotIndex.Main3:
          case PartySlotIndex.Main4:
          case PartySlotIndex.Main5:
            if (slotData.Type == PartySlotType.ForcedHero || slotData.Type == PartySlotType.Npc || slotData.Type == PartySlotType.NpcHero)
            {
              this.Frame.set_sprite(this.Frame_Hero);
              break;
            }
            this.Frame.set_sprite(this.Frame_Main);
            break;
          case PartySlotIndex.Sub1:
          case PartySlotIndex.Sub2:
            this.Frame.set_sprite(this.Frame_Sub);
            break;
          case PartySlotIndex.Support:
            this.Frame.set_sprite(this.Frame_Support);
            break;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Label, (UnityEngine.Object) null))
      {
        switch (slotData.Index)
        {
          case PartySlotIndex.Main1:
            this.Label.set_sprite(this.Label_Leader);
            ((Component) this.Label).get_gameObject().SetActive(true);
            break;
          case PartySlotIndex.Main2:
          case PartySlotIndex.Main3:
          case PartySlotIndex.Main4:
          case PartySlotIndex.Main5:
            if (slotData.Type == PartySlotType.ForcedHero || slotData.Type == PartySlotType.NpcHero)
            {
              this.Label.set_sprite(this.Label_Hero);
              ((Component) this.Label).get_gameObject().SetActive(true);
              break;
            }
            this.Label.set_sprite((Sprite) null);
            ((Component) this.Label).get_gameObject().SetActive(false);
            break;
          case PartySlotIndex.Sub1:
          case PartySlotIndex.Sub2:
            this.Label.set_sprite(this.Label_Sub);
            ((Component) this.Label).get_gameObject().SetActive(true);
            break;
          case PartySlotIndex.Support:
            this.Label.set_sprite(this.Label_Support);
            ((Component) this.Label).get_gameObject().SetActive(true);
            break;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lock, (UnityEngine.Object) null))
      {
        if (slotData.Type == PartySlotType.Free || slotData.Type == PartySlotType.Locked)
          this.Lock.SetActive(false);
        else if (slotData.Type == PartySlotType.Forced || slotData.Type == PartySlotType.ForcedHero || (slotData.Type == PartySlotType.Npc || slotData.Type == PartySlotType.NpcHero))
          this.Lock.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Support_Empty, (UnityEngine.Object) null))
      {
        if (slotData.Index == PartySlotIndex.Support)
          this.Support_Empty.SetActive(dataOfClass1 == null && dataOfClass2 == null);
        else
          this.Support_Empty.SetActive(false);
      }
      if (slotData.Type == PartySlotType.Locked)
      {
        if (slotData.IsSettable)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OverlayImage, (UnityEngine.Object) null))
            this.OverlayImage.SetActive(true);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
            return;
          this.Disabled.SetActive(false);
        }
        else
          this.SetSlotDisabled();
      }
      else if ((slotData.Type == PartySlotType.Forced || slotData.Type == PartySlotType.ForcedHero) && !MonoSingleton<GameManager>.Instance.Player.Units.Any<UnitData>((Func<UnitData, bool>) (unit => unit.UnitID == slotData.UnitName)))
      {
        this.SetSlotDisabled();
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
          return;
        this.Disabled.SetActive(false);
      }
    }

    private void SetSlotDisabled()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
        this.Disabled.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Label, (UnityEngine.Object) null))
        ((Component) this.Label).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lock, (UnityEngine.Object) null))
        this.Lock.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Support_Empty, (UnityEngine.Object) null))
        return;
      this.Support_Empty.SetActive(false);
    }
  }
}
