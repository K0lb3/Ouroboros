// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemBase
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AppealItemBase : MonoBehaviour
  {
    private Sprite mAppealSprite;
    public Image AppealObject;

    public AppealItemBase()
    {
      base.\u002Ector();
    }

    public Sprite AppealSprite
    {
      get
      {
        return this.mAppealSprite;
      }
      set
      {
        this.mAppealSprite = value;
      }
    }

    protected virtual void Awake()
    {
      if (!Object.op_Inequality((Object) this.AppealObject, (Object) null))
        return;
      ((Component) this.AppealObject).get_gameObject().SetActive(false);
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void Destroy()
    {
    }

    protected virtual void Refresh()
    {
      if (Object.op_Equality((Object) this.mAppealSprite, (Object) null))
      {
        if (!Object.op_Inequality((Object) this.AppealObject, (Object) null))
          return;
        ((Component) this.AppealObject).get_gameObject().SetActive(false);
      }
      else
      {
        ((Component) this.AppealObject).get_gameObject().SetActive(true);
        this.AppealObject.set_sprite(this.mAppealSprite);
      }
    }
  }
}
