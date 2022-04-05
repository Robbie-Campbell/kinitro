// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.TeamsBot
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// This maintains the persistant participant data.
    /// </summary>
    public class StaticParticipant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticParticipant"/> class.
        /// </summary>
        public StaticParticipant()
        {
            this.TimeSpoken = new Stopwatch();
            this.TimeInMeeting = new Stopwatch();
        }

        /// <summary>
        /// Gets or sets time spent speaking.
        /// </summary>
        public Stopwatch TimeSpoken { get; set; }

        /// <summary>
        /// Gets or sets time spent in meeting.
        /// </summary>
        public Stopwatch TimeInMeeting { get; set; }
    }
}
