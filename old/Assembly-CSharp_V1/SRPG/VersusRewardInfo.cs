// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRewardInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class VersusRewardInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;
    public bool season;

    public VersusRewardInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.template, (Object) null) || Object.op_Equality((Object) this.parent, (Object) null))
        return;
      this.Setup();
    }

    private void Setup()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam == null)
        return;
      for (int index = versusTowerParam.Length - 1; index >= 0; --index)
      {
        if (!((string) versusTowerParam[index].VersusTowerID != instance.VersusTowerMatchName) && !string.IsNullOrEmpty((string) versusTowerParam[index].ArrivalIteminame))
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.template);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            DataSource.Bind<VersusTowerParam>(gameObject, versusTowerParam[index]);
            VersusTowerRewardItem component = (VersusTowerRewardItem) gameObject.GetComponent<VersusTowerRewardItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh();
            if (Object.op_Inequality((Object) this.parent, (Object) null))
              gameObject.get_transform().SetParent(this.parent.get_transform(), false);
          }
        }
      }
      this.template.SetActive(false);
    }
  }
}
