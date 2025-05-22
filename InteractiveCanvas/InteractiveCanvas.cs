using System;
using System.Drawing;
using System.Windows.Forms;
using CanvasGraphics;

namespace InteractiveCanvas {
    public abstract class InteractiveCanvas : IDisposable {
        public Color[,] Cells { get; private set; }
        public virtual IRenderer Renderer { get; private set; }

        public event EventHandler OnCanvasUpdate;
        public event EventHandler OnCanvasMouseEnter;
        public event EventHandler OnCanvasMouseLeave;

        public event EventHandler<CellMouseEventArgs> OnCellMouseEnter;
        public event EventHandler<CellMouseEventArgs> OnCellMouseLeave;
        public event EventHandler<CellMouseEventArgs> OnCellMouseDown;
        public event EventHandler<CellMouseEventArgs> OnCellMouseUp;
        
        public event EventHandler<KeyEventArgs> OnKeyDown;
        public event EventHandler<KeyEventArgs> OnKeyUp;
        
        public event EventHandler OnDisposed;

        public virtual void OnCreate(Color[,] cells) {
            Renderer = new Renderer();
            Cells = cells;

            RaiseOnCanvasUpdate();
        }

        protected virtual void RaiseOnCanvasUpdate() {
            OnCanvasUpdate?.Invoke(this, EventArgs.Empty);
        }
        public virtual Size CreateRenderedImage(Graphics graphics, Point point) {
            return Renderer.Render(Cells, graphics, point);
        }

        public virtual void RaiseOnCanvasEnter() { 
            OnCanvasMouseEnter?.Invoke(this, EventArgs.Empty);
        }
        public virtual void RaiseOnCanvasLeave() {
            OnCanvasMouseLeave?.Invoke(this, EventArgs.Empty);
        }

        public virtual void RaiseOnCellMouseEnter(CellMouseEventArgs e) { 
            OnCellMouseEnter?.Invoke(this, e);
        }
        public virtual void RaiseOnCellMouseLeave(CellMouseEventArgs e) {
            OnCellMouseLeave?.Invoke(this, e);
        }
        public virtual void RaiseOnCellMouseDown(CellMouseEventArgs e) {
            OnCellMouseDown?.Invoke(this, e);
        }
        public virtual void RaiseOnCellMouseUp(CellMouseEventArgs e) {
            OnCellMouseUp?.Invoke(this, e);
        }

        public virtual void RaiseOnKeyDown(KeyEventArgs e) {
            OnKeyDown?.Invoke(this, e);
        }
        public virtual void RaiseOnKeyUp(KeyEventArgs e) {
            OnKeyUp?.Invoke(this, e);
        }

        public virtual void RaiseOnDisposed() {
            OnDisposed?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Dispose();
    }
}
