using System;
using System.Drawing;

namespace CanvasGraphics {
    public interface IRenderer {
        Size Render(Color[,] cells, Graphics graphics, Point point);
    }
}
