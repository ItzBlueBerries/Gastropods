using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Assist
{
    internal class ModFears
    {
        public static void AddToAllProfiles(IdentifiableType ident, float maxSearchRadius, float minSearchRadius, [Optional] FearProfile fearProfile)
        {
            if (fearProfile == null)
                fearProfile = Get<FearProfile>("slimeStandardFearProfile");

            if (fearProfile.threats.Count > 0)
            {
                FearProfile.ThreatEntry threatEntry = new FearProfile.ThreatEntry()
                {
                    identType = ident,
                    maxSearchRadius = maxSearchRadius,
                    minSearchRadius = minSearchRadius,
                    maxThreatFearPerSec = fearProfile.threats[1].maxThreatFearPerSec,
                    minThreatFearPerSec = fearProfile.threats[1].minThreatFearPerSec
                };

                if (fearProfile.threats.Contains(threatEntry))
                    return;
                else
                    fearProfile.threats.Add(threatEntry);

                if (fearProfile.threatsLookup.ContainsKey(ident) || fearProfile.threatsLookup.ContainsValue(threatEntry))
                    return;
                else
                    fearProfile.threatsLookup.Add(ident, threatEntry);
            }
        }


        public static void RemoveFromProfile(IdentifiableType ident, GameObject slimePrefab)
        {
            FearProfile fearProfile = slimePrefab.GetComponent<FleeThreats>().fearProfile;
            if (fearProfile == null)
                return;

            fearProfile = UnityEngine.Object.Instantiate(fearProfile);

            if (fearProfile.threats.Count > 0)
            {
                FearProfile.ThreatEntry threatEntry = new FearProfile.ThreatEntry()
                {
                    identType = ident,
                    maxSearchRadius = fearProfile.threats[0].maxSearchRadius,
                    minSearchRadius = fearProfile.threats[0].minSearchRadius,
                    maxThreatFearPerSec = fearProfile.threats[0].maxThreatFearPerSec,
                    minThreatFearPerSec = fearProfile.threats[0].minThreatFearPerSec
                };

                if (!fearProfile.threats.Contains(threatEntry))
                    return;
                else
                    fearProfile.threats.Remove(threatEntry);

                if (!fearProfile.threatsLookup.ContainsKey(ident))
                    return;
                else
                    fearProfile.threatsLookup.Remove(ident);
            }

            slimePrefab.GetComponent<FleeThreats>().fearProfile = fearProfile;
        }
    }
}
