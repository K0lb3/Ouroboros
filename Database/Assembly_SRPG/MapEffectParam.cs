namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class MapEffectParam
    {
        private int mIndex;
        private string mIname;
        private string mName;
        private string mExpr;
        private List<string> mValidSkillLists;
        private static int CurrentIndex;
        private static Dictionary<string, List<JobParam>> mHaveJobDict;

        static MapEffectParam()
        {
        }

        public MapEffectParam()
        {
            this.mValidSkillLists = new List<string>();
            base..ctor();
            return;
        }

        public static void AddHaveJob(string skill_iname, JobParam job_param)
        {
            JobParam[] paramArray1;
            if (mHaveJobDict != null)
            {
                goto Label_000F;
            }
            MakeHaveJobLists();
        Label_000F:
            if (mHaveJobDict.ContainsKey(skill_iname) != null)
            {
                goto Label_003E;
            }
            paramArray1 = new JobParam[] { job_param };
            mHaveJobDict.Add(skill_iname, new List<JobParam>(paramArray1));
            goto Label_0065;
        Label_003E:
            if (mHaveJobDict[skill_iname].Contains(job_param) != null)
            {
                goto Label_0065;
            }
            mHaveJobDict[skill_iname].Add(job_param);
        Label_0065:
            return;
        }

        public void Deserialize(JSON_MapEffectParam json)
        {
            string str;
            string[] strArray;
            int num;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIndex = CurrentIndex += 1;
            this.mIname = json.iname;
            this.mName = json.name;
            this.mExpr = json.expr;
            this.mValidSkillLists.Clear();
            if (json.skills == null)
            {
                goto Label_007F;
            }
            strArray = json.skills;
            num = 0;
            goto Label_0076;
        Label_0062:
            str = strArray[num];
            this.mValidSkillLists.Add(str);
            num += 1;
        Label_0076:
            if (num < ((int) strArray.Length))
            {
                goto Label_0062;
            }
        Label_007F:
            return;
        }

        public static List<JobParam> GetHaveJobLists(string skill_iname)
        {
            List<JobParam> list;
            list = new List<JobParam>();
            if (string.IsNullOrEmpty(skill_iname) == null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            if (mHaveJobDict != null)
            {
                goto Label_001F;
            }
            return list;
        Label_001F:
            if (mHaveJobDict.ContainsKey(skill_iname) == null)
            {
                goto Label_003B;
            }
            list = mHaveJobDict[skill_iname];
        Label_003B:
            return list;
        }

        public static unsafe List<MapEffectParam> GetHaveMapEffectLists(string skill_iname)
        {
            List<MapEffectParam> list;
            GameManager manager;
            MapEffectParam param;
            List<MapEffectParam>.Enumerator enumerator;
            list = new List<MapEffectParam>();
            if (string.IsNullOrEmpty(skill_iname) == null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (manager == null)
            {
                goto Label_002F;
            }
            if (manager.MapEffectParam != null)
            {
                goto Label_0031;
            }
        Label_002F:
            return list;
        Label_0031:
            enumerator = manager.MapEffectParam.GetEnumerator();
        Label_003D:
            try
            {
                goto Label_0062;
            Label_0042:
                param = &enumerator.Current;
                if (param.ValidSkillLists.Contains(skill_iname) == null)
                {
                    goto Label_0062;
                }
                list.Add(param);
            Label_0062:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0042;
                }
                goto Label_007F;
            }
            finally
            {
            Label_0073:
                ((List<MapEffectParam>.Enumerator) enumerator).Dispose();
            }
        Label_007F:
            return list;
        }

        public static bool IsMakeHaveJobLists()
        {
            return ((mHaveJobDict == null) == 0);
        }

        public bool IsValidSkill(string skill)
        {
            if (string.IsNullOrEmpty(skill) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mValidSkillLists.Contains(skill);
        }

        public static void MakeHaveJobLists()
        {
            mHaveJobDict = new Dictionary<string, List<JobParam>>();
            return;
        }

        public int Index
        {
            get
            {
                return this.mIndex;
            }
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public string Expr
        {
            get
            {
                return this.mExpr;
            }
        }

        public List<string> ValidSkillLists
        {
            get
            {
                return this.mValidSkillLists;
            }
        }
    }
}

