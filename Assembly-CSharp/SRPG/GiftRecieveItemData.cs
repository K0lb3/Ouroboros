// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItemData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GiftRecieveItemData
  {
    public string iname;
    public int rarity;
    public int num;
    public string name;
    public GiftTypes type;

    public void Set(string iname, GiftTypes giftTipe, int rarity, int num)
    {
      this.iname = iname;
      this.type = giftTipe;
      this.rarity = rarity;
      this.num = num;
    }
  }
}
