// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRankingItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UsageRateRankingItem : MonoBehaviour
  {
    public Text rank;
    public Text unit_name;
    public ImageArray RankIconArray;
    public RawImage_Transparent JobIcon;

    public UsageRateRankingItem()
    {
      base.\u002Ector();
    }

    public void Refresh(int rank_num, RankingUnitData data)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      JobParam jobParam = instance.GetJobParam(data.job_iname);
      UnitParam unitParam = instance.GetUnitParam(data.unit_iname);
      UnitData data1 = new UnitData();
      data1.Setup(unitParam.iname, 0, 1, 1, jobParam.iname, 1, unitParam.element);
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), data1);
      if (Object.op_Inequality((Object) this.rank, (Object) null))
        this.rank.set_text(LocalizedText.Get("sys.RANKING_RANK", new object[1]
        {
          (object) rank_num
        }));
      if (Object.op_Inequality((Object) this.unit_name, (Object) null))
        this.unit_name.set_text(LocalizedText.Get("sys.RANKING_UNIT_NAME", (object) data1.UnitParam.name, (object) jobParam.name));
      ((Behaviour) this.RankIconArray).set_enabled(this.RankIconArray.Images.Length >= rank_num);
      ((Behaviour) this.rank).set_enabled(!((Behaviour) this.RankIconArray).get_enabled());
      if (Object.op_Inequality((Object) this.JobIcon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.JobIcon, jobParam == null ? (string) null : AssetPath.JobIconSmall(jobParam));
      if (!((Behaviour) this.RankIconArray).get_enabled())
        return;
      this.RankIconArray.ImageIndex = rank_num - 1;
    }
  }
}
