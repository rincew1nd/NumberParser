namespace NumberConvertation
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
            this.TranButton = new System.Windows.Forms.Button();
            this.NumberLable = new System.Windows.Forms.Label();
            this.NumberOutput = new System.Windows.Forms.Label();
            this.NumberInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorLable = new System.Windows.Forms.Label();
            this.staroslavNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TranButton
            // 
            this.TranButton.Location = new System.Drawing.Point(162, 57);
            this.TranButton.Name = "TranButton";
            this.TranButton.Size = new System.Drawing.Size(75, 25);
            this.TranButton.TabIndex = 0;
            this.TranButton.Text = "Перевести";
            this.TranButton.UseVisualStyleBackColor = true;
            this.TranButton.Click += new System.EventHandler(this.TranButton_Click);
            // 
            // NumberLable
            // 
            this.NumberLable.AutoSize = true;
            this.NumberLable.Location = new System.Drawing.Point(12, 89);
            this.NumberLable.Name = "NumberLable";
            this.NumberLable.Size = new System.Drawing.Size(57, 13);
            this.NumberLable.TabIndex = 1;
            this.NumberLable.Text = "Числовой";
            // 
            // NumberOutput
            // 
            this.NumberOutput.AutoSize = true;
            this.NumberOutput.Location = new System.Drawing.Point(82, 89);
            this.NumberOutput.MaximumSize = new System.Drawing.Size(287, 20);
            this.NumberOutput.Name = "NumberOutput";
            this.NumberOutput.Size = new System.Drawing.Size(13, 13);
            this.NumberOutput.TabIndex = 2;
            this.NumberOutput.Text = "0";
            // 
            // NumberInput
            // 
            this.NumberInput.Location = new System.Drawing.Point(85, 31);
            this.NumberInput.Name = "NumberInput";
            this.NumberInput.Size = new System.Drawing.Size(287, 20);
            this.NumberInput.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Английский";
            // 
            // errorLable
            // 
            this.errorLable.AutoSize = true;
            this.errorLable.Location = new System.Drawing.Point(12, 122);
            this.errorLable.MaximumSize = new System.Drawing.Size(360, 0);
            this.errorLable.MinimumSize = new System.Drawing.Size(360, 0);
            this.errorLable.Name = "errorLable";
            this.errorLable.Size = new System.Drawing.Size(360, 13);
            this.errorLable.TabIndex = 5;
            this.errorLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // staroslavNum
            // 
            this.staroslavNum.AutoSize = true;
            this.staroslavNum.Location = new System.Drawing.Point(232, 89);
            this.staroslavNum.MaximumSize = new System.Drawing.Size(287, 20);
            this.staroslavNum.Name = "staroslavNum";
            this.staroslavNum.Size = new System.Drawing.Size(13, 13);
            this.staroslavNum.TabIndex = 7;
            this.staroslavNum.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Старославянский";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 216);
            this.Controls.Add(this.staroslavNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.errorLable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NumberInput);
            this.Controls.Add(this.NumberOutput);
            this.Controls.Add(this.NumberLable);
            this.Controls.Add(this.TranButton);
            this.Name = "Form1";
            this.Text = "Перевод с английского в число";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        private System.Windows.Forms.Button TranButton;
        private System.Windows.Forms.Label NumberLable;
        private System.Windows.Forms.Label NumberOutput;
        private System.Windows.Forms.TextBox NumberInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label errorLable;
        private System.Windows.Forms.Label staroslavNum;
        private System.Windows.Forms.Label label2;
    }
}

