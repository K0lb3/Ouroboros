// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.OnStateLoadedListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
