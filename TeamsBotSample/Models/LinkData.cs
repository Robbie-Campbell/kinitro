// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
namespace Microsoft.Psi.TeamsBot
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Stores the data neccessary for users to look at their participation data.
    /// </summary>
    public class LinkData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkData"/> class.
        /// </summary>
        public LinkData()
        {
            this.Name = null;
            this.Link = null;
        }

        /// <summary>
        /// Gets or sets the name of a participant.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the link for  participant to view.
        /// </summary>
        public string Link { get; set; }
    }
}
