namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class BattleMap
    {
        public static readonly int MAX_GRID_WIDTH;
        public static readonly int MAX_GRID_HEIGHT;
        public static readonly int MAX_GRID_MOVING;
        public static readonly int MAP_FLOOR_HEIGHT;
        public string MapSceneName;
        public string BattleSceneName;
        public string EventSceneName;
        public string BGMName;
        private List<UnitSetting> mPartyUnitSettings;
        private List<UnitSubSetting> mPartyUnitSubSettings;
        private List<NPCSetting> mNPCUnitSettings;
        private List<UnitSetting> mArenaUnitSettings;
        private QuestMonitorCondition mWinMonitorCondition;
        private QuestMonitorCondition mLoseMonitorCondition;
        private List<JSON_GimmickEvent> mGimmickEvents;
        private List<TrickSetting> mTrickSettings;
        private BattleCore mBattle;
        private int mWidth;
        private int mHeight;
        private Grid[,] mGrid;
        private BattleMapRoot mRoot;
        private List<Grid> mMoveRoutes;
        private int mMoveStep;
        private Grid[] mCheckGrids;
        public RandDeckResult[] mRandDeckResult;
        private static int[] ADJ_OFFSETS;

        static BattleMap()
        {
            MAX_GRID_WIDTH = 20;
            MAX_GRID_HEIGHT = 20;
            MAX_GRID_MOVING = 0x10;
            MAP_FLOOR_HEIGHT = 2;
            ADJ_OFFSETS = new int[] { 1, 0, 0, 1, -1, 0, 0, -1 };
            return;
        }

        public BattleMap()
        {
            this.mPartyUnitSubSettings = new List<UnitSubSetting>();
            this.mWinMonitorCondition = new QuestMonitorCondition();
            this.mLoseMonitorCondition = new QuestMonitorCondition();
            this.mTrickSettings = new List<TrickSetting>();
            this.mMoveRoutes = new List<Grid>(MAX_GRID_MOVING);
            this.mCheckGrids = new Grid[4];
            base..ctor();
            return;
        }

        public bool CalcMoveRoutes(Unit self)
        {
            Grid grid;
            int num;
            int num2;
            bool flag;
            Grid grid2;
            int num3;
            if (self != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mMoveRoutes.Clear();
            this.mMoveStep = 0;
            grid = this[self.x, self.y];
            this.mMoveRoutes.Add(grid);
            num = self.GetMoveCount(0);
            num2 = 0;
            goto Label_007A;
        Label_0048:
            flag = num2 == (num - 1);
            grid2 = this.CalcNextMoveRoutes(self, grid, flag);
            if (grid2 != null)
            {
                goto Label_0066;
            }
            goto Label_0081;
        Label_0066:
            this.mMoveRoutes.Add(grid2);
            grid = grid2;
            num2 += 1;
        Label_007A:
            if (num2 < num)
            {
                goto Label_0048;
            }
        Label_0081:
            num3 = this.mMoveRoutes.Count - 1;
            goto Label_011A;
        Label_0095:
            if (this.CheckEnableMove(self, this.mMoveRoutes[num3], 0, 0) != null)
            {
                goto Label_00C2;
            }
            this.mMoveRoutes.RemoveAt(num3);
            goto Label_0114;
        Label_00C2:
            if (((self.AI == null) || (self.AI.CheckFlag(0x40) == null)) || (this.mBattle.CheckFriendlyFireOnGridMap(self, this.mMoveRoutes[num3]) == null))
            {
                goto Label_0122;
            }
            this.mMoveRoutes.RemoveAt(num3);
            goto Label_0114;
            goto Label_0122;
        Label_0114:
            num3 -= 1;
        Label_011A:
            if (num3 > 0)
            {
                goto Label_0095;
            }
        Label_0122:
            return ((this.mMoveRoutes.Count <= 1) ? 0 : 1);
        }

        public bool CalcMoveSteps(Unit unit, Grid target, bool ignoreObject)
        {
            int num;
            byte num2;
            bool flag;
            int num3;
            int num4;
            int num5;
            Grid grid;
            Grid grid2;
            if (unit != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.ResetGridSteps();
            num = 1;
            target.step = 0;
            num2 = 0;
            goto Label_0171;
        Label_001E:
            flag = 0;
            num = 1;
            num3 = 0;
            goto Label_0154;
        Label_0029:
            num4 = 0;
            goto Label_0143;
        Label_0031:
            if (num2 == this[num4, num3].step)
            {
                goto Label_004A;
            }
            goto Label_013D;
        Label_004A:
            this.mCheckGrids[0] = this[num4, num3 - 1];
            this.mCheckGrids[1] = this[num4 - 1, num3];
            this.mCheckGrids[2] = this[num4 + 1, num3];
            this.mCheckGrids[3] = this[num4, num3 + 1];
            num5 = 0;
            goto Label_012E;
        Label_009E:
            grid = this.mCheckGrids[num5];
            if (grid != null)
            {
                goto Label_00B5;
            }
            goto Label_0128;
        Label_00B5:
            if (grid.step <= (num2 + 1))
            {
                goto Label_0128;
            }
            if (this.CheckEnableMoveHeight(unit, this[num4, num3], grid) != null)
            {
                goto Label_00E0;
            }
            goto Label_0128;
        Label_00E0:
            if (this.CheckEnableMove(unit, grid, 1, ignoreObject) != null)
            {
                goto Label_00F5;
            }
            goto Label_0128;
        Label_00F5:
            grid.step = (byte) Math.Min(num2 + grid.cost, grid.step);
            num = Math.Max(Math.Min(num, grid.cost), 1);
            flag = 1;
        Label_0128:
            num5 += 1;
        Label_012E:
            if (num5 < ((int) this.mCheckGrids.Length))
            {
                goto Label_009E;
            }
        Label_013D:
            num4 += 1;
        Label_0143:
            if (num4 < this.Width)
            {
                goto Label_0031;
            }
            num3 += 1;
        Label_0154:
            if (num3 < this.Height)
            {
                goto Label_0029;
            }
            if (flag != null)
            {
                goto Label_016B;
            }
            goto Label_0179;
        Label_016B:
            num2 = (byte) (num2 + ((byte) num));
        Label_0171:
            if (num2 < 0x7f)
            {
                goto Label_001E;
            }
        Label_0179:
            grid2 = this[unit.x, unit.y];
            if (grid2.step != 0x7f)
            {
                goto Label_019D;
            }
            return 0;
        Label_019D:
            return 1;
        }

        private Grid CalcNextMoveRoutes(Unit self, Grid current, bool last)
        {
            int num;
            int num2;
            long num3;
            long num4;
            long num5;
            Grid grid;
            Grid grid2;
            byte num6;
            int num7;
            byte num8;
            num = current.x;
            num2 = current.y;
            this.mCheckGrids[0] = this[num, num2 - 1];
            this.mCheckGrids[1] = this[num - 1, num2];
            this.mCheckGrids[2] = this[num + 1, num2];
            this.mCheckGrids[3] = this[num, num2 + 1];
            num3 = (long) ((int) this.mCheckGrids.Length);
            goto Label_00A5;
        Label_0065:
            num5 = (ulong) this.mBattle.GetRandom();
            num3 -= 1L;
            num4 = num5 % num3;
            grid = this.mCheckGrids[(IntPtr) num4];
            this.mCheckGrids[(IntPtr) num4] = this.mCheckGrids[(IntPtr) num3];
            this.mCheckGrids[(IntPtr) num3] = grid;
        Label_00A5:
            if (num3 > 1L)
            {
                goto Label_0065;
            }
            grid2 = null;
            num6 = current.step;
            num7 = 0;
            goto Label_0188;
        Label_00C0:
            if (this.mCheckGrids[num7] != null)
            {
                goto Label_00D3;
            }
            goto Label_0182;
        Label_00D3:
            num8 = this.mCheckGrids[num7].step;
            if (num8 != 0x7f)
            {
                goto Label_00F1;
            }
            goto Label_0182;
        Label_00F1:
            if (num8 >= num6)
            {
                goto Label_0182;
            }
            if (this.CheckEnableMoveHeight(self, current, this.mCheckGrids[num7]) != null)
            {
                goto Label_0115;
            }
            goto Label_0182;
        Label_0115:
            if (this.CheckEnableMove(self, this.mCheckGrids[num7], 1, 0) != null)
            {
                goto Label_0131;
            }
            goto Label_0182;
        Label_0131:
            if (last == null)
            {
                goto Label_0173;
            }
            if (self.AI == null)
            {
                goto Label_0173;
            }
            if (self.AI.CheckFlag(0x40) == null)
            {
                goto Label_0173;
            }
            if (this.mBattle.CheckFriendlyFireOnGridMap(self, this.mCheckGrids[num7]) == null)
            {
                goto Label_0173;
            }
            goto Label_0182;
        Label_0173:
            grid2 = this.mCheckGrids[num7];
            num6 = num8;
        Label_0182:
            num7 += 1;
        Label_0188:
            if (num7 < ((int) this.mCheckGrids.Length))
            {
                goto Label_00C0;
            }
            return grid2;
        }

        public bool CheckEnableMove(Unit self, Grid grid, bool bSurinuke, bool ignoreObject)
        {
            int num;
            int num2;
            int num3;
            Grid grid2;
            GeoParam param;
            Unit unit;
            Unit unit2;
            if (self == null)
            {
                goto Label_000C;
            }
            if (grid != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            num = self.GetMoveCount(0);
            num2 = 0;
            goto Label_0101;
        Label_001D:
            num3 = 0;
            goto Label_00F1;
        Label_0024:
            grid2 = this[grid.x + num2, grid.y + num3];
            if (grid2 != null)
            {
                goto Label_0043;
            }
            return 0;
        Label_0043:
            param = grid2.geo;
            if (param == null)
            {
                goto Label_0077;
            }
            if (param.DisableStopped != null)
            {
                goto Label_0075;
            }
            if (param.cost <= num)
            {
                goto Label_0077;
            }
        Label_0075:
            return 0;
        Label_0077:
            unit = this.mBattle.FindGimmickAtGrid(grid2, 0, self);
            if (unit == null)
            {
                goto Label_009C;
            }
            if (unit.IsIntoUnit != null)
            {
                goto Label_009C;
            }
            return 0;
        Label_009C:
            if (ignoreObject != null)
            {
                goto Label_00ED;
            }
            unit2 = this.mBattle.FindUnitAtGrid(grid2);
            if (unit2 == null)
            {
                goto Label_00ED;
            }
            if (self == unit2)
            {
                goto Label_00ED;
            }
            if (bSurinuke == null)
            {
                goto Label_00DF;
            }
            if (self.Side == unit2.Side)
            {
                goto Label_00ED;
            }
            return 0;
            goto Label_00ED;
        Label_00DF:
            if (unit2.IsIntoUnit != null)
            {
                goto Label_00ED;
            }
            return 0;
        Label_00ED:
            num3 += 1;
        Label_00F1:
            if (num3 < self.SizeY)
            {
                goto Label_0024;
            }
            num2 += 1;
        Label_0101:
            if (num2 < self.SizeX)
            {
                goto Label_001D;
            }
            return 1;
        }

        public bool CheckEnableMoveHeight(Unit self, Grid current, Grid next)
        {
            int num;
            int num2;
            int num3;
            Grid grid;
            Grid grid2;
            int num4;
            if (self == null)
            {
                goto Label_0012;
            }
            if (current == null)
            {
                goto Label_0012;
            }
            if (next != null)
            {
                goto Label_0014;
            }
        Label_0012:
            return 0;
        Label_0014:
            num = self.DisableMoveGridHeight;
            num2 = 0;
            goto Label_009A;
        Label_0022:
            num3 = 0;
            goto Label_008A;
        Label_0029:
            grid = this[current.x + num2, current.y + num3];
            grid2 = this[next.x + num2, next.y + num3];
            if (grid == null)
            {
                goto Label_0065;
            }
            if (grid2 != null)
            {
                goto Label_0067;
            }
        Label_0065:
            return 0;
        Label_0067:
            if (Math.Abs(grid.height - grid2.height) < num)
            {
                goto Label_0086;
            }
            return 0;
        Label_0086:
            num3 += 1;
        Label_008A:
            if (num3 < self.SizeY)
            {
                goto Label_0029;
            }
            num2 += 1;
        Label_009A:
            if (num2 < self.SizeX)
            {
                goto Label_0022;
            }
            return 1;
        }

        public bool CheckEnableMoveTeleport(Unit self, Grid grid, SkillData skill)
        {
            bool flag;
            int num;
            int num2;
            Grid grid2;
            GeoParam param;
            Unit unit;
            Unit unit2;
            if (self == null)
            {
                goto Label_000C;
            }
            if (grid != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            flag = skill.IsTargetTeleport;
            num = 0;
            goto Label_00CE;
        Label_001C:
            num2 = 0;
            goto Label_00BE;
        Label_0023:
            grid2 = this[grid.x + num, grid.y + num2];
            if (grid2 != null)
            {
                goto Label_0042;
            }
            return 0;
        Label_0042:
            param = grid2.geo;
            if (param == null)
            {
                goto Label_0064;
            }
            if (param.DisableStopped == null)
            {
                goto Label_0064;
            }
            return 0;
        Label_0064:
            unit = this.mBattle.FindGimmickAtGrid(grid2, 0, null);
            if (unit == null)
            {
                goto Label_0089;
            }
            if (unit.IsIntoUnit != null)
            {
                goto Label_0089;
            }
            return 0;
        Label_0089:
            if (flag != null)
            {
                goto Label_00BA;
            }
            unit2 = this.mBattle.FindUnitAtGrid(grid2);
            if (unit2 == null)
            {
                goto Label_00BA;
            }
            if (self == unit2)
            {
                goto Label_00BA;
            }
            if (unit2.IsIntoUnit != null)
            {
                goto Label_00BA;
            }
            return 0;
        Label_00BA:
            num2 += 1;
        Label_00BE:
            if (num2 < self.SizeY)
            {
                goto Label_0023;
            }
            num += 1;
        Label_00CE:
            if (num < self.SizeX)
            {
                goto Label_001C;
            }
            return 1;
        }

        public bool CheckGridAdjacent(Grid src, Grid dsc)
        {
            if (src.y != dsc.y)
            {
                goto Label_0026;
            }
            if ((src.x - 1) != dsc.x)
            {
                goto Label_0026;
            }
            return 1;
        Label_0026:
            if (src.y != dsc.y)
            {
                goto Label_004C;
            }
            if ((src.x + 1) != dsc.x)
            {
                goto Label_004C;
            }
            return 1;
        Label_004C:
            if (src.x != dsc.x)
            {
                goto Label_0072;
            }
            if ((src.y - 1) != dsc.y)
            {
                goto Label_0072;
            }
            return 1;
        Label_0072:
            if (src.x != dsc.x)
            {
                goto Label_0098;
            }
            if ((src.y + 1) != dsc.y)
            {
                goto Label_0098;
            }
            return 1;
        Label_0098:
            return 0;
        }

        public bool Deserialize(JSON_Map src)
        {
            int num;
            int num2;
            Grid grid;
            JSON_MapGrid grid2;
            this.mWidth = src.w;
            this.mHeight = src.h;
            if (((int) src.grid.Length) == (this.mWidth * this.mHeight))
            {
                goto Label_003D;
            }
            throw new Exception("Grid size does not match width x height");
        Label_003D:
            this.mGrid = new Grid[this.mWidth, this.mHeight];
            num = 0;
            goto Label_00FF;
        Label_005B:
            num2 = 0;
            goto Label_00EF;
        Label_0062:
            grid = new Grid();
            grid2 = src.grid[num2 + (num * this.mWidth)];
            grid.x = num2;
            grid.y = num;
            grid.height = grid2.h;
            grid.tile = grid2.tile;
            grid.geo = MonoSingleton<GameManager>.Instance.GetGeoParam(grid2.tile);
            grid.cost = (grid.geo == null) ? 1 : grid.geo.cost;
            this.mGrid[num2, num] = grid;
            num2 += 1;
        Label_00EF:
            if (num2 < this.mWidth)
            {
                goto Label_0062;
            }
            num += 1;
        Label_00FF:
            if (num < this.mHeight)
            {
                goto Label_005B;
            }
            this.mRoot = new BattleMapRoot();
            this.mRoot.Initialize(this.mWidth, this.mHeight, this.mGrid);
            this.ResetMoveRoutes();
            return 1;
        }

        public Grid[] FindPath(int startX, int startY, int goalX, int goalY, int disableHeight, GridMap<int> walkableField)
        {
            Grid[] gridArray1;
            Grid grid;
            Grid[] gridArray;
            GridMap<int> map;
            int num;
            int num2;
            bool flag;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            Grid grid2;
            int num9;
            int num10;
            int num11;
            Grid grid3;
            int num12;
            int num13;
            List<Grid> list;
            int num14;
            int num15;
            int num16;
            Grid grid4;
            int num17;
            int num18;
            int num19;
            int num20;
            Grid grid5;
            if ((startX != goalX) || (startY != goalY))
            {
                goto Label_002F;
            }
            grid = this[startX, startY];
            return ((grid == null) ? null : (gridArray1 = new Grid[] { grid }));
        Label_002F:
            if (walkableField.isValid(startX, startY) == null)
            {
                goto Label_006B;
            }
            if (walkableField.isValid(goalX, goalY) == null)
            {
                goto Label_006B;
            }
            if (walkableField.get(startX, startY) < 0)
            {
                goto Label_006B;
            }
            if (walkableField.get(goalX, goalY) >= 0)
            {
                goto Label_006D;
            }
        Label_006B:
            return null;
        Label_006D:
            if (this.mRoot.CalcRoot(startX, startY, goalX, goalY, disableHeight, walkableField) == null)
            {
                goto Label_009A;
            }
            gridArray = this.mRoot.GetRoot();
            if (gridArray == null)
            {
                goto Label_009A;
            }
            return gridArray;
        Label_009A:
            map = new GridMap<int>(this.mWidth, this.mHeight);
            map.fill(0x7fffffff);
            num = this.mWidth * this.mHeight;
            num2 = 0;
            flag = 0;
            num3 = 0;
            goto Label_010B;
        Label_00D3:
            num4 = 0;
            goto Label_00F8;
        Label_00DB:
            if (walkableField.get(num4, num3) < 0)
            {
                goto Label_00F2;
            }
            num2 += 1;
        Label_00F2:
            num4 += 1;
        Label_00F8:
            if (num4 < this.mWidth)
            {
                goto Label_00DB;
            }
            num3 += 1;
        Label_010B:
            if (num3 < this.mHeight)
            {
                goto Label_00D3;
            }
            map.set(startX, startY, 0);
            num5 = 0;
            num6 = 0;
            goto Label_0292;
        Label_012C:
            num7 = 0;
            goto Label_0273;
        Label_0134:
            num8 = 0;
            goto Label_0254;
        Label_013C:
            if (walkableField.get(num8, num7) >= 0)
            {
                goto Label_0152;
            }
            goto Label_024E;
        Label_0152:
            if (map.get(num8, num7) == num6)
            {
                goto Label_0168;
            }
            goto Label_024E;
        Label_0168:
            grid2 = this[num8, num7];
            num9 = 0;
            goto Label_023A;
        Label_017C:
            num10 = num8 + ADJ_OFFSETS[num9 * 2];
            num11 = num7 + ADJ_OFFSETS[(num9 * 2) + 1];
            grid3 = this[num10, num11];
            if (grid3 != null)
            {
                goto Label_01B4;
            }
            goto Label_0234;
        Label_01B4:
            if (walkableField.get(num10, num11) >= 0)
            {
                goto Label_01CA;
            }
            goto Label_0234;
        Label_01CA:
            if (map.get(num10, num11) == 0x7fffffff)
            {
                goto Label_01E3;
            }
            goto Label_0234;
        Label_01E3:
            num12 = grid3.height - grid2.height;
            if (Math.Abs(num12) < disableHeight)
            {
                goto Label_0207;
            }
            goto Label_0234;
        Label_0207:
            map.set(num10, num11, num6 + 1);
            num5 += 1;
            if (num10 != goalX)
            {
                goto Label_0234;
            }
            if (num11 != goalY)
            {
                goto Label_0234;
            }
            flag = 1;
            goto Label_0242;
        Label_0234:
            num9 += 1;
        Label_023A:
            if (num9 < 4)
            {
                goto Label_017C;
            }
        Label_0242:
            if (flag == null)
            {
                goto Label_024E;
            }
            goto Label_0261;
        Label_024E:
            num8 += 1;
        Label_0254:
            if (num8 < this.mWidth)
            {
                goto Label_013C;
            }
        Label_0261:
            if (flag == null)
            {
                goto Label_026D;
            }
            goto Label_0280;
        Label_026D:
            num7 += 1;
        Label_0273:
            if (num7 < this.mHeight)
            {
                goto Label_0134;
            }
        Label_0280:
            if (flag == null)
            {
                goto Label_028C;
            }
            goto Label_02A3;
        Label_028C:
            num6 += 1;
        Label_0292:
            if (num6 >= num)
            {
                goto Label_02A3;
            }
            if (num5 < num2)
            {
                goto Label_012C;
            }
        Label_02A3:
            if (flag != null)
            {
                goto Label_02AC;
            }
            return null;
        Label_02AC:
            num13 = map.get(goalX, goalY) + 1;
            list = new List<Grid>(num13);
            num14 = goalX;
            num15 = goalY;
            num16 = 0;
            goto Label_0375;
        Label_02D1:
            grid4 = this[num14, num15];
            num17 = map.get(num14, num15) - 1;
            list.Add(grid4);
            num18 = 0;
            goto Label_0367;
        Label_02FC:
            num19 = grid4.x + ADJ_OFFSETS[num18 * 2];
            num20 = grid4.y + ADJ_OFFSETS[(num18 * 2) + 1];
            if (this[num19, num20] != null)
            {
                goto Label_033E;
            }
            goto Label_0361;
        Label_033E:
            if (map.get(num19, num20) == num17)
            {
                goto Label_0354;
            }
            goto Label_0361;
        Label_0354:
            num14 = num19;
            num15 = num20;
            goto Label_036F;
        Label_0361:
            num18 += 1;
        Label_0367:
            if (num18 < 4)
            {
                goto Label_02FC;
            }
        Label_036F:
            num16 += 1;
        Label_0375:
            if (num16 < (num13 - 1))
            {
                goto Label_02D1;
            }
            list.Add(this[startX, startY]);
            list.Reverse();
            return list.ToArray();
        }

        public Grid GetCurrentMoveRoutes()
        {
            return this.GetMoveRoutes(this.mMoveStep);
        }

        public Grid GetMoveRoutes(int step)
        {
            return (((0 > step) || (step >= this.mMoveRoutes.Count)) ? null : this.mMoveRoutes[step]);
        }

        public int GetMoveRoutesCount()
        {
            return this.mMoveRoutes.Count;
        }

        public Grid GetNextMoveRoutes()
        {
            return this.GetMoveRoutes(this.mMoveStep + 1);
        }

        public void IncrementMoveStep()
        {
            int num;
            this.mMoveStep = Math.Min(this.mMoveStep += 1, this.mMoveRoutes.Count - 1);
            return;
        }

        public unsafe bool Initialize(BattleCore core, MapParam param)
        {
            string str;
            string str2;
            JSON_Map map;
            string str3;
            string str4;
            JSON_MapUnit unit;
            int num;
            UnitSetting setting;
            JSON_MapPartySubCT bct;
            JSON_MapPartySubCT[] bctArray;
            int num2;
            UnitSubSetting setting2;
            JSON_MapTrick trick;
            JSON_MapTrick[] trickArray;
            int num3;
            TrickSetting setting3;
            int num4;
            NPCSetting setting4;
            int num5;
            UnitSetting setting5;
            this.mBattle = core;
            this.MapSceneName = param.mapSceneName;
            this.BattleSceneName = param.battleSceneName;
            this.EventSceneName = param.eventSceneName;
            this.BGMName = param.bgmName;
            this.mWinMonitorCondition.Clear();
            this.mLoseMonitorCondition.Clear();
            if (string.IsNullOrEmpty(param.mapSceneName) == null)
            {
                goto Label_0069;
            }
            DebugUtility.LogError("not found mapdata.");
            return 0;
        Label_0069:
            str = AssetPath.LocalMap(param.mapSceneName);
            str2 = AssetManager.LoadTextData(str);
            if (str2 != null)
            {
                goto Label_0094;
            }
            DebugUtility.LogError("Failed to load " + str);
            return 0;
        Label_0094:
            map = JSONParser.parseJSONObject<JSON_Map>(str2);
            if (this.Deserialize(map) != null)
            {
                goto Label_00B9;
            }
            DebugUtility.LogError("Failed to load " + str);
            return 0;
        Label_00B9:
            str3 = AssetPath.LocalMap(param.mapSetName);
            str4 = AssetManager.LoadTextData(str3);
            if (str4 != null)
            {
                goto Label_00EB;
            }
            DebugUtility.LogError("マップ配置情報\"" + str3 + "\"に存在しない");
            return 0;
        Label_00EB:
            unit = JSONParser.parseJSONObject<JSON_MapUnit>(str4);
            if (unit != null)
            {
                goto Label_0112;
            }
            DebugUtility.LogError("マップ配置情報\"" + str3 + "\"のパースに失敗");
            return 0;
        Label_0112:
            if (unit.enemy != null)
            {
                goto Label_0141;
            }
            if (unit.arena != null)
            {
                goto Label_0141;
            }
            DebugUtility.LogError("敵ユニットの配置情報がマップ配置情報\"" + str3 + "\"に存在しない");
            return 0;
        Label_0141:
            if (unit.party == null)
            {
                goto Label_019D;
            }
            this.mPartyUnitSettings = new List<UnitSetting>((int) unit.party.Length);
            num = 0;
            goto Label_018D;
        Label_0169:
            setting = new UnitSetting(unit.party[num]);
            this.mPartyUnitSettings.Add(setting);
            num += 1;
        Label_018D:
            if (num < ((int) unit.party.Length))
            {
                goto Label_0169;
            }
        Label_019D:
            if (unit.party_subs == null)
            {
                goto Label_020A;
            }
            if (((int) unit.party_subs.Length) == null)
            {
                goto Label_020A;
            }
            this.mPartyUnitSubSettings = new List<UnitSubSetting>((int) unit.party_subs.Length);
            bctArray = unit.party_subs;
            num2 = 0;
            goto Label_01FF;
        Label_01DC:
            bct = bctArray[num2];
            setting2 = new UnitSubSetting(bct);
            this.mPartyUnitSubSettings.Add(setting2);
            num2 += 1;
        Label_01FF:
            if (num2 < ((int) bctArray.Length))
            {
                goto Label_01DC;
            }
        Label_020A:
            if (unit.tricks == null)
            {
                goto Label_0277;
            }
            if (((int) unit.tricks.Length) == null)
            {
                goto Label_0277;
            }
            this.mTrickSettings = new List<TrickSetting>((int) unit.tricks.Length);
            trickArray = unit.tricks;
            num3 = 0;
            goto Label_026C;
        Label_0249:
            trick = trickArray[num3];
            setting3 = new TrickSetting(trick);
            this.mTrickSettings.Add(setting3);
            num3 += 1;
        Label_026C:
            if (num3 < ((int) trickArray.Length))
            {
                goto Label_0249;
            }
        Label_0277:
            if (unit.enemy == null)
            {
                goto Label_02E8;
            }
            unit.enemy = unit.ReplacedRandEnemy(this.mRandDeckResult, 1);
            this.mNPCUnitSettings = new List<NPCSetting>((int) unit.enemy.Length);
            num4 = 0;
            goto Label_02D8;
        Label_02B4:
            setting4 = new NPCSetting(unit.enemy[num4]);
            this.mNPCUnitSettings.Add(setting4);
            num4 += 1;
        Label_02D8:
            if (num4 < ((int) unit.enemy.Length))
            {
                goto Label_02B4;
            }
        Label_02E8:
            if (unit.arena == null)
            {
                goto Label_0528;
            }
            this.mArenaUnitSettings = new List<UnitSetting>((int) unit.arena.Length);
            num5 = 0;
            goto Label_0518;
        Label_0310:
            setting5 = new UnitSetting();
            setting5.uniqname = unit.arena[num5].name;
            setting5.ai = unit.arena[num5].ai;
            &setting5.pos.x = unit.arena[num5].x;
            &setting5.pos.y = unit.arena[num5].y;
            setting5.dir = unit.arena[num5].dir;
            setting5.waitEntryClock = unit.arena[num5].wait_e;
            setting5.waitMoveTurn = unit.arena[num5].wait_m;
            setting5.waitExitTurn = unit.arena[num5].wait_exit;
            setting5.startCtCalc = unit.arena[num5].ct_calc;
            setting5.startCtVal = unit.arena[num5].ct_val;
            setting5.DisableFirceVoice = (unit.arena[num5].fvoff == 0) == 0;
            setting5.side = 1;
            &setting5.ai_pos.x = unit.arena[num5].ai_x;
            &setting5.ai_pos.y = unit.arena[num5].ai_y;
            setting5.ai_len = unit.arena[num5].ai_len;
            setting5.parent = unit.arena[num5].parent;
            if (unit.arena[num5].trg == null)
            {
                goto Label_0505;
            }
            setting5.trigger = new EventTrigger();
            setting5.trigger.Deserialize(unit.arena[num5].trg);
        Label_0505:
            this.mArenaUnitSettings.Add(setting5);
            num5 += 1;
        Label_0518:
            if (num5 < ((int) unit.arena.Length))
            {
                goto Label_0310;
            }
        Label_0528:
            if (unit.w_cond == null)
            {
                goto Label_0546;
            }
            unit.w_cond.CopyTo(this.mWinMonitorCondition);
        Label_0546:
            if (unit.l_cond == null)
            {
                goto Label_0564;
            }
            unit.l_cond.CopyTo(this.mLoseMonitorCondition);
        Label_0564:
            if (unit.gs == null)
            {
                goto Label_0582;
            }
            this.mGimmickEvents = new List<JSON_GimmickEvent>(unit.gs);
        Label_0582:
            return 1;
        }

        public bool IsLastMoveGrid(Grid last)
        {
            return (last == this.mMoveRoutes[this.mMoveRoutes.Count - 1]);
        }

        public void Release()
        {
            int num;
            int num2;
            int num3;
            if (this.mCheckGrids == null)
            {
                goto Label_0034;
            }
            num = 0;
            goto Label_001F;
        Label_0012:
            this.mCheckGrids[num] = null;
            num += 1;
        Label_001F:
            if (num < ((int) this.mCheckGrids.Length))
            {
                goto Label_0012;
            }
            this.mCheckGrids = null;
        Label_0034:
            if (this.mMoveRoutes == null)
            {
                goto Label_0051;
            }
            this.mMoveRoutes.Clear();
            this.mMoveRoutes = null;
        Label_0051:
            this.mMoveStep = 0;
            if (this.mRoot == null)
            {
                goto Label_0075;
            }
            this.mRoot.Release();
            this.mRoot = null;
        Label_0075:
            if (this.mGrid == null)
            {
                goto Label_00C3;
            }
            num2 = 0;
            goto Label_00B0;
        Label_0087:
            num3 = 0;
            goto Label_00A0;
        Label_008E:
            this.mGrid[num3, num2] = null;
            num3 += 1;
        Label_00A0:
            if (num3 < this.mWidth)
            {
                goto Label_008E;
            }
            num2 += 1;
        Label_00B0:
            if (num2 < this.mHeight)
            {
                goto Label_0087;
            }
            this.mGrid = null;
        Label_00C3:
            return;
        }

        private void ResetGridSteps()
        {
            int num;
            int num2;
            int num3;
            num = 0;
            goto Label_0036;
        Label_0007:
            num2 = 0;
            goto Label_0026;
        Label_000E:
            this.mGrid[num2, num].step = 0x7f;
            num2 += 1;
        Label_0026:
            if (num2 < this.mWidth)
            {
                goto Label_000E;
            }
            num += 1;
        Label_0036:
            if (num < this.mHeight)
            {
                goto Label_0007;
            }
            num3 = 0;
            goto Label_0056;
        Label_0049:
            this.mCheckGrids[num3] = null;
            num3 += 1;
        Label_0056:
            if (num3 < ((int) this.mCheckGrids.Length))
            {
                goto Label_0049;
            }
            return;
        }

        public void ResetMoveRoutes()
        {
            this.mMoveRoutes.Clear();
            this.mMoveStep = 0;
            this.ResetGridSteps();
            return;
        }

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
                return (this.mWidth * this.mHeight);
            }
        }

        public Grid this[int x, int y]
        {
            get
            {
                if (0 > x)
                {
                    goto Label_0034;
                }
                if (x >= this.mWidth)
                {
                    goto Label_0034;
                }
                if (0 > y)
                {
                    goto Label_0034;
                }
                if (y >= this.mHeight)
                {
                    goto Label_0034;
                }
                return this.mGrid[x, y];
            Label_0034:
                return null;
            }
        }

        public Grid this[int i]
        {
            get
            {
                int num;
                int num2;
                num = i % this.mWidth;
                num2 = i / this.mWidth;
                return this[num, num2];
            }
        }
    }
}

