// Decompiled with JetBrains decompiler
// Type: PhotonMessageInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public struct PhotonMessageInfo
{
  private readonly int timeInt;
  public readonly PhotonPlayer sender;
  public readonly PhotonView photonView;

  public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
  {
    this.sender = player;
    this.timeInt = timestamp;
    this.photonView = view;
  }

  public double timestamp
  {
    get
    {
      return (double) (uint) this.timeInt / 1000.0;
    }
  }

  public override string ToString()
  {
    return string.Format("[PhotonMessageInfo: Sender='{1}' Senttime={0}]", (object) this.timestamp, (object) this.sender);
  }
}
