using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace MilkingMachine
{
    [DefOf]
    public static class HediffDefOf
    {
        [MayRequire("rjw.sexperience")]
        public static HediffDef Lactating_Drug;

        [MayRequire("rjw.sexperience")]
        public static HediffDef Lactating_Permanent;
    }
}
