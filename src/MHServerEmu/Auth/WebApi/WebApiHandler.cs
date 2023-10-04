﻿using System.Collections.Specialized;
using System.Text;
using MHServerEmu.Common.Logging;
using MHServerEmu.GameServer.Frontend.Accounts;

namespace MHServerEmu.Auth.WebApi
{
    public enum WebApiRequest
    {
        AccountCreate,
        ServerStatus
    }

    public class WebApiHandler
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        private readonly string ResponseHtml;
        private readonly string AccountCreateFormHtml;

        public WebApiHandler()
        {
            string assetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "AuthServer");
            ResponseHtml = File.ReadAllText(Path.Combine(assetDirectory, "Response.html"));
            AccountCreateFormHtml = File.ReadAllText(Path.Combine(assetDirectory, "AccountCreateForm.html"));
        }

        public byte[] HandleRequest(WebApiRequest request, NameValueCollection queryString)
        {
            switch (request)
            {
                case WebApiRequest.AccountCreate: return HandleAccountCreateRequest(queryString);
                case WebApiRequest.ServerStatus: return HandleServerStatusRequest();
                default: Logger.Warn($"Unhandled request {request}"); return Array.Empty<byte>();
            }
        }

        private byte[] HandleAccountCreateRequest(NameValueCollection queryString)
        {
            // Show account creation form when no parameters are specified in the query string
            if (queryString == null) return Encoding.UTF8.GetBytes(AccountCreateFormHtml);

            // Check input
            if ((FieldIsValid(queryString["email"]) && FieldIsValid(queryString["playerName"]) && FieldIsValid(queryString["password"])) == false)
                return GenerateResponse("Error", "Input is not valid.");

            string accountManagerOutput = AccountManager.CreateAccount(queryString["email"].ToLower(), queryString["playerName"], queryString["password"]);
            Logger.Trace(accountManagerOutput);
            return GenerateResponse("Create Account", accountManagerOutput);
        }

        private byte[] HandleServerStatusRequest()
        {
            return GenerateResponse("Server Status", Program.GetServerStatus());
        }

        private byte[] GenerateResponse(string title, string text)
        {
            StringBuilder sb = new(ResponseHtml);
            sb.Replace("%RESPONSE_TITLE%", title);
            sb.Replace("%RESPONSE_TEXT%", text);
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private static bool FieldIsValid(string field) => (field != null) && (field.Length > 0);
    }
}
