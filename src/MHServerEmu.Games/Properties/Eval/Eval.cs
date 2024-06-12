﻿using MHServerEmu.Core.Collisions;
using MHServerEmu.Core.Extensions;
using MHServerEmu.Core.Helpers;
using MHServerEmu.Core.Logging;
using MHServerEmu.Games.Entities;
using MHServerEmu.Games.Entities.Avatars;
using MHServerEmu.Games.Entities.Inventories;
using MHServerEmu.Games.GameData;
using MHServerEmu.Games.GameData.Calligraphy;
using MHServerEmu.Games.GameData.Prototypes;
using MHServerEmu.Games.Missions;
using MHServerEmu.Games.Powers;
using MHServerEmu.Games.Regions;

namespace MHServerEmu.Games.Properties.Eval
{
    public class Eval
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public static int RunInt(EvalPrototype evalProto, EvalContextData data)
        {
            Run(evalProto, data, out int retVal);
            return retVal;
        }

        public static bool Run(EvalPrototype evalProto, EvalContextData data, out int resultVal)
        {
            EvalVar evalVar = Run(evalProto, data);
            if (FromValue(evalVar, out resultVal) == false)
            {
                Logger.Warn($"Invalid return type [{evalVar.Type}]");
                if (evalProto != null)
                    Logger.Warn($"for operator [{evalProto.Op}] EvalPrototype=[{evalProto.GetType().Name}] ExpressionString=[{evalProto.ExpressionString()}] Path=[{evalProto}]");
                return false;
            }

            return true;
        }

        public static long RunLong(EvalPrototype evalProto, EvalContextData data)
        {
            Run(evalProto, data, out long retVal);
            return retVal;
        }

        public static bool Run(EvalPrototype evalProto, EvalContextData data, out long resultVal)
        {
            EvalVar evalVar = Run(evalProto, data);
            if (FromValue(evalVar, out resultVal) == false)
            {
                Logger.Warn($"Invalid return type [{evalVar.Type}]");
                if (evalProto != null)
                    Logger.Warn($"for operator [{evalProto.Op}] EvalPrototype=[{evalProto.GetType().Name}] ExpressionString=[{evalProto.ExpressionString()}] Path=[{evalProto}]");
                return false;
            }

            return true;
        }

        public static float RunFloat(EvalPrototype evalProto, EvalContextData data)
        {
            Run(evalProto, data, out float retVal);
            return retVal;
        }

        public static bool Run(EvalPrototype evalProto, EvalContextData data, out float resultVal)
        {
            EvalVar evalVar = Run(evalProto, data);
            if (FromValue(evalVar, out resultVal) == false)
            {
                Logger.Warn($"Invalid return type [{evalVar.Type}]");
                if (evalProto != null)
                    Logger.Warn($"for operator [{evalProto.Op}] EvalPrototype=[{evalProto.GetType().Name}] ExpressionString=[{evalProto.ExpressionString()}] Path=[{evalProto}]");
                return false;
            }

            return true;
        }

        public static bool RunBool(EvalPrototype evalProto, EvalContextData data)
        {
            Run(evalProto, data, out bool retVal);
            return retVal;
        }

        public static bool Run(EvalPrototype evalProto, EvalContextData data, out bool resultVal)
        {
            EvalVar evalVar = Run(evalProto, data);
            if (FromValue(evalVar, out resultVal) == false)
            {
                Logger.Warn($"Invalid return type [{evalVar.Type}]");
                if (evalProto != null)
                    Logger.Warn($"for operator [{evalProto.Op}] EvalPrototype=[{evalProto.GetType().Name}] ExpressionString=[{evalProto.ExpressionString()}] Path=[{evalProto}]");
                return false;
            }

            return true;
        }

        public static PrototypeId RunPrototypeId(EvalPrototype evalProto, EvalContextData data)
        {
            Run(evalProto, data, out PrototypeId retVal);
            return retVal;
        }

        public static bool Run(EvalPrototype evalProto, EvalContextData data, out PrototypeId resultVal)
        {
            EvalVar evalVar = Run(evalProto, data);
            if (FromValue(evalVar, out resultVal) == false)
            {
                Logger.Warn($"Invalid return type [{evalVar.Type}]");
                if (evalProto != null)
                    Logger.Warn($"for operator [{evalProto.Op}] EvalPrototype=[{evalProto.GetType().Name}] ExpressionString=[{evalProto.ExpressionString()}] Path=[{evalProto}]");
                return false;
            }

            return true;
        }

        public static bool FromValue(EvalVar evalVar, out int resultVal)
        {
            switch (evalVar.Type)
            {
                case EvalReturnType.Int:
                    resultVal = (int)evalVar.Value.Int;
                    return true;
                case EvalReturnType.Float:
                    resultVal = (int)evalVar.Value.Float;
                    return true;
            }
            resultVal = 0;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out long resultVal)
        {
            switch (evalVar.Type)
            {
                case EvalReturnType.Int:
                    resultVal = evalVar.Value.Int;
                    return true;
                case EvalReturnType.Float:
                    resultVal = (long)evalVar.Value.Float;
                    return true;
            }
            resultVal = 0;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out float resultVal)
        {
            switch (evalVar.Type)
            {
                case EvalReturnType.Int:
                    resultVal = evalVar.Value.Int;
                    return true;
                case EvalReturnType.Float:
                    resultVal = evalVar.Value.Float;
                    return true;
            }
            resultVal = 0.0f;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out bool resultVal)
        {
            switch (evalVar.Type)
            {
                case EvalReturnType.Int:
                    resultVal = evalVar.Value.Int > 0;
                    return true;
                case EvalReturnType.Float:
                    resultVal = evalVar.Value.Float > 0.0f;
                    return true;
                case EvalReturnType.Bool:
                case EvalReturnType.Undefined:
                    resultVal = evalVar.Value.Bool;
                    return true;
            }
            resultVal = false;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out ulong resultVal)
        {
            if (evalVar.Type == EvalReturnType.EntityId)
            {
                resultVal = evalVar.Value.EntityId;
                return true;
            }
            resultVal = Entity.InvalidId;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out PrototypeId resultVal)
        {
            if (evalVar.Type == EvalReturnType.ProtoRef)
            {
                resultVal = evalVar.Value.Proto;
                return true;
            }
            resultVal = PrototypeId.Invalid;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out AssetId resultVal)
        {
            if (evalVar.Type == EvalReturnType.AssetRef)
            {
                resultVal = evalVar.Value.AssetId;
                return true;
            }
            resultVal = AssetId.Invalid;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out PropertyId resultVal)
        {
            resultVal = new();
            if (evalVar.Type == EvalReturnType.PropertyId)
            {
                resultVal.Raw = evalVar.Value.PropId;
                return true;
            }
            resultVal = PropertyId.Invalid;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out Entity resultVal)
        {
            if (evalVar.Type == EvalReturnType.EntityPtr)
            {
                resultVal = evalVar.Value.Entity;
                return true;
            }
            resultVal = null;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out PropertyCollection resultVal, Game game)
        {
            switch (evalVar.Type)
            {
                case EvalReturnType.PropertyCollectionPtr:
                    resultVal = evalVar.Value.Props;
                    return true;
                case EvalReturnType.EntityPtr:
                    resultVal = evalVar.Value.Entity.Properties;
                    return true;
                case EvalReturnType.EntityGuid:
                    resultVal = null;
                    if (evalVar.Value.EntityGuid != 0 && game != null)
                    {
                        var entity = game.EntityManager.GetEntityByDbGuid<Entity>(evalVar.Value.EntityGuid);
                        if (entity != null) resultVal = entity.Properties;
                    }
                    return resultVal != null;
            }
            resultVal = null;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out ConditionCollection resultVal)
        {
            if (evalVar.Type == EvalReturnType.ConditionCollectionPtr)
            {
                resultVal = evalVar.Value.Conditions;
                return true;
            }
            resultVal = null;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out List<PrototypeId> resultVal)
        {
            if (evalVar.Type == EvalReturnType.ProtoRefListPtr)
            {
                resultVal = evalVar.Value.ProtoRefList;
                return true;
            }
            resultVal = null;
            return false;
        }

