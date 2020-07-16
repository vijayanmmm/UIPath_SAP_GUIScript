using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Menu_SelectionByID_DisplayName))]
    [LocalizedDescription(nameof(Resources.Menu_SelectionByID_Description))]
    public class Menu_SelectionByID : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Menu_SelectionByID_MenuID_DisplayName))]
        [LocalizedDescription(nameof(Resources.Menu_SelectionByID_MenuID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> MenuID { get; set; }

        #endregion


        #region Constructors

        public Menu_SelectionByID()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (MenuID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(MenuID)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var menuid = MenuID.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.MenuSelectionByID(menuid);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

