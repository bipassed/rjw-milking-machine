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
    public class BreastMilkingHediff : HediffWithComps
    {
        public static float breastMultiplier;
        public static float lactatingMultiplier;
        public static float udder = 1;
        public override void Tick()
        {
            Pawn pawn = this.pawn;
            if (pawn.IsHashIntervalTick(60000)) //60000
            {
                if (pawn.IsColonist || pawn.IsPrisoner || pawn.IsSlave)
                {
                    IEnumerable<Hediff> breasts = pawn.GetBreastList().Where(breastHediff => Custom_Genital_Helper.is_breast(breastHediff));
                    bool hasBreast = !breasts.EnumerableNullOrEmpty();
                    if (hasBreast)
                    {
                        foreach (Hediff breast in breasts)
                        {
                            CompHediffBodyPart rjwBreastHediff = breast.TryGetComp<CompHediffBodyPart>();
                            if (rjwBreastHediff != null)
                            {
                                // Cows in real life produce 8gal daily while cows in RW produce 14u daily
                                // 1u of milk = 1.75gal or 6624ml
                                // Humans produce at min 216ml max 3031ml or just under 1u for both average 1623.5ml


                                PartSizeExtension.TryGetBreastWeight(breast, out float breastWeight);
                                PartSizeExtension.TryGetCupSize(breast, out float cupSize);
                                breastMultiplier = cupSize / breastWeight;
                                if (pawn.health.hediffSet.HasHediff(HediffDefOf.Lactating_Drug) || pawn.health.hediffSet.HasHediff(HediffDefOf.Lactating_Permanent))
                                {
                                    lactatingMultiplier = 3;
                                }
                                // Racial breast checks
                                if (breast.LabelBase.ToLower().Contains("udder"))
                                    udder = 14;
                                //Log.Message(pawn + "'s 1b is: " + breastMultiplier);
                                Thing breastThing = ThingMaker.MakeThing(ThingDefOf.Milk);
                                breastThing.stackCount = (int)(((pawn.BodySize * breastMultiplier) * udder) * lactatingMultiplier);
                                if (breastThing.stackCount < 1)
                                    breastThing.stackCount = 1;
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
