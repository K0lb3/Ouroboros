// Decompiled with JetBrains decompiler
// Type: EquipmentSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class EquipmentSet : ScriptableObject
{
  public EquipmentSet.EquipmentType Type;
  public bool PrimaryHidden;
  public bool PrimaryForceOverride;
  public GameObject PrimaryHand;
  public List<GameObject> PrimaryHandChangeLists;
  public bool SecondaryHidden;
  public bool SecondaryForceOverride;
  public GameObject SecondaryHand;
  public List<GameObject> SecondaryHandChangeLists;
  public List<GameObject> OptionEquipmentLists;

  public EquipmentSet()
  {
    base.\u002Ector();
  }

  public enum EquipmentType
  {
    Melee,
    Bow,
    Gun,
  }
}
