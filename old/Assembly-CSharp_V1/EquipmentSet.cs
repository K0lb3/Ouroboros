// Decompiled with JetBrains decompiler
// Type: EquipmentSet
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class EquipmentSet : ScriptableObject
{
  public EquipmentSet.EquipmentType Type;
  public bool PrimaryHidden;
  public bool PrimaryForceOverride;
  public GameObject PrimaryHand;
  public bool SecondaryHidden;
  public bool SecondaryForceOverride;
  public GameObject SecondaryHand;

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
