using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Contrib.DefinitionList
{
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("definitionLists")
                .Add(T("A-Z"), "5",
                    menu => menu.Add(T("List"), "0", item => item.Action("Index", "Admin", new { area = "Contrib.DefinitionList" })));
        }
    }
}