// Decompiled with JetBrains decompiler
// Type: IPunTurnManagerCallbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public interface IPunTurnManagerCallbacks
{
  void OnTurnBegins(int turn);

  void OnTurnCompleted(int turn);

  void OnPlayerMove(PhotonPlayer player, int turn, object move);

  void OnPlayerFinished(PhotonPlayer player, int turn, object move);

  void OnTurnTimeEnds(int turn);
}
