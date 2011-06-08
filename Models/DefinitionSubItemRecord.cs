
namespace Contrib.DefinitionList.Models
{
	public class DefinitionSubItemRecord {
		public virtual int Id { get; set; }
		public virtual DefinitionRecord ParentDefinitionRecord { get; set; }
        public virtual DefinitionRecord ChildDefinitionRecord { get; set; }
	}
}
