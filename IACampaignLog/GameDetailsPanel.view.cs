using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class GameDetailsPanel : Panel
   {
      private ComboBox _campaignCombobox, _imperialClassCombo, _player1CharacterCombo,
         _player2CharacterCombo, _player3CharacterCombo, _player4CharacterCombo;
      private Label _campaignLabel, _impPlayerLabel, _playerLabel, _agendasLabel,
         _sideMissionsGreenLabel, _sideMissionsGreyLabel;
      private TextBox _impPlayerText, _player1Text, _player2Text, _player3Text,
         _player4Text;
      private ListBox _sideMissionsGreenListBox, _sideMissionsGreyListBox, _agendasListBox;
      
      public void Initialise()
      {
         this.Size = new System.Drawing.Size(645, 370);
         
         //Campaign label
         _campaignLabel = new Label();
         _campaignLabel.Text = "Campaign";
         _campaignLabel.Size = new System.Drawing.Size(250, 20);
         _campaignLabel.Location = new System.Drawing.Point(5, 5);
         this.Controls.Add(_campaignLabel);
         //Campaign combo
         _campaignCombobox = new ComboBox();
         _campaignCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
         _campaignCombobox.Size = new System.Drawing.Size(250, 30);
         _campaignCombobox.Location = new System.Drawing.Point(5, 30);
         this.Controls.Add(_campaignCombobox);
         
         //Imperial player label
         _impPlayerLabel = new Label();
         _impPlayerLabel.Text = "Imperial Player";
         _impPlayerLabel.Size = new System.Drawing.Size(200, 20);
         _impPlayerLabel.Location = new System.Drawing.Point(5, 65);
         this.Controls.Add(_impPlayerLabel);
         //Imp player textbox
         _impPlayerText = new TextBox();
         _impPlayerText.Size = new System.Drawing.Size(200, 30);
         _impPlayerText.Location = new System.Drawing.Point(5, 85);
         this.Controls.Add(_impPlayerText);
         //Imp player class combo
         _imperialClassCombo = new ComboBox();
         _imperialClassCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _imperialClassCombo.Size = new System.Drawing.Size(200, 30);
         _imperialClassCombo.Location = new System.Drawing.Point(210, 85);
         this.Controls.Add(_imperialClassCombo);
         
         //Player Label
         _playerLabel = new Label();
         _playerLabel.Text = "Hero Players";
         _playerLabel.Size = new System.Drawing.Size(200, 20);
         _playerLabel.Location = new System.Drawing.Point(5, 120);
         this.Controls.Add(_playerLabel);
         //Player 1 Text
         _player1Text = new TextBox();
         _player1Text.Size = new System.Drawing.Size(200, 30);
         _player1Text.Location = new System.Drawing.Point(5, 140);
         this.Controls.Add(_player1Text);
         //Player 1 Character combo
         _player1CharacterCombo = new ComboBox();
         _player1CharacterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _player1CharacterCombo.Size = new System.Drawing.Size(200, 30);
         _player1CharacterCombo.Location = new System.Drawing.Point(210, 140);
         this.Controls.Add(_player1CharacterCombo);
         //Player 2 Text
         _player2Text = new TextBox();
         _player2Text.Size = new System.Drawing.Size(200, 30);
         _player2Text.Location = new System.Drawing.Point(5, 175);
         this.Controls.Add(_player2Text);
         //Player 2 Character combo
         _player2CharacterCombo = new ComboBox();
         _player2CharacterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _player2CharacterCombo.Size = new System.Drawing.Size(200, 30);
         _player2CharacterCombo.Location = new System.Drawing.Point(210, 175);
         this.Controls.Add(_player2CharacterCombo);
         //Player 3 Text
         _player3Text = new TextBox();
         _player3Text.Size = new System.Drawing.Size(200, 30);
         _player3Text.Location = new System.Drawing.Point(5, 210);
         this.Controls.Add(_player3Text);
         //Player 3 Character combo
         _player3CharacterCombo = new ComboBox();
         _player3CharacterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _player3CharacterCombo.Size = new System.Drawing.Size(200, 30);
         _player3CharacterCombo.Location = new System.Drawing.Point(210, 210);
         this.Controls.Add(_player3CharacterCombo);
         //Player 4 Text
         _player4Text = new TextBox();
         _player4Text.Size = new System.Drawing.Size(200, 30);
         _player4Text.Location = new System.Drawing.Point(5, 245);
         this.Controls.Add(_player4Text);
         //Player 4 Character combo
         _player4CharacterCombo = new ComboBox();
         _player4CharacterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         _player4CharacterCombo.Size = new System.Drawing.Size(200, 30);
         _player4CharacterCombo.Location = new System.Drawing.Point(210, 245);
         this.Controls.Add(_player4CharacterCombo);
         
         //Agendas Label
         _agendasLabel = new Label();
         _agendasLabel.Text = "Agenda Sets";
         _agendasLabel.Size = new System.Drawing.Size(200, 20);
         _agendasLabel.Location = new System.Drawing.Point(420, 15);
         this.Controls.Add(_agendasLabel);
         //Agendas list box
         _agendasListBox = new ListBox();
         _agendasListBox.Size = new System.Drawing.Size(200, 90);
         _agendasListBox.Location = new System.Drawing.Point(420, 35);
         _agendasListBox.SelectionMode = SelectionMode.MultiSimple;
         this.Controls.Add(_agendasListBox);
         
         //Side missions green label
         _sideMissionsGreenLabel = new Label();
         _sideMissionsGreenLabel.Text = "Green Side Missions";
         _sideMissionsGreenLabel.Size = new System.Drawing.Size(200, 20);
         _sideMissionsGreenLabel.Location = new System.Drawing.Point(420, 135);
         this.Controls.Add(_sideMissionsGreenLabel);
         //Side missions green list box
         _sideMissionsGreenListBox = new ListBox();
         _sideMissionsGreenListBox.Size = new System.Drawing.Size(200, 75);
         _sideMissionsGreenListBox.Location = new System.Drawing.Point(420, 155);
         _sideMissionsGreenListBox.SelectionMode = SelectionMode.MultiSimple;
         this.Controls.Add(_sideMissionsGreenListBox);
         
         //Side missions grey label
         _sideMissionsGreyLabel = new Label();
         _sideMissionsGreyLabel.Text = "Grey Side Missions (Secret)";
         _sideMissionsGreyLabel.Size = new System.Drawing.Size(200, 20);
         _sideMissionsGreyLabel.Location = new System.Drawing.Point(420, 235);
         this.Controls.Add(_sideMissionsGreyLabel);
         //Side missions green list box
         _sideMissionsGreyListBox = new ListBox();
         _sideMissionsGreyListBox.Size = new System.Drawing.Size(200, 75);
         _sideMissionsGreyListBox.Location = new System.Drawing.Point(420, 255);
         _sideMissionsGreyListBox.SelectionMode = SelectionMode.MultiSimple;
         this.Controls.Add(_sideMissionsGreyListBox);
      }
   }
}

