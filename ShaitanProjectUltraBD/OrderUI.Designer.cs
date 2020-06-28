namespace ShaitanProjectUltraBD
{
    partial class OrderUI
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ProductIDBox = new System.Windows.Forms.TextBox();
            this.QuantityBox = new System.Windows.Forms.TextBox();
            this.BasketAddButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(173, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(474, 305);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(737, 67);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(380, 305);
            this.dataGridView2.TabIndex = 1;
            // 
            // ProductIDBox
            // 
            this.ProductIDBox.Location = new System.Drawing.Point(558, 442);
            this.ProductIDBox.Name = "ProductIDBox";
            this.ProductIDBox.Size = new System.Drawing.Size(100, 20);
            this.ProductIDBox.TabIndex = 2;
            // 
            // QuantityBox
            // 
            this.QuantityBox.Location = new System.Drawing.Point(692, 442);
            this.QuantityBox.Name = "QuantityBox";
            this.QuantityBox.Size = new System.Drawing.Size(100, 20);
            this.QuantityBox.TabIndex = 5;
            // 
            // BasketAddButton
            // 
            this.BasketAddButton.Location = new System.Drawing.Point(558, 511);
            this.BasketAddButton.Name = "BasketAddButton";
            this.BasketAddButton.Size = new System.Drawing.Size(100, 23);
            this.BasketAddButton.TabIndex = 6;
            this.BasketAddButton.Text = "Add to basket";
            this.BasketAddButton.UseVisualStyleBackColor = true;
            this.BasketAddButton.Click += new System.EventHandler(this.BasketAddButton_Click);
            // 
            // OrderUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 585);
            this.Controls.Add(this.BasketAddButton);
            this.Controls.Add(this.QuantityBox);
            this.Controls.Add(this.ProductIDBox);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "OrderUI";
            this.Text = "OrderUI";
            this.Load += new System.EventHandler(this.OrderUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox ProductIDBox;
        private System.Windows.Forms.TextBox QuantityBox;
        private System.Windows.Forms.Button BasketAddButton;
    }
}