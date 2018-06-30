namespace SRPG
{
    using System;

    public class AbilityDeriveData
    {
        private AbilityDeriveParam m_Param;
        private bool m_IsAdd;
        private bool m_IsDisable;

        public AbilityDeriveData(AbilityDeriveParam param)
        {
            base..ctor();
            this.m_Param = param;
            return;
        }

        public bool IsAdd
        {
            get
            {
                return this.m_IsAdd;
            }
            set
            {
                this.m_IsAdd = value;
                return;
            }
        }

        public bool IsDisable
        {
            get
            {
                return this.m_IsDisable;
            }
            set
            {
                this.m_IsDisable = value;
                return;
            }
        }

        public AbilityDeriveParam Param
        {
            get
            {
                return this.m_Param;
            }
        }
    }
}

