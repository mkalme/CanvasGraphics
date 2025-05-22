using System;
using System.Windows.Forms;

namespace InteractiveCanvas.GUI {
    public class ControlMouse {
        public CellPointer CellPointer { get; }
        public Control MainControl { get; }

        public ControlMouse(CellPointer cellPointer, Control mainControl, params Control[] controls) { 
            CellPointer = cellPointer;
            MainControl = mainControl;

            MainControl.MouseEnter += MainControl_MouseEnter;
            MainControl.MouseLeave += MainControl_MouseLeave;
            MainControl.MouseDown += MainControl_MouseDown;
            MainControl.MouseUp += Control_MouseUp;
            MainControl.MouseMove += MainControl_MouseMove;

            foreach (Control control in controls) {
                control.MouseDown += Control_MouseDown;
                control.MouseUp += Control_MouseUp;
                control.MouseMove += Control_MouseMove;
            }
        }

        private void MainControl_MouseEnter(object sender, EventArgs e) {
            CellPointer.Canvas.RaiseOnCanvasEnter();
        }
        private void MainControl_MouseLeave(object sender, EventArgs e) {
            CellPointer.Canvas.RaiseOnCanvasLeave();
        }
        private void MainControl_MouseDown(object sender, MouseEventArgs e) {
            CellPointer.RaiseMouseDown(e.Location, e.Button);
        }
        private void MainControl_MouseMove(object sender, MouseEventArgs e) {
            CellPointer.RaiseMouseMove(e.Location, e.Button);
        }

        private void Control_MouseDown(object sender, MouseEventArgs e) {
            CellPointer.RaiseMouseDown(MainControl.PointToClient(Cursor.Position), e.Button);
        }
        private void Control_MouseUp(object sender, MouseEventArgs e) {
            CellPointer.RaiseMouseUp(e.Button);
        }
        private void Control_MouseMove(object sender, MouseEventArgs e) {
            CellPointer.RaiseMouseMove(MainControl.PointToClient(Cursor.Position), e.Button);
        }
    }
}
