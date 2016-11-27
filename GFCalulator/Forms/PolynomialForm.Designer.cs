namespace GFCalulator.Forms
{
    partial class PolynomialForm
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
            this.polynomialGrid = new System.Windows.Forms.DataGridView();
            this.savePolynomialBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.polynomialGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // polynomialGrid
            // 
            this.polynomialGrid.AllowUserToAddRows = false;
            this.polynomialGrid.AllowUserToDeleteRows = false;
            this.polynomialGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.polynomialGrid.Location = new System.Drawing.Point(12, 13);
            this.polynomialGrid.Name = "polynomialGrid";
            this.polynomialGrid.ReadOnly = true;
            this.polynomialGrid.Size = new System.Drawing.Size(421, 90);
            this.polynomialGrid.TabIndex = 0;
            // 
            // savePolynomialBtn
            // 
            this.savePolynomialBtn.Location = new System.Drawing.Point(319, 109);
            this.savePolynomialBtn.Name = "savePolynomialBtn";
            this.savePolynomialBtn.Size = new System.Drawing.Size(114, 23);
            this.savePolynomialBtn.TabIndex = 1;
            this.savePolynomialBtn.Text = "Dodaj wielomian";
            this.savePolynomialBtn.UseVisualStyleBackColor = true;
            this.savePolynomialBtn.Click += new System.EventHandler(this.savePolynomialBtn_Click);
            // 
            // PolynomialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 137);
            this.Controls.Add(this.savePolynomialBtn);
            this.Controls.Add(this.polynomialGrid);
            this.Name = "PolynomialForm";
            this.Text = "PolynomialForm";
            ((System.ComponentModel.ISupportInitialize)(this.polynomialGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView polynomialGrid;
        public System.Windows.Forms.Button savePolynomialBtn;
    }
}