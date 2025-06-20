using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib;
using CriticalCommonLib.Services;
using CriticalCommonLib.Services.Mediator;
using DalaMock.Host.Mediator;
using Dalamud.Plugin.Services;
using InventoryTools.Mediator;
using InventoryTools.Ui;
using Microsoft.Extensions.Logging;
using OtterGui.Classes;

namespace InventoryTools.Hotkeys;

public class OpenFishingLogHotkey : Hotkey
{
    private readonly ItemSheet _itemSheet;
    private readonly IGameInterface _gameInterface;
    private readonly IGameGui _gameGui;

    public OpenFishingLogHotkey(ILogger<OpenFishingLogHotkey> logger, MediatorService mediatorService, ItemSheet itemSheet, InventoryToolsConfiguration configuration, IGameInterface gameInterface, IGameGui gameGui) : base(logger, mediatorService, configuration)
    {
        _itemSheet = itemSheet;
        _gameInterface = gameInterface;
        _gameGui = gameGui;
    }
    public override ModifiableHotkey? ModifiableHotkey => Configuration.OpenFishingLogHotKey;

    public override bool OnHotKey()
    {
        var id = _gameGui.HoveredItem;
        if (id >= 2000000 || id == 0) return false;
        id %= 500000;
        var item = _itemSheet.GetRowOrDefault((uint) id);
        if (item == null || !item.CanOpenFishingLog) return false;
        _gameInterface.OpenFishingLog(item.RowId, item.ObtainedSpearFishing);
        return true;
    }
}