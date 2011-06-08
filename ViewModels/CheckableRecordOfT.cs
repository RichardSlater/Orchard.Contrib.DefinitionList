using System;

namespace Contrib.DefinitionList.ViewModels
{
    public class CheckableRecord<T> {
        public bool Checked { get; set; }
        public bool Disabled { get; set; }
        public T Record { get; set; }
    }
}