// Decompiled with JetBrains decompiler
// Type: SRPG.BattleMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class BattleMap
  {
    public static readonly int MAX_GRID_WIDTH = 20;
    public static readonly int MAX_GRID_HEIGHT = 20;
    public static readonly int MAX_GRID_MOVING = 16;
    public static readonly int MAP_FLOOR_HEIGHT = 2;
    private static int[] ADJ_OFFSETS = new int[8]
    {
      1,
      0,
      0,
      1,
      -1,
      0,
      0,
      -1
    };
    private List<UnitSubSetting> mPartyUnitSubSettings = new List<UnitSubSetting>();
    private QuestMonitorCondition mWinMonitorCondition = new QuestMonitorCondition();
    private QuestMonitorCondition mLoseMonitorCondition = new QuestMonitorCondition();
    private List<TrickSetting> mTrickSettings = new List<TrickSetting>();
    private List<Grid> mMoveRoutes = new List<Grid>(BattleMap.MAX_GRID_MOVING);
    private Grid[] mCheckGrids = new Grid[4];
    public string MapSceneName;
    public string BattleSceneName;
    public string EventSceneName;
    public string BGMName;
    private List<UnitSetting> mPartyUnitSettings;
    private List<NPCSetting> mNPCUnitSettings;
    private List<UnitSetting> mArenaUnitSettings;
    private List<JSON_GimmickEvent> mGimmickEvents;
    private BattleCore mBattle;
    private int mWidth;
    private int mHeight;
    private Grid[,] mGrid;
    private BattleMapRoot mRoot;
    private int mMoveStep;
    public RandDeckResult[] mRandDeckResult;

    public List<UnitSetting> PartyUnitSettings
    {
      get
      {
        return this.mPartyUnitSettings;
      }
    }

    public List<UnitSubSetting> PartyUnitSubSettings
    {
      get
      {
        return this.mPartyUnitSubSettings;
      }
    }

    public List<NPCSetting> NPCUnitSettings
    {
      get
      {
        return this.mNPCUnitSettings;
      }
    }

    public List<UnitSetting> ArenaUnitSettings
    {
      get
      {
        return this.mArenaUnitSettings;
      }
    }

    public QuestMonitorCondition WinMonitorCondition
    {
      get
      {
        return this.mWinMonitorCondition;
      }
    }

    public QuestMonitorCondition LoseMonitorCondition
    {
      get
      {
        return this.mLoseMonitorCondition;
      }
    }

    public List<JSON_GimmickEvent> GimmickEvents
    {
      get
      {
        return this.mGimmickEvents;
      }
    }

    public List<TrickSetting> TrickSettings
    {
      get
      {
        return this.mTrickSettings;
      }
    }

    public int Width
    {
      get
      {
        return this.mWidth;
      }
    }

    public int Height
    {
      get
      {
        return this.mHeight;
      }
    }

    public int GridCount
    {
      get
      {
        return this.mWidth * this.mHeight;
      }
    }

    public Grid this[int x, int y]
    {
      get
      {
        if (0 <= x && x < this.mWidth && (0 <= y && y < this.mHeight))
          return this.mGrid[x, y];
        return (Grid) null;
      }
    }

    public Grid this[int i]
    {
      get
      {
        return this[i % this.mWidth, i / this.mWidth];
      }
    }

    public bool Initialize(BattleCore core, MapParam param)
    {
      this.mBattle = core;
      this.MapSceneName = param.mapSceneName;
      this.BattleSceneName = param.battleSceneName;
      this.EventSceneName = param.eventSceneName;
      this.BGMName = param.bgmName;
      this.mWinMonitorCondition.Clear();
      this.mLoseMonitorCondition.Clear();
      if (string.IsNullOrEmpty(param.mapSceneName))
      {
        DebugUtility.LogError("not found mapdata.");
        return false;
      }
      string path1 = AssetPath.LocalMap(param.mapSceneName);
      string src1 = AssetManager.LoadTextData(path1);
      if (src1 == null)
      {
        DebugUtility.LogError("Failed to load " + path1);
        return false;
      }
      if (!this.Deserialize(JSONParser.parseJSONObject<JSON_Map>(src1)))
      {
        DebugUtility.LogError("Failed to load " + path1);
        return false;
      }
      string path2 = AssetPath.LocalMap(param.mapSetName);
      string src2 = AssetManager.LoadTextData(path2);
      if (src2 == null)
      {
        DebugUtility.LogError("マップ配置情報\"" + path2 + "\"に存在しない");
        return false;
      }
      JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src2);
      if (jsonObject == null)
      {
        DebugUtility.LogError("マップ配置情報\"" + path2 + "\"のパースに失敗");
        return false;
      }
      if (jsonObject.enemy == null && jsonObject.arena == null)
      {
        DebugUtility.LogError("敵ユニットの配置情報がマップ配置情報\"" + path2 + "\"に存在しない");
        return false;
      }
      if (jsonObject.party != null)
      {
        this.mPartyUnitSettings = new List<UnitSetting>(jsonObject.party.Length);
        for (int index = 0; index < jsonObject.party.Length; ++index)
          this.mPartyUnitSettings.Add(new UnitSetting(jsonObject.party[index]));
      }
      if (jsonObject.party_subs != null && jsonObject.party_subs.Length != 0)
      {
        this.mPartyUnitSubSettings = new List<UnitSubSetting>(jsonObject.party_subs.Length);
        foreach (JSON_MapPartySubCT partySub in jsonObject.party_subs)
          this.mPartyUnitSubSettings.Add(new UnitSubSetting(partySub));
      }
      if (jsonObject.tricks != null && jsonObject.tricks.Length != 0)
      {
        this.mTrickSettings = new List<TrickSetting>(jsonObject.tricks.Length);
        foreach (JSON_MapTrick trick in jsonObject.tricks)
          this.mTrickSettings.Add(new TrickSetting(trick));
      }
      if (jsonObject.enemy != null)
      {
        jsonObject.enemy = jsonObject.ReplacedRandEnemy(this.mRandDeckResult, true);
        this.mNPCUnitSettings = new List<NPCSetting>(jsonObject.enemy.Length);
        for (int index = 0; index < jsonObject.enemy.Length; ++index)
          this.mNPCUnitSettings.Add(new NPCSetting(jsonObject.enemy[index]));
      }
      if (jsonObject.arena != null)
      {
        this.mArenaUnitSettings = new List<UnitSetting>(jsonObject.arena.Length);
        for (int index = 0; index < jsonObject.arena.Length; ++index)
        {
          UnitSetting unitSetting = new UnitSetting();
          unitSetting.uniqname = (OString) jsonObject.arena[index].name;
          unitSetting.ai = (OString) jsonObject.arena[index].ai;
          unitSetting.pos.x = (OInt) jsonObject.arena[index].x;
          unitSetting.pos.y = (OInt) jsonObject.arena[index].y;
          unitSetting.dir = (OInt) jsonObject.arena[index].dir;
          unitSetting.waitEntryClock = (OInt) jsonObject.arena[index].wait_e;
          unitSetting.waitMoveTurn = (OInt) jsonObject.arena[index].wait_m;
          unitSetting.waitExitTurn = (OInt) jsonObject.arena[index].wait_exit;
          unitSetting.startCtCalc = (eMapUnitCtCalcType) jsonObject.arena[index].ct_calc;
          unitSetting.startCtVal = (OInt) jsonObject.arena[index].ct_val;
          unitSetting.DisableFirceVoice = jsonObject.arena[index].fvoff != 0;
          unitSetting.side = (OInt) 1;
          unitSetting.ai_pos.x = (OInt) jsonObject.arena[index].ai_x;
          unitSetting.ai_pos.y = (OInt) jsonObject.arena[index].ai_y;
          unitSetting.ai_len = (OInt) jsonObject.arena[index].ai_len;
          unitSetting.parent = (OString) jsonObject.arena[index].parent;
          if (jsonObject.arena[index].trg != null)
          {
            unitSetting.trigger = new EventTrigger();
            unitSetting.trigger.Deserialize(jsonObject.arena[index].trg);
          }
          this.mArenaUnitSettings.Add(unitSetting);
        }
      }
      if (jsonObject.w_cond != null)
        jsonObject.w_cond.CopyTo(this.mWinMonitorCondition);
      if (jsonObject.l_cond != null)
        jsonObject.l_cond.CopyTo(this.mLoseMonitorCondition);
      if (jsonObject.gs != null)
        this.mGimmickEvents = new List<JSON_GimmickEvent>((IEnumerable<JSON_GimmickEvent>) jsonObject.gs);
      return true;
    }

    public bool Deserialize(JSON_Map src)
    {
      this.mWidth = src.w;
      this.mHeight = src.h;
      if (src.grid.Length != this.mWidth * this.mHeight)
        throw new Exception("Grid size does not match width x height");
      this.mGrid = new Grid[this.mWidth, this.mHeight];
      for (int index1 = 0; index1 < this.mHeight; ++index1)
      {
        for (int index2 = 0; index2 < this.mWidth; ++index2)
        {
          Grid grid = new Grid();
          JSON_MapGrid jsonMapGrid = src.grid[index2 + index1 * this.mWidth];
          grid.x = index2;
          grid.y = index1;
          grid.height = jsonMapGrid.h;
          grid.tile = jsonMapGrid.tile;
          grid.geo = MonoSingleton<GameManager>.Instance.GetGeoParam(jsonMapGrid.tile);
          grid.cost = grid.geo == null ? 1 : (int) grid.geo.cost;
          this.mGrid[index2, index1] = grid;
        }
      }
      this.mRoot = new BattleMapRoot();
      this.mRoot.Initialize(this.mWidth, this.mHeight, this.mGrid);
      this.ResetMoveRoutes();
      return true;
    }

    public void Release()
    {
      if (this.mCheckGrids != null)
      {
        for (int index = 0; index < this.mCheckGrids.Length; ++index)
          this.mCheckGrids[index] = (Grid) null;
        this.mCheckGrids = (Grid[]) null;
      }
      if (this.mMoveRoutes != null)
      {
        this.mMoveRoutes.Clear();
        this.mMoveRoutes = (List<Grid>) null;
      }
      this.mMoveStep = 0;
      if (this.mRoot != null)
      {
        this.mRoot.Release();
        this.mRoot = (BattleMapRoot) null;
      }
      if (this.mGrid == null)
        return;
      for (int index1 = 0; index1 < this.mHeight; ++index1)
      {
        for (int index2 = 0; index2 < this.mWidth; ++index2)
          this.mGrid[index2, index1] = (Grid) null;
      }
      this.mGrid = (Grid[,]) null;
    }

    public bool CheckGridAdjacent(Grid src, Grid dsc)
    {
      return src.y == dsc.y && src.x - 1 == dsc.x || src.y == dsc.y && src.x + 1 == dsc.x || (src.x == dsc.x && src.y - 1 == dsc.y || src.x == dsc.x && src.y + 1 == dsc.y);
    }

    public bool CheckEnableMove(Unit self, Grid grid, bool bSurinuke, bool ignoreObject = false)
    {
      if (self == null || grid == null)
        return false;
      int moveCount = self.GetMoveCount(false);
      for (int index1 = 0; index1 < self.SizeX; ++index1)
      {
        for (int index2 = 0; index2 < self.SizeY; ++index2)
        {
          Grid grid1 = this[grid.x + index1, grid.y + index2];
          if (grid1 == null)
            return false;
          GeoParam geo = grid1.geo;
          if (geo != null && ((bool) geo.DisableStopped || (int) geo.cost > moveCount))
            return false;
          Unit gimmickAtGrid = this.mBattle.FindGimmickAtGrid(grid1, false, self);
          if (gimmickAtGrid != null && !gimmickAtGrid.IsIntoUnit)
            return false;
          if (!ignoreObject)
          {
            Unit unitAtGrid = this.mBattle.FindUnitAtGrid(grid1);
            if (unitAtGrid != null && self != unitAtGrid)
            {
              if (bSurinuke)
              {
                if (self.Side != unitAtGrid.Side)
                  return false;
              }
              else if (!unitAtGrid.IsIntoUnit)
                return false;
            }
          }
        }
      }
      return true;
    }

    public bool CheckEnableMoveTeleport(Unit self, Grid grid, SkillData skill)
    {
      if (self == null || grid == null)
        return false;
      bool isTargetTeleport = skill.IsTargetTeleport;
      for (int index1 = 0; index1 < self.SizeX; ++index1)
      {
        for (int index2 = 0; index2 < self.SizeY; ++index2)
        {
          Grid grid1 = this[grid.x + index1, grid.y + index2];
          if (grid1 == null)
            return false;
          GeoParam geo = grid1.geo;
          if (geo != null && (bool) geo.DisableStopped)
            return false;
          Unit gimmickAtGrid = this.mBattle.FindGimmickAtGrid(grid1, false, (Unit) null);
          if (gimmickAtGrid != null && !gimmickAtGrid.IsIntoUnit)
            return false;
          if (!isTargetTeleport)
          {
            Unit unitAtGrid = this.mBattle.FindUnitAtGrid(grid1);
            if (unitAtGrid != null && self != unitAtGrid && !unitAtGrid.IsIntoUnit)
              return false;
          }
        }
      }
      return true;
    }

    public bool CheckEnableMoveHeight(Unit self, Grid current, Grid next)
    {
      if (self == null || current == null || next == null)
        return false;
      int disableMoveGridHeight = self.DisableMoveGridHeight;
      for (int index1 = 0; index1 < self.SizeX; ++index1)
      {
        for (int index2 = 0; index2 < self.SizeY; ++index2)
        {
          Grid grid1 = this[current.x + index1, current.y + index2];
          Grid grid2 = this[next.x + index1, next.y + index2];
          if (grid1 == null || grid2 == null || Math.Abs(grid1.height - grid2.height) >= disableMoveGridHeight)
            return false;
        }
      }
      return true;
    }

    public void ResetMoveRoutes()
    {
      this.mMoveRoutes.Clear();
      this.mMoveStep = 0;
      this.ResetGridSteps();
    }

    public Grid[] FindPath(int startX, int startY, int goalX, int goalY, int disableHeight, GridMap<int> walkableField)
    {
      if (startX == goalX && startY == goalY)
      {
        Grid grid = this[startX, startY];
        if (grid == null)
          return (Grid[]) null;
        return new Grid[1]{ grid };
      }
      if (!walkableField.isValid(startX, startY) || !walkableField.isValid(goalX, goalY) || (walkableField.get(startX, startY) < 0 || walkableField.get(goalX, goalY) < 0))
        return (Grid[]) null;
      if (this.mRoot.CalcRoot(startX, startY, goalX, goalY, disableHeight, walkableField))
      {
        Grid[] root = this.mRoot.GetRoot();
        if (root != null)
          return root;
      }
      GridMap<int> gridMap = new GridMap<int>(this.mWidth, this.mHeight);
      gridMap.fill(int.MaxValue);
      int num1 = this.mWidth * this.mHeight;
      int num2 = 0;
      bool flag = false;
      for (int y = 0; y < this.mHeight; ++y)
      {
        for (int x = 0; x < this.mWidth; ++x)
        {
          if (walkableField.get(x, y) >= 0)
            ++num2;
        }
      }
      gridMap.set(startX, startY, 0);
      int num3 = 0;
      for (int index1 = 0; index1 < num1 && num3 < num2; ++index1)
      {
        for (int y1 = 0; y1 < this.mHeight; ++y1)
        {
          for (int x1 = 0; x1 < this.mWidth; ++x1)
          {
            if (walkableField.get(x1, y1) >= 0 && gridMap.get(x1, y1) == index1)
            {
              Grid grid1 = this[x1, y1];
              for (int index2 = 0; index2 < 4; ++index2)
              {
                int x2 = x1 + BattleMap.ADJ_OFFSETS[index2 * 2];
                int y2 = y1 + BattleMap.ADJ_OFFSETS[index2 * 2 + 1];
                Grid grid2 = this[x2, y2];
                if (grid2 != null && walkableField.get(x2, y2) >= 0 && (gridMap.get(x2, y2) == int.MaxValue && Math.Abs(grid2.height - grid1.height) < disableHeight))
                {
                  gridMap.set(x2, y2, index1 + 1);
                  ++num3;
                  if (x2 == goalX && y2 == goalY)
                  {
                    flag = true;
                    break;
                  }
                }
              }
              if (flag)
                break;
            }
          }
          if (flag)
            break;
        }
        if (flag)
          break;
      }
      if (!flag)
        return (Grid[]) null;
      int capacity = gridMap.get(goalX, goalY) + 1;
      List<Grid> gridList = new List<Grid>(capacity);
      int x3 = goalX;
      int y3 = goalY;
      for (int index1 = 0; index1 < capacity - 1; ++index1)
      {
        Grid grid = this[x3, y3];
        int num4 = gridMap.get(x3, y3) - 1;
        gridList.Add(grid);
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int x1 = grid.x + BattleMap.ADJ_OFFSETS[index2 * 2];
          int y1 = grid.y + BattleMap.ADJ_OFFSETS[index2 * 2 + 1];
          if (this[x1, y1] != null && gridMap.get(x1, y1) == num4)
          {
            x3 = x1;
            y3 = y1;
            break;
          }
        }
      }
      gridList.Add(this[startX, startY]);
      gridList.Reverse();
      return gridList.ToArray();
    }

    public Grid GetMoveRoutes(int step)
    {
      if (0 <= step && step < this.mMoveRoutes.Count)
        return this.mMoveRoutes[step];
      return (Grid) null;
    }

    public int GetMoveRoutesCount()
    {
      return this.mMoveRoutes.Count;
    }

    public Grid GetCurrentMoveRoutes()
    {
      return this.GetMoveRoutes(this.mMoveStep);
    }

    public Grid GetNextMoveRoutes()
    {
      return this.GetMoveRoutes(this.mMoveStep + 1);
    }

    public bool IsLastMoveGrid(Grid last)
    {
      return last == this.mMoveRoutes[this.mMoveRoutes.Count - 1];
    }

    public void IncrementMoveStep()
    {
      this.mMoveStep = Math.Min(++this.mMoveStep, this.mMoveRoutes.Count - 1);
    }

    private void ResetGridSteps()
    {
      for (int index1 = 0; index1 < this.mHeight; ++index1)
      {
        for (int index2 = 0; index2 < this.mWidth; ++index2)
          this.mGrid[index2, index1].step = (byte) 127;
      }
      for (int index = 0; index < this.mCheckGrids.Length; ++index)
        this.mCheckGrids[index] = (Grid) null;
    }

    public bool CalcMoveSteps(Unit unit, Grid target, bool ignoreObject = false)
    {
      if (unit == null)
        return false;
      this.ResetGridSteps();
      target.step = (byte) 0;
      byte num = 0;
      while (num < (byte) 127)
      {
        bool flag = false;
        int val1 = 1;
        for (int index1 = 0; index1 < this.Height; ++index1)
        {
          for (int index2 = 0; index2 < this.Width; ++index2)
          {
            if ((int) num == (int) this[index2, index1].step)
            {
              this.mCheckGrids[0] = this[index2, index1 - 1];
              this.mCheckGrids[1] = this[index2 - 1, index1];
              this.mCheckGrids[2] = this[index2 + 1, index1];
              this.mCheckGrids[3] = this[index2, index1 + 1];
              for (int index3 = 0; index3 < this.mCheckGrids.Length; ++index3)
              {
                Grid mCheckGrid = this.mCheckGrids[index3];
                if (mCheckGrid != null && (int) mCheckGrid.step > (int) num + 1 && (this.CheckEnableMoveHeight(unit, this[index2, index1], mCheckGrid) && this.CheckEnableMove(unit, mCheckGrid, true, ignoreObject)))
                {
                  mCheckGrid.step = (byte) Math.Min((int) num + mCheckGrid.cost, (int) mCheckGrid.step);
                  val1 = Math.Max(Math.Min(val1, mCheckGrid.cost), 1);
                  flag = true;
                }
              }
            }
          }
        }
        if (flag)
          num += (byte) val1;
        else
          break;
      }
      return this[unit.x, unit.y].step != (byte) 127;
    }

    private Grid CalcNextMoveRoutes(Unit self, Grid current, bool last)
    {
      int x = current.x;
      int y = current.y;
      this.mCheckGrids[0] = this[x, y - 1];
      this.mCheckGrids[1] = this[x - 1, y];
      this.mCheckGrids[2] = this[x + 1, y];
      this.mCheckGrids[3] = this[x, y + 1];
      long length = (long) this.mCheckGrids.Length;
      while (length > 1L)
      {
        long index = (long) this.mBattle.GetRandom() % length--;
        Grid mCheckGrid = this.mCheckGrids[index];
        this.mCheckGrids[index] = this.mCheckGrids[length];
        this.mCheckGrids[length] = mCheckGrid;
      }
      Grid grid = (Grid) null;
      byte num = current.step;
      for (int index = 0; index < this.mCheckGrids.Length; ++index)
      {
        if (this.mCheckGrids[index] != null)
        {
          byte step = this.mCheckGrids[index].step;
          if (step != (byte) 127 && (int) step < (int) num && (this.CheckEnableMoveHeight(self, current, this.mCheckGrids[index]) && this.CheckEnableMove(self, this.mCheckGrids[index], true, false)) && (!last || self.AI == null || (!self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire) || !this.mBattle.CheckFriendlyFireOnGridMap(self, this.mCheckGrids[index]))))
          {
            grid = this.mCheckGrids[index];
            num = step;
          }
        }
      }
      return grid;
    }

    public bool CalcMoveRoutes(Unit self)
    {
      if (self == null)
        return false;
      this.mMoveRoutes.Clear();
      this.mMoveStep = 0;
      Grid current = this[self.x, self.y];
      this.mMoveRoutes.Add(current);
      int moveCount = self.GetMoveCount(false);
      for (int index = 0; index < moveCount; ++index)
      {
        bool last = index == moveCount - 1;
        Grid grid = this.CalcNextMoveRoutes(self, current, last);
        if (grid != null)
        {
          this.mMoveRoutes.Add(grid);
          current = grid;
        }
        else
          break;
      }
      for (int index = this.mMoveRoutes.Count - 1; index > 0; --index)
      {
        if (!this.CheckEnableMove(self, this.mMoveRoutes[index], false, false))
          this.mMoveRoutes.RemoveAt(index);
        else if (self.AI != null && self.AI.CheckFlag(AIFlags.CastSkillFriendlyFire) && this.mBattle.CheckFriendlyFireOnGridMap(self, this.mMoveRoutes[index]))
          this.mMoveRoutes.RemoveAt(index);
        else
          break;
      }
      return this.mMoveRoutes.Count > 1;
    }
  }
}
