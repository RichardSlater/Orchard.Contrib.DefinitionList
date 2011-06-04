using System;
using System.Collections.Generic;
using Contrib.DefinitionList.Models;
using Contrib.DefinitionList.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace Contrib.DefinitionList.Services
{
	public interface IDefinitionListService : IDependency
	{
		#region Content Item Management
		void UpdateDefinitionListForContentItem(ContentItem item, IEnumerable<DefinitionEntry> definitionList);
		#endregion

		#region Definition Item Management
		DefinitionRecord GetById(int id);
		IEnumerable<DefinitionRecord> GetDefinitionList();
		void UpdateDefinitionItem(int id, string term, string definition);
		void DeleteDefinitionItem(int id);
		void CreateDefinitionItem(string term, string definition);
		#endregion
	}
}
