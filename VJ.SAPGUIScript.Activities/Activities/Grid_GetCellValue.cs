using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Grid_GetCellValue_DisplayName))]
    [LocalizedDescription(nameof(Resources.Grid_GetCellValue_Description))]
    public class Grid_GetCellValue : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Grid_GetCellValue_GridID_DisplayName))]
        [LocalizedDescription(nameof(Resources.Grid_GetCellValue_GridID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> GridID { get; set; }

        [LocalizedDisplayName(nameof(Resources.Grid_GetCellValue_RowIndex_DisplayName))]
        [LocalizedDescription(nameof(Resources.Grid_GetCellValue_RowIndex_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<int> RowIndex { get; set; }

        [LocalizedDisplayName(nameof(Resources.Grid_GetCellValue_ColumnID_DisplayName))]
        [LocalizedDescription(nameof(Resources.Grid_GetCellValue_ColumnID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ColumnID { get; set; }

        [LocalizedDisplayName(nameof(Resources.Grid_GetCellValue_CellValue_DisplayName))]
        [LocalizedDescription(nameof(Resources.Grid_GetCellValue_CellValue_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> CellValue { get; set; }

        #endregion


        #region Constructors

        public Grid_GetCellValue()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (GridID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(GridID)));
            if (RowIndex == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(RowIndex)));
            if (ColumnID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ColumnID)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var gridid = GridID.Get(context);
            var rowindex = RowIndex.Get(context);
            var columnid = ColumnID.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();            
            context.SetValue(CellValue, objSAPAuto.Grid_GetCellValue(gridid, rowindex, columnid));
            ///////////////////////////

            // Outputs
            return (ctx) => {
                CellValue.Set(ctx, null);
            };
        }

        #endregion
    }
}

