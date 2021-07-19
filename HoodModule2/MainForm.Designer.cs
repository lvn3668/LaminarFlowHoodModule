namespace LaminarFlowHoodModule {
	partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.gb_Study = new System.Windows.Forms.GroupBox();
			this.combo_Study = new System.Windows.Forms.ComboBox();
			this.gb_Scanner = new System.Windows.Forms.GroupBox();
			this.lbl_Instructions = new System.Windows.Forms.Label();
			this.lbl_Status = new System.Windows.Forms.Label();
			this.tb_1DScanner = new System.Windows.Forms.TextBox();
			this.gb_VolumeControls = new System.Windows.Forms.GroupBox();
			this.box_plasma2 = new System.Windows.Forms.GroupBox();
			this.label_Plasma2 = new System.Windows.Forms.Label();
			this.box_plasma1 = new System.Windows.Forms.GroupBox();
			this.label_Plasma1 = new System.Windows.Forms.Label();
			this.btn_buffycoatDown = new System.Windows.Forms.Button();
			this.box_buffycoat = new System.Windows.Forms.GroupBox();
			this.label_BuffyCoat = new System.Windows.Forms.Label();
			this.btn_plasma2Down = new System.Windows.Forms.Button();
			this.btn_plasma1Up = new System.Windows.Forms.Button();
			this.btn_plasma1Down = new System.Windows.Forms.Button();
			this.btn_plasma2Up = new System.Windows.Forms.Button();
			this.btn_buffycoatUp = new System.Windows.Forms.Button();
			this.btn_Exit = new System.Windows.Forms.Button();
			this.btn_Export = new System.Windows.Forms.Button();
			this.btn_Reset = new System.Windows.Forms.Button();
			this.TwoDScannerBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.lbl_Progress = new System.Windows.Forms.Label();
			this.gb_Study.SuspendLayout();
			this.gb_Scanner.SuspendLayout();
			this.gb_VolumeControls.SuspendLayout();
			this.box_plasma2.SuspendLayout();
			this.box_plasma1.SuspendLayout();
			this.box_buffycoat.SuspendLayout();
			this.SuspendLayout();
			// 
			// gb_Study
			// 
			this.gb_Study.Controls.Add(this.combo_Study);
			this.gb_Study.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.gb_Study.Location = new System.Drawing.Point(13, 12);
			this.gb_Study.Name = "gb_Study";
			this.gb_Study.Size = new System.Drawing.Size(680, 96);
			this.gb_Study.TabIndex = 0;
			this.gb_Study.TabStop = false;
			this.gb_Study.Text = "Select a Study";
			// 
			// combo_Study
			// 
			this.combo_Study.Cursor = System.Windows.Forms.Cursors.Hand;
			this.combo_Study.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo_Study.FormattingEnabled = true;
			this.combo_Study.Items.AddRange(new object[] {
            "MVP"});
			this.combo_Study.Location = new System.Drawing.Point(7, 38);
			this.combo_Study.Name = "combo_Study";
			this.combo_Study.Size = new System.Drawing.Size(667, 39);
			this.combo_Study.Sorted = true;
			this.combo_Study.TabIndex = 0;
			this.combo_Study.TabStop = false;
			this.combo_Study.SelectedIndexChanged += new System.EventHandler(this.combo_Study_SelectedIndexChanged);
			// 
			// gb_Scanner
			// 
			this.gb_Scanner.Controls.Add(this.lbl_Progress);
			this.gb_Scanner.Controls.Add(this.lbl_Instructions);
			this.gb_Scanner.Controls.Add(this.lbl_Status);
			this.gb_Scanner.Controls.Add(this.tb_1DScanner);
			this.gb_Scanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.gb_Scanner.Location = new System.Drawing.Point(12, 114);
			this.gb_Scanner.Name = "gb_Scanner";
			this.gb_Scanner.Size = new System.Drawing.Size(680, 134);
			this.gb_Scanner.TabIndex = 1;
			this.gb_Scanner.TabStop = false;
			this.gb_Scanner.Visible = false;
			// 
			// lbl_Instructions
			// 
			this.lbl_Instructions.AutoSize = true;
			this.lbl_Instructions.Location = new System.Drawing.Point(7, 98);
			this.lbl_Instructions.Name = "lbl_Instructions";
			this.lbl_Instructions.Size = new System.Drawing.Size(155, 31);
			this.lbl_Instructions.TabIndex = 2;
			this.lbl_Instructions.Text = "Instructions";
			// 
			// lbl_Status
			// 
			this.lbl_Status.AutoSize = true;
			this.lbl_Status.Location = new System.Drawing.Point(6, 19);
			this.lbl_Status.Name = "lbl_Status";
			this.lbl_Status.Size = new System.Drawing.Size(92, 31);
			this.lbl_Status.TabIndex = 1;
			this.lbl_Status.Text = "Status";
			// 
			// tb_1DScanner
			// 
			this.tb_1DScanner.Location = new System.Drawing.Point(7, 53);
			this.tb_1DScanner.Name = "tb_1DScanner";
			this.tb_1DScanner.Size = new System.Drawing.Size(317, 38);
			this.tb_1DScanner.TabIndex = 0;
			this.tb_1DScanner.Text = "1D Scanner";
			this.tb_1DScanner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_1DScanner_KeyDown);
			// 
			// gb_VolumeControls
			// 
			this.gb_VolumeControls.Controls.Add(this.box_plasma2);
			this.gb_VolumeControls.Controls.Add(this.box_plasma1);
			this.gb_VolumeControls.Controls.Add(this.btn_buffycoatDown);
			this.gb_VolumeControls.Controls.Add(this.box_buffycoat);
			this.gb_VolumeControls.Controls.Add(this.btn_plasma2Down);
			this.gb_VolumeControls.Controls.Add(this.btn_plasma1Up);
			this.gb_VolumeControls.Controls.Add(this.btn_plasma1Down);
			this.gb_VolumeControls.Controls.Add(this.btn_plasma2Up);
			this.gb_VolumeControls.Controls.Add(this.btn_buffycoatUp);
			this.gb_VolumeControls.Location = new System.Drawing.Point(13, 254);
			this.gb_VolumeControls.Name = "gb_VolumeControls";
			this.gb_VolumeControls.Size = new System.Drawing.Size(674, 129);
			this.gb_VolumeControls.TabIndex = 29;
			this.gb_VolumeControls.TabStop = false;
			this.gb_VolumeControls.Visible = false;
			// 
			// box_plasma2
			// 
			this.box_plasma2.Controls.Add(this.label_Plasma2);
			this.box_plasma2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.box_plasma2.Location = new System.Drawing.Point(226, 36);
			this.box_plasma2.Name = "box_plasma2";
			this.box_plasma2.Size = new System.Drawing.Size(121, 65);
			this.box_plasma2.TabIndex = 19;
			this.box_plasma2.TabStop = false;
			this.box_plasma2.Text = "plasma 2";
			// 
			// label_Plasma2
			// 
			this.label_Plasma2.AutoSize = true;
			this.label_Plasma2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.label_Plasma2.Location = new System.Drawing.Point(6, 22);
			this.label_Plasma2.Name = "label_Plasma2";
			this.label_Plasma2.Size = new System.Drawing.Size(64, 31);
			this.label_Plasma2.TabIndex = 16;
			this.label_Plasma2.Text = "0 ml";
			// 
			// box_plasma1
			// 
			this.box_plasma1.Controls.Add(this.label_Plasma1);
			this.box_plasma1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.box_plasma1.Location = new System.Drawing.Point(31, 36);
			this.box_plasma1.Name = "box_plasma1";
			this.box_plasma1.Size = new System.Drawing.Size(127, 65);
			this.box_plasma1.TabIndex = 18;
			this.box_plasma1.TabStop = false;
			this.box_plasma1.Text = "plasma 1";
			// 
			// label_Plasma1
			// 
			this.label_Plasma1.AutoSize = true;
			this.label_Plasma1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.label_Plasma1.Location = new System.Drawing.Point(6, 22);
			this.label_Plasma1.Name = "label_Plasma1";
			this.label_Plasma1.Size = new System.Drawing.Size(64, 31);
			this.label_Plasma1.TabIndex = 15;
			this.label_Plasma1.Text = "0 ml";
			// 
			// btn_buffycoatDown
			// 
			this.btn_buffycoatDown.Image = ((System.Drawing.Image) (resources.GetObject("btn_buffycoatDown.Image")));
			this.btn_buffycoatDown.Location = new System.Drawing.Point(561, 70);
			this.btn_buffycoatDown.Name = "btn_buffycoatDown";
			this.btn_buffycoatDown.Size = new System.Drawing.Size(47, 53);
			this.btn_buffycoatDown.TabIndex = 26;
			this.btn_buffycoatDown.UseVisualStyleBackColor = true;
			this.btn_buffycoatDown.Click += new System.EventHandler(this.btn_buffycoatDown_Click);
			// 
			// box_buffycoat
			// 
			this.box_buffycoat.Controls.Add(this.label_BuffyCoat);
			this.box_buffycoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.box_buffycoat.Location = new System.Drawing.Point(433, 36);
			this.box_buffycoat.Name = "box_buffycoat";
			this.box_buffycoat.Size = new System.Drawing.Size(122, 65);
			this.box_buffycoat.TabIndex = 20;
			this.box_buffycoat.TabStop = false;
			this.box_buffycoat.Text = "buffy coat";
			// 
			// label_BuffyCoat
			// 
			this.label_BuffyCoat.AutoSize = true;
			this.label_BuffyCoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.label_BuffyCoat.Location = new System.Drawing.Point(6, 22);
			this.label_BuffyCoat.Name = "label_BuffyCoat";
			this.label_BuffyCoat.Size = new System.Drawing.Size(64, 31);
			this.label_BuffyCoat.TabIndex = 17;
			this.label_BuffyCoat.Text = "0 ml";
			// 
			// btn_plasma2Down
			// 
			this.btn_plasma2Down.Image = ((System.Drawing.Image) (resources.GetObject("btn_plasma2Down.Image")));
			this.btn_plasma2Down.Location = new System.Drawing.Point(353, 70);
			this.btn_plasma2Down.Name = "btn_plasma2Down";
			this.btn_plasma2Down.Size = new System.Drawing.Size(47, 53);
			this.btn_plasma2Down.TabIndex = 25;
			this.btn_plasma2Down.UseVisualStyleBackColor = true;
			this.btn_plasma2Down.Click += new System.EventHandler(this.btn_plasma2Down_Click);
			// 
			// btn_plasma1Up
			// 
			this.btn_plasma1Up.Image = ((System.Drawing.Image) (resources.GetObject("btn_plasma1Up.Image")));
			this.btn_plasma1Up.Location = new System.Drawing.Point(164, 11);
			this.btn_plasma1Up.Name = "btn_plasma1Up";
			this.btn_plasma1Up.Size = new System.Drawing.Size(47, 53);
			this.btn_plasma1Up.TabIndex = 21;
			this.btn_plasma1Up.UseVisualStyleBackColor = true;
			this.btn_plasma1Up.Click += new System.EventHandler(this.btn_plasma1Up_Click);
			// 
			// btn_plasma1Down
			// 
			this.btn_plasma1Down.Image = ((System.Drawing.Image) (resources.GetObject("btn_plasma1Down.Image")));
			this.btn_plasma1Down.Location = new System.Drawing.Point(164, 70);
			this.btn_plasma1Down.Name = "btn_plasma1Down";
			this.btn_plasma1Down.Size = new System.Drawing.Size(47, 53);
			this.btn_plasma1Down.TabIndex = 24;
			this.btn_plasma1Down.UseVisualStyleBackColor = true;
			this.btn_plasma1Down.Click += new System.EventHandler(this.btn_plasma1Down_Click);
			// 
			// btn_plasma2Up
			// 
			this.btn_plasma2Up.Image = ((System.Drawing.Image) (resources.GetObject("btn_plasma2Up.Image")));
			this.btn_plasma2Up.Location = new System.Drawing.Point(353, 11);
			this.btn_plasma2Up.Name = "btn_plasma2Up";
			this.btn_plasma2Up.Size = new System.Drawing.Size(47, 53);
			this.btn_plasma2Up.TabIndex = 22;
			this.btn_plasma2Up.UseVisualStyleBackColor = true;
			this.btn_plasma2Up.Click += new System.EventHandler(this.btn_plasma2Up_Click);
			// 
			// btn_buffycoatUp
			// 
			this.btn_buffycoatUp.Image = ((System.Drawing.Image) (resources.GetObject("btn_buffycoatUp.Image")));
			this.btn_buffycoatUp.Location = new System.Drawing.Point(561, 11);
			this.btn_buffycoatUp.Name = "btn_buffycoatUp";
			this.btn_buffycoatUp.Size = new System.Drawing.Size(47, 53);
			this.btn_buffycoatUp.TabIndex = 23;
			this.btn_buffycoatUp.UseVisualStyleBackColor = true;
			this.btn_buffycoatUp.Click += new System.EventHandler(this.btn_buffycoatUp_Click);
			// 
			// btn_Exit
			// 
			this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btn_Exit.Location = new System.Drawing.Point(579, 388);
			this.btn_Exit.Name = "btn_Exit";
			this.btn_Exit.Size = new System.Drawing.Size(110, 75);
			this.btn_Exit.TabIndex = 32;
			this.btn_Exit.Text = "Exit";
			this.btn_Exit.UseVisualStyleBackColor = true;
			this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
			// 
			// btn_Export
			// 
			this.btn_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btn_Export.Location = new System.Drawing.Point(398, 388);
			this.btn_Export.Name = "btn_Export";
			this.btn_Export.Size = new System.Drawing.Size(110, 75);
			this.btn_Export.TabIndex = 31;
			this.btn_Export.Text = "Export";
			this.btn_Export.UseVisualStyleBackColor = true;
			this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
			// 
			// btn_Reset
			// 
			this.btn_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btn_Reset.Location = new System.Drawing.Point(15, 388);
			this.btn_Reset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btn_Reset.Name = "btn_Reset";
			this.btn_Reset.Size = new System.Drawing.Size(110, 75);
			this.btn_Reset.TabIndex = 30;
			this.btn_Reset.Text = "Reset";
			this.btn_Reset.UseVisualStyleBackColor = true;
			this.btn_Reset.Visible = false;
			this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
			// 
			// TwoDScannerBackgroundWorker
			// 
			this.TwoDScannerBackgroundWorker.WorkerReportsProgress = true;
			this.TwoDScannerBackgroundWorker.WorkerSupportsCancellation = true;
			this.TwoDScannerBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TwoDScannerBackgroundWorker_DoWork);
			this.TwoDScannerBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.TwoDScannerBackgroundWorker_ProgressChanged);
			this.TwoDScannerBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TwoDScannerBackgroundWorker_RunWorkerCompleted);
			// 
			// lbl_Progress
			// 
			this.lbl_Progress.AutoSize = true;
			this.lbl_Progress.Location = new System.Drawing.Point(330, 56);
			this.lbl_Progress.Name = "lbl_Progress";
			this.lbl_Progress.Size = new System.Drawing.Size(212, 31);
			this.lbl_Progress.TabIndex = 3;
			this.lbl_Progress.Text = "Progress Report";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(705, 468);
			this.Controls.Add(this.btn_Exit);
			this.Controls.Add(this.btn_Export);
			this.Controls.Add(this.btn_Reset);
			this.Controls.Add(this.gb_VolumeControls);
			this.Controls.Add(this.gb_Scanner);
			this.Controls.Add(this.gb_Study);
			this.Name = "MainForm";
			this.Text = "Hood Module";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.gb_Study.ResumeLayout(false);
			this.gb_Scanner.ResumeLayout(false);
			this.gb_Scanner.PerformLayout();
			this.gb_VolumeControls.ResumeLayout(false);
			this.box_plasma2.ResumeLayout(false);
			this.box_plasma2.PerformLayout();
			this.box_plasma1.ResumeLayout(false);
			this.box_plasma1.PerformLayout();
			this.box_buffycoat.ResumeLayout(false);
			this.box_buffycoat.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gb_Study;
		private System.Windows.Forms.ComboBox combo_Study;
		private System.Windows.Forms.GroupBox gb_Scanner;
		private System.Windows.Forms.Label lbl_Instructions;
		private System.Windows.Forms.Label lbl_Status;
		private System.Windows.Forms.TextBox tb_1DScanner;
		private System.Windows.Forms.GroupBox gb_VolumeControls;
		private System.Windows.Forms.GroupBox box_plasma2;
		private System.Windows.Forms.Label label_Plasma2;
		private System.Windows.Forms.GroupBox box_plasma1;
		private System.Windows.Forms.Label label_Plasma1;
		private System.Windows.Forms.Button btn_buffycoatDown;
		private System.Windows.Forms.GroupBox box_buffycoat;
		private System.Windows.Forms.Label label_BuffyCoat;
		private System.Windows.Forms.Button btn_plasma2Down;
		private System.Windows.Forms.Button btn_plasma1Up;
		private System.Windows.Forms.Button btn_plasma1Down;
		private System.Windows.Forms.Button btn_plasma2Up;
		private System.Windows.Forms.Button btn_buffycoatUp;
		private System.Windows.Forms.Button btn_Exit;
		private System.Windows.Forms.Button btn_Export;
		private System.Windows.Forms.Button btn_Reset;
		private System.ComponentModel.BackgroundWorker TwoDScannerBackgroundWorker;
		private System.Windows.Forms.Label lbl_Progress;
	}
}

