// Decompiled with JetBrains decompiler
// Type: SRPG.EventBanner2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventBanner2 : MonoBehaviour
  {
    private Image mTarget;
    private LoadRequest mLoadRequest;

    public EventBanner2()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mTarget = (Image) ((Component) this).GetComponent<Image>();
      BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(((Component) this).get_gameObject(), (BannerParam) null);
      if (dataOfClass == null)
        return;
      this.mLoadRequest = AssetManager.LoadAsync<GachaTabSprites>(dataOfClass.banner);
    }

    private void Update()
    {
      if (this.mLoadRequest == null || Object.op_Equality((Object) this.mTarget, (Object) null))
      {
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        if (!this.mLoadRequest.isDone)
          return;
        BannerParam dataOfClass = DataSource.FindDataOfClass<BannerParam>(((Component) this).get_gameObject(), (BannerParam) null);
        if (dataOfClass == null)
          return;
        GachaTabSprites asset = this.mLoadRequest.asset as GachaTabSprites;
        if (Object.op_Inequality((Object) asset, (Object) null) && asset.Sprites != null && asset.Sprites.Length > 0)
        {
          Sprite[] sprites = asset.Sprites;
          for (int index = 0; index < sprites.Length; ++index)
          {
            if (Object.op_Inequality((Object) sprites[index], (Object) null) && ((Object) sprites[index]).get_name() == dataOfClass.banr_sprite)
              this.mTarget.set_sprite(sprites[index]);
          }
        }
        ((Behaviour) this).set_enabled(false);
      }
    }
  }
}
