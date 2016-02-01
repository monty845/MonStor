namespace Autoparts {
    partial class Menu {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnPartEditor = new System.Windows.Forms.Button();
            this.btnInvMan = new System.Windows.Forms.Button();
            this.btnStoreEditor = new System.Windows.Forms.Button();
            this.btnInvSearch = new System.Windows.Forms.Button();
            this.btnStoreSearch = new System.Windows.Forms.Button();
            this.btnPartSearch = new System.Windows.Forms.Button();
            this.btnTagEditor = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPartEditor
            // 
            this.btnPartEditor.Location = new System.Drawing.Point(137, 66);
            this.btnPartEditor.Name = "btnPartEditor";
            this.btnPartEditor.Size = new System.Drawing.Size(119, 48);
            this.btnPartEditor.TabIndex = 0;
            this.btnPartEditor.Text = "Part Editor";
            this.btnPartEditor.UseVisualStyleBackColor = true;
            this.btnPartEditor.Click += new System.EventHandler(this.btnPartEditor_Click);
            // 
            // btnInvMan
            // 
            this.btnInvMan.Location = new System.Drawing.Point(137, 12);
            this.btnInvMan.Name = "btnInvMan";
            this.btnInvMan.Size = new System.Drawing.Size(119, 48);
            this.btnInvMan.TabIndex = 1;
            this.btnInvMan.Text = "Inventory Management";
            this.btnInvMan.UseVisualStyleBackColor = true;
            // 
            // btnStoreEditor
            // 
            this.btnStoreEditor.Location = new System.Drawing.Point(137, 120);
            this.btnStoreEditor.Name = "btnStoreEditor";
            this.btnStoreEditor.Size = new System.Drawing.Size(119, 48);
            this.btnStoreEditor.TabIndex = 2;
            this.btnStoreEditor.Text = "Store Editor";
            this.btnStoreEditor.UseVisualStyleBackColor = true;
            // 
            // btnInvSearch
            // 
            this.btnInvSearch.Location = new System.Drawing.Point(12, 12);
            this.btnInvSearch.Name = "btnInvSearch";
            this.btnInvSearch.Size = new System.Drawing.Size(119, 48);
            this.btnInvSearch.TabIndex = 3;
            this.btnInvSearch.Text = "Inventory Search";
            this.btnInvSearch.UseVisualStyleBackColor = true;
            // 
            // btnStoreSearch
            // 
            this.btnStoreSearch.Location = new System.Drawing.Point(12, 120);
            this.btnStoreSearch.Name = "btnStoreSearch";
            this.btnStoreSearch.Size = new System.Drawing.Size(119, 48);
            this.btnStoreSearch.TabIndex = 4;
            this.btnStoreSearch.Text = "Store Search";
            this.btnStoreSearch.UseVisualStyleBackColor = true;
            // 
            // btnPartSearch
            // 
            this.btnPartSearch.Location = new System.Drawing.Point(12, 66);
            this.btnPartSearch.Name = "btnPartSearch";
            this.btnPartSearch.Size = new System.Drawing.Size(119, 48);
            this.btnPartSearch.TabIndex = 5;
            this.btnPartSearch.Text = "Part Search";
            this.btnPartSearch.UseVisualStyleBackColor = true;
            this.btnPartSearch.Click += new System.EventHandler(this.btnPartSearch_Click);
            // 
            // btnTagEditor
            // 
            this.btnTagEditor.Location = new System.Drawing.Point(12, 174);
            this.btnTagEditor.Name = "btnTagEditor";
            this.btnTagEditor.Size = new System.Drawing.Size(119, 48);
            this.btnTagEditor.TabIndex = 6;
            this.btnTagEditor.Text = "Tag Editor";
            this.btnTagEditor.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(137, 174);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 48);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 233);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnTagEditor);
            this.Controls.Add(this.btnPartSearch);
            this.Controls.Add(this.btnStoreSearch);
            this.Controls.Add(this.btnInvSearch);
            this.Controls.Add(this.btnStoreEditor);
            this.Controls.Add(this.btnInvMan);
            this.Controls.Add(this.btnPartEditor);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPartEditor;
        private System.Windows.Forms.Button btnInvMan;
        private System.Windows.Forms.Button btnStoreEditor;
        private System.Windows.Forms.Button btnInvSearch;
        private System.Windows.Forms.Button btnStoreSearch;
        private System.Windows.Forms.Button btnPartSearch;
        private System.Windows.Forms.Button btnTagEditor;
        private System.Windows.Forms.Button btnExit;
    }
}