        public static bool FromValue(EvalVar evalVar, out PrototypeId[] resultVal)
        {
            if (evalVar.Type == EvalReturnType.ProtoRefVectorPtr)
            {
                resultVal = evalVar.Value.ProtoRefVector;
                return true;
            }
            resultVal = null;
            return false;
        }

        public static bool FromValue<T>(EvalVar evalVar, out T resultVal)
        {
            resultVal = default;
            bool result = false;
            Type targetType = typeof(T);
            if (targetType == typeof(int))
            {
                result = FromValue(evalVar, out int resultFrom);
                resultVal = (T)(object)resultFrom;
                return result;
            }
            else if (targetType == typeof(long))
            {
                result = FromValue(evalVar, out long resultFrom);
                resultVal = (T)(object)resultFrom;
                return result;
            }
            else if (targetType == typeof(float))
            {
                result = FromValue(evalVar, out float resultFrom);
                resultVal = (T)(object)resultFrom;
                return result;
            }
            else if (targetType == typeof(bool))
            {
                result = FromValue(evalVar, out bool resultFrom);
                resultVal = (T)(object)resultFrom;
                return result;
            }
            else if (targetType == typeof(PrototypeId))
            {
                result = FromValue(evalVar, out PrototypeId resultFrom);
                resultVal = (T)(object)resultFrom;
                return result;
            }            
            return result;
        }

        private static EvalVar Run(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto == null) return evalVar;

