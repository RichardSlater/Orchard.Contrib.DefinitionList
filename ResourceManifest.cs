using System;
using Orchard.UI.Resources;

namespace Contrib.DefinitionList {
	public class ResourceManifest : IResourceManifestProvider {
		public void BuildManifests(ResourceManifestBuilder builder) {
			builder.Add().DefineStyle("DefinitionListAdmin").SetUrl("contrib-definition-list-admin.css");
		}
	}
}