using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.TextBox_Input_DisplayName))]
    [LocalizedDescription(nameof(Resources.TextBox_Input_Description))]
    public class TextBox_Input : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TextBox_Input_ControlID_DisplayName))]
        [LocalizedDescription(nameof(Resources.TextBox_Input_ControlID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ControlID { get; set; }

        [LocalizedDisplayName(nameof(Resources.TextBox_Input_Text_DisplayName))]
        [LocalizedDescription(nameof(Resources.TextBox_Input_Text_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Text { get; set; }

        #endregion


        #region Constructors

        public TextBox_Input()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ControlID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ControlID)));
            if (Text == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Text)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var controlid = ControlID.Get(context);
            var text = Text.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.TextInputByID(controlid,text);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

