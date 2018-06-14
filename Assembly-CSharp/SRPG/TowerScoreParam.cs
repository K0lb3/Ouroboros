// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScoreParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TowerScoreParam
  {
    public string Rank;
    public OInt Score;
    public OInt TurnCnt;
    public OInt DiedCnt;
    public OInt RetireCnt;
    public OInt RecoverCnt;

    public bool Deserialize(JSON_TowerScore json)
    {
      if (json == null)
        return false;
      this.Rank = json.rank;
      this.Score = (OInt) json.score;
      this.TurnCnt = (OInt) json.turn;
      this.DiedCnt = (OInt) json.died;
      this.RetireCnt = (OInt) json.retire;
      this.RecoverCnt = (OInt) json.recover;
      return true;
    }
  }
}
