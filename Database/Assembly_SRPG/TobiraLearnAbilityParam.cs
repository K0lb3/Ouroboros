namespace SRPG
{
    using System;

    public class TobiraLearnAbilityParam
    {
        private string mAbilityIname;
        private int mLevel;
        private AddType mAddType;
        private string mAbilityOverwrite;

        public TobiraLearnAbilityParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraLearnAbilityParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mAbilityIname = json.abil_iname;
            this.mLevel = json.learn_lv;
            this.mAddType = json.add_type;
            this.mAbilityOverwrite = json.abil_overwrite;
            return;
        }

        public string AbilityIname
        {
            get
            {
                return this.mAbilityIname;
            }
        }

        public int Level
        {
            get
            {
                return this.mLevel;
            }
        }

        public AddType AbilityAddType
        {
            get
            {
                return this.mAddType;
            }
        }

        public string AbilityOverwrite
        {
            get
            {
                return this.mAbilityOverwrite;
            }
        }

        public enum AddType
        {
            Unknow,
            JobOverwrite,
            MasterAdd,
            MasterOverwrite
        }
    }
}

