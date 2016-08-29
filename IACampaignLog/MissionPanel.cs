using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class MissionPanel : Panel
   {
      public delegate void MissionChangedEventHandler(MissionPanel sender, EventArgs e);
      
      private bool _isReadOnly, _hasChanges;
      private Mission _mission;
      private IList<string> _missionNames;
      public event MissionChangedEventHandler MissionChanged;
      
      public MissionPanel (IList<string> missionNames)
      {
         _mission = null;
         _hasChanges = false;
         this.Initialise();
         
         _missionNames = new List<string>(missionNames);
         _missionNameCombo.DataSource = _missionNames;
         
         _missionNameCombo.SelectedValueChanged += Handle_missionNameComboSelectedValueChanged;
         _completedCheckBox.CheckedChanged += Handle_completedCheckBoxCheckedChanged;
         _rebelUpgradeCheckBox.CheckedChanged += Handle_rebelUpgradeCheckBoxCheckedChanged;
         _impUpgradeCheckBox.CheckedChanged += Handle_impUpgradeCheckBoxCheckedChanged;
         _rebelXPText.TextChanged += Handle_TextChanged;
         _creditsText.TextChanged += Handle_TextChanged;
         _impXPText.TextChanged += Handle_TextChanged;
         _influenceText.TextChanged += Handle_TextChanged;
      }

      void Handle_missionNameComboSelectedValueChanged (object sender, EventArgs e)
      {
         _hasChanges = true;
         if (MissionChanged != null)
            MissionChanged(this, EventArgs.Empty);
      }
      
      void Handle_TextChanged(object sender, EventArgs e)
      {
         _hasChanges = true;
         if (MissionChanged != null)
            MissionChanged(this, EventArgs.Empty);
      }

      void Handle_impUpgradeCheckBoxCheckedChanged (object sender, EventArgs e)
      {
         MissionCompletedUpgradeChanged();
         _hasChanges = true;
         if (MissionChanged != null)
            MissionChanged(this, EventArgs.Empty);
      }

      void Handle_rebelUpgradeCheckBoxCheckedChanged (object sender, EventArgs e)
      {
         MissionCompletedUpgradeChanged();
         _hasChanges = true;
         if (MissionChanged != null)
            MissionChanged(this, EventArgs.Empty);
      }

      void Handle_completedCheckBoxCheckedChanged (object sender, EventArgs e)
      {
         MissionCompletedUpgradeChanged();
         _hasChanges = true;
         if (MissionChanged != null)
            MissionChanged(this, EventArgs.Empty);
      }
      
      private void MissionCompletedUpgradeChanged()
      {
         _missionNameCombo.Enabled = !_completedCheckBox.Checked;
         _rebelXPText.Enabled = _completedCheckBox.Checked && !_rebelUpgradeCheckBox.Checked;
         _creditsText.Enabled = _completedCheckBox.Checked && !_rebelUpgradeCheckBox.Checked;
         _impXPText.Enabled = _completedCheckBox.Checked && !_impUpgradeCheckBox.Checked;
         _influenceText.Enabled = _completedCheckBox.Checked && !_impUpgradeCheckBox.Checked;
         _rebelUpgradeCheckBox.Enabled = _completedCheckBox.Checked;
         _impUpgradeCheckBox.Enabled = _completedCheckBox.Checked;
      }
      
      public Mission CurrentMission {get{return _mission;}}
      public bool IsCompleted {get{return _completedCheckBox.Checked;}}
      public bool HasChanges {get{return _hasChanges;}}
      public bool RebelUpgradeCompleted {get{return _rebelUpgradeCheckBox.Checked;}}
      public bool ImpUpgradeCompleted {get{return _impUpgradeCheckBox.Checked;}}
      
      public int EnteredRebelXP
      {
         get
         {
            int enteredValue;
            if (int.TryParse(_rebelXPText.Text, out enteredValue))
            {
               return enteredValue;
            }
            else
               return 0;
         }
      }
      
      public int EnteredImperialXP
      {
         get
         {
            int enteredValue;
            if (int.TryParse(_impXPText.Text, out enteredValue))
            {
               return enteredValue;
            }
            else
               return 0;
         }
      }
      
      public int EnteredCredits
      {
         get
         {
            int enteredValue;
            if (int.TryParse(_creditsText.Text, out enteredValue))
            {
               return enteredValue;
            }
            else
               return 0;
         }
      }
      
      public int EnteredInfluence
      {
         get
         {
            int enteredValue;
            if (int.TryParse(_influenceText.Text, out enteredValue))
            {
               return enteredValue;
            }
            else
               return 0;
         }
      }
      
      public void LoadMission(Mission missionData)
      {
         if (missionData == null)
            throw new ArgumentNullException("missionData");
         
         _mission = missionData;
         if (!string.IsNullOrWhiteSpace(missionData.Name))
            _missionNameCombo.SelectedIndex = _missionNameCombo.Items.IndexOf(missionData.Name);
         else
            _missionNameCombo.SelectedIndex = 0;
         
         _completedCheckBox.Checked = missionData.IsCompleted;
         _missionTypeLabel.Text = missionData.Type.ToString();
         _threatLevelLabel.Text = "Threat Level: " + missionData.ThreatLevel;
         _itemTiersLabel.Text = "Item tiers: " + missionData.AvailableItemTiers;
         _rebelUpgradeCheckBox.Checked = missionData.RebelUpgradeCompleted;
         _impUpgradeCheckBox.Checked = missionData.ImperialUpgradeCompleted;
         _rebelXPText.Text = missionData.RebelXPAwarded.ToString();
         _creditsText.Text = missionData.CreditsAwarded.ToString();
         _impXPText.Text = missionData.ImperialXpAwarded.ToString();
         _influenceText.Text = missionData.InfluenceAwarded.ToString();
         switch (missionData.Type)
         {
         case Mission.MissionType.Story :
               this.BackColor = System.Drawing.Color.LightGray;
               break;
         case Mission.MissionType.Forced :
               this.BackColor = System.Drawing.Color.LightCyan;
               break;
         }
         _hasChanges = false;
      }
      
      public void ApplyChanges()
      {
         _mission.CreditsAwarded = EnteredCredits;
         _mission.ImperialUpgradeCompleted = _impUpgradeCheckBox.Checked;
         _mission.ImperialXpAwarded = EnteredImperialXP;
         _mission.InfluenceAwarded = EnteredInfluence;
         _mission.IsCompleted = _completedCheckBox.Checked;
         _mission.Name = _missionNameCombo.SelectedValue.ToString();
         _mission.RebelUpgradeCompleted = _rebelUpgradeCheckBox.Checked;
         _mission.RebelXPAwarded = EnteredRebelXP;
      }

      public bool ReadOnly {
         get
         {return _isReadOnly;}
         set
         {
            _isReadOnly = value;
            _completedCheckBox.Enabled = !value;
            _creditsText.Enabled = !value;
            _impUpgradeCheckBox.Enabled = !value;
            _impXPText.Enabled = !value;
            _influenceText.Enabled = !value;
            _missionNameCombo.Enabled = !value;
            _rebelUpgradeCheckBox.Enabled = !value;
            _rebelXPText.Enabled = !value;
         }
      }
      
   }
}

