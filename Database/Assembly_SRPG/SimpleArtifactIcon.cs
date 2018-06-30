namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class SimpleArtifactIcon : BaseIcon
    {
        [SerializeField]
        private Text Num;
        [SerializeField]
        private Text HaveNum;

        public SimpleArtifactIcon()
        {
            base..ctor();
            return;
        }

        public override unsafe void UpdateValue()
        {
            object[] objArray1;
            ArtifactParam param;
            int num;
            int num2;
            param = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_009B;
            }
            if ((this.Num != null) == null)
            {
                goto Label_0043;
            }
            num = DataSource.FindDataOfClass<int>(base.get_gameObject(), 0);
            this.Num.set_text(&num.ToString());
        Label_0043:
            if ((this.HaveNum != null) == null)
            {
                goto Label_009B;
            }
            num2 = MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(param.iname, param.rareini);
            if (num2 <= 0)
            {
                goto Label_009B;
            }
            objArray1 = new object[] { (int) num2 };
            this.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", objArray1));
        Label_009B:
            return;
        }
    }
}

