// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_TouchInputModule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [AddComponentMenu("Event/Touch Input Module (SRPG)")]
  public class SRPG_TouchInputModule : PointerInputModule
  {
    private static int mLockCount;
    public GameObject TouchEffect;
    private GameObject[] mTouchEffectPool;
    private int mNumActiveTouchEffects;
    private bool mTouchEffectPoolInitialized;
    public SRPG_TouchInputModule.OnDoubleTapDelegate OnDoubleTap;
    private float mDoubleTap1stReleasedTime;
    private readonly int BUTTON_INDEX_MAX;
    private int pressing_button_index;
    public static bool IsMultiTouching;
    private Vector2 m_LastMousePosition;
    private Vector2 m_MousePosition;
    [SerializeField]
    private bool m_AllowActivationOnStandalone;
    private int mPrimaryFingerID;

    public SRPG_TouchInputModule()
    {
      base.\u002Ector();
    }

    public static void LockInput()
    {
      ++SRPG_TouchInputModule.mLockCount;
      ((Behaviour) EventSystem.get_current().get_currentInputModule()).set_enabled(SRPG_TouchInputModule.mLockCount == 0);
    }

    public static void UnlockInput(bool forceReset = false)
    {
      if (forceReset)
        SRPG_TouchInputModule.mLockCount = 0;
      else
        --SRPG_TouchInputModule.mLockCount;
      ((Behaviour) EventSystem.get_current().get_currentInputModule()).set_enabled(SRPG_TouchInputModule.mLockCount == 0);
    }

    private bool IsHandling { get; set; }

    private void InitTouchEffects()
    {
      if (this.mTouchEffectPoolInitialized)
        return;
      if (Object.op_Inequality((Object) this.TouchEffect, (Object) null))
      {
        for (int index = 0; index < this.mTouchEffectPool.Length; ++index)
        {
          this.mTouchEffectPool[index] = (GameObject) Object.Instantiate<GameObject>((M0) this.TouchEffect);
          this.mTouchEffectPool[index].SetActive(false);
          this.mTouchEffectPool[index].get_transform().SetParent(((Component) UIUtility.ParticleCanvas).get_transform(), false);
        }
      }
      this.mTouchEffectPoolInitialized = true;
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      UIUtility.InitParticleCanvas();
      this.InitTouchEffects();
      this.OnDoubleTap += (SRPG_TouchInputModule.OnDoubleTapDelegate) (position => {});
    }

    public bool allowActivationOnStandalone
    {
      get
      {
        return this.m_AllowActivationOnStandalone;
      }
      set
      {
        this.m_AllowActivationOnStandalone = value;
      }
    }

    public virtual void UpdateModule()
    {
      this.m_LastMousePosition = this.m_MousePosition;
      this.m_MousePosition = Vector2.op_Implicit(Input.get_mousePosition());
    }

    public virtual bool IsModuleSupported()
    {
      if (!this.m_AllowActivationOnStandalone)
        return Application.get_isMobilePlatform();
      return true;
    }

    public virtual bool ShouldActivateModule()
    {
      if (!((BaseInputModule) this).ShouldActivateModule())
        return false;
      if (this.UseFakeInput())
      {
        int num1 = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ? (true ? 1 : 0) : (Input.GetMouseButtonDown(2) ? 1 : 0);
        Vector2 vector2 = Vector2.op_Subtraction(this.m_MousePosition, this.m_LastMousePosition);
        // ISSUE: explicit reference operation
        int num2 = (double) ((Vector2) @vector2).get_sqrMagnitude() > 0.0 ? 1 : 0;
        return (num1 | num2) != 0;
      }
      for (int index = 0; index < Input.get_touchCount(); ++index)
      {
        Touch touch = Input.GetTouch(index);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if (((Touch) @touch).get_phase() == null || ((Touch) @touch).get_phase() == 1 || ((Touch) @touch).get_phase() == 2)
          return true;
      }
      return false;
    }

    private bool UseFakeInput()
    {
      return !Application.get_isMobilePlatform();
    }

    public virtual void Process()
    {
      this.IsHandling = false;
      if (Application.get_platform() == 7 || Application.get_platform() == 2 || (Application.get_platform() == null || Application.get_platform() == 1))
        this.SendUpdateEventToSelectedObject();
      if (!((Behaviour) this).get_enabled())
        return;
      if (this.UseFakeInput())
        this.FakeTouches();
      else
        this.ProcessTouchEvents();
      if (this.IsHandling || !Input.GetKeyDown((KeyCode) 27))
        return;
      BackHandler.Invoke();
      Input.ResetInputAxes();
    }

    private bool SendUpdateEventToSelectedObject()
    {
      if (Object.op_Equality((Object) ((BaseInputModule) this).get_eventSystem().get_currentSelectedGameObject(), (Object) null))
        return false;
      BaseEventData baseEventData = ((BaseInputModule) this).GetBaseEventData();
      ExecuteEvents.Execute<IUpdateSelectedHandler>(((BaseInputModule) this).get_eventSystem().get_currentSelectedGameObject(), baseEventData, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_updateSelectedHandler());
      return ((AbstractEventData) baseEventData).get_used();
    }

    private PointerEventData GetMousePointerEvent(int index)
    {
      int num = new int[3]{ -1, -2, -3 }[index];
      bool mouseButtonDown = Input.GetMouseButtonDown(num);
      PointerEventData pointerEventData;
      bool pointerData = this.GetPointerData(num, ref pointerEventData, true);
      ((AbstractEventData) pointerEventData).Reset();
      if (pointerData)
        pointerEventData.set_position(Vector2.op_Implicit(Input.get_mousePosition()));
      Vector2 vector2 = Vector2.op_Implicit(Input.get_mousePosition());
      pointerEventData.set_delta(Vector2.op_Subtraction(vector2, pointerEventData.get_position()));
      pointerEventData.set_position(vector2);
      pointerEventData.set_scrollDelta(Input.get_mouseScrollDelta());
      ((BaseInputModule) this).get_eventSystem().RaycastAll(pointerEventData, (List<RaycastResult>) ((BaseInputModule) this).m_RaycastResultCache);
      RaycastResult firstRaycast = BaseInputModule.FindFirstRaycast((List<RaycastResult>) ((BaseInputModule) this).m_RaycastResultCache);
      pointerEventData.set_pointerCurrentRaycast(firstRaycast);
      if (mouseButtonDown)
        pointerEventData.set_delta(Vector2.get_zero());
      return pointerEventData;
    }

    private void FakeTouches()
    {
      bool flag = false;
      bool pressed = false;
      bool released = false;
      if (this.pressing_button_index <= -1)
      {
        for (int index = 0; index < this.BUTTON_INDEX_MAX; ++index)
        {
          if (Input.GetMouseButtonDown(index))
          {
            this.pressing_button_index = index;
            pressed = true;
            break;
          }
        }
      }
      for (int index = 0; index < this.BUTTON_INDEX_MAX; ++index)
      {
        if (Input.GetMouseButtonUp(index) && this.pressing_button_index == index)
        {
          this.pressing_button_index = -1;
          released = true;
          break;
        }
      }
      this.IsHandling = pressed || released;
      PointerEventData buttonData1 = (PointerEventData) this.GetMousePointerEventData().GetButtonState((PointerEventData.InputButton) 0).get_eventData().buttonData;
      this.ProcessTouchPress(buttonData1, pressed, released);
      if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
      {
        this.IsHandling = true;
        this.ProcessMove(buttonData1);
        this.ProcessDrag(buttonData1);
        RaycastResult pointerPressRaycast1 = buttonData1.get_pointerPressRaycast();
        // ISSUE: explicit reference operation
        if (((RaycastResult) @pointerPressRaycast1).get_isValid())
        {
          PointerEventData buttonData2 = (PointerEventData) this.GetMousePointerEventData().GetButtonState((PointerEventData.InputButton) 1).get_eventData().buttonData;
          if (Input.GetMouseButtonDown(1))
            buttonData2.set_pointerPressRaycast(buttonData2.get_pointerCurrentRaycast());
          if (Input.GetMouseButton(1))
          {
            RaycastResult pointerPressRaycast2 = buttonData1.get_pointerPressRaycast();
            // ISSUE: explicit reference operation
            GameObject gameObject1 = ((RaycastResult) @pointerPressRaycast2).get_gameObject();
            RaycastResult pointerPressRaycast3 = buttonData2.get_pointerPressRaycast();
            // ISSUE: explicit reference operation
            GameObject gameObject2 = ((RaycastResult) @pointerPressRaycast3).get_gameObject();
            flag = Object.op_Equality((Object) gameObject1, (Object) gameObject2);
          }
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = flag;
    }

    private void ProcessTouchEvents()
    {
      List<GameObject> gameObjectList1 = new List<GameObject>();
      for (int index = 0; index < Input.get_touchCount(); ++index)
      {
        Touch touch = Input.GetTouch(index);
        bool pressed;
        bool released;
        PointerEventData pointerEventData = this.GetTouchPointerEventData(touch, ref pressed, ref released);
        if (this.mPrimaryFingerID == -1 && pressed)
        {
          // ISSUE: explicit reference operation
          this.mPrimaryFingerID = ((Touch) @touch).get_fingerId();
        }
        // ISSUE: explicit reference operation
        if (this.mPrimaryFingerID == ((Touch) @touch).get_fingerId())
        {
          this.ProcessTouchPress(pointerEventData, pressed, released);
          if (!released)
          {
            List<GameObject> gameObjectList2 = gameObjectList1;
            RaycastResult pointerPressRaycast = pointerEventData.get_pointerPressRaycast();
            // ISSUE: explicit reference operation
            GameObject gameObject = ((RaycastResult) @pointerPressRaycast).get_gameObject();
            gameObjectList2.Add(gameObject);
            this.ProcessMove(pointerEventData);
            this.ProcessDrag(pointerEventData);
          }
          else
            this.mPrimaryFingerID = -1;
        }
        else if (pressed)
          pointerEventData.set_pointerPressRaycast(pointerEventData.get_pointerCurrentRaycast());
        else if (!released)
        {
          List<GameObject> gameObjectList2 = gameObjectList1;
          RaycastResult pointerPressRaycast = pointerEventData.get_pointerPressRaycast();
          // ISSUE: explicit reference operation
          GameObject gameObject = ((RaycastResult) @pointerPressRaycast).get_gameObject();
          gameObjectList2.Add(gameObject);
        }
        if (released)
          this.RemovePointerData(pointerEventData);
      }
      SRPG_TouchInputModule.IsMultiTouching = false;
      if (this.mPrimaryFingerID == -1 || gameObjectList1.Count < 2)
        return;
      for (int index1 = 0; index1 < gameObjectList1.Count; ++index1)
      {
        if (Object.op_Equality((Object) gameObjectList1[index1], (Object) null))
          return;
        for (int index2 = index1 + 1; index2 < gameObjectList1.Count; ++index2)
        {
          if (Object.op_Inequality((Object) gameObjectList1[index1], (Object) gameObjectList1[index2]))
            return;
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = true;
    }

    private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
    {
      RaycastResult pointerCurrentRaycast = pointerEvent.get_pointerCurrentRaycast();
      // ISSUE: explicit reference operation
      GameObject gameObject1 = ((RaycastResult) @pointerCurrentRaycast).get_gameObject();
      if (pressed)
      {
        pointerEvent.set_eligibleForClick(true);
        pointerEvent.set_delta(Vector2.get_zero());
        pointerEvent.set_pressPosition(pointerEvent.get_position());
        pointerEvent.set_pointerPressRaycast(pointerEvent.get_pointerCurrentRaycast());
        if (Object.op_Inequality((Object) pointerEvent.get_pointerEnter(), (Object) gameObject1))
        {
          ((BaseInputModule) this).HandlePointerExitAndEnter(pointerEvent, gameObject1);
          pointerEvent.set_pointerEnter(gameObject1);
        }
        GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject1, (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_pointerDownHandler());
        if (Object.op_Equality((Object) gameObject2, (Object) null))
          gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
        if (Object.op_Inequality((Object) gameObject2, (Object) pointerEvent.get_pointerPress()))
        {
          pointerEvent.set_pointerPress(gameObject2);
          pointerEvent.set_rawPointerPress(gameObject1);
          pointerEvent.set_clickCount(0);
        }
        pointerEvent.set_pointerDrag(ExecuteEvents.GetEventHandler<IDragHandler>(gameObject1));
        if (Object.op_Inequality((Object) pointerEvent.get_pointerDrag(), (Object) null))
          ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.get_pointerDrag(), (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_beginDragHandler());
        ((BaseInputModule) this).get_eventSystem().SetSelectedGameObject(ExecuteEvents.GetEventHandler<ISelectHandler>(gameObject1), (BaseEventData) pointerEvent);
      }
      if (!released)
        return;
      float unscaledTime1 = Time.get_unscaledTime();
      if ((double) this.mDoubleTap1stReleasedTime < 0.0)
        this.mDoubleTap1stReleasedTime = unscaledTime1;
      else if ((double) unscaledTime1 - (double) this.mDoubleTap1stReleasedTime >= 0.300000011920929)
      {
        this.mDoubleTap1stReleasedTime = unscaledTime1;
      }
      else
      {
        this.OnDoubleTap(pointerEvent.get_position());
        this.mDoubleTap1stReleasedTime = -1f;
      }
      ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.get_pointerPress(), (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_pointerUpHandler());
      GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
      if (Object.op_Equality((Object) pointerEvent.get_pointerPress(), (Object) eventHandler) && pointerEvent.get_eligibleForClick())
      {
        float unscaledTime2 = Time.get_unscaledTime();
        if ((double) unscaledTime2 - (double) pointerEvent.get_clickTime() < 0.300000011920929)
        {
          PointerEventData pointerEventData = pointerEvent;
          pointerEventData.set_clickCount(pointerEventData.get_clickCount() + 1);
        }
        else
          pointerEvent.set_clickCount(1);
        pointerEvent.set_clickTime(unscaledTime2);
        ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.get_pointerPress(), (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_pointerClickHandler());
        this.SpawnTouchEffect(pointerEvent.get_position());
      }
      else if (Object.op_Inequality((Object) pointerEvent.get_pointerDrag(), (Object) null))
        ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject1, (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_dropHandler());
      pointerEvent.set_eligibleForClick(false);
      pointerEvent.set_pointerPress((GameObject) null);
      pointerEvent.set_rawPointerPress((GameObject) null);
      if (Object.op_Inequality((Object) pointerEvent.get_pointerDrag(), (Object) null))
        ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.get_pointerDrag(), (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_endDragHandler());
      pointerEvent.set_pointerDrag((GameObject) null);
      ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.get_pointerEnter(), (BaseEventData) pointerEvent, (ExecuteEvents.EventFunction<M0>) ExecuteEvents.get_pointerExitHandler());
      pointerEvent.set_pointerEnter((GameObject) null);
    }

    public virtual void DeactivateModule()
    {
      ((BaseInputModule) this).DeactivateModule();
      this.ClearSelection();
    }

    public virtual string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(!this.UseFakeInput() ? "Input: Touch" : "Input: Faked");
      if (this.UseFakeInput())
      {
        PointerEventData pointerEventData1 = this.GetLastPointerEventData(-1);
        if (pointerEventData1 != null)
          stringBuilder.AppendLine(pointerEventData1.ToString());
        PointerEventData pointerEventData2 = this.GetLastPointerEventData(-2);
        if (pointerEventData2 != null)
          stringBuilder.AppendLine(pointerEventData2.ToString());
        PointerEventData pointerEventData3 = this.GetLastPointerEventData(-3);
        if (pointerEventData3 != null)
          stringBuilder.AppendLine(pointerEventData3.ToString());
      }
      else
      {
        using (Dictionary<int, PointerEventData>.Enumerator enumerator = ((Dictionary<int, PointerEventData>) this.m_PointerData).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<int, PointerEventData> current = enumerator.Current;
            stringBuilder.AppendLine(current.ToString());
          }
        }
      }
      return stringBuilder.ToString();
    }

    private void SpawnTouchEffect(Vector2 position)
    {
      if (!this.mTouchEffectPoolInitialized)
        this.InitTouchEffects();
      for (int index = 0; index < this.mTouchEffectPool.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mTouchEffectPool[index], (Object) null) && !this.mTouchEffectPool[index].get_activeSelf())
        {
          GameObject gameObject = this.mTouchEffectPool[index];
          RectTransform transform = gameObject.get_transform() as RectTransform;
          Vector2 vector2;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(((Transform) transform).get_parent() as RectTransform, position, (Camera) null, ref vector2);
          transform.set_anchoredPosition(vector2);
          gameObject.SetActive(true);
          ++this.mNumActiveTouchEffects;
          break;
        }
      }
    }

    private void UpdateTouchEffects()
    {
      for (int index1 = 0; index1 < this.mTouchEffectPool.Length; ++index1)
      {
        if (this.mTouchEffectPool[index1].get_activeSelf())
        {
          bool flag = false;
          UIParticleSystem[] componentsInChildren = (UIParticleSystem[]) this.mTouchEffectPool[index1].GetComponentsInChildren<UIParticleSystem>();
          for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
          {
            if (componentsInChildren[index2].IsAlive())
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
              componentsInChildren[index2].ResetParticleSystem();
            this.mTouchEffectPool[index1].SetActive(false);
            --this.mNumActiveTouchEffects;
          }
        }
      }
    }

    private void Update()
    {
      if (this.mNumActiveTouchEffects <= 0)
        return;
      this.UpdateTouchEffects();
    }

    public delegate void OnDoubleTapDelegate(Vector2 position);
  }
}
