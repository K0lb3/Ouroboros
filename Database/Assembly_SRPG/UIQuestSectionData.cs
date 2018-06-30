namespace SRPG
{
    using System;

    public class UIQuestSectionData
    {
        private SectionParam mParam;

        public UIQuestSectionData(SectionParam param)
        {
            base..ctor();
            this.mParam = param;
            return;
        }

        public string Name
        {
            get
            {
                return this.mParam.name;
            }
        }

        public string SectionID
        {
            get
            {
                return this.mParam.iname;
            }
        }
    }
}

