// Decompiled with JetBrains decompiler
// Type: SRPG.AwardList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(102, "ResetAward", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "SelectEnd", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Select", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "StartUp", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "IsSelectAward", FlowNode.PinTypes.Input, 1)]
  public class AwardList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject AwardListRoot;
    [SerializeField]
    private Text Pager;
    [SerializeField]
    private SRPG_Button Prev;
    [SerializeField]
    private SRPG_Button Next;
    private List<GameObject> mAwardItems;
    private GameManager gm;
    private AwardParam[] mAwards;
    private List<string> mOpenAwards;
    private int mMaxViewItems;
    private int mCurrentPage;
    private int mMaxPage;
    private bool IsRefresh;

    public AwardList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (Object.op_Equality((Object) this.gm, (Object) null))
          {
            DebugUtility.LogError("gm is null");
            break;
          }
          List<AwardParam> awardParamList = new List<AwardParam>((IEnumerable<AwardParam>) this.gm.MasterParam.GetAllAwards());
          awardParamList.Insert(0, this.CreateRemoveAwardData());
          this.mAwards = awardParamList.ToArray();
          if (this.mAwards == null || this.mAwards.Length <= 0)
          {
            DebugUtility.LogError("AwardParams is Null or Not Data!");
            break;
          }
          this.mMaxPage = this.mAwards.Length % this.mMaxViewItems <= 0 ? this.mAwards.Length / this.mMaxViewItems : this.mAwards.Length / this.mMaxViewItems + 1;
          this.mCurrentPage = 0;
          this.IsRefresh = true;
          break;
        case 1:
          this.IsRefresh = true;
          break;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.AwardListRoot, (Object) null))
      {
        int childCount = this.AwardListRoot.get_transform().get_childCount();
        this.mAwardItems = new List<GameObject>();
        if (childCount > 0)
        {
          for (int index = 0; index < childCount; ++index)
          {
            Transform child = this.AwardListRoot.get_transform().GetChild(index);
            if (Object.op_Inequality((Object) child, (Object) null))
            {
              ((Component) child).get_gameObject().SetActive(false);
              this.mAwardItems.Add(((Component) child).get_gameObject());
            }
          }
        }
        this.mMaxViewItems = childCount;
      }
      this.gm = MonoSingleton<GameManager>.Instance;
      if (Object.op_Inequality((Object) this.Prev, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.Prev.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnPrevPage)));
      }
      if (!Object.op_Inequality((Object) this.Next, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.Next.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnNextPage)));
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (!this.IsRefresh)
        return;
      this.IsRefresh = false;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.mAwardItems == null || this.mAwardItems.Count <= 0 || (this.mAwards == null || this.mAwards.Length <= 0))
        return;
      using (List<GameObject>.Enumerator enumerator = this.mAwardItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetActive(false);
      }
      int length = this.mAwards.Length;
      int num1 = this.mMaxViewItems * this.mCurrentPage;
      int num2 = num1 >= length ? 0 : num1;
      int num3 = this.mMaxViewItems * (this.mCurrentPage + 1);
      int num4 = num3 >= length ? length : num3;
      int index1 = num2;
      for (int index2 = 0; index2 < this.mAwardItems.Count; ++index2)
      {
        AwardParam awardParam = index1 >= this.mAwards.Length ? (AwardParam) null : this.mAwards[index1];
        if (awardParam != null)
        {
          GameObject mAwardItem = this.mAwardItems[index2];
          if (Object.op_Inequality((Object) mAwardItem, (Object) null))
          {
            AwardListItem component1 = (AwardListItem) mAwardItem.GetComponent<AwardListItem>();
            if (Object.op_Inequality((Object) component1, (Object) null))
            {
              mAwardItem.SetActive(true);
              mAwardItem.SetActive(true);
              bool hasItem = this.mOpenAwards != null && this.mOpenAwards.Contains(awardParam.iname);
              component1.SetUp(awardParam.iname, hasItem, awardParam.iname == this.gm.Player.SelectedAward, awardParam.grade <= 0);
              Button component2 = (Button) mAwardItem.GetComponent<SRPG_Button>();
              if (Object.op_Inequality((Object) component2, (Object) null))
              {
                ((UnityEventBase) component2.get_onClick()).RemoveAllListeners();
                if (awardParam.grade <= 0)
                {
                  // ISSUE: object of a compiler-generated type is created
                  // ISSUE: method pointer
                  ((UnityEvent) component2.get_onClick()).AddListener(new UnityAction((object) new AwardList.\u003CRefresh\u003Ec__AnonStorey231()
                  {
                    \u003C\u003Ef__this = this,
                    iname = awardParam.iname
                  }, __methodptr(\u003C\u003Em__23F)));
                  ((Selectable) component2).set_interactable(true);
                }
                else
                {
                  if (hasItem)
                  {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: method pointer
                    ((UnityEvent) component2.get_onClick()).AddListener(new UnityAction((object) new AwardList.\u003CRefresh\u003Ec__AnonStorey232()
                    {
                      \u003C\u003Ef__this = this,
                      iname = awardParam.iname
                    }, __methodptr(\u003C\u003Em__240)));
                  }
                  ((Selectable) component2).set_interactable(hasItem);
                }
              }
            }
          }
          if (index1 <= num4)
            ++index1;
        }
      }
      if (Object.op_Inequality((Object) this.Pager, (Object) null))
        this.Pager.set_text(LocalizedText.Get("sys.TEXT_PAGER_TEMP", (object) (this.mCurrentPage + 1).ToString(), (object) this.mMaxPage.ToString()));
      if (!Object.op_Inequality((Object) this.Prev, (Object) null) || !Object.op_Inequality((Object) this.Next, (Object) null))
        return;
      ((Selectable) this.Prev).set_interactable(this.mCurrentPage - 1 >= 0);
      ((Selectable) this.Next).set_interactable(this.mCurrentPage + 1 < this.mMaxPage);
    }

    private void OnNextPage()
    {
      this.mCurrentPage = Mathf.Min(this.mCurrentPage + 1, this.mMaxPage);
      this.Refresh();
    }

    private void OnPrevPage()
    {
      this.mCurrentPage = Mathf.Max(this.mCurrentPage - 1, 0);
      this.Refresh();
    }

    private AwardParam CreateRemoveAwardData()
    {
      return new AwardParam() { grade = -1, iname = string.Empty, name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD") };
    }

    private void OnSelected(string select = "")
    {
      if (!string.IsNullOrEmpty(select))
      {
        FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
    }

    public void SetOpenAwards(string[] awards)
    {
      if (awards == null || awards.Length <= 0)
        return;
      this.mOpenAwards = new List<string>(awards.Length);
      for (int index = 0; index < awards.Length; ++index)
      {
        if (!string.IsNullOrEmpty(awards[index]))
          this.mOpenAwards.Add(awards[index]);
      }
    }
  }
}
