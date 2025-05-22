using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CanvasGraphics;

namespace InteractiveCanvas.GUI {
    public class CellPointer {
        public InteractiveCanvas Canvas { get; set; }
        public Renderer Renderer { get; set; }
        public bool InUpdate { get; private set; }

        protected Point DownCell;

        protected bool IsInCell = false;
        protected Point PrevCell;
        protected Point PrevPoint = Point.Empty;

        public CellPointer(InteractiveCanvas canvas, Renderer renderer) {
            Canvas = canvas;
            Renderer = renderer;
            InUpdate = false;
        }

        public virtual void RaiseMouseDown(Point point, MouseButtons mouseButtons) {
            DownCell = GetCell(point);
            Canvas.RaiseOnCellMouseDown(new CellMouseEventArgs(DownCell, mouseButtons));
        }
        public virtual void RaiseMouseUp(MouseButtons mouseButtons) {
            Canvas.RaiseOnCellMouseUp(new CellMouseEventArgs(DownCell, mouseButtons));
        }

        public virtual void RaiseMouseMove(Point point, MouseButtons mouseButtons) {
            if (PrevPoint.IsEmpty) PrevPoint = point;

            foreach (Point inPoint in DrawLine(point.X, point.Y, PrevPoint.X, PrevPoint.Y)) {
                Point cell = GetCell(inPoint);

                if (!IsInCell) {
                    IsInCell = true;
                    PrevCell = cell;
                } else if (PrevCell != cell) {
                    Canvas.RaiseOnCellMouseLeave(new CellMouseEventArgs(PrevCell, mouseButtons));
                    PrevCell = cell;
                }

                Canvas.RaiseOnCellMouseEnter(new CellMouseEventArgs(cell, mouseButtons));
            }

            PrevPoint = point;
        }

        protected virtual Point GetCell(Point point) {
            Size size = new Size(Renderer.CellSize.Width + Renderer.GridWidth, Renderer.CellSize.Height + Renderer.GridWidth);
            return new Point((int)Math.Floor(point.X / (float)size.Width), (int)Math.Floor(point.Y / (float)size.Height));
        }
        protected virtual IEnumerable<Point> DrawLine(int x, int y, int x2, int y2) {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

            if (w < 0) dx1 = -1;
            else if (w > 0) dx1 = 1;

            if (h < 0) dy1 = -1;
            else if (h > 0) dy1 = 1;

            if (w < 0) dx2 = -1;
            else if (w > 0) dx2 = 1;

            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);

            if (!(longest > shortest)) {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);

                if (h < 0) dy2 = -1;
                else if (h > 0) dy2 = 1;
                dx2 = 0;
            }

            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++) {
                yield return new Point(x, y);

                numerator += shortest;
                if (!(numerator < longest)) {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                } else {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}
