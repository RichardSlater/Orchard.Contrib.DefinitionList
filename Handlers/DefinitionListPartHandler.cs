using System;
using Contrib.DefinitionList.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.DefinitionList.Handlers
{
	public class DefinitionListPartHandler : ContentHandler
	{
		public DefinitionListPartHandler(IRepository<DefinitionListPartRecord> repository)
		{
			Filters.Add(StorageFilter.For(repository));
		}
	}
}