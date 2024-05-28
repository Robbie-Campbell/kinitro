// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace SendMessageToUsers
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
        /// Sends a message to users.
        /// </summary>
        /// <param name="messageToSend">The URL to be sent to the chat.</param>
        public static async void SendMessage(string messageToSend) {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "";

            // Values from app registration
            var clientId = "";
            var clientSecret = "";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var chatMessage = new ChatMessage
            {
                Body = new ItemBody
                {
                    Content = messageToSend,
                },
            };

            // https://teams.microsoft.com/l/channel/19%3aQgf1lnOXPh8QbhTwll-G1cdc8zx1WTykpIiAYhQpSGo1%40thread.tacv2/General?groupId=1b304f62-f323-4721-b5cd-6faaf6e57d4b&tenantId=a41e10c9-a609-4413-bcbc-72c10363381b
            // https://teams.microsoft.com/l/team/19%3azmpzohwUQHsvNUASJGhZ_POG93JO1OXXSYdZoZmYdVw1%40thread.tacv2/conversations?groupId=2849f7bc-b45f-48d7-a738-f2c4e53928a6&tenantId=a41e10c9-a609-4413-bcbc-72c10363381b
            // https://teams.microsoft.com/l/team/19%3azmpzohwUQHsvNUASJGhZ_POG93JO1OXXSYdZoZmYdVw1%40thread.tacv2/conversations?groupId=2849f7bc-b45f-48d7-a738-f2c4e53928a6&tenantId=a41e10c9-a609-4413-bcbc-72c10363381b
            // https://teams.microsoft.com/l/meetup-join/19%3AzmpzohwUQHsvNUASJGhZ_POG93JO1OXXSYdZoZmYdVw1%40thread.tacv2/1649602123834?context=%7B%22Tid%22%3A%22a41e10c9-a609-4413-bcbc-72c10363381b%22%2C%22Oid%22%3A%22e4bde442-4ff4-47ad-be34-5540d94ac21c%22%7D
            await graphClient.Teams["2849f7bc-b45f-48d7-a738-f2c4e53928a6"].Channels["19:zmpzohwUQHsvNUASJGhZ_POG93JO1OXXSYdZoZmYdVw1@thread.tacv2"].Messages.Request().AddResponseAsync(chatMessage);
        }
    }
}
