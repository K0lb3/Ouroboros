// Decompiled with JetBrains decompiler
// Type: SRPG.ShopText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopText : MonoBehaviour
  {
    public string Normal;
    public string Tabi;
    public string Kimagure;
    public string Monozuki;
    public string Tour;
    public string Arena;
    public string Multi;
    public string AwakePiece;
    public string Artifact;
    public string Limited;
    private string mTextID;

    public ShopText()
    {
      base.\u002Ector();
    }

    private void LateUpdate()
    {
      if (this.mTextID != null)
        return;
      Text component = (Text) ((Component) this).GetComponent<Text>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
          this.mTextID = this.Normal;
          break;
        case EShopType.Tabi:
          this.mTextID = this.Tabi;
          break;
        case EShopType.Kimagure:
          this.mTextID = this.Kimagure;
          break;
        case EShopType.Monozuki:
          this.mTextID = this.Monozuki;
          break;
        case EShopType.Tour:
          this.mTextID = this.Tour;
          break;
        case EShopType.Arena:
          this.mTextID = this.Arena;
          break;
        case EShopType.Multi:
          this.mTextID = this.Multi;
          break;
        case EShopType.AwakePiece:
          this.mTextID = this.AwakePiece;
          break;
        case EShopType.Artifact:
          this.mTextID = this.Artifact;
          break;
        case EShopType.Limited:
          this.mTextID = this.Limited;
          break;
      }
      if (string.IsNullOrEmpty(this.mTextID))
        this.mTextID = string.Empty;
      else
        component.set_text(LocalizedText.Get(this.mTextID));
    }
  }
}
