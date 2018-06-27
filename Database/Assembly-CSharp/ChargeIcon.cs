// Decompiled with JetBrains decompiler
// Type: ChargeIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ChargeIcon : MonoBehaviour
{
  public GameObject ChargeIconPrefab;
  private GameObject mChargeIcon;

  public ChargeIcon()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if (!Object.op_Implicit((Object) this.ChargeIconPrefab))
      return;
    this.mChargeIcon = Object.Instantiate((Object) this.ChargeIconPrefab, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as GameObject;
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null))
      return;
    this.mChargeIcon.get_transform().SetParent(((Component) this).get_transform());
    this.mChargeIcon.SetActive(false);
  }

  public void Open()
  {
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null) || this.mChargeIcon.get_activeSelf())
      return;
    this.mChargeIcon.SetActive(true);
  }

  public void Close()
  {
    if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null) || !this.mChargeIcon.get_activeSelf())
      return;
    this.mChargeIcon.SetActive(false);
  }
}
