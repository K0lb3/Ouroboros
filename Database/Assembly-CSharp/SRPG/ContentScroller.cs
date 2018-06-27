// Decompiled with JetBrains decompiler
// Type: SRPG.ContentScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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

    protected virtual void LateUpdate()
    {
      base.LateUpdate();
      if (Object.op_Equality((Object) this.contentController, (Object) null))
        ;
    }
  }
}
