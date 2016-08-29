using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IACampaignLog
{
	public partial class MenuForm : Form
	{
		private MaintainResourcesForm _maintainResForm;
		private CreateGameForm _createGameForm;
      private ViewGameForm _viewGameForm;
      private IList<Game> _gameList;
		
		public MenuForm () : base()
		{
			Initialise();
         
         PopulateGameList();
         
         _gameListView.ItemSelectionChanged += Handle_gameListViewItemSelectionChanged;
			_exitMenuItem.Click += Handle_exitMenuItemClick;
			_maintainMenuItem.Click += Handle_maintainMenuItemClick;
			_createButton.Click += Handle_createButtonClick;
         _openButton.Click += Handle_openButtonClick;
         _gameListView.DoubleClick += Handle_openButtonClick;
			_maintainResForm = null;
			_createGameForm = null;
         _viewGameForm = null;
		}

      void Handle_openButtonClick (object sender, EventArgs e)
      {
         if (_gameListView.SelectedItems.Count > 0 && _viewGameForm == null)
         {
            Game selected = GameController.GetInstance().LoadGame(int.Parse(_gameListView.SelectedItems[0].Text));
            if (selected != null)
            {
               _viewGameForm = new ViewGameForm();
               _viewGameForm.Closed += Handle_viewGameFormClosed;
               _viewGameForm.Show();
               _viewGameForm.LoadGame(selected);
            }
         }
      }

      void Handle_viewGameFormClosed (object sender, EventArgs e)
      {
         _viewGameForm.Closed -= Handle_viewGameFormClosed;
         _viewGameForm = null;
      }

      void Handle_gameListViewItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
      {
         _openButton.Enabled = e.IsSelected && _viewGameForm == null;
      }

		void Handle_createButtonClick (object sender, EventArgs e)
		{
			if (_createGameForm == null)
			{
				_createGameForm = new CreateGameForm();
				_createGameForm.Closed += Handle_createGameFormClosed;
			}
			_createGameForm.Show();
		}

		void Handle_createGameFormClosed (object sender, EventArgs e)
		{
			_createGameForm.Closed -= Handle_createGameFormClosed;
			_createGameForm = null;
         PopulateGameList();
		}

		void Handle_maintainMenuItemClick (object sender, EventArgs e)
		{
			if (_maintainResForm == null)
			{
				_maintainResForm = new MaintainResourcesForm();
				_maintainResForm.Closed += Handle_maintainResFormClosed;
			}
			_maintainResForm.Show();
		}

		void Handle_maintainResFormClosed (object sender, EventArgs e)
		{
			_maintainResForm.Closed -= Handle_maintainResFormClosed;
			_maintainResForm = null;
		}

		void Handle_exitMenuItemClick (object sender, EventArgs e)
		{
			this.Close();
		}
      
      private void PopulateGameList()
      {
         _gameListView.Items.Clear();
         _gameList = new List<Game>(GameController.GetInstance().ListOfT);
         foreach (Game g in _gameList)
         {
            HeroPlayer p1 = g.Heroes.ElementAtOrDefault(0);
            HeroPlayer p2 = g.Heroes.ElementAtOrDefault(1);
            HeroPlayer p3 = g.Heroes.ElementAtOrDefault(2);
            HeroPlayer p4 = g.Heroes.ElementAtOrDefault(3);
            ListViewItem item = new ListViewItem(new string[]{g.Id.ToString(),
               g.GameDate.ToShortDateString(), g.ImpPlayer.Name,
               p1 == null ? string.Empty : p1.Name,
               p2 == null ? string.Empty : p2.Name,
               p3 == null ? string.Empty : p3.Name,
               p4 == null ? string.Empty : p4.Name,
               g.GameCampaign.Name});
            _gameListView.Items.Add(item);
         }
      }
	}
}

