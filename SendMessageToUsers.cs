// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.TeamsBot
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::Azure.Identity;
    using Microsoft.Graph;

    /// <summary>
    /// Sends a message to users.
    /// </summary>
    public static class SendMessageToUsers
    {
        /// <summary>
        /// Generates a graph service client.
        /// </summary>
        /// <returns>The graph service client.</returns>
        public static GraphServiceClient CreateAuthProvider() {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "a41e10c9-a609-4413-bcbc-72c10363381b";

            // Values from app registration
            var clientId = "6a3e328a-bd79-44d5-ac94-d3abdf51e3c0";
            var clientSecret = "c.U7Q~ysNbQ3vlwYOPXWkoTUTSpOPRInT73SY";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            return new GraphServiceClient(clientSecretCredential, scopes);
        }

        /// <summary>
        /// Sends a message to users.
        /// </summary>
        /// <param name="messageToSend">The URL to be sent to the chat.</param>
        public static async void SendMessage(string messageToSend) {
            GraphServiceClient graphClient = CreateAuthProvider();

            var chatMessage = new ChatMessage
            {
                Body = new ItemBody
                {
                    Content = messageToSend,
                },
            };
            await graphClient.Teams["1b304f62-f323-4721-b5cd-6faaf6e57d4b"].Channels["19%3aQgf1lnOXPh8QbhTwll-G1cdc8zx1WTykpIiAYhQpSGo1%40thread.tacv2"].Messages.Request().AddAsync(chatMessage).ConfigureAwait(false);
        }
    }
}
