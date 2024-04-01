using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;

namespace SaurlaxArena;

public static class Commands
{
  public static void PrintFormatToChat(this CCSPlayerController player, string message)
  {
    player.PrintToChat($"[{ChatColors.Green}SaurlaxArena{ChatColors.Default}] {message}");
  }
  static void PrintFormatToChatAll(string message)
  {
    Server.PrintToChatAll($"[{ChatColors.Green}SaurlaxArena{ChatColors.Default}] {message}");
  }
  public static void SA(this CCSPlayerController player, string[] para)
  {
    Dictionary<string, string> commands = new()
    {
      { "bot_kick", "Kicks all bots."},
      { "bot_t [number]", "Adds bots to T side."},
      { "bot_ct [number]", "Adds bots to CT side."}
    };
    player.PrintFormatToChat("Available commands:");
    foreach (var (cmd, desc) in commands)
    {
      player.PrintFormatToChat($"{cmd}: {ChatColors.Grey}{desc}");
    }
  }
  public static void BotKick(this CCSPlayerController player, string[] para)
  {
    Server.ExecuteCommand("bot_kick");
    PrintFormatToChatAll("Kicked all bots.");
  }
  public static void BotT(this CCSPlayerController player, string[] para)
  {
    if (para.Length == 0) para = ["1"];
    if (!int.TryParse(para[0], out var n))
    {
      n = 1;
      player.PrintFormatToChat($"Cant parse {para[0]} to int, defaulting to 1.");
    }
    for (var i = 0; i < n; i++) Server.ExecuteCommand("bot_add_t");
    PrintFormatToChatAll($"Added {n} bots to T side.");
  }
  public static void BotCT(this CCSPlayerController player, string[] para)
  {
    if (para.Length == 0) para = ["1"];
    if (!int.TryParse(para[0], out var n))
    {
      n = 1;
      player.PrintFormatToChat($"Cant parse {para[0]} to int, defaulting to 1.");
    }
    for (var i = 0; i < n; i++) Server.ExecuteCommand("bot_add_ct");
    PrintFormatToChatAll($"Added {n} bots to CT side.");
  }
}