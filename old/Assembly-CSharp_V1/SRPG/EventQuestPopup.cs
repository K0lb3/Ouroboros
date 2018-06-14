// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestPopup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("System/EventQuestPopup", 32741)]
  [FlowNode.Pin(101, "戻る", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "アンロック", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(2, "キャンセル", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "決定", FlowNode.PinTypes.Input, 0)]
  public class EventQuestPopup : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemLayout;
    public GameObject ItemTemplate;
    public Text Message;
    public SRPG_Button BtnDecide;
    public SRPG_Button BtnCancel;
    private ChapterParam Chapter;

    public EventQuestPopup()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.Chapter != null && this.Chapter.keys.Count > 0)
          {
            KeyItem key = this.Chapter.keys[0];
            ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(key.iname);
            if (itemDataByItemId == null || itemDataByItemId.Num < key.num)
            {
              UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_OUTOFKEY"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 101)), (GameObject) null, false, -1);
              break;
            }
            if (!this.Chapter.IsDateUnlock(Network.GetServerTime()))
            {
              UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 101)), (GameObject) null, false, -1);
              break;
            }
          }
          GlobalVars.KeyQuestTimeOver = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          GlobalVars.KeyQuestTimeOver = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
      }
    }

    private void Awake()
    {
    }

    private void Start()
    {
      this.Chapter = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
      if (this.Chapter == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
          this.ItemTemplate.SetActive(false);
        if (Object.op_Inequality((Object) this.Message, (Object) null))
        {
          string str = (string) null;
          ItemParam itemParam = (ItemParam) null;
          int num = 0;
          if (this.Chapter.keys.Count > 0)
          {
            KeyItem key = this.Chapter.keys[0];
            itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(key.iname);
            num = key.num;
          }
          KeyQuestTypes keyQuestType = this.Chapter.GetKeyQuestType();
          bool keyQuestTimeOver = GlobalVars.KeyQuestTimeOver;
          switch (keyQuestType)
          {
            case KeyQuestTypes.Timer:
              if (this.Chapter.keytime != 0L && itemParam != null)
              {
                TimeSpan timeSpan = TimeManager.FromUnixTime(this.Chapter.keytime) - TimeManager.FromUnixTime(0L);
                if (timeSpan.TotalDays >= 1.0)
                {
                  str = LocalizedText.Get(!keyQuestTimeOver ? "sys.KEYQUEST_UNLCOK_TIMER_D" : "sys.KEYQUEST_TIMEOVER_D", (object) itemParam.name, (object) num, (object) timeSpan.Days);
                  break;
                }
                if (timeSpan.TotalHours >= 1.0)
                {
                  str = LocalizedText.Get(!keyQuestTimeOver ? "sys.KEYQUEST_UNLCOK_TIMER_H" : "sys.KEYQUEST_TIMEOVER_H", (object) itemParam.name, (object) num, (object) timeSpan.Hours);
                  break;
                }
                str = LocalizedText.Get(!keyQuestTimeOver ? "sys.KEYQUEST_UNLCOK_TIMER_M" : "sys.KEYQUEST_TIMEOVER_M", (object) itemParam.name, (object) num, (object) Mathf.Max(timeSpan.Minutes, 0));
                break;
              }
              break;
            case KeyQuestTypes.Count:
              str = LocalizedText.Get(!keyQuestTimeOver ? "sys.KEYQUEST_UNLCOK_COUNT" : "sys.KEYQUEST_TIMEOVER_COUNT", (object) itemParam.name, (object) num);
              break;
          }
          this.Message.set_text(str);
        }
        this.Refresh();
      }
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemLayout, (Object) null))
        return;
      for (int index = 0; index < this.Chapter.keys.Count; ++index)
      {
        KeyItem key = this.Chapter.keys[index];
        if (key != null && !string.IsNullOrEmpty(key.iname) && key.num != 0)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(key.iname);
          ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          DataSource.Bind<ChapterParam>(gameObject, this.Chapter);
          DataSource.Bind<ItemParam>(gameObject, itemParam);
          DataSource.Bind<ItemData>(gameObject, itemDataByItemParam);
          DataSource.Bind<KeyItem>(gameObject, key);
          KeyQuestBanner component = (KeyQuestBanner) gameObject.GetComponent<KeyQuestBanner>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.UpdateValue();
          gameObject.get_transform().SetParent(this.ItemLayout.get_transform(), false);
          gameObject.SetActive(true);
        }
      }
    }
  }
}
