namespace SRPG
{
    using System;

    public class AbilityDeriveParam : BaseDeriveParam<AbilityParam>
    {
        public AbilityDeriveParam()
        {
            base..ctor();
            return;
        }

        public string BaseAbilityIname
        {
            get
            {
                return base.m_BaseParam.iname;
            }
        }

        public string DeriveAbilityIname
        {
            get
            {
                return base.m_DeriveParam.iname;
            }
        }

        public string BaseAbilityName
        {
            get
            {
                return base.m_BaseParam.name;
            }
        }

        public string DeriveAbilityName
        {
            get
            {
                return base.m_DeriveParam.name;
            }
        }
    }
}

