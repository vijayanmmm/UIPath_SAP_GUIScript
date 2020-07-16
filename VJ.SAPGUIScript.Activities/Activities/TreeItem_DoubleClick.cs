using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.TreeItem_DoubleClick_DisplayName))]
    [LocalizedDescription(nameof(Resources.TreeItem_DoubleClick_Description))]
    public class TreeItem_DoubleClick : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TreeItem_DoubleClick_ControlID_DisplayName))]
        [LocalizedDescription(nameof(Resources.TreeItem_DoubleClick_ControlID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ControlID { get; set; }

        [LocalizedDisplayName(nameof(Resources.TreeItem_DoubleClick_ItemKey_DisplayName))]
        [LocalizedDescription(nameof(Resources.TreeItem_DoubleClick_ItemKey_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ItemKey { get; set; }

        #endregion


        #region Constructors

        public TreeItem_DoubleClick()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ControlID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ControlID)));
            if (ItemKey == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ItemKey)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var controlid = ControlID.Get(context);
            var itemkey = ItemKey.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.TreeView_Select(controlid, itemkey);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

