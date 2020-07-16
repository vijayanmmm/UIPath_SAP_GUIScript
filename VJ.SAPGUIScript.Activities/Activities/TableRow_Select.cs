using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.TableRow_Select_DisplayName))]
    [LocalizedDescription(nameof(Resources.TableRow_Select_Description))]
    public class TableRow_Select : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TableRow_Select_TableID_DisplayName))]
        [LocalizedDescription(nameof(Resources.TableRow_Select_TableID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> TableID { get; set; }

        [LocalizedDisplayName(nameof(Resources.TableRow_Select_RowNumber_DisplayName))]
        [LocalizedDescription(nameof(Resources.TableRow_Select_RowNumber_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> RowNumber { get; set; }

        #endregion


        #region Constructors

        public TableRow_Select()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (TableID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(TableID)));
            if (RowNumber == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(RowNumber)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var tableid = TableID.Get(context);
            var rownumber = RowNumber.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.TableRow_Select(tableid, rownumber);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

