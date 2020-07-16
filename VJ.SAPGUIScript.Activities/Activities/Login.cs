using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using VJ.SAPGUIScript.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace VJ.SAPGUIScript.Activities
{
    [LocalizedDisplayName(nameof(Resources.Login_DisplayName))]
    [LocalizedDescription(nameof(Resources.Login_Description))]
    public class Login : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.Login_ServerName_DisplayName))]
        [LocalizedDescription(nameof(Resources.Login_ServerName_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> ServerName { get; set; }

        [LocalizedDisplayName(nameof(Resources.Login_Client_DisplayName))]
        [LocalizedDescription(nameof(Resources.Login_Client_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Client { get; set; }

        [LocalizedDisplayName(nameof(Resources.Login_Username_DisplayName))]
        [LocalizedDescription(nameof(Resources.Login_Username_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Username { get; set; }

        [LocalizedDisplayName(nameof(Resources.Login_Password_DisplayName))]
        [LocalizedDescription(nameof(Resources.Login_Password_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<SecureString> Password { get; set; }

        [LocalizedDisplayName(nameof(Resources.Login_Language_DisplayName))]
        [LocalizedDescription(nameof(Resources.Login_Language_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Language { get; set; }

         #endregion


        #region Constructors

        public Login()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (ServerName == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ServerName)));
            if (Client == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Client)));
            if (Username == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Username)));
            if (Password == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Password)));
            if (Language == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Language)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var servername = ServerName.Get(context);
            var client = Client.Get(context);
            var username = Username.Get(context);
            var password = Password.Get(context);
            var language = Language.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            SAPAuto objSAPAuto = new SAPAuto();
            objSAPAuto.login(servername,client,username,password,language);
            ///////////////////////////

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

