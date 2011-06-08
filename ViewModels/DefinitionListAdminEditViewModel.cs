using System;
using Contrib.DefinitionList.Models;
using System.Collections.Generic;

namespace Contrib.DefinitionList.ViewModels {
	public class DefinitionListAdminEditViewModel {
		public int Id { get; set; }
		public string Definition { get; set; }
		public string Term { get; set; }
		public IList<CheckableRecord<DefinitionRecord>> SubItems { get; set; }
	}
}
