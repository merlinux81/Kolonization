using System;
using System.Linq;
using KolonyTools.Kolonization;
using UnityEngine;

namespace Kolonization
{
    public class KolonizationSetup : MonoBehaviour
    {
        // Static singleton instance
        private static KolonizationSetup instance;

        // Static singleton property
        public static KolonizationSetup Instance
        {
            get { return instance ?? (instance = new GameObject("KolonizationSetup").AddComponent<KolonizationSetup>()); }
        }

        //Static data holding variables
        private static KolonizationConfig _Config;

        public KolonizationConfig Config
        {
            get { return _Config ?? (_Config = LoadKolonizationConfig()); }
        }

        private KolonizationConfig LoadKolonizationConfig()
        {
            var kolonyNodes = GameDatabase.Instance.GetConfigNodes("LIFE_SUPPORT_SETTINGS");
            var finalSettings = new KolonizationConfig
            {
                OrbitMultiplier = 0.1f
            };
            foreach (var lsNode in kolonyNodes)
            {
                var settings = ResourceUtilities.LoadNodeProperties<KolonizationConfig>(lsNode);
                finalSettings.OrbitMultiplier = Math.Min(settings.OrbitMultiplier, finalSettings.OrbitMultiplier);
            }
            return finalSettings;
        }
    }
}