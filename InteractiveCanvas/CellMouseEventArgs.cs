using System;
using System.Drawing;
using System.Windows.Forms;

namespace InteractiveCanvas {
    public class CellMouseEventArgs : EventArgs {
        public Point Location { get; private set; }
        public int X => Location.X;
        public int Y => Location.Y;

        public MouseButtons MouseButtons { get; private set; }

        public CellMouseEventArgs(Point location, MouseButtons mouseButtons) { 
            Location = location;
            MouseButtons = mouseButtons;
        }
    }
}
