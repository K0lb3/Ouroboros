// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ConceptCardTrustRewardParam
  {
    public string iname;
    public ConceptCardTrustRewardItemParam[] rewards;

    public bool Deserialize(JSON_ConceptCardTrustRewardParam json)
    {
      this.iname = json.iname;
      if (json.rewards != null)
      {
        this.rewards = new ConceptCardTrustRewardItemParam[json.rewards.Length];
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          ConceptCardTrustRewardItemParam trustRewardItemParam = new ConceptCardTrustRewardItemParam();
          if (!trustRewardItemParam.Deserialize(json.rewards[index]))
            return false;
          this.rewards[index] = trustRewardItemParam;
        }
      }
      return true;
    }
  }
}
