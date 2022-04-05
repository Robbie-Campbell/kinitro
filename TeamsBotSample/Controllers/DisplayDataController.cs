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
    public class DisplayDataController : ControllerBase
    {
        /// <summary>
        /// Gets the data by request of a given participant.
        /// </summary>
        /// <param name="id">The id of the participant.</param>
        /// <returns>The Participants Data.</returns>
        [HttpGet("{id}")]
        public static StaticParticipant GetParticipantData(string id)
        {
            return Measurer.GetParticipantDataByID(id);
        }
    }
}
