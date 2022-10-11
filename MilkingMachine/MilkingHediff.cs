using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using rjw;

namespace MilkingMachine
{
    public class MilkingHediff : HediffWithComps
    {
        public static float penisSize;
        public static float breastSize;
        public override void Tick()
        {
            Pawn pawn = this.pawn;
            if (pawn.IsHashIntervalTick(60000))
            {
                if (pawn.IsColonist || pawn.IsPrisoner || pawn.IsSlave)
                {
                    IEnumerable<Hediff> penises = pawn.GetGenitalsList().Where(genitalHediff => Custom_Genital_Helper.is_penis(genitalHediff));
                    bool hasPenis = !penises.EnumerableNullOrEmpty();
                    IEnumerable<Hediff> breasts = pawn.GetBreastList().Where(breastHediff => Custom_Genital_Helper.is_breast(breastHediff));
                    bool hasBreast = !breasts.EnumerableNullOrEmpty();
                    if (hasPenis)
                    {
                        foreach (Hediff penis in penises)
                        {
                            CompHediffBodyPart rjwPenisHediff = penis.TryGetComp<CompHediffBodyPart>();
                            if (rjwPenisHediff != null)
                            {
                                PartSizeExtension.TryGetLength(penis, out float penisLength);
                                PartSizeExtension.TryGetGirth(penis, out float penisGirth);
                                penisSize = (penisGirth + penisLength) / 2;
                                if (penisSize < 1)
                                    penisSize = 1;
                                // Log.Message(pawn + "'s penis size is: " + penisSize);
                                Thing penisThing = ThingMaker.MakeThing(ThingDefOf.UsedCondom);
                                penisThing.stackCount = (int)(pawn.BodySize * penisSize);
                                GenPlace.TryPlaceThing(penisThing, pawn.Position, pawn.Map, ThingPlaceMode.Near);
                            }
                        }
                    }
                    if (hasBreast)
                    {
                        foreach (Hediff breast in breasts)
                        {
                            CompHediffBodyPart rjwBreastHediff = breast.TryGetComp<CompHediffBodyPart>();
                            if (rjwBreastHediff != null)
                            {
                                PartSizeExtension.TryGetBreastWeight(breast, out float breastWeight);
                                PartSizeExtension.TryGetCupSize(breast, out float cupSize);
                                breastSize = breastWeight + cupSize;
                                if (breastSize < 1)
                                    breastSize = 1;
                                // Log.Message(pawn + "'s breast size is: " + breastSize);
                                Thing breastThing = ThingMaker.MakeThing(ThingDefOf.Milk);
                                breastThing.stackCount = (int)(pawn.BodySize * breastSize);
                                GenPlace.TryPlaceThing(breastThing, pawn.Position, pawn.Map, ThingPlaceMode.Near);
                            }
                        }
                    }
                }
                else
                    return;
            }
        }
	}
}
