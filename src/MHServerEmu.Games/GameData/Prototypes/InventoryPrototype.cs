﻿using MHServerEmu.Games.Entities.Inventories;
using MHServerEmu.Games.Loot;

namespace MHServerEmu.Games.GameData.Prototypes
{
    public class InventoryPrototype : Prototype
    {
        public short Capacity { get; protected set; }
        public PrototypeId[] EntityTypeFilter { get; protected set; }
        public bool ExitWorldOnAdd { get; protected set; }
        public bool ExitWorldOnRemove { get; protected set; }
        public bool PersistedToDatabase { get; protected set; }
        public bool OnPersonLocation { get; protected set; }
        public bool NotifyUI { get; protected set; }
        public short CollectionSortOrder { get; protected set; }
        public bool VisibleToOwner { get; protected set; }
        public bool VisibleToTrader { get; protected set; }
        public bool VisibleToParty { get; protected set; }
        public bool VisibleToProximity { get; protected set; }
        public bool AvatarTeam { get; protected set; }
        public InventoryConvenienceLabel ConvenienceLabel { get; protected set; }
        public bool PlaySoundOnAdd { get; protected set; }
        public bool CapacityUnlimited { get; protected set; }
        public bool VendorInvContentsCanBeBought { get; protected set; }
        public bool ContentsRecoverFromError { get; protected set; }
        public int DestroyContainedAfterSecs { get; protected set; }
        public InventoryEvent DestroyContainedOnEvent { get; protected set; }
        public InventoryCategory Category { get; protected set; }
        public OfferingInventoryUIDataPrototype OfferingInventoryUIData { get; protected set; }
        public bool LockedByDefault { get; protected set; }
        public bool ReplicateForTransfer { get; protected set; }
        public PrototypeId[] ItemSortPreferences { get; protected set; }
        public InventoryUIDataPrototype UIData { get; protected set; }
        public PrototypeId[] SoftCapacitySlotGroupsPC { get; protected set; }       // VectorPrototypeRefPtr InventoryExtraSlotsGroupPrototype
        public int SoftCapacityDefaultSlotsPC { get; protected set; }
        public PrototypeId[] SoftCapacitySlotGroupsConsole { get; protected set; }  // VectorPrototypeRefPtr InventoryExtraSlotsGroupPrototype
        public int SoftCapacityDefaultSlotsConsole { get; protected set; }
        public LocaleStringId DisplayName { get; protected set; }

        /// <summary>
        /// Returns <see langword="true"/> is this <see cref="InventoryPrototype"/> is for a player stash inventory.
        /// </summary>
        public bool IsPlayerStashInventory()
        {
            return Category == InventoryCategory.PlayerStashAvatarSpecific || Category == InventoryCategory.PlayerStashGeneral;
        }

        /// <summary>
        /// Returns <see langword="true"/> is entities that use the provided <see cref="EntityPrototype"/> are allowed to be stored in inventories that use this prototype.
        /// </summary>
        public bool AllowEntity(EntityPrototype entityPrototype)
        {
            if (EntityTypeFilter == null || EntityTypeFilter.Length == 0) return false;

            foreach (PrototypeId entityTypeRef in EntityTypeFilter)
            {
                BlueprintId entityTypeBlueprintRef = GameDatabase.DataDirectory.GetPrototypeBlueprintDataRef(entityTypeRef);
                if (GameDatabase.DataDirectory.PrototypeIsChildOfBlueprint(entityPrototype.DataRef, entityTypeBlueprintRef))
                    return true;
            }

            return false;
        }
    }

    public class InventoryExtraSlotsGroupPrototype : Prototype
    {
        public int MaxExtraSlotCount { get; protected set; }
    }

    public class PlayerStashInventoryPrototype : InventoryPrototype
    {
        public PrototypeId ForAvatar { get; protected set; }
        public AssetId IconPath { get; protected set; }
        public LocaleStringId FulfillmentName { get; protected set; }
        public AssetId[] StashTabCustomIcons { get; protected set; }
    }

    public class EntityInventoryAssignmentPrototype : Prototype
    {
        public PrototypeId Inventory { get; protected set; }
        public PrototypeId LootTable { get; protected set; }
    }

    public class AvatarEquipInventoryAssignmentPrototype : EntityInventoryAssignmentPrototype
    {
        public EquipmentInvUISlot UISlot { get; protected set; }
        public int UnlocksAtCharacterLevel { get; protected set; }
        public PrototypeId UIData { get; protected set; }
    }

    public class InventoryExtraSlotsGrantPrototype : Prototype
    {
        public LocaleStringId DisplayName { get; protected set; }
        public int GrantSlotCount { get; protected set; }
        public PrototypeId SlotGroup { get; protected set; }
    }
}
