namespace InteractiveCanvas.GUI {
    partial class CanvasViewer {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.CanvasPanel = new CanvasPanel();
            this.GlobalContainer = new System.Windows.Forms.TableLayoutPanel();
            this.GlobalContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CanvasPanel
            // 
            this.CanvasPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CanvasPanel.Canvas = null;
            this.CanvasPanel.Location = new System.Drawing.Point(429, 237);
            this.CanvasPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CanvasPanel.Name = "CanvasPanel";
            this.CanvasPanel.Size = new System.Drawing.Size(200, 200);
            this.CanvasPanel.TabIndex = 0;
            // 
            // GlobalContainer
            // 
            this.GlobalContainer.ColumnCount = 1;
            this.GlobalContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GlobalContainer.Controls.Add(this.CanvasPanel, 0, 0);
            this.GlobalContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GlobalContainer.Location = new System.Drawing.Point(0, 0);
            this.GlobalContainer.Margin = new System.Windows.Forms.Padding(0);
            this.GlobalContainer.Name = "GlobalContainer";
            this.GlobalContainer.RowCount = 1;
            this.GlobalContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GlobalContainer.Size = new System.Drawing.Size(1058, 674);
            this.GlobalContainer.TabIndex = 1;
            // 
            // CanvasViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1058, 674);
            this.Controls.Add(this.GlobalContainer);
            this.Name = "CanvasViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Canvas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CanvasViewer_FormClosing);
            this.Load += new System.EventHandler(this.CanvasViewer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CanvasViewer_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CanvasViewer_KeyUp);
            this.GlobalContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CanvasPanel CanvasPanel;
        private System.Windows.Forms.TableLayoutPanel GlobalContainer;
    }
}
