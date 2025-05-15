using System.Linq;
using System.Collections.Generic;
using UnityEngine;
#if VRC_SDK_VRCSDK3
using VRC.SDK3.Dynamics.PhysBone.Components;
#endif

namespace Narazaka.Unity.BoneTools
{
    public class PhysBoneSelector
    {
        public Transform Root;

        public PhysBoneSelector(Transform root)
        {
            Root = root;
        }

#if VRC_SDK_VRCSDK3

        public IEnumerable<VRCPhysBone> GetPhysBonesByRootTransforms(IEnumerable<Transform> bones)
        {
            var bonesSet = new HashSet<Transform>(bones);
            return GetAllPhysBones().Where(pb => pb.rootTransform == null ? bonesSet.Contains(pb.transform) : bonesSet.Contains(pb.rootTransform));
        }

        public IEnumerable<VRCPhysBone> GetRelatedPhysBones(IEnumerable<BoneReference> boneReferences)
        {
            var bonesSet = new HashSet<Transform>(boneReferences.Select(r => r.Bone).Concat(boneReferences.SelectMany(r => r.Parents)));
            return GetAllPhysBones().Where(pb => pb.rootTransform == null ? bonesSet.Contains(pb.transform) : bonesSet.Contains(pb.rootTransform));
        }


        public IEnumerable<VRCPhysBoneCollider> GetPhysBoneRelatedColliders(IEnumerable<VRCPhysBone> pbs)
        {
            return pbs.SelectMany(pb => pb.colliders).Select(cb => cb as VRCPhysBoneCollider).Where(c => c != null);
        }

        public IEnumerable<VRCPhysBoneCollider> GetPhysBoneColliders(IEnumerable<Transform> bones)
        {
            var bonesSet = new HashSet<Transform>(bones);
            return GetAllPhysBoneColliders().Where(c => c.rootTransform == null ? bonesSet.Contains(c.transform) : bonesSet.Contains(c.rootTransform));
        }

        public VRCPhysBone[] GetAllPhysBones()
        {
            return Root.GetComponentsInChildren<VRCPhysBone>(true);
        }

        public VRCPhysBoneCollider[] GetAllPhysBoneColliders()
        {
            return Root.GetComponentsInChildren<VRCPhysBoneCollider>(true);
        }
#endif
    }
}
