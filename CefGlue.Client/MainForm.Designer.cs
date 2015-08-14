namespace CefGlue.Client
{
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserCreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserCloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.browserShowDevToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserCloseDevToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.browserZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserResetZoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.browserGetFrameNamesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleShowOrHideMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleClearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invokeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waitDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.goBackButton = new System.Windows.Forms.ToolStripButton();
            this.goForwardButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.reloadButton = new System.Windows.Forms.ToolStripButton();
            this.homeButton = new System.Windows.Forms.ToolStripButton();
            this.addressTextBox = new CefGlue.Client.ToolStripSpringTextBox();
            this.goButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.visitDOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.browserMenuItem,
            this.consoleMenuItem,
            this.testToolStripMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // fileExitMenuItem
            // 
            this.fileExitMenuItem.Name = "fileExitMenuItem";
            this.fileExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.fileExitMenuItem.Text = "E&xit";
            this.fileExitMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
            // 
            // browserMenuItem
            // 
            this.browserMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browserCreateMenuItem,
            this.browserCloseMenuItem,
            this.toolStripSeparator1,
            this.browserShowDevToolsMenuItem,
            this.browserCloseDevToolsMenuItem,
            this.toolStripSeparator2,
            this.browserZoomInMenuItem,
            this.browserZoomOutMenuItem,
            this.browserResetZoomMenuItem,
            this.toolStripSeparator3,
            this.browserGetFrameNamesMenuItem});
            this.browserMenuItem.Name = "browserMenuItem";
            this.browserMenuItem.Size = new System.Drawing.Size(61, 20);
            this.browserMenuItem.Text = "&Browser";
            // 
            // browserCreateMenuItem
            // 
            this.browserCreateMenuItem.Name = "browserCreateMenuItem";
            this.browserCreateMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserCreateMenuItem.Text = "Create...";
            this.browserCreateMenuItem.Click += new System.EventHandler(this.browserCreateMenuItem_Click);
            // 
            // browserCloseMenuItem
            // 
            this.browserCloseMenuItem.Name = "browserCloseMenuItem";
            this.browserCloseMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserCloseMenuItem.Text = "Close";
            this.browserCloseMenuItem.Click += new System.EventHandler(this.browserCloseMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // browserShowDevToolsMenuItem
            // 
            this.browserShowDevToolsMenuItem.Name = "browserShowDevToolsMenuItem";
            this.browserShowDevToolsMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserShowDevToolsMenuItem.Text = "Show Developer Tools";
            this.browserShowDevToolsMenuItem.Click += new System.EventHandler(this.browserShowDevToolsMenuItem_Click);
            // 
            // browserCloseDevToolsMenuItem
            // 
            this.browserCloseDevToolsMenuItem.Name = "browserCloseDevToolsMenuItem";
            this.browserCloseDevToolsMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserCloseDevToolsMenuItem.Text = "Close Developer Tools";
            this.browserCloseDevToolsMenuItem.Click += new System.EventHandler(this.browserCloseDevToolsMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // browserZoomInMenuItem
            // 
            this.browserZoomInMenuItem.Name = "browserZoomInMenuItem";
            this.browserZoomInMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserZoomInMenuItem.Text = "Zoom In";
            this.browserZoomInMenuItem.Click += new System.EventHandler(this.browserZoomInMenuItem_Click);
            // 
            // browserZoomOutMenuItem
            // 
            this.browserZoomOutMenuItem.Name = "browserZoomOutMenuItem";
            this.browserZoomOutMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserZoomOutMenuItem.Text = "Zoom Out";
            this.browserZoomOutMenuItem.Click += new System.EventHandler(this.browserZoomOutMenuItem_Click);
            // 
            // browserResetZoomMenuItem
            // 
            this.browserResetZoomMenuItem.Name = "browserResetZoomMenuItem";
            this.browserResetZoomMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserResetZoomMenuItem.Text = "Reset Zoom";
            this.browserResetZoomMenuItem.Click += new System.EventHandler(this.browserResetZoomMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // browserGetFrameNamesMenuItem
            // 
            this.browserGetFrameNamesMenuItem.Name = "browserGetFrameNamesMenuItem";
            this.browserGetFrameNamesMenuItem.Size = new System.Drawing.Size(191, 22);
            this.browserGetFrameNamesMenuItem.Text = "Get Frame Names";
            this.browserGetFrameNamesMenuItem.Click += new System.EventHandler(this.browserGetFrameNamesMenuItem_Click);
            // 
            // consoleMenuItem
            // 
            this.consoleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consoleShowOrHideMenuItem,
            this.consoleClearMenuItem});
            this.consoleMenuItem.Name = "consoleMenuItem";
            this.consoleMenuItem.Size = new System.Drawing.Size(62, 20);
            this.consoleMenuItem.Text = "&Console";
            // 
            // consoleShowOrHideMenuItem
            // 
            this.consoleShowOrHideMenuItem.Name = "consoleShowOrHideMenuItem";
            this.consoleShowOrHideMenuItem.Size = new System.Drawing.Size(103, 22);
            this.consoleShowOrHideMenuItem.Text = "&Show";
            this.consoleShowOrHideMenuItem.Click += new System.EventHandler(this.consoleShowOrHideMenuItem_Click);
            // 
            // consoleClearMenuItem
            // 
            this.consoleClearMenuItem.Name = "consoleClearMenuItem";
            this.consoleClearMenuItem.Size = new System.Drawing.Size(103, 22);
            this.consoleClearMenuItem.Text = "&Clear";
            this.consoleClearMenuItem.Click += new System.EventHandler(this.consoleClearMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invokeScriptToolStripMenuItem,
            this.waitDocumentToolStripMenuItem,
            this.visitDOMToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // invokeScriptToolStripMenuItem
            // 
            this.invokeScriptToolStripMenuItem.Name = "invokeScriptToolStripMenuItem";
            this.invokeScriptToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.invokeScriptToolStripMenuItem.Text = "&InvokeScript";
            this.invokeScriptToolStripMenuItem.Click += new System.EventHandler(this.invokeScriptToolStripMenuItem_Click);
            // 
            // waitDocumentToolStripMenuItem
            // 
            this.waitDocumentToolStripMenuItem.Name = "waitDocumentToolStripMenuItem";
            this.waitDocumentToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.waitDocumentToolStripMenuItem.Text = "Wait Document";
            this.waitDocumentToolStripMenuItem.Click += new System.EventHandler(this.waitDocumentToolStripMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // helpAboutMenuItem
            // 
            this.helpAboutMenuItem.Name = "helpAboutMenuItem";
            this.helpAboutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.helpAboutMenuItem.Text = "&About...";
            this.helpAboutMenuItem.Click += new System.EventHandler(this.helpAboutMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "blue_arrow_left_16.png");
            this.imageList.Images.SetKeyName(1, "blue_arrow_right_16.png");
            this.imageList.Images.SetKeyName(2, "close_16.png");
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goBackButton,
            this.goForwardButton,
            this.stopButton,
            this.reloadButton,
            this.homeButton,
            this.addressTextBox,
            this.goButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip.TabIndex = 2;
            // 
            // goBackButton
            // 
            this.goBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.goBackButton.Enabled = false;
            this.goBackButton.Image = global::CefGlue.Client.Properties.Resources.blue_arrow_left_16;
            this.goBackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goBackButton.Name = "goBackButton";
            this.goBackButton.Size = new System.Drawing.Size(23, 22);
            this.goBackButton.Text = "Back";
            this.goBackButton.Click += new System.EventHandler(this.goBackButton_Click);
            // 
            // goForwardButton
            // 
            this.goForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.goForwardButton.Enabled = false;
            this.goForwardButton.Image = global::CefGlue.Client.Properties.Resources.blue_arrow_right_16;
            this.goForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goForwardButton.Name = "goForwardButton";
            this.goForwardButton.Size = new System.Drawing.Size(23, 22);
            this.goForwardButton.Text = "Forward";
            this.goForwardButton.Click += new System.EventHandler(this.goForwardButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopButton.Enabled = false;
            this.stopButton.Image = global::CefGlue.Client.Properties.Resources.close_16;
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(23, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // reloadButton
            // 
            this.reloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reloadButton.Enabled = false;
            this.reloadButton.Image = global::CefGlue.Client.Properties.Resources.reload_16;
            this.reloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(23, 22);
            this.reloadButton.Text = "Reload";
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homeButton.Image = global::CefGlue.Client.Properties.Resources.home_16;
            this.homeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(23, 22);
            this.homeButton.Text = "Home";
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // addressTextBox
            // 
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(858, 25);
            this.addressTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressTextBox_KeyPress);
            // 
            // goButton
            // 
            this.goButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.goButton.Image = global::CefGlue.Client.Properties.Resources.go_16;
            this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(23, 22);
            this.goButton.Text = "Go";
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 658);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(993, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "statusLabel";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Visible = false;
            // 
            // visitDOMToolStripMenuItem
            // 
            this.visitDOMToolStripMenuItem.Name = "visitDOMToolStripMenuItem";
            this.visitDOMToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.visitDOMToolStripMenuItem.Text = "Visit DOM";
            this.visitDOMToolStripMenuItem.Click += new System.EventHandler(this.visitDOMToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 680);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "CefGlue Client";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserCloseMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem browserShowDevToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserCloseDevToolsMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton goBackButton;
        private System.Windows.Forms.ToolStripButton goForwardButton;
        private System.Windows.Forms.ToolStripButton reloadButton;
        private ToolStripSpringTextBox addressTextBox;
        private System.Windows.Forms.ToolStripButton goButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem consoleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleShowOrHideMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleClearMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem browserZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserZoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browserResetZoomMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem browserGetFrameNamesMenuItem;
        private System.Windows.Forms.ToolStripButton homeButton;
        private System.Windows.Forms.ToolStripMenuItem browserCreateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invokeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waitDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitDOMToolStripMenuItem;
    }
}

