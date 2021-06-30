namespace SPListScriptGenerator
{
    partial class frmScriptGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptGenerator));
            this.lblSiteUrl = new System.Windows.Forms.Label();
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.btnShowList = new System.Windows.Forms.Button();
            this.dgLists = new System.Windows.Forms.DataGridView();
            this.SelectList = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgLists)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSiteUrl
            // 
            this.lblSiteUrl.AutoSize = true;
            this.lblSiteUrl.Location = new System.Drawing.Point(39, 29);
            this.lblSiteUrl.Name = "lblSiteUrl";
            this.lblSiteUrl.Size = new System.Drawing.Size(41, 13);
            this.lblSiteUrl.TabIndex = 0;
            this.lblSiteUrl.Text = "Site Url";
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Location = new System.Drawing.Point(84, 26);
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(393, 20);
            this.txtSiteUrl.TabIndex = 1;
            // 
            // btnShowList
            // 
            this.btnShowList.Location = new System.Drawing.Point(529, 29);
            this.btnShowList.Name = "btnShowList";
            this.btnShowList.Size = new System.Drawing.Size(75, 23);
            this.btnShowList.TabIndex = 2;
            this.btnShowList.Tag = "Displays the lists";
            this.btnShowList.Text = "Show Lists";
            this.btnShowList.UseVisualStyleBackColor = true;
            this.btnShowList.Click += new System.EventHandler(this.btnShowList_Click);
            // 
            // dgLists
            // 
            this.dgLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectList});
            this.dgLists.Enabled = false;
            this.dgLists.Location = new System.Drawing.Point(42, 77);
            this.dgLists.Name = "dgLists";
            this.dgLists.Size = new System.Drawing.Size(674, 248);
            this.dgLists.TabIndex = 3;
            // 
            // SelectList
            // 
            this.SelectList.HeaderText = "SelectList";
            this.SelectList.Name = "SelectList";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(369, 372);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate Scripts";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Visible = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // rtbMessage
            // 
            this.rtbMessage.Location = new System.Drawing.Point(788, 77);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(347, 248);
            this.rtbMessage.TabIndex = 5;
            this.rtbMessage.Text = "Log will be displayed here";
            // 
            // frmScriptGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 453);
            this.Controls.Add(this.rtbMessage);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dgLists);
            this.Controls.Add(this.btnShowList);
            this.Controls.Add(this.txtSiteUrl);
            this.Controls.Add(this.lblSiteUrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScriptGenerator";
            this.Text = "SP 2010 List Script Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dgLists)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSiteUrl;
        private System.Windows.Forms.TextBox txtSiteUrl;
        private System.Windows.Forms.Button btnShowList;
        private System.Windows.Forms.DataGridView dgLists;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectList;
    }
}

