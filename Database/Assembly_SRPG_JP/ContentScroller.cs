// Decompiled with JetBrains decompiler
// Type: SRPG.ContentScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ContentScroller : SRPG_ScrollRect
  {
    private ContentController mContentController;

    public ContentController contentController
    {
      get
      {
        if (Object.op_Equality((Object) this.mContentController, (Object) null) && Object.op_Inequality((Object) this.get_content(), (Object) null))
          this.mContentController = (ContentController) ((Component) this.get_content()).GetComponent<ContentController>();
        return this.mContentController;
      }
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      if (Object.op_Equality((Object) this.contentController, (Object) null))
        ;
    }
  }
}
