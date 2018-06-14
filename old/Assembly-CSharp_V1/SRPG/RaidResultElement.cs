// Decompiled with JetBrains decompiler
// Type: SRPG.RaidResultElement
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RaidResultElement : MonoBehaviour, IFlowInterface
  {
    public Text TxtTitle;
    public Text TxtExp;
    public Text TxtGold;
    public Transform ItemParent;
    public GameObject ItemTemplate;
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger;
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval;
    private List<GameObject> mItems;
    private bool mRequest;
    private bool mFinished;
    private float mTimeScale;

    public RaidResultElement()
    {
      base.\u002Ector();
    }

    public float TimeScale
    {
      get
      {
        return this.mTimeScale;
      }
      set
      {
        this.mTimeScale = Mathf.Clamp(value, 0.1f, 10f);
      }
    }

    public void Start()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      this.ItemTemplate.SetActive(false);
    }

    public void Activated(int pinID)
    {
    }

    public bool IsRequest()
    {
      return this.mRequest;
    }

    public bool IsFinished()
    {
      return this.mFinished;
    }

    public void RequestAnimation()
    {
      RaidQuestResult dataOfClass = DataSource.FindDataOfClass<RaidQuestResult>(((Component) this).get_gameObject(), (RaidQuestResult) null);
      if (dataOfClass == null)
      {
        this.mFinished = true;
      }
      else
      {
        if (this.IsRequest() || this.IsFinished())
          return;
        this.mRequest = true;
        if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
          this.TxtTitle.set_text(string.Format(LocalizedText.Get("sys.RAID_RESULT_INDEX"), (object) (dataOfClass.index + 1)));
        if (Object.op_Inequality((Object) this.TxtExp, (Object) null))
          this.TxtExp.set_text(dataOfClass.uexp.ToString());
        if (Object.op_Inequality((Object) this.TxtGold, (Object) null))
          this.TxtGold.set_text(dataOfClass.gold.ToString());
        if (dataOfClass.drops != null)
        {
          this.mItems = new List<GameObject>(dataOfClass.drops.Length);
          for (int index = 0; index < dataOfClass.drops.Length; ++index)
          {
            if (dataOfClass.drops[index] != null)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
              gameObject.get_transform().SetParent(this.ItemParent, false);
              DataSource.Bind<ItemData>(gameObject, dataOfClass.drops[index]);
              this.mItems.Add(gameObject);
            }
          }
        }
        ((Component) this).get_gameObject().SetActive(true);
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        this.StartCoroutine(this.TreasureAnimation());
      }
    }

    [DebuggerHidden]
    private IEnumerator TreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RaidResultElement.\u003CTreasureAnimation\u003Ec__IteratorCF() { \u003C\u003Ef__this = this };
    }
  }
}
