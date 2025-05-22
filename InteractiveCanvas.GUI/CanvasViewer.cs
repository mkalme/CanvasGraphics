using System;
using System.Drawing;
using System.Windows.Forms;
using InteractiveCanvas.Simple;
using CanvasGraphics;
using CustomDialogs;

namespace InteractiveCanvas.GUI {
    public partial class CanvasViewer : Form {
        public InteractiveCanvas Canvas { get; set; }
        public CellPointer CellPointer { get; set; }
        public ControlMouse ControlMouse { get; set; }

        private bool _disposed = false;

        public CanvasViewer() {
            InitializeComponent();

            CanvasPanel.SetDoubleBuffering();

            Canvas = new SimpleInteractiveCanvas();
            Canvas.OnCanvasUpdate += Canvas_OnUpdate;
            Canvas.OnDisposed += Canvas_OnDisposed;

            CanvasPanel.Canvas = Canvas;
        }
        private void CanvasViewer_Load(object sender, EventArgs e) {
            Canvas.OnCreate(new Color[150, 90]);
            CellPointer = new CellPointer(Canvas, Canvas.Renderer as Renderer);
            ControlMouse = new ControlMouse(CellPointer, CanvasPanel, this, GlobalContainer);

            WindowState = FormWindowState.Maximized;
        }
        private void CanvasViewer_FormClosing(object sender, FormClosingEventArgs e) {
            if (!_disposed) {
                e.Cancel = true;
                Canvas.Dispose();
            }
        }

        private int _fps = 0;
        private DateTime _time = DateTime.Now;
        private void Canvas_OnUpdate(object sender, EventArgs e) {
            Invoke(new Action(() => {
                CanvasPanel.Invalidate();

                _fps++;
                if ((DateTime.Now - _time).TotalSeconds >= 1L) {
                    Text = $"Canvas | {_fps / ((DateTime.Now - _time).TotalSeconds)} FPS";

                    _fps = 0;
                    _time = DateTime.Now;
                }
            }));
        }
        private void Canvas_OnDisposed(object sender, EventArgs e) {
            Invoke(new Action(() => {
                _disposed = true;
                Close();
            }));
        }

        private void CanvasViewer_KeyDown(object sender, KeyEventArgs e) {
            Canvas.RaiseOnKeyDown(e);
        }
        private void CanvasViewer_KeyUp(object sender, KeyEventArgs e) {
            Canvas.RaiseOnKeyUp(e);
        }
    }
}
