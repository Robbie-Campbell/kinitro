// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace PsiBot.Services.Controllers
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Psi.TeamsBot;

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
        [HttpGet("getlinks/{name}")]
        public Dictionary<string, LinkData> GetAllLinkData(string name) {

            return Measurer.GetAllLinkData(name);
        }
    }
}
