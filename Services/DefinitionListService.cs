using System;
using System.Collections.Generic;
using System.Linq;
using Contrib.DefinitionList.Models;
using Contrib.DefinitionList.ViewModels;
using Orchard.ContentManagement;
using Orchard.Data;

namespace Contrib.DefinitionList.Services
{
	public class DefinitionListService : IDefinitionListService {
		private readonly IRepository<DefinitionRecord> _definitionListRepository;
		private readonly IRepository<ContentDefinitionRecord> _definitionListContentRepository;

		public DefinitionListService(
			IRepository<DefinitionRecord> definitionListRepository,
			IRepository<ContentDefinitionRecord> definitionListContentRepository) {
			
			_definitionListRepository = definitionListRepository;
			_definitionListContentRepository = definitionListContentRepository;
		}

		public void UpdateDefinitionListForContentItem(ContentItem item, IEnumerable<DefinitionEntry> definitionEntries) {
			var record = item.As<DefinitionListPart>().Record;
			var oldEntries = _definitionListContentRepository.Fetch(
				r => r.DefinitionListPartRecord == record);
			var lookupNew = definitionEntries
				.Where(e => e.IsChecked)
				.Select(e => e.Record)
				.ToDictionary(r => r, r => false);
			// Delete the entries that are no longer there and mark the ones that should stay
			foreach (var oldRecord in oldEntries) {
				if (lookupNew.ContainsKey(oldRecord.DefinitionRecord)) {
					lookupNew[oldRecord.DefinitionRecord] = true;
				}
				else {
					_definitionListContentRepository.Delete(oldRecord);
				}
			}
			// Add the new definitions
			foreach (var newDefinitionEntry in lookupNew.Where(kvp => !kvp.Value).Select(kvp => kvp.Key)) {
				_definitionListContentRepository.Create(new ContentDefinitionRecord {
					DefinitionListPartRecord = record,
					DefinitionRecord = newDefinitionEntry
				});
			}
		}

		public IEnumerable<DefinitionRecord> GetDefinitionList() {
			return _definitionListRepository.Table.ToList();
		}

		public DefinitionRecord GetById(int id) {
			return _definitionListRepository.Table.FirstOrDefault(x => x.Id == id);
		}

		public void UpdateDefinitionItem(int id, string term, string definition) {
			var entity = new DefinitionRecord {
				Id = id,
				Term = term,
				Definition = definition
			};

			_definitionListRepository.Update(entity);
		}
	}
}
