namespace WSC
{
	partial class mainView
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
			this.WebSitesListBox = new System.Windows.Forms.ListBox();
			this.AddButton = new System.Windows.Forms.Button();
			this.WebSiteNameTextBox = new System.Windows.Forms.TextBox();
			this.WebSiteUrlTextBox = new System.Windows.Forms.TextBox();
			this.TimeIntervalTextBox = new System.Windows.Forms.TextBox();
			this.Deletebutton = new System.Windows.Forms.Button();
			this.EditButton = new System.Windows.Forms.Button();
			this.MessageTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// WebSitesListBox
			// 
			this.WebSitesListBox.FormattingEnabled = true;
			this.WebSitesListBox.Location = new System.Drawing.Point(13, 13);
			this.WebSitesListBox.Name = "WebSitesListBox";
			this.WebSitesListBox.Size = new System.Drawing.Size(296, 251);
			this.WebSitesListBox.TabIndex = 0;
			this.WebSitesListBox.SelectedIndexChanged += new System.EventHandler(this.WebSitesListBox_SelectedIndexChanged);
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(331, 113);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(110, 23);
			this.AddButton.TabIndex = 1;
			this.AddButton.Text = "Добавить";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// WebSiteNameTextBox
			// 
			this.WebSiteNameTextBox.Location = new System.Drawing.Point(330, 13);
			this.WebSiteNameTextBox.Name = "WebSiteNameTextBox";
			this.WebSiteNameTextBox.Size = new System.Drawing.Size(236, 20);
			this.WebSiteNameTextBox.TabIndex = 2;
			// 
			// WebSiteUrlTextBox
			// 
			this.WebSiteUrlTextBox.Location = new System.Drawing.Point(330, 49);
			this.WebSiteUrlTextBox.Name = "WebSiteUrlTextBox";
			this.WebSiteUrlTextBox.Size = new System.Drawing.Size(236, 20);
			this.WebSiteUrlTextBox.TabIndex = 3;
			// 
			// TimeIntervalTextBox
			// 
			this.TimeIntervalTextBox.Location = new System.Drawing.Point(330, 87);
			this.TimeIntervalTextBox.Name = "TimeIntervalTextBox";
			this.TimeIntervalTextBox.Size = new System.Drawing.Size(111, 20);
			this.TimeIntervalTextBox.TabIndex = 4;
			// 
			// Deletebutton
			// 
			this.Deletebutton.Location = new System.Drawing.Point(331, 143);
			this.Deletebutton.Name = "Deletebutton";
			this.Deletebutton.Size = new System.Drawing.Size(110, 23);
			this.Deletebutton.TabIndex = 5;
			this.Deletebutton.Text = "Удалить";
			this.Deletebutton.UseVisualStyleBackColor = true;
			this.Deletebutton.Click += new System.EventHandler(this.Deletebutton_Click);
			// 
			// EditButton
			// 
			this.EditButton.Location = new System.Drawing.Point(448, 113);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(104, 23);
			this.EditButton.TabIndex = 6;
			this.EditButton.Text = "Редактировать";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// MessageTextBox
			// 
			this.MessageTextBox.Location = new System.Drawing.Point(331, 173);
			this.MessageTextBox.Multiline = true;
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.ReadOnly = true;
			this.MessageTextBox.Size = new System.Drawing.Size(235, 91);
			this.MessageTextBox.TabIndex = 7;
			// 
			// mainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 286);
			this.Controls.Add(this.MessageTextBox);
			this.Controls.Add(this.EditButton);
			this.Controls.Add(this.Deletebutton);
			this.Controls.Add(this.TimeIntervalTextBox);
			this.Controls.Add(this.WebSiteUrlTextBox);
			this.Controls.Add(this.WebSiteNameTextBox);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.WebSitesListBox);
			this.Name = "mainView";
			this.Text = "mainView";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainView_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox WebSitesListBox;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.TextBox WebSiteNameTextBox;
		private System.Windows.Forms.TextBox WebSiteUrlTextBox;
		private System.Windows.Forms.TextBox TimeIntervalTextBox;
		private System.Windows.Forms.Button Deletebutton;
		private System.Windows.Forms.Button EditButton;
		private System.Windows.Forms.TextBox MessageTextBox;
	}
}