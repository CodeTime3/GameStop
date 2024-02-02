using GameLibrary;

namespace GameStopTestSuite
{
    public class GameUnitTest
    {
        [Theory]
        [InlineData("Assassin's Creed III", "ASSASSINS-CREED-III")]
        [InlineData("Baldur's Gate 3", "BALDURS-GATE-3")]
        [InlineData("Skyrim mod Multiplayer", "SKYRIM-MOD-MULTIPLAYER")]
        [InlineData("The legend of Zelda - Ocarina of time", "THE-LEGEND-OF-ZELDA-OCARINA-OF-TIME")]
        [InlineData(" God of War Ragnarök! " , "GOD-OF-WAR-RAGNAROK")]
        [InlineData("-Baldur's Gate 3-", "BALDURS-GATE-3")]
        public void CreateGameID_should_work(string gameName, string expected)
        {
           string gameID = GameUtilities.StringTransform(gameName);
            
            Assert.Equal(expected, gameID);
        }
    }
}