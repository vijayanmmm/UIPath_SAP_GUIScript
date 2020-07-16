using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Table_GetCellValue_DisplayName))]
    [LocalizedDescription(nameof(Resources.Table_GetCellValue_Description))]
    public class Table_GetCellValue : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Table_GetCellValue_TableID_DisplayName))]
        [LocalizedDescription(nameof(Resources.Table_GetCellValue_TableID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> TableID { get; set; }

        [LocalizedDisplayName(nameof(Resources.Table_GetCellValue_RowIndex_DisplayName))]
        [LocalizedDescription(nameof(Resources.Table_GetCellValue_RowIndex_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<int> RowIndex { get; set; }

        [LocalizedDisplayName(nameof(Resources.Table_GetCellValue_ColumnIndex_DisplayName))]
        [LocalizedDescription(nameof(Resources.Table_GetCellValue_ColumnIndex_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<int> ColumnIndex { get; set; }

        [LocalizedDisplayName(nameof(Resources.Table_GetCellValue_CellValue_DisplayName))]
        [LocalizedDescription(nameof(Resources.Table_GetCellValue_CellValue_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> CellValue { get; set; }

        #endregion


        #region Constructors

        public Table_GetCellValue()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (TableID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(TableID)));
            if (RowIndex == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(RowIndex)));
            if (ColumnIndex == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ColumnIndex)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var tableid = TableID.Get(context);
            var rowindex = RowIndex.Get(context);
            var columnindex = ColumnIndex.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            context.SetValue(CellValue, objSAPAuto.Table_GetCellValue(tableid, rowindex, columnindex));
            ///////////////////////////

            // Outputs
            return (ctx) => {
                CellValue.Set(ctx, null);
            };
        }

        #endregion
    }
}

