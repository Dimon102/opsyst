namespace WinformsMultithreading
{
    partial class hyper_thret
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.consoleLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.lowestPriorityButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Consolas", 10F);
            this.consoleLabel.Location = new System.Drawing.Point(12, 18);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(104, 17);
            this.consoleLabel.TabIndex = 0;
            this.consoleLabel.Text = "ТИПА КОНСОЛЬ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(610, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 58);
            this.button1.TabIndex = 1;
            this.button1.Text = "КНОПКА!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(610, 302);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(178, 58);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(610, 229);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(178, 58);
            this.pauseButton.TabIndex = 1;
            this.pauseButton.Text = "пауза";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.Location = new System.Drawing.Point(610, 151);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(178, 58);
            this.resumeButton.TabIndex = 1;
            this.resumeButton.Text = "возобновить";
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // lowestPriorityButton
            // 
            this.lowestPriorityButton.Location = new System.Drawing.Point(610, 69);
            this.lowestPriorityButton.Name = "lowestPriorityButton";
            this.lowestPriorityButton.Size = new System.Drawing.Size(178, 58);
            this.lowestPriorityButton.TabIndex = 1;
            this.lowestPriorityButton.Text = "в нулину приоритет тупа";
            this.lowestPriorityButton.UseVisualStyleBackColor = true;
            this.lowestPriorityButton.Click += new System.EventHandler(this.lowestPriorityButton_Click);
            // 
            // hyper_thret
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lowestPriorityButton);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.consoleLabel);
            this.Name = "hyper_thret";
            this.Text = "hyper thret))";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Button lowestPriorityButton;
    }
}

