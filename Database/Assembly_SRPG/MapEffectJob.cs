namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class MapEffectJob : MonoBehaviour
    {
        public GameObject GoMapEffectParent;
        public GameObject GoMapEffectBaseItem;
        [CompilerGenerated]
        private static Comparison<MeSkill> <>f__am$cache2;

        public MapEffectJob()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Setup>m__364(MeSkill src, MeSkill dsc)
        {
            if (src != dsc)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            return (dsc.mMapEffectParam.Index - src.mMapEffectParam.Index);
        }

        public unsafe void Setup()
        {
            GameObject[] objArray1;
            JobParam param;
            GameManager manager;
            DataSource source;
            List<MeSkill> list;
            AbilityParam param2;
            LearningSkill skill;
            LearningSkill[] skillArray;
            int num;
            SkillParam param3;
            List<MapEffectParam> list2;
            MapEffectParam param4;
            List<MapEffectParam>.Enumerator enumerator;
            bool flag;
            MeSkill skill2;
            List<MeSkill>.Enumerator enumerator2;
            MeSkill skill3;
            List<MeSkill>.Enumerator enumerator3;
            GameObject obj2;
            param = DataSource.FindDataOfClass<JobParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (manager != null)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            if (this.GoMapEffectParent == null)
            {
                goto Label_0071;
            }
            if (this.GoMapEffectBaseItem == null)
            {
                goto Label_0071;
            }
            this.GoMapEffectBaseItem.SetActive(0);
            objArray1 = new GameObject[] { this.GoMapEffectBaseItem };
            BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>(objArray1));
        Label_0071:
            source = base.get_gameObject().GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_008E;
            }
            source.Clear();
        Label_008E:
            DataSource.Bind<JobParam>(base.get_gameObject(), param);
            if (this.GoMapEffectParent == null)
            {
                goto Label_02A2;
            }
            if (this.GoMapEffectBaseItem == null)
            {
                goto Label_02A2;
            }
            list = new List<MeSkill>();
            if (string.IsNullOrEmpty(param.MapEffectAbility) != null)
            {
                goto Label_01D6;
            }
            param2 = manager.GetAbilityParam(param.MapEffectAbility);
            if (param2 == null)
            {
                goto Label_01D6;
            }
            skillArray = param2.skills;
            num = 0;
            goto Label_01CB;
        Label_00F6:
            skill = skillArray[num];
            param3 = manager.GetSkillParam(skill.iname);
            if (param3 != null)
            {
                goto Label_0118;
            }
            goto Label_01C5;
        Label_0118:
            enumerator = MapEffectParam.GetHaveMapEffectLists(skill.iname).GetEnumerator();
        Label_012F:
            try
            {
                goto Label_01A7;
            Label_0134:
                param4 = &enumerator.Current;
                flag = 0;
                enumerator2 = list.GetEnumerator();
            Label_0148:
                try
                {
                    goto Label_016E;
                Label_014D:
                    skill2 = &enumerator2.Current;
                    if (skill2.Equals(param4, param3) == null)
                    {
                        goto Label_016E;
                    }
                    flag = 1;
                    goto Label_017A;
                Label_016E:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_014D;
                    }
                Label_017A:
                    goto Label_018C;
                }
                finally
                {
                Label_017F:
                    ((List<MeSkill>.Enumerator) enumerator2).Dispose();
                }
            Label_018C:
                if (flag == null)
                {
                    goto Label_0198;
                }
                goto Label_01A7;
            Label_0198:
                list.Add(new MeSkill(param4, param3));
            Label_01A7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0134;
                }
                goto Label_01C5;
            }
            finally
            {
            Label_01B8:
                ((List<MapEffectParam>.Enumerator) enumerator).Dispose();
            }
        Label_01C5:
            num += 1;
        Label_01CB:
            if (num < ((int) skillArray.Length))
            {
                goto Label_00F6;
            }
        Label_01D6:
            if (list.Count == null)
            {
                goto Label_02A2;
            }
            if (<>f__am$cache2 != null)
            {
                goto Label_01FA;
            }
            <>f__am$cache2 = new Comparison<MeSkill>(MapEffectJob.<Setup>m__364);
        Label_01FA:
            MySort<MeSkill>.Sort(list, <>f__am$cache2);
            enumerator3 = list.GetEnumerator();
        Label_020C:
            try
            {
                goto Label_0284;
            Label_0211:
                skill3 = &enumerator3.Current;
                obj2 = Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
                if (obj2 != null)
                {
                    goto Label_0238;
                }
                goto Label_0284;
            Label_0238:
                obj2.get_transform().SetParent(this.GoMapEffectParent.get_transform());
                obj2.get_transform().set_localScale(Vector3.get_one());
                DataSource.Bind<MapEffectParam>(obj2, skill3.mMapEffectParam);
                DataSource.Bind<SkillParam>(obj2, skill3.mSkillParam);
                obj2.SetActive(1);
            Label_0284:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0211;
                }
                goto Label_02A2;
            }
            finally
            {
            Label_0295:
                ((List<MeSkill>.Enumerator) enumerator3).Dispose();
            }
        Label_02A2:
            return;
        }

        private class MeSkill
        {
            public MapEffectParam mMapEffectParam;
            public SkillParam mSkillParam;

            public MeSkill(MapEffectParam me_param, SkillParam skill_param)
            {
                base..ctor();
                this.mMapEffectParam = me_param;
                this.mSkillParam = skill_param;
                return;
            }

            public bool Equals(MapEffectParam me_param, SkillParam skill_param)
            {
                return ((this.mMapEffectParam != me_param) ? 0 : (this.mSkillParam == skill_param));
            }
        }
    }
}

