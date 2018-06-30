namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    internal class GridMeshGenerator
    {
        private const float MinNormalThreshold = 0.5f;
        private List<Vector3> mVerts;
        private List<int> mIndices;

        public GridMeshGenerator()
        {
            this.mVerts = new List<Vector3>(0x100);
            this.mIndices = new List<int>(0x100);
            base..ctor();
            return;
        }

        public unsafe void AddMesh(Mesh mesh, Matrix4x4 matrix, Rect clipRect, bool mirror)
        {
            Vector3[] vectorArray;
            int num;
            int num2;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            int num3;
            Vector3 vector4;
            Vector3 vector5;
            Vector3 vector6;
            Vector3 vector7;
            Vector3 vector8;
            vectorArray = mesh.get_vertices();
            num = ((int) vectorArray.Length) - 1;
            goto Label_0035;
        Label_0012:
            *(&(vectorArray[num])) = &matrix.MultiplyPoint(*(&(vectorArray[num])));
            num -= 1;
        Label_0035:
            if (num >= 0)
            {
                goto Label_0012;
            }
            if (mirror == null)
            {
                goto Label_012D;
            }
            num2 = ((int) mesh.get_triangles().Length) - 3;
            goto Label_0121;
        Label_0053:
            vector = *(&(vectorArray[mesh.get_triangles()[num2 + 2]])) - *(&(vectorArray[mesh.get_triangles()[num2]]));
            vector2 = *(&(vectorArray[mesh.get_triangles()[num2 + 1]])) - *(&(vectorArray[mesh.get_triangles()[num2]]));
            if (Vector3.Dot(&Vector3.Cross(vector, vector2).get_normalized(), Vector3.get_up()) <= 0.5f)
            {
                goto Label_011D;
            }
            this.AddTriangle(*(&(vectorArray[mesh.get_triangles()[num2 + 2]])), *(&(vectorArray[mesh.get_triangles()[num2 + 1]])), *(&(vectorArray[mesh.get_triangles()[num2]])), clipRect);
        Label_011D:
            num2 -= 3;
        Label_0121:
            if (num2 >= 0)
            {
                goto Label_0053;
            }
            goto Label_021F;
        Label_012D:
            num3 = ((int) mesh.get_triangles().Length) - 3;
            goto Label_0217;
        Label_013E:
            vector4 = *(&(vectorArray[mesh.get_triangles()[num3 + 1]])) - *(&(vectorArray[mesh.get_triangles()[num3]]));
            vector5 = *(&(vectorArray[mesh.get_triangles()[num3 + 2]])) - *(&(vectorArray[mesh.get_triangles()[num3]]));
            if (Vector3.Dot(&Vector3.Cross(vector4, vector5).get_normalized(), Vector3.get_up()) <= 0.5f)
            {
                goto Label_0211;
            }
            this.AddTriangle(*(&(vectorArray[mesh.get_triangles()[num3]])), *(&(vectorArray[mesh.get_triangles()[num3 + 1]])), *(&(vectorArray[mesh.get_triangles()[num3 + 2]])), clipRect);
        Label_0211:
            num3 -= 3;
        Label_0217:
            if (num3 >= 0)
            {
                goto Label_013E;
            }
        Label_021F:
            return;
        }

        public unsafe void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Rect rect)
        {
            Vector3 vector;
            Vector3 vector2;
            int num;
            int num2;
            int num3;
            vector = Vector3.Min(Vector3.Min(v0, v1), v2);
            vector2 = Vector3.Max(Vector3.Max(v0, v1), v2);
            if (&vector2.x < &rect.get_xMin())
            {
                goto Label_0068;
            }
            if (&rect.get_xMax() < &vector.x)
            {
                goto Label_0068;
            }
            if (&vector2.z < &rect.get_yMin())
            {
                goto Label_0068;
            }
            if (&rect.get_yMax() >= &vector.z)
            {
                goto Label_0069;
            }
        Label_0068:
            return;
        Label_0069:
            num = this.AddVertex(v0);
            num2 = this.AddVertex(v1);
            num3 = this.AddVertex(v2);
            this.mIndices.Add(num);
            this.mIndices.Add(num2);
            this.mIndices.Add(num3);
            return;
        }

        private int AddVertex(Vector3 v)
        {
            int num;
            num = this.mVerts.Count - 1;
            goto Label_0030;
        Label_0013:
            if ((this.mVerts[num] == v) == null)
            {
                goto Label_002C;
            }
            return num;
        Label_002C:
            num -= 1;
        Label_0030:
            if (num >= 0)
            {
                goto Label_0013;
            }
            this.mVerts.Add(v);
            return (this.mVerts.Count - 1);
        }

        public void Clear()
        {
            this.mVerts.Clear();
            this.mIndices.Clear();
            return;
        }

        public unsafe Mesh CreateMesh()
        {
            Vector2[] vectorArray;
            int num;
            Mesh mesh;
            Vector3 vector;
            Vector3 vector2;
            vectorArray = new Vector2[this.mVerts.Count];
            num = this.mVerts.Count - 1;
            goto Label_0069;
        Label_0024:
            vector = this.mVerts[num];
            &(vectorArray[num]).x = &vector.x;
            vector2 = this.mVerts[num];
            &(vectorArray[num]).y = &vector2.z;
            num -= 1;
        Label_0069:
            if (num >= 0)
            {
                goto Label_0024;
            }
            mesh = new Mesh();
            mesh.set_vertices(this.mVerts.ToArray());
            mesh.set_triangles(this.mIndices.ToArray());
            mesh.set_uv(vectorArray);
            mesh.UploadMeshData(1);
            return mesh;
        }
    }
}

