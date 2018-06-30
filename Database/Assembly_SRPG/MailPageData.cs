namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class MailPageData
    {
        public List<MailData> mails;
        public bool hasNext;
        public bool hasPrev;
        public int page;
        public int pageMax;
        public int mailCount;

        public MailPageData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_Mail[] mailArray)
        {
            Json_Mail mail;
            Json_Mail[] mailArray2;
            int num;
            MailData data;
            if (this.mails != null)
            {
                goto Label_0016;
            }
            this.mails = new List<MailData>();
        Label_0016:
            if (mailArray == null)
            {
                goto Label_0050;
            }
            mailArray2 = mailArray;
            num = 0;
            goto Label_0047;
        Label_0025:
            mail = mailArray2[num];
            data = new MailData();
            data.Deserialize(mail);
            this.mails.Add(data);
            num += 1;
        Label_0047:
            if (num < ((int) mailArray2.Length))
            {
                goto Label_0025;
            }
        Label_0050:
            return;
        }

        public void Deserialize(Json_MailOption mailOption)
        {
            this.hasNext = mailOption.hasNext > 0;
            this.hasPrev = mailOption.hasPrev > 0;
            this.page = mailOption.currentPage;
            this.pageMax = mailOption.totalPage;
            this.mailCount = mailOption.totalCount;
            return;
        }
    }
}

