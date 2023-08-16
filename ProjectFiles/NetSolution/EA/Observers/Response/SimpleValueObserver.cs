﻿// Copyright © 2022 Rockwell Automation, Inc.
// Generated on: 9/14/2022 12:18:31 PM
// Generated by: RA-INT\NHender
//

using Cca.Cgp.Core.Base;
using Cca.Cgp.Core.Base.Observers;

using Serilog;

namespace NetZero.EA.Observers.Response
{
    /// <summary>
    /// simple observer for tag values
    /// </summary>
    public class SimpleValueObserver : ResponseObserver
    {
        #region Private Fields

        private static readonly ILogger s_Log = Log.ForContext<SimpleValueObserver>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Default constructor that observes all values for all tagIds
        /// </summary>
        public SimpleValueObserver() : base()
        {
            ReactionType = Cca.Cgp.Core.Base.Ia.ReactionType.Value;

            if (s_Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
            {
                s_Log.Debug("{method}", nameof(SimpleValueObserver));
            }
        }

        /// <summary>
        /// Constructor to observe a specific tagId
        /// </summary>
        /// <param name="tagId">TagID to observer. This will check matches against the response.id field</param>
        public SimpleValueObserver(string tagId) : base()
        {
            ReactionType = Cca.Cgp.Core.Base.Ia.ReactionType.Value;
            IdMatch = tagId;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// called for new responses that match our criteria
        /// </summary>
        /// <param name="response"></param>
        public override void OnNext(DataItemResponse response)
        {
            // handle this new response
            if (s_Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
            {
                var vqt = response.vqts[0];
                s_Log.Debug("{name}: {method}: {id,-25}: v:{v,-15} t:{t,-31} q:{q}", Name, nameof(OnNext), response.id, vqt.v, vqt.t, vqt.q);
            }
        }

        #endregion Public Methods
    }
}
