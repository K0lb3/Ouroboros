// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_ItemMap
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayVersus_ItemMap : MonoBehaviour
  {
    public Text Name;
    public Text Desc;
    public Image Thumnail;

    public MultiPlayVersus_ItemMap()
    {
      base.\u002Ector();
    }

    public void UpdateParam(QuestParam param)
    {
      if (Object.op_Inequality((Object) this.Name, (Object) null))
        this.Name.set_text(param.name);
      if (Object.op_Inequality((Object) this.Desc, (Object) null))
        this.Desc.set_text(param.expr);
      if (Object.op_Implicit((Object) this.Thumnail))
        ;
    }
  }
}
