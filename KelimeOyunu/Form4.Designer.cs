﻿namespace KelimeOyunu
{
    partial class Form4
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
            label1 = new Label();
            label2 = new Label();
            hikayeTextBox = new TextBox();
            richTextBox1 = new RichTextBox();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            button1 = new Button();
            buttonCikis = new Button();
            panelContainer = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelContainer.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(183, 7);
            label1.Name = "label1";
            label1.Size = new Size(224, 41);
            label1.TabIndex = 0;
            label1.Text = "Hikaye Oluşturucu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(83, 98);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 1;
            label2.Text = "Kelimeler";
            // 
            // hikayeTextBox
            // 
            hikayeTextBox.Location = new Point(161, 95);
            hikayeTextBox.Name = "hikayeTextBox";
            hikayeTextBox.Size = new Size(280, 27);
            hikayeTextBox.TabIndex = 2;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(161, 128);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(280, 212);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(101, 131);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 4;
            label3.Text = "Hikaye";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(161, 346);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(280, 198);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(106, 346);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 6;
            label4.Text = "Resim";
            // 
            // button1
            // 
            button1.Location = new Point(255, 550);
            button1.Name = "button1";
            button1.Size = new Size(94, 49);
            button1.TabIndex = 7;
            button1.Text = "Oluştur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonCikis
            // 
            buttonCikis.Location = new Point(521, 550);
            buttonCikis.Name = "buttonCikis";
            buttonCikis.Size = new Size(94, 49);
            buttonCikis.TabIndex = 8;
            buttonCikis.Text = "Çıkış";
            buttonCikis.UseVisualStyleBackColor = true;
            buttonCikis.Click += buttonCikis_Click;
            // 
            // panelContainer
            // 
            panelContainer.Controls.Add(label1);
            panelContainer.Controls.Add(buttonCikis);
            panelContainer.Controls.Add(label2);
            panelContainer.Controls.Add(button1);
            panelContainer.Controls.Add(hikayeTextBox);
            panelContainer.Controls.Add(label4);
            panelContainer.Controls.Add(richTextBox1);
            panelContainer.Controls.Add(pictureBox1);
            panelContainer.Controls.Add(label3);
            panelContainer.Location = new Point(1, 2);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(618, 633);
            panelContainer.TabIndex = 9;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(632, 636);
            Controls.Add(panelContainer);
            Name = "Form4";
            Text = "Form4";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox hikayeTextBox;
        private RichTextBox richTextBox1;
        private Label label3;
        private PictureBox pictureBox1;
        private Label label4;
        private Button button1;
        private Button buttonCikis;
        private Panel panelContainer;
    }
}