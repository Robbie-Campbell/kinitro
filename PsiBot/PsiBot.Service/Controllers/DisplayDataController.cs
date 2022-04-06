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
    public class DisplayDataController : ControllerBase
    {
        /// <summary>
        /// Gets the data by request of a given participant.
        /// </summary>
        /// <param name="id">The id of the participant.</param>
        /// <returns>The Participants Data.</returns>
        [HttpGet("{id}")]
        public StaticParticipant GetParticipantData(string id)
        {
            return Measurer.GetParticipantDataByID(id);
        }

        /// <summary>
        /// Gets data for testing in the api.
        /// </summary>
        /// <param name="id">The id requested.</param>
        /// <returns>A single participant.</returns>
        [HttpGet("test/{id}")]

        public StaticParticipant GetTestData(string id) {
            Dictionary<string, StaticParticipant> staticParticipants = new Dictionary<string, StaticParticipant>();
            staticParticipants.Add("1234", new StaticParticipant());
            staticParticipants["1234"].TimeInMeeting.Start();
            staticParticipants["1234"].TimeSpoken.Start();
            staticParticipants.Add("12345", new StaticParticipant());
            staticParticipants["12345"].TimeInMeeting.Start();
            staticParticipants["12345"].TimeSpoken.Start();
            return staticParticipants[id];
        }
    }
}
