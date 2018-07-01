// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBtnBaloon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopBtnBaloon : MonoBehaviour
  {
    [SerializeField]
    private Image BaloonChar;
    [SerializeField]
    private Image BaloonTextLeft;
    [SerializeField]
    private Image BaloonTextRight;
    [SerializeField]
    private string ReverseObjectID;
    [HideInInspector]
    public Sprite CurrentTextLeftSprite;
    [HideInInspector]
    public Sprite CurrentTextRightSprite;
    [HideInInspector]
    public Sprite CurrentCharSprite;
    private GameObject mBaloonChar;
    private GameObject mBaloonTextLeft;
    private GameObject mBaloonTextRight;

    public LimitedShopBtnBaloon()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.BaloonChar, (Object) null))
        ((Component) this.BaloonChar).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.BaloonTextLeft, (Object) null))
        ((Component) this.BaloonTextLeft).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.BaloonTextRight, (Object) null))
        ((Component) this.BaloonTextRight).get_gameObject().SetActive(false);
      this.RefreshBaloonImage();
      this.UpdatePosition();
    }

    private void RefreshBaloonImage()
    {
      if (Object.op_Inequality((Object) this.BaloonChar, (Object) null))
      {
        this.BaloonChar.set_sprite(!Object.op_Inequality((Object) this.CurrentCharSprite, (Object) null) ? this.BaloonChar.get_sprite() : this.CurrentCharSprite);
        ((Component) this.BaloonChar).get_gameObject().SetActive(true);
      }
      if (Object.op_Inequality((Object) this.BaloonTextLeft, (Object) null))
      {
        this.BaloonTextLeft.set_sprite(!Object.op_Inequality((Object) this.CurrentTextLeftSprite, (Object) null) ? this.BaloonTextLeft.get_sprite() : this.CurrentTextLeftSprite);
        ((Component) this.BaloonTextLeft).get_gameObject().SetActive(true);
      }
      if (!Object.op_Inequality((Object) this.BaloonTextRight, (Object) null))
        return;
      this.BaloonTextRight.set_sprite(!Object.op_Inequality((Object) this.CurrentTextRightSprite, (Object) null) ? this.BaloonTextRight.get_sprite() : this.CurrentTextRightSprite);
      ((Component) this.BaloonTextRight).get_gameObject().SetActive(true);
    }

    private void UpdatePosition()
    {
      if (string.IsNullOrEmpty(this.ReverseObjectID))
        return;
      GameObject gameObject = GameObjectID.FindGameObject(this.ReverseObjectID);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      UIProjector component = (UIProjector) gameObject.GetComponent<UIProjector>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.ReStart();
    }
  }
}
