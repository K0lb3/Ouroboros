// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.PlayGamesClientConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi
{
  public struct PlayGamesClientConfiguration
  {
    public static readonly PlayGamesClientConfiguration DefaultConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    private readonly bool mEnableSavedGames;
    private readonly InvitationReceivedDelegate mInvitationDelegate;
    private readonly MatchDelegate mMatchDelegate;

    private PlayGamesClientConfiguration(PlayGamesClientConfiguration.Builder builder)
    {
      this.mEnableSavedGames = builder.HasEnableSaveGames();
      this.mInvitationDelegate = builder.GetInvitationDelegate();
      this.mMatchDelegate = builder.GetMatchDelegate();
    }

    public bool EnableSavedGames
    {
      get
      {
        return this.mEnableSavedGames;
      }
    }

    public InvitationReceivedDelegate InvitationDelegate
    {
      get
      {
        return this.mInvitationDelegate;
      }
    }

    public MatchDelegate MatchDelegate
    {
      get
      {
        return this.mMatchDelegate;
      }
    }

    public class Builder
    {
      private InvitationReceivedDelegate mInvitationDelegate = (InvitationReceivedDelegate) ((_param0, _param1) => {});
      private MatchDelegate mMatchDelegate = (MatchDelegate) ((_param0, _param1) => {});
      private bool mEnableSaveGames;

      public PlayGamesClientConfiguration.Builder EnableSavedGames()
      {
        this.mEnableSaveGames = true;
        return this;
      }

      public PlayGamesClientConfiguration.Builder WithInvitationDelegate(InvitationReceivedDelegate invitationDelegate)
      {
        this.mInvitationDelegate = Misc.CheckNotNull<InvitationReceivedDelegate>(invitationDelegate);
        return this;
      }

      public PlayGamesClientConfiguration.Builder WithMatchDelegate(MatchDelegate matchDelegate)
      {
        this.mMatchDelegate = Misc.CheckNotNull<MatchDelegate>(matchDelegate);
        return this;
      }

      internal bool HasEnableSaveGames()
      {
        return this.mEnableSaveGames;
      }

      internal MatchDelegate GetMatchDelegate()
      {
        return this.mMatchDelegate;
      }

      internal InvitationReceivedDelegate GetInvitationDelegate()
      {
        return this.mInvitationDelegate;
      }

      public PlayGamesClientConfiguration Build()
      {
        return new PlayGamesClientConfiguration(this);
      }
    }
  }
}
