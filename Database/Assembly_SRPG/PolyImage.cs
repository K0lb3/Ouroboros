namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PolyImage : Image
    {
        public Quad[] Quads;
        public bool Transparent;
        private RectTransform mRectTransform;

        public PolyImage()
        {
            this.Quads = new Quad[0];
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.mRectTransform = base.GetComponent<RectTransform>();
            return;
        }

        protected override unsafe void OnPopulateMesh(VertexHelper vh)
        {
            UIVertex vertex;
            Color32 color;
            Rect rect;
            Rect rect2;
            int num;
            Sprite sprite;
            float num2;
            float num3;
            Rect rect3;
            int num4;
            int num5;
            Color color2;
            vh.Clear();
            if ((base.get_sprite() == null) == null)
            {
                goto Label_0022;
            }
            if (this.Transparent != null)
            {
                goto Label_003B;
            }
        Label_0022:
            if (&base.get_color().a > 0f)
            {
                goto Label_003C;
            }
        Label_003B:
            return;
        Label_003C:
            vertex = new UIVertex();
            color = base.get_color();
            rect = this.mRectTransform.get_rect();
            num = 0;
            sprite = base.get_sprite();
            if ((sprite != null) == null)
            {
                goto Label_00DB;
            }
            num2 = 1f / ((float) sprite.get_texture().get_width());
            num3 = 1f / ((float) sprite.get_texture().get_height());
            rect3 = sprite.get_rect();
            &rect2..ctor(&rect3.get_x() * num2, &rect3.get_y() * num3, &rect3.get_width() * num2, &rect3.get_height() * num3);
            goto Label_00F6;
        Label_00DB:
            &rect2..ctor(0f, 0f, 1f, 1f);
        Label_00F6:
            if (&color.r != 0xff)
            {
                goto Label_0564;
            }
            if (&color.g != 0xff)
            {
                goto Label_0564;
            }
            if (&color.b != 0xff)
            {
                goto Label_0564;
            }
            if (&color.a != 0xff)
            {
                goto Label_0564;
            }
            num4 = ((int) this.Quads.Length) - 1;
            goto Label_0557;
        Label_014B:
            float introduced12 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced12, &rect.get_xMax(), &&(this.Quads[num4]).v0.x);
            float introduced13 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced13, &rect.get_yMax(), &&(this.Quads[num4]).v0.y);
            &vertex.color = &(this.Quads[num4]).c0;
            float introduced14 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced14, &rect2.get_xMax(), &&(this.Quads[num4]).v0.x);
            float introduced15 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced15, &rect2.get_yMax(), &&(this.Quads[num4]).v0.y);
            vh.AddVert(vertex);
            float introduced16 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced16, &rect.get_xMax(), &&(this.Quads[num4]).v1.x);
            float introduced17 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced17, &rect.get_yMax(), &&(this.Quads[num4]).v1.y);
            &vertex.color = &(this.Quads[num4]).c1;
            float introduced18 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced18, &rect2.get_xMax(), &&(this.Quads[num4]).v1.x);
            float introduced19 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced19, &rect2.get_yMax(), &&(this.Quads[num4]).v1.y);
            vh.AddVert(vertex);
            float introduced20 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced20, &rect.get_xMax(), &&(this.Quads[num4]).v2.x);
            float introduced21 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced21, &rect.get_yMax(), &&(this.Quads[num4]).v2.y);
            &vertex.color = &(this.Quads[num4]).c2;
            float introduced22 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced22, &rect2.get_xMax(), &&(this.Quads[num4]).v2.x);
            float introduced23 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced23, &rect2.get_yMax(), &&(this.Quads[num4]).v2.y);
            vh.AddVert(vertex);
            float introduced24 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced24, &rect.get_xMax(), &&(this.Quads[num4]).v3.x);
            float introduced25 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced25, &rect.get_yMax(), &&(this.Quads[num4]).v3.y);
            &vertex.color = &(this.Quads[num4]).c3;
            float introduced26 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced26, &rect2.get_xMax(), &&(this.Quads[num4]).v3.x);
            float introduced27 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced27, &rect2.get_yMax(), &&(this.Quads[num4]).v3.y);
            vh.AddVert(vertex);
            vh.AddTriangle(num, num + 1, num + 2);
            vh.AddTriangle(num + 2, num + 3, num);
            num += 4;
            num4 -= 1;
        Label_0557:
            if (num4 >= 0)
            {
                goto Label_014B;
            }
            goto Label_0C45;
        Label_0564:
            num5 = ((int) this.Quads.Length) - 1;
            goto Label_0C3D;
        Label_0575:
            float introduced28 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced28, &rect.get_xMax(), &&(this.Quads[num5]).v0.x);
            float introduced29 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced29, &rect.get_yMax(), &&(this.Quads[num5]).v0.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num5]).c0.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num5]).c0.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num5]).c0.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num5]).c0.a * &color.a) / 0xff);
            float introduced30 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced30, &rect2.get_xMax(), &&(this.Quads[num5]).v0.x);
            float introduced31 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced31, &rect2.get_yMax(), &&(this.Quads[num5]).v0.y);
            vh.AddVert(vertex);
            float introduced32 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced32, &rect.get_xMax(), &&(this.Quads[num5]).v1.x);
            float introduced33 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced33, &rect.get_yMax(), &&(this.Quads[num5]).v1.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num5]).c1.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num5]).c1.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num5]).c1.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num5]).c1.a * &color.a) / 0xff);
            float introduced34 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced34, &rect2.get_xMax(), &&(this.Quads[num5]).v1.x);
            float introduced35 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced35, &rect2.get_yMax(), &&(this.Quads[num5]).v1.y);
            vh.AddVert(vertex);
            float introduced36 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced36, &rect.get_xMax(), &&(this.Quads[num5]).v2.x);
            float introduced37 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced37, &rect.get_yMax(), &&(this.Quads[num5]).v2.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num5]).c2.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num5]).c2.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num5]).c2.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num5]).c2.a * &color.a) / 0xff);
            float introduced38 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced38, &rect2.get_xMax(), &&(this.Quads[num5]).v2.x);
            float introduced39 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced39, &rect2.get_yMax(), &&(this.Quads[num5]).v2.y);
            vh.AddVert(vertex);
            float introduced40 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced40, &rect.get_xMax(), &&(this.Quads[num5]).v3.x);
            float introduced41 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced41, &rect.get_yMax(), &&(this.Quads[num5]).v3.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num5]).c3.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num5]).c3.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num5]).c3.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num5]).c3.a * &color.a) / 0xff);
            float introduced42 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced42, &rect2.get_xMax(), &&(this.Quads[num5]).v3.x);
            float introduced43 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced43, &rect2.get_yMax(), &&(this.Quads[num5]).v3.y);
            vh.AddVert(vertex);
            vh.AddTriangle(num, num + 1, num + 2);
            vh.AddTriangle(num + 2, num + 3, num);
            num += 4;
            num5 -= 1;
        Label_0C3D:
            if (num5 >= 0)
            {
                goto Label_0575;
            }
        Label_0C45:
            return;
        }
    }
}

