// Decompiled with JetBrains decompiler
// Type: SRPG.MailData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class MailData
  {
    public long mid;
    public string msg;
    public GiftData[] gifts;
    public long read;
    public long post_at;
    public int period;
    public int type;

    public bool Deserialize(Json_Mail json)
    {
      this.mid = json.mid;
      this.msg = json.msg;
      if (this.msg.Contains("{"))
      {
        LocMailMsg jsonObject = JSONParser.parseJSONObject<LocMailMsg>(this.msg);
        if (jsonObject != null)
        {
          string configLanguage = GameUtility.Config_Language;
          if (configLanguage != null)
          {
            // ISSUE: reference to a compiler-generated field
            if (MailData.\u003C\u003Ef__switch\u0024map12 == null)
            {
              // ISSUE: reference to a compiler-generated field
              MailData.\u003C\u003Ef__switch\u0024map12 = new Dictionary<string, int>(3)
              {
                {
                  "french",
                  0
                },
                {
                  "german",
                  1
                },
                {
                  "spanish",
                  2
                }
              };
            }
            int num;
            // ISSUE: reference to a compiler-generated field
            if (MailData.\u003C\u003Ef__switch\u0024map12.TryGetValue(configLanguage, out num))
            {
              switch (num)
              {
                case 0:
                  this.msg = jsonObject.fr;
                  goto label_11;
                case 1:
                  this.msg = jsonObject.de;
                  goto label_11;
                case 2:
                  this.msg = jsonObject.es;
                  goto label_11;
              }
            }
          }
          this.msg = jsonObject.en;
        }
      }
label_11:
      this.gifts = new GiftData[json.gifts.Length];
      for (int index = 0; index < json.gifts.Length; ++index)
      {
        GiftData giftData = new GiftData();
        giftData.iname = json.gifts[index].iname;
        giftData.num = json.gifts[index].num;
        giftData.gold = json.gifts[index].gold;
        giftData.coin = json.gifts[index].coin;
        giftData.arenacoin = json.gifts[index].arenacoin;
        giftData.multicoin = json.gifts[index].multicoin;
        giftData.kakeracoin = json.gifts[index].kakeracoin;
        giftData.rarity = json.gifts[index].rare;
        giftData.UpdateGiftTypes();
        this.gifts[index] = giftData;
      }
      this.read = (long) json.read;
      this.post_at = json.post_at;
      this.period = json.period;
      this.type = json.type;
      return true;
    }

    public bool IsPeriod
    {
      get
      {
        return this.period == 1;
      }
    }

    public bool IsReadMail()
    {
      return this.read != 0L;
    }

    public bool Contains(GiftTypes giftType)
    {
      if (this.gifts == null)
        return false;
      for (int index = 0; index < this.gifts.Length; ++index)
      {
        if (this.gifts[index].CheckGiftTypeIncluded(giftType))
          return true;
      }
      return false;
    }

    public GiftData Find(GiftTypes giftType)
    {
      if (this.gifts == null)
        return (GiftData) null;
      for (int index = 0; index < this.gifts.Length; ++index)
      {
        if (this.gifts[index].CheckGiftTypeIncluded(giftType))
          return this.gifts[index];
      }
      return (GiftData) null;
    }
  }
}
