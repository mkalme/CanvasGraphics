using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace InteractiveCanvas.Simple {
    public class SimpleInteractiveCanvas : InteractiveCanvas {
        public Color BackColor { get; set; }
        public Color MouseOverColor { get; set; }
        public Color SelectionColor { get; set; }

        private Random _random = new Random();
        private float _brightness = 1;
        private bool _randomize = false;
        private int _radius = 2;

        private bool[,] _selected;

        private Thread _loopThread;
        private bool _run = true;

        public SimpleInteractiveCanvas() {
            BackColor = Color.FromArgb(60, 60, 60);
            MouseOverColor = Color.FromArgb(100, 100, 100);
            SelectionColor = Color.White;
        }

        public override void OnCreate(Color[,] cells) {
            PaintCanvas(cells);
            base.OnCreate(cells);

            _selected = new bool[cells.GetLength(0), cells.GetLength(1)];

            OnCellMouseDown += Canvas_OnMouseDown;
            OnCellMouseUp += Canvas_OnMouseUp;
            OnCellMouseEnter += Canvas_OnMouseEnter;
            OnCellMouseLeave += Canvas_OnMouseLeave;

            OnKeyDown += Canvas_OnKeyDown;

            StartGameLoop();
        }

        private void StartGameLoop() {
            _loopThread = new Thread(() => {
                while (_run) {
                    RaiseOnCanvasUpdate();
                }

                RaiseOnDisposed();
            });

            _loopThread.Start();
        }

        private void PaintCanvas(Color[,] cells) {
            Parallel.For(0, cells.GetLength(1), y => {
                for (int x = 0; x < cells.GetLength(0); x++) {
                    if (_randomize) {
                        cells[x, y] = CreateRandomColor();
                    } else {
                        cells[x, y] = BackColor;
                    }
                }
            });
        }
        private Color CreateRandomColor() {
            Color output = Color.FromArgb(_random.Next());
            return Color.FromArgb((int)(output.R * _brightness), (int)(output.G * _brightness), (int)(output.B * _brightness));
        }

        private bool _mouseDown = false;
        private void Canvas_OnMouseDown(object sender, CellMouseEventArgs e) {
            _mouseDown = true;
            ActivateSelection(e);
        }
        private void Canvas_OnMouseUp(object sender, CellMouseEventArgs e) {
            _mouseDown = false;
        }
        private void Canvas_OnMouseEnter(object sender, CellMouseEventArgs e) {
            if (_mouseDown) {
                ActivateSelection(e);
            } else if (IsInCanvas(e.Location) && !_selected[e.X, e.Y]) {
                Cells[e.X, e.Y] = MouseOverColor;
            }
        }
        private void Canvas_OnMouseLeave(object sender, CellMouseEventArgs e) {
            if (!IsInCanvas(e.Location) || _selected[e.X, e.Y]) return;
            Cells[e.X, e.Y] = BackColor;
        }

        private void ActivateSelection(CellMouseEventArgs e) {
            for (int y = -_radius; y < _radius + 1; y++) {
                for (int x = -_radius; x < _radius + 1; x++) {
                    SelectCell(e.X + x, e.Y + y, e.MouseButtons);
                }
            }
        }
        private void SelectCell(int x, int y, MouseButtons mouseButtons) {
            if (!IsInCanvas(new Point(x, y))) return;
            if (_selected[x, y] && mouseButtons == MouseButtons.Left) return;

            _selected[x, y] = mouseButtons == MouseButtons.Left;
            Cells[x, y] = _selected[x, y] ? (_randomize ? CreateRandomColor() : SelectionColor) : BackColor;
        }

        private bool IsInCanvas(Point point) { 
            return point.X >= 0 && point.Y >= 0 && point.X < Cells.GetLength(0) && point.Y < Cells.GetLength(1);
        }

        private void Canvas_OnKeyDown(object sender, KeyEventArgs e) {
            _randomize ^= e.KeyCode == Keys.G;

            switch (e.KeyCode) {
                case Keys.D1:
                    _brightness = 0.2F;
                    break;
                case Keys.D2:
                    _brightness = 0.4F;
                    break;
                case Keys.D3:
                    _brightness = 0.6F;
                    break;
                case Keys.D4:
                    _brightness = 0.8F;
                    break;
                case Keys.D5:
                    _brightness = 1F;
                    break;
                case Keys.Add:
                    _radius++;
                    break;
                case Keys.Subtract:
                    _radius--;
                    break;
            }
        }

        public override void Dispose() {
            _run = false;
        }
    }
}
