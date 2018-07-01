// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReadMail2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReadMail2", 32741)]
  [FlowNode.Pin(0, "開封", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(5, "ユニット引き換え券", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "アイテム引き換え券", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(7, "武具引き換え券", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(8, "念装引き換え券", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(20, "一件受け取り完了", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "一括受け取り完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "いくつか受け取れなかった", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(23, "何も受け取れなかった", FlowNode.PinTypes.Output, 23)]
  public class FlowNode_ReadMail2 : FlowNode_Network
  {
    private const int INPUT_OPEN_MAIL = 0;
    private const int INPUT_OPEN_SELECT_UNIT_SUMMON_TICKET = 5;
    private const int INPUT_OPEN_SELECT_ITEM_SUMMON_TICKET = 6;
    private const int INPUT_OPEN_SELECT_ARTIFACT_SUMMON_TICKET = 7;
    private const int INPUT_OPEN_SELECT_CONCEPT_CARD_SUMMON_TICKET = 8;
    private const int OUTPUT_RECEIVED_MAIL = 20;
    private const int OUTPUT_RECEIVED_MAILS = 21;
    private const int OUTPUT_RECEIVED_SOME = 22;
    private const int OUTPUT_NOT_RECEIVED_ANYTHING = 23;
    private FlowNode_ReadMail2.ReceiveStatus mReceiveStatus;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          MailWindow.MailReadRequestData dataOfClass = DataSource.FindDataOfClass<MailWindow.MailReadRequestData>(((Component) this).get_gameObject(), (MailWindow.MailReadRequestData) null);
          if (Network.Mode == Network.EConnectMode.Offline)
          {
            this.ActivateOutputLinks(21);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          List<MailData> currentMails = MonoSingleton<GameManager>.Instance.Player.CurrentMails;
          List<MailData> mailDataList = new List<MailData>();
          List<long> ids = new List<long>((IEnumerable<long>) dataOfClass.mailIDs);
          List<MailData> all = currentMails.FindAll((Predicate<MailData>) (md =>
          {
            if (!ids.Contains(md.mid))
              return false;
            ids.Remove(md.mid);
            return true;
          }));
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
        case 8:
          string str = GlobalVars.UnlockUnitID;
          if (pinID == 5)
          {
            str = GlobalVars.UnlockUnitID;
            if (!string.IsNullOrEmpty(str))
              GlobalVars.UnitGetReward = new UnitGetParam(MonoSingleton<GameManager>.Instance.GetItemParam(str));
          }
          else if (pinID == 6)
            str = GlobalVars.ItemSelectListItemData.iiname;
          else if (pinID == 7)
            str = GlobalVars.ArtifactListItem.iname;
          else if (pinID == 8)
          {
            str = GetConceptCardListWindow.GetSelectedConceptCard();
            GetConceptCardListWindow.ClearSelectedConceptCard();
          }
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
        if (giftData.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
        {
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(giftData.ConceptCardIname);
          rewardData.AddReward(conceptCardParam, giftData.ConceptCardNum);
          if (giftData.IsGetConceptCardUnit)
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(giftData.ConceptCardGetUnitIname);
            rewardData.AddReward(itemParam, 1);
          }
        }
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

    private void SetRewordData(MailData[] mail_datas)
    {
      if (mail_datas == null || mail_datas.Length <= 0)
        return;
      RewardData rewardData1 = new RewardData();
      for (int index = 0; index < mail_datas.Length; ++index)
      {
        MailData mailData = mail_datas[index];
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
          case Network.EErrCode.Gift_ConceptCardBoxLimit:
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
          if (jsonObject.body.mails != null)
            GlobalVars.ConceptCardNum.Set(jsonObject.body.mails.concept_count);
          if (jsonObject.body.processed != null)
          {
            if (jsonObject.body.processed.Length > 0)
            {
              MailData[] mail_datas = new MailData[jsonObject.body.processed.Length];
              for (int index1 = 0; index1 < mail_datas.Length; ++index1)
              {
                mail_datas[index1] = new MailData();
                mail_datas[index1].Deserialize(jsonObject.body.processed[index1]);
                if (mail_datas[index1].Contains(GiftTypes.ConceptCard))
                {
                  GlobalVars.IsDirtyConceptCardData.Set(true);
                  for (int index2 = 0; index2 < mail_datas[index1].gifts.Length; ++index2)
                  {
                    string conceptCardIname = mail_datas[index1].gifts[index2].ConceptCardIname;
                    if (mail_datas[index1].gifts[index2].IsGetConceptCardUnit)
                      FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardIname));
                  }
                }
              }
              this.SetRewordData(mail_datas);
            }
          }
        }
        catch (Exception ex)
        {
          GlobalVars.UnitGetReward = (UnitGetParam) null;
          DebugUtility.LogException(ex);
          return;
        }
        if (GlobalVars.LastReward != null && GlobalVars.LastReward.Get() != null)
          MonoSingleton<GameManager>.Instance.Player.OnGoldChange(GlobalVars.LastReward.Get().Gold);
        ((Behaviour) this).set_enabled(false);
        this.Success();
      }
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
        this.currentNums.Add(GiftTypes.ConceptCard, GlobalVars.ConceptCardNum.Get());
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
          if (gift.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
          {
            Dictionary<GiftTypes, int> currentNums;
            GiftTypes index;
            (currentNums = this.currentNums)[index = GiftTypes.ConceptCard] = currentNums[index] + gift.ConceptCardNum;
          }
        }
      }

      public bool CheckReceivable(MailData mailData)
      {
        int num1 = 0;
        int num2 = 0;
        bool flag = true;
        if (mailData.gifts != null)
        {
          foreach (GiftData gift in mailData.gifts)
          {
            if (gift.CheckGiftTypeIncluded(GiftTypes.Artifact))
              num1 += this.currentNums[GiftTypes.Artifact] + gift.num;
            if (gift.CheckGiftTypeIncluded(GiftTypes.ConceptCard))
            {
              num2 += this.currentNums[GiftTypes.ConceptCard] + gift.ConceptCardNum;
              if (gift.conceptCard != null)
              {
                ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(gift.conceptCard.iname);
                if (conceptCardParam != null && conceptCardParam.type != eCardType.Equipment)
                  flag = false;
              }
            }
          }
          if (num1 > (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap || num2 > (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax && flag)
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
