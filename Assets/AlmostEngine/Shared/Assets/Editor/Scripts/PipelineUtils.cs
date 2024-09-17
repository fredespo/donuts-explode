using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


namespace AlmostEngine
{
    [InitializeOnLoad]
    public class PipelineUtils
    {
        public enum PipelineType
        {
            Unsupported,
            BuiltInPipeline,
            UniversalPipeline,
            HDPipeline
        }

        public static PipelineType GetCurrentPipeline()
        {
#if UNITY_2019_1_OR_NEWER
            if (GraphicsSettings.renderPipelineAsset != null)
            {
                // SRP
                var srpType = GraphicsSettings.renderPipelineAsset.GetType().ToString();
                if (srpType.Contains("HDRenderPipelineAsset"))
                {
                    return PipelineType.HDPipeline;
                }
                else if (srpType.Contains("UniversalRenderPipelineAsset") || srpType.Contains("LightweightRenderPipelineAsset"))
                {
                    return PipelineType.UniversalPipeline;
                }
                else return PipelineType.Unsupported;
            }
#elif UNITY_2017_1_OR_NEWER
            if (GraphicsSettings.renderPipelineAsset != null)
            {
                // SRP not supported before 2019
                return PipelineType.Unsupported;
            }
#endif
            // no SRP
            return PipelineType.BuiltInPipeline;
        }


        static PipelineUtils()
        {
            AutoSetDefines();
        }

        static void AutoSetDefines()
        {
            var pipeline = GetCurrentPipeline();
            var activeTarget = EditorUserBuildSettings.activeBuildTarget;
            var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(activeTarget);

            if (pipeline == PipelineType.UniversalPipeline)
            {
                SymbolsUtils.AddDefine(buildTargetGroup, "USC_UNITY_PIPELINE_URP");
            }
            else
            {
                SymbolsUtils.RemoveDefine(buildTargetGroup, "USC_UNITY_PIPELINE_URP");
            }
            if (pipeline == PipelineType.HDPipeline)
            {
                SymbolsUtils.AddDefine(buildTargetGroup, "USC_UNITY_PIPELINE_HDRP");
            }
            else
            {
                SymbolsUtils.RemoveDefine(buildTargetGroup, "USC_UNITY_PIPELINE_HDRP");
            }
            if (pipeline == PipelineType.BuiltInPipeline)
            {
                SymbolsUtils.AddDefine(buildTargetGroup, "USC_UNITY_PIPELINE_LEGACY");
            }
            else
            {
                SymbolsUtils.RemoveDefine(buildTargetGroup, "USC_UNITY_PIPELINE_LEGACY");
            }
        }


    }
}