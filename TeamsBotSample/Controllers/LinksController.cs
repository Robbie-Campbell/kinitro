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
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        /// <summary>
        /// Gets the data by request of a given participant.
        /// </summary>
        /// <returns>The Participants Data.</returns>
        [HttpGet("getLinks")]
        public static Dictionary<string, LinkData> GetAllLinkData() {
            return Measurer.GetAllLinkData();
        }
    }
}
