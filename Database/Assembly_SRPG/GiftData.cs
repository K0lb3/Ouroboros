namespace SRPG
{
    using GR;
    using System;

    public class GiftData
    {
        public string iname;
        public int num;
        public int gold;
        public int coin;
        public int arenacoin;
        public int multicoin;
        public int kakeracoin;
        public GiftConceptCard conceptCard;
        public long giftTypes;
        public int rarity;

        public GiftData()
        {
            base..ctor();
            return;
        }

        public bool CheckGiftTypeIncluded(GiftTypes flag)
        {
            return (((this.giftTypes & flag) == 0L) == 0);
        }

        public ArtifactData CreateArtifactData()
        {
            ArtifactParam param;
            ArtifactData data;
            Json_Artifact artifact;
            if (this.CheckGiftTypeIncluded(0x40L) != null)
            {
                goto Label_001A;
            }
            DebugUtility.LogError("このギフトは武具ではありません");
            return null;
        Label_001A:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname);
            if (param != null)
            {
                goto Label_0038;
            }
            return null;
        Label_0038:
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            if (this.IsValidRarity == null)
            {
                goto Label_0082;
            }
            artifact.rare = this.rarity;
            goto Label_008E;
        Label_0082:
            artifact.rare = param.rareini;
        Label_008E:
            data.Deserialize(artifact);
            return data;
        }

        public void SetGiftTypeIncluded(GiftTypes flag)
        {
            this.giftTypes |= flag;
            return;
        }

        public void UpdateGiftTypes()
        {
            if (this.gold <= 0)
            {
                goto Label_0014;
            }
            this.SetGiftTypeIncluded(2L);
        Label_0014:
            if (this.coin <= 0)
            {
                goto Label_0028;
            }
            this.SetGiftTypeIncluded(4L);
        Label_0028:
            if (this.arenacoin <= 0)
            {
                goto Label_003C;
            }
            this.SetGiftTypeIncluded(8L);
        Label_003C:
            if (this.multicoin <= 0)
            {
                goto Label_0051;
            }
            this.SetGiftTypeIncluded(0x10L);
        Label_0051:
            if (this.kakeracoin <= 0)
            {
                goto Label_0066;
            }
            this.SetGiftTypeIncluded(0x20L);
        Label_0066:
            if (string.IsNullOrEmpty(this.ConceptCardIname) != null)
            {
                goto Label_0082;
            }
            this.SetGiftTypeIncluded(0x1000L);
        Label_0082:
            if (string.IsNullOrEmpty(this.iname) == null)
            {
                goto Label_0093;
            }
            return;
        Label_0093:
            if (this.iname.StartsWith("AF_") == null)
            {
                goto Label_00C2;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x40L);
            goto Label_0217;
        Label_00C2:
            if (this.iname.StartsWith("IT_STS_") == null)
            {
                goto Label_00F4;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x2000L);
            goto Label_0217;
        Label_00F4:
            if (this.iname.StartsWith("IT_SU_") == null)
            {
                goto Label_0126;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x100L);
            goto Label_0217;
        Label_0126:
            if (this.iname.StartsWith("IT_SI_") == null)
            {
                goto Label_0158;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x200L);
            goto Label_0217;
        Label_0158:
            if (this.iname.StartsWith("IT_SA_") == null)
            {
                goto Label_018A;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x400L);
            goto Label_0217;
        Label_018A:
            if (this.iname.StartsWith("IT_") == null)
            {
                goto Label_01B8;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(1L);
            goto Label_0217;
        Label_01B8:
            if (this.iname.StartsWith("UN_") == null)
            {
                goto Label_01EA;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x80L);
            goto Label_0217;
        Label_01EA:
            if (this.iname.StartsWith("AWARD_") == null)
            {
                goto Label_0217;
            }
            if (this.num <= 0)
            {
                goto Label_0217;
            }
            this.SetGiftTypeIncluded(0x800L);
        Label_0217:
            return;
        }

        public string ConceptCardIname
        {
            get
            {
                if (this.conceptCard != null)
                {
                    goto Label_0011;
                }
                return string.Empty;
            Label_0011:
                return this.conceptCard.iname;
            }
        }

        public int ConceptCardNum
        {
            get
            {
                if (this.conceptCard != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                return this.conceptCard.num;
            }
        }

        public bool IsGetConceptCardUnit
        {
            get
            {
                if (this.conceptCard != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                return (string.IsNullOrEmpty(this.conceptCard.get_unit) == 0);
            }
        }

        public string ConceptCardGetUnitIname
        {
            get
            {
                if (this.conceptCard != null)
                {
                    goto Label_0011;
                }
                return string.Empty;
            Label_0011:
                return this.conceptCard.get_unit;
            }
        }

        public bool IsValidRarity
        {
            get
            {
                return ((this.rarity == -1) == 0);
            }
        }

        public bool NotSet
        {
            get
            {
                return (this.giftTypes == 0L);
            }
        }

        public class GiftConceptCard
        {
            public string iname;
            public int num;
            public string get_unit;

            public GiftConceptCard()
            {
                base..ctor();
                return;
            }
        }
    }
}

