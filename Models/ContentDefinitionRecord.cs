namespace Contrib.DefinitionList.Models {
	public class ContentDefinitionRecord {
		public virtual int Id { get; set; }
		public virtual DefinitionListPartRecord DefinitionListPartRecord { get; set; }
		public virtual DefinitionRecord DefinitionRecord { get; set; }
	}
}
