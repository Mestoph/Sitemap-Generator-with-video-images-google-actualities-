namespace SiteMapGenerator
{
    partial class FrmGui
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGui));
            this.BtnGen = new System.Windows.Forms.Button();
            this.Pb = new System.Windows.Forms.ProgressBar();
            this.TxtUrl = new System.Windows.Forms.TextBox();
            this.Lbl = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.Fctb = new FastColoredTextBoxNS.FastColoredTextBox();
            this.Num = new System.Windows.Forms.NumericUpDown();
            this.LblDepth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Fctb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGen
            // 
            this.BtnGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnGen.Location = new System.Drawing.Point(756, 4);
            this.BtnGen.Name = "BtnGen";
            this.BtnGen.Size = new System.Drawing.Size(75, 22);
            this.BtnGen.TabIndex = 0;
            this.BtnGen.Text = "Generate";
            this.BtnGen.UseVisualStyleBackColor = true;
            this.BtnGen.Click += new System.EventHandler(this.BtnGen_Click);
            // 
            // Pb
            // 
            this.Pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pb.Location = new System.Drawing.Point(5, 520);
            this.Pb.Name = "Pb";
            this.Pb.Size = new System.Drawing.Size(748, 13);
            this.Pb.TabIndex = 1;
            // 
            // TxtUrl
            // 
            this.TxtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtUrl.Location = new System.Drawing.Point(5, 5);
            this.TxtUrl.Name = "TxtUrl";
            this.TxtUrl.Size = new System.Drawing.Size(752, 20);
            this.TxtUrl.TabIndex = 2;
            this.TxtUrl.TextChanged += new System.EventHandler(this.TxtUrl_TextChanged);
            // 
            // Lbl
            // 
            this.Lbl.AutoSize = true;
            this.Lbl.Location = new System.Drawing.Point(2, 29);
            this.Lbl.Name = "Lbl";
            this.Lbl.Size = new System.Drawing.Size(104, 13);
            this.Lbl.TabIndex = 4;
            this.Lbl.Text = "Generated Sitemap :";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(756, 517);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 22);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // Fctb
            // 
            this.Fctb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.Fctb.AutoIndentCharsPatterns = "";
            this.Fctb.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.Fctb.BackBrush = null;
            this.Fctb.CharHeight = 14;
            this.Fctb.CharWidth = 8;
            this.Fctb.CommentPrefix = null;
            this.Fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Fctb.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.Fctb.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Fctb.IsReplaceMode = false;
            this.Fctb.Language = FastColoredTextBoxNS.Language.XML;
            this.Fctb.LeftBracket = '<';
            this.Fctb.LeftBracket2 = '(';
            this.Fctb.Location = new System.Drawing.Point(5, 51);
            this.Fctb.Name = "Fctb";
            this.Fctb.Paddings = new System.Windows.Forms.Padding(0);
            this.Fctb.ReadOnly = true;
            this.Fctb.RightBracket = '>';
            this.Fctb.RightBracket2 = ')';
            this.Fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.Fctb.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("Fctb.ServiceColors")));
            this.Fctb.ShowFoldingLines = true;
            this.Fctb.ShowLineNumbers = false;
            this.Fctb.Size = new System.Drawing.Size(826, 463);
            this.Fctb.TabIndex = 6;
            this.Fctb.Zoom = 100;
            // 
            // Num
            // 
            this.Num.Location = new System.Drawing.Point(111, 27);
            this.Num.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num.Name = "Num";
            this.Num.Size = new System.Drawing.Size(42, 20);
            this.Num.TabIndex = 7;
            this.Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num.ValueChanged += new System.EventHandler(this.Num_ValueChanged);
            // 
            // LblDepth
            // 
            this.LblDepth.AutoSize = true;
            this.LblDepth.Location = new System.Drawing.Point(159, 29);
            this.LblDepth.Name = "LblDepth";
            this.LblDepth.Size = new System.Drawing.Size(200, 13);
            this.LblDepth.TabIndex = 8;
            this.LblDepth.Text = "depths (work only on the same domaine).";
            // 
            // FrmGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 539);
            this.Controls.Add(this.LblDepth);
            this.Controls.Add(this.Num);
            this.Controls.Add(this.Fctb);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.Lbl);
            this.Controls.Add(this.TxtUrl);
            this.Controls.Add(this.Pb);
            this.Controls.Add(this.BtnGen);
            this.Name = "FrmGui";
            this.Text = "Sitemap Generator";
            ((System.ComponentModel.ISupportInitialize)(this.Fctb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGen;
        private System.Windows.Forms.ProgressBar Pb;
        private System.Windows.Forms.TextBox TxtUrl;
        private System.Windows.Forms.Label Lbl;
        private System.Windows.Forms.Button BtnCancel;
        private FastColoredTextBoxNS.FastColoredTextBox Fctb;
        private System.Windows.Forms.NumericUpDown Num;
        private System.Windows.Forms.Label LblDepth;
    }
}

