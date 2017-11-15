namespace Project1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resStationsDGV = new System.Windows.Forms.DataGridView();
            this.rsBusyCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsOpCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsVjCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsVkCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsQjCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsQkCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rsDispCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratTableDGV = new System.Windows.Forms.DataGridView();
            this.ratRFCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratRATCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instructionQueueDGV = new System.Windows.Forms.DataGridView();
            this.queueOpCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.queueDestCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.queueV1Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.queueV2Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stepButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resStationsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratTableDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instructionQueueDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(678, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // resStationsDGV
            // 
            this.resStationsDGV.AllowUserToAddRows = false;
            this.resStationsDGV.AllowUserToDeleteRows = false;
            this.resStationsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resStationsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resStationsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rsBusyCol,
            this.rsOpCol,
            this.rsVjCol,
            this.rsVkCol,
            this.rsQjCol,
            this.rsQkCol,
            this.rsDispCol});
            this.resStationsDGV.Location = new System.Drawing.Point(12, 27);
            this.resStationsDGV.Name = "resStationsDGV";
            this.resStationsDGV.ReadOnly = true;
            this.resStationsDGV.RowHeadersWidth = 60;
            this.resStationsDGV.RowTemplate.ReadOnly = true;
            this.resStationsDGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.resStationsDGV.Size = new System.Drawing.Size(576, 209);
            this.resStationsDGV.TabIndex = 1;
            // 
            // rsBusyCol
            // 
            this.rsBusyCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsBusyCol.FillWeight = 99.81239F;
            this.rsBusyCol.HeaderText = "Busy";
            this.rsBusyCol.Name = "rsBusyCol";
            this.rsBusyCol.ReadOnly = true;
            this.rsBusyCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsOpCol
            // 
            this.rsOpCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsOpCol.FillWeight = 99.84148F;
            this.rsOpCol.HeaderText = "Op";
            this.rsOpCol.Name = "rsOpCol";
            this.rsOpCol.ReadOnly = true;
            this.rsOpCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsVjCol
            // 
            this.rsVjCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsVjCol.FillWeight = 99.86642F;
            this.rsVjCol.HeaderText = "Vj";
            this.rsVjCol.Name = "rsVjCol";
            this.rsVjCol.ReadOnly = true;
            this.rsVjCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsVkCol
            // 
            this.rsVkCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsVkCol.FillWeight = 101.2021F;
            this.rsVkCol.HeaderText = "Vk";
            this.rsVkCol.Name = "rsVkCol";
            this.rsVkCol.ReadOnly = true;
            this.rsVkCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsQjCol
            // 
            this.rsQjCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsQjCol.FillWeight = 99.71875F;
            this.rsQjCol.HeaderText = "Qj";
            this.rsQjCol.Name = "rsQjCol";
            this.rsQjCol.ReadOnly = true;
            this.rsQjCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsQkCol
            // 
            this.rsQkCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsQkCol.FillWeight = 99.76121F;
            this.rsQkCol.HeaderText = "Qk";
            this.rsQkCol.Name = "rsQkCol";
            this.rsQkCol.ReadOnly = true;
            this.rsQkCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rsDispCol
            // 
            this.rsDispCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rsDispCol.FillWeight = 99.79761F;
            this.rsDispCol.HeaderText = "Disp";
            this.rsDispCol.Name = "rsDispCol";
            this.rsDispCol.ReadOnly = true;
            this.rsDispCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ratTableDGV
            // 
            this.ratTableDGV.AllowUserToAddRows = false;
            this.ratTableDGV.AllowUserToDeleteRows = false;
            this.ratTableDGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ratTableDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ratTableDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ratRFCol,
            this.ratRATCol});
            this.ratTableDGV.Location = new System.Drawing.Point(12, 242);
            this.ratTableDGV.Name = "ratTableDGV";
            this.ratTableDGV.ReadOnly = true;
            this.ratTableDGV.RowHeadersWidth = 60;
            this.ratTableDGV.ShowRowErrors = false;
            this.ratTableDGV.Size = new System.Drawing.Size(224, 237);
            this.ratTableDGV.TabIndex = 2;
            // 
            // ratRFCol
            // 
            this.ratRFCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ratRFCol.HeaderText = "RF";
            this.ratRFCol.Name = "ratRFCol";
            this.ratRFCol.ReadOnly = true;
            this.ratRFCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ratRATCol
            // 
            this.ratRATCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ratRATCol.HeaderText = "RAT";
            this.ratRATCol.Name = "ratRATCol";
            this.ratRATCol.ReadOnly = true;
            this.ratRATCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // instructionQueueDGV
            // 
            this.instructionQueueDGV.AllowUserToAddRows = false;
            this.instructionQueueDGV.AllowUserToDeleteRows = false;
            this.instructionQueueDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionQueueDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instructionQueueDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.queueOpCol,
            this.queueDestCol,
            this.queueV1Col,
            this.queueV2Col});
            this.instructionQueueDGV.Location = new System.Drawing.Point(242, 242);
            this.instructionQueueDGV.Name = "instructionQueueDGV";
            this.instructionQueueDGV.ReadOnly = true;
            this.instructionQueueDGV.RowHeadersWidth = 50;
            this.instructionQueueDGV.ShowRowErrors = false;
            this.instructionQueueDGV.Size = new System.Drawing.Size(346, 236);
            this.instructionQueueDGV.TabIndex = 3;
            // 
            // queueOpCol
            // 
            this.queueOpCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.queueOpCol.HeaderText = "Op";
            this.queueOpCol.Name = "queueOpCol";
            this.queueOpCol.ReadOnly = true;
            this.queueOpCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // queueDestCol
            // 
            this.queueDestCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.queueDestCol.HeaderText = "Destination Register";
            this.queueDestCol.Name = "queueDestCol";
            this.queueDestCol.ReadOnly = true;
            this.queueDestCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // queueV1Col
            // 
            this.queueV1Col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.queueV1Col.HeaderText = "Value1";
            this.queueV1Col.Name = "queueV1Col";
            this.queueV1Col.ReadOnly = true;
            this.queueV1Col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // queueV2Col
            // 
            this.queueV2Col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.queueV2Col.HeaderText = "Value2";
            this.queueV2Col.Name = "queueV2Col";
            this.queueV2Col.ReadOnly = true;
            this.queueV2Col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // stepButton
            // 
            this.stepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stepButton.Enabled = false;
            this.stepButton.Location = new System.Drawing.Point(594, 455);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 23);
            this.stepButton.TabIndex = 4;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(594, 429);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(75, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(594, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cycle:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 494);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.instructionQueueDGV);
            this.Controls.Add(this.ratTableDGV);
            this.Controls.Add(this.resStationsDGV);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Project1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resStationsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ratTableDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instructionQueueDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridView resStationsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsBusyCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsOpCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsVjCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsVkCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsQjCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsQkCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsDispCol;
        private System.Windows.Forms.DataGridView ratTableDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratRFCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratRATCol;
        private System.Windows.Forms.DataGridView instructionQueueDGV;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn queueOpCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn queueDestCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn queueV1Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn queueV2Col;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

