namespace sterowanieSilnikami
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gearForwardNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.gearBackwardNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.usbResponseTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gearForwardNumberUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearBackwardNumberUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ffToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // ffToolStripMenuItem
            // 
            this.ffToolStripMenuItem.Name = "ffToolStripMenuItem";
            this.ffToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F15;
            this.ffToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.ffToolStripMenuItem.Text = "ff";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "bieg 0";
            // 
            // gearForwardNumberUpDown
            // 
            this.gearForwardNumberUpDown.Location = new System.Drawing.Point(25, 71);
            this.gearForwardNumberUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.gearForwardNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gearForwardNumberUpDown.Name = "gearForwardNumberUpDown";
            this.gearForwardNumberUpDown.Size = new System.Drawing.Size(46, 20);
            this.gearForwardNumberUpDown.TabIndex = 4;
            this.gearForwardNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gearBackwardNumberUpDown
            // 
            this.gearBackwardNumberUpDown.Location = new System.Drawing.Point(25, 97);
            this.gearBackwardNumberUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.gearBackwardNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gearBackwardNumberUpDown.Name = "gearBackwardNumberUpDown";
            this.gearBackwardNumberUpDown.Size = new System.Drawing.Size(46, 20);
            this.gearBackwardNumberUpDown.TabIndex = 4;
            this.gearBackwardNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // usbResponseTb
            // 
            this.usbResponseTb.Location = new System.Drawing.Point(135, 47);
            this.usbResponseTb.Multiline = true;
            this.usbResponseTb.Name = "usbResponseTb";
            this.usbResponseTb.ReadOnly = true;
            this.usbResponseTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.usbResponseTb.Size = new System.Drawing.Size(261, 96);
            this.usbResponseTb.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Odpowiedź USB";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usbResponseTb);
            this.Controls.Add(this.gearBackwardNumberUpDown);
            this.Controls.Add(this.gearForwardNumberUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gearForwardNumberUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gearBackwardNumberUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ffToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown gearForwardNumberUpDown;
        private System.Windows.Forms.NumericUpDown gearBackwardNumberUpDown;
        private System.Windows.Forms.TextBox usbResponseTb;
        private System.Windows.Forms.Label label2;
    }
}

