namespace SRPG
{
    using GR;
    using System;

    public class ConceptCardMaterialData
    {
        private OLong mUniqueID;
        private OString mIName;
        private OInt mNum;
        private ConceptCardParam mParam;

        public ConceptCardMaterialData()
        {
            this.mUniqueID = 0L;
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ConceptCardMaterial json)
        {
            this.mUniqueID = json.id;
            this.mIName = json.iname;
            this.mNum = json.num;
            this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
            return 1;
        }

        public OLong UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public OString IName
        {
            get
            {
                return this.mIName;
            }
        }

        public OInt Num
        {
            get
            {
                return this.mNum;
            }
            set
            {
                this.mNum = value;
                return;
            }
        }

        public ConceptCardParam Param
        {
            get
            {
                return this.mParam;
            }
        }
    }
}

