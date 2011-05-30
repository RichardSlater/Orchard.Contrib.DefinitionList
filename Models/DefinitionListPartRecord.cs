using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace Contrib.DefinitionList.Models {
	public class DefinitionListPartRecord : ContentPartRecord {
		public DefinitionListPartRecord() {
			Entries = new List<ContentDefinitionRecord>();
		}
		public virtual IList<ContentDefinitionRecord> Entries { get; set; }
	}
}