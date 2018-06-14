// Decompiled with JetBrains decompiler
// Type: IconLoader
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class IconLoader : MonoBehaviour
{
  private string mPath;
  private LoadRequest mResourceReq;
  private Texture mIcon;

  public IconLoader()
  {
    base.\u002Ector();
  }

  public string ResourcePath
  {
    set
    {
      if (this.mPath == value && Object.op_Equality((Object) this.IconTexture, (Object) this.mIcon))
        return;
      this.mPath = value;
      this.IconTexture = (Texture) null;
      if (string.IsNullOrEmpty(this.mPath))
      {
        this.mResourceReq = (LoadRequest) null;
        ((Behaviour) this).set_enabled(false);
      }
      else
      {
        this.mResourceReq = GameUtility.LoadResourceAsyncChecked<Texture2D>(this.mPath);
        ((Behaviour) this).set_enabled(true);
        if (!((Component) this).get_gameObject().get_activeInHierarchy())
          return;
        this.Update();
      }
    }
  }

  private void Update()
  {
    if (this.mResourceReq == null)
    {
      ((Behaviour) this).set_enabled(false);
    }
    else
    {
      if (!this.mResourceReq.isDone)
        return;
      this.IconTexture = !Object.op_Inequality(this.mResourceReq.asset, (Object) null) ? (Texture) Texture2D.get_blackTexture() : (Texture) (this.mResourceReq.asset as Texture2D);
      this.mResourceReq = (LoadRequest) null;
      ((Behaviour) this).set_enabled(false);
    }
  }

  private Texture IconTexture
  {
    set
    {
      this.mIcon = value;
      RawImage component = (RawImage) ((Component) this).GetComponent<RawImage>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.set_texture(value);
    }
    get
    {
      RawImage component = (RawImage) ((Component) this).GetComponent<RawImage>();
      if (Object.op_Inequality((Object) component, (Object) null))
        return component.get_texture();
      return (Texture) null;
    }
  }
}
