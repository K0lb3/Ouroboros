namespace SRPG
{
    using System;

    public class BaseDeriveParam<T>
    {
        public SkillAbilityDeriveParam m_SkillAbilityDeriveParam;
        public T m_BaseParam;
        public T m_DeriveParam;

        public BaseDeriveParam()
        {
            base..ctor();
            return;
        }

        public int MasterIndex
        {
            get
            {
                return this.m_SkillAbilityDeriveParam.m_OriginIndex;
            }
        }
    }
}

