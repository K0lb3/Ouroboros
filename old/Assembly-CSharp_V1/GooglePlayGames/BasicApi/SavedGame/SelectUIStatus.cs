// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.SelectUIStatus
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.SavedGame
{
  public enum SelectUIStatus
  {
    BadInputError = -4,
    AuthenticationError = -3,
    TimeoutError = -2,
    InternalError = -1,
    SavedGameSelected = 1,
    UserClosedUI = 2,
  }
}
