using System;
using Contrib.DefinitionList.Models;

namespace Contrib.DefinitionList.ViewModels
{
	public class DefinitionEntry {
		public DefinitionRecord Record { get; set; }
		public bool IsChecked { get; set; }
	}
}
