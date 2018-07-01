// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReadMail2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(7, "武具引き換え券", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(0, "開封", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(20, "一件受け取り完了", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "一括受け取り完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "いくつか受け取れなかった", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(23, "何も受け取れなかった", FlowNode.PinTypes.Output, 23)]
  [FlowNode.Pin(5, "ユニット引き換え券", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "アイテム引き換え券", FlowNode.PinTypes.Input, 6)]
  [FlowNode.NodeType("System/ReadMail2", 32741)]
  public class FlowNode_ReadMail2 : FlowNode_Network
  {
    private FlowNode_ReadMail2.ReceiveStatus mReceiveStatus;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          FlowNode_ReadMail2.\u003COnActivate\u003Ec__AnonStorey2D0 activateCAnonStorey2D0 = new FlowNode_ReadMail2.\u003COnActivate\u003Ec__AnonStorey2D0();
          MailWindow.MailReadRequestData dataOfClass = DataSource.FindDataOfClass<MailWindow.MailReadRequestData>(((Component) this).get_gameObject(), (MailWindow.MailReadRequestData) null);
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(21);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          List<MailData> currentMails = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
          List<MailData> mailDataList = new List<MailData>();
          // ISSUE: reference to a compiler-generated field
          activateCAnonStorey2D0.ids = new List<long>((IEnumerable<long>) dataOfClass.mailIDs);
          // ISSUE: reference to a compiler-generated method
          List<MailData> all = currentMails.FindAll(new Predicate<MailData>(activateCAnonStorey2D0.\u003C\u003Em__2B7));
          if (all.Count < 1)
          {
            this.ActivateOutputLinks(21);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          List<GiftData> giftDataList = new List<GiftData>();
          using (FlowNode_ReadMail2.HaveCheckScope haveCheckScope = new FlowNode_ReadMail2.HaveCheckScope())
          {
            using (List<MailData>.Enumerator enumerator = all.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                MailData current = enumerator.Current;
                if (haveCheckScope.CheckReceivable(current))
                {
                  mailDataList.Add(current);
                  if (current.gifts != null)
                  {
                    for (int index = 0; index < current.gifts.Length; ++index)
                    {
                      if (current.gifts[index] != null)
                        giftDataList.Add(current.gifts[index]);
                    }
                  }
                  haveCheckScope.AddCurrentNum(current);
                }
              }
            }
          }
          if (giftDataList.Count >= 1)
            GlobalVars.UnitGetReward = new UnitGetParam(giftDataList.ToArray());
          if (mailDataList.Count < 1)
          {
            this.ActivateOutputLinks(23);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          this.mReceiveStatus = mailDataList.Count >= all.Count ? FlowNode_ReadMail2.ReceiveStatus.Recieve : FlowNode_ReadMail2.ReceiveStatus.NotReceiveSome;
          long[] mailids = new long[mailDataList.Count];
          for (int index = 0; index < mailDataList.Count; ++index)
            mailids[index] = mailDataList[index].mid;
          this.ExecRequest((WebAPI) new ReqMailRead(mailids, dataOfClass.isPeriod, dataOfClass.page, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 5:
        case 6:
        case 7:
          string str = GlobalVars.UnlockUnitID;
          switch (pinID - 5)
          {
            case 0:
              str = GlobalVars.UnlockUnitID;
              break;
            case 1:
              str = GlobalVars.ItemSelectListItemData.iiname;
              this.AnalyticsTrackItemReceived(str, 1);
              break;
            case 2:
              str = GlobalVars.ArtifactListItem.iname;
              this.AnalyticsTrackItemReceived(str, 1);
              break;
          }
          if (!string.IsNullOrEmpty(str) && pinID == 5)
            GlobalVars.UnitGetReward = new UnitGetParam(MonoSingleton<GameManager>.Instance.GetItemParam(str));
          this.mReceiveStatus = FlowNode_ReadMail2.ReceiveStatus.Recieve;
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            ((Behaviour) this).set_enabled(false);
            this.Success();
            break;
          }
          this.ExecRequest((WebAPI) new ReqMailRead((long) GlobalVars.SelectedMailUniqueID, (int) GlobalVars.SelectedMailPeriod == 1, (int) GlobalVars.SelectedMailPage, str, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
    }

    private RewardData GiftDataToRewardData(GiftData[] giftDatas)
    {
      RewardData rewardData = new RewardData();
      rewardData.Exp = 0;
      rewardData.Stamina = 0;
      rewardData.MultiCoin = 0;
      rewardData.KakeraCoin = 0;
      for (int index = 0; index < giftDatas.Length; ++index)
      {
        GiftData giftData = giftDatas[index];
        rewardData.Coin += giftData.coin;
        rewardData.Gold += giftData.gold;
        rewardData.ArenaMedal += giftData.arenacoin;
        rewardData.MultiCoin += giftData.multicoin;
        rewardData.KakeraCoin += giftData.kakeracoin;
        if (giftData.iname != null)
        {
          if (giftData.CheckGiftTypeIncluded(GiftTypes.Artifact))
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(giftData.iname);
            rewardData.AddReward(artifactParam, giftData.num);
          }
          else if (giftData.CheckGiftTypeIncluded(GiftTypes.Award))
          {
            AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(giftData.iname);
            rewardData.AddReward(awardParam.ToItemParam(), giftData.num);
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(giftData.iname);
            rewardData.AddReward(itemParam, giftData.num);
          }
        }
      }
      return rewardData;
    }

    private void SetRewardData(Json_Mail[] json)
    {
      if (json == null || json.Length == 0)
        return;
      MailData[] mailDataArray = new MailData[json.Length];
      for (int index = 0; index < mailDataArray.Length; ++index)
      {
        mailDataArray[index] = new MailData();
        mailDataArray[index].Deserialize(json[index]);
      }
      RewardData rewardData1 = new RewardData();
      for (int index = 0; index < mailDataArray.Length; ++index)
      {
        MailData mailData = mailDataArray[index];
        if (mailData != null)
        {
          RewardData rewardData2 = this.GiftDataToRewardData(mailData.gifts);
          using (Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator = rewardData2.GiftRecieveItemDataDic.Values.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GiftRecieveItemData current = enumerator.Current;
              rewardData1.AddReward(current);
            }
          }
          rewardData1.Exp += rewardData2.Exp;
          rewardData1.Stamina += rewardData2.Stamina;
          rewardData1.Coin += rewardData2.Coin;
          rewardData1.Gold += rewardData2.Gold;
          rewardData1.ArenaMedal += rewardData2.ArenaMedal;
          rewardData1.MultiCoin += rewardData2.MultiCoin;
          rewardData1.KakeraCoin += rewardData2.KakeraCoin;
        }
      }
      GlobalVars.LastReward.Set(rewardData1);
    }

    private void Success()
    {
      if (this.mReceiveStatus == FlowNode_ReadMail2.ReceiveStatus.Recieve)
        this.ActivateOutputLinks(20);
      else if (this.mReceiveStatus == FlowNode_ReadMail2.ReceiveStatus.NotReceiveAll)
      {
        this.ActivateOutputLinks(23);
      }
      else
      {
        if (this.mReceiveStatus != FlowNode_ReadMail2.ReceiveStatus.NotReceiveSome)
          return;
        this.ActivateOutputLinks(22);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        GlobalVars.UnitGetReward = (UnitGetParam) null;
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoMail:
            this.OnBack();
            break;
          case Network.EErrCode.MailReadable:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReadMail2.Json_MailRead> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReadMail2.Json_MailRead>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.mails);
          if (jsonObject.body.artifacts != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, true);
          this.SetRewardData(jsonObject.body.processed);
          this.TrackGiftAnalytics(jsonObject.body.processed);
        }
        catch (Exception ex)
        {
          GlobalVars.UnitGetReward = (UnitGetParam) null;
          DebugUtility.LogException(ex);
          return;
        }
        ((Behaviour) this).set_enabled(false);
        this.Success();
      }
    }

    private void TrackGiftAnalytics(Json_Mail[] inObtainedGifts)
    {
      if (inObtainedGifts == null)
        throw new Exception("Tracking null gifts received!");
      foreach (Json_Mail inObtainedGift in inObtainedGifts)
      {
        foreach (Json_Gift gift in inObtainedGift.gifts)
        {
          if (gift.coin > 0)
            AnalyticsManager.TrackFreePremiumCurrencyObtain((long) gift.coin, "Gifts");
          else if (gift.gold > 0)
            AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) gift.gold, "Gifts", (string) null);
          else if (gift.num > 0 && !string.IsNullOrEmpty(gift.iname))
          {
            Debug.Log((object) (">>>>> others : " + gift.iname + ", amt : " + (object) gift.num));
            this.AnalyticsTrackItemReceived(gift.iname, gift.num);
          }
        }
      }
    }

    private void AnalyticsTrackItemReceived(string inItemName, int inAmount)
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(inItemName);
      if (itemDataByItemId != null)
      {
        if (itemDataByItemId.ItemType == EItemType.Ticket)
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) inAmount, "Gifts", (string) null);
        else
          AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Item, (long) inAmount, "Gifts", (string) null);
      }
      else
        Debug.LogWarning((object) "We have an unrecognized item ID.");
    }

    private enum ReceiveStatus
    {
      Recieve,
      NotReceiveAll,
      NotReceiveSome,
    }

    public class Json_MailRead
    {
      public Json_PlayerData player;
      public Json_Unit[] units;
      public Json_Item[] items;
      public Json_Friend[] friends;
      public Json_Artifact[] artifacts;
      public Json_Mails mails;
      public Json_Mail[] processed;
    }

    private class HaveCheckScope : IDisposable
    {
      private Dictionary<GiftTypes, int> currentNums;

      public HaveCheckScope()
      {
        this.currentNums = new Dictionary<GiftTypes, int>();
        this.currentNums.Add(GiftTypes.Artifact, MonoSingleton<GameManager>.Instance.Player.ArtifactNum);
      }

      public void AddCurrentNum(MailData mailData)
      {
        if (mailData.gifts == null)
          return;
        foreach (GiftData gift in mailData.gifts)
        {
          if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
          {
            Dictionary<GiftTypes, int> currentNums;
            GiftTypes index;
            (currentNums = this.currentNums)[index = GiftTypes.Artifact] = currentNums[index] + gift.num;
          }
        }
      }

      public bool CheckReceivable(MailData mailData)
      {
        int num = 0;
        if (mailData.gifts != null)
        {
          foreach (GiftData gift in mailData.gifts)
          {
            if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact) && gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
              num += this.currentNums[GiftTypes.Artifact] + gift.num;
          }
          if (num > (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
            return false;
        }
        return true;
      }

      public void Dispose()
      {
        this.currentNums.Clear();
      }
    }
  }
}
