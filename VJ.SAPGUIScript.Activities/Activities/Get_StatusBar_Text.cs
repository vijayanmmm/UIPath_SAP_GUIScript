using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Get_StatusBar_Text_DisplayName))]
    [LocalizedDescription(nameof(Resources.Get_StatusBar_Text_Description))]
    public class Get_StatusBar_Text : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Get_StatusBar_Text_MessageType_DisplayName))]
        [LocalizedDescription(nameof(Resources.Get_StatusBar_Text_MessageType_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> MessageType { get; set; }

        [LocalizedDisplayName(nameof(Resources.Get_StatusBar_Text_StatusTextValue_DisplayName))]
        [LocalizedDescription(nameof(Resources.Get_StatusBar_Text_StatusTextValue_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<string> StatusTextValue { get; set; }

        #endregion


        #region Constructors

        public Get_StatusBar_Text()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {

            base.CacheMetadata(metadata);
        }


        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            Tuple<String, String> output = objSAPAuto.StatusTextAndMessageType();
            ///////////////////////////

            // Outputs
            return (ctx) => {
                MessageType.Set(ctx, output.Item1);
                StatusTextValue.Set(ctx, output.Item2);
            };
        }

        #endregion
    }
}

