namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UsageRateRankingItem : MonoBehaviour
    {
        public Text rank;
        public Text unit_name;
        public ImageArray RankIconArray;
        public RawImage_Transparent JobIcon;

        public UsageRateRankingItem()
        {
            base..ctor();
            return;
        }

        public void Refresh(int rank_num, RankingUnitData data)
        {
            object[] objArray2;
            object[] objArray1;
            GameManager manager;
            JobParam param;
            UnitParam param2;
            UnitData data2;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.GetJobParam(data.job_iname);
            param2 = manager.GetUnitParam(data.unit_iname);
            data2 = new UnitData();
            data2.Setup(param2.iname, 0, 1, 1, param.iname, 1, param2.element, 0);
            DataSource.Bind<UnitData>(base.get_gameObject(), data2);
            if ((this.rank != null) == null)
            {
                goto Label_0085;
            }
            objArray1 = new object[] { (int) rank_num };
            this.rank.set_text(LocalizedText.Get("sys.RANKING_RANK", objArray1));
        Label_0085:
            if ((this.unit_name != null) == null)
            {
                goto Label_00C8;
            }
            objArray2 = new object[] { data2.UnitParam.name, param.name };
            this.unit_name.set_text(LocalizedText.Get("sys.RANKING_UNIT_NAME", objArray2));
        Label_00C8:
            this.RankIconArray.set_enabled((((int) this.RankIconArray.Images.Length) < rank_num) == 0);
            this.rank.set_enabled(this.RankIconArray.get_enabled() == 0);
            if ((this.JobIcon != null) == null)
            {
                goto Label_0132;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.JobIcon, (param == null) ? null : AssetPath.JobIconSmall(param));
        Label_0132:
            if (this.RankIconArray.get_enabled() == null)
            {
                goto Label_0150;
            }
            this.RankIconArray.ImageIndex = rank_num - 1;
        Label_0150:
            return;
        }
    }
}

