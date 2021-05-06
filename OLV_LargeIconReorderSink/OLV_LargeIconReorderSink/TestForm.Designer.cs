namespace OLV_LargeIconReorderSink
{
    partial class TestForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnCreateGroups = new System.Windows.Forms.Button();
            this.olvcImage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvcImage);
            this.objectListView1.AllColumns.Add(this.olvcGroup);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcImage,
            this.olvcGroup});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.HasCollapsibleGroups = false;
            this.objectListView1.HideSelection = false;
            this.objectListView1.IsSearchOnSortColumn = false;
            this.objectListView1.Location = new System.Drawing.Point(12, 47);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowSortIndicators = false;
            this.objectListView1.Size = new System.Drawing.Size(776, 391);
            this.objectListView1.SortGroupItemsByPrimaryColumn = false;
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.LargeIcon;
            // 
            // btnAddItems
            // 
            this.btnAddItems.Location = new System.Drawing.Point(12, 12);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(75, 23);
            this.btnAddItems.TabIndex = 1;
            this.btnAddItems.Text = "Add Items";
            this.btnAddItems.UseVisualStyleBackColor = true;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnCreateGroups
            // 
            this.btnCreateGroups.Location = new System.Drawing.Point(93, 12);
            this.btnCreateGroups.Name = "btnCreateGroups";
            this.btnCreateGroups.Size = new System.Drawing.Size(105, 23);
            this.btnCreateGroups.TabIndex = 2;
            this.btnCreateGroups.Text = "Create Groups";
            this.btnCreateGroups.UseVisualStyleBackColor = true;
            this.btnCreateGroups.Click += new System.EventHandler(this.btnCreateGroups_Click);
            // 
            // olvcImage
            // 
            this.olvcImage.Text = "Image";
            // 
            // olvcGroup
            // 
            this.olvcGroup.Text = "Group";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCreateGroups);
            this.Controls.Add(this.btnAddItems);
            this.Controls.Add(this.objectListView1);
            this.Name = "TestForm";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button btnCreateGroups;
        private BrightIdeasSoftware.OLVColumn olvcImage;
        private BrightIdeasSoftware.OLVColumn olvcGroup;
    }
}

