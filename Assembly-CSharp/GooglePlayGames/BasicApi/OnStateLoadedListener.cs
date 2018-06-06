// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.OnStateLoadedListener
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public interface OnStateLoadedListener
  {
    void OnStateLoaded(bool success, int slot, byte[] data);

    byte[] OnStateConflict(int slot, byte[] localData, byte[] serverData);

    void OnStateSaved(bool success, int slot);
  }
}
