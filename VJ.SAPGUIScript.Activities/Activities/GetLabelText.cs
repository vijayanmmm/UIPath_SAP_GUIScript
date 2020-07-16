using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.GetLabelText_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetLabelText_Description))]
    public class GetLabelText : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetLabelText_ControlID_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetLabelText_ControlID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ControlID { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetLabelText_LabelText_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetLabelText_LabelText_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> LabelText { get; set; }

        #endregion


        #region Constructors

        public GetLabelText()
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
            string strLabelText= objSAPAuto.GetLabelText(controlid);
            
            ///////////////////////////

            // Outputs
            return (ctx) => {
                LabelText.Set(ctx, strLabelText);
            };
        }

        #endregion
    }
}

