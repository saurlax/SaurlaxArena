using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;

namespace SaurlaxArena;

public class SaurlaxArena : BasePlugin
{
  public override string ModuleAuthor => "Saurlax";
  public override string ModuleDescription => "A simple practice arena plugin for cs2.";
  public override string ModuleName => "SaurlaxArena";
  public override string ModuleVersion => "0.0.1";


  readonly Dictionary<string, Action<CCSPlayerController, string[]>> commands = new()
  {
    {"sa", Commands.SA},
    {"bot_kick", Commands.BotKick},
    {"bot_t", Commands.BotT},
    {"bot_ct", Commands.BotCT}
  };

  [GameEventHandler]
  public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
  {
    var player = @event.Userid;
    if (player.IsValid && !player.IsBot)
    {
      player.PrintFormatToChat("Use .sa to see available commands.");
    }
    return HookResult.Continue;
  }

  [GameEventHandler]
  public HookResult OnPlayerChat(EventPlayerChat @event, GameEventInfo info)
  {
    var player = Utilities.GetPlayerFromUserid(@event.Userid);
    var text = @event.Text.Trim();
    if (text.StartsWith('!') || text.StartsWith('.'))
    {
      var args = text[1..].ToLower().Split(" ");
      var cmd = args[0];
      var para = args[1..];
      if (commands.TryGetValue(cmd, out Action<CCSPlayerController, string[]>? value)) value(player, para);
    }
    return HookResult.Continue;
  }
}
