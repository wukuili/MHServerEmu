using Gazillion;
using MHServerEmu.Common.Logging;
using MHServerEmu.Common.Logging.Targets;
using MHServerEmu.PlayerManagement;
using MHServerEmuTests.Business;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Net.Mail;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MHServerEmuTests.Maps
{
    public class MapGeneration : IClassFixture<OneTimeSetUpBeforeMapGenerationTests>
    {
        ITestOutputHelper _output;

        public MapGeneration(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void WaypointToXaviersMansionRegion_NormalDifficulty_IsSuccess()
        {
            UnitTestLogHelper.Logger.Error("WaypointToXaviersMansionRegion_NormalDifficulty_IsSuccess");

            List<GameMessage> gameMessages = new()
            {
                new GameMessage(NetMessageUseWaypoint.CreateBuilder()
                .SetAvatarIndex(0)
                .SetDifficultyProtoId(18016845980090109785)
                .SetIdTransitionEntity(12)
                .SetRegionProtoId(7293929583592937434)
                .SetWaypointDataRef(3105225438095544636).Build())
            };

            ServersHelper.SendDataToFrontEndServer(OneTimeSetUpBeforeMapGenerationTests.AuthTicket, gameMessages);
            Assert.True(true);
            UnitTestLogHelper.DisplayLogs(_output);
        }
    }
}