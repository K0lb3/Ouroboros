// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class NewBadge : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObject;
    [SerializeField]
    public NewBadgeType SelectBadgeType;

    public NewBadge()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.BadgeObject, (Object) null))
        this.BadgeObject = ((Component) this).get_gameObject();
      NewBadgeParam dataOfClass = DataSource.FindDataOfClass<NewBadgeParam>(this.BadgeObject, (NewBadgeParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.use_newflag)
      {
        ((Component) this).get_gameObject().SetActive(dataOfClass.is_new);
      }
      else
      {
        bool active = ((Component) this).get_gameObject().GetActive();
        switch (dataOfClass.type)
        {
          default:
            ((Component) this).get_gameObject().SetActive(active);
            break;
        }
      }
    }

    private void Update()
    {
    }
  }
}
