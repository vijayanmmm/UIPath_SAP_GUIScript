using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.ContextMenuSelectItem_DisplayName))]
    [LocalizedDescription(nameof(Resources.ContextMenuSelectItem_Description))]
    public class ContextMenuSelectItem : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.ContextMenuSelectItem_GridID_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContextMenuSelectItem_GridID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> GridID { get; set; }

        [LocalizedDisplayName(nameof(Resources.ContextMenuSelectItem_ContextMenuItem_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContextMenuSelectItem_ContextMenuItem_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ContextMenuItem { get; set; }

        #endregion


        #region Constructors

        public ContextMenuSelectItem()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (GridID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(GridID)));
            if (ContextMenuItem == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ContextMenuItem)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var gridid = GridID.Get(context);
            var contextmenuitem = ContextMenuItem.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.ContextMenuSelectItem(gridid,contextmenuitem);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

