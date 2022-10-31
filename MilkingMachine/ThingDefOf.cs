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
    public static class ThingDefOf
    {
        public static ThingDef Milk;

        public static ThingDef UsedCondom;

        [MayRequire("rjw.sexperience")]
        public static ThingDef GatheredCum;

        public static ThingDef LM_DragonCum;
    }
}
