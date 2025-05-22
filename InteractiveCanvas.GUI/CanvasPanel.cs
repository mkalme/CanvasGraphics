using System;
using System.Drawing;
using System.Windows.Forms;

namespace InteractiveCanvas.GUI {
    public class CanvasPanel : Panel {
        public InteractiveCanvas Canvas { get; set; }

        private BufferedGraphics _bufferedGraphics;

        public CanvasPanel() {
            using (Graphics graphics = CreateGraphics()) {
                _bufferedGraphics = BufferedGraphicsManager.Current.Allocate(graphics, new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            Size = Canvas.CreateRenderedImage(_bufferedGraphics.Graphics, new Point(0, 0));
            _bufferedGraphics.Render(e.Graphics);
        }
    }
}
