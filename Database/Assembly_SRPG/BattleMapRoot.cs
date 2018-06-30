namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class BattleMapRoot
    {
        private int m_Width;
        private int m_Height;
        private Element[] m_Elements;
        private int m_TotalCost;
        private Element m_Start;
        private Element m_End;
        private List<Element> m_CalcStack;

        public BattleMapRoot()
        {
            this.m_CalcStack = new List<Element>();
            base..ctor();
            return;
        }

        private unsafe void _CalcRootInit(int moveHeight, GridMap<int> walkableField)
        {
            int num;
            Element element;
            int num2;
            int num3;
            this.m_TotalCost = 0x7fffffff;
            this.m_Start = null;
            this.m_End = null;
            this.m_CalcStack.Clear();
            num = 0;
            goto Label_00D5;
        Label_002B:
            element = this.m_Elements[num];
            element.cost = 0x7fffffff;
            element.root = null;
            num2 = 0;
            goto Label_00C3;
        Label_004D:
            num3 = &(element.link[num2]).cost;
            if (moveHeight >= &(element.link[num2]).height)
            {
                goto Label_0083;
            }
            num3 += 0x2710;
            goto Label_00AD;
        Label_0083:
            if (walkableField.get(element.grid.x, element.grid.y) >= 0)
            {
                goto Label_00AD;
            }
            num3 += 0x4e20;
        Label_00AD:
            &(element.link[num2]).calc_cost = num3;
            num2 += 1;
        Label_00C3:
            if (num2 < ((int) element.link.Length))
            {
                goto Label_004D;
            }
            num += 1;
        Label_00D5:
            if (num < ((int) this.m_Elements.Length))
            {
                goto Label_002B;
            }
            return;
        }

        private unsafe void _CalcRootSubroutine(Element element)
        {
            int num;
            int num2;
            Link link;
            if (element.cost < this.m_TotalCost)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (element.link == null)
            {
                goto Label_002A;
            }
            if (((int) element.link.Length) != null)
            {
                goto Label_002B;
            }
        Label_002A:
            return;
        Label_002B:
            num = 0;
            num2 = 0;
            goto Label_00AF;
        Label_0034:
            link = *(&(element.link[num2]));
            num = element.cost + &link.calc_cost;
            if (num < &link.element.cost)
            {
                goto Label_006C;
            }
            goto Label_00AB;
        Label_006C:
            &link.element.cost = num;
            &link.element.root = element;
            this.m_CalcStack.Remove(&link.element);
            this.m_CalcStack.Add(&link.element);
        Label_00AB:
            num2 += 1;
        Label_00AF:
            if (num2 < ((int) element.link.Length))
            {
                goto Label_0034;
            }
            return;
        }

        public bool CalcRoot(int startX, int startY, int endX, int endY, int moveHeight, GridMap<int> walkableField)
        {
            Element element;
            if ((this.m_Elements != null) && (((int) this.m_Elements.Length) != null))
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            if ((startX != endX) || (startY != endY))
            {
                goto Label_002B;
            }
            return 0;
        Label_002B:
            this._CalcRootInit(moveHeight, walkableField);
            this.m_Start = this.GetElement(startX, startY);
            if (this.m_Start != null)
            {
                goto Label_0050;
            }
            return 0;
        Label_0050:
            this.m_End = this.GetElement(endX, endY);
            if (this.m_End != null)
            {
                goto Label_006C;
            }
            return 0;
        Label_006C:
            this.m_Start.cost = 0;
            this.m_CalcStack.Add(this.m_Start);
            goto Label_00CB;
        Label_008E:
            element = this.m_CalcStack[0];
            this.m_CalcStack.RemoveAt(0);
            if (element != this.m_End)
            {
                goto Label_00C4;
            }
            this.m_TotalCost = element.cost;
            goto Label_00CB;
        Label_00C4:
            this._CalcRootSubroutine(element);
        Label_00CB:
            if (this.m_CalcStack.Count > 0)
            {
                goto Label_008E;
            }
            return ((this.m_TotalCost != 0x7fffffff) ? 1 : 0);
        }

        private Element GetElement(int index)
        {
            if (index < 0)
            {
                goto Label_0015;
            }
            if (index < ((int) this.m_Elements.Length))
            {
                goto Label_0017;
            }
        Label_0015:
            return null;
        Label_0017:
            return this.m_Elements[index];
        }

        private Element GetElement(int x, int y)
        {
            if (x < 0)
            {
                goto Label_0026;
            }
            if (x >= this.m_Width)
            {
                goto Label_0026;
            }
            if (y < 0)
            {
                goto Label_0026;
            }
            if (y < this.m_Height)
            {
                goto Label_0028;
            }
        Label_0026:
            return null;
        Label_0028:
            return this.m_Elements[(y * this.m_Width) + x];
        }

        public Grid[] GetRoot()
        {
            List<Grid> list;
            Element element;
            if (this.m_Start == null)
            {
                goto Label_0016;
            }
            if (this.m_End != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return null;
        Label_0018:
            list = new List<Grid>();
            element = this.m_End;
            list.Add(element.grid);
            goto Label_004E;
        Label_0036:
            list.Add(element.root.grid);
            element = element.root;
        Label_004E:
            if (element.root != null)
            {
                goto Label_0036;
            }
            if (list.Count >= 2)
            {
                goto Label_0067;
            }
            return null;
        Label_0067:
            list.Reverse();
            if (list[0] == this.m_Start.grid)
            {
                goto Label_0086;
            }
            return null;
        Label_0086:
            return list.ToArray();
        }

        public unsafe void Initialize(int gridWidth, int gridHeight, Grid[,] gridMap)
        {
            int num;
            int num2;
            int num3;
            Grid grid;
            Element element;
            Element[] elementArray;
            int num4;
            int num5;
            int num6;
            Element element2;
            Element element3;
            int num7;
            int num8;
            this.m_Width = gridWidth;
            this.m_Height = gridHeight;
            this.m_Elements = new Element[this.m_Height * this.m_Width];
            num = 0;
            goto Label_006B;
        Label_002D:
            num2 = 0;
            goto Label_0060;
        Label_0034:
            num3 = (num * gridWidth) + num2;
            grid = gridMap[num2, num];
            element = new Element();
            element.grid = grid;
            this.m_Elements[num3] = element;
            num2 += 1;
        Label_0060:
            if (num2 < gridWidth)
            {
                goto Label_0034;
            }
            num += 1;
        Label_006B:
            if (num < gridHeight)
            {
                goto Label_002D;
            }
            elementArray = new Element[4];
            num4 = 0;
            goto Label_01EA;
        Label_0082:
            num5 = 0;
            goto Label_01DC;
        Label_008A:
            num6 = (num4 * gridWidth) + num5;
            element2 = this.m_Elements[num6];
            element3 = null;
            num7 = 0;
            if ((num5 - 1) < 0)
            {
                goto Label_00CD;
            }
            element3 = this.GetElement(num6 - 1);
            if (element3 == null)
            {
                goto Label_00CD;
            }
            elementArray[num7++] = element3;
        Label_00CD:
            if ((num5 + 1) >= gridWidth)
            {
                goto Label_00F6;
            }
            element3 = this.GetElement(num6 + 1);
            if (element3 == null)
            {
                goto Label_00F6;
            }
            elementArray[num7++] = element3;
        Label_00F6:
            if ((num4 - 1) < 0)
            {
                goto Label_011F;
            }
            element3 = this.GetElement(num6 - gridWidth);
            if (element3 == null)
            {
                goto Label_011F;
            }
            elementArray[num7++] = element3;
        Label_011F:
            if ((num4 + 1) >= gridHeight)
            {
                goto Label_0148;
            }
            element3 = this.GetElement(num6 + gridWidth);
            if (element3 == null)
            {
                goto Label_0148;
            }
            elementArray[num7++] = element3;
        Label_0148:
            element2.link = new Link[num7];
            num8 = 0;
            goto Label_01CD;
        Label_015E:
            &(element2.link[num8]).element = elementArray[num8];
            &(element2.link[num8]).cost = elementArray[num8].grid.cost;
            &(element2.link[num8]).height = elementArray[num8].grid.height - element2.grid.height;
            num8 += 1;
        Label_01CD:
            if (num8 < num7)
            {
                goto Label_015E;
            }
            num5 += 1;
        Label_01DC:
            if (num5 < gridWidth)
            {
                goto Label_008A;
            }
            num4 += 1;
        Label_01EA:
            if (num4 < gridHeight)
            {
                goto Label_0082;
            }
            return;
        }

        public void Release()
        {
            this.m_Elements = null;
            this.m_Start = null;
            this.m_End = null;
            this.m_CalcStack.Clear();
            return;
        }

        private class Element
        {
            public Grid grid;
            public BattleMapRoot.Link[] link;
            public int cost;
            public BattleMapRoot.Element root;

            public Element()
            {
                base..ctor();
                return;
            }

            public override string ToString()
            {
                object[] objArray1;
                objArray1 = new object[] { (int) this.grid.x, (int) this.grid.y, (int) this.cost, (int) ((this.link == null) ? 0 : ((int) this.link.Length)) };
                return string.Format("pos[{0:D2},{1:D2}] cost[{2}] links[{3}] root[{4}]", objArray1);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Link
        {
            public BattleMapRoot.Element element;
            public int cost;
            public int calc_cost;
            public int height;
            public override string ToString()
            {
                object[] objArray1;
                objArray1 = new object[] { (int) this.element.grid.x, (int) this.element.grid.y, (int) this.cost, (int) this.height, (int) this.calc_cost };
                return string.Format("pos[{0:D2},{1:D2}] cost[{2}] height[{3}] total[{4}]", objArray1);
            }
        }
    }
}

