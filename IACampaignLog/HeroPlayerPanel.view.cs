using System;
using System.Windows.Forms;

namespace IACampaignLog
{
   public partial class HeroPlayerPanel : PlayerPanelBase<HeroPlayer>
   {
      private MenuItem _sellMenuItem;
         
      private void Initialise()
      {
         _itemListView.Columns.Add("Tier");
         
         //Item trade context menu
         _itemActionMenuItem.Text = "Trade";
         //Item sell menu item
         _sellMenuItem = new MenuItem("Sell");
         _itemContextMenu.MenuItems.Add(_sellMenuItem);
         
      }
   }
}

