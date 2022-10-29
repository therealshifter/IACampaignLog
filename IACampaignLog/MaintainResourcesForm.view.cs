using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace IACampaignLog
{
   public partial class MaintainResourcesForm : EditFormBase
   {
      private Panel _mainControlsPanel, _agendaPanel, _characterPanel, _classPanel,
                    _itemPanel, _rewardPanel, _sideMissionPanel, _storyMissionPanel,
                    _campaignPanel, _threatMissionPanel;
      private ComboBox _maintainCombo, _agendaSetCombo, _classSetCombo, _agendaTypeCombo,
                       _itemTierComboBox, _classCharacterCombo, _storyMissionCampaignCombo,
                       _sideMissionTypeCombo, _threatMissionBaneCombo, _characterSideMissionCombo, 
                       _campaignMissionTypeCombo, _campaignMissionNameCombo,
                       _campaignMissionItemTiersCombo, _rewardTypeCombo;
      private TextBox _nameText, _agendaInfluenceText, _itemCreditText, _classXpText, _agendaDiscardCostText;
      private Button _addButton, _saveButton, _cancelButton, _campaignAddMissionButton,
                     _campaignSaveMissionButton, _campaignCancelMissionButton;
      private Label _agendaSetLabel, _nameLabel, _agendaInfluenceLabel, _classSetLabel,
                    _classXpLabel, _itemCreditLabel, _itemTierLabel, _storyMissionCampaignLabel,
                    _sideMissionColourLabel, _characterSideMissionLabel, _threatMissionBaneLabel,
                    _campaignStartingRebelXPLabel, _campaignStartingImpXPLabel,
                    _campaignStartingInfluenceLabel, _campaignStartingCreditsLabel,
                    _campaignMissionNameLabel, _campaignMissionTypeLabel, _rewardTypeLabel,
                    _campaignMissionThreatLevelLabel, _campaignMissionItemTiersLabel, _agendaDiscardCostLabel;
      private ListView _agendasListView, _charactersListView, _classesListView, 
                     _itemsListView, _rewardsListView, _sideMissionsListView, _threatMissionListView,
                     _storyMissionsListView, _campaignListView, _campaignMissionListView;
      private CheckBox _campaignAllowSidesCheckBox, _campaignAllowForcedCheckBox, _campaignIncludeThreatCheckBox,
                     _classCardIsItemCheckBox;
      private GroupBox _campaignMissionsGroupBox;
      private NumericUpDown _campaignStartingRebelXpText, _campaignStartingImpXpText,
                            _campaignStartingCreditsText, _campaignStartingInfluenceText,
                            _campaignMissionThreatLevelText;
      
      private void Initialise ()
      {
         this.Text = "Maintain Resources";
         
         //top panel
         _mainControlsPanel = new Panel();
         _mainControlsPanel.Dock = DockStyle.Top;
         _mainControlsPanel.Height = 85;
         //Maintain combo
         _maintainCombo = new ComboBox();
         _maintainCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _maintainCombo.Location = new System.Drawing.Point(5, 5);
         _mainControlsPanel.Controls.Add(_maintainCombo);
         //Name label
         _nameLabel = new Label();
         _nameLabel.Text = "Name";
         _nameLabel.Size = new System.Drawing.Size(250, 20);
         _nameLabel.Location = new System.Drawing.Point(5, 40);
         _mainControlsPanel.Controls.Add(_nameLabel);
         //Name text box
         _nameText = new TextBox();
         _nameText.Size = new System.Drawing.Size(250, 30);
         _nameText.Location = new System.Drawing.Point(5, 60);
         _mainControlsPanel.Controls.Add(_nameText);
         //Add button
         _addButton = new Button();
         _addButton.Text = "Add";
         _addButton.Size = new System.Drawing.Size(50, 30);
         _addButton.Location = new System.Drawing.Point(265, 55);
         _mainControlsPanel.Controls.Add(_addButton);
         //Save button
         _saveButton = new Button();
         _saveButton.Text = "Save";
         _saveButton.Size = new System.Drawing.Size(50, 30);
         _saveButton.Location = new System.Drawing.Point(370, 55);
         _mainControlsPanel.Controls.Add(_saveButton);
         //Cancel button
         _cancelButton = new Button();
         _cancelButton.Text = "Cancel";
         _cancelButton.Size = new System.Drawing.Size(50, 30);
         _cancelButton.Location = new System.Drawing.Point(425, 55);
         _mainControlsPanel.Controls.Add(_cancelButton);
         
         //Agenda panel
         _agendaPanel = new Panel();
         _agendaPanel.Visible = false;
         _agendaPanel.Dock = DockStyle.Fill;
         //Agenda set label
         _agendaSetLabel = new Label();
         _agendaSetLabel.Text = "Agenda Set (select or enter new)";
         _agendaSetLabel.Size = new System.Drawing.Size(250, 20);
         _agendaSetLabel.Location = new System.Drawing.Point(5, 90);
         _agendaPanel.Controls.Add(_agendaSetLabel);
         //Agenda set combo box
         _agendaSetCombo = new ComboBox();
         _agendaSetCombo.Size = new System.Drawing.Size(200, 30);
         _agendaSetCombo.Location = new System.Drawing.Point(5, 110);
         _agendaPanel.Controls.Add(_agendaSetCombo);
         //Agenda influence label
         _agendaInfluenceLabel = new Label();
         _agendaInfluenceLabel.Size = new System.Drawing.Size(200, 20);
         _agendaInfluenceLabel.Location = new System.Drawing.Point(5, 145);
         _agendaInfluenceLabel.Text = "Influence";
         _agendaPanel.Controls.Add(_agendaInfluenceLabel);
         //Agenda influence text box
         _agendaInfluenceText = new TextBox();
         _agendaInfluenceText.Size = new System.Drawing.Size(200, 30);
         _agendaInfluenceText.Location = new System.Drawing.Point(5, 165);
         _agendaPanel.Controls.Add(_agendaInfluenceText);
         //Agenda discard cost label
         _agendaDiscardCostLabel = new Label();
         _agendaDiscardCostLabel.Size = new System.Drawing.Size(200, 20);
         _agendaDiscardCostLabel.Location =  new System.Drawing.Point(225, 145);
         _agendaDiscardCostLabel.Text = "Discard Cost";
         _agendaPanel.Controls.Add(_agendaDiscardCostLabel);
         //Agenda discard cost text box
         _agendaDiscardCostText = new TextBox();
         _agendaDiscardCostText.Size = new System.Drawing.Size(200, 30);
         _agendaDiscardCostText.Location = new System.Drawing.Point(225, 165);
         _agendaPanel.Controls.Add(_agendaDiscardCostText);
         //Agenda type combo
         _agendaTypeCombo = new ComboBox();
         _agendaTypeCombo.Size = new System.Drawing.Size(100, 30);
         _agendaTypeCombo.Location = new System.Drawing.Point(5, 200);
         _agendaTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _agendaPanel.Controls.Add(_agendaTypeCombo);
         //Agendas list view
         _agendasListView = new ListView();
         _agendasListView.Size = new System.Drawing.Size(475, 170);
         _agendasListView.Location = new System.Drawing.Point(5, 240);
         _agendasListView.FullRowSelect = true;
         _agendasListView.MultiSelect = false;
         _agendasListView.View = View.Details;
         _agendasListView.Columns.Add("Id");
         _agendasListView.Columns.Add("Set");
         _agendasListView.Columns.Add("Name");
         _agendasListView.Columns.Add("INF");
         _agendasListView.Columns.Add("Type");
         _agendasListView.Columns.Add("DC");
         _agendasListView.Columns[0].Width = 0;
         _agendasListView.Columns[1].Width += 90;
         _agendasListView.Columns[2].Width += 90;
         _agendasListView.Columns[3].Width -= 30;
         _agendasListView.Columns[4].Width += 20;
         _agendasListView.Columns[5].Width -= 20;
         _agendaPanel.Controls.Add(_agendasListView);
         
         //Campaign panel
         _campaignPanel = new Panel();
         _campaignPanel.Visible = false;
         _campaignPanel.Dock = DockStyle.Fill;
         //Campaign allow side missions check box
         _campaignAllowSidesCheckBox = new CheckBox();
         _campaignAllowSidesCheckBox.Size = new System.Drawing.Size(150, 25);
         _campaignAllowSidesCheckBox.Location = new System.Drawing.Point(10, 90);
         _campaignAllowSidesCheckBox.Text = "Allow Side Missions";
         _campaignAllowSidesCheckBox.Checked = true;
         _campaignPanel.Controls.Add(_campaignAllowSidesCheckBox);
         //Campaign allow forced missions check box
         _campaignAllowForcedCheckBox = new CheckBox();
         _campaignAllowForcedCheckBox.Size = new System.Drawing.Size(150, 25);
         _campaignAllowForcedCheckBox.Location = new System.Drawing.Point(160, 90);
         _campaignAllowForcedCheckBox.Text = "Allow Forced Missions";
         _campaignAllowForcedCheckBox.Checked = true;
         _campaignPanel.Controls.Add(_campaignAllowForcedCheckBox);
         //Campaign allow threat missions check box
         _campaignIncludeThreatCheckBox = new CheckBox();
         _campaignIncludeThreatCheckBox.Size = new System.Drawing.Size(150, 25);
         _campaignIncludeThreatCheckBox.Location = new System.Drawing.Point(320, 90);
         _campaignIncludeThreatCheckBox.Text = "Include Threat Missions";
         _campaignIncludeThreatCheckBox.Checked = false;
         _campaignPanel.Controls.Add(_campaignIncludeThreatCheckBox);
         //Campaign starting rebel xp label
         _campaignStartingRebelXPLabel = new Label();
         _campaignStartingRebelXPLabel.Text = "Starting Rebel XP";
         _campaignStartingRebelXPLabel.Size = new System.Drawing.Size(60, 30);
         _campaignStartingRebelXPLabel.Location = new System.Drawing.Point(5, 120);
         _campaignPanel.Controls.Add(_campaignStartingRebelXPLabel);
         //Campaign starting rebel xp textbox
         _campaignStartingRebelXpText = new NumericUpDown();
         _campaignStartingRebelXpText.Size = new System.Drawing.Size(50, 30);
         _campaignStartingRebelXpText.Location = new System.Drawing.Point(65, 120);
         _campaignPanel.Controls.Add(_campaignStartingRebelXpText);
         //Campaign starting imp xp label
         _campaignStartingImpXPLabel = new Label();
         _campaignStartingImpXPLabel.Text = "Starting Imp XP";
         _campaignStartingImpXPLabel.Size = new System.Drawing.Size(50, 30);
         _campaignStartingImpXPLabel.Location = new System.Drawing.Point(120, 120);
         _campaignPanel.Controls.Add(_campaignStartingImpXPLabel);
         //Campaign starting imp xp textbox
         _campaignStartingImpXpText = new NumericUpDown();
         _campaignStartingImpXpText.Size = new System.Drawing.Size(50, 30);
         _campaignStartingImpXpText.Location = new System.Drawing.Point(170, 120);
         _campaignPanel.Controls.Add(_campaignStartingImpXpText);
         //Campaign starting credits label
         _campaignStartingCreditsLabel = new Label();
         _campaignStartingCreditsLabel.Text = "Starting Credits (per player)";
         _campaignStartingCreditsLabel.Size = new System.Drawing.Size(90, 30);
         _campaignStartingCreditsLabel.Location = new System.Drawing.Point(225, 120);
         _campaignPanel.Controls.Add(_campaignStartingCreditsLabel);
         //Campaign starting credits textbox
         _campaignStartingCreditsText = new NumericUpDown();
         _campaignStartingCreditsText.Size = new System.Drawing.Size(50, 30);
         _campaignStartingCreditsText.Location = new System.Drawing.Point(315, 120);
         _campaignStartingCreditsText.Increment = 50;
         _campaignStartingCreditsText.Maximum = 10000;
         _campaignPanel.Controls.Add(_campaignStartingCreditsText);
         //Campaign starting influence label
         _campaignStartingInfluenceLabel = new Label();
         _campaignStartingInfluenceLabel.Text = "Starting Influence";
         _campaignStartingInfluenceLabel.Size = new System.Drawing.Size(50, 30);
         _campaignStartingInfluenceLabel.Location = new System.Drawing.Point(370, 120);
         _campaignPanel.Controls.Add(_campaignStartingInfluenceLabel);
         //Campaign starting influence textbox
         _campaignStartingInfluenceText = new NumericUpDown();
         _campaignStartingInfluenceText.Size = new System.Drawing.Size(50, 30);
         _campaignStartingInfluenceText.Location = new System.Drawing.Point(425, 120);
         _campaignPanel.Controls.Add(_campaignStartingInfluenceText);
         
         //Campaign missions panel
         _campaignMissionsGroupBox = new GroupBox();
         _campaignMissionsGroupBox.Text = "Mission Template";
         _campaignMissionsGroupBox.Size = new System.Drawing.Size(475, 190);
         _campaignMissionsGroupBox.Location = new System.Drawing.Point(5, 150);
         _campaignMissionsGroupBox.BackColor = System.Drawing.Color.LightGray;
         _campaignPanel.Controls.Add(_campaignMissionsGroupBox);
         //Campaign mission name label
         _campaignMissionNameLabel = new Label();
         _campaignMissionNameLabel.Text = "Name";
         _campaignMissionNameLabel.Size = new System.Drawing.Size(100, 15);
         _campaignMissionNameLabel.Location = new System.Drawing.Point(5, 20);
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionNameLabel);
         //Campaign mission name combo
         _campaignMissionNameCombo = new ComboBox();
         _campaignMissionNameCombo.Size = new System.Drawing.Size(145, 30);
         _campaignMissionNameCombo.Location = new System.Drawing.Point(5, 35);
         _campaignMissionNameCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _campaignMissionNameCombo.DropDownWidth = 200;
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionNameCombo);
         //Campaign mission type label
         _campaignMissionTypeLabel = new Label();
         _campaignMissionTypeLabel.Text = "Type";
         _campaignMissionTypeLabel.Size = new System.Drawing.Size(100, 15);
         _campaignMissionTypeLabel.Location = new System.Drawing.Point(150, 20);
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionTypeLabel);
         //Campaign mission type combo
         _campaignMissionTypeCombo = new ComboBox();
         _campaignMissionTypeCombo.Size = new System.Drawing.Size(100, 30);
         _campaignMissionTypeCombo.Location = new System.Drawing.Point(150, 35);
         _campaignMissionTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionTypeCombo);
         //Campaign mission threat label
         _campaignMissionThreatLevelLabel = new Label();
         _campaignMissionThreatLevelLabel.Text = "Threat lvl";
         _campaignMissionThreatLevelLabel.Size = new System.Drawing.Size(55, 15);
         _campaignMissionThreatLevelLabel.Location = new System.Drawing.Point(250, 20);
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionThreatLevelLabel);
         //Campaign threat text
         _campaignMissionThreatLevelText = new NumericUpDown();
         _campaignMissionThreatLevelText.Size = new System.Drawing.Size(55, 30);
         _campaignMissionThreatLevelText.Location = new System.Drawing.Point(250, 35);
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionThreatLevelText);
         //Campaign mission item tier label
         _campaignMissionItemTiersLabel = new Label();
         _campaignMissionItemTiersLabel.Text = "Item Tiers";
         _campaignMissionItemTiersLabel.Size = new System.Drawing.Size(55, 15);
         _campaignMissionItemTiersLabel.Location = new System.Drawing.Point(305, 20);
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionItemTiersLabel);
         //Campaign missions item tier combo
         _campaignMissionItemTiersCombo = new ComboBox();
         _campaignMissionItemTiersCombo.Size = new System.Drawing.Size(50, 30);
         _campaignMissionItemTiersCombo.Location = new System.Drawing.Point(305, 35);
         _campaignMissionItemTiersCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionItemTiersCombo);
         //Campaign mission add button
         _campaignAddMissionButton = new Button();
         _campaignAddMissionButton.Text = "+";
         _campaignAddMissionButton.Size = new System.Drawing.Size(30, 20);
         _campaignAddMissionButton.Location = new System.Drawing.Point(365, 35);
         _campaignMissionsGroupBox.Controls.Add(_campaignAddMissionButton);
         //Campaign mission save button
         _campaignSaveMissionButton = new Button();
         _campaignSaveMissionButton.Text = ">>";
         _campaignSaveMissionButton.Size = new System.Drawing.Size(30, 20);
         _campaignSaveMissionButton.Location = new System.Drawing.Point(410, 35);
         _campaignMissionsGroupBox.Controls.Add(_campaignSaveMissionButton);
         //Campaign mission cancel button
         _campaignCancelMissionButton = new Button();
         _campaignCancelMissionButton.Text = "x";
         _campaignCancelMissionButton.Size = new System.Drawing.Size(30, 20);
         _campaignCancelMissionButton.Location = new System.Drawing.Point(440, 35);
         _campaignMissionsGroupBox.Controls.Add(_campaignCancelMissionButton);
         //Campaign mission list view
         _campaignMissionListView = new ListView();
         _campaignMissionListView.Size = new System.Drawing.Size(465, 117);
         _campaignMissionListView.Location = new System.Drawing.Point(5, 62);
         _campaignMissionListView.FullRowSelect = true;
         _campaignMissionListView.MultiSelect = false;
         _campaignMissionListView.View = View.Details;
         _campaignMissionListView.Columns.Add("#");
         _campaignMissionListView.Columns.Add("Type");
         _campaignMissionListView.Columns.Add("Name");
         _campaignMissionListView.Columns.Add("Threat Lvl");
         _campaignMissionListView.Columns.Add("Item Tiers");
         _campaignMissionListView.Columns[0].Width = 30;
         _campaignMissionListView.Columns[1].Width += 50;
         _campaignMissionListView.Columns[2].Width += 100;
         _campaignMissionsGroupBox.Controls.Add(_campaignMissionListView);
         
         //Campaign list view
         _campaignListView = new ListView();
         _campaignListView.Size = new System.Drawing.Size(475, 80);
         _campaignListView.Location = new System.Drawing.Point(5, 345);
         _campaignListView.FullRowSelect = true;
         _campaignListView.MultiSelect = false;
         _campaignListView.View = View.Details;
         _campaignListView.Columns.Add("Id");
         _campaignListView.Columns.Add("Name");
         _campaignListView.Columns.Add("SM");
         _campaignListView.Columns.Add("FM");
         _campaignListView.Columns.Add("TM");
         _campaignListView.Columns.Add("Reb XP");
         _campaignListView.Columns.Add("Cred's");
         _campaignListView.Columns.Add("Imp XP");
         _campaignListView.Columns.Add("INF");
         _campaignListView.Columns.Add("# M's");
         _campaignListView.Columns[0].Width = 0;
         _campaignListView.Columns[1].Width += 40;
         _campaignListView.Columns[2].Width -= 20;
         _campaignListView.Columns[3].Width -= 20;
         _campaignListView.Columns[4].Width -= 20;
         _campaignListView.Columns[5].Width -= 10;
         _campaignListView.Columns[6].Width -= 10;
         _campaignListView.Columns[7].Width -= 10;
         _campaignListView.Columns[8].Width -= 20;
         _campaignListView.Columns[9].Width -= 20;
         _campaignPanel.Controls.Add(_campaignListView);
         
         //Character panel
         _characterPanel = new Panel();
         _characterPanel.Visible = false;
         _characterPanel.Dock = DockStyle.Fill;
         //Character Side mission label
         _characterSideMissionLabel = new Label();
         _characterSideMissionLabel.Text = "Side Mission";
         _characterSideMissionLabel.Size = new System.Drawing.Size(250, 20);
         _characterSideMissionLabel.Location = new System.Drawing.Point(5, 90);
         _characterPanel.Controls.Add(_characterSideMissionLabel);
         //Character Side mission colour combo box
         _characterSideMissionCombo = new ComboBox();
         _characterSideMissionCombo.Size = new System.Drawing.Size(200, 30);
         _characterSideMissionCombo.Location = new System.Drawing.Point(5, 110);
         _characterSideMissionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _characterPanel.Controls.Add(_characterSideMissionCombo);
         //Characters list view
         _charactersListView = new ListView();
         _charactersListView.Size = new System.Drawing.Size(470, 200);
         _charactersListView.Location = new System.Drawing.Point(5, 200);
         _charactersListView.FullRowSelect = true;
         _charactersListView.MultiSelect = false;
         _charactersListView.View = View.Details;
         _charactersListView.Columns.Add("Id");
         _charactersListView.Columns.Add("Name");
         _charactersListView.Columns.Add("Related Mission");
         _charactersListView.Columns[0].Width = 0;
         _charactersListView.Columns[1].Width += 100;
         _charactersListView.Columns[2].Width += 100;
         _characterPanel.Controls.Add(_charactersListView);
         
         //Class panel
         _classPanel = new Panel();
         _classPanel.Visible = false;
         _classPanel.Dock = DockStyle.Fill;
         //Class set label
         _classSetLabel = new Label();
         _classSetLabel.Text = "Class Set (select or enter new)";
         _classSetLabel.Size = new System.Drawing.Size(250, 20);
         _classSetLabel.Location = new System.Drawing.Point(5, 90);
         _classPanel.Controls.Add(_classSetLabel);
         //Class set combo box
         _classSetCombo = new ComboBox();
         _classSetCombo.Size = new System.Drawing.Size(200, 30);
         _classSetCombo.Location = new System.Drawing.Point(5, 110);
         _classPanel.Controls.Add(_classSetCombo);
         //Class Associated character combo
         _classCharacterCombo = new ComboBox();
         _classCharacterCombo.Size = new System.Drawing.Size(200, 30);
         _classCharacterCombo.Location = new System.Drawing.Point(210, 110);
         _classCharacterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _classPanel.Controls.Add(_classCharacterCombo);
         //Class XP label
         _classXpLabel = new Label();
         _classXpLabel.Size = new System.Drawing.Size(250, 20);
         _classXpLabel.Location = new System.Drawing.Point(5, 145);
         _classXpLabel.Text = "XP";
         _classPanel.Controls.Add(_classXpLabel);
         //Class XP text box
         _classXpText = new TextBox();
         _classXpText.Size = new System.Drawing.Size(250, 30);
         _classXpText.Location = new System.Drawing.Point(5, 165);
         _classPanel.Controls.Add(_classXpText);
         //Class card is Item check box
         _classCardIsItemCheckBox = new CheckBox();
         _classCardIsItemCheckBox.Size = new System.Drawing.Size(250, 30);
         _classCardIsItemCheckBox.Location = new System.Drawing.Point(260, 165);
         _classCardIsItemCheckBox.Checked = false;
         _classCardIsItemCheckBox.Text = "Is Item";
         _classPanel.Controls.Add(_classCardIsItemCheckBox);
         //Class list view
         _classesListView = new ListView();
         _classesListView.Size = new System.Drawing.Size(470, 200);
         _classesListView.Location = new System.Drawing.Point(5, 200);
         _classesListView.FullRowSelect = true;
         _classesListView.MultiSelect = false;
         _classesListView.View = View.Details;
         _classesListView.Columns.Add("Id");
         _classesListView.Columns.Add("Set");
         _classesListView.Columns.Add("Name");
         _classesListView.Columns.Add("XP");
         _classesListView.Columns.Add("Character");
         _classesListView.Columns.Add("Item");
         _classesListView.Columns[0].Width = 0;
         _classesListView.Columns[1].Width += 85;
         _classesListView.Columns[2].Width += 80;
         _classesListView.Columns[3].Width -= 32;
         _classesListView.Columns[4].Width += 40;
         _classesListView.Columns[5].Width -= 27;
         _classPanel.Controls.Add(_classesListView);
         
         //Item panel
         _itemPanel = new Panel();
         _itemPanel.Visible = false;
         _itemPanel.Dock = DockStyle.Fill;
         //Item tier label
         _itemTierLabel = new Label();
         _itemTierLabel.Text = "Tier";
         _itemTierLabel.Size = new System.Drawing.Size(250, 20);
         _itemTierLabel.Location = new System.Drawing.Point(5, 90);
         _itemPanel.Controls.Add(_itemTierLabel);
         //Item tier combo box
         _itemTierComboBox = new ComboBox();
         _itemTierComboBox.Size = new System.Drawing.Size(200, 30);
         _itemTierComboBox.Location = new System.Drawing.Point(5, 110);
         _itemTierComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
         _itemPanel.Controls.Add(_itemTierComboBox);
         //Item Credits label
         _itemCreditLabel = new Label();
         _itemCreditLabel.Size = new System.Drawing.Size(250, 20);
         _itemCreditLabel.Location = new System.Drawing.Point(5, 145);
         _itemCreditLabel.Text = "Credits";
         _itemPanel.Controls.Add(_itemCreditLabel);
         //Item credits text box
         _itemCreditText = new TextBox();
         _itemCreditText.Size = new System.Drawing.Size(250, 30);
         _itemCreditText.Location = new System.Drawing.Point(5, 165);
         _itemPanel.Controls.Add(_itemCreditText);
         //Items list view
         _itemsListView = new ListView();
         _itemsListView.Size = new System.Drawing.Size(470, 200);
         _itemsListView.Location = new System.Drawing.Point(5, 200);
         _itemsListView.FullRowSelect = true;
         _itemsListView.MultiSelect = false;
         _itemsListView.View = View.Details;
         _itemsListView.Columns.Add("Id");
         _itemsListView.Columns.Add("Name");
         _itemsListView.Columns.Add("Tier");
         _itemsListView.Columns.Add("Credits");
         _itemsListView.Columns[0].Width = 0;
         _itemsListView.Columns[1].Width += 100;
         _itemPanel.Controls.Add(_itemsListView);
         
         //Reward panel
         _rewardPanel = new Panel();
         _rewardPanel.Visible = false;
         _rewardPanel.Dock = DockStyle.Fill;
         //Reward Type Label
         _rewardTypeLabel = new Label();
         _rewardTypeLabel.Text = "Reward Type";
         _rewardTypeLabel.Size = new System.Drawing.Size(250, 20);
         _rewardTypeLabel.Location = new System.Drawing.Point(5, 90);
         _rewardPanel.Controls.Add(_rewardTypeLabel);
         //Reward Type Combo
         _rewardTypeCombo = new ComboBox();
         _rewardTypeCombo.Size = new System.Drawing.Size(200, 30);
         _rewardTypeCombo.Location = new System.Drawing.Point(5, 110);
         _rewardTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _rewardPanel.Controls.Add(_rewardTypeCombo);
         //Rewards list view
         _rewardsListView = new ListView();
         _rewardsListView.Size = new System.Drawing.Size(470, 250);
         _rewardsListView.Location = new System.Drawing.Point(5, 150);
         _rewardsListView.FullRowSelect = true;
         _rewardsListView.MultiSelect = false;
         _rewardsListView.View = View.Details;
         _rewardsListView.Columns.Add("Id");
         _rewardsListView.Columns.Add("Name");
         _rewardsListView.Columns.Add("Type");
         _rewardsListView.Columns[0].Width = 0;
         _rewardsListView.Columns[1].Width += 200;
         _rewardsListView.Columns[1].Width += 20;
         _rewardPanel.Controls.Add(_rewardsListView);
         
         //Side mission panel
         _sideMissionPanel = new Panel();
         _sideMissionPanel.Visible = false;
         _sideMissionPanel.Dock = DockStyle.Fill;
         //Side mission colour label
         _sideMissionColourLabel = new Label();
         _sideMissionColourLabel.Text = "Mission Type";
         _sideMissionColourLabel.Size = new System.Drawing.Size(250, 20);
         _sideMissionColourLabel.Location = new System.Drawing.Point(5, 90);
         _sideMissionPanel.Controls.Add(_sideMissionColourLabel);
         //Side mission colour combo box
         _sideMissionTypeCombo = new ComboBox();
         _sideMissionTypeCombo.Size = new System.Drawing.Size(200, 30);
         _sideMissionTypeCombo.Location = new System.Drawing.Point(5, 110);
         _sideMissionTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _sideMissionPanel.Controls.Add(_sideMissionTypeCombo);
         //Side missions list view
         _sideMissionsListView = new ListView();
         _sideMissionsListView.Size = new System.Drawing.Size(470, 250);
         _sideMissionsListView.Location = new System.Drawing.Point(5, 150);
         _sideMissionsListView.FullRowSelect = true;
         _sideMissionsListView.MultiSelect = false;
         _sideMissionsListView.View = View.Details;
         _sideMissionsListView.Columns.Add("Id");
         _sideMissionsListView.Columns.Add("Name");
         _sideMissionsListView.Columns.Add("Type");
         _sideMissionsListView.Columns[0].Width = 0;
         _sideMissionsListView.Columns[1].Width += 100;
         _sideMissionsListView.Columns[2].Width += 100;
         _sideMissionPanel.Controls.Add(_sideMissionsListView);

         //Threat mission panel
         _threatMissionPanel = new Panel();
         _threatMissionPanel.Visible = false;
         _threatMissionPanel.Dock = DockStyle.Fill;
         //Threat mission bane label
         _threatMissionBaneLabel = new Label();
         _threatMissionBaneLabel.Text = "Bane Reward";
         _threatMissionBaneLabel.Size = new System.Drawing.Size(250, 20);
         _threatMissionBaneLabel.Location = new System.Drawing.Point(5, 90);
         _threatMissionPanel.Controls.Add(_threatMissionBaneLabel);
         //Threat mission bane reward combo
         _threatMissionBaneCombo = new ComboBox();
         _threatMissionBaneCombo.Size = new System.Drawing.Size(200, 30);
         _threatMissionBaneCombo.Location = new System.Drawing.Point(5, 110);
         _threatMissionBaneCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _threatMissionPanel.Controls.Add(_threatMissionBaneCombo);
         //Threat missions list view
         _threatMissionListView = new ListView();
         _threatMissionListView.Size = new System.Drawing.Size(470, 250);
         _threatMissionListView.Location = new System.Drawing.Point(5, 150);
         _threatMissionListView.FullRowSelect = true;
         _threatMissionListView.MultiSelect = false;
         _threatMissionListView.View = View.Details;
         _threatMissionListView.Columns.Add("Id");
         _threatMissionListView.Columns.Add("Name");
         _threatMissionListView.Columns.Add("Bane");
         _threatMissionListView.Columns[0].Width = 0;
         _threatMissionListView.Columns[1].Width += 100;
         _threatMissionListView.Columns[2].Width += 100;
         _threatMissionPanel.Controls.Add(_threatMissionListView);

         //story mission panel
         _storyMissionPanel = new Panel();
         _storyMissionPanel.Visible = false;
         _storyMissionPanel.Dock = DockStyle.Fill;
         //campaign label
         _storyMissionCampaignLabel = new Label();
         _storyMissionCampaignLabel.Text = "Campaign";
         _storyMissionCampaignLabel.Size = new System.Drawing.Size(250, 20);
         _storyMissionCampaignLabel.Location = new System.Drawing.Point(5, 90);
         _storyMissionPanel.Controls.Add(_storyMissionCampaignLabel);
         //Campaign combo
         _storyMissionCampaignCombo = new ComboBox();
         _storyMissionCampaignCombo.Size = new System.Drawing.Size(200, 30);
         _storyMissionCampaignCombo.Location = new System.Drawing.Point(5, 110);
         _storyMissionCampaignCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _storyMissionPanel.Controls.Add(_storyMissionCampaignCombo);
         //Story missions list view
         _storyMissionsListView = new ListView();
         _storyMissionsListView.Size = new System.Drawing.Size(470, 250);
         _storyMissionsListView.Location = new System.Drawing.Point(5, 150);
         _storyMissionsListView.FullRowSelect = true;
         _storyMissionsListView.MultiSelect = false;
         _storyMissionsListView.View = View.Details;
         _storyMissionsListView.Columns.Add("Id");
         _storyMissionsListView.Columns.Add("Name");
         _storyMissionsListView.Columns.Add("Campaign");
         _storyMissionsListView.Columns[0].Width = 0;
         _storyMissionsListView.Columns[1].Width += 100;
         _storyMissionsListView.Columns[2].Width += 100;
         _storyMissionPanel.Controls.Add(_storyMissionsListView);
         
         this.Controls.Add(_mainControlsPanel);
         this.Controls.AddRange(new Control[] {_agendaPanel, _campaignPanel, _characterPanel, _classPanel, 
                                _itemPanel, _rewardPanel, _sideMissionPanel, _storyMissionPanel, _threatMissionPanel});
         
      }
   }
}

