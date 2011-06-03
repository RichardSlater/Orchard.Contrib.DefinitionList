using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Contrib.DefinitionList.ViewModels
{
    public class DefinitionListAdminCreateViewModel {
        [Required, DisplayName("Term")]
        public string DefinitionTerm { get; set; }

        [Required, DisplayName("Definition")]
        public string DefinitionDefinition { get; set; }
    }
}