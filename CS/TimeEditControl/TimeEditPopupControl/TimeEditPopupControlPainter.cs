using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Skins;
using System.Drawing;
using DevExpress.Utils.Drawing;

namespace TimeEditControl
{

    public class TimeEditPopupControlPainter
    {
        TimeEditPopupControlViewInfo PopupViewInfo;
        Pen drawPen = new Pen(new SolidBrush(Color.Black));

        public TimeEditPopupControlPainter(TimeEditPopupControlViewInfo viewInfo)
        {
            PopupViewInfo = viewInfo;
        }

        ~TimeEditPopupControlPainter()
        {
            drawPen = null;
            PopupViewInfo = null;
        }

        public void Draw(GraphicsCache cache)
        {
            DrawContent(cache);
        }

        private void DrawContent(GraphicsCache cache)
        {
            DrawLabels(cache);
            DrawCells(cache);
        }

        protected virtual void DrawLabels(GraphicsCache cache)
        {
            drawPen.Color =PopupViewInfo.PopupAppearance.BackColor;
            SizeF HourSize = cache.Graphics.MeasureString("Hour", PopupViewInfo.PopupAppearance.Font);
            SizeF MinuteSize = cache.Graphics.MeasureString("Minute", PopupViewInfo.PopupAppearance.Font);

            PointF HourPoint = CalcLabelPoint(PopupViewInfo.CellsList[1], HourSize);
            PointF MinutePoint = CalcLabelPoint(PopupViewInfo.CellsList[13], MinuteSize);

            cache.Graphics.DrawString("Hour", PopupViewInfo.PopupAppearance.Font, drawPen.Brush, HourPoint);
            cache.Graphics.DrawString("Minute", PopupViewInfo.PopupAppearance.Font, drawPen.Brush, MinutePoint);

        }

        protected virtual void DrawCells(GraphicsCache cache)
        {
            foreach (CellViewInfo cellInfo in PopupViewInfo.CellsList)
            {
                drawPen.Color = PopupViewInfo.PopupAppearance.BackColor;
                if (cellInfo.isSelected)
                {
                    ObjectPainter.DrawObject(cache, SkinElementPainter.Default, GetCellElementInfo(cellInfo, cache, true));
                    drawPen.Color = PopupViewInfo.PopupAppearance.BackColor2;
                }
                if (cellInfo.isHotPointed)
                {
                    ObjectPainter.DrawObject(cache, SkinElementPainter.Default, GetCellElementInfo(cellInfo, cache, false));
                    drawPen.Color = PopupViewInfo.PopupAppearance.BackColor2;
                }
                cache.Graphics.DrawString(cellInfo.CellText, cellInfo.cellTextFont,drawPen.Brush, SetTextPoint(cellInfo, cache.Graphics));
            }
        }

        private PointF SetTextPoint(CellViewInfo cellInfo, Graphics g)
        {
            SizeF textSize = g.MeasureString(cellInfo.CellText, PopupViewInfo.PopupAppearance.Font);
            float x = cellInfo.cellBounds.X + cellInfo.cellBounds.Width - 3 - textSize.Width;
            float y = cellInfo.cellBounds.Y + cellInfo.cellBounds.Height - 1 - textSize.Height;

            return new PointF(x, y);
        }

        protected virtual SkinElementInfo GetCellElementInfo(CellViewInfo cell, GraphicsCache cache, bool selected)
        {
            Skin skin = CommonSkins.GetSkin(PopupViewInfo.ownerControl.OwnerEdit.LookAndFeel.ActiveLookAndFeel);
            SkinElementInfo skinInfo;
            if (selected)
                skinInfo = new SkinElementInfo(skin[CommonSkins.SkinSelection], cell.cellBounds);
            else
                skinInfo = new SkinElementInfo(skin[CommonSkins.SkinHighlightedItem], cell.cellBounds);            
            skinInfo.Cache = cache;
            return skinInfo;
        }

        private PointF CalcLabelPoint(CellViewInfo cell, SizeF textSize)
        {
            const int indent = 2;
            float x = cell.cellBounds.X + cell.cellBounds.Width / 2 - textSize.Width / 2;
            float y = cell.cellBounds.Y - textSize.Height - indent;
            return new PointF(x, y);
        }
    }
}
