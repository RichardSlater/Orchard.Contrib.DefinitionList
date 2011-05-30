using System;
using System.Collections.Generic;
using Contrib.DefinitionList.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;

namespace Contrib.DefinitionList {
	public class Migrations : DataMigrationImpl {
		private readonly IRepository<DefinitionRecord> _definitionListRepository;
		private readonly IEnumerable<DefinitionRecord> _sampleRecords = new List<DefinitionRecord> {
			new DefinitionRecord { Term = "Term A", Definition = "This is the definition for Term A" },
			new DefinitionRecord { Term = "Term B", Definition = "This is the definition for Term B" },
			new DefinitionRecord { Term = "Term C", Definition = "This is the definition for Term C" }
		};

		public Migrations(IRepository<DefinitionRecord> definitionListRepository) {
			_definitionListRepository = definitionListRepository;
		}

		public int Create()
		{
			SchemaBuilder.CreateTable("DefinitionListPartRecord",
				table => table
					.ContentPartRecord()
				);

			SchemaBuilder.CreateTable("DefinitionRecord",
				table => table
					.Column<int>("Id", column => column.PrimaryKey().Identity())
					.Column<string>("Term")
					.Column<string>("Definition")
				);

			SchemaBuilder.CreateTable("ContentDefinitionRecord",
				table => table
					.Column<int>("Id", column => column.PrimaryKey().Identity())
					.Column<int>("DefinitionListPartRecord_Id")
					.Column<int>("DefinitionRecord_Id")
				);

			ContentDefinitionManager.AlterPartDefinition(
				"DefinitionListPart",
				builder => builder.Attachable());

			if (_definitionListRepository == null)
				throw new InvalidOperationException("Cannot find the Definition List Repository");

			foreach (var entity in _sampleRecords) {
				_definitionListRepository.Create(entity);
			}

			return 1;
		}
	}
}