namespace DB_Yelp
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
            this.comboBoxQuery = new System.Windows.Forms.ComboBox();
            this.comboBoxGiven = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblLat = new System.Windows.Forms.Label();
            this.lblLong = new System.Windows.Forms.Label();
            this.tbxLatitude = new System.Windows.Forms.TextBox();
            this.tbxLongitude = new System.Windows.Forms.TextBox();
            this.GEBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridResults = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxQuery
            // 
            this.comboBoxQuery.FormattingEnabled = true;
            this.comboBoxQuery.Location = new System.Drawing.Point(247, 12);
            this.comboBoxQuery.Name = "comboBoxQuery";
            this.comboBoxQuery.Size = new System.Drawing.Size(979, 21);
            this.comboBoxQuery.TabIndex = 1;
            this.comboBoxQuery.SelectedIndexChanged += new System.EventHandler(this.comboBoxQuery_SelectedIndexChanged);
            // 
            // comboBoxGiven
            // 
            this.comboBoxGiven.FormattingEnabled = true;
            this.comboBoxGiven.Location = new System.Drawing.Point(28, 12);
            this.comboBoxGiven.Name = "comboBoxGiven";
            this.comboBoxGiven.Size = new System.Drawing.Size(196, 21);
            this.comboBoxGiven.TabIndex = 2;
            this.comboBoxGiven.SelectedIndexChanged += new System.EventHandler(this.comboBoxGiven_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1245, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Google Earth";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.BackColor = System.Drawing.Color.Transparent;
            this.lblLat.ForeColor = System.Drawing.Color.Silver;
            this.lblLat.Location = new System.Drawing.Point(28, 40);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(48, 13);
            this.lblLat.TabIndex = 6;
            this.lblLat.Text = "Latitude:";
            // 
            // lblLong
            // 
            this.lblLong.AutoSize = true;
            this.lblLong.BackColor = System.Drawing.Color.Transparent;
            this.lblLong.ForeColor = System.Drawing.Color.Silver;
            this.lblLong.Location = new System.Drawing.Point(189, 40);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new System.Drawing.Size(57, 13);
            this.lblLong.TabIndex = 7;
            this.lblLong.Text = "Longitude:";
            // 
            // tbxLatitude
            // 
            this.tbxLatitude.Location = new System.Drawing.Point(82, 37);
            this.tbxLatitude.Name = "tbxLatitude";
            this.tbxLatitude.Size = new System.Drawing.Size(100, 20);
            this.tbxLatitude.TabIndex = 8;
            // 
            // tbxLongitude
            // 
            this.tbxLongitude.Location = new System.Drawing.Point(247, 37);
            this.tbxLongitude.Name = "tbxLongitude";
            this.tbxLongitude.Size = new System.Drawing.Size(100, 20);
            this.tbxLongitude.TabIndex = 9;
            // 
            // GEBrowser
            // 
            this.GEBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GEBrowser.Location = new System.Drawing.Point(0, 0);
            this.GEBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.GEBrowser.Name = "GEBrowser";
            this.GEBrowser.Size = new System.Drawing.Size(874, 608);
            this.GEBrowser.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(28, 68);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridResults);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GEBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1314, 608);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.TabIndex = 4;
            // 
            // dataGridResults
            // 
            this.dataGridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridResults.Location = new System.Drawing.Point(0, 0);
            this.dataGridResults.Name = "dataGridResults";
            this.dataGridResults.Size = new System.Drawing.Size(436, 608);
            this.dataGridResults.TabIndex = 8;
            this.dataGridResults.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridResults_CellEnter);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(368, 35);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1354, 726);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tbxLongitude);
            this.Controls.Add(this.tbxLatitude);
            this.Controls.Add(this.lblLong);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.comboBoxGiven);
            this.Controls.Add(this.comboBoxQuery);
            this.Name = "Form1";
            this.Text = "Yelp Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxQuery;
        private System.Windows.Forms.ComboBox comboBoxGiven;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label lblLong;
        private System.Windows.Forms.TextBox tbxLatitude;
        private System.Windows.Forms.TextBox tbxLongitude;
        private System.Windows.Forms.WebBrowser GEBrowser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridResults;
        private System.Windows.Forms.Button btnRefresh;

    }
}

