namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1)]
    public class SelectArtifactInfo : MonoBehaviour, IFlowInterface
    {
        public GameObject AbilityListItem;
        public float ability_unlock_alpha;
        public float ability_hidden_alpha;
        public GameObject lock_object;

        public SelectArtifactInfo()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void Awake()
        {
        }

        private void Refresh()
        {
            ArtifactParam param;
            ArtifactData data;
            Json_Artifact artifact;
            MasterParam param2;
            GameObject obj2;
            CanvasGroup group;
            bool flag;
            ArtifactParam param3;
            List<AbilityData> list;
            AbilityParam param4;
            int num;
            AbilityData data2;
            <Refresh>c__AnonStorey3A1 storeya;
            param = GlobalVars.ArtifactListItem.param;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = GlobalVars.ArtifactListItem.param.iname;
            artifact.rare = GlobalVars.ArtifactListItem.param.rareini;
            data.Deserialize(artifact);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), param);
            DataSource.Bind<ArtifactData>(base.get_gameObject(), data);
            if ((this.AbilityListItem != null) == null)
            {
                goto Label_01DF;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam;
            obj2 = this.AbilityListItem;
            group = obj2.GetComponent<CanvasGroup>();
            flag = 0;
            param3 = data.ArtifactParam;
            list = data.LearningAbilities;
            if (param3.abil_inames == null)
            {
                goto Label_01B9;
            }
            storeya = new <Refresh>c__AnonStorey3A1();
            param4 = null;
            storeya.abil_iname = null;
            num = 0;
            goto Label_0123;
        Label_00C6:
            if (string.IsNullOrEmpty(param3.abil_inames[num]) != null)
            {
                goto Label_011D;
            }
            if (param3.abil_shows[num] != null)
            {
                goto Label_00EE;
            }
            goto Label_011D;
        Label_00EE:
            storeya.abil_iname = param3.abil_inames[num];
            param4 = param2.GetAbilityParam(param3.abil_inames[num]);
            if (param4 == null)
            {
                goto Label_011D;
            }
            goto Label_0133;
        Label_011D:
            num += 1;
        Label_0123:
            if (num < ((int) param3.abil_inames.Length))
            {
                goto Label_00C6;
            }
        Label_0133:
            if (param4 != null)
            {
                goto Label_0160;
            }
            group.set_alpha(this.ability_hidden_alpha);
            DataSource.Bind<AbilityParam>(this.AbilityListItem, null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, null);
            return;
        Label_0160:
            DataSource.Bind<AbilityParam>(this.AbilityListItem, param4);
            DataSource.Bind<AbilityData>(obj2, null);
            if ((group != null) == null)
            {
                goto Label_01B9;
            }
            if (list == null)
            {
                goto Label_01B9;
            }
            if (list == null)
            {
                goto Label_01B9;
            }
            data2 = list.Find(new Predicate<AbilityData>(storeya.<>m__403));
            if (data2 == null)
            {
                goto Label_01B9;
            }
            DataSource.Bind<AbilityData>(obj2, data2);
            flag = 1;
        Label_01B9:
            if (flag == null)
            {
                goto Label_01D2;
            }
            group.set_alpha(this.ability_unlock_alpha);
            goto Label_01DF;
        Label_01D2:
            group.set_alpha(this.ability_hidden_alpha);
        Label_01DF:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3A1
        {
            internal string abil_iname;

            public <Refresh>c__AnonStorey3A1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__403(AbilityData x)
            {
                return (x.Param.iname == this.abil_iname);
            }
        }
    }
}

