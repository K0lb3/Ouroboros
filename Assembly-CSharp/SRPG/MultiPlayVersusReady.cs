// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusReady
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayVersusReady : MonoBehaviour
  {
    public TargetPlate TargetTemplate;
    public TargetPlate TargetObjTemplate;
    public TargetPlate TargetTrickTemplate;
    public GameObject TargetParent;
    public GameObject TargetMarker;
    public Button CameraRotateL;
    public Button CameraRotateR;
    public TouchController TouchController;
    public Button GoButton;
    public GameObject QuestObj;
    public bool TowerMode;
    private bool m_Ready;
    private bool m_SyncLoad;
    private QuestParam m_CurrentQuest;
    private Vector3 m_CameraPos;
    private Vector3 m_CameraRot;
    private Vector3 m_CameraNextPos;
    private int m_SelectParty;
    private List<TacticsUnitController> m_Units;
    private TacticsSceneSettings m_SceneRoot;
    private TargetPlate m_Status;
    private TargetPlate m_StatusObj;
    private TargetPlate m_StatusTrick;
    private List<BattleMap> m_Map;
    private TargetCamera m_Camera;
    private readonly float CAM_ROTATE_TIME;
    private float m_CamAngle;
    private float m_CamAngleStart;
    private float m_CamAngleGoal;
    private float m_CamRotateTime;
    private float m_CamYawMin;
    private float m_CamYawMax;
    private bool m_CamMove;
    private IntVector2 m_SelectGrid;
    private GameObject m_Marker;
    private GameObject m_TrickMarkerObj;
    private Dictionary<string, GameObject> m_TrickMarkers;
    private List<MyPhoton.MyPlayer> m_Players;

    public MultiPlayVersusReady()
    {
      base.\u002Ector();
    }

    private BattleMap CurrentMap
    {
      get
      {
        if (this.m_Map != null)
          return this.m_Map[0];
        return (BattleMap) null;
      }
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (GameUtility.IsDebugBuild)
        ((GUIEventListener) ((Component) this).get_gameObject().AddComponent<GUIEventListener>()).Listeners = new GUIEventListener.GUIEvent(this.DebugPlacement);
      this.m_SelectParty = 0;
      this.m_CurrentQuest = instance.FindQuest(GlobalVars.SelectedQuestID);
      if (this.m_CurrentQuest != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestObj, (UnityEngine.Object) null))
        DataSource.Bind<QuestParam>(this.QuestObj, this.m_CurrentQuest);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.GoButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickGo)));
      }
      this.InitTouchArea();
      this.InitStatusWindow();
      this.InitTargetMarker();
      this.StartCoroutine(this.LoadSceneAsync());
    }

    private void InitCamera()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) Camera.get_main(), (UnityEngine.Object) null))
        return;
      Camera main = Camera.get_main();
      GameSettings instance = GameSettings.Instance;
      ((Component) Camera.get_main()).get_gameObject().SetActive(true);
      RenderPipeline.Setup(Camera.get_main());
      this.m_Camera = GameUtility.RequireComponent<TargetCamera>(((Component) Camera.get_main()).get_gameObject());
      this.m_Camera.CameraDistance = instance.GameCamera_DefaultDistance;
      if (this.CurrentMap != null)
      {
        this.m_CameraPos.x = (__Null) ((double) this.CurrentMap.Width * 0.5);
        this.m_CameraPos.z = (__Null) ((double) this.CurrentMap.Height * 0.5);
        // ISSUE: explicit reference operation
        ((Vector3) @this.m_CameraRot).Set(instance.GameCamera_AngleX, -instance.GameCamera_YawMin, 0.0f);
      }
      ((Component) main).get_transform().set_position(this.m_CameraPos);
      ((Component) main).get_transform().set_rotation(Quaternion.Euler(this.m_CameraRot));
      main.set_fieldOfView(instance.GameCamera_TacticsSceneFOV);
      this.m_CamAngle = instance.GameCamera_YawMin;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CameraRotateL, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CameraRotateL.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCameraRotateL)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CameraRotateR, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CameraRotateR.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCameraRotateR)));
      }
      this.UpdateCameraPosition();
    }

    private void InitStatusWindow()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTemplate, (UnityEngine.Object) null))
      {
        this.m_Status = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.TargetTemplate);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Status, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetParent, (UnityEngine.Object) null))
            ((Component) this.m_Status).get_gameObject().get_transform().SetParent(((Component) this).get_transform(), false);
          this.m_Status.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
          this.m_Status.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetObjTemplate, (UnityEngine.Object) null))
      {
        this.m_StatusObj = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.TargetObjTemplate);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_StatusObj, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetParent, (UnityEngine.Object) null))
            ((Component) this.m_StatusObj).get_gameObject().get_transform().SetParent(((Component) this).get_transform(), false);
          this.m_StatusObj.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
          this.m_StatusObj.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetTrickTemplate, (UnityEngine.Object) null))
        return;
      this.m_StatusTrick = (TargetPlate) UnityEngine.Object.Instantiate<TargetPlate>((M0) this.TargetTrickTemplate);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_StatusTrick, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetParent, (UnityEngine.Object) null))
        ((Component) this.m_StatusTrick).get_gameObject().get_transform().SetParent(((Component) this).get_transform(), false);
      this.m_StatusTrick.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
      this.m_StatusTrick.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
    }

    private void InitTouchArea()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TouchController, (UnityEngine.Object) null))
        return;
      this.TouchController.OnClick = new TouchController.ClickEvent(this.OnTouchClick);
    }

    private void InitMap()
    {
      for (int index = 0; index < this.m_CurrentQuest.map.Count; ++index)
      {
        BattleMap battleMap = new BattleMap();
        if (!battleMap.Initialize((BattleCore) null, this.m_CurrentQuest.map[index]))
          break;
        this.m_Map.Add(battleMap);
      }
    }

    private void InitTargetMarker()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetMarker, (UnityEngine.Object) null))
        return;
      this.m_Marker = UnityEngine.Object.Instantiate((UnityEngine.Object) this.TargetMarker, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
      this.m_Marker.get_gameObject().SetActive(false);
      GameUtility.SetLayer(this.m_Marker, GameUtility.LayerUI, true);
    }

    private List<Unit> LoadMultiTower()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      List<Unit> unitList = new List<Unit>();
      if (this.m_Players == null)
        this.m_Players = new List<MyPhoton.MyPlayer>((IEnumerable<MyPhoton.MyPlayer>) roomPlayerList);
      for (int index1 = 0; index1 < roomPlayerList.Count; ++index1)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index1].json);
        if (photonPlayerParam != null)
        {
          for (int index2 = 0; index2 < photonPlayerParam.units.Length; ++index2)
          {
            if (photonPlayerParam.units[index2] != null && photonPlayerParam.units[index2].sub == 0)
            {
              UnitData unitdata = new UnitData();
              if (unitdata != null)
              {
                unitdata.Deserialize(photonPlayerParam.units[index2].unitJson);
                DownloadUtility.DownloadUnit(unitdata.UnitParam, (JobData[]) null);
                Unit unit = new Unit();
                if (unit != null && unit.Setup(unitdata, this.CurrentMap.PartyUnitSettings[photonPlayerParam.units[index2].place], (Unit.DropItem) null, (Unit.DropItem) null))
                {
                  unit.OwnerPlayerIndex = photonPlayerParam.playerIndex;
                  unitList.Add(unit);
                }
              }
            }
          }
        }
      }
      int count = this.CurrentMap.NPCUnitSettings.Count;
      List<NPCSetting> npcUnitSettings = this.CurrentMap.NPCUnitSettings;
      for (int index = 0; index < count; ++index)
      {
        DownloadUtility.DownloadUnit(npcUnitSettings[index]);
        Unit unit = new Unit();
        if (unit.Setup((UnitData) null, (UnitSetting) npcUnitSettings[index], (Unit.DropItem) null, (Unit.DropItem) null))
          unitList.Add(unit);
      }
      return unitList;
    }

    private List<Unit> LoadVersusParty()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData party = player.Partys[7];
      List<Unit> unitList = new List<Unit>();
      for (int index = 0; index < party.MAX_UNIT; ++index)
      {
        long unitUniqueId = party.GetUnitUniqueID(index);
        if (unitUniqueId != 0L)
        {
          UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
          DownloadUtility.DownloadUnit(unitDataByUniqueId.UnitParam, (JobData[]) null);
          Unit unit = new Unit();
          int versusPlacement = player.GetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index);
          unit.Setup(unitDataByUniqueId, this.CurrentMap.PartyUnitSettings[versusPlacement], (Unit.DropItem) null, (Unit.DropItem) null);
          unitList.Add(unit);
        }
      }
      return unitList;
    }

    [DebuggerHidden]
    private IEnumerator LoadSceneAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MultiPlayVersusReady.\u003CLoadSceneAsync\u003Ec__Iterator104() { \u003C\u003Ef__this = this };
    }

    private void Update()
    {
      if (!this.m_Ready)
        return;
      this.UpdateCamera();
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!this.TowerMode || this.m_SyncLoad || !instance.IsUpdatePlayerProperty)
        return;
      instance.IsUpdatePlayerProperty = false;
      this.SyncRoomPlayer();
    }

    private void UpdateCamera()
    {
      this.UpdateCameraRotate();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TouchController, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) Camera.get_main(), (UnityEngine.Object) null) && !this.m_CamMove)
      {
        Camera main = Camera.get_main();
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @this.TouchController.Velocity).get_magnitude() > 0.0)
        {
          Vector2 velocity = this.TouchController.Velocity;
          Vector3 right = ((Component) main).get_transform().get_right();
          Vector3 forward = ((Component) main).get_transform().get_forward();
          right.y = (__Null) 0.0;
          forward.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          ((Vector3) @right).Normalize();
          // ISSUE: explicit reference operation
          ((Vector3) @forward).Normalize();
          Vector3 screenPoint = main.WorldToScreenPoint(this.m_CameraPos);
          Vector2 vector2_1 = Vector2.op_Implicit(Vector3.op_Subtraction(main.WorldToScreenPoint(Vector3.op_Addition(Vector3.op_Addition(this.m_CameraPos, right), forward)), screenPoint));
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @velocity;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (__Null) ((^local1).x / (double) Mathf.Abs((float) vector2_1.x));
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local2 = @velocity;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).y = (__Null) ((^local2).y / (double) Mathf.Abs((float) vector2_1.y));
          Vector2 vector2_2;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).\u002Ector((float) (right.x * velocity.x + forward.x * velocity.y), (float) (right.z * velocity.x + forward.z * velocity.y));
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local3 = @this.m_CameraPos;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local3).x = (^local3).x - vector2_2.x;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local4 = @this.m_CameraPos;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local4).z = (^local4).z - vector2_2.y;
          if (this.CurrentMap != null)
          {
            this.m_CameraPos.x = (__Null) (double) Mathf.Clamp((float) this.m_CameraPos.x, 0.1f, (float) this.CurrentMap.Width - 0.1f);
            this.m_CameraPos.z = (__Null) (double) Mathf.Clamp((float) this.m_CameraPos.z, 0.1f, (float) this.CurrentMap.Height - 0.1f);
          }
          this.TouchController.Velocity = Vector2.get_zero();
          this.UpdateCameraPosition();
          this.m_CameraNextPos = this.m_CameraPos;
        }
        else
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.m_CameraPos, this.m_CameraNextPos);
          // ISSUE: explicit reference operation
          if ((double) ((Vector3) @vector3).get_magnitude() > 0.00999999977648258)
          {
            this.m_CameraPos = Vector3.Lerp(this.m_CameraPos, this.m_CameraNextPos, 0.1f);
            this.UpdateCameraPosition();
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CameraRotateL, (UnityEngine.Object) null))
        ((Selectable) this.CameraRotateL).set_interactable((double) this.m_CamAngle > (double) this.m_CamYawMin);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CameraRotateR, (UnityEngine.Object) null))
        return;
      ((Selectable) this.CameraRotateR).set_interactable((double) this.m_CamAngle < (double) this.m_CamYawMax);
    }

    private void UpdateCameraRotate()
    {
      if (!this.m_CamMove)
        return;
      GameSettings instance = GameSettings.Instance;
      this.m_CamRotateTime += Time.get_deltaTime();
      if ((double) this.m_CamRotateTime > (double) this.CAM_ROTATE_TIME)
      {
        this.m_CamRotateTime = this.CAM_ROTATE_TIME;
        this.m_CamMove = false;
      }
      this.m_CamAngle = Mathf.Lerp(this.m_CamAngleStart, this.m_CamAngleGoal, Mathf.Sin((float) (1.57079637050629 * ((double) this.m_CamRotateTime / (double) this.CAM_ROTATE_TIME))));
      if ((double) this.m_CamAngle < (double) instance.GameCamera_YawMin)
        this.m_CamAngle = instance.GameCamera_YawMin;
      else if ((double) this.m_CamAngle > (double) instance.GameCamera_YawMax)
        this.m_CamAngle = instance.GameCamera_YawMax;
      this.UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
      GameSettings instance = GameSettings.Instance;
      this.m_CameraPos.y = (__Null) (double) GameUtility.CalcHeight((float) this.m_CameraPos.x, (float) this.m_CameraPos.z);
      this.m_Camera.Pitch = (float) this.m_CameraRot.x;
      this.m_Camera.Yaw = (float) this.m_CameraRot.y;
      this.m_Camera.SetPositionYaw(Vector3.op_Addition(this.m_CameraPos, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), this.m_CamAngle);
    }

    private void SyncRoomPlayer()
    {
      bool flag1 = false;
      bool flag2 = false;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
      if (this.m_Players == null)
        flag2 = true;
      else if (roomPlayerList.Count != this.m_Players.Count)
      {
        flag2 = true;
      }
      else
      {
        for (int index1 = 0; index1 < roomPlayerList.Count; ++index1)
        {
          if (!roomPlayerList[index1].json.Equals(this.m_Players[index1].json))
          {
            JSON_MyPhotonPlayerParam photonPlayerParam1 = JSON_MyPhotonPlayerParam.Parse(this.m_Players[index1].json);
            JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index1].json);
            if (photonPlayerParam2 != null && photonPlayerParam1 != null && photonPlayerParam2.playerIndex != instance.MyPlayerIndex)
            {
              if (photonPlayerParam2.units.Length != photonPlayerParam1.units.Length)
              {
                flag2 = true;
              }
              else
              {
                for (int index2 = 0; index2 < photonPlayerParam2.units.Length; ++index2)
                {
                  UnitData unitData1 = new UnitData();
                  UnitData unitData2 = new UnitData();
                  unitData1.Deserialize(photonPlayerParam2.units[index2].unitJson);
                  unitData2.Deserialize(photonPlayerParam1.units[index2].unitJson);
                  if (unitData1.UnitParam.iname != unitData2.UnitParam.iname)
                    flag2 = true;
                  else if (photonPlayerParam2.units[index2].place != photonPlayerParam1.units[index2].place)
                    flag1 = true;
                }
              }
            }
          }
        }
      }
      this.m_Players = roomPlayerList;
      if (flag2)
      {
        this.CloseUnitStatus();
        this.m_SyncLoad = true;
        this.StartCoroutine(this.LoadUnit());
      }
      else if (flag1)
      {
        for (int index1 = 0; index1 < roomPlayerList.Count; ++index1)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MultiPlayVersusReady.\u003CSyncRoomPlayer\u003Ec__AnonStorey34E playerCAnonStorey34E = new MultiPlayVersusReady.\u003CSyncRoomPlayer\u003Ec__AnonStorey34E();
          // ISSUE: reference to a compiler-generated field
          playerCAnonStorey34E.param = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index1].json);
          // ISSUE: reference to a compiler-generated field
          if (playerCAnonStorey34E.param != null)
          {
            // ISSUE: reference to a compiler-generated field
            for (int index2 = 0; index2 < playerCAnonStorey34E.param.units.Length; ++index2)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              MultiPlayVersusReady.\u003CSyncRoomPlayer\u003Ec__AnonStorey34D playerCAnonStorey34D = new MultiPlayVersusReady.\u003CSyncRoomPlayer\u003Ec__AnonStorey34D();
              // ISSUE: reference to a compiler-generated field
              playerCAnonStorey34D.\u003C\u003Ef__ref\u0024846 = playerCAnonStorey34E;
              // ISSUE: reference to a compiler-generated field
              playerCAnonStorey34D.unitData = new UnitData();
              // ISSUE: reference to a compiler-generated field
              if (playerCAnonStorey34D.unitData != null)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                playerCAnonStorey34D.unitData.Deserialize(playerCAnonStorey34E.param.units[index2].unitJson);
                // ISSUE: reference to a compiler-generated method
                TacticsUnitController controller = this.m_Units.Find(new Predicate<TacticsUnitController>(playerCAnonStorey34D.\u003C\u003Em__3AF));
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
                {
                  // ISSUE: reference to a compiler-generated field
                  OIntVector2 pos = this.CurrentMap.PartyUnitSettings[playerCAnonStorey34E.param.units[index2].place].pos;
                  controller.Unit.x = (int) pos.x;
                  controller.Unit.y = (int) pos.y;
                  this.CalcPosition(controller);
                }
              }
            }
          }
        }
      }
      this.UpdateGridColor();
    }

    [DebuggerHidden]
    private IEnumerator LoadUnit()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MultiPlayVersusReady.\u003CLoadUnit\u003Ec__Iterator105() { \u003C\u003Ef__this = this };
    }

    private void OnLoadScene(GameObject go)
    {
      this.m_SceneRoot = (TacticsSceneSettings) go.GetComponent<TacticsSceneSettings>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SceneRoot, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) Camera.get_main(), (UnityEngine.Object) null))
      {
        RenderPipeline component = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.BackgroundImage = (Texture) this.m_SceneRoot.BackgroundImage;
          component.ScreenFilter = (Texture) this.m_SceneRoot.ScreenFilter;
        }
        if (this.m_SceneRoot.OverrideCameraSettings)
        {
          this.m_CamYawMin = this.m_SceneRoot.CameraYawMin;
          this.m_CamYawMax = this.m_SceneRoot.CameraYawMax;
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          this.m_CamYawMin = instance.GameCamera_YawMin;
          this.m_CamYawMax = instance.GameCamera_YawMax;
        }
        this.m_SceneRoot.GenerateGridMesh(this.CurrentMap.Width, this.CurrentMap.Height);
      }
      go.SetActive(true);
    }

    private void OnDestroy()
    {
      if (this.CurrentMap == null)
        return;
      AssetManager.UnloadScene(this.CurrentMap.MapSceneName);
    }

    private void UpdateUnitStatus(int add, bool lerp = false)
    {
      this.m_SelectParty += add;
      if (this.m_SelectParty < 0)
        this.m_SelectParty = this.m_Units.Count - 1;
      else if (this.m_SelectParty >= this.m_Units.Count)
        this.m_SelectParty = 0;
      int selectParty = this.m_SelectParty;
      int hp = (int) this.m_Units[selectParty].Unit.CurrentStatus.param.hp;
      if (this.m_Units[selectParty].Unit.IsGimmick && !this.m_Units[selectParty].Unit.IsBreakObj)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Status, (UnityEngine.Object) null))
          this.m_Status.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_StatusTrick, (UnityEngine.Object) null))
          this.m_StatusTrick.Close();
        this.m_StatusObj.SetNoAction(this.m_Units[selectParty].Unit, (List<LogSkill.Target.CondHit>) null);
        this.m_StatusObj.Open();
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_StatusObj, (UnityEngine.Object) null))
          this.m_StatusObj.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_StatusTrick, (UnityEngine.Object) null))
          this.m_StatusTrick.Close();
        this.m_Status.SetNoAction(this.m_Units[selectParty].Unit, (List<LogSkill.Target.CondHit>) null);
        this.m_Status.SetHpGaugeParam(EUnitSide.Player, hp, hp, 0, 0);
        this.m_Status.Open();
        this.m_Status.UpdateHpGauge();
        this.m_Status.HideButton();
      }
      this.UpdateMarker(this.m_Units[selectParty]);
      if (lerp)
      {
        this.m_CameraNextPos = this.m_CameraPos;
        this.m_CameraNextPos.x = ((Component) this.m_Units[selectParty]).get_transform().get_position().x;
        this.m_CameraNextPos.z = ((Component) this.m_Units[selectParty]).get_transform().get_position().z;
      }
      else
      {
        this.m_CameraPos.x = ((Component) this.m_Units[selectParty]).get_transform().get_position().x;
        this.m_CameraPos.z = ((Component) this.m_Units[selectParty]).get_transform().get_position().z;
        this.m_CameraNextPos = this.m_CameraPos;
      }
      this.UpdateCameraPosition();
    }

    private void CloseUnitStatus()
    {
      this.m_Status.Close();
      this.UpdateMarker((TacticsUnitController) null);
    }

    private void OnNextUnit(GameObject obj)
    {
      if (this.m_SyncLoad)
        return;
      this.UpdateUnitStatus(1, false);
      MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0.0f);
    }

    private void OnPrevUnit(GameObject obj)
    {
      if (this.m_SyncLoad)
        return;
      this.UpdateUnitStatus(-1, false);
      MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0.0f);
    }

    private void OnTouchClick(Vector2 screenPos)
    {
      RaycastHit raycastHit;
      if (this.m_SyncLoad || !UnityEngine.Object.op_Inequality((UnityEngine.Object) Camera.get_main(), (UnityEngine.Object) null) || (this.CurrentMap == null || !Physics.Raycast(Camera.get_main().ScreenPointToRay(Vector2.op_Implicit(screenPos)), ref raycastHit)))
        return;
      // ISSUE: explicit reference operation
      this.m_SelectGrid.x = Mathf.FloorToInt((float) ((RaycastHit) @raycastHit).get_point().x);
      // ISSUE: explicit reference operation
      this.m_SelectGrid.y = Mathf.FloorToInt((float) ((RaycastHit) @raycastHit).get_point().z);
      this.UpdateSelectGrid();
    }

    private List<int> CheckExistUnit(int x, int y)
    {
      List<int> intList = new List<int>();
      for (int index = 0; index < this.m_Units.Count; ++index)
      {
        TacticsUnitController unit = this.m_Units[index];
        if (unit.Unit.x == x && unit.Unit.y == y)
          intList.Add(index);
      }
      return intList;
    }

    private void UpdateSelectGrid()
    {
      if (this.m_SelectGrid.x < 0 || this.m_SelectGrid.y < 0 || !this.m_Ready)
        return;
      if (this.m_SelectParty < 0)
      {
        for (int index = 0; index < this.m_Units.Count; ++index)
        {
          TacticsUnitController unit = this.m_Units[index];
          if (unit.Unit.x == this.m_SelectGrid.x && unit.Unit.y == this.m_SelectGrid.y)
          {
            this.m_SelectParty = index;
            break;
          }
        }
        if (this.m_SelectParty < 0)
          return;
        this.UpdateUnitStatus(0, false);
      }
      else
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        List<int> intList = this.CheckExistUnit(this.m_SelectGrid.x, this.m_SelectGrid.y);
        if (intList.Count > 0)
        {
          int num = intList[0];
          for (int index = 0; index < intList.Count; ++index)
          {
            if (this.m_Units[intList[index]].Unit.OwnerPlayerIndex == instance.MyPlayerIndex)
            {
              num = intList[index];
              break;
            }
          }
          this.m_SelectParty = num;
          this.UpdateUnitStatus(0, true);
          MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0005", 0.0f);
        }
        else
        {
          if (this.TowerMode && (this.m_Units[this.m_SelectParty].Unit.Side != EUnitSide.Player || this.m_Units[this.m_SelectParty].Unit.OwnerPlayerIndex != instance.MyPlayerIndex))
            return;
          using (List<UnitSetting>.Enumerator enumerator = this.CurrentMap.PartyUnitSettings.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              UnitSetting current = enumerator.Current;
              if (this.CheckExistUnit((int) current.pos.x, (int) current.pos.y).Count <= 0 && (int) current.pos.x == this.m_SelectGrid.x && (int) current.pos.y == this.m_SelectGrid.y)
              {
                this.m_Units[this.m_SelectParty].Unit.x = (int) current.pos.x;
                this.m_Units[this.m_SelectParty].Unit.y = (int) current.pos.y;
                this.CalcPosition(this.m_Units[this.m_SelectParty]);
                this.UpdateUnitStatus(0, true);
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0.0f);
                if (!this.TowerMode)
                  break;
                this.SendPlacementInfo();
                break;
              }
            }
          }
        }
      }
    }

    private void CalcPosition(TacticsUnitController controller)
    {
      Vector3 vector3;
      // ISSUE: explicit reference operation
      ((Vector3) @vector3).\u002Ector((float) controller.Unit.x + 0.5f, 100f, (float) controller.Unit.y + 0.5f);
      ((Component) controller).get_gameObject().get_transform().set_position(vector3);
    }

    private void UpdateMarker(TacticsUnitController controller = null)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Marker, (UnityEngine.Object) null))
      {
        this.m_Marker.get_transform().SetParent(((Component) controller).get_transform(), false);
        this.m_Marker.get_transform().set_localPosition(Vector3.op_Multiply(Vector3.get_up(), 1.5f));
        this.m_Marker.get_gameObject().SetActive(true);
      }
      else
      {
        this.m_Marker.get_gameObject().SetActive(false);
        this.m_Marker.get_transform().SetParent(((Component) this).get_transform(), false);
      }
    }

    private void OnCameraRotateL()
    {
      if (this.m_SyncLoad || this.m_CamMove)
        return;
      GameSettings instance = GameSettings.Instance;
      this.m_CamAngleStart = this.m_CamAngle;
      this.m_CamAngleGoal = Mathf.Max(this.m_CamAngle - instance.GameCamera_YawSoftLimit, this.m_CamYawMin);
      this.m_CamRotateTime = 0.0f;
      this.m_CamMove = true;
    }

    private void OnCameraRotateR()
    {
      if (this.m_SyncLoad || this.m_CamMove)
        return;
      GameSettings instance = GameSettings.Instance;
      this.m_CamAngleStart = this.m_CamAngle;
      this.m_CamAngleGoal = Mathf.Min(this.m_CamAngle + instance.GameCamera_YawSoftLimit, this.m_CamYawMax);
      this.m_CamRotateTime = 0.0f;
      this.m_CamMove = true;
    }

    private int GetPlacementID(int x, int y)
    {
      int num = 0;
      if (this.CurrentMap != null)
      {
        for (int index = 0; index < this.CurrentMap.PartyUnitSettings.Count; ++index)
        {
          if ((int) this.CurrentMap.PartyUnitSettings[index].pos.x == x && (int) this.CurrentMap.PartyUnitSettings[index].pos.y == y)
          {
            num = index;
            break;
          }
        }
      }
      return num;
    }

    private bool IsSamePosition()
    {
      for (int index1 = 0; index1 < this.m_Units.Count; ++index1)
      {
        for (int index2 = index1 + 1; index2 < this.m_Units.Count; ++index2)
        {
          if (this.m_Units[index1].Unit.x == this.m_Units[index2].Unit.x && this.m_Units[index1].Unit.y == this.m_Units[index2].Unit.y)
            return true;
        }
      }
      return false;
    }

    private void OnClickGo()
    {
      if (this.m_SyncLoad)
        return;
      if (this.IsSamePosition())
      {
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "SAME_POSITION");
      }
      else
      {
        this.m_Ready = false;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        for (int index = 0; index < this.m_Units.Count; ++index)
          player.SetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index, this.GetPlacementID(this.m_Units[index].Unit.x, this.m_Units[index].Unit.y));
        player.SavePlayerPrefs();
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_PLACEMENT");
      }
    }

    private void SendPlacementInfo()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MultiPlayVersusReady.\u003CSendPlacementInfo\u003Ec__AnonStorey34F infoCAnonStorey34F = new MultiPlayVersusReady.\u003CSendPlacementInfo\u003Ec__AnonStorey34F();
      // ISSUE: reference to a compiler-generated field
      infoCAnonStorey34F.pt = PunMonoSingleton<MyPhoton>.Instance;
      // ISSUE: reference to a compiler-generated field
      MyPhoton.MyPlayer myPlayer = infoCAnonStorey34F.pt.GetMyPlayer();
      if (myPlayer == null)
        return;
      // ISSUE: reference to a compiler-generated field
      infoCAnonStorey34F.param = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
      // ISSUE: reference to a compiler-generated field
      if (infoCAnonStorey34F.param.units != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MultiPlayVersusReady.\u003CSendPlacementInfo\u003Ec__AnonStorey350 infoCAnonStorey350 = new MultiPlayVersusReady.\u003CSendPlacementInfo\u003Ec__AnonStorey350();
        // ISSUE: reference to a compiler-generated field
        infoCAnonStorey350.\u003C\u003Ef__ref\u0024847 = infoCAnonStorey34F;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (infoCAnonStorey350.i = 0; infoCAnonStorey350.i < infoCAnonStorey34F.param.units.Length; ++infoCAnonStorey350.i)
        {
          // ISSUE: reference to a compiler-generated method
          TacticsUnitController tacticsUnitController = this.m_Units.Find(new Predicate<TacticsUnitController>(infoCAnonStorey350.\u003C\u003Em__3B0));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null))
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            infoCAnonStorey34F.param.units[infoCAnonStorey350.i].place = this.GetPlacementID(tacticsUnitController.Unit.x, tacticsUnitController.Unit.y);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) infoCAnonStorey350.i, infoCAnonStorey34F.param.units[infoCAnonStorey350.i].place, false);
          }
        }
      }
      PlayerPrefsUtility.Save();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      infoCAnonStorey34F.pt.SetMyPlayerParam(infoCAnonStorey34F.param.Serialize());
    }

    private void UpdateGridColor()
    {
      if (this.CurrentMap == null)
        return;
      GameSettings instance1 = GameSettings.Instance;
      GridMap<Color32> grid = new GridMap<Color32>(this.CurrentMap.Width, this.CurrentMap.Height);
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (this.CurrentMap.PartyUnitSettings != null)
      {
        using (List<UnitSetting>.Enumerator enumerator = this.CurrentMap.PartyUnitSettings.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            UnitSetting current = enumerator.Current;
            grid.set((int) current.pos.x, (int) current.pos.y, Color32.op_Implicit(instance1.Colors.WalkableArea));
          }
        }
      }
      using (List<TacticsUnitController>.Enumerator enumerator = this.m_Units.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TacticsUnitController current = enumerator.Current;
          if (current.Unit.OwnerPlayerIndex != instance2.MyPlayerIndex)
            grid.set(current.Unit.x, current.Unit.y, Color32.op_Implicit(instance1.Colors.Helper));
        }
      }
      if (this.CurrentMap.NPCUnitSettings != null)
      {
        using (List<NPCSetting>.Enumerator enumerator = this.CurrentMap.NPCUnitSettings.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NPCSetting current = enumerator.Current;
            grid.set((int) current.pos.x, (int) current.pos.y, Color32.op_Implicit(instance1.Colors.Enemy));
          }
        }
      }
      this.m_SceneRoot.ShowGridLayer(0, grid, true);
    }

    private string DebugSearchPos(int x, int y)
    {
      List<UnitSetting> partyUnitSettings = this.CurrentMap.PartyUnitSettings;
      List<UnitSetting> arenaUnitSettings = this.CurrentMap.ArenaUnitSettings;
      string str = string.Empty;
      if (partyUnitSettings != null)
      {
        for (int index = 0; index < partyUnitSettings.Count; ++index)
        {
          if (x == (int) partyUnitSettings[index].pos.x && y == (int) partyUnitSettings[index].pos.y)
          {
            str = "p" + string.Format("{0:D2}", (object) index);
            break;
          }
        }
      }
      if (string.IsNullOrEmpty(str) && arenaUnitSettings != null)
      {
        for (int index = 0; index < arenaUnitSettings.Count; ++index)
        {
          if (x == (int) arenaUnitSettings[index].pos.x && y == (int) arenaUnitSettings[index].pos.y)
          {
            str = "e" + string.Format("{0:D2}", (object) index);
            break;
          }
        }
      }
      if (string.IsNullOrEmpty(str))
        str = "   ";
      return str;
    }

    private void DebugPlacement(GameObject go)
    {
      if (this.m_Map == null || this.m_Map.Count == 0)
        return;
      int width = this.CurrentMap.Width;
      int height = this.CurrentMap.Height;
      GUILayout.Box(string.Empty, new GUILayoutOption[2]
      {
        GUILayout.Width(400f),
        GUILayout.Height(500f)
      });
      GUILayout.BeginArea(new Rect(20f, 30f, 400f, 500f));
      GUILayout.BeginHorizontal(new GUILayoutOption[1]
      {
        GUILayout.Width(300f)
      });
      GUILayout.Label("┌", new GUILayoutOption[0]);
      GUILayout.Space(-10f);
      for (int index = 0; index < width; ++index)
      {
        if (index != 0)
        {
          GUILayout.Label("┬", new GUILayoutOption[0]);
          GUILayout.Space(-10f);
        }
        GUILayout.Label("─", new GUILayoutOption[0]);
        GUILayout.Space(-10f);
      }
      GUILayout.Label("┐", new GUILayoutOption[0]);
      GUILayout.EndHorizontal();
      for (int y = 0; y < height; ++y)
      {
        GUILayout.Space(-10f);
        GUILayout.BeginHorizontal(new GUILayoutOption[1]
        {
          GUILayout.Width(300f)
        });
        for (int x = 0; x < width; ++x)
        {
          GUILayout.Label("│", new GUILayoutOption[0]);
          GUILayout.Space(-10f);
          GUILayout.Label(this.DebugSearchPos(x, y), new GUILayoutOption[0]);
          GUILayout.Space(-10f);
        }
        GUILayout.Label("│", new GUILayoutOption[0]);
        GUILayout.EndHorizontal();
      }
      GUILayout.Space(-10f);
      GUILayout.BeginHorizontal(new GUILayoutOption[1]
      {
        GUILayout.Width(300f)
      });
      GUILayout.Label("└", new GUILayoutOption[0]);
      GUILayout.Space(-10f);
      for (int index = 0; index < width; ++index)
      {
        if (index != 0)
        {
          GUILayout.Label("┴", new GUILayoutOption[0]);
          GUILayout.Space(-10f);
        }
        GUILayout.Label("─", new GUILayoutOption[0]);
        GUILayout.Space(-10f);
      }
      GUILayout.Label("┘", new GUILayoutOption[0]);
      GUILayout.EndHorizontal();
      GUILayout.EndArea();
    }
  }
}
