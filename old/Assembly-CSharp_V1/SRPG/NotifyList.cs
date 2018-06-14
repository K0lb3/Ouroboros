// Decompiled with JetBrains decompiler
// Type: SRPG.NotifyList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NotifyList : MonoBehaviour
  {
    private static NotifyList mInstance;
    public RectTransform ListParent;
    public NotifyListItem Item_Generic;
    public NotifyListItem Item_LoginBonus;
    public NotifyListItem Item_Mission;
    public NotifyListItem Item_DailyMission;
    public NotifyListItem Item_ContentsUnlock;
    public NotifyListItem Item_QuestSupport;
    public NotifyListItem Item_Award;
    public string FadeTrigger;
    public float FadeTime;
    private List<NotifyListItem> mItems;
    private List<NotifyListItem> mQueue;
    public float Lifetime;
    public float Spacing;
    public float MaxHeight;
    public float Interval;
    public float FadeInterval;
    public float GroupSpan;
    private float mStackHeight;
    private float mGroupTime;
    public string[] DebugItems;

    public NotifyList()
    {
      base.\u002Ector();
    }

    public static void Push(string msg)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Generic, (Object) null))
        return;
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_Generic);
      notifyListItem.Message.set_text(msg);
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushQuestSupport(int count, int gold)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_QuestSupport, (Object) null))
        return;
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_QuestSupport);
      notifyListItem.Message.set_text(LocalizedText.Get("sys.NOTIFY_SUPPORT", (object) count, (object) gold));
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushContentsUnlock(UnlockParam unlock)
    {
      if (unlock.UnlockTarget == UnlockTargets.Tower || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || (!Object.op_Inequality((Object) NotifyList.mInstance.Item_ContentsUnlock, (Object) null) || unlock == null))
        return;
      string str = LocalizedText.Get("sys.UNLOCK_" + unlock.iname.ToUpper());
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_ContentsUnlock);
      notifyListItem.Message.set_text(LocalizedText.Get("sys.NOTIFY_CONTENTSUNLOCK", new object[1]
      {
        (object) str
      }));
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushLoginBonus(ItemData data)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_LoginBonus, (Object) null))
        return;
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_LoginBonus);
      notifyListItem.Message.set_text(LocalizedText.Get("sys.LOGBO_TODAY", new object[1]
      {
        (object) data.Param.name
      }));
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushDailyTrophy(TrophyParam trophy)
    {
      if (!Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_DailyMission, (Object) null))
        return;
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_DailyMission);
      notifyListItem.Message.set_text(LocalizedText.Get("sys.TRPYCOMP", new object[1]
      {
        (object) trophy.Name
      }));
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushTrophy(TrophyParam trophy)
    {
      if (trophy == null || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Mission, (Object) null))
        return;
      NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_Mission);
      notifyListItem.Message.set_text(LocalizedText.Get("sys.TRPYCOMP", new object[1]
      {
        (object) trophy.Name
      }));
      NotifyList.mInstance.Push(notifyListItem);
    }

    public static void PushAward(TrophyParam trophy)
    {
      if (trophy == null || !Object.op_Inequality((Object) NotifyList.mInstance, (Object) null) || !Object.op_Inequality((Object) NotifyList.mInstance.Item_Award, (Object) null))
        return;
      for (int index = 0; index < trophy.Items.Length; ++index)
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.GetAwardParam(trophy.Items[index].iname);
        if (awardParam != null)
        {
          NotifyListItem notifyListItem = (NotifyListItem) Object.Instantiate<NotifyListItem>((M0) NotifyList.mInstance.Item_Award);
          notifyListItem.Message.set_text(LocalizedText.Get("sys.AWARD_GET", new object[1]
          {
            (object) awardParam.name
          }));
          NotifyList.mInstance.Push(notifyListItem);
        }
        else
          DebugUtility.LogError("Not found trophy award. iname is [ " + trophy.Items[index].iname + " ]");
      }
    }

    private bool Push(NotifyListItem item)
    {
      if (Object.op_Equality((Object) item, (Object) null))
        return false;
      RectTransform transform = ((Component) item).get_transform() as RectTransform;
      ((Component) item).get_gameObject().SetActive(true);
      float preferredHeight = LayoutUtility.GetPreferredHeight(transform);
      ((Component) item).get_gameObject().SetActive(false);
      item.Lifetime = this.Interval;
      item.Height = preferredHeight;
      Object.DontDestroyOnLoad((Object) ((Component) item).get_gameObject());
      this.mQueue.Add(item);
      return true;
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new NotifyList.\u003CStart\u003Ec__IteratorC2() { \u003C\u003Ef__this = this };
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) NotifyList.mInstance, (Object) this))
        return;
      NotifyList.mInstance = (NotifyList) null;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) NotifyList.mInstance, (Object) null))
      {
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
      else
      {
        NotifyList.mInstance = this;
        if (Object.op_Inequality((Object) this.Item_Generic, (Object) null) && ((Component) this.Item_Generic).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_Generic).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.Item_LoginBonus, (Object) null) && ((Component) this.Item_LoginBonus).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_LoginBonus).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.Item_Mission, (Object) null) && ((Component) this.Item_Mission).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_Mission).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.Item_DailyMission, (Object) null) && ((Component) this.Item_DailyMission).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_DailyMission).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.Item_ContentsUnlock, (Object) null) && ((Component) this.Item_ContentsUnlock).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_ContentsUnlock).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.Item_QuestSupport, (Object) null) && ((Component) this.Item_QuestSupport).get_gameObject().get_activeInHierarchy())
          ((Component) this.Item_QuestSupport).get_gameObject().SetActive(false);
        Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
      }
    }

    private void Update()
    {
      float unscaledDeltaTime = Time.get_unscaledDeltaTime();
      if (this.mItems.Count > 0 || this.mQueue.Count > 0)
        this.mGroupTime += unscaledDeltaTime;
      if (this.mItems.Count > 0)
      {
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          this.mItems[index].Lifetime -= unscaledDeltaTime;
          if ((double) this.mItems[index].Lifetime <= 0.0)
          {
            if (!string.IsNullOrEmpty(this.FadeTrigger))
            {
              Animator component = (Animator) ((Component) this.mItems[index]).GetComponent<Animator>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.SetTrigger(this.FadeTrigger);
            }
            Object.Destroy((Object) ((Component) this.mItems[index]).get_gameObject(), this.FadeTime);
            this.mItems.RemoveAt(index);
            --index;
          }
        }
        if (this.mItems.Count <= 0)
        {
          this.mGroupTime = 0.0f;
          this.mStackHeight = 0.0f;
        }
      }
      if (this.mQueue.Count <= 0)
        return;
      if (this.mItems.Count == 0)
        this.mGroupTime = 0.0f;
      NotifyListItem m = this.mQueue[0];
      if ((double) this.mStackHeight + (double) this.mQueue[0].Height + (double) this.Spacing > (double) this.MaxHeight || (double) this.mGroupTime >= (double) this.GroupSpan)
        return;
      if ((double) this.mQueue[0].Lifetime > 0.0)
      {
        this.mQueue[0].Lifetime -= unscaledDeltaTime;
      }
      else
      {
        RectTransform transform = ((Component) m).get_transform() as RectTransform;
        ((Transform) transform).SetParent((Transform) this.ListParent, false);
        transform.set_anchoredPosition(new Vector2(0.0f, -this.mStackHeight));
        this.mStackHeight += m.Height + this.Spacing;
        ((Component) m).get_gameObject().SetActive(true);
        this.mItems.Add(m);
        this.mQueue.RemoveAt(0);
        for (int index = 0; index < this.mItems.Count; ++index)
          this.mItems[index].Lifetime = this.Lifetime + (float) index * this.FadeInterval;
      }
    }
  }
}
