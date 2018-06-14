// Decompiled with JetBrains decompiler
// Type: GpsGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

public class GpsGiftIcon : MonoBehaviour
{
  private Image m_Image;

  public GpsGiftIcon()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.m_Image = (Image) ((Component) this).get_gameObject().GetComponent<Image>();
    if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
      return;
    ((Behaviour) this.m_Image).set_enabled(false);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_Image, (Object) null))
      return;
    GameManager instance = MonoSingleton<GameManager>.Instance;
    if (!Object.op_Inequality((Object) instance, (Object) null) || instance.Player == null)
      return;
    if (instance.Player.ValidGpsGift)
      ((Behaviour) this.m_Image).set_enabled(true);
    else
      ((Behaviour) this.m_Image).set_enabled(false);
  }
}
