using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooMetadataTest
{
    public class ColumnSchema
    {
        public string ColumnName { get; set; }
        public int ColumnOrdinal { get; set; }
        public int ColumnSize { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }
        public string IsUnique { get; set; }
        public int IsKey { get; set; }
        public int BaseServerName { get; set; }
        public int BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public int BaseSchemaName { get; set; }
        public int BaseTableName { get; set; }
        public Type DataType { get; set; }
        public string AllowDBNull { get; set; }
        public int ProviderType { get; set; }
        public int IsAliased { get; set; }
        public int IsExpression { get; set; }
        public string IsIdentity { get; set; }
        public string IsAutoIncrement { get; set; }
        public string IsRowVersion { get; set; }
        public int IsHidden { get; set; }
        public string IsLong { get; set; }
        public string IsReadOnly { get; set; }
        public Type ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }
        public int XmlSchemaCollectionDatabase { get; set; }
        public int XmlSchemaCollectionOwningSchema { get; set; }
        public int XmlSchemaCollectionName { get; set; }
        public int UdtAssemblyQualifiedName { get; set; }
        public int NonVersionedProviderType { get; set; }
        public string IsColumnSet { get; set; }
    }
}
