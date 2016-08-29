using System;
using System.Xml.Linq;

namespace IACampaignLog
{
   public interface ISerialisable
   {
      XElement Serialise();
   }
}

