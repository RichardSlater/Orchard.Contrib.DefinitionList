using System;
using System.Collections.Generic;
using Contrib.DefinitionList.Models;
using Contrib.DefinitionList.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace Contrib.DefinitionList.Services
{
	public interface IDefinitionListService : IDependency {
		void UpdateDefinitionListForContentItem(ContentItem item, IEnumerable<DefinitionEntry> definitionList);
		IEnumerable<DefinitionRecord> GetDefinitionList();
	}
}
