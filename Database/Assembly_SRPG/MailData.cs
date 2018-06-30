namespace SRPG
{
    using System;

    public class MailData
    {
        public long mid;
        public string msg;
        public GiftData[] gifts;
        public long read;
        public long post_at;
        public int period;

        public MailData()
        {
            base..ctor();
            return;
        }

        public bool Contains(GiftTypes giftType)
        {
            int num;
            if (this.gifts != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = 0;
            goto Label_002D;
        Label_0014:
            if (this.gifts[num].CheckGiftTypeIncluded(giftType) == null)
            {
                goto Label_0029;
            }
            return 1;
        Label_0029:
            num += 1;
        Label_002D:
            if (num < ((int) this.gifts.Length))
            {
                goto Label_0014;
            }
            return 0;
        }

        public bool Deserialize(Json_Mail json)
        {
            int num;
            GiftData data;
            this.mid = json.mid;
            this.msg = json.msg;
            this.gifts = new GiftData[(int) json.gifts.Length];
            num = 0;
            goto Label_0157;
        Label_0032:
            data = new GiftData();
            data.iname = json.gifts[num].iname;
            data.num = json.gifts[num].num;
            data.gold = json.gifts[num].gold;
            data.coin = json.gifts[num].coin;
            data.arenacoin = json.gifts[num].arenacoin;
            data.multicoin = json.gifts[num].multicoin;
            data.kakeracoin = json.gifts[num].kakeracoin;
            if (json.gifts[num].concept_card == null)
            {
                goto Label_0131;
            }
            data.conceptCard = new GiftData.GiftConceptCard();
            data.conceptCard.iname = json.gifts[num].concept_card.iname;
            data.conceptCard.num = json.gifts[num].concept_card.num;
            data.conceptCard.get_unit = json.gifts[num].concept_card.get_unit;
        Label_0131:
            data.rarity = json.gifts[num].rare;
            data.UpdateGiftTypes();
            this.gifts[num] = data;
            num += 1;
        Label_0157:
            if (num < ((int) json.gifts.Length))
            {
                goto Label_0032;
            }
            this.read = (long) json.read;
            this.post_at = json.post_at;
            this.period = json.period;
            return 1;
        }

        public GiftData Find(GiftTypes giftType)
        {
            int num;
            if (this.gifts != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            num = 0;
            goto Label_0034;
        Label_0014:
            if (this.gifts[num].CheckGiftTypeIncluded(giftType) == null)
            {
                goto Label_0030;
            }
            return this.gifts[num];
        Label_0030:
            num += 1;
        Label_0034:
            if (num < ((int) this.gifts.Length))
            {
                goto Label_0014;
            }
            return null;
        }

        public bool IsReadMail()
        {
            return ((this.read == 0L) == 0);
        }

        public bool IsPeriod
        {
            get
            {
                return (this.period == 1);
            }
        }
    }
}

