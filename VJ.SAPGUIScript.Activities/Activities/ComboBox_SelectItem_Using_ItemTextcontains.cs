using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_DisplayName))]
    [LocalizedDescription(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_Description))]
    public class ComboBox_SelectItem_Using_ItemTextcontains : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_ComboBoxID_DisplayName))]
        [LocalizedDescription(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_ComboBoxID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ComboBoxID { get; set; }

        [LocalizedDisplayName(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_ItemText_DisplayName))]
        [LocalizedDescription(nameof(Resources.ComboBox_SelectItem_Using_ItemTextcontains_ItemText_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ItemText { get; set; }

        #endregion


        #region Constructors

        public ComboBox_SelectItem_Using_ItemTextcontains()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ComboBoxID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ComboBoxID)));
            if (ItemText == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ItemText)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var comboboxid = ComboBoxID.Get(context);
            var itemtext = ItemText.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.ComboBox_SelectItem_Using_ItemTextcontains(comboboxid, itemtext);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

