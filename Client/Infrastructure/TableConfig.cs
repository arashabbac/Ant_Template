namespace Infrastructure
{
    public class TableConfig<T>
    {
        public TableConfig() : base()
        {
        }

        public string TableCssClass { get; set; } = "table-responsive text-nowrap ant-table-wrapper-rtl";

        public string ColumnCssClass { get; set; } = "text-center";

        public bool Loading { get; set; }

        public System.Collections.Generic.Dictionary<string, object>
            OnRow(AntDesign.TableModels.RowData<T> row)
        {
            var rowNumber = new System.Collections.Generic.Dictionary<string, object>()
            {
                { "rownumber", row.RowIndex }
            };

            return rowNumber;
        }
    }
}
