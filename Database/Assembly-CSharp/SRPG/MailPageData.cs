// Decompiled with JetBrains decompiler
// Type: SRPG.MailPageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class MailPageData
  {
    public List<MailData> mails;
    public bool hasNext;
    public bool hasPrev;
    public int page;
    public int pageMax;
    public int mailCount;

    public void Deserialize(Json_Mail[] mailArray)
    {
      if (this.mails == null)
        this.mails = new List<MailData>();
      if (mailArray == null)
        return;
      foreach (Json_Mail mail in mailArray)
      {
        MailData mailData = new MailData();
        mailData.Deserialize(mail);
        this.mails.Add(mailData);
      }
    }

    public void Deserialize(Json_MailOption mailOption)
    {
      this.hasNext = (int) mailOption.hasNext > 0;
      this.hasPrev = (int) mailOption.hasPrev > 0;
      this.page = mailOption.currentPage;
      this.pageMax = mailOption.totalPage;
      this.mailCount = mailOption.totalCount;
    }
  }
}
