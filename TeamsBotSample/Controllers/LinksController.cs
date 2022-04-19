// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.TeamsBot
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Generates a URL for the display of user participation data.
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        /// <summary>
        /// Gets the data by request of a given participant.
        /// </summary>
        /// <param name="name">Name of the Participant.</param>
        /// <returns>The Participants Data.</returns>
        [HttpGet("getlinks")]
        public static Dictionary<string, LinkData> GetAllLinkData(string name)
        {
            return Measurer.GetAllLinkData(name);
        }
    }
}
