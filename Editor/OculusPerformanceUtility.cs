using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Runtime.InteropServices;
using XRStats = UnityEngine.XR.Provider.XRStats;

public static class OculusPerformance
{
    [DllImport("OculusXRPlugin", CharSet = CharSet.Auto)]
    public static extern void SetCPULevel(int cpuLevel);

    [DllImport("OculusXRPlugin", CharSet = CharSet.Auto)]
    public static extern void SetGPULevel(int gpuLevel);
}

public class OculusStats
{
    private static IntegratedSubsystem m_Display;
    [DllImport("OculusXRPlugin", CharSet=CharSet.Auto)]
    private static extern void GetOVRPVersion(byte[] version);
    
    /// Gets the version of OVRPlugin currently in use. Format: "major.minor.release"
    public static string PluginVersion
    {
        get
        {
            byte[] buf = new byte[256];
            GetOVRPVersion(buf);
            return System.Text.Encoding.ASCII.GetString(buf);
        }
    }

    public static class AdaptivePerformance
    {
        public static float GPUAppTime
        {
            get
            {
                float val;
                ((XRDisplaySubsystem) GetFirstDisplaySubsystem()).TryGetAppGPUTimeLastFrame(out val);
                return val;
            }
        }
        
        public static float GPUCompositorTime
        {
            get
            {
                float val;
                ((XRDisplaySubsystem) GetFirstDisplaySubsystem()).TryGetCompositorGPUTimeLastFrame(out val);
                return val;
            }
        }
        
        public static float MotionToPhoton
        {
            get
            {
                float val;
                ((XRDisplaySubsystem) GetFirstDisplaySubsystem()).TryGetMotionToPhoton(out val);
                return val;
            }
        }

        public static float RefreshRate
        {
            get
            {
                float val;
                ((XRDisplaySubsystem) GetFirstDisplaySubsystem()).TryGetDisplayRefreshRate(out val);
                return val;
            }
        }
        
        /// Gets the current battery temperature in degrees Celsius.
        public static float BatteryTemp
        {
            get
            {
                float batteryTemp;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "batteryTemperature", out batteryTemp);
                return batteryTemp;
            }
        }
        
        /// Gets the current available battery charge, ranging from 0 (empty) to 1 (full).
        public static float BatteryLevel
        {
            get
            {
                float batteryLevel;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "batteryLevel", out batteryLevel);
                return batteryLevel;
            }
        }

        /// If true, the system is running in a reduced performance mode to save power.
        public static bool PowerSavingMode
        {
            get
            {
                float powerSavingMode;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "powerSavingMode", out powerSavingMode);
                return !(powerSavingMode == 0.0f);
            }
        }

        /// Returns the recommended amount to scale GPU work in order to maintain framerate.
        /// Can be used to adjust viewportScale and textureScale
        public static float AdaptivePerformanceScale
        {
            get
            {
                float performanceScale;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "adaptivePerformanceScale", out performanceScale);
                return performanceScale;
            }
        }

        /// Gets the current CPU performance level, integer in the range 0 - 3.
        public static int CPULevel
        {
            get
            {
                float cpuLevel;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "cpuLevel", out cpuLevel);
                return (int) cpuLevel;
            }
        }
        
        /// Gets the current GPU performance level, integer in the range 0 - 3.
        public static int GPULevel
        {
            get
            {
                float gpuLevel;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "gpuLevel", out gpuLevel);
                return (int) gpuLevel;
            }
        }
    }

    public static class PerfMetrics
    {
        public static float AppCPUTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.appcputime", out val);
                return val;
            }
        }
        
        public static float AppGPUTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.appgputime", out val);
                return val;
            }
        }


        public static float CompositorCPUTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.compositorcputime", out val);
                return val;
            }
        }
        
        public static float CompositorGPUTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.compositorgputime", out val);
                return val;
            }
        }
        
        public static float GPUUtilization
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.gpuutil", out val);
                return val;
            }
        }

        
        public static float CPUUtilizationAverage
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.cpuutilavg", out val);
                return val;
            }
        }
        
        public static float CPUUtilizationWorst
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.cpuutilworst", out val);
                return val;
            }
        }

        public static float CPUClockFrequency
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.cpuclockfreq", out val);
                return val;
            }
        }        
        
        public static float GPUClockFrequency
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "perfmetrics.gpuclockfreq", out val);
                return val;
            }
        }
        
        [DllImport("OculusXRPlugin", CharSet = CharSet.Auto)]
        public static extern void EnablePerfMetrics(bool enable);
    }

    public static class AppMetrics
    {
        public static float AppQueueAheadTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.appqueueahead", out val);
                return val;
            }
        }
        
        public static float AppCPUElapsedTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.cpuelapsedtime", out val);
                return val;
            }
        }
        
        public static float CompositorDroppedFrames
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.compositordroppedframes", out val);
                return val;
            }
        }
        
        public static float CompositorLatency
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.compositorlatency", out val);
                return val;
            }
        }
        
        public static float CompositorCPUTime
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.compositorcputime", out val);
                return val;
            }
        }
        
        public static float CPUStartToGPUEnd
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.compositorcpustartgpuendelapsedtime", out val);
                return val;
            }
        }
        
        public static float GPUEndToVsync
        {
            get
            {
                float val;
                XRStats.TryGetStat(GetFirstDisplaySubsystem(), "appstats.compositorgpuendtovsyncelapsedtime", out val);
                return val;
            }
        }
        
        [DllImport("OculusXRPlugin", CharSet = CharSet.Auto)]
        public static extern void EnableAppMetrics(bool enable);
    }


    private static IntegratedSubsystem GetFirstDisplaySubsystem()
    {
        if (m_Display != null)
            return m_Display;
        List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances(displays);
        if (displays.Count == 0)
        {
            Debug.LogError("No display subsystem found.");
            return null;
        }
        m_Display = displays[0];
        return m_Display;
    }
}
