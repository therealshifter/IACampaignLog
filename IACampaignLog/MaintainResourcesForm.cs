using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class MaintainResourcesForm : EditFormBase
   {
      private IList<KeyValuePair<Maintain, string>> _maintainItemsList;
      private IList<KeyValuePair<Item.ItemTier, string>> _itemTierList;
      private IList<CardSet<Agenda>> _agendaSetList;
      private IList<CardSet<ClassCard>> _classSetList;
      private IList<Character> _classCharacterList;
      private IList<Campaign> _campaignList;
      private IList<KeyValuePair<Agenda.AgendaType, string>> _agendaTypeList;
      private IList<KeyValuePair<SideMission.MissionType, string>> _sideMissionTypeList;
      private IList<SideMission> _characterSideMissionsList;
      private IList<StoryMission> _campaignMissionNamesList;
      private IList<Mission.MissionType> _campaignMissionTypesList;
      private IList<MissionHeader> _selectedCampaignMissions;
      private IList<Item.ItemTier> _campaignMissionItemTierList;
      private StoryMission _emptyCampaignMissionName;
      private int _selectedListItemId;
      private int _selectedCampaignMissionIndex;
      
      private enum Maintain
      {
         Agenda,
         Campaign,
         Character,
         Class,
         Item,
         Reward,
         SideMission,
         StoryMission
      }
      
      public MaintainResourcesForm ()
      {
         this.Initialise();
         _emptyCampaignMissionName = new StoryMission(-1, string.Empty, null);
         
         //Maintain combo data setup
         _maintainItemsList = new List<KeyValuePair<Maintain, string>>();
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Agenda, "Agenda"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Campaign, "Campaign"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Character, "Character"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Class, "Class"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Item, "Item"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.Reward, "Reward"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.SideMission, "Side Mission"));
         _maintainItemsList.Add(new KeyValuePair<Maintain, string>(Maintain.StoryMission, "Story Mission"));
         _maintainCombo.SelectedIndexChanged += Handle_maintainComboSelectedIndexChanged;
         _maintainCombo.DataSource = _maintainItemsList;
         _maintainCombo.DisplayMember = "Value";
         _maintainCombo.ValueMember = "Key";
         _maintainCombo.SelectedIndex = -1;
         _maintainCombo.SelectedIndex = 0;
         
         //Item tier combo data setup
         _itemTierList = new List<KeyValuePair<Item.ItemTier, string>>();
         _itemTierList.Add(new KeyValuePair<Item.ItemTier, string>(Item.ItemTier.I, "Tier 1"));
         _itemTierList.Add(new KeyValuePair<Item.ItemTier, string>(Item.ItemTier.II, "Tier 2"));
         _itemTierList.Add(new KeyValuePair<Item.ItemTier, string>(Item.ItemTier.III, "Tier 3"));
         _itemTierComboBox.DataSource = _itemTierList;
         _itemTierComboBox.DisplayMember = "Value";
         _itemTierComboBox.ValueMember = "Key";
         _itemTierComboBox.SelectedIndex = 0;
         
         //Agenda type selected data
         _agendaTypeList = new List<KeyValuePair<Agenda.AgendaType, string>>();
         _agendaTypeList.Add(new KeyValuePair<Agenda.AgendaType, string>(Agenda.AgendaType.SideMission, "Side Mission"));
         _agendaTypeList.Add(new KeyValuePair<Agenda.AgendaType, string>(Agenda.AgendaType.ForcedMission, "Forced Mission"));
         _agendaTypeList.Add(new KeyValuePair<Agenda.AgendaType, string>(Agenda.AgendaType.Ongoing, "Ongoing"));
         _agendaTypeList.Add(new KeyValuePair<Agenda.AgendaType, string>(Agenda.AgendaType.Secret, "Secret"));
         _agendaTypeCombo.DataSource = _agendaTypeList;
         _agendaTypeCombo.DisplayMember = "Value";
         _agendaTypeCombo.ValueMember = "Key";
         
         //Side mission colour selected data
         _sideMissionTypeList = new List<KeyValuePair<SideMission.MissionType, string>>();
         _sideMissionTypeList.Add(new KeyValuePair<SideMission.MissionType, string>(SideMission.MissionType.Red, "Red (Hero)"));
         _sideMissionTypeList.Add(new KeyValuePair<SideMission.MissionType, string>(SideMission.MissionType.Green, "Green (Ally)"));
         _sideMissionTypeList.Add(new KeyValuePair<SideMission.MissionType, string>(SideMission.MissionType.Grey, "Grey (Card)"));
         _sideMissionTypeList.Add(new KeyValuePair<SideMission.MissionType, string>(SideMission.MissionType.Forced, "Forced"));
         _sideMissionTypeCombo.DataSource = _sideMissionTypeList;
         _sideMissionTypeCombo.DisplayMember = "Value";
         _sideMissionTypeCombo.ValueMember = "Key";
         
         //Campaign mission types selected data
         _campaignMissionTypesList = new List<Mission.MissionType>();
         _campaignMissionTypesList.Add(Mission.MissionType.Story);
         _campaignMissionTypesList.Add(Mission.MissionType.Side);
         _campaignMissionTypeCombo.DataSource = _campaignMissionTypesList;
         
         //Campaign mission item tiers combo
         _campaignMissionItemTierList = new List<Item.ItemTier>();
         _campaignMissionItemTierList.Add(Item.ItemTier.I);
         _campaignMissionItemTierList.Add(Item.ItemTier.I | Item.ItemTier.II);
         _campaignMissionItemTierList.Add(Item.ItemTier.II);
         _campaignMissionItemTierList.Add(Item.ItemTier.II | Item.ItemTier.III);
         _campaignMissionItemTierList.Add(Item.ItemTier.III);
         _campaignMissionItemTiersCombo.DataSource = _campaignMissionItemTierList;
         
         _selectedCampaignMissions = new List<MissionHeader>();
         
         //Update set combo boxes by refreshing the panels
         RefreshAgendaPanel(true);
         RefreshCampaignPanel(true);
         RefreshCharacterPanel(true);
         RefreshClassPanel(true);
         RefreshItemPanel(true);
         RefreshRewardPanel(true);
         RefreshSideMissionPanel(true);
         RefreshStoryMissionPanel(true);
         SelectedListItemIdChanged(-1);
         
         _addButton.Click += Handle_addButtonClick;
         _saveButton.Click += Handle_saveButtonClick;
         _cancelButton.Click += Handle_cancelButtonClick;
         _agendasListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _campaignListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _campaignMissionListView.ItemSelectionChanged += Handle_campaignMissionsItemSelectiongChanged;
         _charactersListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _classesListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _itemsListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _rewardsListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _sideMissionsListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _storyMissionsListView.ItemSelectionChanged += Handle_ListViewItemSelectionChanged;
         _campaignAddMissionButton.Click += Handle_campaignAddMissionButtonClick;
         _campaignSaveMissionButton.Click += Handle_campaignSaveMissionButtonClick;
         _campaignCancelMissionButton.Click += Handle_campaignCancelMissionButtonClick;
      }

      void Handle_campaignCancelMissionButtonClick (object sender, EventArgs e)
      {
         RefreshCampaignMissions(false);
      }

      void Handle_campaignSaveMissionButtonClick (object sender, EventArgs e)
      {
         if (_campaignMissionListView.SelectedItems.Count > 0)
         {
            MissionHeader selected = (MissionHeader)_campaignMissionListView.SelectedItems[0].Tag;
            selected.Name = _campaignMissionNameCombo.SelectedValue.ToString();
            selected.ThreatLevel = (int)_campaignMissionThreatLevelText.Value;
            selected.Type = (Mission.MissionType)_campaignMissionTypeCombo.SelectedItem;
            selected.AvailableItemTiers = (Item.ItemTier)_campaignMissionItemTiersCombo.SelectedItem;
            RefreshCampaignMissions(true);
         }
      }

      void Handle_campaignAddMissionButtonClick (object sender, EventArgs e)
      {
         MissionHeader newMission = new MissionHeader((Mission.MissionType)_campaignMissionTypeCombo.SelectedItem, (int)_campaignMissionThreatLevelText.Value);
         newMission.Name = _campaignMissionNameCombo.SelectedValue.ToString();
         newMission.AvailableItemTiers = (Item.ItemTier)_campaignMissionItemTiersCombo.SelectedItem;
         _selectedCampaignMissions.Add(newMission);
         RefreshCampaignMissions(true);
      }
      
      void Handle_campaignMissionsItemSelectiongChanged(object sender, ListViewItemSelectionChangedEventArgs e)
      {
         if (sender is ListView)
         {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
               ListViewItem lvi = (sender as ListView).SelectedItems[0];
               SelectedCampaignMissionIndexChanged(lvi.Index);
               MissionHeader selectedMission = (MissionHeader)lvi.Tag;
               _campaignMissionNameCombo.SelectedValue = selectedMission.Name;
               _campaignMissionThreatLevelText.Value = selectedMission.ThreatLevel;
               _campaignMissionTypeCombo.SelectedItem = selectedMission.Type;
               _campaignMissionItemTiersCombo.SelectedItem = selectedMission.AvailableItemTiers;
            }
         }
      }

      void Handle_ListViewItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
      {
         if (sender is ListView)
         {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
               ListViewItem lvi = (sender as ListView).SelectedItems[0];
               SelectedListItemIdChanged(int.Parse(lvi.Text));
               
               if (sender == _agendasListView)
               {
                  Agenda selectedAgenda = AgendaController.GetInstance().FindAgendaWithId(_selectedListItemId);
                  _nameText.Text = selectedAgenda.Name;
                  _agendaSetCombo.SelectedItem = (CardSet<Agenda>)lvi.Tag;
                  _agendaTypeCombo.SelectedValue = selectedAgenda.AgendaCardType;
                  _agendaInfluenceText.Text = selectedAgenda.InfluenceCost.ToString();
                  _agendaSetCombo.Enabled = false;
               }
               else if (sender == _campaignListView)
               {
                  Campaign selectedCampaign = CampaignController.GetInstance().FindWithId(_selectedListItemId);
                  _nameText.Text = selectedCampaign.Name;
                  _campaignAllowForcedCheckBox.Checked = selectedCampaign.AllowForcedMissions;
                  _campaignAllowSidesCheckBox.Checked = selectedCampaign.AllowSideMissions;
                  _campaignStartingCreditsText.Value = selectedCampaign.StartingCredits;
                  _campaignStartingImpXpText.Value = selectedCampaign.StartingImperialXP;
                  _campaignStartingInfluenceText.Value = selectedCampaign.StartingInfluence;
                  _campaignStartingRebelXpText.Value = selectedCampaign.StartingRebelXP;
                  _selectedCampaignMissions = selectedCampaign.CloneMissionTemplate();
                  RefreshCampaignMissions(true);
               }
               else if (sender == _charactersListView)
               {
                  Character selectedChar = (Character)lvi.Tag;
                  _nameText.Text = selectedChar.Name;
                  _characterSideMissionCombo.SelectedItem = selectedChar.RelatedSideMission;
               }
               else if (sender == _classesListView)
               {
                  ClassCard selectedClass = ClassController.GetInstance().FindClassCardWithId(_selectedListItemId);
                  _nameText.Text = selectedClass.Name;
                  _classSetCombo.SelectedItem = (CardSet<ClassCard>)lvi.Tag;
                  _classXpText.Text = selectedClass.XpCost.ToString();
                  _classCharacterCombo.SelectedItem = ((CardSet<ClassCard>)lvi.Tag).AssociatedCharacter;
                  _classSetCombo.Enabled = false;
                  _classCharacterCombo.Enabled = false;
               }
               else if (sender == _itemsListView)
               {
                  Item selectedItem = (Item)lvi.Tag;
                  _nameText.Text = selectedItem.Name;
                  _itemTierComboBox.SelectedValue = selectedItem.Tier;
                  _itemCreditText.Text = selectedItem.CreditCost.ToString();
               }
               else if (sender == _rewardsListView)
               {
                  Reward selectedReward = (Reward)lvi.Tag;
                  _nameText.Text = selectedReward.Name;
               }
               else if (sender == _sideMissionsListView)
               {
                  SideMission selectedSide = (SideMission)lvi.Tag;
                  _nameText.Text = selectedSide.Name;
                  _sideMissionTypeCombo.SelectedValue = selectedSide.SideMissionType;
               }
               else if (sender == _storyMissionsListView)
               {
                  StoryMission selectedStory = (StoryMission)lvi.Tag;
                  _nameText.Text = selectedStory.Name;
                  _storyMissionCampaignCombo.SelectedItem = selectedStory.MissionCampaign;
               }
            }
         }
      }

      void Handle_addButtonClick (object sender, EventArgs e)
      {
         int prevId = _selectedListItemId;
         SelectedListItemIdChanged(-1);
         if (!AddSaveEditedResource())
            SelectedListItemIdChanged(prevId);
      }
      
      void Handle_saveButtonClick (object sender, EventArgs e)
      {
         if (AddSaveEditedResource())
            SelectedListItemIdChanged(-1);
      }

      void Handle_cancelButtonClick (object sender, EventArgs e)
      {
         CancelListItemSelected();
      }

      void Handle_maintainComboSelectedIndexChanged (object sender, EventArgs e)
      {
         if (_maintainCombo.SelectedValue != null)
         {
            _agendaPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Agenda;
            _campaignPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Campaign;
            _characterPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Character;
            _classPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Class;
            _itemPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Item;
            _rewardPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.Reward;
            _sideMissionPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.SideMission;
            _storyMissionPanel.Visible = (Maintain)_maintainCombo.SelectedValue == Maintain.StoryMission;
            _nameText.Text = string.Empty;
            SelectedListItemIdChanged(-1);
         }
      }
      
      private bool AddSaveEditedResource()
      {
         bool isValid = false;
         
         switch ((Maintain)_maintainCombo.SelectedValue)
         {
         case Maintain.Agenda :
            {
               Agenda selected = AgendaController.GetInstance().FindAgendaWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateAgendaPanel(existingName);
               if (isValid)
               {
                  CardSet<Agenda> selectedSet = (CardSet<Agenda>)_agendaSetCombo.SelectedItem;
                  if (selectedSet == null)
                  {
                     selectedSet = AgendaController.GetInstance().AddSet(_agendaSetCombo.Text, CharacterController.GetInstance().ImperialCharacter);
                  }
                  if (selected == null)
                  {
                     AgendaController.GetInstance().AddAgenda(selectedSet, _nameText.Text, int.Parse(_agendaInfluenceText.Text), (Agenda.AgendaType)_agendaTypeCombo.SelectedValue);
                     MessageBox.Show("Agenda added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.InfluenceCost = int.Parse(_agendaInfluenceText.Text);
                     selected.AgendaCardType = (Agenda.AgendaType)_agendaTypeCombo.SelectedValue;
                     AgendaController.GetInstance().HasChanges = true;
                     MessageBox.Show("Agenda saved successfully");
                  }
                  RefreshAgendaPanel(true);
               }
               break;
            }
         case Maintain.Campaign :
            {
               Campaign selected = CampaignController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateCampaignPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     selected = CampaignController.GetInstance().AddCampaign(_nameText.Text, _selectedCampaignMissions);
                     MessageBox.Show("Campaign added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.MissionTemplate = _selectedCampaignMissions;
                     CampaignController.GetInstance().HasChanges = true;
                     MessageBox.Show("Campaign saved successfully");
                  }
                  Console.WriteLine(selected.MissionTemplate.Count.ToString());
                  selected.StartingCredits = (int)_campaignStartingCreditsText.Value;
                  selected.StartingImperialXP = (int)_campaignStartingImpXpText.Value;
                  selected.StartingInfluence = (int)_campaignStartingInfluenceText.Value;
                  selected.StartingRebelXP = (int)_campaignStartingRebelXpText.Value;
                  selected.AllowSideMissions = _campaignAllowSidesCheckBox.Checked;
                  selected.AllowForcedMissions = _campaignAllowForcedCheckBox.Checked;
                  _selectedCampaignMissions = new List<MissionHeader>();
                  RefreshCampaignPanel(true);
                  RefreshStoryMissionPanel(true);
               }
               break;
            }
         case Maintain.Character :
            {
               Character selected = CharacterController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateCharacterPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     CharacterController.GetInstance().AddCharacter(_nameText.Text, (SideMission)_characterSideMissionCombo.SelectedItem);
                     MessageBox.Show("Character added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.RelatedSideMission = (SideMission)_characterSideMissionCombo.SelectedItem;
                     CharacterController.GetInstance().HasChanges = true;
                     MessageBox.Show("Character saved successfully");
                  }
                  RefreshCharacterPanel(true);
                  //Refresh class panel to include new character in list
                  RefreshClassPanel(true);
               }
               break;
            }
         case Maintain.Class :
            {
               ClassCard selected = ClassController.GetInstance().FindClassCardWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateClassPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     CardSet<ClassCard> selectedSet = (CardSet<ClassCard>)_classSetCombo.SelectedItem;
                     if (selectedSet == null)
                     {
                        selectedSet = ClassController.GetInstance().AddSet(_classSetCombo.Text, (Character)_classCharacterCombo.SelectedItem);
                     }
                     ClassController.GetInstance().AddClassCard(selectedSet, _nameText.Text, int.Parse(_classXpText.Text));
                     MessageBox.Show("Class added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.XpCost = int.Parse(_classXpText.Text);
                     ClassController.GetInstance().HasChanges = true;
                     MessageBox.Show("Class saved successfully");
                  }
                  RefreshClassPanel(true);
               }
               break;
            }
         case Maintain.Item :
            {
               Item selected = ItemController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateItemPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     ItemController.GetInstance().AddItem(_nameText.Text, int.Parse(_itemCreditText.Text), (Item.ItemTier)_itemTierComboBox.SelectedValue);
                     MessageBox.Show("Item added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.CreditCost = int.Parse(_itemCreditText.Text);
                     selected.Tier = (Item.ItemTier)_itemTierComboBox.SelectedValue;
                     ItemController.GetInstance().HasChanges = true;
                     MessageBox.Show("Item saved successfully");
                  }
                  RefreshItemPanel(true);
               }
               break;
            }
         case Maintain.Reward :
            {
               Reward selected = RewardController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateRewardPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     RewardController.GetInstance().AddReward(_nameText.Text);
                     MessageBox.Show("Reward added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     RewardController.GetInstance().HasChanges = true;
                     MessageBox.Show("Reward saved successfully");
                  }
                  RefreshRewardPanel(true);
               }
               break;
            }
         case Maintain.SideMission :
            {
               SideMission selected = SideMissionController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateSideMissionPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     SideMissionController.GetInstance().AddSideMission(_nameText.Text, (SideMission.MissionType)_sideMissionTypeCombo.SelectedValue);
                     MessageBox.Show("Side Mission added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.SideMissionType = (SideMission.MissionType)_sideMissionTypeCombo.SelectedValue;
                     SideMissionController.GetInstance().HasChanges = true;
                     MessageBox.Show("Side Mission saved successfully");
                  }
                  RefreshSideMissionPanel(true);
                  RefreshCharacterPanel(true);
               }
               break;
            }
         case Maintain.StoryMission :
            {
               StoryMission selected = StoryMissionController.GetInstance().FindWithId(_selectedListItemId);
               string existingName = selected == null ? string.Empty : selected.Name;
               isValid = ValidateStoryMissionPanel(existingName);
               if (isValid)
               {
                  if (selected == null)
                  {
                     StoryMissionController.GetInstance().AddStoryMission(_nameText.Text, (Campaign)_storyMissionCampaignCombo.SelectedItem);
                     MessageBox.Show("Story Mission added successfully");
                  }
                  else
                  {
                     selected.Name = _nameText.Text;
                     selected.MissionCampaign = (Campaign)_storyMissionCampaignCombo.SelectedItem;
                     StoryMissionController.GetInstance().HasChanges = true;
                     MessageBox.Show("Story Mission saved successfully");
                  }
                  RefreshStoryMissionPanel(true);
               }
               break;
            }
         }
         
         return isValid;
      }
      
      private void CancelListItemSelected()
      {
         RefreshAgendaPanel(false);
         RefreshCampaignPanel(false);
         RefreshCharacterPanel(false);
         RefreshClassPanel(false);
         RefreshItemPanel(false);
         RefreshRewardPanel(false);
         RefreshSideMissionPanel(false);
         RefreshStoryMissionPanel(false);
         SelectedListItemIdChanged(-1);
      }
      
      private void SelectedListItemIdChanged(int id)
      {
         _selectedListItemId = id;
         _saveButton.Enabled = id >= 0;
      }
      
      private void SelectedCampaignMissionIndexChanged(int index)
      {
         _selectedCampaignMissionIndex = index;
         _campaignSaveMissionButton.Enabled = index >= 0;
      }
      
      private void RefreshCampaignMissions(bool reloadList)
      {
         _campaignMissionThreatLevelText.Value = 0;
         _campaignMissionTypeCombo.SelectedItem = Mission.MissionType.Story;
         _campaignMissionItemTiersCombo.SelectedItem = Item.ItemTier.I;
         _campaignMissionNamesList = new List<StoryMission>(StoryMissionController.GetInstance().ListOfT);
         _campaignMissionNamesList.Insert(0, _emptyCampaignMissionName);
         _campaignMissionNameCombo.DataSource = _campaignMissionNamesList;
         _campaignMissionNameCombo.DisplayMember = "Name";
         _campaignMissionNameCombo.ValueMember = "Name";
         
         if (reloadList)
         {
            _campaignMissionListView.Items.Clear();
            int missionNum = 1;
            foreach (MissionHeader h in _selectedCampaignMissions)
            {
               ListViewItem lvmission = new ListViewItem(new string[]{
                                    missionNum.ToString(),
                                    h.Type.ToString(),
                                    h.Name,
                                    h.ThreatLevel.ToString(),
                                    h.AvailableItemTiers.ToString()});
               lvmission.Tag = h;
               _campaignMissionListView.Items.Add(lvmission);
               missionNum++;
            }
         }
         _campaignMissionListView.SelectedItems.Clear();
         SelectedCampaignMissionIndexChanged(-1);
      }
      
      private void RefreshAgendaPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _agendaInfluenceText.Text = string.Empty;
         _agendaSetList = new List<CardSet<Agenda>>(AgendaController.GetInstance().ListOfT);
         _agendaSetCombo.Enabled = true;
         _agendaSetCombo.DataSource = null;
         _agendaSetCombo.DataSource = _agendaSetList;
         _agendaSetCombo.DisplayMember = "Name";
         _agendaSetCombo.ValueMember = "Id";
         
         if (reloadList)
         {
            _agendasListView.Items.Clear();
            foreach (CardSet<Agenda> csa in AgendaController.GetInstance().ListOfT)
            {
               foreach (Agenda a in csa.ListOfT)
               {
                  ListViewItem lvi = new ListViewItem(new string[]{
                                       a.Id.ToString(),
                                       csa.Name,
                                       a.Name,
                                       a.InfluenceCost.ToString(),
                                       a.AgendaCardType.ToString()});
                  lvi.Tag = csa;
                  _agendasListView.Items.Add(lvi);
               }
            }
         }
         _agendasListView.SelectedItems.Clear();
      }
      
      private void RefreshCampaignPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _campaignAllowForcedCheckBox.Checked = true;
         _campaignAllowSidesCheckBox.Checked = true;
         _campaignStartingCreditsText.Value = 0;
         _campaignStartingImpXpText.Value = 0;
         _campaignStartingInfluenceText.Value = 0;
         _campaignStartingRebelXpText.Value = 0;
         _selectedCampaignMissions = new List<MissionHeader>();
         RefreshCampaignMissions(true);
         
         if (reloadList)
         {
            _campaignListView.Items.Clear();
            foreach (Campaign c in CampaignController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    c.Id.ToString(),
                                    c.Name,
                                    c.AllowSideMissions.ToString(),
                                    c.AllowForcedMissions.ToString(),
                                    c.StartingRebelXP.ToString(),
                                    c.StartingCredits.ToString(),
                                    c.StartingImperialXP.ToString(),
                                    c.StartingInfluence.ToString(),
                                    c.MissionTemplate.Count.ToString()});
               lvi.Tag = c;
               _campaignListView.Items.Add(lvi);
            }
         }
         _campaignListView.SelectedItems.Clear();
      }
      
      private void RefreshCharacterPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _characterSideMissionsList = new List<SideMission>(SideMissionController.GetInstance().SideMissionsOfType(SideMission.MissionType.Red));
         _characterSideMissionCombo.DataSource = null;
         _characterSideMissionCombo.DataSource = _characterSideMissionsList;
         _characterSideMissionCombo.DisplayMember = "Name";
         _characterSideMissionCombo.ValueMember = "Id";
         
         if (reloadList)
         {
            _charactersListView.Items.Clear();
            foreach (Character c in CharacterController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    c.Id.ToString(),
                                    c.Name,
                                    c.RelatedSideMission.Name});
               lvi.Tag = c;
               _charactersListView.Items.Add(lvi);
            }
         }
         _charactersListView.SelectedItems.Clear();
      }
      
      private void RefreshClassPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _classXpText.Text = string.Empty;
         _classSetList = new List<CardSet<ClassCard>>(ClassController.GetInstance().ListOfT);
         _classSetCombo.Enabled = true;
         _classCharacterCombo.Enabled = true;
         _classSetCombo.DataSource = null;
         _classSetCombo.DataSource = _classSetList;
         _classSetCombo.DisplayMember = "Name";
         _classSetCombo.ValueMember = "Id";
         _classCharacterList = new List<Character>(CharacterController.GetInstance().ListOfT);
         _classCharacterList.Insert(0, CharacterController.GetInstance().ImperialCharacter);
         _classCharacterCombo.DataSource = null;
         _classCharacterCombo.DataSource = _classCharacterList;
         _classCharacterCombo.DisplayMember = "Name";
         _classCharacterCombo.ValueMember = "Id";
         
         if (reloadList)
         {
            _classesListView.Items.Clear();
            foreach (CardSet<ClassCard> csc in ClassController.GetInstance().ListOfT)
            {
               foreach (ClassCard c in csc.ListOfT)
               {
                  ListViewItem lvi = new ListViewItem(new string[]{
                                       c.Id.ToString(),
                                       csc.Name,
                                       c.Name,
                                       c.XpCost.ToString(),
                                       csc.AssociatedCharacter.Name});
                  lvi.Tag = csc;
                  _classesListView.Items.Add(lvi);
               }
            }
         }
         _classesListView.SelectedItems.Clear();
      }
      
      private void RefreshItemPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _itemCreditText.Text = string.Empty;
         _itemTierComboBox.SelectedIndex = 0;
         
         if (reloadList)
         {
            _itemsListView.Items.Clear();
            foreach (Item i in ItemController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    i.Id.ToString(),
                                    i.Name,
                                    i.Tier.ToString(),
                                    i.CreditCost.ToString()});
               lvi.Tag = i;
               _itemsListView.Items.Add(lvi);
            }
         }
         _itemsListView.SelectedItems.Clear();
      }
      
      private void RefreshRewardPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         
         if (reloadList)
         {
            _rewardsListView.Items.Clear();
            foreach (Reward r in RewardController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    r.Id.ToString(),
                                    r.Name});
               lvi.Tag = r;
               _rewardsListView.Items.Add(lvi);
            }
         }
         _rewardsListView.SelectedItems.Clear();
      }
      
      private void RefreshSideMissionPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         
         if (reloadList)
         {
            _sideMissionsListView.Items.Clear();
            foreach (SideMission sm in SideMissionController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    sm.Id.ToString(),
                                    sm.Name,
                                    sm.SideMissionType.ToString()});
               lvi.Tag = sm;
               _sideMissionsListView.Items.Add(lvi);
            }
         }
         _sideMissionsListView.SelectedItems.Clear();
      }
      
      private void RefreshStoryMissionPanel(bool reloadList)
      {
         _nameText.Text = string.Empty;
         _campaignList = new List<Campaign>(CampaignController.GetInstance().ListOfT);
         _storyMissionCampaignCombo.DataSource = _campaignList;
         _storyMissionCampaignCombo.DisplayMember = "Name";
         _storyMissionCampaignCombo.ValueMember = "Id";
         
         
         if (reloadList)
         {
            _storyMissionsListView.Items.Clear();
            foreach (StoryMission s in StoryMissionController.GetInstance().ListOfT)
            {
               ListViewItem lvi = new ListViewItem(new string[]{
                                    s.Id.ToString(),
                                    s.Name,
                                    s.MissionCampaign.Name.ToString()});
               lvi.Tag = s;
               _storyMissionsListView.Items.Add(lvi);
            }
         }
         _storyMissionsListView.SelectedItems.Clear();
      }
      
      public override void ExecuteSave ()
      {
         if (AgendaController.GetInstance().HasChanges)
            AgendaController.GetInstance().Save();
         if (CampaignController.GetInstance().HasChanges)
            CampaignController.GetInstance().Save();
         if (CharacterController.GetInstance().HasChanges)
            CharacterController.GetInstance().Save();
         if (ClassController.GetInstance().HasChanges)
            ClassController.GetInstance().Save();
         if (ItemController.GetInstance().HasChanges)
            ItemController.GetInstance().Save();
         if (RewardController.GetInstance().HasChanges)
            RewardController.GetInstance().Save();
         if (SideMissionController.GetInstance().HasChanges)
            SideMissionController.GetInstance().Save();
         if (StoryMissionController.GetInstance().HasChanges)
            StoryMissionController.GetInstance().Save();
         base.ExecuteSave ();
      }
      
      public override void ExecuteCancel()
      {
         if (AgendaController.GetInstance().HasChanges)
            AgendaController.GetInstance().Load();
         if (CampaignController.GetInstance().HasChanges)
            CampaignController.GetInstance().Load();
         if (CharacterController.GetInstance().HasChanges)
            CharacterController.GetInstance().Load();
         if (ClassController.GetInstance().HasChanges)
            ClassController.GetInstance().Load();
         if (ItemController.GetInstance().HasChanges)
            ItemController.GetInstance().Load();
         if (RewardController.GetInstance().HasChanges)
            RewardController.GetInstance().Load();
         if (SideMissionController.GetInstance().HasChanges)
            SideMissionController.GetInstance().Load();
         if (StoryMissionController.GetInstance().HasChanges)
            StoryMissionController.GetInstance().Load();
         base.ExecuteCancel();
      }
      
      public bool ValidateAgendaPanel(string existingName)
      {
         bool isValid = false;
         if (_agendaPanel.Visible)
         {
            CardSet<Agenda> selectedSet = (CardSet<Agenda>)_agendaSetCombo.SelectedItem;
            if (selectedSet == null)
            {
               if (!string.IsNullOrWhiteSpace(_agendaSetCombo.Text))
               {
                  //New set
                  isValid = true;
               }
               else
               {
                  //invalid set name
                  isValid = false;
                  MessageBox.Show("Agenda set name must not be empty");
               }
            }
            else isValid = true;
            if (isValid)
            {
               //valid set selected: check agenda name
               if (ValidateNameTextbox())
               {
                  if (selectedSet != null)
                  {
                     //Check for existing agenda name in set
                     IList<Agenda> agendasWithName = AgendaController.GetInstance().FindAgendaInSetWithName(selectedSet.Id, _nameText.Text);
                     if (agendasWithName != null && agendasWithName.Count > 0 && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
                     {
                        //Agenda name exists
                        isValid = false;
                        MessageBox.Show("An Agenda in that set with that name exists");
                     }
                     else isValid = true;
                  }
                  if (isValid)
                  {
                     if (string.IsNullOrWhiteSpace(_agendaInfluenceText.Text))
                     {
                        isValid = false;
                        MessageBox.Show("Influence must not be empty");
                     }
                     else
                     {
                        int influence;
                        if (int.TryParse(_agendaInfluenceText.Text, out influence))
                           isValid = true;
                        else
                        {
                           isValid = false;
                           MessageBox.Show("Influence must be a whole number");
                        }
                     }
                  }
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateCampaignPanel(string existingName)
      {
         bool isValid = false;
         if (_campaignPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               bool existing = CampaignController.GetInstance().FindWithName(_nameText.Text).Count > 0;
               if (existing && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Campaign exists
                  isValid = false;
                  MessageBox.Show("Campaign with name '" + _nameText.Text + "' exists");
               }
               if (isValid)
               {
                  if (_selectedCampaignMissions.Count < 1)
                  {
                     isValid = false;
                     MessageBox.Show("Must have at least 1 mission in template");
                  }
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateCharacterPanel(string existingName)
      {
         bool isValid = false;
         if (_characterPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               bool existing = CharacterController.GetInstance().FindWithName(_nameText.Text).Count > 0;
               if (existing && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Character exists
                  isValid = false;
                  MessageBox.Show("Charater with name '" + _nameText.Text + "' exists");
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateClassPanel(string existingName)
      {
         bool isValid = false;
         if (_classPanel.Visible)
         {
            CardSet<ClassCard> selectedSet = (CardSet<ClassCard>)_classSetCombo.SelectedItem;
            if (selectedSet == null)
            {
               if (!string.IsNullOrWhiteSpace(_classSetCombo.Text))
               {
                  //New set
                  isValid = true;
               }
               else
               {
                  //invalid set name
                  isValid = false;
                  MessageBox.Show("Class set name must not be empty");
               }
            }
            else isValid = true;
            if (isValid)
            {
               //valid set selected: check class name
               if (ValidateNameTextbox())
               {
                  if (selectedSet != null)
                  {
                     //Check for existing class name in set
                     IList<ClassCard> classesWithName = ClassController.GetInstance().FindClassCardInSetWithName(selectedSet.Id, _nameText.Text);
                     if (classesWithName != null && classesWithName.Count > 0 && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
                     {
                        //Class name exists
                        isValid = false;
                        MessageBox.Show("A Class in that set with that name exists");
                     }
                     else isValid = true;
                  }
                  if (isValid)
                  {
                     if (string.IsNullOrWhiteSpace(_classXpText.Text))
                     {
                        isValid = false;
                        MessageBox.Show("XP must not be empty");
                     }
                     else
                     {
                        int xp;
                        if (int.TryParse(_classXpText.Text, out xp))
                           isValid = true;
                        else
                        {
                           isValid = false;
                           MessageBox.Show("XP must be a whole number");
                        }
                     }
                  }
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateItemPanel(string existingName)
      {
         bool isValid = false;
         if (_itemPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               Item existing = ItemController.GetInstance().FindWithNameInTier(_nameText.Text, (Item.ItemTier)_itemTierComboBox.SelectedValue);
               if (existing != null && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Item exists
                  isValid = false;
                  MessageBox.Show("Item with name '" + _nameText.Text + "' exists in Tier " + _itemTierComboBox.SelectedValue);
               }
               if (isValid)
               {
                  if (string.IsNullOrWhiteSpace(_itemCreditText.Text))
                  {
                     isValid = false;
                     MessageBox.Show("Credits must not be empty");
                  }
                  else
                  {
                     int credits;
                     if (int.TryParse(_itemCreditText.Text, out credits))
                        isValid = true;
                     else
                     {
                        isValid = false;
                        MessageBox.Show("Credits must be a whole number");
                     }
                  }
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateRewardPanel(string existingName)
      {
         bool isValid = false;
         if (_rewardPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               bool existing = RewardController.GetInstance().FindWithName(_nameText.Text).Count > 0;
               if (existing && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Reward exists
                  isValid = false;
                  MessageBox.Show("Reward with name '" + _nameText.Text + "' exists");
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateSideMissionPanel(string existingName)
      {
         bool isValid = false;
         if (_sideMissionPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               bool existing = SideMissionController.GetInstance().FindWithName(_nameText.Text).Count > 0;
               if (existing && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Side Mission exists
                  isValid = false;
                  MessageBox.Show("Side Mission with name '" + _nameText.Text + "' exists");
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateStoryMissionPanel(string existingName)
      {
         bool isValid = false;
         if (_storyMissionPanel.Visible)
         {
            isValid = ValidateNameTextbox();
            if(isValid)
            {
               bool existing = StoryMissionController.GetInstance().FindWithName(_nameText.Text).Count > 0;
               if (existing && !_nameText.Text.ToLower().Equals(existingName.ToLower()))
               {
                  //Story mission exists
                  isValid = false;
                  MessageBox.Show("Story Mission with name '" + _nameText.Text + "' exists");
               }
            }
         }
         return isValid;
      }
      
      public bool ValidateNameTextbox()
      {
         bool isValid = true;
         if (string.IsNullOrWhiteSpace(_nameText.Text))
         {
            //invalid name
            isValid = false;
            MessageBox.Show("Name must not be empty");
         }
         return isValid;
      }
   }
}

