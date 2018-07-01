// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnObject3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/配置2(2D)", "シーンにオブジェクトを配置します。", 5592405, 4473992)]
  public class Event2dAction_SpawnObject3 : EventAction
  {
    private Vector2 pvt = new Vector2(0.5f, 0.5f);
    public const string ResourceDir = "Event2dAssets/";
    [StringIsResourcePathPopup(typeof (GameObject), "Event2dAssets/")]
    public string ResourceID;
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    [HideInInspector]
    public bool Persistent;
    [HideInInspector]
    public Vector2 Position;
    private GameObject mGO;
    public Event2dAction_SpawnObject3.SiblingOrder Order;
    [HideInInspector]
    public string CharaID;
    [HideInInspector]
    public Event2dAction_SpawnObject3.ActorChildOrder ChildOrder;
    private RectTransform rectTransform;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SpawnObject3.\u003CPreloadAssets\u003Ec__IteratorA7()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && Object.op_Inequality(this.mResourceLoadRequest.asset, (Object) null))
      {
        GameObject asset = this.mResourceLoadRequest.asset as GameObject;
        RectTransform component = (RectTransform) asset.GetComponent<RectTransform>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.pvt = component.get_pivot();
        this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.get_zero(), asset.get_transform().get_rotation()) as GameObject;
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.Root)
        {
          if (this.Persistent && Object.op_Inequality((Object) TacticsSceneSettings.Instance, (Object) null))
            this.mGO.get_transform().SetParent(((Component) TacticsSceneSettings.Instance).get_transform(), true);
        }
        else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.ChildOfActor)
        {
          if (!string.IsNullOrEmpty(this.CharaID))
          {
            EventStandCharaController2 instances = EventStandCharaController2.FindInstances(this.CharaID);
            if (Object.op_Inequality((Object) instances, (Object) null))
            {
              this.mGO.get_transform().SetParent(((Component) instances).get_gameObject().get_transform(), true);
              if (this.ChildOrder == Event2dAction_SpawnObject3.ActorChildOrder.Over)
                this.mGO.get_transform().SetAsLastSibling();
              else
                this.mGO.get_transform().SetAsFirstSibling();
              this.rectTransform = (RectTransform) this.mGO.GetComponent<RectTransform>();
              if (Object.op_Inequality((Object) this.rectTransform, (Object) null))
              {
                this.rectTransform.set_pivot(this.pvt);
                this.rectTransform.set_anchoredPosition(Vector2.get_zero());
                RectTransform rectTransform = this.rectTransform;
                Vector2 vector2_1 = this.convertPosition(this.Position);
                this.rectTransform.set_anchorMax(vector2_1);
                Vector2 vector2_2 = vector2_1;
                rectTransform.set_anchorMin(vector2_2);
              }
            }
          }
        }
        else
        {
          float num1 = 0.0f;
          for (int index = 0; index < ((Component) this.ActiveCanvas).get_transform().get_childCount(); ++index)
          {
            Transform child = ((Component) this.ActiveCanvas).get_transform().GetChild(index);
            if (Object.op_Inequality((Object) ((Component) child).GetComponent<EventDialogBubbleCustom>(), (Object) null))
            {
              num1 = (float) ((Component) child).get_transform().get_position().z;
              break;
            }
          }
          this.mGO.get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), true);
          this.mGO.get_transform().SetAsLastSibling();
          if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnDialog)
          {
            Vector3 position = this.mGO.get_transform().get_position();
            position.z = (__Null) ((double) num1 - 1.0);
            this.mGO.get_transform().set_position(position);
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnStandChara)
          {
            int num2 = -1;
            for (int index = 0; index < ((Component) this.ActiveCanvas).get_transform().get_childCount(); ++index)
            {
              if (Object.op_Inequality((Object) ((Component) ((Component) this.ActiveCanvas).get_transform().GetChild(index)).GetComponent<EventDialogBubbleCustom>(), (Object) null))
              {
                num2 = index;
                break;
              }
            }
            if (num2 > 0)
              this.mGO.get_transform().SetSiblingIndex(num2);
            if ((double) num1 < -1.0)
            {
              Vector3 position = this.mGO.get_transform().get_position();
              position.z = (__Null) ((double) num1 + 1.0);
              this.mGO.get_transform().set_position(position);
            }
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnBackGround)
          {
            int num2 = -1;
            for (int index = 0; index < ((Component) this.ActiveCanvas).get_transform().get_childCount(); ++index)
            {
              if (Object.op_Inequality((Object) ((Component) ((Component) this.ActiveCanvas).get_transform().GetChild(index)).GetComponent<EventStandCharaController2>(), (Object) null))
              {
                num2 = index;
                break;
              }
            }
            if (num2 > 0)
              this.mGO.get_transform().SetSiblingIndex(num2);
          }
          this.rectTransform = (RectTransform) this.mGO.GetComponent<RectTransform>();
          if (Object.op_Inequality((Object) this.rectTransform, (Object) null))
          {
            this.rectTransform.set_pivot(this.pvt);
            this.rectTransform.set_anchoredPosition(Vector2.get_zero());
            RectTransform rectTransform = this.rectTransform;
            Vector2 vector2_1 = this.convertPosition(this.Position);
            this.rectTransform.set_anchorMax(vector2_1);
            Vector2 vector2_2 = vector2_1;
            rectTransform.set_anchorMin(vector2_2);
          }
        }
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGO, (Object) null) || this.Persistent && !Object.op_Equality((Object) this.mGO.get_transform().get_parent(), (Object) null))
        return;
      Object.Destroy((Object) this.mGO);
    }

    private Vector2 convertPosition(Vector2 pos)
    {
      return Vector2.Scale(Vector2.op_Addition(pos, Vector2.get_one()), new Vector2(0.5f, 0.5f));
    }

    public enum SiblingOrder
    {
      Root,
      OnDialog,
      OnStandChara,
      OnBackGround,
      ChildOfActor,
    }

    public enum ActorChildOrder
    {
      Over,
      Under,
    }
  }
}
