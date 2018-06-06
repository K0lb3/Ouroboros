// Decompiled with JetBrains decompiler
// Type: SRPG.BattleStamp
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleStamp : MonoBehaviour
  {
    public BattleStamp.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public Sprite[] Sprites;
    public GameObject[] Prefabs;
    public string SpriteGameObjectID;
    public string SelectCursorGameObjectID;
    private List<ListItemEvents> mItems;
    private int mSelectID;

    public BattleStamp()
    {
      base.\u002Ector();
    }

    public int SelectStampID
    {
      get
      {
        return this.mSelectID;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
        if (Object.op_Equality((Object) this.ListParent, (Object) null))
          this.ListParent = ((Component) this.ItemTemplate).get_transform().get_parent() as RectTransform;
      }
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ListParent, (Object) null) || this.Sprites == null)
        return;
      for (int index1 = 0; index1 < this.Sprites.Length; ++index1)
      {
        ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ItemTemplate);
        ((Component) listItemEvents).get_gameObject().SetActive(true);
        ((Object) ((Component) listItemEvents).get_gameObject()).set_name(((Object) ((Component) listItemEvents).get_gameObject()).get_name() + ":" + index1.ToString());
        ((Component) listItemEvents).get_transform().SetParent((Transform) this.ListParent, false);
        GameObjectID[] componentsInChildren1 = (GameObjectID[]) ((Component) listItemEvents).GetComponentsInChildren<GameObjectID>();
        if (componentsInChildren1 != null)
        {
          for (int index2 = 0; index2 < componentsInChildren1.Length; ++index2)
          {
            if (componentsInChildren1[index2].ID.Equals(this.SpriteGameObjectID))
            {
              Image image = !Object.op_Equality((Object) componentsInChildren1[index2], (Object) null) ? (Image) ((Component) componentsInChildren1[index2]).get_gameObject().GetComponent<Image>() : (Image) null;
              if (Object.op_Inequality((Object) image, (Object) null))
                image.set_sprite(this.Sprites[index1]);
            }
            else if (componentsInChildren1[index2].ID.Equals(this.SelectCursorGameObjectID))
              ((Component) componentsInChildren1[index2]).get_gameObject().SetActive(index1 == this.mSelectID);
          }
        }
        this.mItems.Add(listItemEvents);
        listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
        {
          this.mSelectID = this.mItems.FindIndex((Predicate<ListItemEvents>) (it => Object.op_Equality((Object) ((Component) it).get_gameObject(), (Object) go.get_gameObject())));
          for (int index1 = 0; index1 < this.mItems.Count; ++index1)
          {
            GameObjectID[] componentsInChildren = (GameObjectID[]) ((Component) this.mItems[index1]).GetComponentsInChildren<GameObjectID>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
              {
                if (componentsInChildren[index2].ID.Equals(this.SelectCursorGameObjectID))
                  ((Component) componentsInChildren[index2]).get_gameObject().SetActive(index1 == this.mSelectID);
              }
            }
          }
          if (this.mSelectID < 0)
            return;
          Sprite sprite = this.Sprites[this.mSelectID];
          if (!Object.op_Implicit((Object) sprite) || this.OnSelectItem == null)
            return;
          this.OnSelectItem(sprite);
        });
      }
    }

    public delegate void SelectEvent(Sprite sprite);
  }
}
