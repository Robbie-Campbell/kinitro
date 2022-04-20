// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.TeamsBot
{
    using System;
    using System.Collections.Generic;
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
            this.TimeSpoken = 0;
            this.TimeInMeeting = new Stopwatch();
            this.ParticipantName = string.Empty;
            this.NumberOfTimesSpoken = 0;
            this.MeetingTotalTimeSpoken = 0;
            this.MeetingTotalTimeInMeeting = 0;
            this.IsSpeaking = 0;
            this.MeetingId = string.Empty;
        }

        /// <summary>
        /// Gets or sets time spent speaking.
        /// </summary>
        public int TimeSpoken { get; set; }

        /// <summary>
        /// Gets or sets meeting ID.
        /// </summary>
        public string MeetingId { get; set; }

        /// <summary>
        /// Gets or sets time spent in meeting.
        /// </summary>
        public Stopwatch TimeInMeeting { get; set; }

        /// <summary>
        /// Gets or sets the average time spent speaking in a meeting.
        /// </summary>
        public double MeetingTotalTimeSpoken { get; set; }

        /// <summary>
        /// Gets or sets the average time spent speaking in a meeting.
        /// </summary>
        public double MeetingTotalTimeInMeeting { get; set; }

        /// <summary>
        /// Gets or sets the id of the participant.
        /// </summary>
        public string ParticipantName { get; set; }

        /// <summary>
        /// Gets or sets the number of times spoken in the meeting.
        /// </summary>
        public int NumberOfTimesSpoken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the participant is currently speaking.
        /// </summary>
        public int IsSpeaking { get; set; }

        /// <summary>
        /// Establshes the average amount of time spoken for the meeting.
        /// </summary>
        /// <param name="participants">All of the meeting participants.</param>
        public void SetMeetingAverageForTimeSpeaking(Dictionary<string, StaticParticipant> participants)
        {
            if (participants == null)
            {
                throw new ArgumentNullException(nameof(participants));
            }

            long totalMeetingTime = 0;
            long totalTimeSpeaking = 0;
            foreach (StaticParticipant participant in participants.Values)
            {
                totalMeetingTime += participant.TimeInMeeting.ElapsedMilliseconds;
                totalTimeSpeaking += participant.TimeSpoken;
            }

            this.MeetingTotalTimeInMeeting = totalMeetingTime;
            this.MeetingTotalTimeSpoken = totalTimeSpeaking;
        }
    }
}
