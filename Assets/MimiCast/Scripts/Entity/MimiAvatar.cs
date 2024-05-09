using System.Linq;
using UniGLTF;
using UniGLTF.Utils;
using UnityEngine;
using UniVRM10;

namespace MimiCast.Scripts.Entity
{
    public class MimiAvatar
    {

        public Vrm10Instance Instance
        {
            get;
            private set;
        }
        
        public RuntimeGltfInstance ModelData
        {
            get;
            private set;
        }

        public Transform Head
        {
            get;
            private set;
        }
        
        public MimiAvatar(Vrm10Instance instance)
        {
            Instance = instance;
            ModelData = instance.GetComponent<RuntimeGltfInstance>();
            ModelData.EnableUpdateWhenOffscreen();
          
            var animator = instance.GetComponent<Animator>();
            Head = instance.Runtime.ControlRig.GetBoneTransform(HumanBodyBones.Head);
            
        }
        
        public void Dispose()
        {
            GameObject.Destroy(ModelData.gameObject);
        }
        
        public static string GetHierarchyPath(GameObject gameObject )
        {
            var path = gameObject.name;
            var parent = gameObject.transform.parent;

            while ( parent != null )
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }

            return path;
        }
        
    }
}