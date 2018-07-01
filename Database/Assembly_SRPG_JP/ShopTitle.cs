// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ShopTitle : MonoBehaviour
  {
    public ImageArray IamgeArray;

    public ShopTitle()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
          this.IamgeArray.ImageIndex = 0;
          break;
        case EShopType.Tabi:
          this.IamgeArray.ImageIndex = 1;
          break;
        case EShopType.Kimagure:
          this.IamgeArray.ImageIndex = 2;
          break;
        case EShopType.Guerrilla:
          this.IamgeArray.ImageIndex = 3;
          break;
        default:
          ((Component) this).get_gameObject().SetActive(false);
          break;
      }
    }
  }
}
