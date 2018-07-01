// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLevelUpConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class UnitLevelUpConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private RectTransform ItemLayoutParent;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Button DecideButton;
    private List<GameObject> mExpItems;
    public UnitLevelUpConfirmWindow.ConfirmDecideEvent OnDecideEvent;
    private Dictionary<string, int> mSelectItems;

    public UnitLevelUpConfirmWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
    }

    private void Start()
    {
    }

    public void Refresh(Dictionary<string, int> dict)
    {
      if (dict == null || dict.Count < 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = dict.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ItemParam itemParam = instance.MasterParam.GetItemParam(current);
          if (itemParam != null && dict[current] > 0)
          {
            ItemData data = new ItemData();
            data.Setup(0L, itemParam, dict[current]);
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
            gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
            DataSource.Bind<ItemData>(gameObject, data);
            this.mExpItems.Add(gameObject);
            gameObject.SetActive(true);
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent != null)
        this.OnDecideEvent();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    public delegate void ConfirmDecideEvent();
  }
}