            return evalProto.Op switch
            {
                EvalOp.And => RunAnd(evalProto, data),
                EvalOp.Equals => RunEquals(evalProto, data),
                EvalOp.GreaterThan => RunGreaterThan(evalProto, data),
                EvalOp.IsContextDataNull => RunIsContextDataNull(evalProto, data),
                EvalOp.LessThan => RunLessThan(evalProto, data),
                EvalOp.DifficultyTierRange => RunDifficultyTierRange(evalProto, data),
                EvalOp.MissionIsActive => RunMissionIsActive(evalProto, data),
                EvalOp.MissionIsComplete => RunMissionIsComplete(evalProto, data),
                EvalOp.Not => RunNot(evalProto, data),
                EvalOp.Or => RunOr(evalProto, data),
                EvalOp.HasEntityInInventory => RunHasEntityInInventory(evalProto, data),
                EvalOp.LoadAssetRef => RunLoadAssetRef(evalProto, data),
                EvalOp.LoadBool => RunLoadBool(evalProto, data),
                EvalOp.LoadFloat => RunLoadFloat(evalProto, data),
                EvalOp.LoadInt => RunLoadInt(evalProto, data),
                EvalOp.LoadProtoRef => RunLoadProtoRef(evalProto, data),
                EvalOp.LoadContextInt => RunLoadContextInt(evalProto, data),
                EvalOp.LoadContextProtoRef => RunLoadContextProtoRef(evalProto, data),
                EvalOp.For => RunFor(evalProto, data),
                EvalOp.ForEachConditionInContext => RunForEachConditionInContext(evalProto, data),
                EvalOp.ForEachProtoRefInContextRefList => RunForEachProtoRefInContextRefList(evalProto, data),
                EvalOp.IfElse => RunIfElse(evalProto, data),
                EvalOp.Scope => RunScope(evalProto, data),
                EvalOp.ExportError => RunExportError(evalProto, data),
                EvalOp.LoadCurve => RunLoadCurve(evalProto, data),
                EvalOp.Add => RunAdd(evalProto, data),
                EvalOp.Div => RunDiv(evalProto, data),
                EvalOp.Exponent => RunExponent(evalProto, data),
                EvalOp.Max => RunMax(evalProto, data),
                EvalOp.Min => RunMin(evalProto, data),
                EvalOp.Modulus => RunModulus(evalProto, data),
                EvalOp.Mult => RunMult(evalProto, data),
                EvalOp.Sub => RunSub(evalProto, data),
                EvalOp.AssignProp => RunAssignProp(evalProto, data),
                EvalOp.AssignPropEvalParams => RunAssignPropEvalParams(evalProto, data),
                EvalOp.HasProp => RunHasProp(evalProto, data),
                EvalOp.LoadProp => RunLoadProp(evalProto, data),
                EvalOp.LoadPropContextParams => RunLoadPropContextParams(evalProto, data),
                EvalOp.LoadPropEvalParams => RunLoadPropEvalParams(evalProto, data),
                EvalOp.SwapProp => RunSwapProp(evalProto, data),
                EvalOp.RandomFloat => RunRandomFloat(evalProto, data),
                EvalOp.RandomInt => RunRandomInt(evalProto, data),
                EvalOp.LoadEntityToContextVar => RunLoadEntityToContextVar(evalProto, data),
                EvalOp.LoadConditionCollectionToContext => RunLoadConditionCollectionToContext(evalProto, data),
                EvalOp.EntityHasKeyword => RunEntityHasKeyword(evalProto, data),
                EvalOp.EntityHasTalent => RunEntityHasTalent(evalProto, data),
                EvalOp.GetCombatLevel => RunGetCombatLevel(evalProto, data),
                EvalOp.GetPowerRank => RunGetPowerRank(evalProto, data),
                EvalOp.CalcPowerRank => RunCalcPowerRank(evalProto, data),
                EvalOp.IsInParty => RunIsInParty(evalProto, data),
                EvalOp.GetDamageReductionPct => RunGetDamageReductionPct(evalProto, data),
                EvalOp.GetDistanceToEntity => RunGetDistanceToEntity(evalProto, data),
                EvalOp.IsDynamicCombatLevelEnabled => RunIsDynamicCombatLevelEnabled(evalProto, data),
                _ => Logger.WarnReturn(evalVar, "Invalid Operation"),
            };
        }

        private static EvalVar GetEvalVarFromContext(EvalContext context, EvalContextData data, bool writable, bool checkNull = true)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            bool readOnly;

            if (context < EvalContext.MaxVars)
            {
                evalVar = data.Vars[(int)context].Var;
                readOnly = data.Vars[(int)context].ReadOnly;
            }
            else if (context == EvalContext.CallerStack)
            {
                evalVar.SetPropertyCollectionPtr(data.CallerStackProperties);
                readOnly = false;
            }
            else if (context == EvalContext.LocalStack)
            {
                evalVar.SetPropertyCollectionPtr(data.LocalStackProperties);
                readOnly = false;
            }
            else if (context == EvalContext.Globals)
            {
                GlobalsPrototype globals = GameDatabase.GlobalsPrototype;
                GlobalPropertiesPrototype globalProperties = globals?.Properties;
                if (globalProperties == null || checkNull && globalProperties.Properties == null)
                    return Logger.WarnReturn(evalVar, "Failed to get globals prototype for eval with Globals context type.");
                evalVar.SetPropertyCollectionPtr(globalProperties.Properties);
                readOnly = true;
            }
            else
                return Logger.WarnReturn(evalVar, "Invalid Context");

            if (writable && readOnly)
            {
                evalVar.SetError();
                return Logger.WarnReturn(evalVar, $"Attempting to get a writable '{context}' from a context that has it set as read-only");
            }

            if (checkNull && evalVar.Type == EvalReturnType.PropertyCollectionPtr && evalVar.Value.Props == null)
            {
                evalVar.SetError();
                return Logger.WarnReturn(evalVar, $"Attempting to get '{context}' from a context that doesn't have it set");
            }

            return evalVar;
        }

        private static EvalVar RunAnd(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not AndPrototype andProto) return evalVar;
            if (andProto.Arg1 == null || andProto.Arg2 == null) return evalVar;

            EvalVar arg1 = Run(andProto.Arg1, data);
            if (arg1.Type != EvalReturnType.Bool)
                return Logger.WarnReturn(evalVar, "And: Non-Bool/Error field Arg1");

            if (arg1.Value.Bool)
            {
                EvalVar arg2 = Run(andProto.Arg2, data);
                if (arg2.Type != EvalReturnType.Bool)
                    return Logger.WarnReturn(evalVar, "Equals: Non-Bool/Error field Arg2");
                evalVar.SetBool(arg2.Value.Bool);
            }
            else
                evalVar.SetBool(false);

            return evalVar;
        }

        private static EvalVar RunEquals(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not EqualsPrototype equalsProto) return evalVar;

            EvalVar arg1 = Run(equalsProto.Arg1, data);
            if (arg1.Type == EvalReturnType.Error)
                return Logger.WarnReturn(evalVar, "Equals: Error field Arg1");

            EvalVar arg2 = Run(equalsProto.Arg2, data);
            if (arg2.Type == EvalReturnType.Error)
                return Logger.WarnReturn(evalVar, "Equals: Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(arg1.Value.Int == arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(Segment.EpsilonTest(arg1.Value.Int, arg2.Value.Float, equalsProto.Epsilon));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(Segment.EpsilonTest(arg1.Value.Float, arg2.Value.Int, equalsProto.Epsilon));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(Segment.EpsilonTest(arg1.Value.Float, arg2.Value.Float, equalsProto.Epsilon));
            else if (arg1.Type == EvalReturnType.ProtoRef && arg2.Type == EvalReturnType.ProtoRef)
                evalVar.SetBool(arg1.Value.Proto == arg2.Value.Proto);
            else if (arg1.Type == EvalReturnType.AssetRef && arg2.Type == EvalReturnType.AssetRef)
                evalVar.SetBool(arg1.Value.AssetId == arg2.Value.AssetId);
            else if (arg1.Type == EvalReturnType.Bool && arg2.Type == EvalReturnType.Bool)
                evalVar.SetBool(arg1.Value.Bool == arg2.Value.Bool);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunGreaterThan(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not GreaterThanPrototype greaterThanProto) return evalVar;

            EvalVar arg1 = Run(greaterThanProto.Arg1, data);
            if (arg1.IsNumeric() == false) 
                return Logger.WarnReturn(evalVar, "GreaterThan: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(greaterThanProto.Arg2, data);
            if (arg2.IsNumeric() == false) 
                return Logger.WarnReturn(evalVar, "GreaterThan: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(arg1.Value.Int > arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(arg1.Value.Int > arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(arg1.Value.Float > arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(arg1.Value.Float > arg2.Value.Float);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunIsContextDataNull(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not IsContextDataNullPrototype isContextDataNullProto) return evalVar;

            EvalVar contextVar = GetEvalVarFromContext(isContextDataNullProto.Context, data, false, false);
            switch (contextVar.Type)
            {
                case EvalReturnType.PropertyCollectionPtr:
                    if (FromValue(contextVar, out PropertyCollection collection) == false) return evalVar;
                    evalVar.SetBool(collection == null);
                    break;

                case EvalReturnType.ProtoRefListPtr:
                    if (FromValue(contextVar, out List<PrototypeId> protoRefList) == false) return evalVar;
                    evalVar.SetBool(protoRefList == null);
                    break;

                case EvalReturnType.ProtoRefVectorPtr:
                    if (FromValue(contextVar, out PrototypeId[] protoRefVector) == false) return evalVar;
                    evalVar.SetBool(protoRefVector == null);
                    break;

                case EvalReturnType.EntityPtr:
                    if (FromValue(contextVar, out Entity entity) == false) return evalVar;
                    evalVar.SetBool(entity == null);
                    break;

                case EvalReturnType.Error:
                    if (isContextDataNullProto.Context >= EvalContext.Var1 && isContextDataNullProto.Context < EvalContext.MaxVars)
                    {
                        evalVar.SetBool(true);
                        break;
                    }
                    return Logger.WarnReturn(evalVar, "IsContextDataNull Eval being checked on a context evalVar that is not a pointer!");

                default:
                    return Logger.WarnReturn(evalVar, "IsContextDataNull Eval being checked on a context evalVar that is not a pointer!");
            }

            return evalVar;
        }

        private static EvalVar RunLessThan(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not LessThanPrototype lessThanProto) return evalVar;

            EvalVar arg1 = Run(lessThanProto.Arg1, data);
            if (arg1.IsNumeric() == false) return Logger.WarnReturn(evalVar, "LessThan: Non-Numeric/Error field Arg1");
            EvalVar arg2 = Run(lessThanProto.Arg2, data);
            if (arg2.IsNumeric() == false) return Logger.WarnReturn(evalVar, "LessThan: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(arg1.Value.Int < arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(arg1.Value.Int < arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetBool(arg1.Value.Float < arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetBool(arg1.Value.Float < arg2.Value.Float);
            else return Logger.WarnReturn(evalVar, "Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunDifficultyTierRange(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not DifficultyTierRangePrototype DifficultyTierRangeProto) return evalVar;

            PrototypeId tierRef = PrototypeId.Invalid;
            EvalVar contextVar = GetEvalVarFromContext(DifficultyTierRangeProto.Context, data, false);
            if (FromValue(contextVar, out Entity entity))
            {
                WorldEntity worldEntity = entity as WorldEntity;
                Region region = worldEntity?.Region;
                if (region == null && entity is Player player)
                    region = player.GetRegion();
                if (region != null)
                    tierRef = region.GetDifficultyTierRef();
            }
            else if (FromValue(contextVar, out PropertyCollection collection))
                tierRef = collection.GetProperty(PropertyEnum.DifficultyTier);

            if (tierRef == PrototypeId.Invalid)
            {
                evalVar.SetBool(true);
                return evalVar;
            }
            else
            {
                evalVar.SetBool(DifficultyTierPrototype.InRange(tierRef, DifficultyTierRangeProto.Min, DifficultyTierRangeProto.Max));
                return evalVar;
            }
        }

        private static EvalVar RunMissionIsActive(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not MissionIsActivePrototype missionIsActiveProto) return evalVar;
            if (FromValue(GetEvalVarFromContext(missionIsActiveProto.Context, data, false), out Entity entity) == false) return evalVar;

            Player player = entity as Player;
            if (player == null && entity is Avatar avatar)
                player = avatar.GetOwnerOfType<Player>();
            if (player == null) return Logger.WarnReturn(evalVar, "Context is not a player.");

            MissionPrototype missionProto = GameDatabase.GetPrototype<MissionPrototype>(missionIsActiveProto.Mission);
            if (missionProto == null) return Logger.WarnReturn(evalVar, "Missing Mission field.");

            if (missionProto.ApprovedForUse() == false || missionProto.IsLiveTuningEnabled() == false)
            {
                evalVar.SetBool(false);
                return evalVar;
            }

            Mission mission = MissionManager.FindMissionForPlayer(player, missionIsActiveProto.Mission);
            evalVar.SetBool(mission != null && mission.State == MissionState.Active);
            return evalVar;
        }

        private static EvalVar RunMissionIsComplete(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not MissionIsCompletePrototype missionIsCompleteProto) return evalVar;

            if (FromValue(GetEvalVarFromContext(missionIsCompleteProto.Context, data, false), out Entity entity) == false) return evalVar;

            Player player = entity?.GetSelfOrOwnerOfType<Player>();
            if (player == null) return Logger.WarnReturn(evalVar, "Context is not a player.");

            MissionPrototype missionProto = GameDatabase.GetPrototype<MissionPrototype>(missionIsCompleteProto.Mission);
            if (missionProto == null) return evalVar;

            if (missionProto.ApprovedForUse() == false || missionProto.IsLiveTuningEnabled() == false)
            {
                evalVar.SetBool(false);
                return evalVar;
            }

            bool avatarMissionState = false;
            Avatar avatar = null;
            if (missionProto.SaveStatePerAvatar)
            {
                avatar = entity as Avatar;
                if (avatar == null) return Logger.WarnReturn(evalVar, "Mission state is per-avatar but Context is not an avatar.");
                if (player.PrimaryAvatar != avatar)
                    avatarMissionState = true;
            }

            if (avatarMissionState)
                evalVar.SetBool((int)avatar.Properties[PropertyEnum.AvatarMissionState, missionProto.DataRef] == (int)MissionState.Completed);
            else
            {
                Mission mission = MissionManager.FindMissionForPlayer(player, missionProto.DataRef);
                evalVar.SetBool(mission != null && mission.State == MissionState.Completed);
            }

            return evalVar;
        }

        private static EvalVar RunNot(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not NotPrototype NotProto) return evalVar;
            if (NotProto.Arg == null) return evalVar;

            EvalVar arg1 = Run(NotProto.Arg, data);
            if (arg1.Type != EvalReturnType.Bool)
                return Logger.WarnReturn(evalVar, "Not: Non-Bool/Error field Arg");

            evalVar.SetBool(!arg1.Value.Bool);
            return evalVar;
        }

        private static EvalVar RunOr(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not OrPrototype OrProto) return evalVar;
            if (OrProto.Arg1 == null || OrProto.Arg2 == null) return evalVar;

            EvalVar arg1 = Run(OrProto.Arg1, data);
            if (arg1.Type != EvalReturnType.Bool)
                return Logger.WarnReturn(evalVar, "Or: Non-Bool/Error field Arg1");

            if (arg1.Value.Bool)
                evalVar.SetBool(false); 
            else
            {
                EvalVar arg2 = Run(OrProto.Arg2, data);
                if (arg2.Type != EvalReturnType.Bool)
                    return Logger.WarnReturn(evalVar, "Or: Non-Bool/Error field Arg2");
                evalVar.SetBool(arg2.Value.Bool);
            }

            return evalVar;
        }

        private static EvalVar RunHasEntityInInventory(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not HasEntityInInventoryPrototype hasEntityInInventoryProto) return evalVar;
            if (FromValue(GetEvalVarFromContext(hasEntityInInventoryProto.Context, data, false), out Entity inventoryOwner) == false)
                return Logger.WarnReturn(evalVar, "Inventory owner is not valid. Make sure you're using the var1 (or EntityPointer) context.");

            PrototypeId entityRef = hasEntityInInventoryProto.Entity;
            InventoryConvenienceLabel inventoryLabel = hasEntityInInventoryProto.Inventory;
            if (inventoryLabel == InventoryConvenienceLabel.None)
                return Logger.WarnReturn(evalVar, "The EntityInventory field in the HasEntityInInventoryPrototype is not valid.");

            BlueprintId parent = BlueprintId.Invalid;
            var dataDir = GameDatabase.DataDirectory;
            if (entityRef != PrototypeId.Invalid && dataDir.PrototypeIsADefaultPrototype(entityRef))
                parent = dataDir.GetPrototypeBlueprintDataRef(entityRef);

            Inventory inventory = inventoryOwner.GetInventory(inventoryLabel);
            bool inInventory = false;
            if (inventory != null)
                foreach (var entry in inventory)
                {
                    ulong entityId = entry.Id;
                    Game game = inventoryOwner.Game;
                    Entity inventoryEntity = game.EntityManager.GetEntity<WorldEntity>(entityId);
                    if (inventoryEntity == null) continue;
                    PrototypeId inventoryEntityRef = inventoryEntity.PrototypeDataRef;
                    if (entityRef == PrototypeId.Invalid 
                        || inventoryEntityRef == entityRef
                        || (parent != BlueprintId.Invalid && dataDir.PrototypeIsChildOfBlueprint(inventoryEntityRef, parent)))
                    {
                        inInventory = true;
                        break;
                    }
                }

            evalVar.SetBool(inInventory);
            return evalVar;
        }

        private static EvalVar RunLoadAssetRef(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();           
            if (evalProto is not LoadAssetRefPrototype loadAssetRefProto)
            {
                evalVar.SetError();
                return evalVar;
            }
            evalVar.SetAssetRef(loadAssetRefProto.Value);
            return evalVar;
        }

        private static EvalVar RunLoadBool(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();            
            if (evalProto is not LoadBoolPrototype loadBoolProto)
            {
                evalVar.SetError();
                return evalVar;
            }
            evalVar.SetBool(loadBoolProto.Value);
            return evalVar;
        }

        private static EvalVar RunLoadFloat(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            if (evalProto is not LoadFloatPrototype loadFloatProto)
            {
                evalVar.SetError();
                return evalVar;
            }
            evalVar.SetFloat(loadFloatProto.Value);
            return evalVar;
        }

        private static EvalVar RunLoadInt(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            if (evalProto is not LoadIntPrototype loadIntProto)
            {
                evalVar.SetError();
                return evalVar;
            }
            evalVar.SetInt(loadIntProto.Value);
            return evalVar;
        }

        private static EvalVar RunLoadProtoRef(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            if (evalProto is not LoadProtoRefPrototype loadProtoRefProto)
            {
                evalVar.SetError();
                return evalVar;
            }
            evalVar.SetProtoRef(loadProtoRefProto.Value);
            return evalVar;
        }

        private static EvalVar RunLoadContextInt(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not LoadContextIntPrototype loadContextIntProto) return evalVar;

            EvalContext context = loadContextIntProto.Context;
            if (context < 0 || context >= EvalContext.MaxVars)
                return Logger.WarnReturn(evalVar, $"LoadContextInt: Context ({context}) is out of the bounds of possible context vars ({EvalContext.MaxVars})");

            if (data.Vars[(int)context].Var.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, $"LoadContextInt: Non-Numeric value in Context Var {context}");

            FromValue(data.Vars[(int)context].Var, out long resultInt);
            evalVar.SetInt(resultInt);

            return evalVar;
        }

        private static EvalVar RunLoadContextProtoRef(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not LoadContextProtoRefPrototype loadContextProtoRefProto) return evalVar;

            EvalContext context = loadContextProtoRefProto.Context;
            if (context < 0 || context >= EvalContext.MaxVars)
                return Logger.WarnReturn(evalVar, $"LoadContextProtoRef: Context ({context}) is out of the bounds of possible context vars ({EvalContext.MaxVars})");

            FromValue(data.Vars[(int)context].Var, out PrototypeId resultProtoRef);
            evalVar.SetProtoRef(resultProtoRef);

            return evalVar;
        }

        private static EvalVar RunFor(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not ForPrototype forProto) return evalVar;

            if (forProto.LoopVarInit == null)
                return Logger.WarnReturn(evalVar, "No eval in For eval LoopVarInit");

            if (forProto.LoopAdvance == null)
                return Logger.WarnReturn(evalVar, "No eval in For eval LoopAdvance");

            if (forProto.LoopCondition == null)
                return Logger.WarnReturn(evalVar, "No eval in For eval LoopCondition");

            if (forProto.ScopeLoopBody.IsNullOrEmpty())
                return Logger.WarnReturn(evalVar, "No evals in For eval ScopeLoopBody");

            var dataCallerStackProps = data.CallerStackProperties;
            var dataLocalStackProps = data.LocalStackProperties;
            data.CallerStackProperties = dataLocalStackProps;
            var localStackProps = new PropertyCollection();
            data.LocalStackProperties = localStackProps;

            if (forProto.PreLoop != null)
            {
                evalVar = Run(forProto.PreLoop, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();
            }

            evalVar = Run(forProto.LoopVarInit, data);
            if (evalVar.Type == EvalReturnType.Error) return Return();

            evalVar = Run(forProto.LoopCondition, data);
            if (evalVar.Type != EvalReturnType.Bool) return Return();

            while (evalVar.Value.Bool)
            {
                foreach (var eachProto in forProto.ScopeLoopBody)
                {
                    if (eachProto == null) continue;

                    data.CallerStackProperties = dataLocalStackProps;
                    data.LocalStackProperties = localStackProps;

                    evalVar = Run(eachProto, data);
                    if (evalVar.Type == EvalReturnType.Error) return Return();
                }

                data.CallerStackProperties = dataLocalStackProps;
                data.LocalStackProperties = localStackProps;

                evalVar = Run(forProto.LoopAdvance, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();

                evalVar = Run(forProto.LoopCondition, data);
                if (evalVar.Type != EvalReturnType.Bool) return Return();
            }

            data.CallerStackProperties = dataCallerStackProps;
            data.LocalStackProperties = localStackProps;

            if (forProto.PostLoop != null)
            {
                evalVar = Run(forProto.PostLoop, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();
            }

            return Return();

            EvalVar Return()
            {
                data.CallerStackProperties = dataCallerStackProps;
                data.LocalStackProperties = dataLocalStackProps;
                return evalVar;
            }
        }

        private static EvalVar RunForEachConditionInContext(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not ForEachConditionInContextPrototype forEachProto) return evalVar;

            if (forEachProto.ScopeLoopBody.IsNullOrEmpty())
                return Logger.WarnReturn(evalVar, "No evals in ForEachProtoRefInContextRefList eval ScopeLoopBody");

            var dataCallerStackProps = data.CallerStackProperties;
            var dataLocalStackProps = data.LocalStackProperties;
            data.CallerStackProperties = dataLocalStackProps;
            var localStackProps = new PropertyCollection();
            data.LocalStackProperties = localStackProps;

            if (forEachProto.PreLoop != null)
            {
                evalVar = Run(forEachProto.PreLoop, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();
            }

            if (FromValue(GetEvalVarFromContext(forEachProto.ConditionCollectionContext, data, false), out ConditionCollection conditionCollection) == false)
                return evalVar;

            if (conditionCollection != null)
                foreach (var kvp in conditionCollection)
                {
                    Condition condition = kvp.Value;
                    if (condition == null) continue;

                    data.SetVar_PropertyCollectionPtr(EvalContext.Condition, condition.Properties);
                    data.SetReadOnlyVar_ProtoRefVectorPtr(EvalContext.ConditionKeywords, condition.Keywords);

                    if (forEachProto.LoopConditionPreScope != null)
                    {
                        evalVar = Run(forEachProto.LoopConditionPreScope, data);
                        if (evalVar.Type != EvalReturnType.Bool) return Return();
                        if (evalVar.Value.Bool == false) break;
                    }

                    foreach (EvalPrototype eachProto in forEachProto.ScopeLoopBody)
                    {
                        if (eachProto == null) continue;

                        data.CallerStackProperties = dataLocalStackProps;
                        data.LocalStackProperties = localStackProps;

                        evalVar = Run(eachProto, data);
                        if (evalVar.Type == EvalReturnType.Error) return Return();
                    }

                    if (forEachProto.LoopConditionPostScope != null)
                    {
                        evalVar = Run(forEachProto.LoopConditionPostScope, data);
                        if (evalVar.Type != EvalReturnType.Bool) return Return();
                        if (evalVar.Value.Bool == false) break;
                    }
                }

            data.SetVar_ConditionCollectionPtr(EvalContext.Condition, null);
            data.SetVar_ProtoRefVectorPtr(EvalContext.ConditionKeywords, null);

            data.CallerStackProperties = dataLocalStackProps;
            data.LocalStackProperties = localStackProps;

            if (forEachProto.PostLoop != null)
            {
                evalVar = Run(forEachProto.PostLoop, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();
            }

            return Return();

            EvalVar Return()
            {
                data.CallerStackProperties = dataCallerStackProps;
                data.LocalStackProperties = dataLocalStackProps;
                return evalVar;
            }
        }

        private static EvalVar RunForEachProtoRefInContextRefList(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not ForEachProtoRefInContextRefListPrototype forEachProto) return evalVar;

            if (forEachProto.ScopeLoopBody.IsNullOrEmpty())
                return Logger.WarnReturn(evalVar, "No evals in ForEachProtoRefInContextRefList eval ScopeLoopBody");

            var dataCallerStackProps = data.CallerStackProperties;
            var dataLocalStackProps = data.LocalStackProperties;
            data.CallerStackProperties = dataLocalStackProps;
            var localStackProps = new PropertyCollection();
            data.LocalStackProperties = localStackProps;

            if (forEachProto.PreLoop != null)
            {
                evalVar = Run(forEachProto.PreLoop, data);
                if (evalVar.Type == EvalReturnType.Error) return Return();
            }

            if (RunForEachProtoRefInContextRefListType(forEachProto, data, ref evalVar, dataLocalStackProps, localStackProps) == false)
            {
                Logger.Warn("A ForEachProtoRefInContextRefList prototype specified a ProtoRefListContext that is not a valid PrototypeDataRefList or PrototypeDataRefVector.");
                return Return();
            }

            data.CallerStackProperties = dataLocalStackProps;
            data.LocalStackProperties = localStackProps;

            if (forEachProto.PostLoop != null)
            {
                evalVar = Run(forEachProto.PostLoop, data);
                if (evalVar.Type == EvalReturnType.Error)
                    return Return();
            }

            return Return();

            EvalVar Return()
            {
                data.CallerStackProperties = dataCallerStackProps;
                data.LocalStackProperties = dataLocalStackProps;
                return evalVar;
            }
        }

        private static bool RunForEachProtoRefInContextRefListType(ForEachProtoRefInContextRefListPrototype forEachProto, EvalContextData data, ref EvalVar evalVar,
            PropertyCollection originalLocalStackProps, PropertyCollection localStackProps)
        {
            if (forEachProto == null || originalLocalStackProps == null) return false;

            EvalVar varList = GetEvalVarFromContext(forEachProto.ProtoRefListContext, data, false);
            if (FromValue(varList, out List<PrototypeId> protoRefList) == false)
            {
                if (FromValue(varList, out PrototypeId[] protoRefVector)) // part from Eval::runForEachProtoRefInContextRefList
                    protoRefList = new (protoRefVector);
                else
                    return false;
            }

            if (protoRefList != null)
                foreach (var protoRef in protoRefList)
                {
                    if (forEachProto.LoopCondition != null)
                    {
                        evalVar = Run(forEachProto.LoopCondition, data);
                        if (evalVar.Type != EvalReturnType.Bool) return false;
                        if (evalVar.Value.Bool == false) break;
                    }

                    localStackProps[PropertyEnum.EvalLoopVarProtoRef, 0] = protoRef;

                    foreach (var evalProto in forEachProto.ScopeLoopBody)
                    {
                        if (evalProto == null) continue;

                        data.CallerStackProperties = originalLocalStackProps;
                        data.LocalStackProperties = localStackProps;

                        evalVar = Run(evalProto, data);
                        if (evalVar.Type == EvalReturnType.Error) return false;
                    }
                }

            return true;
        }

        private static EvalVar RunIfElse(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not IfElsePrototype ifElseProto) return evalVar;

            if (ifElseProto.Conditional == null)
                return Logger.WarnReturn(evalVar, "IfElse Eval with a NULL Conditional field!");

            if (ifElseProto.EvalIf == null)
                return Logger.WarnReturn(evalVar, "IfElse Eval with a NULL EvalIf field!");

            EvalVar conditionalVar = Run(ifElseProto.Conditional, data);
            bool conditionalValue;
            switch (conditionalVar.Type)
            {
                case EvalReturnType.Bool:
                case EvalReturnType.Float:
                case EvalReturnType.Int:
                    FromValue(conditionalVar, out conditionalValue);
                    break;
                default:
                    return Logger.WarnReturn(evalVar, "Non-Value/Bool Conditional.");
            }

            if (conditionalValue)
                evalVar = Run(ifElseProto.EvalIf, data);
            else
                if (ifElseProto.EvalElse != null)
                    evalVar = Run(ifElseProto.EvalElse, data);
                else
                    evalVar.SetUndefined();

            return evalVar;
        }

        private static EvalVar RunScope(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not ScopePrototype scopeProto) return evalVar;

            if (scopeProto.Scope.IsNullOrEmpty())
                return Logger.WarnReturn(evalVar, "No eval entries in Scope eval");

            var dataCallerStackProps = data.CallerStackProperties;
            var dataLocalStackProps = data.LocalStackProperties;
            var localStackProps = new PropertyCollection();

            bool errors = false;
            foreach (var evalEach in scopeProto.Scope)
            {
                if (evalEach == null) continue;

                data.CallerStackProperties = dataLocalStackProps;
                data.LocalStackProperties = localStackProps;

                evalVar = Run(evalEach, data);
                if (evalVar.Type == EvalReturnType.Error) errors = true;
            }

            data.CallerStackProperties = dataCallerStackProps;
            data.LocalStackProperties = dataLocalStackProps;

            if (errors)
                evalVar.SetError();

            return evalVar;
        }

        private static EvalVar RunExportError(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            Logger.Warn("Eval failed to export correctly from Calligraphy");
            return evalVar;
        }

        private static EvalVar RunLoadCurve(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not LoadCurvePrototype loadCurveProto) return evalVar;

            if (loadCurveProto.Curve == CurveId.Invalid)
                return Logger.WarnReturn(evalVar, "LoadCurvePrototype contains Invalid \"Curve\" Field");

            if (loadCurveProto.Index == null)
                return Logger.WarnReturn(evalVar, "LoadCurvePrototype contains NULL \"Index\" Field");

            Curve curve = GameDatabase.GetCurve(loadCurveProto.Curve);
            if (curve == null) return evalVar;

            EvalVar indexVar = Run(loadCurveProto.Index, data);
            int index;
            switch (indexVar.Type)
            {
                case EvalReturnType.Int:
                    index = (int)indexVar.Value.Int;
                    break;
                case EvalReturnType.Float:
                    index = (int)indexVar.Value.Float;
                    break;
                default:
                    return Logger.WarnReturn(evalVar, $"LoadCurvePrototype contains an invalid var type for its \"Index\" Field! (Index var type=[{indexVar.Type}])");
            }

            if (curve.IndexInRange(index) == false)
            {
                Logger.Warn($"LoadCurvePrototype index ({index}) is out of range of the curve {GameDatabase.GetCurveName(loadCurveProto.Curve)}, clamping to bounds and still running");
                index = Math.Clamp(index, curve.MinPosition, curve.MaxPosition);
            }

            evalVar.SetFloat(curve.GetAt(index));
            return evalVar;
        }

        private static EvalVar RunAdd(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not AddPrototype addProto) return evalVar;

            EvalVar arg1 = Run(addProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Add: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(addProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Add: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(arg1.Value.Int + arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Int + arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(arg1.Value.Float + arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Float + arg2.Value.Float);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunDiv(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not DivPrototype divProto) return evalVar;

            EvalVar arg1 = Run(divProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Div: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(divProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Div: Non-Numeric/Error field Arg2");

            if (arg2.Type == EvalReturnType.Int && arg2.Value.Int == 0)
                return Logger.WarnReturn(evalVar, "Div: Arg2=0 DIVZERO!");
            else if (arg2.Type == EvalReturnType.Float && arg2.Value.Float == 0)
                return Logger.WarnReturn(evalVar, "Div: Arg2=0.0f DIVZERO!");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(arg1.Value.Int / (float)arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Int / arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(arg1.Value.Float / arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Float / arg2.Value.Float);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunExponent(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not ExponentPrototype exponentProto) return evalVar;

            EvalVar baseVar = Run(exponentProto.BaseArg, data);
            if (baseVar.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Exponent: Non-Numeric/Error field Base");

            EvalVar exponentVar = Run(exponentProto.ExpArg, data);
            if (exponentVar.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Exponent: Non-Numeric/Error field Exponent");

            if (FromValue(baseVar, out float baseFloat) == false)
                return Logger.WarnReturn(evalVar, "Exponent: Error Extracting Base from evalVar");

            if (FromValue(exponentVar, out float expFloat) == false)
                return Logger.WarnReturn(evalVar, "Exponent: Error Extracting Exponent from evalVar");

            evalVar.SetFloat(MathF.Pow(baseFloat, expFloat));
            return evalVar;
        }

        private static EvalVar RunMax(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not MaxPrototype maxProto) return evalVar;

            EvalVar arg1 = Run(maxProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Max: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(maxProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Max: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(Math.Max(arg1.Value.Int, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(Math.Max(arg1.Value.Int, arg2.Value.Float));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(Math.Max(arg1.Value.Float, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(Math.Max(arg1.Value.Float, arg2.Value.Float));
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunMin(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not MinPrototype minProto) return evalVar;

            EvalVar arg1 = Run(minProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Min: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(minProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Min: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(Math.Min(arg1.Value.Int, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(Math.Min(arg1.Value.Int, arg2.Value.Float));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(Math.Min(arg1.Value.Float, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(Math.Min(arg1.Value.Float, arg2.Value.Float));
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunModulus(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not ModulusPrototype modulusProto) return evalVar;

            EvalVar arg1 = Run(modulusProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Modulus: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(modulusProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Modulus: Non-Numeric/Error field Arg2");
            
            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(MathHelper.Modulus(arg1.Value.Int, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(MathHelper.FloatModulus(arg1.Value.Int, arg2.Value.Float));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(MathHelper.FloatModulus(arg1.Value.Float, arg2.Value.Int));
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(MathHelper.FloatModulus(arg1.Value.Float, arg2.Value.Float));
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunMult(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not MultPrototype multProto) return evalVar;

            EvalVar arg1 = Run(multProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Multiply: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(multProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Multiply: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(arg1.Value.Int * arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Int * arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(arg1.Value.Float * arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Float * arg2.Value.Float);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunSub(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new();
            evalVar.SetError();
            if (evalProto is not SubPrototype subProto) return evalVar;

            EvalVar arg1 = Run(subProto.Arg1, data);
            if (arg1.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Subtract: Non-Numeric/Error field Arg1");

            EvalVar arg2 = Run(subProto.Arg2, data);
            if (arg2.IsNumeric() == false)
                return Logger.WarnReturn(evalVar, "Subtract: Non-Numeric/Error field Arg2");

            if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Int)
                evalVar.SetInt(arg1.Value.Int - arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Int && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Int - arg2.Value.Float);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Int)
                evalVar.SetFloat(arg1.Value.Float - arg2.Value.Int);
            else if (arg1.Type == EvalReturnType.Float && arg2.Type == EvalReturnType.Float)
                evalVar.SetFloat(arg1.Value.Float - arg2.Value.Float);
            else
                Logger.Warn("Error with arg types!");

            return evalVar;
        }

        private static EvalVar RunAssignProp(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not AssignPropPrototype assignPropProto) return evalVar;

            if (assignPropProto.Eval == null)
                return Logger.WarnReturn(evalVar, "AssignPropPrototype contains NULL \"Eval\" Field");

            if (assignPropProto.Prop == PropertyId.Invalid)
                return Logger.WarnReturn(evalVar, "AssignPropPrototype contains Invalid \"Prop\" Field");

            EvalVar assignVar = Run(assignPropProto.Eval, data);
            if (assignVar.Type == EvalReturnType.Error || assignVar.Type == EvalReturnType.Undefined)
                return Logger.WarnReturn(evalVar, "AssignPrototype has Eval that returns Error or Undefined Value");

            if (FromValue(GetEvalVarFromContext(assignPropProto.Context, data, true), out PropertyCollection collection) == false)
                return evalVar;

            if (collection == null)
                return Logger.WarnReturn(evalVar, "Invalid Context");

            PropertyId propId = assignPropProto.Prop;
            PropertyEnum propEnum = propId.Enum;
            PropertyInfo propInfo = GameDatabase.PropertyInfoTable.LookupPropertyInfo(propEnum);
            PropertyDataType propertyType = propInfo.DataType;

            switch (propertyType)
            {
                case PropertyDataType.Integer:
                    if (FromValue(assignVar, out long intValue))
                        collection[propId] = intValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Int, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.Real:
                    if (FromValue(assignVar, out float floatValue))
                        collection[propId] = floatValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Float, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.Boolean:
                    if (FromValue(assignVar, out bool boolValue))
                        collection[propId] = boolValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Bool, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.EntityId:
                    if (FromValue(assignVar, out ulong entityIdValue))
                        collection[propId] = entityIdValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to EntityId, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.RegionId:
                    if (FromValue(assignVar, out ulong regionIdValue))
                        collection[propId] = regionIdValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to RegionId, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.Prototype:
                    if (FromValue(assignVar, out PrototypeId protoRefValue))
                        collection[propId] = protoRefValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Prototype, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.Asset:
                    if (FromValue(assignVar, out AssetId assetValue))
                        collection[propId] = assetValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Asset, Property: [{propInfo.PropertyName}]");
                    break;

                case PropertyDataType.Time:
                    if (FromValue(assignVar, out long timeSpanValue))
                        collection[propId] = TimeSpan.FromMilliseconds(timeSpanValue);
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Int, Property: [{propInfo.PropertyName}]");
                    break;

                default:
                    return Logger.WarnReturn(evalVar, $"Assignment into invalid property (property type is not int/float/bool)! Property: [{propInfo.PropertyName}]");
            }

            evalVar.SetUndefined();
            return evalVar;
        }

        private static EvalVar RunAssignPropEvalParams(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();

            if (evalProto is not AssignPropEvalParamsPrototype assignPropEvalParamsProto)
                return evalVar;

            if (assignPropEvalParamsProto.Eval == null)
                return Logger.WarnReturn(evalVar, "AssignPropEvalParamsPrototype contains NULL \"Eval\" Field");

            if (assignPropEvalParamsProto.Prop == PrototypeId.Invalid)
                return Logger.WarnReturn(evalVar, "AssignPropEvalParamsPrototype contains Invalid \"Prop\" Field");

            EvalVar assignVar = Run(assignPropEvalParamsProto.Eval, data);
            if (assignVar.Type == EvalReturnType.Error || assignVar.Type == EvalReturnType.Undefined)
                return Logger.WarnReturn(evalVar, "AssignPropEvalParamsPrototype has Eval that returns Error or Undefined Value");

            if (FromValue(GetEvalVarFromContext(assignPropEvalParamsProto.Context, data, true), out PropertyCollection collection) == false)
                return evalVar;

            if (collection == null)
                return Logger.WarnReturn(evalVar, "Invalid Context");

            PropertyInfoTable propInfoTable = GameDatabase.PropertyInfoTable;
            PropertyEnum propEnum = propInfoTable.GetPropertyEnumFromPrototype(assignPropEvalParamsProto.Prop);
            PropertyInfo propInfo = propInfoTable.LookupPropertyInfo(propEnum);
            PropertyParam[] paramValues = propInfo.DefaultParamValues;
            for (int i = 0; i < propInfo.ParamCount; i++)
            {
                if (i >= 4) break;
                EvalPrototype paramEval = i switch
                {
                    0 => assignPropEvalParamsProto.Param0,
                    1 => assignPropEvalParamsProto.Param1,
                    2 => assignPropEvalParamsProto.Param2,
                    3 => assignPropEvalParamsProto.Param3,
                    _ => null
                };

                if (paramEval == null) continue;

                switch (propInfo.GetParamType(i))
                {
                    case PropertyParamType.Asset:
                        if (FromValue(Run(paramEval, data), out AssetId assetParam))
                            paramValues[i] = Property.ToParam(assetParam);
                        break;

                    case PropertyParamType.Prototype:
                        if (FromValue(Run(paramEval, data), out PrototypeId protoRefParam))
                            paramValues[i] = Property.ToParam(propEnum, i, protoRefParam);
                        break;

                    case PropertyParamType.Integer:
                        if (FromValue(Run(paramEval, data), out int intParam))
                            paramValues[i] = (PropertyParam)intParam;
                        break;

                    default:
                        return Logger.WarnReturn(evalVar, "Encountered an unknown prop param type in an AssignPropEvalParams Eval!");
                }
            }

            PropertyId propId = new (propEnum, paramValues);

            switch (propInfo.DataType)
            {
                case PropertyDataType.Integer:
                    if (FromValue(assignVar, out long intValue))
                        collection[propId] = intValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Int, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.Real:
                    if (FromValue(assignVar, out float floatValue))
                        collection[propId] = floatValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Float, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.Boolean:
                    if (FromValue(assignVar, out bool boolValue))
                        collection[propId] = boolValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Bool, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.EntityId:
                    if (FromValue(assignVar, out ulong entityIdValue))
                        collection[propId] = entityIdValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to EntityId, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.RegionId:
                    if (FromValue(assignVar, out ulong regionIdValue))
                        collection[propId] = regionIdValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to RegionId, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.Prototype:
                    if (FromValue(assignVar, out PrototypeId protoRefValue))
                        collection[propId] = protoRefValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Prototype, Property: {propInfo.PropertyName}");
                    break;

                case PropertyDataType.Asset:
                    if (FromValue(assignVar, out AssetId assetValue))
                        collection[propId] = assetValue;
                    else
                        return Logger.WarnReturn(evalVar, $"Unable to convert TYPE to Asset, Property: {propInfo.PropertyName}");
                    break;

                default:
                    return Logger.WarnReturn(evalVar, $"Assignment into invalid property (property type is not int/float/bool)! Property: {propInfo.PropertyName}");
            }

            evalVar.SetUndefined();
            return evalVar;
        }

        private static EvalVar RunHasProp(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar evalVar = new ();
            evalVar.SetError();
            if (evalProto is not HasPropPrototype hasPropProto) return evalVar;

            if (hasPropProto.Prop == PropertyId.Invalid)
                return Logger.WarnReturn(evalVar, "HasPropPrototype contains Invalid \"Prop\" Field");

            if (FromValue(GetEvalVarFromContext(hasPropProto.Context, data, false), out PropertyCollection collection, data.Game) == false)
                return evalVar;

            if (collection == null)
                return Logger.WarnReturn(evalVar, "Invalid Context");

            evalVar.SetBool(collection.HasProperty(hasPropProto.Prop));
            return evalVar;
        }

        private static EvalVar RunLoadProp(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunLoadPropContextParams(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunLoadPropEvalParams(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunSwapProp(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunRandomFloat(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunRandomInt(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunLoadEntityToContextVar(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunLoadConditionCollectionToContext(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunEntityHasKeyword(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunEntityHasTalent(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunGetCombatLevel(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunGetPowerRank(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunCalcPowerRank(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunIsInParty(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunGetDamageReductionPct(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunGetDistanceToEntity(EvalPrototype evalProto, EvalContextData data)
        {
            throw new NotImplementedException();
        }

        private static EvalVar RunIsDynamicCombatLevelEnabled(EvalPrototype evalProto, EvalContextData data)
        {
            EvalVar var = new();
            var.SetError();
            var.SetBool(true);
            return var;
        }

    }
}
