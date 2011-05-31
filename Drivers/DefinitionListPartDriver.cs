using System.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Contrib.DefinitionList.Models;
using Contrib.DefinitionList.Services;
using Contrib.DefinitionList.ViewModels;

namespace Contrib.DefinitionList.Drivers {
	[UsedImplicitly]
	public class DefinitionListPartDriver : ContentPartDriver<DefinitionListPart> {
		private readonly IDefinitionListService _definitionListService;

		private const string TemplateName = "Parts/DefinitionList";

		public DefinitionListPartDriver(IDefinitionListService definitionListService) {
			_definitionListService = definitionListService;
		}

		protected override string Prefix {
			get { return "DefinitionList"; }
		}

		protected override DriverResult Display(DefinitionListPart part, string displayType, dynamic shapeHelper) {
			return ContentShape("Parts_DefinitionList", 
				 () => shapeHelper.Parts_DefinitionList(
					 ContentPart: part, 
					 DefinitionList: part.Entries));
		}

		protected override DriverResult Editor(DefinitionListPart part, dynamic shapeHelper) {
			return ContentShape("Parts_DefinitionList_Edit",
				() => shapeHelper.EditorTemplate(
					TemplateName: TemplateName,
					Model: BuildEditorViewModel(part),
					Prefix: Prefix));
		}

		protected override DriverResult Editor(DefinitionListPart part, IUpdateModel updater, dynamic shapeHelper) {
			var model = new EditDefinitionListViewModel();
			updater.TryUpdateModel(model, Prefix, null, null);

			if (part.ContentItem.Id != 0) {
				_definitionListService.UpdateDefinitionListForContentItem(part.ContentItem, model.DefinitionList);
			}

			return Editor(part, shapeHelper);
		}

		private EditDefinitionListViewModel BuildEditorViewModel(DefinitionListPart part) {
			var definitionEntry = part.Entries.ToLookup(e => e.Id);
			return new EditDefinitionListViewModel {
				DefinitionList = _definitionListService.GetDefinitionList().Select(r => new DefinitionEntry {
					Record = r,
					IsChecked = definitionEntry.Contains(r.Id)
				}).ToList()
			};
		}
	}
}