using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class PlayerPanel<T> : Panel where T : Player
   {
      private Label _playerNameLabel, _heroNameLabel, _xpLabel, _influenceLabel, _itemListLabel,
            _classCardListLabel, _rewardListLabel;
      private Button _addItemButton, _addClassCardButton, _addRewardButton;
      private ComboBox _addItemCombo, _addClassCombo, _addRewardCombo;
      private ListView _itemListView, _classCardListView, _rewardListView;
      private ContextMenu _tradeItemContextMenu;
      private MenuItem _tradeMenuItem;
         
      public void Initialise()
      {
         this.Size = new System.Drawing.Size(705, 100);
         this.BorderStyle = BorderStyle.FixedSingle;
         
         //player name label
         _playerNameLabel = new Label();
         _playerNameLabel.Size = new System.Drawing.Size(80, 25);
         _playerNameLabel.Location = new System.Drawing.Point(5, 5);
         this.Controls.Add(_playerNameLabel);
         
         //hero name label
         _heroNameLabel = new Label();
         _heroNameLabel.Size = new System.Drawing.Size(80, 25);
         _heroNameLabel.Location = new System.Drawing.Point(5, 30);
         this.Controls.Add(_heroNameLabel);
         
         //xp label
         _xpLabel = new Label();
         _xpLabel.Size = new System.Drawing.Size(40, 25);
         _xpLabel.Location = new System.Drawing.Point(90, 5);
         this.Controls.Add(_xpLabel);
         
         //influence label
         _influenceLabel = new Label();
         _influenceLabel.Size = new System.Drawing.Size(40, 25);
         _influenceLabel.Location = new System.Drawing.Point(90, 30);
         this.Controls.Add(_influenceLabel);
         
         //item list label
         _itemListLabel = new Label();
         if (typeof(T) == typeof(HeroPlayer))
            _itemListLabel.Text = "Items";
         else if (typeof(T) == typeof(ImperialPlayer))
            _itemListLabel.Text = "Agenda";
         _itemListLabel.Size = new System.Drawing.Size(43, 25);
         _itemListLabel.Location = new System.Drawing.Point(135, 5);
         this.Controls.Add(_itemListLabel);
         
         //add item combo
         _addItemCombo = new ComboBox();
         _addItemCombo.Size = new System.Drawing.Size(100, 30);
         _addItemCombo.Location = new System.Drawing.Point(178, 5);
         _addItemCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _addItemCombo.DropDownWidth = 200;
         this.Controls.Add(_addItemCombo);
         
         //add item button
         _addItemButton = new Button();
         _addItemButton.Text = "Add";
         _addItemButton.Size = new System.Drawing.Size(40, 20);
         _addItemButton.Location = new System.Drawing.Point(280, 5);
         this.Controls.Add(_addItemButton);
         
         //item list
         _itemListView = new ListView();
         _itemListView.Size = new System.Drawing.Size(185, 60);
         _itemListView.Location = new System.Drawing.Point(135, 30);
         _itemListView.FullRowSelect = true;
         _itemListView.MultiSelect = false;
         _itemListView.View = View.Details;
         _itemListView.Columns.Add("Id");
         _itemListView.Columns.Add("Name");
         _itemListView.Columns.Add("Cost");
         if (typeof(T) == typeof(HeroPlayer))
            _itemListView.Columns.Add("Tier");
         _itemListView.Columns[0].Width = 0;
         this.Controls.Add(_itemListView);
         
         //class list label
         _classCardListLabel = new Label();
         _classCardListLabel.Text = "Class";
         _classCardListLabel.Size = new System.Drawing.Size(40, 25);
         _classCardListLabel.Location = new System.Drawing.Point(325, 5);
         this.Controls.Add(_classCardListLabel);
         
         //add class card combo
         _addClassCombo = new ComboBox();
         _addClassCombo.Size = new System.Drawing.Size(100, 30);
         _addClassCombo.Location = new System.Drawing.Point(365, 5);
         _addClassCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _addClassCombo.DropDownWidth = 200;
         this.Controls.Add(_addClassCombo);
         
         //add class card button
         _addClassCardButton = new Button();
         _addClassCardButton.Size = new System.Drawing.Size(40, 20);
         _addClassCardButton.Text = "Add";
         _addClassCardButton.Location = new System.Drawing.Point(470, 5);
         this.Controls.Add(_addClassCardButton);
         
         //class card list
         _classCardListView = new ListView();
         _classCardListView.Size = new System.Drawing.Size(185, 60);
         _classCardListView.Location = new System.Drawing.Point(325, 30);
         _classCardListView.FullRowSelect = true;
         _classCardListView.MultiSelect = false;
         _classCardListView.View = View.Details;
         _classCardListView.Columns.Add("Id");
         _classCardListView.Columns.Add("Name");
         _classCardListView.Columns.Add("Cost");
         _classCardListView.Columns[0].Width = 0;
         this.Controls.Add(_classCardListView);
         
         //reward list label
         _rewardListLabel = new Label();
         _rewardListLabel.Text = "Reward";
         _rewardListLabel.Size = new System.Drawing.Size(43, 25);
         _rewardListLabel.Location = new System.Drawing.Point(515, 5);
         this.Controls.Add(_rewardListLabel);
         
         //add reward combo
         _addRewardCombo = new ComboBox();
         _addRewardCombo.Size = new System.Drawing.Size(100, 30);
         _addRewardCombo.Location = new System.Drawing.Point(558, 5);
         _addRewardCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _addRewardCombo.DropDownWidth = 200;
         this.Controls.Add(_addRewardCombo);
         
         //add reward button
         _addRewardButton = new Button();
         _addRewardButton.Size = new System.Drawing.Size(40, 20);
         _addRewardButton.Text = "Add";
         _addRewardButton.Location = new System.Drawing.Point(660, 5);
         this.Controls.Add(_addRewardButton);
         
         //reward list
         _rewardListView = new ListView();
         _rewardListView.Size = new System.Drawing.Size(185, 60);
         _rewardListView.Location = new System.Drawing.Point(515, 30);
         _rewardListView.FullRowSelect = true;
         _rewardListView.MultiSelect = false;
         _rewardListView.View = View.Details;
         _rewardListView.Columns.Add("Id");
         _rewardListView.Columns.Add("Name");
         _rewardListView.Columns[0].Width = 0;
         this.Controls.Add(_rewardListView);
         
         //Item trade context menu
         _tradeItemContextMenu = new ContextMenu();
         _tradeMenuItem = new MenuItem("Trade");
         _tradeItemContextMenu.MenuItems.Add(_tradeMenuItem);
      }
   }
}

