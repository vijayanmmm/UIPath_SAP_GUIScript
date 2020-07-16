using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.CheckBox_ClickByID_DisplayName))]
    [LocalizedDescription(nameof(Resources.CheckBox_ClickByID_Description))]
    public class CheckBox_ClickByID : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.CheckBox_ClickByID_ControlID_DisplayName))]
        [LocalizedDescription(nameof(Resources.CheckBox_ClickByID_ControlID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ControlID { get; set; }

        [LocalizedDisplayName(nameof(Resources.CheckBox_ClickByID_Tick_DisplayName))]
        [LocalizedDescription(nameof(Resources.CheckBox_ClickByID_Tick_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Tick { get; set; }

        #endregion


        #region Constructors

        public CheckBox_ClickByID()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ControlID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ControlID)));
            if (Tick == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Tick)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var controlid = ControlID.Get(context);
            var tick = Tick.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.CheckBoxSelectByID(controlid,tick);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

