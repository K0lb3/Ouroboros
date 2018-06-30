namespace SRPG
{
    using System;

    public class Json_ShopItemDesc
    {
        public string iname;
        public int num;
        public string itype;
        public int maxnum;
        public int boughtnum;
        public int has_count;

        public Json_ShopItemDesc()
        {
            base..ctor();
            return;
        }

        public bool IsItem
        {
            get
            {
                return (this.itype == "item");
            }
        }

        public bool IsArtifact
        {
            get
            {
                return (this.itype == "artifact");
            }
        }

        public bool IsConceptCard
        {
            get
            {
                return (this.itype == "concept_card");
            }
        }
    }
}

