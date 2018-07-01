// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestCampaignData
  {
    public QuestCampaignValueTypes type;
    public string unit;
    public int value;

    public float GetRate()
    {
      return (float) this.value / 100f;
    }
  }
}
