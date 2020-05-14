namespace LOR_Windows_Form
{
    partial class LORGG
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
            this.components = new System.ComponentModel.Container();
            this.DeckList = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.CardList = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeckList
            // 
            this.DeckList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.DeckList.ImageSize = new System.Drawing.Size(30, 30);
            this.DeckList.TransparentColor = System.Drawing.Color.DarkRed;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "GO";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CardList
            // 
            this.CardList.Location = new System.Drawing.Point(339, 34);
            this.CardList.Name = "CardList";
            this.CardList.Size = new System.Drawing.Size(271, 360);
            this.CardList.TabIndex = 1;
            this.CardList.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(456, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Card List";
            // 
            // LORGG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 466);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CardList);
            this.Controls.Add(this.button1);
            this.Name = "LORGG";
            this.Text = "LORGG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LORGG_FormClosing);
            this.Load += new System.EventHandler(this.LORGG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList DeckList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox CardList;
        private System.Windows.Forms.Label label1;
    }
}

