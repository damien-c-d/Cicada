namespace DeadsetAutoClicker;

partial class MainForm
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
        StartButton = new Button();
        StopButton = new Button();
        DelayUpDown = new NumericUpDown();
        LeftClickRadioButton = new RadioButton();
        RightClickRadioButton = new RadioButton();
        MiddleClickRadioButton = new RadioButton();
        DelayLabel = new Label();
        RepeatSetCountRadioButton = new RadioButton();
        RepeatCountUpDown = new NumericUpDown();
        ClickTypeGroupBox = new GroupBox();
        RepeatGroupBox = new GroupBox();
        InfinitelyRadioButton = new RadioButton();
        AutoClickBackgroundWorker = new System.ComponentModel.BackgroundWorker();
        ClickAmountGroupBox = new GroupBox();
        SingleClickRadioButton = new RadioButton();
        DoubleClickRadioButton = new RadioButton();
        TripleClickRadioButton = new RadioButton();
        ((System.ComponentModel.ISupportInitialize)DelayUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)RepeatCountUpDown).BeginInit();
        ClickTypeGroupBox.SuspendLayout();
        RepeatGroupBox.SuspendLayout();
        ClickAmountGroupBox.SuspendLayout();
        SuspendLayout();
        // 
        // StartButton
        // 
        StartButton.Cursor = Cursors.Hand;
        StartButton.FlatStyle = FlatStyle.System;
        StartButton.Location = new Point(331, 272);
        StartButton.Name = "StartButton";
        StartButton.Size = new Size(174, 27);
        StartButton.TabIndex = 0;
        StartButton.Text = "Start (Ctrl + F12)";
        StartButton.UseVisualStyleBackColor = true;
        StartButton.Click += StartButton_Click;
        // 
        // StopButton
        // 
        StopButton.Cursor = Cursors.Hand;
        StopButton.FlatStyle = FlatStyle.System;
        StopButton.Location = new Point(14, 272);
        StopButton.Name = "StopButton";
        StopButton.Size = new Size(174, 27);
        StopButton.TabIndex = 1;
        StopButton.Text = "Stop (Alt+F12)";
        StopButton.UseVisualStyleBackColor = true;
        StopButton.Click += StopButton_Click;
        // 
        // DelayUpDown
        // 
        DelayUpDown.Location = new Point(140, 240);
        DelayUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
        DelayUpDown.Name = "DelayUpDown";
        DelayUpDown.Size = new Size(76, 24);
        DelayUpDown.TabIndex = 2;
        // 
        // LeftClickRadioButton
        // 
        LeftClickRadioButton.AutoSize = true;
        LeftClickRadioButton.Cursor = Cursors.Hand;
        LeftClickRadioButton.Location = new Point(7, 24);
        LeftClickRadioButton.Name = "LeftClickRadioButton";
        LeftClickRadioButton.Size = new Size(84, 21);
        LeftClickRadioButton.TabIndex = 3;
        LeftClickRadioButton.TabStop = true;
        LeftClickRadioButton.Text = "Left-Click";
        LeftClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // RightClickRadioButton
        // 
        RightClickRadioButton.AutoSize = true;
        RightClickRadioButton.Cursor = Cursors.Hand;
        RightClickRadioButton.Location = new Point(240, 24);
        RightClickRadioButton.Name = "RightClickRadioButton";
        RightClickRadioButton.Size = new Size(93, 21);
        RightClickRadioButton.TabIndex = 4;
        RightClickRadioButton.TabStop = true;
        RightClickRadioButton.Text = "Right-Click";
        RightClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // MiddleClickRadioButton
        // 
        MiddleClickRadioButton.AutoSize = true;
        MiddleClickRadioButton.Cursor = Cursors.Hand;
        MiddleClickRadioButton.Location = new Point(116, 24);
        MiddleClickRadioButton.Name = "MiddleClickRadioButton";
        MiddleClickRadioButton.Size = new Size(103, 21);
        MiddleClickRadioButton.TabIndex = 5;
        MiddleClickRadioButton.TabStop = true;
        MiddleClickRadioButton.Text = "Middle-Click";
        MiddleClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // DelayLabel
        // 
        DelayLabel.AutoSize = true;
        DelayLabel.ForeColor = Color.White;
        DelayLabel.Location = new Point(12, 242);
        DelayLabel.Name = "DelayLabel";
        DelayLabel.Size = new Size(122, 17);
        DelayLabel.TabIndex = 7;
        DelayLabel.Text = "Delay (in seconds)";
        // 
        // RepeatSetCountRadioButton
        // 
        RepeatSetCountRadioButton.AutoSize = true;
        RepeatSetCountRadioButton.Cursor = Cursors.Hand;
        RepeatSetCountRadioButton.Location = new Point(116, 24);
        RepeatSetCountRadioButton.Name = "RepeatSetCountRadioButton";
        RepeatSetCountRadioButton.Size = new Size(86, 21);
        RepeatSetCountRadioButton.TabIndex = 9;
        RepeatSetCountRadioButton.TabStop = true;
        RepeatSetCountRadioButton.Text = "Set Count";
        RepeatSetCountRadioButton.UseVisualStyleBackColor = true;
        RepeatSetCountRadioButton.CheckedChanged += RepeatSetCountRadioButton_CheckedChanged;
        // 
        // RepeatCountUpDown
        // 
        RepeatCountUpDown.Location = new Point(216, 24);
        RepeatCountUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
        RepeatCountUpDown.Name = "RepeatCountUpDown";
        RepeatCountUpDown.Size = new Size(60, 24);
        RepeatCountUpDown.TabIndex = 10;
        // 
        // ClickTypeGroupBox
        // 
        ClickTypeGroupBox.Controls.Add(LeftClickRadioButton);
        ClickTypeGroupBox.Controls.Add(MiddleClickRadioButton);
        ClickTypeGroupBox.Controls.Add(RightClickRadioButton);
        ClickTypeGroupBox.ForeColor = Color.White;
        ClickTypeGroupBox.Location = new Point(14, 14);
        ClickTypeGroupBox.Name = "ClickTypeGroupBox";
        ClickTypeGroupBox.Size = new Size(492, 68);
        ClickTypeGroupBox.TabIndex = 12;
        ClickTypeGroupBox.TabStop = false;
        ClickTypeGroupBox.Text = "Click Type";
        // 
        // RepeatGroupBox
        // 
        RepeatGroupBox.Controls.Add(InfinitelyRadioButton);
        RepeatGroupBox.Controls.Add(RepeatSetCountRadioButton);
        RepeatGroupBox.Controls.Add(RepeatCountUpDown);
        RepeatGroupBox.ForeColor = Color.White;
        RepeatGroupBox.Location = new Point(14, 88);
        RepeatGroupBox.Name = "RepeatGroupBox";
        RepeatGroupBox.Size = new Size(492, 68);
        RepeatGroupBox.TabIndex = 13;
        RepeatGroupBox.TabStop = false;
        RepeatGroupBox.Text = "Repeat";
        // 
        // InfinitelyRadioButton
        // 
        InfinitelyRadioButton.AutoSize = true;
        InfinitelyRadioButton.Cursor = Cursors.Hand;
        InfinitelyRadioButton.Location = new Point(7, 24);
        InfinitelyRadioButton.Name = "InfinitelyRadioButton";
        InfinitelyRadioButton.Size = new Size(82, 21);
        InfinitelyRadioButton.TabIndex = 10;
        InfinitelyRadioButton.TabStop = true;
        InfinitelyRadioButton.Text = "Infinitely";
        InfinitelyRadioButton.UseVisualStyleBackColor = true;
        // 
        // AutoClickBackgroundWorker
        // 
        AutoClickBackgroundWorker.WorkerSupportsCancellation = true;
        AutoClickBackgroundWorker.DoWork += AutoClickBackgroundWorker_DoWork;
        // 
        // ClickAmountGroupBox
        // 
        ClickAmountGroupBox.Controls.Add(SingleClickRadioButton);
        ClickAmountGroupBox.Controls.Add(DoubleClickRadioButton);
        ClickAmountGroupBox.Controls.Add(TripleClickRadioButton);
        ClickAmountGroupBox.ForeColor = Color.White;
        ClickAmountGroupBox.Location = new Point(14, 162);
        ClickAmountGroupBox.Name = "ClickAmountGroupBox";
        ClickAmountGroupBox.Size = new Size(492, 68);
        ClickAmountGroupBox.TabIndex = 13;
        ClickAmountGroupBox.TabStop = false;
        ClickAmountGroupBox.Text = "Click Amount";
        // 
        // SingleClickRadioButton
        // 
        SingleClickRadioButton.AutoSize = true;
        SingleClickRadioButton.Cursor = Cursors.Hand;
        SingleClickRadioButton.Location = new Point(7, 24);
        SingleClickRadioButton.Name = "SingleClickRadioButton";
        SingleClickRadioButton.Size = new Size(64, 21);
        SingleClickRadioButton.TabIndex = 3;
        SingleClickRadioButton.TabStop = true;
        SingleClickRadioButton.Text = "Single";
        SingleClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // DoubleClickRadioButton
        // 
        DoubleClickRadioButton.AutoSize = true;
        DoubleClickRadioButton.Cursor = Cursors.Hand;
        DoubleClickRadioButton.Location = new Point(116, 24);
        DoubleClickRadioButton.Name = "DoubleClickRadioButton";
        DoubleClickRadioButton.Size = new Size(71, 21);
        DoubleClickRadioButton.TabIndex = 5;
        DoubleClickRadioButton.TabStop = true;
        DoubleClickRadioButton.Text = "Double";
        DoubleClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // TripleClickRadioButton
        // 
        TripleClickRadioButton.AutoSize = true;
        TripleClickRadioButton.Cursor = Cursors.Hand;
        TripleClickRadioButton.Location = new Point(240, 24);
        TripleClickRadioButton.Name = "TripleClickRadioButton";
        TripleClickRadioButton.Size = new Size(62, 21);
        TripleClickRadioButton.TabIndex = 4;
        TripleClickRadioButton.TabStop = true;
        TripleClickRadioButton.Text = "Triple";
        TripleClickRadioButton.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.DodgerBlue;
        ClientSize = new Size(517, 311);
        Controls.Add(ClickAmountGroupBox);
        Controls.Add(RepeatGroupBox);
        Controls.Add(ClickTypeGroupBox);
        Controls.Add(DelayLabel);
        Controls.Add(DelayUpDown);
        Controls.Add(StopButton);
        Controls.Add(StartButton);
        DoubleBuffered = true;
        Font = new Font("Ebrima", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        SizeGripStyle = SizeGripStyle.Hide;
        Text = "Deadset AutoClicker";
        ((System.ComponentModel.ISupportInitialize)DelayUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)RepeatCountUpDown).EndInit();
        ClickTypeGroupBox.ResumeLayout(false);
        ClickTypeGroupBox.PerformLayout();
        RepeatGroupBox.ResumeLayout(false);
        RepeatGroupBox.PerformLayout();
        ClickAmountGroupBox.ResumeLayout(false);
        ClickAmountGroupBox.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button StartButton;
    private Button StopButton;
    private NumericUpDown DelayUpDown;
    private RadioButton LeftClickRadioButton;
    private RadioButton RightClickRadioButton;
    private RadioButton MiddleClickRadioButton;
    private Label DelayLabel;
    private RadioButton RepeatSetCountRadioButton;
    private NumericUpDown RepeatCountUpDown;
    private GroupBox ClickTypeGroupBox;
    private GroupBox RepeatGroupBox;
    private RadioButton InfinitelyRadioButton;
    private System.ComponentModel.BackgroundWorker AutoClickBackgroundWorker;
    private GroupBox ClickAmountGroupBox;
    private RadioButton SingleClickRadioButton;
    private RadioButton DoubleClickRadioButton;
    private RadioButton TripleClickRadioButton;
}