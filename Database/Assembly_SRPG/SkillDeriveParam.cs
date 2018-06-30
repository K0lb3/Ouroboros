namespace SRPG
{
    using System;

    public class SkillDeriveParam : BaseDeriveParam<SkillParam>
    {
        public SkillDeriveParam()
        {
            base..ctor();
            return;
        }

        public string BaseSkillIname
        {
            get
            {
                return base.m_BaseParam.iname;
            }
        }

        public string DeriveSkillIname
        {
            get
            {
                return base.m_DeriveParam.iname;
            }
        }

        public string BaseSkillName
        {
            get
            {
                return base.m_BaseParam.name;
            }
        }

        public string DeriveSkillName
        {
            get
            {
                return base.m_DeriveParam.name;
            }
        }
    }
}

