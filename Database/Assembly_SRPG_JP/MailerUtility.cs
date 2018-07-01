// Decompiled with JetBrains decompiler
// Type: SRPG.MailerUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using DeviceKit;

namespace SRPG
{
  public class MailerUtility
  {
    public static void Launch(string mailto, string subject, string body)
    {
      App.LaunchMailer(mailto, subject, body.Replace("\n", "%0A"));
    }
  }
}
