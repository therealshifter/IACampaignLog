using System;
using System.Windows.Forms;

namespace IACampaignLog
{
	public partial class MenuForm : Form
	{
		private MainMenu _appMenu;
		private MenuItem _fileMenuItem, _exitMenuItem, _maintainMenuItem;
		private Button _createButton, _openButton;
      private Panel _bottomButtonsPanel;
      private ListView _gameListView;
      	
		private void Initialise()
		{
			this.Text = "Imperial Assault Campaign Log";
			this.Size = new System.Drawing.Size(600, 500);
			
			_appMenu = new MainMenu();
			_fileMenuItem = new MenuItem("File");
			_maintainMenuItem = new MenuItem("Maintain Resources");
			_exitMenuItem = new MenuItem("Exit");
			_fileMenuItem.MenuItems.Add(_maintainMenuItem);
			_fileMenuItem.MenuItems.Add(_exitMenuItem);
			_appMenu.MenuItems.Add(_fileMenuItem);
			this.Menu = _appMenu;
         
         //Game list
         _gameListView = new ListView();
         _gameListView.Dock = DockStyle.Fill;
         _gameListView.BorderStyle = BorderStyle.Fixed3D;
         _gameListView.FullRowSelect = true;
         _gameListView.MultiSelect = false;
         _gameListView.Columns.Add("Id");
         _gameListView.Columns.Add("Game Date");
         _gameListView.Columns.Add("Imperial");
         _gameListView.Columns.Add("Player 1");
         _gameListView.Columns.Add("Player 2");
         _gameListView.Columns.Add("Player 3");
         _gameListView.Columns.Add("Player 4");
         _gameListView.Columns.Add("Campaign");
         _gameListView.Columns[0].Width = 0;
         _gameListView.Columns[1].Width += 20;
         _gameListView.Columns[2].Width += 20;
         _gameListView.Columns[3].Width += 20;
         _gameListView.Columns[4].Width += 20;
         _gameListView.Columns[5].Width += 20;
         _gameListView.Columns[6].Width += 20;
         _gameListView.Columns[7].Width += 30;
         _gameListView.View = View.Details;
         this.Controls.Add(_gameListView);
			
			//bottom buttons
			_bottomButtonsPanel = new Panel();
			_bottomButtonsPanel.Dock = DockStyle.Bottom;
			_bottomButtonsPanel.Height = 40;
			//Create button
			_createButton = new Button();
			_createButton.Text = "New Game";
			_createButton.Size = new System.Drawing.Size(70, 30);
			_createButton.Location = new System.Drawing.Point(50, 5);
			_createButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			_bottomButtonsPanel.Controls.Add(_createButton);
			//Open button
			_openButton = new Button();
			_openButton.Text = "Open";
			_openButton.Size = new System.Drawing.Size(70, 30);
			_openButton.Location = new System.Drawing.Point(125, 5);
			_openButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
         _openButton.Enabled = false;
			_bottomButtonsPanel.Controls.Add(_openButton);
			
			this.Controls.Add(_bottomButtonsPanel);
		}
	}
}

