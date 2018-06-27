// Decompiled with JetBrains decompiler
// Type: SRPG.BGSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
  {
    public float ScrollSpeed;
    public float BGWidth;
    public List<GiziScroll> SyncScrollWith;
    public string SyncScrollWithID;
    private float mScrollPos;
    private float mDesiredScrollPos;
    private Vector2 mDefaultPosition;
    private bool mResetScrollPos;
    public float DefaultScrollRatio;

    public BGSlider()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mResetScrollPos = true;
      if (this.SyncScrollWith.Count == 0 && !string.IsNullOrEmpty(this.SyncScrollWithID))
      {
        GameObject[] gameObjects = GameObjectID.FindGameObjects(this.SyncScrollWithID);
        if (gameObjects != null)
        {
          foreach (GameObject gameObject in gameObjects)
            this.SyncScrollWith.Add((GiziScroll) gameObject.GetComponent<GiziScroll>());
        }
      }
      Canvas component = (Canvas) ((Component) this).get_gameObject().GetComponent<Canvas>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Rect pixelRect = component.get_pixelRect();
      // ISSUE: explicit reference operation
      this.BGWidth = (float) ((double) ((Rect) @pixelRect).get_width() / (double) component.get_scaleFactor() + 100.0);
    }

    private void ClampScrollPos(float min, float max)
    {
      this.mScrollPos = Mathf.Clamp(this.mScrollPos, min, max);
      this.mDesiredScrollPos = Mathf.Clamp(this.mDesiredScrollPos, min, max);
    }

    private void Update()
    {
      this.mScrollPos = Mathf.Lerp(this.mScrollPos, this.mDesiredScrollPos, Time.get_deltaTime() * this.ScrollSpeed);
      Rect rect = (((Component) this).get_transform() as RectTransform).get_rect();
      // ISSUE: explicit reference operation
      float max = Mathf.Max(this.BGWidth - ((Rect) @rect).get_width(), 0.0f);
      if (this.mResetScrollPos)
      {
        this.mScrollPos = this.mDesiredScrollPos = max * this.DefaultScrollRatio;
        this.mResetScrollPos = false;
      }
      this.ClampScrollPos(0.0f, max);
      using (List<GiziScroll>.Enumerator enumerator = this.SyncScrollWith.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GiziScroll current = enumerator.Current;
          if (Object.op_Inequality((Object) current, (Object) null) && (double) max > 0.0)
            current.ScrollPos = this.mScrollPos / max;
        }
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (Object.op_Inequality((Object) eventData.get_pointerDrag(), (Object) ((Component) this).get_gameObject()))
        return;
      this.mDesiredScrollPos -= (float) eventData.get_delta().x;
      ((AbstractEventData) eventData).Use();
    }
  }
}
