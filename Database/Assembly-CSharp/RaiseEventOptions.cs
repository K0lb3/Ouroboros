// Decompiled with JetBrains decompiler
// Type: RaiseEventOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class RaiseEventOptions
{
  public static readonly RaiseEventOptions Default = new RaiseEventOptions();
  public EventCaching CachingOption;
  public byte InterestGroup;
  public int[] TargetActors;
  public ReceiverGroup Receivers;
  public byte SequenceChannel;
  public bool ForwardToWebhook;
  public bool Encrypt;
}
