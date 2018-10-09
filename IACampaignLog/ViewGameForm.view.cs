using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class ViewGameForm : EditFormBase
   {
      private Button _showGameDetailsButton, _applyMissionChangesButton,
         _cancelMissionChangesButton, _addForcedMissionButton;
      private Panel _missionListPanel, _topControlsPanel, _playerListPanel;
      private Label _heroCreditsLabel, _availableSidesLabel;
      private ComboBox _availableSide1Combo, _availableSide2Combo;
      
      private void Initialise()
      {
         this.Size = new System.Drawing.Size(1210, 660);
         
         //Top controls panel
         _topControlsPanel = new Panel();
         _topControlsPanel.Dock = DockStyle.Top;
         _topControlsPanel.Height = 40;
         this.Controls.Add(_topControlsPanel);
         //Show Details button
         _showGameDetailsButton = new Button();
         _showGameDetailsButton.Text = "Show Details";
         _showGameDetailsButton.Size = new System.Drawing.Size(80, 30);
         _showGameDetailsButton.Location = new System.Drawing.Point(5, 5);
         _topControlsPanel.Controls.Add(_showGameDetailsButton);
         //Add Forced mission button
         _addForcedMissionButton = new Button();
         _addForcedMissionButton.Text = "+ Forced";
         _addForcedMissionButton.Enabled = false;
         _addForcedMissionButton.Size = new System.Drawing.Size(80, 30);
         _addForcedMissionButton.Location = new System.Drawing.Point(100, 5);
         _topControlsPanel.Controls.Add(_addForcedMissionButton);
         //Apply changes button
         _applyMissionChangesButton = new Button();
         _applyMissionChangesButton.Text = "Apply Changes";
         _applyMissionChangesButton.Enabled = false;
         _applyMissionChangesButton.Size = new System.Drawing.Size(100, 30);
         _applyMissionChangesButton.Location = new System.Drawing.Point(265, 5);
         _topControlsPanel.Controls.Add(_applyMissionChangesButton);
         //Cancel changes button
         _cancelMissionChangesButton = new Button();
         _cancelMissionChangesButton.Text = "Cancel Changes";
         _cancelMissionChangesButton.Enabled = false;
         _cancelMissionChangesButton.Size = new System.Drawing.Size(100, 30);
         _cancelMissionChangesButton.Location = new System.Drawing.Point(370, 5);
         _topControlsPanel.Controls.Add(_cancelMissionChangesButton);
         //hero credits label
         _heroCreditsLabel = new Label();
         _heroCreditsLabel.Size = new System.Drawing.Size(80, 25);
         _heroCreditsLabel.Location = new System.Drawing.Point(490, 8);
         _heroCreditsLabel.BackColor = System.Drawing.Color.PaleGoldenrod;
         _topControlsPanel.Controls.Add(_heroCreditsLabel);
         //available sides label
         _availableSidesLabel = new Label();
         _availableSidesLabel.Size = new System.Drawing.Size(125, 25);
         _availableSidesLabel.Location = new System.Drawing.Point(580, 10);
         _availableSidesLabel.Text = "Available Side Missions";
         _topControlsPanel.Controls.Add(_availableSidesLabel);
         //Available side mission 1 combo
         _availableSide1Combo = new ComboBox();
         _availableSide1Combo.Size = new System.Drawing.Size(200, 30);
         _availableSide1Combo.Location = new System.Drawing.Point(710, 10);
         _availableSide1Combo.DropDownStyle = ComboBoxStyle.DropDownList;
         _topControlsPanel.Controls.Add(_availableSide1Combo);
         //Available side mission 2 combo
         _availableSide2Combo = new ComboBox();
         _availableSide2Combo.Size = new System.Drawing.Size(200, 30);
         _availableSide2Combo.Location = new System.Drawing.Point(920, 10);
         _availableSide2Combo.DropDownStyle = ComboBoxStyle.DropDownList;
         _topControlsPanel.Controls.Add(_availableSide2Combo);
         
         //Mission list panel
         _missionListPanel = new Panel();
         _missionListPanel.Location = new System.Drawing.Point(0, 40);
         _missionListPanel.Size = new System.Drawing.Size(470, 530);
         _missionListPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
         _missionListPanel.AutoScroll = true;
         this.Controls.Add(_missionListPanel);
         
         //Player list panel
         _playerListPanel = new Panel();
         _playerListPanel.Location = new System.Drawing.Point(470, 40);
         _playerListPanel.Size = new System.Drawing.Size(725, 530);
         _playerListPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
         _playerListPanel.AutoScroll = true;
         this.Controls.Add(_playerListPanel);
      }
   }
}

