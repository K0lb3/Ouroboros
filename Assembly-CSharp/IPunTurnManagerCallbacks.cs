// Decompiled with JetBrains decompiler
// Type: IPunTurnManagerCallbacks
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public interface IPunTurnManagerCallbacks
{
  void OnTurnBegins(int turn);

  void OnTurnCompleted(int turn);

  void OnPlayerMove(PhotonPlayer player, int turn, object move);

  void OnPlayerFinished(PhotonPlayer player, int turn, object move);

  void OnTurnTimeEnds(int turn);
}
