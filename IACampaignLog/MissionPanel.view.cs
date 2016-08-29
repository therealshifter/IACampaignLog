using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class MissionPanel : Panel
   {
      private CheckBox _completedCheckBox, _rebelUpgradeCheckBox, _impUpgradeCheckBox;
      private TextBox _rebelXPText, _impXPText, _creditsText, _influenceText;
      private ComboBox _missionNameCombo;
      private Label _rebelXPLabel, _impXPLabel, _creditsLabel, _influenceLabel,
                    _missionTypeLabel, _threatLevelLabel, _itemTiersLabel;
      
      public void Initialise()
      {
         this.Size = new System.Drawing.Size(440, 75);
         this.BorderStyle = BorderStyle.FixedSingle;
         
         //Mission name combo
         _missionNameCombo = new ComboBox();
         _missionNameCombo.Size = new System.Drawing.Size(200, 30);
         _missionNameCombo.Location = new System.Drawing.Point(5, 5);
         _missionNameCombo.DropDownStyle = ComboBoxStyle.DropDownList;
         this.Controls.Add(_missionNameCombo);
         
         //Completed check box
         _completedCheckBox = new CheckBox();
         _completedCheckBox.Text = "Completed";
         _completedCheckBox.Size = new System.Drawing.Size(80, 20);
         _completedCheckBox.Location = new System.Drawing.Point(70, 32);
         this.Controls.Add(_completedCheckBox);
         
         //Mission type label
         _missionTypeLabel = new Label();
         _missionTypeLabel.Location = new System.Drawing.Point(20, 55);
         _missionTypeLabel.Size = new System.Drawing.Size(60, 20);
         this.Controls.Add(_missionTypeLabel);
         
         //Threat level type label
         _threatLevelLabel = new Label();
         _threatLevelLabel.Location = new System.Drawing.Point(90, 55);
         _threatLevelLabel.Size = new System.Drawing.Size(90, 20);
         this.Controls.Add(_threatLevelLabel);
         
         //Item tiers label
         _itemTiersLabel = new Label();
         _itemTiersLabel.Location = new System.Drawing.Point(210, 55);
         _itemTiersLabel.Size = new System.Drawing.Size(90, 20);
         this.Controls.Add(_itemTiersLabel);
         
         //Rebel XP label
         _rebelXPLabel = new Label();
         _rebelXPLabel.Text = "XP";
         _rebelXPLabel.Location = new System.Drawing.Point(210, 5);
         _rebelXPLabel.Size = new System.Drawing.Size(20, 20);
         this.Controls.Add(_rebelXPLabel);
         
         //Rebel XP Text box
         _rebelXPText = new TextBox();
         _rebelXPText.Size = new System.Drawing.Size(30, 30);
         _rebelXPText.Location = new System.Drawing.Point(230, 5);
         _rebelXPText.Enabled = false;
         this.Controls.Add(_rebelXPText);
         
         //Credits label
         _creditsLabel = new Label();
         _creditsLabel.Text = "$";
         _creditsLabel.Location = new System.Drawing.Point(260, 5);
         _creditsLabel.Size = new System.Drawing.Size(20, 20);
         this.Controls.Add(_creditsLabel);
         
         //Rebel Credits text box
         _creditsText = new TextBox();
         _creditsText.Size = new System.Drawing.Size(30, 30);
         _creditsText.Location = new System.Drawing.Point(280, 5);
         _creditsText.Enabled = false;
         this.Controls.Add(_creditsText);
         
         //Imp xp label
         _impXPLabel = new Label();
         _impXPLabel.Text = "XP";
         _impXPLabel.Location = new System.Drawing.Point(320, 5);
         _impXPLabel.Size = new System.Drawing.Size(20, 20);
         this.Controls.Add(_impXPLabel);
         
         //Imperial XP text box
         _impXPText = new TextBox();
         _impXPText.Size = new System.Drawing.Size(30, 30);
         _impXPText.Location = new System.Drawing.Point(340, 5);
         _impXPText.Enabled = false;
         this.Controls.Add(_impXPText);
         
         //Influence label
         _influenceLabel = new Label();
         _influenceLabel.Text = "Inf";
         _influenceLabel.Location = new System.Drawing.Point(370, 5);
         _influenceLabel.Size = new System.Drawing.Size(20, 20);
         this.Controls.Add(_influenceLabel);
         
         //Influence text box
         _influenceText = new TextBox();
         _influenceText.Size = new System.Drawing.Size(30, 30);
         _influenceText.Location = new System.Drawing.Point(390, 5);
         _influenceText.Enabled = false;
         this.Controls.Add(_influenceText);
         
         //Rebel Upgrade Check box
         _rebelUpgradeCheckBox = new CheckBox();
         _rebelUpgradeCheckBox.Text = "Rebel Upgrade";
         _rebelUpgradeCheckBox.Size = new System.Drawing.Size(100, 20);
         _rebelUpgradeCheckBox.Location = new System.Drawing.Point(220, 32);
         _rebelUpgradeCheckBox.Enabled = false;
         this.Controls.Add(_rebelUpgradeCheckBox);
         
         //Imp upgrade check box
         _impUpgradeCheckBox = new CheckBox();
         _impUpgradeCheckBox.Text = "Empire Upgrade";
         _impUpgradeCheckBox.Size = new System.Drawing.Size(110, 20);
         _impUpgradeCheckBox.Location = new System.Drawing.Point(330, 32);
         _impUpgradeCheckBox.Enabled = false;
         this.Controls.Add(_impUpgradeCheckBox);
      }
   }
}

