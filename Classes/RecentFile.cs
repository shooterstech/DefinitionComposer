using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Runtime;

namespace DefinitionComposer.Classes {
	public class RecentFile: IEquatable<RecentFile> {

		public RecentFile(string filePath, DefinitionType type, string setName) { 
			this.FilePath = filePath;
			this.DefinitionType = type;	
			this.SetName = setName;
		}

		public string FilePath { get; set; }
		public DefinitionType DefinitionType { get; set; }
		public string SetName { get; set; }

		public bool Equals( RecentFile other ) {
			if (other == null)
				return false;

			if (this.FilePath == other.FilePath)
				return true;

			return false; 
		}

		public override string ToString() {
			return $"{DefinitionType} {SetName}";
		}
	}

    public class  RecentFiles 
    {

		public EventHandler<EventArgs<RecentFile>> OnRecentFileAdded;

		public List<RecentFile> Files {  get; set; } = new List<RecentFile>();

		public void Add( RecentFile file ) {

			RecentFile toRemove = null;
			foreach( var f in Files ) {
				if (f.Equals( file )) {
					toRemove = f;
				}
			}
			if ( toRemove != null)
				Files.Remove( toRemove );

			if (Files.Count > 20)
				Files.RemoveAt( Files.Count - 1 );

			Files.Insert( 0, file );

			if (OnRecentFileAdded != null)
				OnRecentFileAdded.Invoke( this, new EventArgs<RecentFile>( file ) );
		}
        
    }
}
