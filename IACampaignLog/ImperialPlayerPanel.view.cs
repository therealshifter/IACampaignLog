using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class ImperialPlayerPanel : PlayerPanelBase<ImperialPlayer>
   {
      private Label _influenceLabel;
         
      private void Initialise()
      {
         _itemListView.Columns.Add("Target");
         
         //influence label
         _influenceLabel = new Label();
         _influenceLabel.Size = new System.Drawing.Size(40, 25);
         _influenceLabel.Location = new System.Drawing.Point(90, 30);
         this.Controls.Add(_influenceLabel);
         
         _itemActionMenuItem.Text = "Target";
      }
   }
}

