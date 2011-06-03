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
// Spacer - REMOVE

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

        public ActionResult Index()
        {
            IEnumerable<DefinitionRecord> definitions = _definitionListService.GetDefinitionList();
            var entries = definitions.Select(CreateDefinitionEntry).ToList();
            var model = new DefinitionListAdminIndexViewModel { Tags = entries };
            return View(model);
        }

        private static DefinitionEntry CreateDefinitionEntry(DefinitionRecord definitionRecord)
        {
            return new DefinitionEntry
            {
                Record = definitionRecord,
                IsChecked = false,
            };
        }
    }
}