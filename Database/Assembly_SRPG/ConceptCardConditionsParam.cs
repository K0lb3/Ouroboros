namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class ConceptCardConditionsParam
    {
        public string iname;
        public string unit_group;
        public EUseConditionsType units_conditions_type;
        public string job_group;
        public EUseConditionsType jobs_conditions_type;
        public ESex sex;
        public int[] birth_id;
        private Dictionary<EElement, int> conditions_elements;
        private int element_sum;

        public ConceptCardConditionsParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ConceptCardConditionsParam json)
        {
            int num;
            this.iname = json.iname;
            this.unit_group = json.un_group;
            this.units_conditions_type = json.units_cnds_type;
            this.job_group = json.job_group;
            this.jobs_conditions_type = json.jobs_cnds_type;
            this.sex = json.sex;
            if (json.birth_id == null)
            {
                goto Label_008F;
            }
            this.birth_id = new int[(int) json.birth_id.Length];
            num = 0;
            goto Label_0081;
        Label_006D:
            this.birth_id[num] = json.birth_id[num];
            num += 1;
        Label_0081:
            if (num < ((int) this.birth_id.Length))
            {
                goto Label_006D;
            }
        Label_008F:
            this.conditions_elements = new Dictionary<EElement, int>();
            this.conditions_elements.Add(1, json.el_fire);
            this.conditions_elements.Add(2, json.el_watr);
            this.conditions_elements.Add(3, json.el_wind);
            this.conditions_elements.Add(4, json.el_thdr);
            this.conditions_elements.Add(5, json.el_lit);
            this.conditions_elements.Add(6, json.el_drk);
            this.element_sum = ((((json.el_fire + json.el_watr) + json.el_wind) + json.el_thdr) + json.el_lit) + json.el_drk;
            return 1;
        }

        public string GetConditionDescription()
        {
            List<string> list;
            MasterParam param;
            bool flag;
            int num;
            string str;
            UnitGroupParam param2;
            List<EElement> list2;
            bool flag2;
            int num2;
            EElement element;
            JobGroupParam param3;
            bool flag3;
            int num3;
            string str2;
            JobParam param4;
            list = new List<string>();
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            if (this.birth_id == null)
            {
                goto Label_0094;
            }
            if (((int) this.birth_id.Length) <= 0)
            {
                goto Label_0094;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_BIRTH"));
            flag = 0;
            num = 0;
            goto Label_0076;
        Label_0043:
            if (flag == null)
            {
                goto Label_0059;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
        Label_0059:
            str = UnitParam.GetBirthplaceName(this.birth_id[num]);
            list.Add(str);
            flag = 1;
            num += 1;
        Label_0076:
            if (num < ((int) this.birth_id.Length))
            {
                goto Label_0043;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
        Label_0094:
            param2 = param.GetUnitGroup(this.unit_group);
            if (param2 == null)
            {
                goto Label_00C6;
            }
            list.Add(param2.GetName());
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
        Label_00C6:
            list2 = new List<EElement>(this.conditions_elements.Keys);
            if (this.element_sum <= 0)
            {
                goto Label_0183;
            }
            flag2 = 0;
            num2 = 0;
            goto Label_0165;
        Label_00EF:
            if (this.conditions_elements[list2[num2]] == 1)
            {
                goto Label_010E;
            }
            goto Label_015F;
        Label_010E:
            if (flag2 == null)
            {
                goto Label_0125;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
        Label_0125:
            element = list2[num2];
            list.Add(LocalizedText.Get("sys.UNIT_ELEMENT_" + ((int) element)));
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_ELEMENT"));
            flag2 = 1;
        Label_015F:
            num2 += 1;
        Label_0165:
            if (num2 < list2.Count)
            {
                goto Label_00EF;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
        Label_0183:
            if (this.sex == null)
            {
                goto Label_01BE;
            }
            list.Add(LocalizedText.Get("sys.SEX_" + ((int) this.sex)));
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
        Label_01BE:
            param3 = param.GetJobGroup(this.job_group);
            if (param3 == null)
            {
                goto Label_0241;
            }
            flag3 = 0;
            num3 = 0;
            goto Label_0221;
        Label_01DE:
            if (flag3 == null)
            {
                goto Label_01F5;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
        Label_01F5:
            str2 = param3.jobs[num3];
            param4 = param.GetJobParam(str2);
            list.Add(param4.name);
            flag3 = 1;
            num3 += 1;
        Label_0221:
            if (num3 < ((int) param3.jobs.Length))
            {
                goto Label_01DE;
            }
            list.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
        Label_0241:
            list.RemoveAt(list.Count - 1);
            return string.Join(string.Empty, list.ToArray());
        }

        public string GetConditionDescriptionEquip()
        {
            string str;
            string str2;
            str = this.GetConditionDescription();
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_EQUIP");
            return (str + str2);
        }

        public JobGroupParam GetJobGroupParam()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.GetJobGroup(this.job_group);
        }

        public UnitGroupParam GetUnitGroupParam()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(this.unit_group);
        }

        public bool IsMatchBirth(int unit_birth)
        {
            if (this.birth_id == null)
            {
                goto Label_002D;
            }
            if (((int) this.birth_id.Length) <= 0)
            {
                goto Label_002D;
            }
            if (Array.IndexOf<int>(this.birth_id, unit_birth) != -1)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            return 1;
        }

        public bool IsMatchElement(EElement element)
        {
            if (this.element_sum > 0)
            {
                goto Label_000E;
            }
            return 1;
        Label_000E:
            return (this.conditions_elements[element] > 0);
        }

        public bool IsMatchJobGroup(string job_iname)
        {
            JobGroupParam param;
            bool flag;
            if (string.IsNullOrEmpty(this.job_group) == null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetJobGroup(this.job_group);
            if (param != null)
            {
                goto Label_0030;
            }
            return 1;
        Label_0030:
            flag = param.IsInGroup(job_iname);
            if (this.jobs_conditions_type != 1)
            {
                goto Label_0049;
            }
            return (flag == 0);
        Label_0049:
            return flag;
        }

        public bool IsMatchUnitGroup(string unit_iname)
        {
            UnitGroupParam param;
            bool flag;
            if (string.IsNullOrEmpty(this.unit_group) == null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(this.unit_group);
            if (param != null)
            {
                goto Label_0030;
            }
            return 1;
        Label_0030:
            flag = param.IsInGroup(unit_iname);
            if (this.units_conditions_type != 1)
            {
                goto Label_0049;
            }
            return (flag == 0);
        Label_0049:
            return flag;
        }
    }
}

