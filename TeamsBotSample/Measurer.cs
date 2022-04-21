// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

// One of the frame ID's: e4bde442-4ff4-47ad-be34-5540d94ac21c.
namespace Microsoft.Psi.TeamsBot
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    // using SendMessageToUsers;
    using PsiImage = Microsoft.Psi.Imaging.Image;

    /// <summary>
    /// Represents a participant engagement component base class.
    /// </summary>
    public class Measurer : ParticipantEngagementBotBase
    {
        private static Dictionary<string, StaticParticipant> staticParticipants = new Dictionary<string, StaticParticipant>();
        private static Dictionary<string, LinkData> linkData = new Dictionary<string, LinkData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurer"/> class.
        /// </summary>
        /// <param name="pipeline">The pipeline to add the component to.</param>
        /// <param name="interval">Interval at which to render and emit frames of the rendered visual.</param>
        /// <param name="screenWidth">Width at which to render the shared screen.</param>
        /// <param name="screenHeight">Height at which to render the shared screen.</param>
        public Measurer(Pipeline pipeline, TimeSpan interval, int screenWidth, int screenHeight)
            : base(pipeline, interval, screenWidth, screenHeight)
        {
        }

        private static Dictionary<string, StaticParticipant> StaticParticipants { get => staticParticipants; }

        /*
        public static void CreateParticipant(string id, string name)
        {
            StaticParticipants.Add(id, new StaticParticipant());
            StaticParticipants[id].TimeInMeeting.Start();
            string webLink =  + id;
            linkData.Add(id, new LinkData());
            linkData[id].Link = webLink;
            staticParticipants[id].ParticipantName = name;
            linkData[id].Name = name;
        }
        */

        /// <summary>
        /// Updates the name of a participant.
        /// </summary>
        /// <param name="id">The id of the participant.</param>
        /// <param name="name">The name of the participant.</param>
        public static void UpdateParticipantName(string id, string name)
        {
            if (staticParticipants.ContainsKey(id))
            {
                staticParticipants[id].ParticipantName = name;
                linkData[id].Name = name;
            }
        }

        /// <summary>
        /// Gets the participant data by ID.
        /// </summary>
        /// <param name="id">The id of the participant.</param>
        /// <returns>The participant and their data.</returns>
        public static StaticParticipant GetParticipantDataByID(string id)
        {
            if (StaticParticipants.ContainsKey(id))
            {
                return StaticParticipants[id];
            }

            return null;
        }

        /// <summary>
        /// Gets all link data for api or null value.
        /// </summary>
        /// <param name="name">The name of the participant.</param>
        /// <returns>Link data or null.</returns>
        public static Dictionary<string, LinkData> GetAllLinkData(string name)
        {
            return linkData.Where(i => i.Value.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture)
            .Contains(name.ToLower(System.Globalization.CultureInfo.CurrentCulture))).ToDictionary(i => i.Key, i => i.Value);
        }

        /// <summary>
        /// Clears both dictionaries on bot leaving.
        /// </summary>
        public static void ClearDictionaries()
        {
            linkData.Clear();
            staticParticipants.Clear();
        }

        /// <inheritdoc />
        protected override IEnumerable<Participant> UpdateModel(Dictionary<string, Shared<PsiImage>> video, Dictionary<string, List<DateTime>> speech, DateTime originatingTime)
        {
            if (video == null)
            {
                throw new ArgumentNullException(nameof(video));
            }

            if (speech == null)
            {
                throw new ArgumentNullException(nameof(speech));
            }

            var num = video.Count;
            var theta = num % 2 == 0 ? Math.PI / 2 : Math.PI;

            var participants = new Dictionary<string, Participant>();
            foreach (var frame in video)
            {
                participants.Add(frame.Key, new Participant(frame.Value, Math.Sin(theta), Math.Cos(theta), ThumbnailWindowScale, ThumbnailWindowScale));
                if (!StaticParticipants.ContainsKey(frame.Key))
                {
                    StaticParticipants.Add(frame.Key, new StaticParticipant());
                    StaticParticipants[frame.Key].TimeInMeeting.Start();
                    string webLink = "https://participation-measurer-rr56qis6kq-nw.a.run.app/data/" + frame.Key;
                    linkData.Add(frame.Key, new LinkData());
                    linkData[frame.Key].Link = webLink;
                }
            }

            // Checks if anyone is speaking
            bool anyoneIsSpeaking = speech.Sum(x => x.Value.Count) > 0;
            if (anyoneIsSpeaking)
            {
                foreach (var s in speech)
                {
                    if (participants.TryGetValue(s.Key, out Participant p))
                    {
                        StaticParticipant currentParticipant = StaticParticipants[s.Key];

                        // Gets whether the participant is speaking
                        bool isSpeaking = s.Value.Select(x => s.Key).Count() > 3;
                        if (isSpeaking)
                        {
                            currentParticipant.IsSpeaking++;
                            if (currentParticipant.IsSpeaking == 2)
                            {
                                currentParticipant.NumberOfTimesSpoken++;
                            }

                            currentParticipant.TimeSpoken += 40;
                            currentParticipant.SetMeetingAverageForTimeSpeaking(StaticParticipants);
                            linkData[s.Key].Link = linkData[s.Key].Link;
                        }
                        else
                        {
                            currentParticipant.IsSpeaking = 0;
                        }
                    }
                }
            }

            return participants.Values;
        }

        /// <inheritdoc />
        protected override void UpdateVisual(IEnumerable<Participant> participants, Graphics graphics)
        {
            if (participants == null)
            {
                throw new ArgumentNullException(nameof(participants));
            }

            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
        }
    }
}
