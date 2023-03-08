using Terraria;
using Terraria.ModLoader;

namespace EndlessEscapade.Content.Seamap
{
    class TestSeamapCommand : ModCommand // alternative to DevConch
    {
        public override string Command => "eeseamap";
        public override CommandType Type => CommandType.Chat;
        public override void Action(CommandCaller caller, string input, string[] args) {
            if (args.Length > 0) {
                if (args[0] == "enter") {
                    SeamapHandler.EnterSeamap();
                }
                else if (args[0] == "exit") {
                    SeamapHandler.ExitSeamap();
                }
                else if (args[0] == "test") {
                    Main.gameMenu = true;
                }
            }
        }
    }
}
