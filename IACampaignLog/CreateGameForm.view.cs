using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class CreateGameForm : EditFormBase
   {
      private GameDetailsPanel _gameDetails;
      
      private void Initialise()
      {
         this.Text = "Create New Game";
         _gameDetails = new GameDetailsPanel();
         _gameDetails.Dock = DockStyle.Fill;
         this.Controls.Add(_gameDetails);
         this.Size = new System.Drawing.Size(650, 420);
      }
   }
}

