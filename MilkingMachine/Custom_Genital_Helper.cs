using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using rjw;

namespace MilkingMachine
{
    public static class Custom_Genital_Helper
    {
		public static bool is_penis(Hediff hed)
		{
			if (hed == null)
				return false;
			if (!hed.def.defName.ToLower().Contains("penis") && !hed.def.defName.ToLower().Contains("ovipositorm") && !hed.def.defName.ToLower().Contains("pegdick"))
			{
				if (hed.def.defName.ToLower().Contains("tentacle"))
					return !hed.def.defName.ToLower().Contains("penis");
				return false;
			}
			return true;
		}
		public static bool is_breast(Hediff hed)
		{
			if (hed == null)
				return false;
			if (!hed.def.defName.ToLower().Contains("breast") && !hed.def.defName.ToLower().Contains("chest"))
				return false;
			return true;
		}
	}
}
