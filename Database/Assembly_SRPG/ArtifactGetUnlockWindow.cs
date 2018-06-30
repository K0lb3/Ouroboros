namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(100, "Unlock", 1, 100), Pin(0x65, "Selected Quest", 1, 0x65), Pin(0x1f5, "武具詳細情報セット(out)", 1, 0x1f5), Pin(500, "武具詳細情報セット(in)", 0, 500)]
    public class ArtifactGetUnlockWindow : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_REFRESH = 1;
        private const int INPUT_UNLOCK = 100;
        private const int INPUT_SELECT_QUEST = 0x65;
        private const int INPUT_ARTIFACT_DETAIL_SET = 500;
        private const int OUTPUT_ARTIFACT_DETAIL_SET = 0x1f5;
        private ArtifactData UnlockArtifact;
        public StatusList ArtifactStatus;
        public GameObject AbilityListItem;
        public float ability_unlock_alpha;
        public float ability_hidden_alpha;
        public GameObject lock_object;
        [HeaderBar("▼セット効果確認用のボタン"), SerializeField]
        private Button m_SetEffectsButton;

        public ArtifactGetUnlockWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            num = pinID;
            if (num == 1)
            {
                goto Label_0026;
            }
            if (num == 500)
            {
                goto Label_0031;
            }
            goto Label_0047;
        Label_0026:
            this.Refresh();
            goto Label_0047;
        Label_0031:
            this.SetArtifactDetailData();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x1f5);
        Label_0047:
            return;
        }

        private unsafe void Refresh()
        {
            ArtifactData data;
            Json_Artifact artifact;
            MasterParam param;
            GameObject obj2;
            CanvasGroup group;
            bool flag;
            ArtifactParam param2;
            List<AbilityData> list;
            AbilityParam param3;
            int num;
            AbilityData data2;
            BaseStatus status;
            BaseStatus status2;
            <Refresh>c__AnonStorey2F5 storeyf;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = GlobalVars.ArtifactListItem.param.iname;
            artifact.rare = GlobalVars.ArtifactListItem.param.rareini;
            data.Deserialize(artifact);
            this.UnlockArtifact = data;
            DataSource.Bind<ArtifactData>(base.get_gameObject(), this.UnlockArtifact);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), this.UnlockArtifact.ArtifactParam);
            if ((this.AbilityListItem != null) == null)
            {
                goto Label_01E6;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            obj2 = this.AbilityListItem;
            group = obj2.GetComponent<CanvasGroup>();
            flag = 0;
            param2 = data.ArtifactParam;
            list = data.LearningAbilities;
            if (param2.abil_inames == null)
            {
                goto Label_01C0;
            }
            storeyf = new <Refresh>c__AnonStorey2F5();
            param3 = null;
            storeyf.abil_iname = null;
            num = 0;
            goto Label_012C;
        Label_00CF:
            if (string.IsNullOrEmpty(param2.abil_inames[num]) != null)
            {
                goto Label_0126;
            }
            if (param2.abil_shows[num] != null)
            {
                goto Label_00F7;
            }
            goto Label_0126;
        Label_00F7:
            storeyf.abil_iname = param2.abil_inames[num];
            param3 = param.GetAbilityParam(param2.abil_inames[num]);
            if (param3 == null)
            {
                goto Label_0126;
            }
            goto Label_013C;
        Label_0126:
            num += 1;
        Label_012C:
            if (num < ((int) param2.abil_inames.Length))
            {
                goto Label_00CF;
            }
        Label_013C:
            if (param3 != null)
            {
                goto Label_0169;
            }
            group.set_alpha(this.ability_hidden_alpha);
            DataSource.Bind<AbilityParam>(this.AbilityListItem, null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, null);
            return;
        Label_0169:
            DataSource.Bind<AbilityParam>(this.AbilityListItem, param3);
            DataSource.Bind<AbilityData>(obj2, null);
            if ((group != null) == null)
            {
                goto Label_01C0;
            }
            if (list == null)
            {
                goto Label_01C0;
            }
            if (list == null)
            {
                goto Label_01C0;
            }
            data2 = list.Find(new Predicate<AbilityData>(storeyf.<>m__27C));
            if (data2 == null)
            {
                goto Label_01C0;
            }
            DataSource.Bind<AbilityData>(obj2, data2);
            flag = 1;
        Label_01C0:
            if (flag == null)
            {
                goto Label_01D9;
            }
            group.set_alpha(this.ability_unlock_alpha);
            goto Label_01E6;
        Label_01D9:
            group.set_alpha(this.ability_hidden_alpha);
        Label_01E6:
            status = new BaseStatus();
            status2 = new BaseStatus();
            this.UnlockArtifact.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            this.ArtifactStatus.SetValues(status, status2, 0);
            if ((this.m_SetEffectsButton != null) == null)
            {
                goto Label_0281;
            }
            if (this.UnlockArtifact.ArtifactParam == null)
            {
                goto Label_0281;
            }
            this.m_SetEffectsButton.set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.UnlockArtifact.ArtifactParam.iname));
            if (this.m_SetEffectsButton.get_interactable() == null)
            {
                goto Label_0281;
            }
            ArtifactSetList.SetSelectedArtifactParam(this.UnlockArtifact.ArtifactParam);
        Label_0281:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void SetArtifactData()
        {
            GlobalVars.ConditionJobs = GlobalVars.ArtifactListItem.param.condition_jobs;
            return;
        }

        private void SetArtifactDetailData()
        {
            if (this.UnlockArtifact == null)
            {
                goto Label_001B;
            }
            ArtifactDetailWindow.SetArtifactParam(this.UnlockArtifact.ArtifactParam);
        Label_001B:
            return;
        }

        private void Start()
        {
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey2F5
        {
            internal string abil_iname;

            public <Refresh>c__AnonStorey2F5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__27C(AbilityData x)
            {
                return (x.Param.iname == this.abil_iname);
            }
        }
    }
}

