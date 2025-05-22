using System;
using System.Drawing;
using System.Threading.Tasks;

namespace CanvasGraphics {
    public class Renderer : IRenderer {
        public virtual Size CellSize { get; set; }
        public virtual Color GridColor { get; set; }
        public virtual int GridWidth { get; set; }

        private Color[,] _cache;
        private Color _gridColorCache;

        public Renderer() {
            CellSize = new Size(10, 10);
            GridColor = Color.Black;
            GridWidth = 1;
        }

        public Size Render(Color[,] cells, Graphics graphics, Point point) {
            int width = cells.GetLength(0) * CellSize.Width + (cells.GetLength(0) + 1) * GridWidth;
            int height = cells.GetLength(1) * CellSize.Height + (cells.GetLength(1) + 1) * GridWidth;

            DrawGrid(cells, graphics, point, width, height);
            DrawCells(graphics, point, cells);

            return new Size(width, height);
        }

        protected virtual void DrawGrid(Color[,] cells, Graphics graphics, Point point, int width, int height) {
            if ((_cache != null && _gridColorCache == GridColor) || GridWidth < 1) return;

            SolidBrush brush = new SolidBrush(GridColor);

            for (int y = 0; y < cells.GetLength(1) + 1; y++) {
                int yPoint = y * CellSize.Height + GridWidth * y + point.Y;
                graphics.FillRectangle(brush, point.X, yPoint, width, GridWidth);
            }

            for (int x = 0; x < cells.GetLength(0) + 1; x++) {
                int xPoint = x * CellSize.Width + GridWidth * x + point.X;
                graphics.FillRectangle(brush, xPoint, point.Y, GridWidth, height);
            }

            _gridColorCache = GridColor;
        }
        protected virtual void DrawCells(Graphics graphics, Point point, Color[,] cells) {
            for (int y = 0; y < cells.GetLength(1); y++) {
                int yPoint = y * CellSize.Height + GridWidth * y + GridWidth;

                for (int x = 0; x < cells.GetLength(0); x++) {
                    int xPoint = x * CellSize.Width + GridWidth * x + GridWidth;

                    if (_cache == null || _cache[x, y] != cells[x, y]) {
                        graphics.FillRectangle(new SolidBrush(cells[x, y]), xPoint + point.X, yPoint + point.Y, CellSize.Width, CellSize.Height);
                    }
                }
            }

            _cache = CloneCells(cells);
        }

        private static Color[,] CloneCells(Color[,] cells) {
            Color[,] output = new Color[cells.GetLength(0), cells.GetLength(1)];

            Parallel.For(0, cells.GetLength(1), y => {
                for (int x = 0; x < cells.GetLength(0); x++) {
                    output[x, y] = cells[x, y];
                }
            });

            return output;
        }
    }
}
