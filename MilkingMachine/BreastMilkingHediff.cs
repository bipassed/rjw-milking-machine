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
        public static bool MlieMilkableColonists = ModsConfig.IsActive("mlie.milkablecolonists");
        public static bool ED86MilkableColonists = ModsConfig.IsActive("rjw.milk.humanoid");
        public static bool Biotech = ModsConfig.BiotechActive;

        public static float breastMultiplier = 1;
        public static float traitMultiplier = 1;
        public static float mcLactating = 1;
        public static float bLactating = 1;
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
                                breastMultiplier = (cupSize + breastWeight);
                                // Mod checks
                                if (MlieMilkableColonists == true || ED86MilkableColonists == true)
                                    if (pawn.health.hediffSet.HasHediff(VariousDefOf.Lactating_Drug) || pawn.health.hediffSet.HasHediff(VariousDefOf.Lactating_Permanent))
                                        mcLactating = 2;
                                else if (Biotech == true)
                                    if (pawn.health.hediffSet.HasHediff(VariousDefOf.Lactating))
                                        bLactating = 2;
                                // Racial breast checks
                                else if (breast.LabelBase.ToLower().Contains("udder"))
                                    udder = 6;
                                // Trait checks
                                else if (pawn.story.traits.HasTrait(VariousDefOf.LM_NaturalCow) || pawn.story.traits.HasTrait(VariousDefOf.LM_NaturalHucow))
                                    traitMultiplier = 3;
                                Need sexNeed = pawn.needs.TryGetNeed(VariousDefOf.Sex);
                                Thing breastThing = ThingMaker.MakeThing(VariousDefOf.Milk);
                                breastThing.stackCount = (int)(pawn.BodySize * breastMultiplier * mcLactating * bLactating * traitMultiplier * udder);
                                if (breastThing.stackCount < 1)
                                    breastThing.stackCount = 1;
                                GenPlace.TryPlaceThing(breastThing, pawn.Position, pawn.Map, ThingPlaceMode.Near);
                                sexNeed.CurLevel += 1;
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
