// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusSeasonRewardInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;
    private List<GameObject> mItems;

    public VersusSeasonRewardInfo()
    {
      base.\u002Ector();
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.template, (Object) null))
        return;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).get_gameObject(), (VersusTowerParam) null);
      if (dataOfClass != null)
      {
        while (this.mItems.Count < dataOfClass.SeasonIteminame.Length)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.template);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            if (Object.op_Inequality((Object) this.parent, (Object) null))
              gameObject.get_transform().SetParent(this.parent.get_transform(), false);
            this.mItems.Add(gameObject);
          }
        }
        for (int idx = 0; idx < dataOfClass.SeasonIteminame.Length; ++idx)
        {
          GameObject mItem = this.mItems[idx];
          if (Object.op_Inequality((Object) mItem, (Object) null))
          {
            DataSource.Bind<VersusTowerParam>(mItem, dataOfClass);
            mItem.SetActive(true);
            VersusTowerRewardItem component = (VersusTowerRewardItem) mItem.GetComponent<VersusTowerRewardItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Season, idx);
          }
        }
      }
      this.template.SetActive(false);
    }
  }
}
