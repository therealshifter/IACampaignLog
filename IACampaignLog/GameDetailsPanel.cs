using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
   public partial class GameDetailsPanel : Panel
   {
      private bool _isReadOnly;
      private IList<Campaign> _campaignList;
      private IList<Character> _p1CharacterList, _p2CharacterList, _p3CharacterList, _p4CharacterList;
      private IList<CardSet<ClassCard>> _imperialClassList;
      private IList<CardSet<Agenda>> _agendaSetList;
      private IList<SideMission> _greenMissions, _greyMissions;
      private Game _gameDetails;
      
      public GameDetailsPanel ()
      {
         this.Initialise();
         
         //Campaign selector data
         _campaignList = new List<Campaign>(CampaignController.GetInstance().ListOfT);
         _campaignCombobox.DataSource = _campaignList;
         _campaignCombobox.DisplayMember = "Name";
         _campaignCombobox.ValueMember = "Id";
         
         _imperialClassList = new List<CardSet<ClassCard>>(ClassController.GetInstance().FindClassSetForCharacter(CharacterController.GetInstance().ImperialCharacter.Id));
         _imperialClassCombo.DataSource = _imperialClassList;
         _imperialClassCombo.DisplayMember = "Name";
         _imperialClassCombo.ValueMember = "Id";
         
         _p1CharacterList = new List<Character>(CharacterController.GetInstance().ListOfT);
         _p2CharacterList = new List<Character>(CharacterController.GetInstance().ListOfT);
         _p3CharacterList = new List<Character>(CharacterController.GetInstance().ListOfT);
         _p4CharacterList = new List<Character>(CharacterController.GetInstance().ListOfT);
         _player1CharacterCombo.DataSource = _p1CharacterList;
         _player1CharacterCombo.DisplayMember = "Name";
         _player1CharacterCombo.ValueMember = "Id";
         _player2CharacterCombo.DataSource = _p2CharacterList;
         _player2CharacterCombo.DisplayMember = "Name";
         _player2CharacterCombo.ValueMember = "Id";
         _player3CharacterCombo.DataSource = _p3CharacterList;
         _player3CharacterCombo.DisplayMember = "Name";
         _player3CharacterCombo.ValueMember = "Id";
         _player4CharacterCombo.DataSource = _p4CharacterList;
         _player4CharacterCombo.DisplayMember = "Name";
         _player4CharacterCombo.ValueMember = "Id";
         
         _agendaSetList = new List<CardSet<Agenda>>(AgendaController.GetInstance().ListOfT);
         
         //Agenda list box data
         _agendasListBox.DataSource = _agendaSetList;
         _agendasListBox.DisplayMember = "Name";
         
         //Side mission list boxes data
         _greenMissions = new List<SideMission>(SideMissionController.GetInstance().SideMissionsOfType(SideMission.MissionType.Green));
         _greyMissions = new List<SideMission>(SideMissionController.GetInstance().SideMissionsOfType(SideMission.MissionType.Grey));
         _sideMissionsGreenListBox.DataSource = _greenMissions;
         _sideMissionsGreyListBox.DataSource = _greyMissions;
         _sideMissionsGreenListBox.DisplayMember = "Name";
         _sideMissionsGreyListBox.DisplayMember = "Name";
         
         _gameDetails = null;
      }

      public void LoadGame(Game gameDetails)
      {
         _gameDetails = gameDetails;
         if (_gameDetails != null)
         {
            _campaignCombobox.SelectedItem = gameDetails.GameCampaign;
            _imperialClassCombo.SelectedValue = gameDetails.ImpPlayer.PlayerClass.Id;
            _impPlayerText.Text = gameDetails.ImpPlayer.Name;
            _player1CharacterCombo.SelectedIndex = _player1CharacterCombo.Items.IndexOf(gameDetails.Heroes[0].PlayerCharacter);
            _player1Text.Text = gameDetails.Heroes[0].Name;
            if (gameDetails.Heroes.Count > 1)
            {
               _player2CharacterCombo.SelectedValue = gameDetails.Heroes[1].PlayerCharacter.Id;
               _player2Text.Text = gameDetails.Heroes[1].Name;
            }
            if (gameDetails.Heroes.Count > 2)
            {
               _player3CharacterCombo.SelectedValue = gameDetails.Heroes[2].PlayerCharacter.Id;
               _player3Text.Text = gameDetails.Heroes[2].Name;
            }
            if (gameDetails.Heroes.Count > 3)
            {
               _player4CharacterCombo.SelectedValue = gameDetails.Heroes[3].PlayerCharacter.Id;
               _player4Text.Text = gameDetails.Heroes[3].Name;
            }
            
            _agendasListBox.DataSource = _gameDetails.SelectedAgendaSets;
            _agendasListBox.DisplayMember = "Name";
            _agendasListBox.SelectedItems.Clear();
            
            _sideMissionsGreenListBox.DataSource = _gameDetails.SelectedSideMissions.Where((x) => x.SideMissionType == SideMission.MissionType.Green).ToList();
            _sideMissionsGreenListBox.DisplayMember = "Name";
            _sideMissionsGreenListBox.SelectedItems.Clear();
            
            _sideMissionsGreyListBox.DataSource = _gameDetails.SelectedSideMissions.Where((x) => x.SideMissionType == SideMission.MissionType.Grey).ToList();
            _sideMissionsGreyListBox.DisplayMember = "Name";
            _sideMissionsGreyListBox.SelectedItems.Clear();
            
            ReadOnly = true;
         }
      }
      
      public bool ReadOnly {
         get
         {return _isReadOnly;}
         set
         {
            _isReadOnly = value;
            _campaignCombobox.Enabled = !value;
            _impPlayerText.Enabled = !value;
            _imperialClassCombo.Enabled = !value;
            _player1Text.Enabled = !value;
            _player1CharacterCombo.Enabled = !value;
            _player2Text.Enabled = !value;
            _player2CharacterCombo.Enabled = !value;
            _player3Text.Enabled = !value;
            _player3CharacterCombo.Enabled = !value;
            _player4Text.Enabled = !value;
            _player4CharacterCombo.Enabled = !value;
            _agendasListBox.Enabled = !value;
            _sideMissionsGreenListBox.Enabled = !value;
            _sideMissionsGreyListBox.Enabled = !value;
         }
      }
      
      private bool ValidateCharacter(IList<Character> selectedCharacters, Character playerCharacter)
      {
         if (selectedCharacters.Contains(playerCharacter))
         {
            MessageBox.Show("More than one player with Character '" + playerCharacter.Name + "'");
            return false;
         }
         else
         {
            selectedCharacters.Add(playerCharacter);
            return true;
         }
      }
      
      public bool Validate()
      {
         bool isValid = true;
         if (string.IsNullOrWhiteSpace(_impPlayerText.Text))
         {
            isValid = false;
            MessageBox.Show("Imperial player must not be empty");
         }
         if (isValid)
         {
            IList<Character> selectedCharacters = new List<Character>(4);
            if (!string.IsNullOrWhiteSpace(_player1Text.Text))
            {
               selectedCharacters.Add((Character)_player1CharacterCombo.SelectedItem);
            }
            if (!string.IsNullOrWhiteSpace(_player2Text.Text))
            {
               isValid = ValidateCharacter(selectedCharacters, (Character)_player2CharacterCombo.SelectedItem);
            }
            if (isValid && !string.IsNullOrWhiteSpace(_player3Text.Text))
            {
               isValid = ValidateCharacter(selectedCharacters, (Character)_player3CharacterCombo.SelectedItem);
            }
            if (isValid && !string.IsNullOrWhiteSpace(_player4Text.Text))
            {
               isValid = ValidateCharacter(selectedCharacters, (Character)_player4CharacterCombo.SelectedItem);
            }
            if (isValid && selectedCharacters.Count < 2)
            {
               isValid = false;
               MessageBox.Show("Must have at least 2 players");
            }
            if (isValid)
            {
               if (_agendasListBox.SelectedItems.Count != 6)
               {
                  MessageBox.Show("Must have 6 Agenda sets selected");
                  isValid = false;
               }
            }
            if (isValid)
            {
               if (SelectedCampaign.AllowSideMissions && _sideMissionsGreenListBox.SelectedItems.Count != 4)
               {
                  MessageBox.Show("Must have 4 Green side missions selected");
                  isValid = false;
               }
            }
            if (isValid)
            {
               if (SelectedCampaign.AllowSideMissions && _sideMissionsGreyListBox.SelectedItems.Count != 4)
               {
                  MessageBox.Show("Must have 4 Grey side missions selected");
                  isValid = false;
               }
            }
         }
         return isValid;
      }
      
      public Campaign SelectedCampaign
      {
         get{return (Campaign)_campaignCombobox.SelectedItem;}
      }
      
      public string ImperialPlayerName
      {
         get{return _impPlayerText.Text;}
      }
      
      public CardSet<ClassCard> ImperialClass
      {
         get{return (CardSet<ClassCard>)_imperialClassCombo.SelectedItem;}
      }
      
      public string Player1Name
      {
         get
         {return _player1Text.Text;}
      }
      
      public string Player2Name
      {
         get
         {return _player2Text.Text;}
      }
      
      public string Player3Name
      {
         get
         {return _player3Text.Text;}
      }
      
      public string Player4Name
      {
         get
         {return _player4Text.Text;}
      }
      
      public Character Player1Character
      {
         get
         {return (Character)_player1CharacterCombo.SelectedItem;}
      }
      
      public Character Player2Character
      {
         get
         {return (Character)_player2CharacterCombo.SelectedItem;}
      }
      
      public Character Player3Character
      {
         get
         {return (Character)_player3CharacterCombo.SelectedItem;}
      }
      
      public Character Player4Character
      {
         get
         {return (Character)_player4CharacterCombo.SelectedItem;}
      }
      
      public IList<CardSet<Agenda>> SelectedAgendas()
      {
         return (from CardSet<Agenda> csa in _agendasListBox.SelectedItems
                 select csa).ToList();
      }
      
      public IList<SideMission> SelectedSideMissions()
      {
         return (from SideMission a in _sideMissionsGreenListBox.SelectedItems
                 select a)
                 .Union(from SideMission b in _sideMissionsGreyListBox.SelectedItems
                        select b).ToList();
      }
   }
}

