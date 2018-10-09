using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace IACampaignLog
{
   public class SingletonListController<T> where T : Identifiable, ISerialisable
   {
      protected static SingletonListController<T> _instance = null;
      private IList<T> _listOfT;
      private Func<XElement, T> _deserialiseT;
      private string _resourcePath;
      
      protected SingletonListController (string resourcePath, Func<XElement, T> deserialiseT)
      {
         _listOfT = new List<T>();
         _deserialiseT = deserialiseT;
         _resourcePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + resourcePath;
         HasChanges = false;
         Load();
      }
      
      public bool HasChanges {get; set;}
      public IList<T> ListOfT {get{return _listOfT;}}
      public string ResourcePath {get{return _resourcePath;}}
      
      public T FindWithId(int id)
      {
         return _listOfT.Where(x => x.Id == id).SingleOrDefault();
      }
      
      public IList<T> FindWithName(string name)
      {
         return (from T c in this.ListOfT
                 where c.Name.ToLower().Equals(name.ToLower())
                 select c).ToList();
      }

      public IList<T> FindWithNameContaining(string inName)
      {
         return (from T c in this.ListOfT
                 where c.Name.ToLower().Contains(inName.ToLower())
                 select c).ToList();
      }

      protected T AddT(Func<int, T> constructT)
      {
         int newId = 0;
         T newT = null;
         if (constructT == null)
         {throw new ArgumentException("Must specify type construction delegate", "constructT");}
         
         if (this.ListOfT.Count > 0)
         {
            //Get max id
            newId = this.ListOfT.Max((x) => x.Id) + 1;
         }
         newT = constructT(newId);
         this.ListOfT.Add(newT);
         HasChanges = true;
         return newT;
      }
      
      public virtual void Load()
      {
         this.ListOfT.Clear();
         
         if (System.IO.File.Exists(_resourcePath))
         {
            XDocument xdoc = XDocument.Load(_resourcePath);
            foreach (XElement elem in xdoc.Root.Elements())
            {
               this.ListOfT.Add(_deserialiseT(elem));
            }
            HasChanges = false;
         }
      }
      
      public virtual void Save(Func<T, XElement> serialiseT)
      {
         if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(_resourcePath)))
         {System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(_resourcePath));}
         
         XDocument xdoc = new XDocument();
         xdoc.Add(new XElement("Resource"));
         foreach (T item in this.ListOfT)
         {
            xdoc.Root.Add(serialiseT(item));
         }
         xdoc.Save(_resourcePath);
         HasChanges = false;
      }
      
      public virtual void Save()
      {
         this.Save((x) => x.Serialise());
      }
   }
}

