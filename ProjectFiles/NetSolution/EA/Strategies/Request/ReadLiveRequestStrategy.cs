﻿// Copyright © 2022 Rockwell Automation, Inc. Generated on: 9/14/2022 12:18:31 PM Generated by: RA-INT\NHender
using Cca.Cgp.Common.Model;
using Cca.Cgp.Core.Base;
using Cca.Cgp.Core.Base.Extensions;
using Cca.Cgp.Core.Base.Ia;
using Cca.Cgp.Core.Base.Interfaces;
using Cca.Extensions.Common;
using Cca.Extensions.Common.Logging;
using Serilog;
using NetZero.Extensions;

namespace NetZero.EA.Strategies.Request
{
    public class ReadLiveRequestStrategy : RequestStrategy
    {
        #region Private Fields

        private static readonly ILogger s_Log = Log.ForContext<ReadLiveRequestStrategy>();

        #endregion Private Fields

        #region Public Constructors

        public ReadLiveRequestStrategy(IRequestHandler requestHandler)
            : base(requestHandler)
        {
            // change priority according to your needs
            Priority = 1;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// handles an individual request
        /// </summary>
        /// <param name="request"><see cref="DataItemRequest"/> to process</param>
        public override void HandleMessage(DataItemRequest request)
        {
            if (s_Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
            {
                s_Log.Here().Debug("{id}", request.id);
            }

            var id = request.id;
            var node = FqnLookup.GetNodeFromId(id);

            // this handles messages one by one
            //var vqt = Cache.ContainsID(request.id) ? Cache.GetLatestVqt(request.id) : null;
            if (node is { })
            {
                //Log.Info($"{nameof(HandleMessage)}: found node: {node.FullyQualifiedName} : {node.Id}");
                var vqt = node.GetCurrentVqt();
                if (vqt is { })
                {
                    // send the last cached value
                    RequestHandler.QueueResponse(request.GetReactionFromVqt(vqt, vqt.GetMimeType()));
                }
                else
                {
                    // send an error response
                    RequestHandler.QueueResponse(request.GetErrorReaction($"No cached values for {request.id}"));
                }
            }
            else
            {
                // send an error response
                RequestHandler.QueueResponse(request.GetErrorReaction($"No node for {request.id}"));
            }
        }

        /// <summary>
        /// determines if this strategy should handled a given request based on the properties of
        /// the request object
        /// </summary>
        /// <param name="request"><see cref="DataItemRequest"/> to check</param>
        /// <returns>True if this strategy handles this request</returns>
        public override bool Handles(DataItemRequest request)
        {
            // do we have the necessary information to process this request?
            if (request is null || string.IsNullOrEmpty(request.contextPrefix) || string.IsNullOrEmpty(request.id))
            {
                return false;
            }

            // we should return true if this strategy handles this request
            return request.actionType.Equals(ActionType.Read);
        }

        #endregion Public Methods
    }
}
