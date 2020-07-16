using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Get_TextBox_Value_DisplayName))]
    [LocalizedDescription(nameof(Resources.Get_TextBox_Value_Description))]
    public class Get_TextBox_Value : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Get_TextBox_Value_ControlID_DisplayName))]
        [LocalizedDescription(nameof(Resources.Get_TextBox_Value_ControlID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ControlID { get; set; }

        [LocalizedDisplayName(nameof(Resources.Get_TextBox_Value_TextBoxValue_DisplayName))]
        [LocalizedDescription(nameof(Resources.Get_TextBox_Value_TextBoxValue_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> TextBoxValue { get; set; }

        #endregion


        #region Constructors

        public Get_TextBox_Value()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ControlID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ControlID)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var controlid = ControlID.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            String strTextBoxValue = objSAPAuto.Get_TextBox_Value_ByID(controlid);
            ///////////////////////////

            // Outputs
            return (ctx) => {
                TextBoxValue.Set(ctx, strTextBoxValue);
            };
        }

        #endregion
    }
}

