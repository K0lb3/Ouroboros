// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SimpleArtifactIcon : BaseIcon
  {
    [SerializeField]
    private Text Num;
    [SerializeField]
    private Text HaveNum;

    public override void UpdateValue()
    {
      ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(((Component) this).get_gameObject(), (ArtifactParam) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.set_text(DataSource.FindDataOfClass<int>(((Component) this).get_gameObject(), 0).ToString());
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int artifactNumByRarity = MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(dataOfClass.iname, dataOfClass.rareini);
      if (artifactNumByRarity <= 0)
        return;
      this.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) artifactNumByRarity
      }));
    }
  }
}
