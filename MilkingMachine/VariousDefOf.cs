using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace MilkingMachine
{
    [DefOf]
    public static class VariousDefOf
    {
        public static ThingDef Milk;
        public static ThingDef UsedCondom;
        public static ThingDef LM_DragonCum;
        public static NeedDef Sex = DefDatabase<NeedDef>.GetNamed("Sex");
        public static TraitDef LM_NaturalHucow = DefDatabase<TraitDef>.GetNamed("LM_NaturalHucow");
        public static TraitDef LM_HighTestosterone = DefDatabase<TraitDef>.GetNamed("LM_HighTestosterone");
        public static TraitDef LM_NaturalCow = DefDatabase<TraitDef>.GetNamed("LM_NaturalCow");

        [MayRequire("rjw.sexperience")]
        public static ThingDef GatheredCum;
        [MayRequire("rjw.sexperience")]
        public static HediffDef Lactating_Drug;
        [MayRequire("rjw.sexperience")]
        public static HediffDef Lactating_Permanent;

        [MayRequireBiotech]
        public static HediffDef Lactating;
    }
}
