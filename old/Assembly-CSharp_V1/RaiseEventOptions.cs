// Decompiled with JetBrains decompiler
// Type: RaiseEventOptions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
