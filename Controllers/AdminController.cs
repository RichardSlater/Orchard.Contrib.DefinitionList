using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Contrib.DefinitionList.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Mvc.Extensions;
using Contrib.DefinitionList.Models;
using Contrib.DefinitionList.ViewModels;
using Orchard.DisplayManagement.Shapes;

namespace Contrib.DefinitionList.Controllers {
    [ValidateInput(false)]
    public class AdminController : Controller {
        private readonly IDefinitionListService _definitionListService;

        public AdminController(IDefinitionListService definitionListService, IOrchardServices services) {
            Services = services;
            _definitionListService = definitionListService;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

		#region Create

		public ActionResult Create() {
			return View(new DefinitionListAdminCreateViewModel());
		}

		[HttpPost]
		public ActionResult Create(FormCollection values) {
			var viewModel = new DefinitionListAdminCreateViewModel();

			if (!TryUpdateModel(viewModel))
				return View(viewModel);

			_definitionListService.CreateDefinitionItem(viewModel.DefinitionTerm, viewModel.DefinitionDefinition);

			return RedirectToAction("Index");
		}

		#endregion

		#region Read

		public ActionResult Index() {
			var definitions = _definitionListService.GetDefinitionList();
			var entries = definitions.Select(CreateDefinitionEntry).ToList();
			var model = new DefinitionListAdminIndexViewModel { Definitions = entries };
			return View(model);
		}

		#endregion

		#region Update

		public ActionResult Edit(int id) {
			var definition = _definitionListService.GetById(id);
			
			if (definition == null)
				return RedirectToAction("Index");

			var childItems = _definitionListService.GetChildItemsById(id);

			var viewModel = new DefinitionListAdminEditViewModel {
				Id = definition.Id,
				Term = definition.Term,
				Definition = definition.Definition,
				SubItems = childItems.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(FormCollection values) {
			var viewModel = new DefinitionListAdminEditViewModel();

			if (!TryUpdateModel(viewModel))
				return View(viewModel);

			_definitionListService.UpdateDefinitionItem(viewModel.Id, viewModel.Term, viewModel.Definition);

			return RedirectToAction("Index");
		}

		#endregion

		#region Delete

		public ActionResult Delete(int id) {
			var definition = _definitionListService.GetById(id);

			if (definition == null)
				return RedirectToAction("Index");
			
			var viewModel = new DefinitionListAdminDeleteViewModel
			{
				Id = definition.Id,
				Term = definition.Term,
				Definition = definition.Definition
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Delete(FormCollection values) {
			var viewModel = new DefinitionListAdminDeleteViewModel();

			if (!TryUpdateModel(viewModel))
				return View(viewModel);

			_definitionListService.DeleteDefinitionItem(viewModel.Id);

			return RedirectToAction("Index");
		}
		
		#endregion

		
		private static DefinitionEntry CreateDefinitionEntry(DefinitionRecord definitionRecord)
		{
			return new DefinitionEntry
			{
				Record = definitionRecord,
				IsChecked = false
			};
		}
	}
}