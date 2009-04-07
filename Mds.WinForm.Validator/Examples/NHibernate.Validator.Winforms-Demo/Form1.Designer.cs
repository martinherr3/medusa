namespace NHibernate.Validator.Winforms_Demo {
	partial class Form1 {
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
			this.components = new System.ComponentModel.Container();
			this.btnSave = new System.Windows.Forms.Button();
			this.tFirstName = new System.Windows.Forms.TextBox();
			this.tLastName = new System.Windows.Forms.TextBox();
			this.tStreet = new System.Windows.Forms.TextBox();
			this.tNumber = new System.Windows.Forms.TextBox();
			this.tEmail = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(121, 142);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(125, 38);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// tFirstName
			// 
			this.tFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tFirstName.Location = new System.Drawing.Point(93, 12);
			this.tFirstName.Name = "tFirstName";
			this.tFirstName.Size = new System.Drawing.Size(138, 23);
			this.tFirstName.TabIndex = 1;
			// 
			// tLastName
			// 
			this.tLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tLastName.Location = new System.Drawing.Point(93, 38);
			this.tLastName.Name = "tLastName";
			this.tLastName.Size = new System.Drawing.Size(138, 23);
			this.tLastName.TabIndex = 1;
			// 
			// tStreet
			// 
			this.tStreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tStreet.Location = new System.Drawing.Point(93, 64);
			this.tStreet.Name = "tStreet";
			this.tStreet.Size = new System.Drawing.Size(138, 23);
			this.tStreet.TabIndex = 1;
			// 
			// tNumber
			// 
			this.tNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tNumber.Location = new System.Drawing.Point(264, 64);
			this.tNumber.Name = "tNumber";
			this.tNumber.Size = new System.Drawing.Size(56, 23);
			this.tNumber.TabIndex = 1;
			// 
			// tEmail
			// 
			this.tEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tEmail.Location = new System.Drawing.Point(93, 90);
			this.tEmail.Name = "tEmail";
			this.tEmail.Size = new System.Drawing.Size(138, 23);
			this.tEmail.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "First name";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(13, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Last name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(27, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Address";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(45, 93);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "Email";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(248, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(16, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "#";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(366, 196);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tEmail);
			this.Controls.Add(this.tNumber);
			this.Controls.Add(this.tStreet);
			this.Controls.Add(this.tLastName);
			this.Controls.Add(this.tFirstName);
			this.Controls.Add(this.btnSave);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox tFirstName;
		private System.Windows.Forms.TextBox tLastName;
		private System.Windows.Forms.TextBox tStreet;
		private System.Windows.Forms.TextBox tNumber;
		private System.Windows.Forms.TextBox tEmail;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}

