using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.TreeView_Select_DisplayName))]
    [LocalizedDescription(nameof(Resources.TreeView_Select_Description))]
    public class TreeView_Select : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TreeView_Select_TreeID_DisplayName))]
        [LocalizedDescription(nameof(Resources.TreeView_Select_TreeID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> TreeID { get; set; }

        [LocalizedDisplayName(nameof(Resources.TreeView_Select_TreeItemKey_DisplayName))]
        [LocalizedDescription(nameof(Resources.TreeView_Select_TreeItemKey_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> TreeItemKey { get; set; }

        #endregion


        #region Constructors

        public TreeView_Select()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (TreeID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(TreeID)));
            if (TreeItemKey == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(TreeItemKey)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var treeid = TreeID.Get(context);
            var treeitemkey = TreeItemKey.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.TreeView_Select(treeid, treeitemkey);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

