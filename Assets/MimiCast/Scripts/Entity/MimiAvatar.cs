using UniGLTF;
using UnityEngine;
using UniVRM10;

namespace MimiCast.Scripts.Entity
{
    public class MimiAvatar
    {
        public RuntimeGltfInstance ModelData
        {
            get;
            private set;
        }
        
        public MimiAvatar(RuntimeGltfInstance runtime)
        {
            ModelData = runtime;
            
            var animation = ModelData.GetComponent<Animation>();
            if (animation && animation.clip != null)
            {
                animation.Play(animation.clip.name);
            }
        }
        
        public void Dispose()
        {
            GameObject.Destroy(ModelData.gameObject);
        }
        
    }
}