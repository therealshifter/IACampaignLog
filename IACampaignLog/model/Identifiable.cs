using System;
namespace IACampaignLog
{
   public abstract class Identifiable
   {
      private int _id;
      
      public Identifiable (int id, string name)
      {
         _id = id;
         Name = name;
      }
      
      public int Id {get{return _id;}}
      public string Name {get; set;}
   }
}

