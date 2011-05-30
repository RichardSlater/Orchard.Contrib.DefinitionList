using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;

namespace Contrib.DefinitionList.Models {
	public class DefinitionListPart : ContentPart<DefinitionListPartRecord> {
		public IEnumerable<DefinitionRecord> Entries {
			get {
				return Record.Entries.Select(e => e.DefinitionRecord);
			}
		}
	}
}